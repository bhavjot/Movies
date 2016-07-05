using System.Linq;
using CBA.Movies.Repository.Data;
using NUnit.Framework;
using Moq;
namespace CBA.Movies.Repository.Test
{
    [TestFixture]
    class DataSourceRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
            MockData = new MockData();
        }

        public MockData MockData { get; set; }


        [Test]
        public void repository_return_movies()
        
        {
           var remoteDataSource = new Mock<IDataSourceAdapter>();

            remoteDataSource.Setup(e => e.GetAllData()).Returns(MockData.MockMovieDataList);

          var repository = new DataSourceRepository(remoteDataSource.Object);


          var result = repository.GetAllMovies().ToList();

           Assert.AreEqual(MockData.MockMovieList.Count, result.Count);
        
        }

    }
}



