using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class HieuXe
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? TenHieuXe { get; set; }
    }
}
