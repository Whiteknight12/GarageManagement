using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungThongBaoController : BaseController<NguoiDungThongBao>
    {
        private readonly ApplicationDbContext _db;
        
        public NguoiDungThongBaoController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<NguoiDungThongBao>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<NguoiDungThongBao>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("nguoiDungIdAndThongBaoId/{nguoiDungId}/{thongBaoId}")]
        public async Task<ActionResult<NguoiDungThongBao>> getByNguoiDungIdAndThongBaoId(Guid nguoiDungId, Guid thongBaoId)
        {
            var result = _db.nguoiDungThongBaos.FirstOrDefault(u => u.nguoiDungId == nguoiDungId && u.thongBaoId == thongBaoId);
            if (result is not null) return Ok(result);
            return NotFound();
        }

        [HttpPost]
        public override async Task<ActionResult<NguoiDungThongBao>> Create([FromBody] NguoiDungThongBao entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] NguoiDungThongBao entity)
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
