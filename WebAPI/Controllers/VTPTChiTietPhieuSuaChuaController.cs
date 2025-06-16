using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class VTPTChiTietPhieuSuaChuaController : BaseController<VTPTChiTietPhieuSuaChua>
    {
        private readonly ApplicationDbContext _db;
        
        public VTPTChiTietPhieuSuaChuaController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db=applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<VTPTChiTietPhieuSuaChua>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<VTPTChiTietPhieuSuaChua>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("ChiTietPhieuSuaChuaId/{id}")]
        public async Task<ActionResult<IEnumerable<VTPTChiTietPhieuSuaChua>>> getListByChiTietPhieuSuaChuaId(Guid id)
        {
            var result=_db.vtptChiTietPhieuSuaChuas.Where(u=>u.ChiTietPhieuSuaChuaId == id).ToList();
            if (result.Count == 0) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public override async Task<ActionResult<VTPTChiTietPhieuSuaChua>> Create([FromBody] VTPTChiTietPhieuSuaChua entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] VTPTChiTietPhieuSuaChua entity)
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
