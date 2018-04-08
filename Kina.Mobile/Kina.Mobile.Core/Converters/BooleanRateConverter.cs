using System;
using System.Globalization;
using Xamarin.Forms;

namespace Kina.Mobile.Core.Converters
{
    class BooleanRateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int rate = 0;
            foreach(var boolean in (bool[])value)
            {
                if (boolean)
                {
                    rate++;
                }
            }

            return rate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool[] boolean = new bool[(int)parameter];
            for(int index = (int)value; index >= 0; --index)
            {
                boolean[index] = true;
            }

            return boolean;
        }
    }
}
