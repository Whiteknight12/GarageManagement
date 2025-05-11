using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ChiTietPhieuSuaChua
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PhieuSuaChuaId { get; set; }
        public Guid VatTuPhuTungId {  get; set; }
        public Guid TienCongId { get; set; }

        public string? NoiDung { get; set; }
        public int SoLuong { get; set; }
        //DonGia => trich xuat tu VatTuPhuTung 
        public double ThanhTien {  get; set; } // = VatTu.DonGia * SoLuong + TienCong.Tien
    }
}   
