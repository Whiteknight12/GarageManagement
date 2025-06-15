using APIClassLibrary.APIModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace GarageManagement.Services
{
    public class NoiDungSuaChuaConverter: IValueConverter
    {
        public ObservableCollection<NoiDungSuaChua> NoiDungList { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid id && NoiDungList != null)
            {
                if (NoiDungList is not null && NoiDungList.Any())
                {
                    var returnItem= NoiDungList.FirstOrDefault(x => x.Id == id);
                    if ( returnItem != null ) return returnItem;
                }     
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
