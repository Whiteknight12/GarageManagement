using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class NoiDungPhieuSuaChua
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoiDungID { get; set; }
        public int? PhieuSuaChuaID { get; set; }
        public string? NoiDung {  get; set; }
        public int? VatTuPhuTungID {  get; set; }
        public int? SoLuong {  get; set; }
        public double? DonGia {  get; set; }
        public int? TienCongID {  get; set; }
        public double? ThanhTien {  get; set; }
    }
}
