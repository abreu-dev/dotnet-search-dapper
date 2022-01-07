using DotNetSearch.Tests.Fixtures;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Validators.LivroValidators;
using System;
using System.Linq;
using Xunit;

namespace DotNetSearch.Domain.Tests.Validators
{
    public class LivroValidatorTests
    {
        #region AddLivroValidator
        [Fact]
        public void AddLivroValidator_ShouldFailValidation_WhenEmptyTitulo()
        {
            var livro = LivroFixture.BuildEntity();
            livro.Titulo = "";

            var validationResult = new AddLivroValidator().Validate(livro);

            Assert.Equal(DomainMessages.RequiredField.Format("Título").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void AddLivroValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var livro = LivroFixture.BuildEntity();

            var validationResult = new AddLivroValidator().Validate(livro);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion

        #region UpdateLivroValidator
        [Fact]
        public void UpdateLivroValidator_ShouldFailValidation_WhenEmptyId()
        {
            var livro = LivroFixture.BuildEntity();

            var validationResult = new UpdateLivroValidator().Validate(livro);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateLivroValidator_ShouldFailValidation_WhenEmptyTitulo()
        {
            var livro = LivroFixture.BuildEntity(true);
            livro.Titulo = "";

            var validationResult = new UpdateLivroValidator().Validate(livro);

            Assert.Equal(DomainMessages.RequiredField.Format("Título").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateLivroValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var livro = LivroFixture.BuildEntity(true);

            var validationResult = new UpdateLivroValidator().Validate(livro);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion

        #region RemoveLivroValidator
        [Fact]
        public void RemoveLivroValidator_ShouldFailValidation_WhenEmptyId()
        {
            var validationResult = new RemoveLivroValidator().Validate(Guid.Empty);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void RemoveLivroValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var validationResult = new RemoveLivroValidator().Validate(Guid.NewGuid());

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion
    }
}
