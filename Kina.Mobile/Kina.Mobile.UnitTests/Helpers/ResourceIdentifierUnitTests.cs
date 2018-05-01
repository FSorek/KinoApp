using Kina.Mobile.Core.Helpers;
using Kina.Mobile.UnitTests.ClassData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kina.Mobile.UnitTests.Helpers
{
    public class ResourceIdentifierUnitTests
    {
        [Fact]
        public void CategoryUriReturnsCategoryUri()
        {
            var expected = "https://epertuar.azurewebsites.net/api/Movie/Genres";
            var actual = ResourceIdentifier.CategoryUri();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(CityNames))]
        public void CinemasInCityReturnsProperShowUri(string city)
        {
            var expectedRegexPattern = @"https://epertuar.azurewebsites.net/api/Show/[a-zA-Ząćęłńóśźż\s-]+";
            var actual = ResourceIdentifier.CinemasInCityUri(city);
            Assert.Matches(expectedRegexPattern, actual);
        }

        [Theory]
        [ClassData(typeof(TestLocations))]
        public void CinemasInRangeReturnsProperDistanceUri(double latitude, double longtitude, int distance)
        {
            var expectedRegexPatter = @"https://epertuar.azurewebsites.net/api/Show/Distance\?Lng=\d+.*\d+&Lat=\d+.*\d+&range=\d+";
            var actual = ResourceIdentifier.CinemasInRangeUri(latitude, longtitude, distance);
            Assert.Matches(expectedRegexPatter, actual);
        }

        [Fact]
        public void CityUriReturnsCityUri()
        {
            var expected = "https://epertuar.azurewebsites.net/api/Cinema/Cities";
            var actual = ResourceIdentifier.CityUri();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestMovieIds))]
        public void MovieUriReturnsProperMovieUri(long id)
        {
            var expectedRegexPattern = @"https://epertuar.azurewebsites.net/api/Movie/\d+";
            var actual = ResourceIdentifier.MovieUri(id);
            Assert.Matches(expectedRegexPattern, actual);
        }

        [Fact]
        public void RatingUriReturnsRatingUri()
        {
            var expected = "https://epertuar.azurewebsites.net/api/Rating";
            var actual = ResourceIdentifier.RatingUri();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(TestRatingIds))]
        public void ScoreIdReturnsProperRatingUri(long movieId, long cinemaId)
        {
            var expectedRegexPattern = @"https://epertuar.azurewebsites.net/api/Rating\?IdC=\d+&IdMovie=\d+";
            var actual = ResourceIdentifier.ScoreUri(movieId, cinemaId);
            Assert.Matches(expectedRegexPattern, actual);
        }
    }
}
