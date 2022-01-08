using System;
using System.Collections.Generic;

namespace DotNetSearch.Application.Contratos
{
    public class LivroContrato : Contrato
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string Capa { get; set; }
        public int NumeroPaginas { get; set; }
        public DateTime DataPublicacao { get; set; }
        public AutorContrato Autor { get; set; }
        public IEnumerable<CategoriaContrato> Categorias { get; set; }
    }
}
