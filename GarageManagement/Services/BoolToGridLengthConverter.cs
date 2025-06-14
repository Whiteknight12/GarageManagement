using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace GarageManagement.Services;

public class BoolToGridLengthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isCollapsed = (bool)value;

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            // Android: nhỏ hơn so với Windows/iOS
            return isCollapsed ? new GridLength(60) : new GridLength(100);
        }
        else
        {
            // Windows hoặc iOS
            return isCollapsed ? new GridLength(80) : new GridLength(200);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
