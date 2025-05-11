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

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ChiTietPhieuSuaChua>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ChiTietPhieuSuaChua>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<ChiTietPhieuSuaChua>> Create([FromBody] ChiTietPhieuSuaChua entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] ChiTietPhieuSuaChua entity)
        {
            return await base.Update(entity);
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
