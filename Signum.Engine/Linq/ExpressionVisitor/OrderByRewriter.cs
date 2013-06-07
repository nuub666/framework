﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using Signum.Utilities;
using Signum.Entities.DynamicQuery;
using Signum.Utilities.DataStructures;
using Signum.Utilities.ExpressionTrees;

namespace Signum.Engine.Linq
{
    internal class OrderByRewriter : DbExpressionVisitor
    {
        List<ColumnExpression> gatheredKeys;
        ReadOnlyCollection<OrderExpression> gatheredOrderings;
        SelectExpression outerMostSelect;

        private OrderByRewriter() { }

        public IDisposable Scope()
        {
            var oldOrderings = gatheredOrderings;
            var oldKeys = gatheredKeys;

            gatheredOrderings = null;
            gatheredKeys = null;
            return new Disposable(() =>
            {
                gatheredKeys = oldKeys;
                gatheredOrderings = oldOrderings;
            });
        }

        static internal Expression Rewrite(Expression expression)
        {
            return new OrderByRewriter().Visit(expression);
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            using (Scope())
            {
                var oldOuterMostSelect = outerMostSelect;
                outerMostSelect = proj.Select;

                var result = base.VisitProjection(proj);
                outerMostSelect = oldOuterMostSelect;

                return result;
            }

        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            bool isOuterMost = select == outerMostSelect;

            if (select.IsOrderAlsoByKeys && gatheredKeys == null)
                gatheredKeys = new List<ColumnExpression>();


            List<ColumnExpression> saveKeys = null;
            if (gatheredKeys != null && (select.IsDistinct || select.GroupBy.HasItems()))
                saveKeys = gatheredKeys.ToList();


            select = (SelectExpression)base.VisitSelect(select);

            if (saveKeys != null)
                gatheredKeys = saveKeys;

            List<ColumnDeclaration> newColumns = null;

            if (select.GroupBy.HasItems())
            {
                gatheredOrderings = null;

                if (gatheredKeys != null)
                {
                    ColumnGenerator cg = new ColumnGenerator(select.Columns);

                    var newKeys = new List<ColumnDeclaration>();

                    foreach (var ge in select.GroupBy)
                    {
                        var cd = cg.Columns.FirstOrDefault(a => DbExpressionComparer.AreEqual(a.Expression, ge));

                        if (cd != null)
                            newKeys.Add(cd);
                        else
                            newKeys.Add(cg.NewColumn(ge));
                    }

                    if (cg.Columns.Count() != select.Columns.Count)
                        newColumns = cg.Columns.ToList();

                    gatheredKeys.AddRange(newKeys.Select(cd => new ColumnExpression(cd.Expression.Type, select.Alias, cd.Name)));
                }
            }

            if (select.IsDistinct)
            {
                gatheredOrderings = null;

                if (gatheredKeys != null)
                {
                    gatheredKeys.AddRange(select.Columns.Select(cd => cd.GetReference(select.Alias)));
                }
            }

            if (select.IsReverse && !gatheredOrderings.IsNullOrEmpty())
                gatheredOrderings = gatheredOrderings.Select(o => new OrderExpression(
                    o.OrderType == OrderType.Ascending ? OrderType.Descending : OrderType.Ascending,
                    o.Expression)).ToReadOnly();  

            if (select.OrderBy != null && select.OrderBy.Count > 0)
                this.PrependOrderings(select.OrderBy);

            ReadOnlyCollection<OrderExpression> orderings = null;

            if(isOuterMost && !IsCountSumOrAvg(select) || select.Top != null)
            {
                if (select.Top != null)
                    AppendKeys();

                orderings = gatheredOrderings;
                gatheredOrderings = null;
            }

            if (AreEqual(select.OrderBy, orderings) && !select.IsReverse && newColumns == null)
                return select;

            return new SelectExpression(select.Alias, select.IsDistinct, select.Top, (IEnumerable<ColumnDeclaration>)newColumns ?? select.Columns,
                select.From, select.Where, orderings, select.GroupBy, select.SelectOptions & ~SelectOptions.Reverse);
        }


