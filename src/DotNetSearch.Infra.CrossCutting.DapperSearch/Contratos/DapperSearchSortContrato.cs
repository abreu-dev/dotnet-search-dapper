using DotNetSearch.Infra.CrossCutting.DapperSearch.Enums;

namespace DotNetSearch.Infra.CrossCutting.DapperSearch.Contratos
{
    public class DapperSearchSortContrato
    {
        public string PropertyName { get; set; }
        public DapperSearchSortDirection Direction { get; set; }
    }
}
