using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyenController : BaseController<PhanQuyen>
    {
        
        public PhanQuyenController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
           
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PhanQuyen>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<PhanQuyen>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<PhanQuyen>> Create([FromBody] PhanQuyen entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] PhanQuyen entity)
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
