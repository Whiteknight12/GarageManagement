using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class NhomNguoiDung
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenNhom { get; set; }
    }
}
