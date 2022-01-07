using DotNetSearch.Application.Contratos;
using DotNetSearch.Domain.Entities;
using System;

namespace DotNetSearch.Tests.Fixtures
{
    public static class AutorFixture
    {
        public static Autor BuildEntity(bool generateId = false)
        {
            return new Autor()
            {
                Id = generateId ? Guid.NewGuid() : Guid.Empty,
                Nome = "Stephen King",
                DataNascimento = DateTime.UtcNow
            };
        }

        public static AutorContrato BuildContrato(bool generateId = false)
        {
            return new AutorContrato()
            {
                Id = generateId ? Guid.NewGuid() : Guid.Empty,
                Nome = "Stephen King",
                DataNascimento = DateTime.UtcNow
            };
        }
    }
}
