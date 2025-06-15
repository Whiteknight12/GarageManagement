using APIClassLibrary.APIModels;
using System.Globalization;

namespace GarageManagement.Services
{
    public class VatTuPhuTungIdConverter: IValueConverter
    {
        public IEnumerable<VatTuPhuTung> VatTuList { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid id && VatTuList != null)
            {
                return VatTuList.FirstOrDefault(x => x.VatTuPhuTungId == id);
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
