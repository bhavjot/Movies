using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;    
using System.Collections.Generic;
using Moq;
using CBA.MoviesSource.Data;
using CBA.MoviesSource;
using NUnit.Framework;

namespace CBA.MoviesSource.Test
{
    [TestFixture]
    public class MovieDataServicTest
    {
        [SetUp]
        public void Setup()
        {
            MockData = new MockData();
        }

        public MockData MockData { get; set; }

        [Test]
        public void service_returns_result_according_to_filter_parameters()
        {
            var movieDataSource = new Mock<IMovieDataSource>();
            var repository = new Mock<IMovieDataSourceRepository>();
          
            var movieService = new MovieDataService(repository.Object);
            var filterParam = new MovieFilterParameters();

            //var filteredMovies = new List<Movie>(Given.SomeMovies);
            //var sortedMovies = new List<Movie>(Given.SomeMovies);
            //var pagedMovies = new List<Movie>(Given.SomeMovies);

            //var page = new Page<Movie>
            //{
            //    Items = pagedMovies,
            //    Skip = query.Skip,
            //    Take = query.Take,
            //    TotalItems = Given.SomeMovies.Count
            //};

            movieDataSource.Setup(x => x.GetAllData()).Returns(MockData.MockMovieDataList);
            //filter.Setup(x => x.Apply(query, It.IsAny<IEnumerable<Movie>>())).Returns(filteredMovies);
            //sort.Setup(x => x.Apply(query.SortBy, query.SortAscending, filteredMovies)).Returns(sortedMovies);
            //pager.Setup(x => x.Page(sortedMovies, query)).Returns(page);

            //Page<Movie> result = service.Search(query);

           // Assert.AreEqual(page, result);
            movieDataSource.Verify(x => x.GetAllData());
            //filter.Verify(x => x.Apply(query, It.IsAny<IEnumerable<Movie>>()));
            //sort.Verify(x => x.Apply(query.SortBy, query.SortAscending, filteredMovies));
            //pager.Verify(x => x.Page(sortedMovies, query));
        }
    }
}
