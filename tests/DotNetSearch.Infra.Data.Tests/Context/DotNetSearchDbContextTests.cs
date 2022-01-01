using DotNetSearch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace DotNetSearch.Infra.Data.Tests.Context
{
    public class DotNetSearchDbContextTests
    {
        private readonly DotNetSearchDbContext _dotNetSearchDbContext;

        public DotNetSearchDbContextTests()
        {
            _dotNetSearchDbContext = new DotNetSearchDbContext(
                new DbContextOptionsBuilder<DotNetSearchDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        }

        [Fact]
        public void Autor_ShouldReturnAutorDbSet()
        {
            var resultado = _dotNetSearchDbContext.Autor;

            Assert.NotNull(resultado);
        }

        [Fact]
        public void Livro_ShouldReturnLivroDbSet()
        {
            var resultado = _dotNetSearchDbContext.Livro;

            Assert.NotNull(resultado);
        }

        [Fact]
        public void Categoria_ShouldReturnCategoriaDbSet()
        {
            var resultado = _dotNetSearchDbContext.Categoria;

            Assert.NotNull(resultado);
        }
    }
}
