using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class ChiTietBaoCaoDoanhThuThang
    {
        public Guid Id { get; set; }
        public Guid BaoCaoDoanhThuThangId { get; set; }
        public Guid HieuXeId { get; set; }
        public int SoLuotSua { get; set; }
        public double ThanhTien { get; set; }
        public float TiLe { get; set; }
    }
}
