using System;
using System.Globalization;
using System.Windows.Data;

namespace Walle.View
{
    /// <summary>
    /// Thes converter translates between a string and an unsigned integer. Used in WPF to bind to input boxes where the output value must be a uint.
    /// </summary>
    internal class UintConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var integer = value is uint ? (uint) value : 0;
            return integer.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint integer;
            if (uint.TryParse(value.ToString(), out integer))
                return integer;
            return 0;
        }
    }
}