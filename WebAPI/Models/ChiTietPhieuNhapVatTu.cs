using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ChiTietPhieuNhapVatTu
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PhieuNhapId { get; set; }
        public Guid VatTuId { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
    }
}
