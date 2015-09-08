using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTransformer
{
    public static class ExpressionTransformer<From, To>
    {
        private class Visitor<TFrom, TTo> : ExpressionVisitor
        {
            public ParameterExpression ParameterExpression { get; private set; }

            public Visitor(ParameterExpression parameterExpression)
            {
                this.ParameterExpression = parameterExpression;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ParameterExpression;
            }

            /// <summary>
            /// Visit the children of MemberExpression
            /// </summary>
            /// <param name="node">Represents field or property</param>
            /// <returns>Modyfied children of MemberExpression</returns>
            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.DeclaringType == typeof(TFrom))
                {
                    return Expression.MakeMemberAccess(this.Visit(node.Expression), typeof(TTo).GetMember(node.Member.Name).FirstOrDefault());
                }
                else
                {
                    return base.VisitMember(node);
                }
            }
        }

        public static Expression<Func<To, bool>> Transform(Expression<Func<From, bool>> expression)
        {
            Visitor<From, To> expressionTransformer = new Visitor<From, To>(Expression.Parameter(typeof(To), expression.Parameters[0].Name));

            //Method "Visit" dispatches the expression or the list of expressions to one of the more specialized visit methods in this class
            return Expression.Lambda<Func<To, bool>>(expressionTransformer.Visit(expression.Body), expressionTransformer.ParameterExpression);
        }
    }
}
