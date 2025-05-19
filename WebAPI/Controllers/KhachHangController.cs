using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Khach hang")]
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : BaseController<KhachHang>
    {
        ApplicationDbContext _applicationDbContext; 
        public KhachHangController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("PhoneNumber/{phonenumber}")]
        public async Task<ActionResult<KhachHang>> GetByPhoneNumber(string phonenumber)
        {
            var user = await _applicationDbContext.khachHangs.Where(u => u.SoDienThoai == phonenumber).FirstOrDefaultAsync();
            if (user is not null) return Ok(user);
            return NotFound();
        }
    }
}
