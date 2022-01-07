using DotNetSearch.Tests.Fixtures;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Validators.AutorValidators;
using System;
using System.Linq;
using Xunit;

namespace DotNetSearch.Domain.Tests.Validators
{
    public class AutorValidatorTests
    {
        #region AddAutorValidator
        [Fact]
        public void AddAutorValidator_ShouldFailValidation_WhenEmptyNome()
        {
            var autor = AutorFixture.BuildEntity();
            autor.Nome = "";

            var validationResult = new AddAutorValidator().Validate(autor);

            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void AddAutorValidator_ShouldFailValidation_WhenEmptyDataNascimento()
        {
            var autor = AutorFixture.BuildEntity();
            autor.DataNascimento = DateTime.MinValue;

            var validationResult = new AddAutorValidator().Validate(autor);

            Assert.Equal(DomainMessages.RequiredField.Format("DataNascimento").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void AddAutorValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var autor = AutorFixture.BuildEntity();

            var validationResult = new AddAutorValidator().Validate(autor);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion

        #region UpdateAutorValidator
        [Fact]
        public void UpdateAutorValidator_ShouldFailValidation_WhenEmptyId()
        {
            var autor = AutorFixture.BuildEntity();

            var validationResult = new UpdateAutorValidator().Validate(autor);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateAutorValidator_ShouldFailValidation_WhenEmptyNome()
        {
            var autor = AutorFixture.BuildEntity(true);
            autor.Nome = "";

            var validationResult = new UpdateAutorValidator().Validate(autor);

            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateAutorValidator_ShouldFailValidation_WhenEmptyDataNascimento()
        {
            var autor = AutorFixture.BuildEntity(true);
            autor.DataNascimento = DateTime.MinValue;

            var validationResult = new UpdateAutorValidator().Validate(autor);

            Assert.Equal(DomainMessages.RequiredField.Format("DataNascimento").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateAutorValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var autor = AutorFixture.BuildEntity(true);

            var validationResult = new UpdateAutorValidator().Validate(autor);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion

        #region RemoveAutorValidator
        [Fact]
        public void RemoveAutorValidator_ShouldFailValidation_WhenEmptyId()
        {
            var validationResult = new RemoveAutorValidator().Validate(Guid.Empty);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void RemoveAutorValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var validationResult = new RemoveAutorValidator().Validate(Guid.NewGuid());

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion
    }
}
