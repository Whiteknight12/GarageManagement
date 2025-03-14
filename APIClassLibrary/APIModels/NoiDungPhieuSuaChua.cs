using System.ComponentModel;

namespace APIClassLibrary.APIModels
{
    public class NoiDungPhieuSuaChua : INotifyPropertyChanged
    {
        public int NoiDungID { get; set; }
        public string? NoiDung { get; set; }
        public int? PhieuSuaChuaID { get; set; }
        public int? VatTuPhuTungID { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }
        public int? TienCongID { get; set; }
        public double? ThanhTien { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
