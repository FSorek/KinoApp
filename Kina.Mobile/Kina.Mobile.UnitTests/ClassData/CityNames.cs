using System.Collections;
using System.Collections.Generic;

namespace Kina.Mobile.UnitTests.ClassData
{
    public class CityNames : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>
        {
            new object[] { "Rumia" },
            new object[] { "Gdynia" },
            new object[] { "Sopot" },
            new object[] { "Gdańsk" },
            new object[] { "Starogard" }
        };

        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
