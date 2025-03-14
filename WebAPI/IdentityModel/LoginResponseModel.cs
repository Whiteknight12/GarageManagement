namespace WebAPI.IdentityModel
{
    public class LoginResponseModel
    {
        public string? Username { get; set; }
        public string? Token { get; set; }
        public int? Expiration { get; set; }
        public string? Role { get; set; }
    }
}
