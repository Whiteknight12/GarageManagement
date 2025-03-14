using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class PhieuThuTien
    {
        public int PhieuThuTienId { get; set; }
        public string? TenChuXe { get; set; }
        public string? BienSoXe { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }
        public DateTime? NgayThuTien { get; set; }
        public double? SoTienThu { get; set; }
        public int? CarID { get; set; }
        public int? UserID { get; set; }
    }
}
