using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class User
    {
        public int UserID { get; set; }
        public string? Fullname { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? HasAccount { get; set; }
        public int? UserAccountID { get; set; }
        public double? TienNo { get; set; }
        public string? UserRole { get; set; }
        public string? Email { get; set; }
    }
}
