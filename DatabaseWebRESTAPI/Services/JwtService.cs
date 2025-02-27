using DatabaseWebRESTAPI.Data;
using DatabaseWebRESTAPI.Domain.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatabaseWebRESTAPI.Services
{
    public class JwtService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IConfiguration _configuration;
        public JwtService(ApplicationDbContext dbcontext, IConfiguration configuration)
        {
            _dbcontext = dbcontext;
            _configuration = configuration;
        }
        public async Task<LoginResponseModel> Authenticate(LoginRequestModel request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password)) return null;
            var useraccount = await _dbcontext.UserAccounts.FirstOrDefaultAsync(x => x.Username == request.Username);
            if (useraccount == null || !PasswordHandler.VerifyPassword(request.Password, useraccount.Password!)) return null;
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenvaliditymin=_configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenexpirytimestamp = DateTime.UtcNow.AddMinutes(tokenvaliditymin);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.Username)
                }),
                Expires = tokenexpirytimestamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var securitytoken= tokenhandler.CreateToken(tokendescriptor);
            var accessstoken = tokenhandler.WriteToken(securitytoken);
            return new LoginResponseModel
            {
                AccessToken=accessstoken,
                Username= request.Username,
                Role = useraccount.Role,
                ExpireIn =(int)tokenexpirytimestamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
