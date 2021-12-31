using System;

namespace DotNetSearch.Domain.Entities
{
    public class LivroCategorias : Entity
    {
        public Livro Livro { get; set; }
        public Guid LivroId { get; set; }

        public Categoria Categoria { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
