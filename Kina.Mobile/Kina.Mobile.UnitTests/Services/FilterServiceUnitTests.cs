using Kina.Mobile.Core.Services;
using Kina.Mobile.DataProvider.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Kina.Mobile.UnitTests.Services
{
    public class FilterServiceUnitTests
    {
        string _title;
        string _genre;
        private FilterService _serviceUnderTest;
        private SimpleMovie _movie;

        public FilterServiceUnitTests()
        {
            _title = "Just antoher day";
            _genre = "Action";
            _serviceUnderTest = new FilterService();
            _movie = new SimpleMovie
            {
                Name = _title,
                Genre = new List<string>
                {
                    _genre
                },
                Shows = new List<SimpleShow>
                {
                    new SimpleShow
                    {
                        ShowDate = DateTime.Today.Date,
                        Start = "10:00"
                    }
                }
            };
        }

        [Fact]
        public void ClearingFilterNullifingAllAttributes()
        {
            _serviceUnderTest.Category = "Some category";
            _serviceUnderTest.End = TimeSpan.Parse("6:00");
            _serviceUnderTest.Start = TimeSpan.Parse("9:00");
            _serviceUnderTest.Title = "SomeTitle";

            _serviceUnderTest.ClearFilter();
            List<object> actual = new List<object>
            {
                _serviceUnderTest.Category,
                _serviceUnderTest.End,
                _serviceUnderTest.Start,
                _serviceUnderTest.Title
            };

            Assert.Collection(actual, s => Assert.Null(s),
                                      s => Assert.Equal(default(TimeSpan), (TimeSpan)s),
                                      s => Assert.Equal(default(TimeSpan), (TimeSpan)s),
                                      s => Assert.Null(s));
        }

        [Fact]
        public void CheckingMovieWithShowsForTodayAndNullFilterReturnsTrue()
        {
            var actual = _serviceUnderTest.Check(_movie);
            Assert.True(actual);
        }

        [Fact]
        public void CheckingMovieWithoutShowsReturnsFalse()
        {
            SimpleMovie movie = new SimpleMovie
            {
                Name = "Just another day",
                Genre = new List<string>
                {
                    "Action"
                },
                Shows = new List<SimpleShow>()
            };

            var actual = _serviceUnderTest.Check(movie);
            Assert.False(actual);
        }

        [Fact]
        public void CheckingMovieWithoutShowsForTodayReturnsFalse()
        {
            FilterService serviceUnderTest = new FilterService();
            _movie = new SimpleMovie
            {
                Name = "Just another day",
                Genre = new List<string>
                {
                    "Action"
                },
                Shows = new List<SimpleShow>
                {
                    new SimpleShow
                    {
                        ShowDate = DateTime.Today.Date.AddDays(1.0),
                        Start = "10:00"
                    }
                }
            };

            var actual = _serviceUnderTest.Check(_movie);
            Assert.False(actual);
        }

        [Fact]
        public void CheckingMovieWithMatchingTitleReturnsTrue()
        {
            _serviceUnderTest.Title = _title;

            var actual = _serviceUnderTest.Check(_movie);
            Assert.True(actual);
        }

        [Fact]
        public void CheckingMovieWithoutMatchingTitleReturnsFalse()
        {
            _serviceUnderTest.Title = "Green grass";

            var actual = _serviceUnderTest.Check(_movie);
            Assert.False(actual);
        }

        [Fact]
        public void CheckingMovieWithMatchingCategoryReturnsTrue()
        {
            _serviceUnderTest.Category = _genre;

            var actual = _serviceUnderTest.Check(_movie);
            Assert.True(actual);
        }

        [Fact]
        public void CheckingMovieWithoutMatchingCategoryReturnsFalse()
        {
            _serviceUnderTest.Category = "StrangeCategoryDoNotLook";

            var actual = _serviceUnderTest.Check(_movie);
            Assert.False(actual);
        }

        [Fact]
        public void CheckingMovieBeforeStartSettingReturnsFalse()
        {
            _serviceUnderTest.Start = TimeSpan.Parse("11:00");
            _serviceUnderTest.End = TimeSpan.Parse("12:00");

            var actual = _serviceUnderTest.Check(_movie);
            Assert.False(actual);
        }

        [Fact]
        public void CheckingMovieAfterEndSettingReturnsFalse()
        {
            _serviceUnderTest.Start = TimeSpan.Parse("07:00");
            _serviceUnderTest.End = TimeSpan.Parse("09:00");

            var actual = _serviceUnderTest.Check(_movie);
            Assert.False(actual);
        }

        [Fact]
        public void CheckingMovieBetweenStartAndEndSettingReturnsTrue()
        {
            _serviceUnderTest.Start = TimeSpan.Parse("09:00");
            _serviceUnderTest.End = TimeSpan.Parse("12:00");

            var actual = _serviceUnderTest.Check(_movie);
            Assert.True(actual);
        }
    }
}
