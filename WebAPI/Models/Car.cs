using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("CAR")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NAME")]
        public string? Name { get; set; }
        [Column("MODEL")]
        public string? Model { get; set; }
        [Column("BIENSO")]
        public string? BienSo { get; set; }
        [Column("TENCHUXE")]
        public string? TenChuXe { get; set; }
        [Column("DIACHI")]
        public string? DiaChi { get; set; }
        [Column("DIENTHOAI")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chuỗi chỉ được chứa các chữ số.")]
        public string? DienThoai { get; set; }
        public bool? IsAvailable { get; set; }
        public double? TienNoCuaChuXe { get; set; }
    }
}
