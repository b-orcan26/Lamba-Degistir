using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Server
{
    
     public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val)
            {
                return Brushes.Green.ToString();
            }
            else
            {
                
                return Brushes.Red.ToString();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return Brushes.Green.ToString();
                else
                    return Brushes.Red.ToString();
            }
            return Brushes.Red;
        }
    }
}
