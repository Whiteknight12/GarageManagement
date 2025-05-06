using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PhieuSuaChua
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid XeId { get; set; } 
        public DateTime NgaySuaChua {  get; set; }
        public double TongTien { get; set; }
    }
}
