using DotNetSearch.Infra.CrossCutting.LinqSearch.Enums;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos
{
    public class SearchFilterContrato
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public SearchFilterOperation Operation { get; set; }
    }
}
