using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class DeleteModeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Chuyển đổi từ bool thành string
            if (value is bool isDeleteMode)
            {
                return isDeleteMode ? "Hủy" : "Xóa";
            }

            return "Xóa"; // Giá trị mặc định
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
