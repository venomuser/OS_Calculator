using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS_Calculator.Common.Extensions;

namespace OS_Calculator.MVVM.Converters
{
    public class NumericConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value.ToInt();
            
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value.ToInt() == null)
            {
                return 0;
            }
            else
                return value.ToInt();
        }
    }
}
