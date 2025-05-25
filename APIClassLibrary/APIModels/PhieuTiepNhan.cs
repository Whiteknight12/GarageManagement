using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuTiepNhan: INotifyPropertyChanged
    {
        public Guid Id { get; set; } 
        public DateTime? NgayTiepNhan { get; set; }
        //khoa ngoai 
        public Guid XeId { get; set; }
        public bool DaHoanThanhBaoTri { get; set; }

        //field de xu li UI
        public int? IdForUI { get; set; }
        public string? BienSoXe { get; set; }
        public string? TinhTrang { get; set; } 
        public string? TenKhachHang { get; set; }
        public Guid? KhachHangId { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
