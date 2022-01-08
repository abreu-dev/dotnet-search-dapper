using AutoMapper;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Application.Services;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Tests.Fixtures;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Linq;
using Xunit;

namespace DotNetSearch.Application.Tests.Services
{
    public class LivroAppServiceTests
    {
        private readonly IMapper _mapper;
        private readonly ILivroRepository _livroRepository;
        private readonly ILivroAppService _livroAppService;

        public LivroAppServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _livroRepository = Substitute.For<ILivroRepository>();
            _livroAppService = new LivroAppService(_mapper, _livroRepository);
        }

        #region Add
        [Fact]
        public void Add_ShouldFailValidation_WhenInvalidLivro()
        {
            var contrato = LivroFixture.BuildContrato();
            var entidade = LivroFixture.BuildEntity();
            entidade.Titulo = "";
            _mapper.Map<Livro>(contrato).Returns(entidade);

            var resultado = _livroAppService.Add(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Add_ShouldAddAndCommit_WhenValidLivro()
        {
            var contrato = LivroFixture.BuildContrato();
            var entidade = LivroFixture.BuildEntity();
            _mapper.Map<Livro>(contrato).Returns(entidade);

            var resultado = _livroAppService.Add(contrato).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _livroRepository.Received(1).Add(entidade);
            _livroRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldFailValidation_WhenInvalidLivro()
        {
            var contrato = LivroFixture.BuildContrato();
            var entidade = LivroFixture.BuildEntity();
            entidade.Titulo = "";
            _mapper.Map<Livro>(contrato).Returns(entidade);

            var resultado = _livroAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Update_ShouldFailValidation_WhenLivroNotFound()
        {
            var contrato = LivroFixture.BuildContrato(true);
            var entidade = LivroFixture.BuildEntity();
            entidade.Id = contrato.Id;
            _mapper.Map<Livro>(contrato).Returns(entidade);
            _livroRepository.GetById(contrato.Id).ReturnsNull();

            var resultado = _livroAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
            Assert.Equal(DomainMessages.NotFound.Format("Livro").Message,
                resultado.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Update_ShouldUpdateAndCommit_WhenValidLivro()
        {
            var contrato = LivroFixture.BuildContrato(true);
            var entidade = LivroFixture.BuildEntity();
            entidade.Id = contrato.Id;
            var dbEntity = LivroFixture.BuildEntity();
            dbEntity.Id = contrato.Id;
            _mapper.Map<Livro>(contrato).Returns(entidade);
            _livroRepository.GetById(contrato.Id).Returns(dbEntity);
            _mapper.Map(entidade, dbEntity).Returns(entidade);

            var resultado = _livroAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _livroRepository.Received(1).Update(dbEntity);
            _livroRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldFailValidation_WhenInvalidLivro()
        {
            var resultado = _livroAppService.Remove(Guid.Empty).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Remove_ShouldFailValidation_WhenLivroNotFound()
        {
            var dbEntity = LivroFixture.BuildEntity(true);
            _livroRepository.GetById(dbEntity.Id).ReturnsNull();

            var resultado = _livroAppService.Remove(dbEntity.Id).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
            Assert.Equal(DomainMessages.NotFound.Format("Livro").Message,
                resultado.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Remove_ShouldRemoveAndCommit_WhenValidLivro()
        {
            var dbEntity = LivroFixture.BuildEntity(true);
            _livroRepository.GetById(dbEntity.Id).Returns(dbEntity);

            var resultado = _livroAppService.Remove(dbEntity.Id).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _livroRepository.Received(1).Remove(dbEntity);
            _livroRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion
    }
}
