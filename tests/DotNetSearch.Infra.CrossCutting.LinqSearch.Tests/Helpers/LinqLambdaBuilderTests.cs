using DotNetSearch.Infra.CrossCutting.LinqSearch.Helpers;
using Xunit;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Tests.Helpers
{
    public class LinqLambdaBuilderTests
    {
        //#region BuildPredicate
        //[Fact]
        //public void BuildPredicate()
        //{
        //    Expression<Func<MyEntityConcreteClass, bool>> expected = x => x.Nome == "Terror";
        //    var searchContrato = new SearchContrato()
        //    {
        //        Page = 1,
        //        PageSize = 10,
        //        Filters = new List<SearchFilterContrato>()
        //        {
        //            new SearchFilterContrato()
        //            {
        //                PropertyName = "Nome",
        //                PropertyValue = "Terror",
        //                Operation = SearchFilterOperation.Equals
        //            }
        //        }
        //    };

        //    var actual = LinqLambdaBuilder.BuildPredicate<MyEntityConcreteClass>(searchContrato);

        //    Assert.Equal(actual.ToString(), expected.ToString());
        //}
        //#endregion

        #region Equal
        [Fact]
        public void Equal_ShouldBuildExpressionEqualWithParentProperty()
        {
            var expected = "x => (x.Nome == \"Terror\")";

            var actual = LinqLambdaBuilder.Equal<MyParentConcreteClass>("Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion
        [Fact]
        public void Equal_ShouldBuildExpressionEqualWithChildProperty()
        {
            var expected = "x => (x.Child.Nome == \"Terror\")";

            var actual = LinqLambdaBuilder.Equal<MyParentConcreteClass>("Child.Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }

        #region Like
        [Fact]
        public void Like_ShouldBuildExpressionLikeWithParentProperty()
        {
            var expected = "x => x.Nome.Contains(\"Terror\")";

            var actual = LinqLambdaBuilder.Like<MyParentConcreteClass>("Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        [Fact]
        public void Like_ShouldBuildExpressionLikeWithChildProperty()
        {
            var expected = "x => x.Child.Nome.Contains(\"Terror\")";

            var actual = LinqLambdaBuilder.Like<MyParentConcreteClass>("Child.Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region And
        [Fact]
        public void And_ShouldBuildTwoExpressionTogetherWithAnd()
        {
            var expected = "x => ((x.Nome == \"Terror\") AndAlso (x.Nome == \"Comédia\"))";
            var firstExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>("Nome", "Terror");
            var secondExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>("Nome", "Comédia");

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
            var firstExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>("Nome", "Terror");
            var secondExpression = LinqLambdaBuilder.Equal<MyParentConcreteClass>("Nome", "Comédia");

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