        protected override Expression VisitScalar(ScalarExpression scalar)
        {
            if (!scalar.Select.IsForXmlPathEmpty)
            {
                using (Scope())
                    return base.VisitScalar(scalar);
            }
            else
            {
                using (Scope())
                {
                    var oldOuterMostSelect = outerMostSelect;
                    outerMostSelect = scalar.Select;

                    var result = base.VisitScalar(scalar);
                    outerMostSelect = oldOuterMostSelect;

                    return result;
                }
            }
        }

        protected override Expression VisitExists(ExistsExpression exists)
        {
            using (Scope())
                return base.VisitExists(exists);
        }

        protected override Expression VisitIn(InExpression @in)
        {
            if (@in.Values != null)
                return base.VisitIn(@in);
            else
                using (Scope())
                    return base.VisitIn(@in);
        }

        static bool AreEqual(IEnumerable<OrderExpression> col1, IEnumerable<OrderExpression> col2)
        {
            bool col1Empty = col1 == null || col1.IsEmpty();
            bool col2Empty = col2 == null || col2.IsEmpty();

            if (col1Empty && col2Empty)
                return true;

            if (col1Empty || col2Empty)
                return false;

            return col1 == col2;
        }

        private bool IsCountSumOrAvg(SelectExpression select)
        {
            ColumnDeclaration col = select.Columns.Only();
            if (col == null)
                return false;

            Expression exp = col.Expression;

            if (exp is IsNullExpression)
                exp = ((IsNullExpression)exp).Expression;

            if (exp.NodeType == ExpressionType.Coalesce)
            {
                var be = ((BinaryExpression)exp);
                if (be.Right.NodeType == ExpressionType.Constant || be.Right.NodeType == (ExpressionType)DbExpressionType.SqlConstant)
                    exp = ((BinaryExpression)exp).Left;
            }

            AggregateExpression aggExp = exp as AggregateExpression;
            if (aggExp == null)
                return false;

            return aggExp.AggregateFunction == AggregateFunction.Count ||
                aggExp.AggregateFunction == AggregateFunction.Sum ||
                aggExp.AggregateFunction == AggregateFunction.Average;
        }

        protected override Expression VisitJoin(JoinExpression join)
        {
            SourceExpression left = this.VisitSource(join.Left);

            ReadOnlyCollection<OrderExpression> leftOrders = this.gatheredOrderings;
            this.gatheredOrderings = null;

            SourceExpression right = join.Right is TableExpression ? join.Right : this.VisitSource(join.Right);

            this.PrependOrderings(leftOrders);

            Expression condition = this.Visit(join.Condition);

            if (left != join.Left || right != join.Right || condition != join.Condition)
            {
                return new JoinExpression(join.JoinType, left, right, condition);
            }
            return join;
        }

        protected override Expression VisitSetOperator(SetOperatorExpression set)
        {
            using (Scope())
                return base.VisitSetOperator(set);
        }

        protected override Expression VisitTable(TableExpression table)
        {
            if (gatheredKeys != null)
                gatheredKeys.Add(table.GetIdExpression());

            return table;
        }

        protected void AppendKeys()
        {
            if(this.gatheredKeys.IsNullOrEmpty())
                return;

            if (this.gatheredOrderings.IsNullOrEmpty())
                this.gatheredOrderings = this.gatheredKeys.Select(a => new OrderExpression(OrderType.Ascending, a)).ToReadOnly();
            else
            {
                var hs = this.gatheredOrderings.Select(a => CleanCast(a.Expression)).OfType<ColumnExpression>().ToHashSet();
                var postOrders = this.gatheredKeys.Where(e => !hs.Contains(CleanCast(e))).Select(a => new OrderExpression(OrderType.Ascending, a));

                this.gatheredOrderings = this.gatheredOrderings.Concat(postOrders).ToReadOnly();
            }
        }

        static Expression CleanCast(Expression exp)
        {
            while (exp.NodeType == ExpressionType.Convert)
                exp = ((UnaryExpression)exp).Operand;

            return exp;
        }

        protected void PrependOrderings(ReadOnlyCollection<OrderExpression> newOrderings)
        {
            if (!newOrderings.IsNullOrEmpty())
            {
                if (this.gatheredOrderings.IsNullOrEmpty())
                {
                    this.gatheredOrderings = newOrderings;
                }
                else
                {
                    List<OrderExpression> list = this.gatheredOrderings.ToList();
                    list.InsertRange(0, newOrderings);
                    this.gatheredOrderings = list.ToReadOnly();
                }
            }
        }
    }
}
