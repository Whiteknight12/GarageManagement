using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class CarRecord
    {
        public int CarRecordID { get; set; }
        public string? TenChuXe { get; set; }
        public string? TenXe { get; set; }
        public string? BienSo { get; set; }
        public string? DiaChi { get; set; }
        public string? HieuXe { get; set; }
        public DateTime? NgayTiepNhan { get; set; }
        public string? SoDienThoai { get; set; }
    }
}
