using DotNetSearch.Infra.CrossCutting.Search.Enums;
using System.Collections.Generic;

namespace DotNetSearch.Infra.CrossCutting.Search.Contratos
{
    public class SearchContrato
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public SearchFramework Framework { get; set; }
        public SearchSortContrato Sort { get; set; }
        public IEnumerable<SearchFilterContrato> Filters { get; set; }
    }
}
