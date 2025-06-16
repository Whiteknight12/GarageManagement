using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Data;
using WebAPI.IdentityModel;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _service;
        private readonly ApplicationDbContext _dbContext;
        public AccountController(JwtService service, ApplicationDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            var result = await _service.Authentication(request);
            if (result is null) return Unauthorized();
            return result;
        }

        [HttpGet("reset-password/{username}")]
        public async Task<ActionResult<string>> ResetPassword(string username)
        {
            var user = await _dbContext.taiKhoans.FirstAsync(us => us.TenDangNhap == username);
            user.MatKhau = GenerateRandomPassword(10);
            _dbContext.taiKhoans.Update(user);
            await _dbContext.SaveChangesAsync();
            return Ok(user.MatKhau); 
        }
        

        private string GenerateRandomPassword(int length)
        {
            const string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                 + "abcdefghijklmnopqrstuvwxyz"
                                 + "0123456789";
            var bytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var sb = new StringBuilder(length);
            foreach (var b in bytes)
            {
                // b mod charset.Length để chọn ký tự
                sb.Append(charset[b % charset.Length]);
            }
            return sb.ToString();
        }
    }

}
