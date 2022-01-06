using DotNetSearch.API.Common;
using DotNetSearch.API.Controllers;
using DotNetSearch.API.Controllers.V1;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DotNetSearch.API.Tests.Controllers.V1
{
    public class CategoriaControllerTests
    {
        private readonly ICategoriaAppService _categoriaAppService;
        private readonly CategoriaController _categoriaController;

        public CategoriaControllerTests()
        {
            _categoriaAppService = Substitute.For<ICategoriaAppService>();
            _categoriaController = new CategoriaController(_categoriaAppService);
        }

        [Fact]
        public void GetAll_ShouldReturnAppServiceGetAllResult()
        {
            var esperado = new List<CategoriaContrato>() { 
                new CategoriaContrato() { Id = Guid.NewGuid(), Nome = "Terror" } };
            _categoriaAppService.GetAll().Returns(esperado);

            var resultado = _categoriaController.GetAll().GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void GetById_ShouldReturnAppServiceGetByIdResult()
        {
            var esperado = new CategoriaContrato() { Id = Guid.NewGuid(), Nome = "Terror" };
            _categoriaAppService.GetById(esperado.Id).Returns(esperado);

            var resultado = _categoriaController.GetById(esperado.Id).GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void Add_ShouldReturn200()
        {
            var contrato = new CategoriaContrato() { Nome = "Terror" };
            _categoriaAppService.Add(contrato).Returns(new ValidationResult());

            var resultado = _categoriaController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var okResultado = resultado as OkResult;
            Assert.NotNull(okResultado);
            Assert.Equal(200, okResultado.StatusCode);
        }

        [Fact]
        public void Add_ShouldReturn422()
        {
            var contrato = new CategoriaContrato() { Nome = "Terror" };
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("", "ErrorMessage"));
            _categoriaAppService.Add(contrato).Returns(validationResult);

            var resultado = _categoriaController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var unprocessableEntityObjectResultado = resultado as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityObjectResultado);
            Assert.Equal(422, unprocessableEntityObjectResultado.StatusCode);
            var unprocessableEntityResponse = unprocessableEntityObjectResultado.Value as UnprocessableEntityResponse;
            Assert.NotNull(unprocessableEntityResponse);
            Assert.Single(unprocessableEntityResponse.Errors);
            Assert.Equal("ErrorMessage", unprocessableEntityResponse.Errors.Single());
        }

        [Fact]
        public void Update_ShouldReturn200()
        {
            var contrato = new CategoriaContrato() { Id = Guid.NewGuid(), Nome = "Terror" };
            _categoriaAppService.Update(contrato).Returns(new ValidationResult());

            var resultado = _categoriaController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var okResultado = resultado as OkResult;
            Assert.NotNull(okResultado);
            Assert.Equal(200, okResultado.StatusCode);
        }

        [Fact]
        public void Update_ShouldReturn422()
        {
            var contrato = new CategoriaContrato() { Id = Guid.NewGuid(), Nome = "Terror" };
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("", "ErrorMessage"));
            _categoriaAppService.Update(contrato).Returns(validationResult);

            var resultado = _categoriaController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var unprocessableEntityObjectResultado = resultado as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityObjectResultado);
            Assert.Equal(422, unprocessableEntityObjectResultado.StatusCode);
            var unprocessableEntityResponse = unprocessableEntityObjectResultado.Value as UnprocessableEntityResponse;
            Assert.NotNull(unprocessableEntityResponse);
            Assert.Single(unprocessableEntityResponse.Errors);
            Assert.Equal("ErrorMessage", unprocessableEntityResponse.Errors.Single());
        }

        [Fact]
        public void Remove_ShouldReturn200()
        {
            var id = Guid.NewGuid();
            _categoriaAppService.Remove(id).Returns(new ValidationResult());

            var resultado = _categoriaController.Remove(id).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var okResultado = resultado as OkResult;
            Assert.NotNull(okResultado);
            Assert.Equal(200, okResultado.StatusCode);
        }

        [Fact]
        public void Remove_ShouldReturn422()
        {
            var id = Guid.NewGuid();
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("", "ErrorMessage"));
            _categoriaAppService.Remove(id).Returns(validationResult);

            var resultado = _categoriaController.Remove(id).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var unprocessableEntityObjectResultado = resultado as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityObjectResultado);
            Assert.Equal(422, unprocessableEntityObjectResultado.StatusCode);
            var unprocessableEntityResponse = unprocessableEntityObjectResultado.Value as UnprocessableEntityResponse;
            Assert.NotNull(unprocessableEntityResponse);
            Assert.Single(unprocessableEntityResponse.Errors);
            Assert.Equal("ErrorMessage", unprocessableEntityResponse.Errors.Single());
        }
    }
}
