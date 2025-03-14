using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class PhieuSuaChua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhieuSuaChuaID { get; set; }
        public string? BienSoXe { get; set; }
        public DateTime? NgaySuaChua {  get; set; }
    }
}
