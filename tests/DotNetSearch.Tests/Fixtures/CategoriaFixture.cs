using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;
using System;

namespace DotNetSearch.Tests.Fixtures
{
    public static class CategoriaFixture
    {
        public static Categoria BuildEntity(bool generateId = false)
        {
            return new Categoria()
            {
                Id = generateId ? Guid.NewGuid() : Guid.Empty,
                Nome = "Terror"
            };
        }

        public static CategoriaContrato BuildContrato(bool generateId = false)
        {
            return new CategoriaContrato()
            {
                Id = generateId ? Guid.NewGuid() : Guid.Empty,
                Nome = "Terror"
            };
        }
    }
}
