using DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Helpers
{
    public static class LinqLambdaBuilder
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(SearchContrato searchContrato)
        {
            var firstExpression = Equal<T>("Nome", searchContrato.Filters.First().PropertyValue);
            var secondExpression = Equal<T>("Nome", "Comédia");

            return firstExpression.Or(secondExpression);
        }

        public static Expression<Func<T, bool>> Equal<T>(string propertyName, string propertyValue)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            MemberExpression memberExpression = Expression.PropertyOrField(parameterExpression, propertyName);
            ConstantExpression constantExpression = Expression.Constant(propertyValue, typeof(string));
            BinaryExpression equalExpression = Expression.Equal(memberExpression, constantExpression);

            return Expression.Lambda<Func<T, bool>>(equalExpression, parameterExpression);
        }

        public static Expression<Func<T, bool>> Like<T>(string propertyName, string propertyValue)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            MemberExpression memberExpression = Expression.PropertyOrField(parameterExpression, propertyName);
            MethodInfo methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constantExpression = Expression.Constant(propertyValue, typeof(string));
            MethodCallExpression containsExpression = Expression.Call(memberExpression, methodInfo, constantExpression);

            return Expression.Lambda<Func<T, bool>>(containsExpression, parameterExpression);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[b.Parameters[0]] = p;

            Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.subst[b.Parameters[0]] = p;

            Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
    }

    internal class SubstExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;
            if (subst.TryGetValue(node, out newValue))
            {
                return newValue;
            }
            return node;
        }
    }

}
