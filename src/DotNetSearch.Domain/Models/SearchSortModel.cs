using DotNetSearch.Domain.Enums;

namespace DotNetSearch.Domain.Models
{
    public class SearchSortModel
    {
        public string PropertyName { get; set; }
        public SearchSortDirection Direction { get; set; }
    }
}
