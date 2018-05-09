using System.Collections;
using System.Collections.Generic;

namespace Kina.Mobile.UnitTests.ClassData
{
    class TestLocations : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>
        {
            new object[] { 54.1, 18.666, 12 },
            new object[] { 52.6846, 17.9995545, 22 },
            new object[] { 56.11158, 18, 50 },
            new object[] { 50.54862, 16.0004445, 50 },
            new object[] { 53.11857, 17.0005, 5 }
        };

        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
