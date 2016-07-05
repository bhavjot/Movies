using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;
using MoviesLibrary;
using System.Collections.Generic;

namespace CBA.MoviesSource.Data
{
    public interface IMovieDataSource
    {
        List<MovieData> GetAllData();
        MovieData GetDataById(int id);
        int Create(MovieData movie);
        void Update(MovieData movie);
    }
    /// <summary>
    /// Class wrapping all methods making bare calls to actual library methods
    /// </summary>  
    [AutoWired(LifetimeScope.SingleInstance)]
    public class MovieDataSourceWrapper: IMovieDataSource
    {
        private readonly MovieDataSource _movieDataSource;

        public MovieDataSourceWrapper():this(new MovieDataSource())
        {
            
        }


        public MovieDataSourceWrapper(MovieDataSource movieDataSource) { _movieDataSource = movieDataSource; }

        public List<MovieData> GetAllData()
        {
           return  _movieDataSource.GetAllData();            
        }

        public MovieData GetDataById(int id)
        {
            return _movieDataSource.GetDataById(id);
        }

        public int Create(MovieData movie)
        {
            return _movieDataSource.Create(movie);
        }

        public void Update(MovieData movie)
        {
            _movieDataSource.Update(movie);
        }
    }
}
