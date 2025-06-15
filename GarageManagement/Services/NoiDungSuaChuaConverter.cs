using APIClassLibrary.APIModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class NoiDungSuaChuaConverter: IValueConverter
    {
        public IEnumerable<NoiDungSuaChua> NoiDungList { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid id && NoiDungList != null)
            {
                return NoiDungList.FirstOrDefault(x => x.Id == id);
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
