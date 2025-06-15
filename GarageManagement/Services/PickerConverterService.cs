using APIClassLibrary.APIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (value is Guid guid && parameter is ObservableCollection<VatTuPhuTung> listVT)
            {
                return listVT.FirstOrDefault(x => x.VatTuPhuTungId == guid);
            }

            if (value is Guid guid2 && parameter is ObservableCollection<NoiDungSuaChua> listND)
            {
                return listND.FirstOrDefault(x => x.Id == guid2);
            }

            if (value is Guid guid3 && parameter is ObservableCollection<TienCong> listTC)
            {
                return listTC.FirstOrDefault(x => x.Id == guid3);
            }

            return null;
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
