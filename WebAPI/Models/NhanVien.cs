using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class NhanVien
    {
        [Key]
        public Guid Id { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public Guid? TaiKhoanId { get; set; }
    }
}
