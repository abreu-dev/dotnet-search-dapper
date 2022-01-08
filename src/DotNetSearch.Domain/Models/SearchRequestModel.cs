namespace DotNetSearch.Domain.Models
{
    public class SearchRequestModel
    {
        public string Filter { get; set; }
        public SearchSortModel[] Sort { get; set; }
    }
}
