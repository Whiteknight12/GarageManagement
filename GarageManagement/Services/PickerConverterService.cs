﻿using APIClassLibrary.APIModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class PickerConverterService : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is VatTuPhuTung vattuphutung)
            {
                return vattuphutung.VatTuPhuTungId;
            }
            if (value is TienCong tiencong) return tiencong.Id;
            if (value is NoiDungSuaChua noiDungSuaChua) return noiDungSuaChua.Id;
            return null;
        }
    }
}
