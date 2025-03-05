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
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số.")]
        public string? BienSo { get; set; }
    }
}
