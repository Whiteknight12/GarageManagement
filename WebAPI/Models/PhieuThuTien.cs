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
        //email lay tu khach hang id ra 
        public DateTime NgayThuTien { get; set; }
        public double SoTienThu {  get; set; }
    }
}
