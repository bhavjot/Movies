using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CBA.MoviesSource.Data
{
    public interface IMovieDataSourceRepository
    {
        int Create(Movie movie);
        IEnumerable<Movie> GetAllMovies();
        Movie GetDataById(int id);
        IEnumerable<Movie> GetMoviesByPage(int PageNo, int moviesPerPage,IEnumerable<Movie> movies);
        IEnumerable<Movie> Search(MovieSearch movieSearchValues, IEnumerable<Movie> movies);
        IEnumerable<Movie> Sort(MovieField movieSortField, bool sortAscending, IEnumerable<Movie> movies);
        void Update(Movie movie);
    }

    /// <summary>
    /// Class having methods for all requirements 
    /// It extends the data source class to return filtered, sorted movies, which can then be returned page by page
    /// </summary>
    public class MovieDataSourceRepository : IMovieDataSourceRepository 
    {
        private IMovieDataSource _movieDataSource;
        
        public MovieDataSourceRepository(IMovieDataSource movieDataSource)
        {
            _movieDataSource = movieDataSource;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
           return _movieDataSource.GetAllData().Select(m => m.ToMovie());

        }
       
        public Movie GetDataById(int id)
        {
            return _movieDataSource.GetDataById(id).ToMovie();
        }

        public int Create(Movie movie)
        {            
            return _movieDataSource.Create(movie.ToMovieData());

        }

        public void Update(Movie movie)
        {
             _movieDataSource.Update(movie.ToMovieData());
             return; 
        }

        public IEnumerable<Movie> GetMoviesByPage(int PageNo, int moviesPerPage, IEnumerable<Movie> movies)
        {
            int Skip = (PageNo - 1) * moviesPerPage;
            int Take = movies.Count() - Skip;
            Take = Take >= moviesPerPage ? moviesPerPage : Take;
            return movies.Skip(Skip).Take(Take);

        } 

        public IEnumerable<Movie> Sort(MovieField movieSortField, bool sortAscending, IEnumerable<Movie> movies)
        {
            PropertyInfo property = typeof(Movie).GetProperty(movieSortField.ToString());

            if (property == null)
                throw new ArgumentException("Invalid Movie Sort Field provided");

            movies = sortAscending
                ? movies.OrderBy(x => property.GetValue(x, null))
                : movies.OrderByDescending(x => property.GetValue(x, null));

            return movies;
        }

        public IEnumerable<Movie> Search(MovieSearch movieSearchValues, IEnumerable<Movie> movies)
        {

            if (movieSearchValues.Id.HasValue)
            {
                movies = movies.Where(m => m.MovieId.Equals(movieSearchValues.Id));
            }
            if (movieSearchValues.Rating.HasValue)
            {
                movies = movies.Where(m => m.Rating.Equals(movieSearchValues.Rating));
            }
            if (movieSearchValues.ReleaseDate.HasValue)
            {
                movies = movies.Where(m => m.ReleaseDate.Equals(movieSearchValues.ReleaseDate));
            }
            if (!String.IsNullOrEmpty(movieSearchValues.Title))
            {
                movies = movies.Where(m => m.Title.ToLower().Contains(movieSearchValues.Title.ToLower()));
            }
            if (!String.IsNullOrEmpty(movieSearchValues.Cast))
            {
                movies = movies.Where(m => m.Cast.Any(c => c.ToLower().Contains(movieSearchValues.Cast.ToLower())));
            }
            if (!String.IsNullOrEmpty(movieSearchValues.Classification))
            {
                movies = movies.Where(m => m.Classification.ToLower().Contains(movieSearchValues.Cast.ToLower()));
            }
            if (!String.IsNullOrEmpty(movieSearchValues.Genre))
            {
                movies = movies.Where(m => m.Genre.ToLower().Contains(movieSearchValues.Genre.ToLower()));
            }
            return movies;
        }
       
    }
}
