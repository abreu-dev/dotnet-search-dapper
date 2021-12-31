using System;
using System.Collections.Generic;

namespace DotNetSearch.Domain.Entities
{
    public class Autor : Entity
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public virtual IEnumerable<Livro> Livros { get; set; }
    }
}
