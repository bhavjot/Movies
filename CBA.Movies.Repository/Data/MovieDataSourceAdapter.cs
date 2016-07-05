using System;
using System.Collections.Generic;
using System.Linq;
using MoviesLibrary;

namespace CBA.MoviesSource.Data
{
    /// <summary>
    /// Class to return data either from cache or by making calls to actual movie library
    /// </summary>
    public class MovieDataSourceAdapter : IMovieDataSource
    {
        public static readonly string CacheKey = "MOVIE_DATA";
       
        private IMovieDataSource _movieDataSource;
        private readonly TimeSpan _dataRefreshFrequency;
        private readonly Func<DateTimeOffset> _dateTimeNow;

        private IMovieCache _cache;
        
        
        public MovieDataSourceAdapter(IMovieDataSource movieDataSource,IMovieCache cache,TimeSpan dataRefreshFrequency,
            Func<DateTimeOffset> dateTimeNow) 
        { 
            _movieDataSource = movieDataSource;
            _cache = cache;
            _dataRefreshFrequency = dataRefreshFrequency; 
            _dateTimeNow = dateTimeNow;
        }

          
        public List<MovieData> GetAllData()
        {
            List<MovieData> _movieDataList;
            // lock so that only one reader executes this while rest wait
            lock (typeof(MovieDataSourceAdapter))
            {
                _movieDataList = _cache.Get<List<MovieData>>(CacheKey);

                if (_movieDataList == null)
                {
                    _movieDataList = _movieDataSource.GetAllData();
                    _cache.Add(CacheKey, _movieDataList, _dateTimeNow().Date.Add(_dataRefreshFrequency));
                }
            }
            return _movieDataList;
        }
       
        public MovieData GetDataById(int id)
        {
            return GetAllData().SingleOrDefault(m => m.MovieId.Equals(id));
        }

        public int Create(MovieData movie)
        {
            _cache.Remove(CacheKey);
            return _movieDataSource.Create(movie);
        }

        public void Update(MovieData movie)
        {
            _cache.Remove(CacheKey);
            _movieDataSource.Update(movie);
            return;
        }
    
    }
}
