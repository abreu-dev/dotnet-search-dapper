using DotNetSearch.API.Common;
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
    public class AutorControllerTests
    {
        private readonly IAutorAppService _autorAppService;
        private readonly AutorController _autorController;

        public AutorControllerTests()
        {
            _autorAppService = Substitute.For<IAutorAppService>();
            _autorController = new AutorController(_autorAppService);
        }

        #region GetAll
        [Fact]
        public void GetAll_ShouldReturnAppServiceGetAllResult()
        {
            var esperado = new List<AutorContrato>() { 
                new AutorContrato() { Id = Guid.NewGuid(), Nome = "Stephen King" } };
            _autorAppService.GetAll().Returns(esperado);

            var resultado = _autorController.GetAll().GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_ShouldReturnAppServiceGetByIdResult()
        {
            var esperado = new AutorContrato() { Id = Guid.NewGuid(), Nome = "Stephen King" };
            _autorAppService.GetById(esperado.Id).Returns(esperado);

            var resultado = _autorController.GetById(esperado.Id).GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
        }
        #endregion

        #region Add
        [Fact]
        public void Add_ShouldReturn200()
        {
            var contrato = new AutorContrato() { Nome = "Stephen King" };
            _autorAppService.Add(contrato).Returns(new ValidationResult());

            var resultado = _autorController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var okResultado = resultado as OkResult;
            Assert.NotNull(okResultado);
            Assert.Equal(200, okResultado.StatusCode);
        }

        [Fact]
        public void Add_ShouldReturn422()
        {
            var contrato = new AutorContrato() { Nome = "Stephen King" };
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("", "ErrorMessage"));
            _autorAppService.Add(contrato).Returns(validationResult);

            var resultado = _autorController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var unprocessableEntityObjectResultado = resultado as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityObjectResultado);
            Assert.Equal(422, unprocessableEntityObjectResultado.StatusCode);
            var unprocessableEntityResponse = unprocessableEntityObjectResultado.Value as UnprocessableEntityResponse;
            Assert.NotNull(unprocessableEntityResponse);
            Assert.Single(unprocessableEntityResponse.Errors);
            Assert.Equal("ErrorMessage", unprocessableEntityResponse.Errors.Single());
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldReturn200()
        {
            var contrato = new AutorContrato() { Id = Guid.NewGuid(), Nome = "Stephen King" };
            _autorAppService.Update(contrato).Returns(new ValidationResult());

            var resultado = _autorController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var okResultado = resultado as OkResult;
            Assert.NotNull(okResultado);
            Assert.Equal(200, okResultado.StatusCode);
        }

        [Fact]
        public void Update_ShouldReturn422()
        {
            var contrato = new AutorContrato() { Id = Guid.NewGuid(), Nome = "Stephen King" };
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("", "ErrorMessage"));
            _autorAppService.Update(contrato).Returns(validationResult);

            var resultado = _autorController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var unprocessableEntityObjectResultado = resultado as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityObjectResultado);
            Assert.Equal(422, unprocessableEntityObjectResultado.StatusCode);
            var unprocessableEntityResponse = unprocessableEntityObjectResultado.Value as UnprocessableEntityResponse;
            Assert.NotNull(unprocessableEntityResponse);
            Assert.Single(unprocessableEntityResponse.Errors);
            Assert.Equal("ErrorMessage", unprocessableEntityResponse.Errors.Single());
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldReturn200()
        {
            var id = Guid.NewGuid();
            _autorAppService.Remove(id).Returns(new ValidationResult());

            var resultado = _autorController.Remove(id).GetAwaiter().GetResult();

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
            _autorAppService.Remove(id).Returns(validationResult);

            var resultado = _autorController.Remove(id).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            var unprocessableEntityObjectResultado = resultado as UnprocessableEntityObjectResult;
            Assert.NotNull(unprocessableEntityObjectResultado);
            Assert.Equal(422, unprocessableEntityObjectResultado.StatusCode);
            var unprocessableEntityResponse = unprocessableEntityObjectResultado.Value as UnprocessableEntityResponse;
            Assert.NotNull(unprocessableEntityResponse);
            Assert.Single(unprocessableEntityResponse.Errors);
            Assert.Equal("ErrorMessage", unprocessableEntityResponse.Errors.Single());
        }
        #endregion
    }
}
