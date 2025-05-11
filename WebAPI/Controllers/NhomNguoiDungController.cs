using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomNguoiDungController : BaseController<NhomNguoiDung>
    {
        private readonly ApplicationDbContext _db;
        public NhomNguoiDungController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }
        [HttpGet("NhomNguoiDung/{tenNhom}")]
        public async Task<ActionResult<NhomNguoiDung>> GetNhomNguoiDungByTenNhom(string tenNhom)
        {
            var result=await _db.nhomNguoiDungs.FirstOrDefaultAsync(x => x.TenNhom == tenNhom);
            if (result is not null) return Ok(result);
            return NotFound();
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<NhomNguoiDung>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<NhomNguoiDung>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<NhomNguoiDung>> Create([FromBody] NhomNguoiDung entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] NhomNguoiDung entity)
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
