using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;
using CBA.MoviesSource.Data;
using System.Collections.Generic;

namespace CBA.MoviesSource
{
    public interface IMovieDataService
    {
        int Create(Movie movie);
        IEnumerable<Movie> GetMovies(MovieFilterParameters filterParam);
        void Update(Movie movie);
    }

    [AutoWired(LifetimeScope.SingleInstance)]
    public class MovieDataService : IMovieDataService
    {
        private readonly IMovieDataSourceRepository _movieDataSourceRepository;

        public MovieDataService(IMovieDataSourceRepository movieDataSourceRepository)
        {
            _movieDataSourceRepository = movieDataSourceRepository;           
        }

        public IEnumerable<Movie> GetMovies(MovieFilterParameters filterParam)
        {

            var movies = _movieDataSourceRepository.GetAllMovies();
            if (filterParam != null){

                movies = _movieDataSourceRepository.Search(filterParam.MovieSearch, movies);
                movies = _movieDataSourceRepository.Sort(filterParam.SortField, filterParam.SortAscending, movies);
                movies = _movieDataSourceRepository.GetMoviesByPage(filterParam.PageNo, filterParam.ItemsPerPage, movies);
            }
           
            return movies;
        }

        public int Create(Movie movie)
        {
            return _movieDataSourceRepository.Create(movie);
        }

        public void Update(Movie movie)
        {
            _movieDataSourceRepository.Update(movie);
            return;
        }
    }
}
