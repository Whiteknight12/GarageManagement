using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class BoolToAccentConverter : IValueConverter
    {
        // Active color (when bool==true)
        private static readonly Color ActiveColor = Color.FromArgb("#3B82F6");
        // Inactive color (when bool==false)
        private static readonly Color InactiveColor = Colors.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return ActiveColor;
            return InactiveColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // one-way binding only
            throw new NotImplementedException();
        }
    }
}
