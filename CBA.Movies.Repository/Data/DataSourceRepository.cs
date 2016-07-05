using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;

namespace CBA.Movies.Repository.Data
{
    public interface IDataSourceRepository
    {
        int Create(Movie movie);
        IEnumerable<Movie> GetAllMovies();
        Movie GetDataById(int id);
        IEnumerable<Movie> GetMoviesByPage(int pageNo, int moviesPerPage,IEnumerable<Movie> movies);
        IEnumerable<Movie> Search(SearchQuery movieSearchValues, IEnumerable<Movie> movies);
        IEnumerable<Movie> Sort(SortField sortField, bool sortAscending, IEnumerable<Movie> movies);
        void Update(Movie movie);
    }

    /// <summary>
    /// Class having methods for all requirements 
    /// It extends the data source class to return filtered, sorted movies, which can then be returned page by page
    /// </summary>
    [AutoWired(LifetimeScope.SingleInstance)]
    public class DataSourceRepository : IDataSourceRepository 
    {
        private readonly IDataSourceAdapter _dataSource;
        
        public DataSourceRepository(IDataSourceAdapter dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
           return _dataSource.GetAllData().Select(m => m.ToMovie());

        }
       
        public Movie GetDataById(int id)
        {
            return _dataSource.GetDataById(id).ToMovie();
        }

        public int Create(Movie movie)
        {            
            return _dataSource.Create(movie.ToMovieData());

        }

        public void Update(Movie movie)
        {
             _dataSource.Update(movie.ToMovieData());
             return; 
        }

        public IEnumerable<Movie> GetMoviesByPage(int pageNo, int moviesPerPage, IEnumerable<Movie> movies)
        {
            int Skip = (pageNo - 1) * moviesPerPage;
            int Take = movies.Count() - Skip;
            Take = Take >= moviesPerPage ? moviesPerPage : Take;
            return movies.Skip(Skip).Take(Take);

        } 

        public IEnumerable<Movie> Sort(SortField sortField, bool sortAscending, IEnumerable<Movie> movies)
        {
            PropertyInfo property = typeof(Movie).GetProperty(sortField.ToString());

            if (property == null)
                throw new ArgumentException("Invalid Sort Field provided");

            movies = sortAscending
                ? movies.OrderBy(x => property.GetValue(x, null))
                : movies.OrderByDescending(x => property.GetValue(x, null));

            return movies;
        }

        public IEnumerable<Movie> Search(SearchQuery searchQuery, IEnumerable<Movie> movies)
        {

            if (searchQuery.Id.HasValue)
            {
                movies = movies.Where(m => m.MovieId.Equals(searchQuery.Id));
            }
            if (searchQuery.Rating.HasValue)
            {
                movies = movies.Where(m => m.Rating.Equals(searchQuery.Rating));
            }
            if (searchQuery.ReleaseDate.HasValue)
            {
                movies = movies.Where(m => m.ReleaseDate.Equals(searchQuery.ReleaseDate));
            }
            if (!String.IsNullOrEmpty(searchQuery.Title))
            {
                movies = movies.Where(m => m.Title.ToLower().Contains(searchQuery.Title.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchQuery.Cast))
            {
                movies = movies.Where(m => m.Cast.Any(c => c.ToLower().Contains(searchQuery.Cast.ToLower())));
            }
            if (!String.IsNullOrEmpty(searchQuery.Classification))
            {
                movies = movies.Where(m => m.Classification.ToLower().Contains(searchQuery.Cast.ToLower()));
            }
            if (!String.IsNullOrEmpty(searchQuery.Genre))
            {
                movies = movies.Where(m => m.Genre.ToLower().Contains(searchQuery.Genre.ToLower()));
            }
            return movies;
        }
       
    }
}
