using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy ="Xe")]
    [Route("api/[controller]")]
    [ApiController]
    public class XeController : BaseController<Xe>
    {
        ApplicationDbContext _applicationDbContext; 
        public XeController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("BienSo/{bienso}")]
        public async Task<ActionResult<Xe?>> GetXeByBienSo(string bienso)
        {
            var xe = await _applicationDbContext.xes.Where(u => u.BienSo == bienso).FirstOrDefaultAsync();
            if (xe is not null) return Ok(xe);
            return NotFound();
        }

        [HttpGet("GetListByPhoneNumber/{phoneNumber}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByPhoneNumber(string sdt)
        {
            var kh = _applicationDbContext.khachHangs.FirstOrDefault(kh => kh.SoDienThoai == sdt);
            var list = await _applicationDbContext.xes.Where(u => u.KhachHangId == kh.Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet("GetListByHieuXe/{hieuxe}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByHieuXe(string hieuxe)
        {
            var hx = _applicationDbContext.hieuXes.First(hx => string.Compare(hieuxe.ToLower(), hx.TenHieuXe.ToLower()) == 0);
            var list = await _applicationDbContext.xes.Where(u => u.HieuXeId == hx.Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        
        
    }
}
