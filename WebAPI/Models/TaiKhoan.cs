using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace WebAPI.Models
{
    public class TaiKhoan
    {
        [Key]
        public Guid Id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public Guid NhomNguoiDungId { get; set; }
    }
}
