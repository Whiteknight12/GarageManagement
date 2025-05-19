using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Nhom nguoi dung")]
    [Route("api/[controller]")]
    [ApiController]
    public class NhomNguoiDungController : BaseController<NhomNguoiDung>
    {
        private readonly ApplicationDbContext _db;
        public NhomNguoiDungController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }
        [HttpGet("NhomNguoiDung/{tenNhom}")]
        public async Task<ActionResult<NhomNguoiDung>> GetNhomNguoiDungByTenNhom(string tenNhom)
        {
            var result=await _db.nhomNguoiDungs.FirstOrDefaultAsync(x => x.TenNhom == tenNhom);
            if (result is not null) return Ok(result);
            return NotFound();
        }
    }
}
