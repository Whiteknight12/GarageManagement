using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class BaoCaoDoanhThuThang
    {
        [Key]
        public Guid Id { get; set; }
        public int Thang { get; set; }
        public double TongDoanhThu { get; set; }
    }
}
