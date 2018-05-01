using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Kina.Mobile.UnitTests.Services
{
    public class DataServiceUnitTests
    {
        private DataService _dataService;
        private Mock<IDataConverter> _fakeConverter;
        private Mock<IHttpService> _fakeHttpService;

        //fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<string>))).Returns(null);
        public DataServiceUnitTests()
        {
            _fakeConverter = new Mock<IDataConverter>();
            _fakeHttpService = new Mock<IHttpService>();
        }

        #region Get methods returns empty objects when HttpService throw Exception

        [Fact]
        public void GetCategoriesReturnsEmptyListWhenHttpServiceThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<string> actual = _dataService.GetCategories().Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCinemasInCityReturnsEmptyListWhenHttpServiceThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<Cinema> actual = _dataService.GetCinemasInCity("Gdynia").Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCinemasInRangeReturnsEmptyListWhenHttpServiceThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<Cinema> actual = _dataService.GetCinemasInRange(54.5 , 18.5, 50).Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCitiesReturnsEmptyListWhenHttpServiceThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<string> actual = _dataService.GetCities().Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetMovieReturnsEmptyListWhenHttpServiceThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.GetMovie(1).Result;
            var actualStrings = new List<string>()
            {
                actual.Name,
                actual.OriginalName,
                actual.Director,
                actual.Writers,
                actual.Stars,
                actual.Storyline,
                actual.Trailer,
                actual.Rating
            };

            Assert.All(actualStrings, item => Assert.Null(item));
        }

        [Fact]
        public void GetRatingReturnsEmptyListWhenHttpServiceThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.GetRating(1, 1).Result;
            Assert.Empty(actual);
        }

        #endregion

        #region Get methods returns empty objects when DataConverter throw Exception

        [Fact]
        public void GetCategoriesReturnsEmptyListWhenDataConverterThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<string>))).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<string> actual = _dataService.GetCategories().Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCinemasInCityReturnsEmptyListWhenDataConverterThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<Cinema>))).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<Cinema> actual = _dataService.GetCinemasInCity("Gdynia").Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCinemasInRangeReturnsEmptyListWhenDataConverterThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<Cinema>))).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<Cinema> actual = _dataService.GetCinemasInRange(54.5, 18.5, 50).Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetCitiesReturnsEmptyListWhenDataConverterThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<string>))).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<string> actual = _dataService.GetCities().Result;
            Assert.Empty(actual);
        }

        [Fact]
        public void GetMovieReturnsEmptyListWhenDataConverterThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(Movie))).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.GetMovie(1).Result;
            var actualStrings = new List<string>()
            {
                actual.Name,
                actual.OriginalName,
                actual.Director,
                actual.Writers,
                actual.Stars,
                actual.Storyline,
                actual.Trailer,
                actual.Rating
            };

            Assert.All(actualStrings, item => Assert.Null(item));
        }

        [Fact]
        public void GetRatingReturnsEmptyListWhenDataConverterThrowsException()
        {
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<UserScore>))).Throws(new Exception());
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.GetRating(1, 1).Result;
            Assert.Empty(actual);
        }

        #endregion

        #region Get methods returns not-empty objects when services do not throw Exception

        [Fact]
        public void GetCategoriesReturnsNotEmptyList()
        {
            var list = new List<string>
            {
                "Result A", "Result B", "Result C"
            };
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<string>))).Returns(list);
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<string> actual = _dataService.GetCategories().Result;
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetCinemasInCityReturnsNotEmptyList()
        {
            var list = new List<Cinema>
            {
                new Cinema(), new Cinema()
            };
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<Cinema>))).Returns(list);
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<Cinema> actual = _dataService.GetCinemasInCity("Gdynia").Result;
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetCinemasInRangeReturnsNotEmptyList()
        {
            var list = new List<Cinema>
            {
                new Cinema(), new Cinema()
            };
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<Cinema>))).Returns(list);
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<Cinema> actual = _dataService.GetCinemasInRange(54.5, 18.5, 50).Result;
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetCitiesReturnsNotEmptyListn()
        {
            var list = new List<string>
            {
                "Result A", "Result B", "Result C"
            };
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<string>))).Returns(list);
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            List<string> actual = _dataService.GetCities().Result;
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetMovieReturnsNotEmptyMovie()
        {
            var movie = new Movie
            {
                Name = "Developers diary",
                OriginalName = "Developers diary",
                Director = "Maybe me, maybe you",
                Writers = "Papryki team"
            };
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(Movie))).Returns(movie);
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.GetMovie(1).Result;
            var actualStrings = new List<string>()
            {
                actual.Name,
                actual.OriginalName,
                actual.Director,
                actual.Writers,
            };

            Assert.Collection(actualStrings, item => Assert.Equal(movie.Name, item),
                                             item => Assert.Equal(movie.OriginalName, item),
                                             item => Assert.Equal(movie.Director, item),
                                             item => Assert.Equal(movie.Writers, item));
        }

        [Fact]
        public void GetRatingReturnsNotEmptyList()
        {
            var list = new List<UserScore>
            {
                new UserScore(), new UserScore()
            };
            _fakeHttpService.Setup(h => h.Get(It.IsAny<string>())).Returns(Task.FromResult("A very proper json, I swear it"));
            _fakeConverter.Setup(c => c.FromJson(It.IsAny<string>(), typeof(List<UserScore>))).Returns(list);
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.GetRating(1, 1).Result;
            Assert.NotEmpty(actual);
        }

        #endregion

        #region Post method tests
        
        [Fact]
        public void PostScoreReturnsTrueWhenHttpServiceSucceded()
        {
            _fakeHttpService.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<StringContent>())).Returns(Task.FromResult(true));
            _fakeConverter.Setup(c => c.SerializeScore(It.IsAny<UserScore>())).Returns("A very proper serialized user score");
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.PostScore(new UserScore()).Result;
            Assert.True(actual);
        }

        [Fact]
        public void PostScoreReturnsFalseWhenHttpServiceNotSucceded()
        {
            _fakeHttpService.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<StringContent>())).Returns(Task.FromResult(false));
            _fakeConverter.Setup(c => c.SerializeScore(It.IsAny<UserScore>())).Returns("A very proper serialized user score");
            _dataService = new DataService(_fakeHttpService.Object, _fakeConverter.Object);

            var actual = _dataService.PostScore(new UserScore()).Result;
            Assert.False(actual);
        }

        #endregion
    }
}
