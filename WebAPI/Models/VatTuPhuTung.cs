using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class VatTuPhuTung
    {
        [Key]
        public Guid VatTuPhuTungId { get; set; } = Guid.NewGuid();
        public string TenLoaiVatTuPhuTung { get; set; }
        public double GiaLoaiVatTuPhuTung { get; set; }
    }
}
