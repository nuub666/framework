﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Signum.Entities;
using Signum.Utilities;
using Signum.Utilities.Reflection;
using Signum.Utilities.DataStructures;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using Signum.Utilities.ExpressionTrees;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace Signum.Entities.Reflection
{
    /* Fields
     *   Value
     *   Modifiables
     *      MList
     *      EmbeddedEntities
     *      IdentifiableEntities
     *      
     * An identifiable can be accesed thought:
     *   Normal Reference
     *   Interface
     *   Lite
     */


    public static class Reflector
    {
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        static Reflector()
        {
            DescriptionManager.CleanTypeName = CleanTypeName; //To allow MyEntityDN
            DescriptionManager.CleanType = t => EnumEntity.Extract(t) ?? t.CleanType(); //To allow Lite<T>

            DescriptionManager.DefaultDescriptionOptions += DescriptionManager_IsEnumsInEntities;
            DescriptionManager.DefaultDescriptionOptions += DescriptionManager_IsQuery;
            DescriptionManager.DefaultDescriptionOptions += DescriptionManager_IsSymbolContainer;
            DescriptionManager.DefaultDescriptionOptions += DescriptionManager_IsIIdentifiable;

            DescriptionManager.ShouldLocalizeMemeber += DescriptionManager_ShouldLocalizeMemeber;
            DescriptionManager.Invalidate();
        }

        static bool DescriptionManager_ShouldLocalizeMemeber(MemberInfo arg)
        {
            return !arg.HasAttribute<HiddenPropertyAttribute>();
        }

        static ResetLazy<HashSet<Type>> EnumsInEntities = new ResetLazy<HashSet<Type>>(() =>
        {
            return (from a in AppDomain.CurrentDomain.GetAssemblies().Where(a => a.HasAttribute<DefaultAssemblyCultureAttribute>())
                    from t in a.GetTypes()
                    where typeof(IIdentifiable).IsAssignableFrom(t) || typeof(ModifiableEntity).IsAssignableFrom(t)
                    let da = t.SingleAttributeInherit<DescriptionOptionsAttribute>()
                    where da == null || da.Options.IsSet(DescriptionOptions.Members)
                    from p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    where DescriptionManager.OnShouldLocalizeMember(p)
                    let et = p.PropertyType.UnNullify()
                    where et.IsEnum && et.Assembly.HasAttribute<DefaultAssemblyCultureAttribute>()
                    select et).ToHashSet();
        });

        static DescriptionOptions? DescriptionManager_IsEnumsInEntities(Type t)
        {
            return EnumsInEntities.Value.Contains(t) ? DescriptionOptions.Members | DescriptionOptions.Description : (DescriptionOptions?)null;
        }

        static DescriptionOptions? DescriptionManager_IsIIdentifiable(Type t)
        {
             return t.IsInterface && typeof(IIdentifiable).IsAssignableFrom(t) ? DescriptionOptions.Members : (DescriptionOptions?)null;
        }

        static DescriptionOptions? DescriptionManager_IsQuery(Type t)
        {
            return t.IsEnum && t.Name.EndsWith("Query") ? DescriptionOptions.Members : (DescriptionOptions?)null;
        }

        static DescriptionOptions? DescriptionManager_IsSymbolContainer(Type t)
        {
            return t.IsAbstract && t.IsSealed && 
                t.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Any(a => typeof(Symbol).IsAssignableFrom(a.FieldType) || typeof(IOperationSymbolContainer).IsAssignableFrom(a.FieldType)) ? DescriptionOptions.Members : (DescriptionOptions?)null;
        }

        public static string CleanTypeName(Type t)
        {
            return t.Name.RemovePostfix("DN").RemovePostfix("Model");
        }

        static string RemovePostfix(this string text, string postfix)
        {
            if (text.EndsWith(postfix) && text != postfix)
                return text.Substring(0, text.Length - postfix.Length);

            return text;
        }

        public static bool IsMList(this Type ft)
        {
            return ft.ElementType() != null && IsModifiable(ft);
        }

        public static bool IsModifiable(this Type t)
        {
            return typeof(Modifiable).IsAssignableFrom(t);
        }

        public static bool IsIIdentifiable(this Type type)
        {
            return typeof(IIdentifiable).IsAssignableFrom(type);
        }

        public static bool IsIRootEntity(this Type type)
        {
            return typeof(IRootEntity).IsAssignableFrom(type);
        }

        public static bool IsModifiableEntity(this Type type)
        {
            return typeof(ModifiableEntity).IsAssignableFrom(type);
        }

        public static bool IsModifiableIdentifiableOrLite(this Type t)
        {
            return t.IsModifiable() || t.IsIIdentifiable() || t.IsLite();
        }

        public static bool IsIdentifiableEntity(this Type ft)
        {
            return typeof(IdentifiableEntity).IsAssignableFrom(ft);
        }

        public static bool IsEmbeddedEntity(this Type t)
        {
            return typeof(EmbeddedEntity).IsAssignableFrom(t);
        }

        public static FieldInfo[] InstanceFieldsInOrder(Type type)
        {
            var result = type.For(t => t != typeof(object), t => t.BaseType)
                .Reverse()
                .SelectMany(t => t.GetFields(flags | BindingFlags.DeclaredOnly).OrderBy(f => f.MetadataToken)).ToArray();

            return result;
        }

        public static PropertyInfo[] PublicInstanceDeclaredPropertiesInOrder(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                       .Where(p => !p.HasAttribute<HiddenPropertyAttribute>())
                       .OrderBy(f => f.MetadataToken).ToArray();
        }

        public static PropertyInfo[] PublicInstancePropertiesInOrder(Type type)
        {
            Dictionary<string, PropertyInfo> properties = new Dictionary<string,PropertyInfo>();

            foreach (var t in type.Follow(t => t.BaseType).Reverse())
            {
                foreach (var pi in PublicInstanceDeclaredPropertiesInOrder(t))
	            {
                    properties[pi.Name] = pi;
	            }
            }

            return properties.Values.ToArray();
        }

        public static MemberInfo[] GetMemberList<T, S>(Expression<Func<T, S>> lambdaToField)
        {
            Expression e = lambdaToField.Body;

            UnaryExpression ue = e as UnaryExpression;
            if (ue != null && ue.NodeType == ExpressionType.Convert && ue.Type == typeof(object))
                e = ue.Operand;

            MemberInfo[] result = GetMemberListBase(e);

            return result;
        }

        public static MemberInfo[] GetMemberListBase(Expression e)
        {
            return e.Follow(NextExpression).Select(GetMember).NotNull().Reverse().ToArray();
        }

        static Expression NextExpression(Expression e)
        {
            switch (e.NodeType)
            {
                case ExpressionType.MemberAccess: return ((MemberExpression)e).Expression;
                case ExpressionType.Call:
                    {
                        MethodCallExpression mce = (MethodCallExpression)e;

                        var parent = mce.Method.IsExtensionMethod() ? mce.Arguments.FirstEx() : mce.Object;

                        if (parent != null)
                            return parent;

                        break;
                    }
                case ExpressionType.Convert: return ((UnaryExpression)e).Operand;
                case ExpressionType.Parameter: return null;
            }

            throw new InvalidCastException("Not supported {0}".Formato(e.NodeType));
        }

        static readonly string[] collectionMethods = new[] { "Element" };

        static MemberInfo GetMember(Expression e)
        {
            switch (e.NodeType)
            {
                case ExpressionType.MemberAccess:
                {
                    MemberExpression me = (MemberExpression)e;
                    if (me.Member.DeclaringType.IsLite() && !me.Member.Name.StartsWith("Entity"))
                        throw new InvalidOperationException("Members of Lite not supported"); 

                    return me.Member;
                }
                case ExpressionType.Call:
                {
                    MethodCallExpression mce = (MethodCallExpression)e;

                    var parent = mce.Method.IsExtensionMethod() ? mce.Arguments.FirstEx() : mce.Object; 

                    if(parent != null && parent.Type.ElementType() == e.Type)
                        return parent.Type.GetProperty("Item");

                    return mce.Method;
                }
                case ExpressionType.Convert: return ((UnaryExpression)e).Type;
                case ExpressionType.Parameter: return null;
                default: throw new InvalidCastException("Not supported {0}".Formato(e.NodeType));
            }
        }

        internal static FieldInfo FindFieldInfo(Type type, PropertyInfo value)
        {
            var fi = TryFindFieldInfo(type, value);

            if (fi == null)
                throw new InvalidOperationException("No FieldInfo for '{0}' found on '{1}'".Formato(value.Name, type.Name));

            return fi;
        }

        static readonly BindingFlags privateFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic;
        public static FieldInfo TryFindFieldInfo(Type type, PropertyInfo pi)
        {
            FieldInfo fi = null;
            for (Type tempType = type; tempType != null && fi == null; tempType = tempType.BaseType)
            {
                fi = (tempType.GetField(pi.Name, privateFlags) ??
                      tempType.GetField("m" + pi.Name, privateFlags) ??
                      tempType.GetField("_" + pi, privateFlags));
            }

            return fi;
        }

        public static PropertyInfo FindPropertyInfo(FieldInfo fi)
        {
            var pi = TryFindPropertyInfo(fi);

            if (pi == null)
                throw new InvalidOperationException("No PropertyInfo for '{0}' found".Formato(fi.Name));

            return pi;
        }

        public static PropertyInfo TryFindPropertyInfo(FieldInfo fi)
        {
            const BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var propertyName = PropertyName(fi.Name);

            var result = fi.DeclaringType.GetProperty(propertyName, flags, null, null, new Type[0], null);

            if (result != null)
                return result;

            foreach (Type i in fi.DeclaringType.GetInterfaces())
            {
                result = fi.DeclaringType.GetProperty(i.FullName + "." + propertyName, flags);

                if (result != null)
                    return result;
            }

            return null;
        }
        
        public static Func<string, string> PropertyName = (string fieldName) =>
        {
            //if (name.Length > 2)
            //{
            //    if (name.StartsWith("_"))
            //        name = name.Substring(1);
            //    else if (name.StartsWith("m") && char.IsUpper(name[1]))
            //        name = Char.ToLower(name[1]) + name.Substring(2);
            //}

            return fieldName.FirstUpper();
        };

        public static bool QueryableProperty(Type type, PropertyInfo pi)
        {
            QueryablePropertyAttribute spa = pi.SingleAttribute<QueryablePropertyAttribute>();
            if (spa != null)
                return spa.AvailableForQueries;

            FieldInfo fi = TryFindFieldInfo(type, pi);
            if (fi != null && !fi.HasAttribute<IgnoreAttribute>())
                return true;

            if (ExpressionCleaner.HasExpansions(type, pi))
                return true;

            return false;
        }

        public static Func<IFormattable, string> GetPropertyFormatter(string format, string unitName)
        {
            if (format != null)
            {
                if (unitName != null)
                    return a => a == null ? null : a.ToString(format, CultureInfo.CurrentCulture) + " " + unitName;
                else
                    return a => a == null ? null : a.ToString(format, CultureInfo.CurrentCulture);
            }
            else
            {
                if (unitName != null)
                    return a => a == null ? null : a.ToString() + " " + unitName;
                else
                    return a => a == null ? null : a.ToString();
            }
        }

        public static string FormatString(PropertyRoute route)
        {
            PropertyRoute simpleRoute = route.SimplifyNoRoot();

            FormatAttribute format = simpleRoute.PropertyInfo.SingleAttribute<FormatAttribute>();
            if (format != null)
                return format.Format;

            var pp = Validator.TryGetPropertyValidator(simpleRoute);
            if (pp != null)
            {
                DateTimePrecissionValidatorAttribute datetimePrecission = pp.Validators.OfType<DateTimePrecissionValidatorAttribute>().SingleOrDefaultEx();
                if (datetimePrecission != null)
                    return datetimePrecission.FormatString;

                TimeSpanPrecissionValidatorAttribute timeSpanPrecission = pp.Validators.OfType<TimeSpanPrecissionValidatorAttribute>().SingleOrDefaultEx();
                if (timeSpanPrecission != null)
                    return timeSpanPrecission.FormatString;

                DecimalsValidatorAttribute decimals = pp.Validators.OfType<DecimalsValidatorAttribute>().SingleOrDefaultEx();
                if (decimals != null)
                    return "N" + decimals.DecimalPlaces;

                StringCaseValidatorAttribute stringCase = pp.Validators.OfType<StringCaseValidatorAttribute>().SingleOrDefaultEx();
                if (stringCase != null)
                    return stringCase.TextCase == Case.Lowercase ? "L" : "U";
            }

            return FormatString(route.Type);
        }

        public static string FormatString(Type type)
        {
            type = type.UnNullify();
            if (type.IsEnum)
                return null;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    return "g";
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return "D";
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return "N2";
            }
            return null;
        }

        public static bool IsNumber(Type type)
        {
            type = type.UnNullify();
            if (type.IsEnum)
                return false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }
            return false;
        }

        public static bool IsDecimalNumber(Type type)
        {
            type = type.UnNullify();
            if (type.IsEnum)
                return false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
            }
            return false;
        }

        static readonly Regex validIdentifier = new Regex(@"^[_\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nl}][_\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nl}\p{Nd}]*$");
        public static bool ValidIdentifier(string identifier)
        {
            return validIdentifier.IsMatch(identifier);
        }

        public static void AssertValidIdentifier(string step)
        {
            if (!ValidIdentifier(step))
                throw new FormatException("'{0}' is not a valid identifier".Formato(step));
        }
    }
}
