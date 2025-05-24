using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class TaiKhoan
    {
        public Guid Id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public Guid NhomNguoiDungId { get; set; }
        public DateTime NgayCap { get; set; }

        //nhung truong duoi chi de xu li UI, khong luu vao db
        public string? HoTenNguoiDung { get; set; }
        public string? VaiTro { get; set; }
        public string? DangHoatDong { get; set; }
        public string? CCCD { get; set; }
        public int? UIId { get; set; }
    }
}
