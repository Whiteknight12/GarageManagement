﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
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
            var taiKhoan=await _db.taiKhoans.Where(x => x.TenDangNhap == request.Username && x.MatKhau == request.Password).FirstOrDefaultAsync();
            var role = _db.nhomNguoiDungs.FirstOrDefault(u => u.Id == taiKhoan.NhomNguoiDungId); 
            if (taiKhoan == null) return null;

            var userId = taiKhoan.Id;
            var chucNangIds = await _db.phanQuyens.Where(r => r.NhomNguoiDungId == role.Id)
                .Select(r => r.ChucNangId)
                .ToListAsync(); ;
            var chucNangs = await _db.chucNangs.Where(cn => chucNangIds.Contains(cn.Id))
                .Select(cn => cn.TenChucNang).ToListAsync(); 

            var issuer= _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenvaliditymins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenexpirytimestamp = DateTime.UtcNow.AddMinutes(tokenvaliditymins);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, request.Username),
                new Claim(JwtRegisteredClaimNames.NameId, taiKhoan.Id.ToString()),
                //new Claim(ClaimTypes.Role, role.TenNhom)
            };
            claims.AddRange(chucNangs.Select(p => new Claim(ClaimTypes.Role, p)));
            
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenexpirytimestamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var securitytoken = tokenhandler.CreateToken(tokendescriptor);
            var accesstoken = tokenhandler.WriteToken(securitytoken);
            return new LoginResponseModel
            {
                AccountId = taiKhoan.Id,
                Token = accesstoken,
                Username = request.Username,
                Expiry = tokenexpirytimestamp,
                Role = role.TenNhom,
                Permissions = chucNangs
            };
            
        }
    }
}
