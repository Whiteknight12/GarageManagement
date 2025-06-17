using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class ThongBaoColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool daDoc = (bool)value;
            // Khi đã đọc → xám nhạt; khi chưa đọc → xanh lá tươi
            return daDoc
                ? Color.FromArgb("#E5E7EB")  // gray-200
                : Color.FromArgb("#22C55E"); // green-500
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
