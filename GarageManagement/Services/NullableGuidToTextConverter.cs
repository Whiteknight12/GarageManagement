using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class NullableGuidToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Guid guid && guid != Guid.Empty
               ? guid.ToString()
               : "Chưa có tài khoản";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && Guid.TryParse(s, out var guid))
                return guid;
            return null;
        }
    }
}
