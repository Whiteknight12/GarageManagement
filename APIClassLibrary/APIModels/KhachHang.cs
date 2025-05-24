using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class KhachHang
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CCCD { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty;
        public string HoVaTen { get; set; }
        public int? Tuoi { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public bool DaCoTaiKhoan => TaiKhoanId != null;
        public Guid? TaiKhoanId { get; set; }
        public double TienNo { get; set; }
        public string? Email { get; set; }

        public int STT { get; set; }
        public bool IsSelected { get; set; } = false; 
    }
}
