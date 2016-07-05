using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;
using CBA.Movies.Repository.Data;
using System.Collections.Generic;

namespace CBA.Movies.Repository
{
    public interface IDataService
    {
        int Create(Movie movie);
        IEnumerable<Movie> GetMovies(FilterParameters filterParam);
        void Update(Movie movie);
    }

    [AutoWired(LifetimeScope.SingleInstance)]
    public class DataService : IDataService
    {
        private readonly IDataSourceRepository _dataSourceRepository;

        public DataService(IDataSourceRepository dataSourceRepository)
        {
            _dataSourceRepository = dataSourceRepository;           
        }

        public IEnumerable<Movie> GetMovies(FilterParameters filterParam)
        {

            var movies = _dataSourceRepository.GetAllMovies();
            if (filterParam != null){

                movies = _dataSourceRepository.Search(filterParam.SearchQuery, movies);
                movies = _dataSourceRepository.Sort(filterParam.SortField, filterParam.SortAscending, movies);
                movies = _dataSourceRepository.GetMoviesByPage(filterParam.PageNo, filterParam.ItemsPerPage, movies);
            }
           
            return movies;
        }

        public int Create(Movie movie)
        {
            return _dataSourceRepository.Create(movie);
        }

        public void Update(Movie movie)
        {
            _dataSourceRepository.Update(movie);
            return;
        }
    }
}
