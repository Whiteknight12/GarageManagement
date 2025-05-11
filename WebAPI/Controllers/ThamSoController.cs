using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //them role de phan quyen tham so 
    //[Authorize(Roles = "")]
    [Route("api/[controller]")]
    [ApiController]
    public class ThamSoController : BaseController<ThamSo>
    {
        private readonly ApplicationDbContext _db; 
        public ThamSoController(ApplicationDbContext db) : base(db) {
            _db = db;
        }

        [HttpGet("GetSoXeTiepNhanToiDaMotNgay")]
        public async Task<ActionResult<ThamSo>> GetSoXeTiepNhanToiDaMotNgay()
        {
            var result=await _db.thamSos.FirstOrDefaultAsync(u => u.TenThamSo == "SoXeTiepNhanToiDaMotNgay");
            if (result is not null) return Ok(result);
            return NotFound();
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ThamSo>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ThamSo>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<ThamSo>> Create([FromBody] ThamSo entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] ThamSo entity)
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
