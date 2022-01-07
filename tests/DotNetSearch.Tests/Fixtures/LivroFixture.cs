using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DotNetSearch.Tests.Fixtures
{
    public static class LivroFixture
    {
        public static Livro BuildEntity(bool generateId = false)
        {
            var autor = AutorFixture.BuildEntity(true);
            var categoria1 = CategoriaFixture.BuildEntity(true);
            var categoria2 = CategoriaFixture.BuildEntity(true);

            return new Livro()
            {
                Id = generateId ? Guid.NewGuid() : Guid.Empty,
                Titulo = "It (A Coisa)",
                Sinopse = "Sinopse",
                Capa = "Capa",
                NumeroPaginas = 695,
                DataPublicacao = new DateTime(1986, 09, 15),
                Autor = autor,
                AutorId = autor.Id,
                Categorias = new List<LivroCategoria>() {
                    new LivroCategoria() { CategoriaId = categoria1.Id, Categoria = categoria1 },
                    new LivroCategoria() { CategoriaId = categoria2.Id, Categoria = categoria2 }
                }
            };
        }

        public static LivroContrato BuildContrato(bool generateId = false)
        {
            var autor = AutorFixture.BuildContrato(true);

            return new LivroContrato()
            {
                Id = generateId ? Guid.NewGuid() : Guid.Empty,
                Titulo = "It (A Coisa)",
                Sinopse = "Sinopse",
                Capa = "Capa",
                NumeroPaginas = 695,
                DataPublicacao = new DateTime(1986, 09, 15),
                Autor = autor
            };
        }
    }
}
