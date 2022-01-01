using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace DotNetSearch.Infra.Data.Tests.Repositories
{
    public class CategoriaRepositoryTests
    {
        private readonly DotNetSearchDbContext _dotNetSearchDbContext;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaRepositoryTests()
        {
            _dotNetSearchDbContext = new DotNetSearchDbContext(
                new DbContextOptionsBuilder<DotNetSearchDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
            _categoriaRepository = new CategoriaRepository(_dotNetSearchDbContext);
        }

        [Fact]
        public void Constructor_ShouldInstantiate()
        {
            Assert.NotNull(_categoriaRepository);
        }
    }
}
