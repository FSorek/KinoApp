namespace Kina.Mobile.Core.Converters
{
    class BooleanRateConverter
    {
        public long Convert(object value)
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

        public object ConvertBack(object value, object parameter)
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
