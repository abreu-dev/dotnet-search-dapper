using DotNetSearch.Infra.CrossCutting.Search.Contratos;
using DotNetSearch.Infra.CrossCutting.Search.Enums;
using DotNetSearch.Infra.CrossCutting.Search.Helpers;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace DotNetSearch.Infra.CrossCutting.Search.Tests.Helpers
{
    public class LinqLambdaBuilderTests
    {
        #region BuildPredicate
        [Fact]
        public void BuildPredicate_ShouldBuildExpressionWithOneFilter()
        {
            var expected = "x => (x.Nome == \"Terror\")";
            var searchContrato = new SearchContrato()
            {
                Filters = new List<SearchFilterContrato>()
                {
                    new SearchFilterContrato()
                    {
                        PropertyName = "Nome",
                        PropertyValue = "Terror",
                        Operation = SearchFilterOperation.Equals
                    }
                }
            };

            var actual = LinqLambdaBuilder.BuildPredicate<MyParentConcreteClass>(searchContrato);

            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void BuildPredicate_ShouldBuildExpressionWithTwoFilters()
        {
            var expected = "x => ((x.Nome == \"Terror\") OrElse (x.Nome == \"Comédia\"))";
            var searchContrato = new SearchContrato()
            {
                Filters = new List<SearchFilterContrato>()
                {
                    new SearchFilterContrato()
                    {
                        PropertyName = "Nome",
                        PropertyValue = "Terror",
                        Operation = SearchFilterOperation.Equals
                    },
                    new SearchFilterContrato()
                    {
                        PropertyName = "Nome",
                        PropertyValue = "Comédia",
                        Operation = SearchFilterOperation.Equals
                    }
                }
            };

            var actual = LinqLambdaBuilder.BuildPredicate<MyParentConcreteClass>(searchContrato);

            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region Equal
        [Fact]
        public void Equal_ShouldBuildExpressionEqualWithParentProperty()
        {
            var expected = "x => (x.Nome == \"Terror\")";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(MyParentConcreteClass), "x");

            var actual = LinqLambdaBuilder.Equal<MyParentConcreteClass>(parameterExpression, "Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion
        [Fact]
        public void Equal_ShouldBuildExpressionEqualWithChildProperty()
        {
            var expected = "x => (x.Child.Nome == \"Terror\")";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(MyParentConcreteClass), "x");

            var actual = LinqLambdaBuilder.Equal<MyParentConcreteClass>(parameterExpression, "Child.Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }

        #region Like
        [Fact]
        public void Like_ShouldBuildExpressionLikeWithParentProperty()
        {
            var expected = "x => x.Nome.Contains(\"Terror\")";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(MyParentConcreteClass), "x");

            var actual = LinqLambdaBuilder.Like<MyParentConcreteClass>(parameterExpression, "Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        [Fact]
        public void Like_ShouldBuildExpressionLikeWithChildProperty()
        {
            var expected = "x => x.Child.Nome.Contains(\"Terror\")";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(MyParentConcreteClass), "x");

            var actual = LinqLambdaBuilder.Like<MyParentConcreteClass>(parameterExpression, "Child.Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region And
        [Fact]
        public void And_ShouldBuildTwoExpressionTogetherWithAnd()
        {
            var expected = "x => ((x.Nome == \"Terror\") AndAlso (x.Nome == \"Comédia\"))";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(MyParentConcreteClass), "x");
            var firstExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>(parameterExpression, "Nome", "Terror");
            var secondExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>(parameterExpression, "Nome", "Comédia");

            var actual = firstExpression.And(secondExpression);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region Or
        [Fact]
        public void Or_ShouldBuildTwoExpressionTogetherWithOr()
        {
            var expected = "x => ((x.Nome == \"Terror\") OrElse (x.Nome == \"Comédia\"))";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(MyParentConcreteClass), "x");
            var firstExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>(parameterExpression, "Nome", "Terror");
            var secondExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>(parameterExpression, "Nome", "Comédia");

            var actual = firstExpression.Or(secondExpression);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion
    }

    public class MyParentConcreteClass 
    {
        public string Nome { get; set; }
        public MyChildConcreteClass Child { get; set; }
    }

    public class MyChildConcreteClass
    {
        public string Nome { get; set; }
    }
}
