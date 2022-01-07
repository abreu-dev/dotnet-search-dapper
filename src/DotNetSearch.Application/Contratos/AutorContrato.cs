using System;

namespace DotNetSearch.Application.Contratos
{
    public class AutorContrato : Contrato
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
