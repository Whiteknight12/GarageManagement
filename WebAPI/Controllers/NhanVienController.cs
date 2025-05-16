using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : BaseController<NhanVien>
    {
        private readonly ApplicationDbContext _db;

        public NhanVienController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<NhanVien>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<NhanVien>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<NhanVien>> Create([FromBody] NhanVien entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] NhanVien entity)
        {
            return await base.Update(entity);
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }

        [HttpGet("TaiKhoanId/{TaiKhoanId}")]
        public async Task<ActionResult<NhanVien>> GetByTaiKhoanId(Guid TaiKhoanId)
        {
            var result = await _db.nhanViens.FirstOrDefaultAsync(x => x.TaiKhoanId == TaiKhoanId);
            if (result is not null) return Ok(result);
            return NotFound();
        }
    }
}
