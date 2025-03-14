using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PhieuThuTien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhieuThuTienId { get; set; }
        public string? TenChuXe { get; set; }
        public string? BienSoXe { get; set; }
        public string? DienThoai { get; set; }
        public string? Email { get; set; }
        public DateTime? NgayThuTien { get; set; }
        public double? SoTienThu {  get; set; }
        public int? CarID { get; set; }
        public int? UserID { get; set; }
    }
}
