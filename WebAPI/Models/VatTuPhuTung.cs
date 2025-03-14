using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class VatTuPhuTung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VTPTID { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
}
