using DotNetSearch.Infra.CrossCutting.LinqSearch.Enums;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos
{
    public class LinqSearchSortContrato
    {
        public string PropertyName { get; set; }
        public LinqSearchSortDirection Direction { get; set; }
    }
}
