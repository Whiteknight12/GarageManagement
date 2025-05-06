using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APIClassLibrary.APIModels
{
    public class ChiTietPhieuSuaChua : INotifyPropertyChanged
    {
        public Guid PhieuSuaChuaId { get; set; }
        public Guid VatTuPhuTungId { get; set; }
        public Guid TienCongId { get; set; }

        public string? NoiDung { get; set; }
        // so luong VatTu (thuoc 1 loai) 
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }
        public double DonGia { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
