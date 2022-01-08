using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Models;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.NSubstitute;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DotNetSearch.Infra.Data.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly MyEntityConcreteClass _myEntityConcreteClass;
        private readonly DbSet<MyEntityConcreteClass> _myEntityConcreteClassDbSet;
        private readonly MyDbContextConcreteClass _myDbContextConcreteClass;
        private readonly MyRepositoryConcreteClass _myRepositoryConcreteClass;

        public RepositoryTests()
        {
            _myEntityConcreteClass = new MyEntityConcreteClass() { Id = Guid.NewGuid() };
            _myEntityConcreteClassDbSet = new List<MyEntityConcreteClass>()
                { _myEntityConcreteClass }.AsQueryable().BuildMockDbSet();
            _myDbContextConcreteClass = Substitute.For<MyDbContextConcreteClass>(
                new DbContextOptionsBuilder<MyDbContextConcreteClass>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _myDbContextConcreteClass.Set<MyEntityConcreteClass>().Returns(_myEntityConcreteClassDbSet);
            _myRepositoryConcreteClass = new MyRepositoryConcreteClass(_myDbContextConcreteClass);
        }

        #region UnitOfWork
        [Fact]
        public void UnitOfWork_ShouldReturnDbContextAsUnitOfWork()
        {
            var resultado = _myRepositoryConcreteClass.UnitOfWork;

            Assert.NotNull(resultado);
        }
        #endregion

        #region Query
        [Fact]
        public void Query_ShouldReturnEntityQueryable()
        {
            var resultado = _myRepositoryConcreteClass.Query();

            Assert.Single(resultado);
            Assert.Equal(_myEntityConcreteClass, resultado.Single());
        }
        #endregion

        #region Search
        [Fact]
        public void Search_ShouldReturnEntityList()
        {
            var resultado = _myRepositoryConcreteClass.Search(x => x.Id == _myEntityConcreteClass.Id)
                .GetAwaiter().GetResult();

            Assert.Single(resultado);
            Assert.Equal(_myEntityConcreteClass, resultado.Single());
        }

        [Fact]
        public void Search_ShouldReturnEmptyList()
        {
            var resultado = _myRepositoryConcreteClass.Search(x => x.Id == Guid.NewGuid())
                .GetAwaiter().GetResult();

            Assert.Empty(resultado);
        }
        #endregion

        #region GetAll
        [Fact]
        public void GetAll_ShouldReturnEntityList()
        {
            var resultado = _myRepositoryConcreteClass.GetAll().GetAwaiter().GetResult();

            Assert.Single(resultado);
            Assert.Equal(_myEntityConcreteClass, resultado.Single());
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_ShouldReturnEntity()
        {
            var resultado = _myRepositoryConcreteClass.GetById(_myEntityConcreteClass.Id).GetAwaiter().GetResult();

            Assert.Equal(_myEntityConcreteClass, resultado);
        }

        [Fact]
        public void GetById_ShouldReturnNull()
        {
            var resultado = _myRepositoryConcreteClass.GetById(Guid.NewGuid()).GetAwaiter().GetResult();

            Assert.Null(resultado);
        }
        #endregion

        #region Add
        [Fact]
        public void Add_ShouldCallDbSetAdd()
        {
            var entidade = new MyEntityConcreteClass() { Id = Guid.NewGuid() };

            _myRepositoryConcreteClass.Add(entidade);

            _myEntityConcreteClassDbSet.Received(1).Add(entidade);
        }
        #endregion

        #region Update
        [Fact]
        public void Update_ShouldCallDbSetUpdate()
        {
            var entidade = new MyEntityConcreteClass() { Id = Guid.NewGuid() };

            _myRepositoryConcreteClass.Update(entidade);

            _myEntityConcreteClassDbSet.Received(1).Update(entidade);
        }
        #endregion

        #region Remove
        [Fact]
        public void Remove_ShouldCallDbSetRemove()
        {
            var entidade = new MyEntityConcreteClass() { Id = Guid.NewGuid() };

            _myRepositoryConcreteClass.Remove(entidade);

            _myEntityConcreteClassDbSet.Received(1).Remove(entidade);
        }
        #endregion
    }

    public class MyEntityConcreteClass : Entity { }

    public class MyDbContextConcreteClass : BaseDbContext
    {
        public MyDbContextConcreteClass(DbContextOptions options) : base(options) { }

        public DbSet<MyEntityConcreteClass> MyEntityConcreteClass { get; set; }
    }

    public class MyRepositoryConcreteClass : Repository<MyEntityConcreteClass>
    {
        public MyRepositoryConcreteClass(MyDbContextConcreteClass myDbContextConcreteClass)
            : base(myDbContextConcreteClass) { }

        public override Task<IEnumerable<MyEntityConcreteClass>> DapperSearch(SearchRequestModel searchRequestModel)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<MyEntityConcreteClass>> DapperGetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<MyEntityConcreteClass> DapperGetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
