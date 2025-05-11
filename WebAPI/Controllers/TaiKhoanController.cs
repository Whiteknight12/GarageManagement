using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : BaseController<TaiKhoan>
    {
        public TaiKhoanController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<TaiKhoan>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<TaiKhoan>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<TaiKhoan>> Create([FromBody] TaiKhoan entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] TaiKhoan entity)
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
