using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class KhachHang
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CCCD { get; set; } = string.Empty;
        public string HoVaTen { get; set; }
        public int? Tuoi { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public Guid? TaiKhoanId { get; set; }
        // không dùng đến, tính trong UI 
        public double TienNo { get; set; }
        public string? Email { get; set; }
        public string GioiTinh { get; set; } = string.Empty;
    }
}
