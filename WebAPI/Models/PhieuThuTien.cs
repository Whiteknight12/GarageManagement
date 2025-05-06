using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PhieuThuTien
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public Guid KhachHangId { get; set; }
        public Guid XeId { get; set; }
        public string Email { get; set; }
        public DateTime NgayThuTien { get; set; }
        public double SoTienThu {  get; set; }
    }
}
