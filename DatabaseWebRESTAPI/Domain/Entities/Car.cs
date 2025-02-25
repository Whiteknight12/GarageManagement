using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWebRESTAPI.Domain.Entities
{
    [Table("CAR")]
    public class Car
    {
        [Column("CarID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }
        [Column("OwnerAddress")]
        [MaxLength(100)]
        public string? OwnerAddress { get; set; }
        [Column("OwnerName")]
        [MaxLength(100)]
        public string? OwnerName { get; set; }
        [Column("OwnerPhone")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số.")]
        public string? OwnerPhone { get; set; }
        [Column("SentDate")]
        public DateTime? SentDate { get; set; }
        [Column("CarBrand")]
        [MaxLength(100)]
        public string? CarBrand { get; set; }
        [Column("CarNumber")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số.")]
        public long? CarNumber { get; set; }
    }
}
