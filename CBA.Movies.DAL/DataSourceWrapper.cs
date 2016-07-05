using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;
using System.Collections.Generic;
using MoviesLibrary;

namespace CBA.Movies.DAL
{
    public interface IDataSource
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
    public class DataSourceWrapper: IDataSource
    {
        private readonly MovieDataSource _movieDataSource;

        public DataSourceWrapper():this(new MovieDataSource())
        {
            
        }


        public DataSourceWrapper(MovieDataSource movieDataSource) { _movieDataSource = movieDataSource; }

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
