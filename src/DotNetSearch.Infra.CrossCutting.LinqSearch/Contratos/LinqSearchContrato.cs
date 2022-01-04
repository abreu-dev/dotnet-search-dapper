using System.Collections.Generic;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos
{
    public class LinqSearchContrato
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public LinqSearchSortContrato Sort { get; set; }
        public IEnumerable<LinqSearchFilterContrato> Filters { get; set; }
    }
}
