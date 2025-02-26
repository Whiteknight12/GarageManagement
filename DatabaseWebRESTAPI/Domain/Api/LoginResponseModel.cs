namespace DatabaseWebRESTAPI.Domain.Api
{
    public class LoginResponseModel
    {
        public string? AccessToken { get; set; }
        public string? Username { get; set; }
        public int ExpireIn { get; set; }
    }
}
