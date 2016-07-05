using System;
using System.Collections.Generic;
using System.Linq;
using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;
using CBA.Movies.DAL;
using MoviesLibrary;
using log4net;

namespace CBA.Movies.Repository.Data
{
    public interface IDataSourceAdapter
    {
        List<MovieData> GetAllData();
        MovieData GetDataById(int id);
        int Create(MovieData movie);
        void Update(MovieData movie);
    }

    /// <summary>
    /// Class to return data either from cache or by making calls to actual movie library
    /// </summary>
    [AutoWired(LifetimeScope.SingleInstance)]
    public class DataSourceAdapter :  IDataSourceAdapter
    {
        public static readonly string CacheKey = "MOVIE_DATA";
       
        private readonly IDataSource _dataSource;
        private readonly TimeSpan _dataRefreshFrequency;
        private readonly Func<DateTimeOffset> _dateTimeNow;

        private ICache _cache;

        public ILog Log { get; set; }
        
        public DataSourceAdapter(IDataSource dataSource,ICache cache) 
        { 
            _dataSource = dataSource;
            _cache = cache;
            _dataRefreshFrequency = new TimeSpan(1,0,0,0); 
            _dateTimeNow = () => DateTimeOffset.Now;
        }

          
        public List<MovieData> GetAllData()
        {
            List<MovieData> movieDataList;
            // lock so that only one reader executes this while rest wait
            lock (typeof(DataSourceAdapter))
            {
                Log.Debug("Cached Movies");
                movieDataList = _cache.Get<List<MovieData>>(CacheKey);

                if (movieDataList == null)
                {
                    movieDataList = _dataSource.GetAllData();
                    _cache.Add(CacheKey, movieDataList, _dateTimeNow().Date.Add(_dataRefreshFrequency));
                }
            }
            return movieDataList;
        }
       
        public MovieData GetDataById(int id)
        {
            return GetAllData().SingleOrDefault(m => m.MovieId.Equals(id));
        }

        public int Create(MovieData movie)
        {
            _cache.Remove(CacheKey);
            return _dataSource.Create(movie);
        }

        public void Update(MovieData movie)
        {
            _cache.Remove(CacheKey);
            _dataSource.Update(movie);
            return;
        }
    
    }
}
