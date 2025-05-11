using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class KhachHang
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string HoVaTen { get; set; }
        public int? Tuoi { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public bool DaCoTaiKhoan { get; set; }
        public double TienNo { get; set; }
        public Guid NhomNguoiDungId { get; set; }
        public string? Email { get; set; }
    }
}
