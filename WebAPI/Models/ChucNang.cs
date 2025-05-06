using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ChucNang
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TenChucNang { get; set; }
        public string TenManHinhDuocLoad { get; set; }
    }
}
