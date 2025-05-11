using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapVatTuController : BaseController<PhieuNhapVatTu>
    {
        public PhieuNhapVatTuController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PhieuNhapVatTu>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<PhieuNhapVatTu>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<PhieuNhapVatTu>> Create([FromBody] PhieuNhapVatTu entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] PhieuNhapVatTu entity)
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
