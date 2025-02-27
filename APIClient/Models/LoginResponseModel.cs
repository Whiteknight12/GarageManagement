using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Models
{
    public class LoginResponseModel
    {
        public string? AccessToken { get; set; }
        public string? Username { get; set; }
        public int ExpireIn { get; set; }
        public string? Role { get; set; }
    }
}
