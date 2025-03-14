using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string? Fullname { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Chuỗi chỉ được chứa các chữ số.")]
        public string? PhoneNumber { get; set; }
        public bool? HasAccount { get; set; }
        public int? UserAccountID { get; set; }
        public double? TienNo { get; set; }
        public string? UserRole { get; set; }
        public string? Email { get; set; }
    }
}
