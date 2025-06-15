using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class BoolToCompletionTextConverter : IValueConverter
    {
        public string CompletedText { get; set; } = "Đã hoàn thành";
        public string NotCompletedText { get; set; } = "Chưa hoàn thành";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value is bool b && b ? CompletedText : NotCompletedText;

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
