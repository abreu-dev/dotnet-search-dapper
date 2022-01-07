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
    public class CategoriaControllerTests
    {
        private readonly ICategoriaAppService _categoriaAppService;
        private readonly CategoriaController _categoriaController;

        public CategoriaControllerTests()
        {
            _categoriaAppService = Substitute.For<ICategoriaAppService>();
            _categoriaController = new CategoriaController(_categoriaAppService);
        }

        #region GetAll
        [Fact]
        public void GetAll_ShouldReturnAppServiceGetAllResult()
        {
            var esperado = new List<CategoriaContrato>() { CategoriaFixture.BuildContrato(true) };
            _categoriaAppService.GetAll().Returns(esperado);

            var resultado = _categoriaController.GetAll().GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
            _categoriaAppService.Received(1).GetAll();
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_ShouldReturnAppServiceGetByIdResult()
        {
            var esperado = CategoriaFixture.BuildContrato(true);
            _categoriaAppService.GetById(esperado.Id).Returns(esperado);

            var resultado = _categoriaController.GetById(esperado.Id).GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
            _categoriaAppService.Received(1).GetById(esperado.Id);
        }
        #endregion

        #region Add
        [Fact]
        public void Add_ShouldReturnActionResult()
        {
            var contrato = CategoriaFixture.BuildContrato(true);
            _categoriaAppService.Add(contrato).Returns(new ValidationResult());

            var resultado = _categoriaController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _categoriaAppService.Received(1).Add(contrato);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldReturnActionResult()
        {
            var contrato = CategoriaFixture.BuildContrato(true);
            _categoriaAppService.Update(contrato).Returns(new ValidationResult());

            var resultado = _categoriaController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _categoriaAppService.Received(1).Update(contrato);
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldReturnActionResult()
        {
            var id = Guid.NewGuid();
            _categoriaAppService.Remove(id).Returns(new ValidationResult());

            var resultado = _categoriaController.Remove(id).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _categoriaAppService.Received(1).Remove(id);
        }
        #endregion
    }
}
