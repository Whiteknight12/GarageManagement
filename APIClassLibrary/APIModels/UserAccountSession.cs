using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary.APIModels
{
    public class UserAccountSession
    {
        public string? Username { get; set; }
        public string? Token { get; set; }
        public DateTime Expiry { get; set; }
        public string? Role { get; set; }
    }
}
