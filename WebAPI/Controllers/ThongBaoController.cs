using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoController : BaseController<ThongBao>
    {
        private readonly ApplicationDbContext _db;
        
        public ThongBaoController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ThongBao>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ThongBao>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("NhomNguoiDungId/{id}")]
        public async Task<ActionResult<IEnumerable<ThongBao>>> GetByNhomNguoiDungId(Guid id)
        {
            var list = await _db.thongBaos
            .Where(u => (u.NhomNguoiDungId != null && u.NhomNguoiDungId == id) || (u.isForAll != null && u.isForAll == true))
            .ToListAsync();
            if (list is not null && list.Any()) return Ok(list);
            return NotFound();
        }

        [HttpPost]
        public override async Task<ActionResult<ThongBao>> Create([FromBody] ThongBao entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] ThongBao entity)
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
