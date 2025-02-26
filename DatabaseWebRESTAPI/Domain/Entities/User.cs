using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWebRESTAPI.Domain.Entities
{
    [Table("USER")]
    public class User
    {
        [Key]
        [Column("UserID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Column("UserName")]
        [MaxLength(100)]
        public string? UserName { get; set; }
        [Column("Password")]
        public string? Password { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("FullName")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Tên chỉ được chứa chữ cái và khoảng trắng.")]
        public string? FullName { get; set; }
        [Column("PhoneNumber")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Chỉ được nhập số.")]
        public string? PhoneNumber { get; set; }
        [Column("Address")]
        public string? Address { get; set; }
        [Column("Role")]
        public string? Role { get; set; }
        [Column("Age")]
        public int? Age { get; set; }
        [Column("Gender")]
        public string? Gender { get; set; }  
    }
}
