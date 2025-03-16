using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoiDungPhieuSuaChuaController : BaseController<NoiDungPhieuSuaChua>
    {
        ApplicationDbContext _applicationDbContext;
        public NoiDungPhieuSuaChuaController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet("GetListByPhieuSuaChuaID/{ID}")]
        public async Task<ActionResult<IEnumerable<NoiDungPhieuSuaChua>>> GetListByPhieuSuaChuaID(int ID)
        {
            var list = await _applicationDbContext.noiDungPhieuSuaChuas.Where(u => u.PhieuSuaChuaID == ID).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
