using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThuTienController : BaseController<PhieuThuTien>
    {
        ApplicationDbContext _applicationdbcontext;
        public PhieuThuTienController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationdbcontext = applicationDbContext;
        }
        [HttpGet("GetListByMonthAndYear/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<PhieuThuTien>>> GetListByMonthAndYear(int month, int year)
        {
            var list = await _applicationdbcontext.phieuThuTiens.Where(u => u.NgayThuTien.Value.Month == month && u.NgayThuTien.Value.Year == year).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet("GetListByBienSo/{bienso}")]
        public async Task<ActionResult<IEnumerable<PhieuThuTien>>> GetListByBienSo(string bienso)
        {
            var list=await _applicationdbcontext.phieuThuTiens.Where(u => u.BienSoXe == bienso).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
