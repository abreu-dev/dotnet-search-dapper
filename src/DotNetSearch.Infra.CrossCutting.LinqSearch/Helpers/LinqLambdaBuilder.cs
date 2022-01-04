using DotNetSearch.Infra.CrossCutting.Search.Contratos;
using DotNetSearch.Infra.CrossCutting.Search.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DotNetSearch.Infra.CrossCutting.Search.Helpers
{
    public static class LinqLambdaBuilder
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(SearchContrato searchContrato)
        {
            Expression<Func<T, bool>> predicate = null;
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");

            foreach (var filter in searchContrato.Filters)
            {
                Expression<Func<T, bool>> filterExpression = null;

                if (filter.Operation == SearchFilterOperation.Like)
                {
                    filterExpression = Like<T>(parameterExpression, filter.PropertyName, filter.PropertyValue);
                }
                else
                {
                    filterExpression = Equal<T>(parameterExpression, filter.PropertyName, filter.PropertyValue);
                }

                if (predicate == null)
                {
                    predicate = filterExpression;
                } 
                else
                {
                    predicate = predicate.Or(filterExpression);
                }
            }

            return predicate;
        }

        public static Expression<Func<T, bool>> Equal<T>(ParameterExpression parameterExpression, 
            string propertyName, string propertyValue)
        {
            Expression propertyExpression = BuildExpressionProperty<T>(propertyName, parameterExpression);
            ConstantExpression constantExpression = Expression.Constant(propertyValue, typeof(string));
            BinaryExpression equalExpression = Expression.Equal(propertyExpression, constantExpression);

            return Expression.Lambda<Func<T, bool>>(equalExpression, parameterExpression);
        }

        public static Expression<Func<T, bool>> Like<T>(ParameterExpression parameterExpression, 
            string propertyName, string propertyValue)
        {
            Expression propertyExpression = BuildExpressionProperty<T>(propertyName, parameterExpression);
            MethodInfo methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression constantExpression = Expression.Constant(propertyValue, typeof(string));
            MethodCallExpression containsExpression = Expression.Call(propertyExpression, methodInfo, constantExpression);

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
        
        private static Expression BuildExpressionProperty<T>(this string propertyName, ParameterExpression parameterExpression)
        {
            Expression propertyExpression = parameterExpression;

            foreach (var property in propertyName.Split('.'))
            {
                propertyExpression = Expression.Property(propertyExpression, property);
            }

            return propertyExpression;
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
