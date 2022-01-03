using DotNetSearch.Infra.CrossCutting.LinqSearch.Enums;

namespace DotNetSearch.Infra.CrossCutting.LinqSearch.Contratos
{
    public class LinqSearchFilterContrato
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public LinqSearchFilterOperation Operation { get; set; }
    }
}
