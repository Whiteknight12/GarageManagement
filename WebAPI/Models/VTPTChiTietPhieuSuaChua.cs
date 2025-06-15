using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class VTPTChiTietPhieuSuaChua
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChiTietPhieuSuaChuaId { get; set; }
        public Guid VatTuPhuTungId { get; set; }
        public double DonGiaVTPTApDung { get; set; }
        public int SoLuong { get; set; }
    }
}
