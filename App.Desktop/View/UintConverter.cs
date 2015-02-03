using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Walle.View
{
    class UintConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var integer = value is uint ? (uint) value : 0;
            return integer.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint integer;
            if(uint.TryParse(value.ToString(), out integer))
                return integer;
            return 0;
        }
    }
}
