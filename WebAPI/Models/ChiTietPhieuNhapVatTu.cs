namespace WebAPI.Models
{
    public class ChiTietPhieuNhapVatTu
    {
        public Guid PhieuNhapId { get; set; }
        public Guid VatTuId { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
    }
}
