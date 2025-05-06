using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class PhanQuyen
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid NhomNguoiDungId { get; set; }
        public Guid ChucNangId { get; set; }
    }
}
