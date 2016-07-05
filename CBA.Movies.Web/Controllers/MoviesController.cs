using System.Collections.Generic;
using System.Web.Http;
using CBA.Movies.Repository;

namespace CBA.Movies.Web.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly IDataService _movieDataService;

        public MoviesController(IDataService movieDataService)
        {
            _movieDataService = movieDataService;
        }

       // GET api/<movies>
        public IEnumerable<Movie> Get([FromUri] FilterParameters filterParam)
        {
            filterParam = filterParam ?? new FilterParameters();
            return _movieDataService.GetMovies(filterParam);
        }

        // POST api/movies
        public int Post([FromBody] Movie movie)
        {
            return _movieDataService.Create(movie);
        }

        // PUT api/movies/5
        public void Put(int id, [FromBody] Movie movie)
        {
            movie.MovieId = id;
            _movieDataService.Update(movie);
        }
    }
}