using System;
using System.Text.Json.Serialization;

namespace DotNetSearch.Domain.Entities
{
    public class LivroCategoria : Entity
    {
        public virtual Livro Livro { get; set; }
        public Guid LivroId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
