using DotNetSearch.Tests.Fixtures;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Validators.CategoriaValidators;
using System;
using System.Linq;
using Xunit;

namespace DotNetSearch.Domain.Tests.Validators
{
    public class CategoriaValidatorTests
    {
        #region AddCategoriaValidator
        [Fact]
        public void AddCategoriaValidator_ShouldFailValidation_WhenEmptyNome()
        {
            var categoria = CategoriaFixture.BuildEntity();
            categoria.Nome = "";

            var validationResult = new AddCategoriaValidator().Validate(categoria);

            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void AddCategoriaValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var categoria = CategoriaFixture.BuildEntity();

            var validationResult = new AddCategoriaValidator().Validate(categoria);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion

        #region UpdateCategoriaValidator
        [Fact]
        public void UpdateCategoriaValidator_ShouldFailValidation_WhenEmptyId()
        {
            var categoria = CategoriaFixture.BuildEntity();

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateCategoriaValidator_ShouldFailValidation_WhenEmptyNome()
        {
            var categoria = CategoriaFixture.BuildEntity(true);
            categoria.Nome = "";

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);

            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateCategoriaValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var categoria = CategoriaFixture.BuildEntity(true);

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion

        #region RemoveCategoriaValidator
        [Fact]
        public void RemoveCategoriaValidator_ShouldFailValidation_WhenEmptyId()
        {
            var validationResult = new RemoveCategoriaValidator().Validate(Guid.Empty);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void RemoveCategoriaValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var validationResult = new RemoveCategoriaValidator().Validate(Guid.NewGuid());

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        #endregion
    }
}
