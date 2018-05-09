using System.Collections.Generic;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Model
{
    public class Group<T> : List<T>
    {
        public Color TextColor { get; set; }
        public string DisplayName { get; set; }
        public string ShortName { get; set; }

        public Group(Color textColor, string displayName, string shortName)
        {
            TextColor = textColor;
            DisplayName = displayName;
            ShortName = shortName;
        }
    }
}
