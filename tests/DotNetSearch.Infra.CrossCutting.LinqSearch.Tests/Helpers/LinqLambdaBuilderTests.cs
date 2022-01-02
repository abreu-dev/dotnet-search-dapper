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
        public void Equal_ShouldBuildExpressionEqual()
        {
            var expected = "x => (x.Nome == \"Terror\")";

            var actual = LinqLambdaBuilder.Equal<MyEntityConcreteClass>("Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region Like
        [Fact]
        public void Like_ShouldBuildExpressionLike()
        {
            var expected = "x => x.Nome.Contains(\"Terror\")";

            var actual = LinqLambdaBuilder.Like<MyEntityConcreteClass>("Nome", "Terror");

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region And
        [Fact]
        public void And()
        {
            var expected = "x => ((x.Nome == \"Terror\") AndAlso (x.Nome == \"Comédia\"))";
            var firstExpression = LinqLambdaBuilder.Equal<MyEntityConcreteClass>("Nome", "Terror");
            var secondExpression = LinqLambdaBuilder.Equal<MyEntityConcreteClass>("Nome", "Comédia");

            var actual = firstExpression.And(secondExpression);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion

        #region Or
        [Fact]
        public void Or()
        {
            var expected = "x => ((x.Nome == \"Terror\") OrElse (x.Nome == \"Comédia\"))";
            var firstExpression = LinqLambdaBuilder.Equal<MyEntityConcreteClass>("Nome", "Terror");
            var secondExpression = LinqLambdaBuilder.Equal<MyEntityConcreteClass>("Nome", "Comédia");

            var actual = firstExpression.Or(secondExpression);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual.ToString());
        }
        #endregion
    }

    public class MyEntityConcreteClass 
    {
        public string Nome { get; set; }
    }
}
