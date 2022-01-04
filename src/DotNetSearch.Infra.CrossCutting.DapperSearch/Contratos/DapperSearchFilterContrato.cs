using DotNetSearch.Infra.CrossCutting.DapperSearch.Enums;

namespace DotNetSearch.Infra.CrossCutting.DapperSearch.Contratos
{
    public class DapperSearchFilterContrato
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public DapperSearchFilterOperation Operation { get; set; }
    }
}
