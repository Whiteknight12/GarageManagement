﻿namespace WebAPI.IdentityModel
{
    public class LoginResponseModel
    {
        public Guid? AccountId { get; set; }
        public string? Username { get; set; }
        public string? Token { get; set; }
        public DateTime Expiry { get; set; }
        public string? Role { get; set; }
        public List<string>? Permissions { get; set; }
    }
}
