using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuTiepNhanController : BaseController<PhieuTiepNhan>
    {
        public PhieuTiepNhanController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PhieuTiepNhan>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<PhieuTiepNhan>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<PhieuTiepNhan>> Create([FromBody] PhieuTiepNhan entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] PhieuTiepNhan entity)
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
