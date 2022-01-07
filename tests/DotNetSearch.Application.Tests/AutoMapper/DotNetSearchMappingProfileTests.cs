using AutoMapper;
using DotNetSearch.Application.AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;
using System;
using Xunit;

namespace DotNetSearch.Application.Tests.AutoMapper
{
    public class DotNetSearchMappingProfileTests
    {
        private readonly IMapper _mapper;

        public DotNetSearchMappingProfileTests()
        {
            _mapper = new MapperConfiguration(p => p.AddProfile(new DotNetSearchMappingProfile())).CreateMapper();
        }

        #region Categoria
        [Fact]
        public void Map_ShouldMapCategoriaToCategoriaContrato()
        {
            // Arrange
            var categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Nome = "Terror"
            };

            // Act
            var result = _mapper.Map<CategoriaContrato>(categoria);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoria.Id, result.Id);
            Assert.Equal(categoria.Nome, result.Nome);
        }

        [Fact]
        public void Map_ShouldMapCategoriaContratoToCategoria()
        {
            // Arrange
            var categoriaContrato = new CategoriaContrato()
            {
                Id = Guid.NewGuid(),
                Nome = "Terror"
            };

            // Act
            var result = _mapper.Map<Categoria>(categoriaContrato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoriaContrato.Id, result.Id);
            Assert.Equal(categoriaContrato.Nome, result.Nome);
        }

        [Fact]
        public void Map_ShouldMapCategoriaToCategoria()
        {
            // Arrange
            var categoriaSource = new Categoria()
            {
                Id = Guid.NewGuid(),
                Nome = "Terror"
            };
            var categoriaDestination = new Categoria()
            {
                Id = Guid.NewGuid(),
                Nome = "Comédia"
            };

            // Act
            _mapper.Map(categoriaSource, categoriaDestination);

            // Assert
            Assert.Equal(categoriaSource.Id, categoriaDestination.Id);
            Assert.Equal(categoriaSource.Nome, categoriaDestination.Nome);
        }
        #endregion

        #region Autor
        [Fact]
        public void Map_ShouldMapAutorToAutorContrato()
        {
            // Arrange
            var autor = new Autor()
            {
                Id = Guid.NewGuid(),
                Nome = "Stephen King"
            };

            // Act
            var result = _mapper.Map<AutorContrato>(autor);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(autor.Id, result.Id);
            Assert.Equal(autor.Nome, result.Nome);
        }

        [Fact]
        public void Map_ShouldMapAutorContratoToAutor()
        {
            // Arrange
            var autorContrato = new AutorContrato()
            {
                Id = Guid.NewGuid(),
                Nome = "Stephen King"
            };

            // Act
            var result = _mapper.Map<Autor>(autorContrato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(autorContrato.Id, result.Id);
            Assert.Equal(autorContrato.Nome, result.Nome);
        }

        [Fact]
        public void Map_ShouldMapAutorToAutor()
        {
            // Arrange
            var autorSource = new Autor()
            {
                Id = Guid.NewGuid(),
                Nome = "Stephen King"
            };
            var autorDestination = new Autor()
            {
                Id = Guid.NewGuid(),
                Nome = "Comédia"
            };

            // Act
            _mapper.Map(autorSource, autorDestination);

            // Assert
            Assert.Equal(autorSource.Id, autorDestination.Id);
            Assert.Equal(autorSource.Nome, autorDestination.Nome);
        }
        #endregion
    }
}
