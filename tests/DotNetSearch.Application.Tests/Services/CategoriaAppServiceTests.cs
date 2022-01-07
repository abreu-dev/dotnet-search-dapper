using AutoMapper;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Application.Services;
using DotNetSearch.Tests.Fixtures;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Linq;
using Xunit;

namespace DotNetSearch.Application.Tests.Services
{
    public class CategoriaAppServiceTests
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaAppService _categoriaAppService;

        public CategoriaAppServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _categoriaRepository = Substitute.For<ICategoriaRepository>();
            _categoriaAppService = new CategoriaAppService(_mapper, _categoriaRepository);
        }

        #region Add
        [Fact]
        public void Add_ShouldFailValidation_WhenInvalidCategoria()
        {
            var contrato = CategoriaFixture.BuildContrato();
            var entidade = CategoriaFixture.BuildEntity();
            entidade.Nome = "";
            _mapper.Map<Categoria>(contrato).Returns(entidade);

            var resultado = _categoriaAppService.Add(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Add_ShouldAddAndCommit_WhenValidCategoria()
        {
            var contrato = CategoriaFixture.BuildContrato();
            var entidade = CategoriaFixture.BuildEntity();
            _mapper.Map<Categoria>(contrato).Returns(entidade);

            var resultado = _categoriaAppService.Add(contrato).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _categoriaRepository.Received(1).Add(entidade);
            _categoriaRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldFailValidation_WhenInvalidCategoria()
        {
            var contrato = CategoriaFixture.BuildContrato();
            var entidade = CategoriaFixture.BuildEntity();
            entidade.Nome = "";
            _mapper.Map<Categoria>(contrato).Returns(entidade);

            var resultado = _categoriaAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Update_ShouldFailValidation_WhenCategoriaNotFound()
        {
            var contrato = CategoriaFixture.BuildContrato(true);
            var entidade = CategoriaFixture.BuildEntity();
            entidade.Id = contrato.Id;
            _mapper.Map<Categoria>(contrato).Returns(entidade);
            _categoriaRepository.GetById(contrato.Id).ReturnsNull();

            var resultado = _categoriaAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
            Assert.Equal(DomainMessages.NotFound.Format("Categoria").Message,
                resultado.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Update_ShouldUpdateAndCommit_WhenValidCategoria()
        {
            var contrato = CategoriaFixture.BuildContrato(true);
            var entidade = CategoriaFixture.BuildEntity();
            entidade.Id = contrato.Id;
            var dbEntity = CategoriaFixture.BuildEntity();
            dbEntity.Id = contrato.Id;
            _mapper.Map<Categoria>(contrato).Returns(entidade);
            _categoriaRepository.GetById(contrato.Id).Returns(dbEntity);
            _mapper.Map(entidade, dbEntity).Returns(entidade);

            var resultado = _categoriaAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _categoriaRepository.Received(1).Update(dbEntity);
            _categoriaRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldFailValidation_WhenInvalidCategoria()
        {
            var resultado = _categoriaAppService.Remove(Guid.Empty).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Remove_ShouldFailValidation_WhenCategoriaNotFound()
        {
            var dbEntity = CategoriaFixture.BuildEntity(true);
            _categoriaRepository.GetById(dbEntity.Id).ReturnsNull();

            var resultado = _categoriaAppService.Remove(dbEntity.Id).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
            Assert.Equal(DomainMessages.NotFound.Format("Categoria").Message,
                resultado.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Remove_ShouldRemoveAndCommit_WhenValidCategoria()
        {
            var dbEntity = CategoriaFixture.BuildEntity(true);
            _categoriaRepository.GetById(dbEntity.Id).Returns(dbEntity);

            var resultado = _categoriaAppService.Remove(dbEntity.Id).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _categoriaRepository.Received(1).Remove(dbEntity);
            _categoriaRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion
    }
}
