using DotNetSearch.Infra.CrossCutting.LinqSearch.Enums;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos
{
    public class SearchSortContrato
    {
        public string PropertyName { get; set; }
        public SearchSortDirection Direction { get; set; }
    }
}
