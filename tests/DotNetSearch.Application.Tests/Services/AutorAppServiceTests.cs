using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Interfaces;
using DotNetSearch.Application.Services;
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
    public class AutorAppServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IAutorRepository _autorRepository;
        private readonly IAutorAppService _autorAppService;

        public AutorAppServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _autorRepository = Substitute.For<IAutorRepository>();
            _autorAppService = new AutorAppService(_mapper, _autorRepository);
        }

        #region Add
        [Fact]
        public void Add_ShouldFailValidation_WhenInvalidAutor()
        {
            var contrato = new AutorContrato() { Nome = "" };
            var entidade = new Autor() { Nome = contrato.Nome };
            _mapper.Map<Autor>(contrato).Returns(entidade);

            var resultado = _autorAppService.Add(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Add_ShouldAddAndCommit_WhenValidAutor()
        {
            var contrato = new AutorContrato() { Nome = "Stephen King", DataNascimento = DateTime.UtcNow };
            var entidade = new Autor() { Nome = contrato.Nome, DataNascimento = DateTime.UtcNow };
            _mapper.Map<Autor>(contrato).Returns(entidade);

            var resultado = _autorAppService.Add(contrato).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _autorRepository.Received(1).Add(entidade);
            _autorRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldFailValidation_WhenInvalidAutor()
        {
            var contrato = new AutorContrato() { Nome = "" };
            var entidade = new Autor() { Nome = contrato.Nome };
            _mapper.Map<Autor>(contrato).Returns(entidade);

            var resultado = _autorAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Update_ShouldFailValidation_WhenAutorNotFound()
        {
            var contrato = new AutorContrato() { Id = Guid.NewGuid(), Nome = "Stephen King", DataNascimento = DateTime.UtcNow };
            var entidade = new Autor() { Id = contrato.Id, Nome = contrato.Nome, DataNascimento = DateTime.UtcNow };
            _mapper.Map<Autor>(contrato).Returns(entidade);
            _autorRepository.GetById(contrato.Id).ReturnsNull();

            var resultado = _autorAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
            Assert.Equal(DomainMessages.NotFound.Format("Autor").Message,
                resultado.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Update_ShouldUpdateAndCommit_WhenValidAutor()
        {
            var contrato = new AutorContrato() { Id = Guid.NewGuid(), Nome = "Stephen King", DataNascimento = DateTime.UtcNow };
            var entidade = new Autor() { Id = contrato.Id, Nome = contrato.Nome, DataNascimento = contrato.DataNascimento };
            var dbEntity = new Autor() { Id = contrato.Id, Nome = "Comédia", DataNascimento = DateTime.UtcNow.AddDays(-30) };
            _mapper.Map<Autor>(contrato).Returns(entidade);
            _autorRepository.GetById(contrato.Id).Returns(dbEntity);
            _mapper.Map(entidade, dbEntity).Returns(entidade);

            var resultado = _autorAppService.Update(contrato).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _autorRepository.Received(1).Update(dbEntity);
            _autorRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldFailValidation_WhenInvalidAutor()
        {
            var resultado = _autorAppService.Remove(Guid.Empty).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
        }

        [Fact]
        public void Remove_ShouldFailValidation_WhenAutorNotFound()
        {
            var dbEntity = new Autor() { Id = Guid.NewGuid(), Nome = "Stephen King" };
            _autorRepository.GetById(dbEntity.Id).ReturnsNull();

            var resultado = _autorAppService.Remove(dbEntity.Id).GetAwaiter().GetResult();

            Assert.False(resultado.IsValid);
            Assert.Equal(DomainMessages.NotFound.Format("Autor").Message,
                resultado.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Remove_ShouldRemoveAndCommit_WhenValidAutor()
        {
            var dbEntity = new Autor() { Id = Guid.NewGuid(), Nome = "Stephen King" };
            _autorRepository.GetById(dbEntity.Id).Returns(dbEntity);

            var resultado = _autorAppService.Remove(dbEntity.Id).GetAwaiter().GetResult();

            Assert.True(resultado.IsValid);
            _autorRepository.Received(1).Remove(dbEntity);
            _autorRepository.UnitOfWork.Received(1).Commit();
        }
        #endregion
    }
}
