using AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Application.Services;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetSearch.Application.Tests.Services
{
    public class AppServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MyEntityConcreteClass> _repository;
        private readonly MyAppServiceConcreteClass _myAppServiceConcreteClass;

        public AppServiceTests()
        {
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IRepository<MyEntityConcreteClass>>();
            _myAppServiceConcreteClass = new MyAppServiceConcreteClass(_mapper, _repository);
        }

        [Fact]
        public void GetAll_ShouldReturnEmptyContratoList()
        {
            _repository.GetAll().Returns(new List<MyEntityConcreteClass>());

            var resultado = _myAppServiceConcreteClass.GetAll().GetAwaiter().GetResult();

            Assert.Empty(resultado);
        }

        [Fact]
        public void GetAll_ShouldReturnContratoList()
        {
            var entidadeUm = new MyEntityConcreteClass() { Id = Guid.NewGuid() };
            var entidadeDois = new MyEntityConcreteClass() { Id = Guid.NewGuid() };
            var entidadeLista = new List<MyEntityConcreteClass>() { entidadeUm, entidadeDois };
            var contratoUm = new MyContratoConcreteClass() { };
            var contratoDois = new MyContratoConcreteClass() { };
            var contratoLista = new List<MyContratoConcreteClass>() { contratoUm, contratoDois };
            _repository.GetAll().Returns(entidadeLista);
            _mapper.Map<IEnumerable<MyContratoConcreteClass>>(entidadeLista).Returns(contratoLista);

            var resultado = _myAppServiceConcreteClass.GetAll().GetAwaiter().GetResult();

            Assert.Equal(contratoLista, resultado);
        }

        [Fact]
        public void GetById_ShouldReturnNull()
        {
            var id = Guid.NewGuid();
            _repository.GetById(id).ReturnsNull();
            _mapper.Map<MyContratoConcreteClass>(null).ReturnsNull();

            var resultado = _myAppServiceConcreteClass.GetById(id).GetAwaiter().GetResult();

            Assert.Null(resultado);
        }

        [Fact]
        public void GetById_ShouldReturnContrato()
        {
            var entidade = new MyEntityConcreteClass() { Id = Guid.NewGuid() };
            var contrato = new MyContratoConcreteClass() { };
            _repository.GetById(entidade.Id).Returns(entidade);
            _mapper.Map<MyContratoConcreteClass>(entidade).Returns(contrato);

            var resultado = _myAppServiceConcreteClass.GetById(entidade.Id).GetAwaiter().GetResult();

            Assert.Equal(contrato, resultado);
        }
    }

    public class MyEntityConcreteClass : Entity { }

    public class MyContratoConcreteClass : Contrato { }

    public class MyAppServiceConcreteClass : AppService<MyContratoConcreteClass, MyEntityConcreteClass>
    {
        public MyAppServiceConcreteClass(IMapper mapper,
                                         IRepository<MyEntityConcreteClass> repository)
            : base(mapper, repository) { }
    }
}
