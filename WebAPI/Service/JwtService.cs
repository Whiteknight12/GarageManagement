using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data;
using WebAPI.IdentityModel;

namespace WebAPI.Service
{
    public class JwtService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public JwtService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<LoginResponseModel> Authentication(LoginRequestModel request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password)) return null;
            var useraccount=await _db.UserAccounts.Where(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefaultAsync();
            if (useraccount == null) return null;
            var issuer= _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenvaliditymins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenexpirytimestamp = DateTime.UtcNow.AddMinutes(tokenvaliditymins);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.Username),
                    new Claim(JwtRegisteredClaimNames.NameId, useraccount.Id.ToString()),
                    new Claim(ClaimTypes.Role, useraccount.Role) // Thêm Role vào Claims
                }),
                Expires = tokenexpirytimestamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenhandler=new JwtSecurityTokenHandler();
            var securitytoken= tokenhandler.CreateToken(tokendescriptor);
            var accesstoken = tokenhandler.WriteToken(securitytoken);
            return new LoginResponseModel
            {
                Token = accesstoken,
                Username = request.Username,
                Expiration = (int)tokenexpirytimestamp.Subtract(DateTime.UtcNow).TotalSeconds,
                Role= useraccount.Role
            };
        }
    }
}
