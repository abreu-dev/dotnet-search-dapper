using System;
using System.Collections.Generic;

namespace DotNetSearch.Domain.Entities
{
    public class Livro : Entity
    {
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Capa { get; set; }
        public int NumeroPaginas { get; set; }
        public DateTime DataPublicacao { get; set; }

        public Autor Autor { get; set; }
        public Guid AutorId { get; set; }

        public virtual List<LivroCategoria> Categorias { get; set; } 
    }
}
