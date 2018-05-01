using System.Collections;
using System.Collections.Generic;

namespace Kina.Mobile.UnitTests.ClassData
{
    public class TestMovieIds : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>
        {
            new object[] { 5 },
            new object[] { 22 },
            new object[] { 50 },
            new object[] { 12 },
            new object[] { 112 }
        };

        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
