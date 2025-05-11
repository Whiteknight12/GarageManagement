namespace WebAPI.Models
{
    public class ChiTietBaoCaoTon
    {
        public Guid Id { get; set; }
        public Guid VatTuPhuTungId { get; set; }
        public Guid BaoCaoTonId { get; set; }
        public int TonDau { get; set; }
        public int PhatSinh { get; set; }
        public int TonCuoi { get; set; }
    }
}
