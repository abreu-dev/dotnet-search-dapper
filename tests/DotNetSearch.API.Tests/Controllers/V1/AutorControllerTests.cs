using DotNetSearch.API.Controllers.V1;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Tests.Fixtures;
using FluentValidation.Results;
using NSubstitute;
using System;
using System.Collections.Generic;
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
            var esperado = new List<AutorContrato>() { AutorFixture.BuildContrato(true) };
            _autorAppService.GetAll().Returns(esperado);

            var resultado = _autorController.GetAll().GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
            _autorAppService.Received(1).GetAll();
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_ShouldReturnAppServiceGetByIdResult()
        {
            var esperado = AutorFixture.BuildContrato(true);
            _autorAppService.GetById(esperado.Id).Returns(esperado);

            var resultado = _autorController.GetById(esperado.Id).GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
            _autorAppService.Received(1).GetById(esperado.Id);
        }
        #endregion

        #region Add
        [Fact]
        public void Add_ShouldReturnActionResult()
        {
            var contrato = AutorFixture.BuildContrato(true);
            _autorAppService.Add(contrato).Returns(new ValidationResult());

            var resultado = _autorController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _autorAppService.Received(1).Add(contrato);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldReturnActionResult()
        {
            var contrato = AutorFixture.BuildContrato(true);
            _autorAppService.Update(contrato).Returns(new ValidationResult());

            var resultado = _autorController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _autorAppService.Received(1).Update(contrato);
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldReturnActionResult()
        {
            var id = Guid.NewGuid();
            _autorAppService.Remove(id).Returns(new ValidationResult());

            var resultado = _autorController.Remove(id).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _autorAppService.Received(1).Remove(id);
        }
        #endregion
    }
}
