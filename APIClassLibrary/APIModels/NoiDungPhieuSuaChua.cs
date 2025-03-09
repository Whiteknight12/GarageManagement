using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class NoiDungPhieuSuaChua
    {
        public int NoiDungID { get; set; }
        public string? NoiDung { get; set; }
        public int? VatTuPhuTungID { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }
        public double? TienCong { get; set; }
        public double? ThanhTien { get; set; }
    }
}
