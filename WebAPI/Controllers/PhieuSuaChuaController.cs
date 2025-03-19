using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuSuaChuaController : BaseController<PhieuSuaChua>
    {
        ApplicationDbContext _applicationDbContext; 
        public PhieuSuaChuaController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("GetByBienSoXe/{bienso}")]
        public async Task<ActionResult<PhieuSuaChua>> GetByBienSoXe(string bienso)
        {
            var obj = await _applicationDbContext.phieuSuaChuas.Where(u => u.BienSoXe == bienso).FirstOrDefaultAsync(); 
            return Ok(new { data = obj });
        }
        [HttpGet("GetListByBienSoXe/{bienso}")]
        public async Task<ActionResult<IEnumerable<PhieuSuaChua>>> GetListByBienSoXe(string bienso)
        {
            var list=await _applicationDbContext.phieuSuaChuas.Where(u=>u.BienSoXe==bienso).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
