using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI.Data;

namespace WebAPI.Middlewares
{
    public class ClaimsLoaderMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsLoaderMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context, ApplicationDbContext dbContext)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdString = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userIdString, out var userIdGuid))
                {
                    var user = await dbContext.taiKhoans.FirstOrDefaultAsync(u => u.Id == userIdGuid);

                    var role = await dbContext.nhomNguoiDungs.FirstOrDefaultAsync(r => r.Id == user.NhomNguoiDungId);

                    var chucNangIds = await dbContext.phanQuyens
                        .Where(r => r.NhomNguoiDungId == role.Id)
                        .Select(r => r.ChucNangId)
                        .ToListAsync();

                    var chucNangs = await dbContext.chucNangs
                        .Where(cn => chucNangIds.Contains(cn.Id))
                        .Select(cn => cn.TenChucNang)
                        .ToListAsync();

                    var claims = chucNangs.Select(p => new Claim("Permission", p));
                    var identity = context.User.Identity as ClaimsIdentity;
                    identity.AddClaims(claims);
                }

            }
            //else tra ve bad request hoac error thieu id 
            await _next(context);
        }
    }
    public static class ClaimsLoaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseClaimsLoader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsLoaderMiddleware>();
        }
    }
}
