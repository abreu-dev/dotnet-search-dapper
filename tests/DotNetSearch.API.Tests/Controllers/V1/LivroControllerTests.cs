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
    public class LivroControllerTests
    {
        private readonly ILivroAppService _livroAppService;
        private readonly LivroController _livroController;

        public LivroControllerTests()
        {
            _livroAppService = Substitute.For<ILivroAppService>();
            _livroController = new LivroController(_livroAppService);
        }

        #region GetAll
        [Fact]
        public void GetAll_ShouldReturnAppServiceGetAllResult()
        {
            var esperado = new List<LivroContrato>() { LivroFixture.BuildContrato(true) };
            _livroAppService.GetAll().Returns(esperado);

            var resultado = _livroController.GetAll().GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
            _livroAppService.Received(1).GetAll();
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_ShouldReturnAppServiceGetByIdResult()
        {
            var esperado = LivroFixture.BuildContrato(true);
            _livroAppService.GetById(esperado.Id).Returns(esperado);

            var resultado = _livroController.GetById(esperado.Id).GetAwaiter().GetResult();

            Assert.Equal(esperado, resultado);
            _livroAppService.Received(1).GetById(esperado.Id);
        }
        #endregion

        #region Add
        [Fact]
        public void Add_ShouldReturnActionResult()
        {
            var contrato = LivroFixture.BuildContrato(true);
            _livroAppService.Add(contrato).Returns(new ValidationResult());

            var resultado = _livroController.Add(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _livroAppService.Received(1).Add(contrato);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldReturnActionResult()
        {
            var contrato = LivroFixture.BuildContrato(true);
            _livroAppService.Update(contrato).Returns(new ValidationResult());

            var resultado = _livroController.Update(contrato).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _livroAppService.Received(1).Update(contrato);
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldReturnActionResult()
        {
            var id = Guid.NewGuid();
            _livroAppService.Remove(id).Returns(new ValidationResult());

            var resultado = _livroController.Remove(id).GetAwaiter().GetResult();

            Assert.NotNull(resultado);
            _livroAppService.Received(1).Remove(id);
        }
        #endregion
    }
}
