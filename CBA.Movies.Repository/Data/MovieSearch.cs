
namespace CBA.MoviesSource.Data
{
    public enum MovieField
    {
        Id,
        Classification,
        Title,
        Genre,
        Rating,
        ReleaseDate
    }
    public class MovieSearch
    {
        public int? Id { get; set; }
        public string Classification { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int? Rating { get; set; }
        public string Cast { get; set; }
        public int? ReleaseDate { get; set; }
    }
    

    public class MovieFilterParameters
    {
        public MovieFilterParameters()
        {
            MovieSearch = new MovieSearch();
            SortField = MovieField.Title;
            SortAscending = true;
            PageNo = 1;
            ItemsPerPage = 20;
        }

        public MovieSearch MovieSearch { get; set; }

        public MovieField SortField { get; set; }
        public bool SortAscending { get; set; }

        public int PageNo { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
