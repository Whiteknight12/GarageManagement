using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ChiTietPhieuSuaChua
    {
        [Key]
        public Guid PhieuSuaChuaId { get; set; }
        [Key]
        public Guid VatTuPhuTungId {  get; set; }
        public Guid TienCongId { get; set; }

        public string? NoiDung { get; set; }
        // so luong VatTu (thuoc 1 loai) 
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien {  get; set; } // = VatTu.DonGia * SoLuong + TienCong.Tien
    }
}   
