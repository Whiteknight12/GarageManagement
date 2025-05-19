using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.InteropServices;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Phieu tiep nhan")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuTiepNhanController : BaseController<PhieuTiepNhan>
    {
        private readonly ApplicationDbContext _db;
        public PhieuTiepNhanController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }
        

        [HttpGet("XeId/{xeId}")]
        public async Task<ActionResult<IEnumerable<PhieuTiepNhan>>> GetListByXeId(Guid xeId)
        {
            var result = await _db.phieuTiepNhans.Where(x => x.XeId == xeId).ToListAsync();
            if (result is not null) return Ok(result);
            return NotFound();
        }

        [HttpGet("DayAndMonth/{day}/{month}")]
        public async Task<ActionResult<IEnumerable<PhieuTiepNhan>>> GetListPhieuTiepNhanByDayAndMonth(int day, int month)
        {
            var result=await _db.phieuTiepNhans.Where(u=>u.NgayTiepNhan.Value.Day==day && u.NgayTiepNhan.Value.Month==month).ToListAsync();
            if (result is not null) return Ok(result);
            return NotFound();
        }
    }
}
