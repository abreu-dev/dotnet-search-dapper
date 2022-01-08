using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace DotNetSearch.Console
{
    static class Program
    {
        static void Main()
        {
            System.Console.WriteLine("Beginning\n\n");

            System.Console.WriteLine(FormatWhere());

            System.Console.WriteLine("\n\nFinal");
            System.Console.ReadKey();
        }

        static string FormatWhere()
        {
            SqlBuilder sqlBuilder = new SqlBuilder();
            ParameterExpression parameterBuilder = Expression.Parameter(typeof(SqlBuilder), "builder");
            LambdaExpression lambdaExpression = DynamicExpressionParser.ParseLambda(
                new ParameterExpression[] { parameterBuilder },
                null,
                "builder.Or(\"Nome\",\"Nome\")");

            var result = lambdaExpression.Compile().DynamicInvoke(sqlBuilder);
            return result.ToString();
        }
    }

    internal abstract class BaseBuilder
    {
        public abstract object Or(object o1, object o2);

        public abstract object Like(string fieldPath, string value);
    }

    internal class SqlBuilder : BaseBuilder
    {
        public override object Or(object o1, object o2)
        {
            return "OuIsso";
        }

        public override object Like(string fieldPath, string value)
        {
            return "TipoIsso";
        }
    }
}
