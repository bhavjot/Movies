using System.Collections.Generic;
using System.Linq;
using MoviesLibrary;

namespace CBA.Movies.Repository.Test
{
    public class MockData
    {
        public MockData()
        {
            MockMovieDataList = new[]
            {
                new MovieData
                {
                    Cast = new[] {"Angelina Jolie", "Brad Pitt"},
                    Classification = "MA",
                    Genre = "Romance",
                    MovieId = 1,
                    Rating = 4,
                    ReleaseDate = 2014,
                    Title = "Test Movie 1"
                },
                new MovieData
                {
                    Cast = new[] {"Leonardo DiCaprio", "Sandra Bullock"},
                    Classification = "MA",
                    Genre = "Action",
                    MovieId = 2,
                    Rating = 5,
                    ReleaseDate = 2014,
                    Title = "Test Movie 2"
                }
            }.ToList();

            MockMovieList = new[]
            {
                new Movie
                {
                    Cast = new[] {"Angelina Jolie", "Brad Pitt"},
                    Classification = "MA",
                    Genre = "Romance",
                    MovieId = 1,
                    Rating = 4,
                    ReleaseDate = 2014,
                    Title = "Test Movie 1"
                },
                new Movie
                {
                    Cast = new[] {"Leonardo DiCaprio", "Sandra Bullock"},
                    Classification = "MA",
                    Genre = "Action",
                    MovieId = 2,
                    Rating = 5,
                    ReleaseDate = 2014,
                    Title = "Test Movie 2"
                }
            }.ToList();
        }

        public List<MovieData> MockMovieDataList { get; private set; }

        public List<Movie> MockMovieList { get; private set; }
    }
}
