using System.Collections;
using System.Collections.Generic;

namespace Kina.Mobile.UnitTests.ClassData
{
    public class TestRatingIds : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>
        {
            new object[] { 1, 12 },
            new object[] { 15, 12 },
            new object[] { 14, 12 },
            new object[] { 16, 24 },
            new object[] { 15, 15 }
        };

        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
