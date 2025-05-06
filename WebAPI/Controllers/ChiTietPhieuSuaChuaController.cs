using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietPhieuSuaChuaController : BaseController<ChiTietPhieuSuaChua>
    {
        ApplicationDbContext _applicationDbContext;
        public ChiTietPhieuSuaChuaController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("GetListByPhieuSuaChuaID/{ID}")]
        public async Task<ActionResult<IEnumerable<ChiTietPhieuSuaChua>>> GetListByPhieuSuaChuaID(Guid Id)
        {
            var list = await _applicationDbContext.chiTietPhieuSuaChuas.Where(u => u.PhieuSuaChuaId == Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
