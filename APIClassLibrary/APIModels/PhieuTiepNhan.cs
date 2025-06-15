using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuTiepNhan : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public DateTime? NgayTiepNhan { get; set; }
        public Guid XeId { get; set; }

        private bool _daHoanThanhBaoTri;
        public bool DaHoanThanhBaoTri
        {
            get => _daHoanThanhBaoTri;
            set
            {
                _daHoanThanhBaoTri = value;
                OnPropertyChanged(nameof(DaHoanThanhBaoTri)); // Thêm dòng này
            }
        }

        public int? IdForUI { get; set; }
        public string? BienSoXe { get; set; }
        public string? TinhTrang
        {
            get => _tinhTrang;
            set
            {
                _tinhTrang = value;
                OnPropertyChanged(nameof(TinhTrang));
            }
        }
        private string? _tinhTrang;
        public string? TenKhachHang { get; set; }
        public Guid? KhachHangId { get; set; }
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
