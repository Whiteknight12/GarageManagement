using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("CARRECORD")]
    public class CarRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarRecordID { get; set; }
        public string? TenChuXe { get; set; }
        public string? TenXe { get; set; }
        public string? BienSo {  get; set; }
        public string? DiaChi { get; set; }
        public string? HieuXe { get; set; }
        public DateTime? NgayTiepNhan {  get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Chuỗi chỉ được chứa các chữ số.")]
        public string? SoDienThoai { get; set; }
    }
}
