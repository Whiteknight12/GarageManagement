using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class TaiKhoan
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public Guid NhomNguoiDungId { get; set; }
    }
}
