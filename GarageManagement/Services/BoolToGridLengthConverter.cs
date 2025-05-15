using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace GarageManagement.Services;

public class BoolToGridLengthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? new GridLength(80) : new GridLength(200);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}