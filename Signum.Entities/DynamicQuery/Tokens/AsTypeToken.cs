using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Utilities;
using System.Linq.Expressions;
using Signum.Entities.Basics;

namespace Signum.Entities.DynamicQuery
{

    [Serializable]
    public class AsTypeToken : QueryToken
    {
        Type entityType;
        internal AsTypeToken(QueryToken parent, Type type)
            : base(parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (type == null)
                throw new ArgumentNullException("type");

            this.entityType = type;

            this.Priority = 8;
        }

        public override Type Type
        {
            get { return entityType.BuildLite(); }
        }

        public override string ToString()
        {
            return QueryTokenMessage.As0.NiceToString().Formato(Type.NiceName());
        }

        public override string Key
        {
            get { return "({0})".Formato(TypeDN.GetCleanName(entityType)); }
        }

        protected override Expression BuildExpressionInternal(BuildExpressionContext context)
        {
            Expression baseExpression = Parent.BuildExpression(context);

            Expression result = Expression.TypeAs(baseExpression.ExtractEntity(false), entityType);

            return result.BuildLite();
        }

        protected override List<QueryToken> SubTokensOverride()
        {
            return SubTokensBase(entityType, GetImplementations());
        }

        public override string Format
        {
            get { return null; }
        }

        public override string Unit
        {
            get { return null; }
        }

        public override Implementations? GetImplementations()
        {
            return Implementations.By(entityType);
        }

        public override string IsAllowed()
        {
            var parent = Parent.IsAllowed();
            var routes = GetPropertyRoute().IsAllowed();

            if (parent.HasText() && routes.HasText())
                QueryTokenMessage.And.NiceToString().Combine(parent, routes);

            return parent ?? routes;
        }

        public override PropertyRoute GetPropertyRoute()
        {
            return PropertyRoute.Root(entityType);
        }

        public override string NiceName()
        {
            return QueryTokenMessage._0As1.NiceToString().Formato(Parent.ToString(), entityType.NiceName());
        }

        public override QueryToken Clone()
        {
            return new AsTypeToken(Parent.Clone(), entityType);
        }
    }
    
}
