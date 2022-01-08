using DotNetSearch.Domain.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace DotNetSearch.Infra.Data.Filters
{
    public class PostgreSqlFilterParser : IFilterParser
    {
        private readonly string _tablename;

        public PostgreSqlFilterParser(string tablename)
        {
            _tablename = tablename;
        }

        public string Parse(string filter)
        {
            foreach (var op in typeof(IFilterParser).GetRuntimeMethods().Select(x => x.Name))
            {
                filter = filter.Replace($"{op}(", $"parser.{op}(");
            }

            var exp = DynamicExpressionParser.ParseLambda(new[] { Expression.Parameter(GetType(), "parser") },
                typeof(string),
                filter);

            return exp.Compile().DynamicInvoke(this).ToString();
        }

        public string BuildPropertyPath(string propertyPath)
        {
            return $"{_tablename}.\"{propertyPath.FirstCharToUpper()}\"";
        }

        public string And(string leftCondition, string rightCondition)
        {
            return $"{leftCondition} AND {rightCondition}";
        }

        public string Or(string leftCondition, string rightCondition)
        {
            return $"{leftCondition} OR {rightCondition}";
        }

        public string Eq(string propertyPath, string value)
        {
            return $"{BuildPropertyPath(propertyPath)} = '{value}'";
        }

        public string Ne(string propertyPath, string value)
        {
            return $"{BuildPropertyPath(propertyPath)} != '{value}'";
        }

        public string Like(string propertyPath, string value)
        {
            return $"LOWER({BuildPropertyPath(propertyPath)}) LIKE '%{value.ToLower()}%'";
        }
    }
}
