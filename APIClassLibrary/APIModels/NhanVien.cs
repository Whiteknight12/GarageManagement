using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class NhanVien 
    {
        public Guid Id { get; set; }
        public string CCCD { get; set; } = string.Empty;
        public string GioiTinh { get; set; } = string.Empty;
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public Guid? TaiKhoanId { get; set; }
        public int STT { get; set; }
        public bool IsSelected { get; set; } = false; 
    }
}
