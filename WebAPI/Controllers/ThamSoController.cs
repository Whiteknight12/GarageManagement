using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Tham so")]
    [Route("api/[controller]")]
    [ApiController]
    public class ThamSoController : BaseController<ThamSo>
    {
        private readonly ApplicationDbContext _db; 
        public ThamSoController(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        [HttpGet("SoXeTiepNhanToiDaMotNgay")]
        public async Task<ActionResult<ThamSo>> GetSoXeTiepNhanToiDaMotNgay()
        {
            var result=await _db.thamSos.FirstOrDefaultAsync(u => u.TenThamSo == "SoXeTiepNhanToiDaMotNgay");
            if (result is not null) return Ok(result);
            return NotFound();
        }
        
    }
}
