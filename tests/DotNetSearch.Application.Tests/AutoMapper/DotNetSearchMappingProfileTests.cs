using AutoMapper;
using DotNetSearch.Application.AutoMapper;
using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Tests.Fixtures;
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
            var categoria = CategoriaFixture.BuildEntity(true);

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
            var categoriaContrato = CategoriaFixture.BuildContrato(true);

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
            var categoriaSource = CategoriaFixture.BuildEntity(true);
            categoriaSource.Nome = "Terror";
            var categoriaDestination = CategoriaFixture.BuildEntity(true);
            categoriaDestination.Nome = "Comédia";

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
            var autor = AutorFixture.BuildEntity(true);

            // Act
            var result = _mapper.Map<AutorContrato>(autor);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(autor.Id, result.Id);
            Assert.Equal(autor.Nome, result.Nome);
            Assert.Equal(autor.DataNascimento, result.DataNascimento);
        }

        [Fact]
        public void Map_ShouldMapAutorContratoToAutor()
        {
            // Arrange
            var autorContrato = AutorFixture.BuildContrato(true);

            // Act
            var result = _mapper.Map<Autor>(autorContrato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(autorContrato.Id, result.Id);
            Assert.Equal(autorContrato.Nome, result.Nome);
            Assert.Equal(autorContrato.DataNascimento, result.DataNascimento);
        }

        [Fact]
        public void Map_ShouldMapAutorToAutor()
        {
            // Arrange
            var autorSource = AutorFixture.BuildEntity(true);
            autorSource.Nome = "Stephen King";
            var autorDestination = AutorFixture.BuildEntity(true);
            autorDestination.Nome = "Rick Riordan";

            // Act
            _mapper.Map(autorSource, autorDestination);

            // Assert
            Assert.Equal(autorSource.Id, autorDestination.Id);
            Assert.Equal(autorSource.Nome, autorDestination.Nome);
            Assert.Equal(autorSource.DataNascimento, autorDestination.DataNascimento);
        }
        #endregion

        #region Livro
        [Fact]
        public void Map_ShouldMapLivroToLivroContrato()
        {
            // Arrange
            var livro = LivroFixture.BuildEntity(true);

            // Act
            var result = _mapper.Map<LivroContrato>(livro);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(livro.Id, result.Id);
            Assert.Equal(livro.Titulo, result.Titulo);
        }

        [Fact]
        public void Map_ShouldMapLivroContratoToLivro()
        {
            // Arrange
            var livroContrato = LivroFixture.BuildContrato(true);

            // Act
            var result = _mapper.Map<Livro>(livroContrato);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(livroContrato.Id, result.Id);
            Assert.Equal(livroContrato.Titulo, result.Titulo);
        }

        [Fact]
        public void Map_ShouldMapLivroToLivro()
        {
            // Arrange
            var livroSource = LivroFixture.BuildEntity(true);
            livroSource.Titulo = "It";
            var livroDestination = LivroFixture.BuildEntity(true);
            livroDestination.Titulo = "Percy Jackson & the Olympians: The Lightning Thief";

            // Act
            _mapper.Map(livroSource, livroDestination);

            // Assert
            Assert.Equal(livroSource.Id, livroDestination.Id);
            Assert.Equal(livroSource.Titulo, livroDestination.Titulo);
        }
        #endregion
    }
}
