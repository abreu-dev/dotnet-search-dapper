using System.Collections.Generic;

namespace DotNetSearch.Infra.CrossCutting.DapperSearch.Contratos
{
    public class DapperSearchContrato
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public DapperSearchSortContrato Sort { get; set; }
        public IEnumerable<DapperSearchFilterContrato> Filters { get; set; }
    }
}
