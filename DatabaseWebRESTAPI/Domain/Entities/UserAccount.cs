using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWebRESTAPI.Domain.Entities
{
    [Table("USER_ACCOUNT")]
    public class UserAccount
    {
        [Key]
        [Column("UserAccountID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserAccountID { get; set; }
        [Column("Username")]
        public string? Username { get; set; }
        [Column("Password")]
        public string? Password { get; set; }
        [Column("Role")]
        public string? Role { get; set; }
    }
}
