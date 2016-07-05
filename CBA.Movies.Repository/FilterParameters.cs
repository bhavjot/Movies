
namespace CBA.Movies.Repository
{
    public enum SortField
    {
        Id,
        Classification,
        Title,
        Genre,
        Rating,
        ReleaseDate
    }
    public class SearchQuery
    {
        public int? Id { get; set; }
        public string Classification { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int? Rating { get; set; }
        public string Cast { get; set; }
        public int? ReleaseDate { get; set; }
    }
    

    public class FilterParameters
    {
        public FilterParameters()
        {
            SearchQuery = new SearchQuery();
            SortField = SortField.Title;
            SortAscending = true;
            PageNo = 1;
            ItemsPerPage = 20;
        }

        public SearchQuery SearchQuery { get; set; }

        public SortField SortField { get; set; }
        public bool SortAscending { get; set; }

        public int PageNo { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
