using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using DotNetSearch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using Xunit;

namespace DotNetSearch.Infra.Data.Tests.Repositories
{
    public class CategoriaRepositoryTests
    {
        private readonly DotNetSearchDbContext _dotNetSearchDbContext;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaRepositoryTests()
        {
            _dotNetSearchDbContext = new DotNetSearchDbContext(
                new DbContextOptionsBuilder<DotNetSearchDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _dbConnectionFactory = Substitute.For<IDbConnectionFactory>();
            _categoriaRepository = new CategoriaRepository(_dotNetSearchDbContext, _dbConnectionFactory);
        }

        #region Constructor
        [Fact]
        public void Constructor_ShouldInstantiate()
        {
            Assert.NotNull(_categoriaRepository);
        }
        #endregion
    }
}
