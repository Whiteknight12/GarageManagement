using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class NameToIDConverterService: IValueConverter
    {
        public static Dictionary<string, string> HieuXeMap { get; set; } = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = value as string;
            if (id != null && HieuXeMap.TryGetValue(id, out var ten))
            {
                return ten;
            }
            return "...";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
