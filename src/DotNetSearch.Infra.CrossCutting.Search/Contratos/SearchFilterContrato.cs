using DotNetSearch.Infra.CrossCutting.Search.Enums;

namespace DotNetSearch.Infra.CrossCutting.Search.Contratos
{
    public class SearchFilterContrato
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public SearchFilterOperation Operation { get; set; }
    }
}
