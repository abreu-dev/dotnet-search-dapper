using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Validators.CategoriaValidators;
using System;
using System.Linq;
using Xunit;

namespace DotNetSearch.Domain.Tests.Validators
{
    public class CategoriaValidatorTests
    {
        [Fact]
        public void AddCategoriaValidator_ShouldFailValidation_WhenEmptyNome()
        {
            var categoria = new Categoria()
            {
                Nome = ""
            };

            var validationResult = new AddCategoriaValidator().Validate(categoria);

            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void AddCategoriaValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var categoria = new Categoria()
            {
                Nome = "Terror"
            };

            var validationResult = new AddCategoriaValidator().Validate(categoria);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void UpdateCategoriaValidator_ShouldFailValidation_WhenEmptyId()
        {
            var categoria = new Categoria()
            {
                Id = Guid.Empty,
                Nome = "Terror"
            };

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);

            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateCategoriaValidator_ShouldFailValidation_WhenEmptyNome()
        {
            var categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Nome = ""
            };

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);

            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message,
                validationResult.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void UpdateCategoriaValidator_ShouldBeValid_WhenBeWithinValidationRules()
        {
            var categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Nome = "Terror"
            };

            var validationResult = new UpdateCategoriaValidator().Validate(categoria);

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

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
    }
}
