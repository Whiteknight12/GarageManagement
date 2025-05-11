using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class VatTuPhuTung
    {
        [Key]
        public Guid VatTuPhuTungId { get; set; } = Guid.NewGuid();
        public string TenLoaiVatTuPhuTung { get; set; }
        public int SoLuong { get; set; }
        public double DonGiaBanLoaiVatTuPhuTung { get; set; } // tạo 1 biến tham số xác định đơn giá bán = bao nhiêu % đơn giá nhập 
    }
}
