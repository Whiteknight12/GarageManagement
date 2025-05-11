using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ChiTietBaoCaoDoanhThuThang
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BaoCaoDoanhThuThangId { get; set; }
        public Guid HieuXeId { get; set; }
        public int SoLuotSua { get; set; }
        public double ThanhTien { get; set; }
        public float TiLe { get; set; }
    }
}
