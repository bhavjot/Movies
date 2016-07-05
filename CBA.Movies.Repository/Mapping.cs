
using MoviesLibrary;

namespace CBA.Movies.Repository
 {    
   public static class Mapping
    {
        public static MovieData ToMovieData(this Movie movie)
        {
            var movieData = new MovieData
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Cast = movie.Cast,
                Classification = movie.Classification,
                Genre = movie.Genre,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate
            };

            return movieData;
        }

        public static Movie ToMovie(this MovieData movieData)
        {
            var movie = new Movie
            {
                MovieId = movieData.MovieId,
                Title = movieData.Title,
                Cast = movieData.Cast,
                Classification = movieData.Classification,
                Genre = movieData.Genre,
                Rating = movieData.Rating,
                ReleaseDate = movieData.ReleaseDate
            };

            return movie;
        }
    }
}