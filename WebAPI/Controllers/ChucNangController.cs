using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ChucNangController : BaseController<ChucNang>
    {
        private readonly ApplicationDbContext _db; 
        public ChucNangController(ApplicationDbContext applicationDbContext
            ) : base(applicationDbContext)
        {
            _db = applicationDbContext;        
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ChucNang>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ChucNang>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<ChucNang>> Create([FromBody] ChucNang entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] ChucNang entity)
        {
            return await base.Update(entity);
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }
        [HttpGet("/name/{name}")]
        public async Task<ActionResult> GetIdByName(string name)
        {
            var c = await _db.chucNangs.FirstOrDefaultAsync(cn => name.Equals(cn.TenChucNang.ToLower()));
            return Ok(c);
        }
    }
}
