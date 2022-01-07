using System;

namespace DotNetSearch.Domain.Entities
{
    public class Autor : Entity
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
