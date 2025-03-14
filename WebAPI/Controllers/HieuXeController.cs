using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HieuXeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public HieuXeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<HieuXe> GetAll()
        {
            return _db.hieuXes;
        }
        [HttpGet("{id}")]
        public async Task<HieuXe> GetByID(int id)
        {
            return await _db.hieuXes.Where(u=>u.Id==id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(HieuXe entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.Id }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(HieuXe entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted=await _db.hieuXes.Where(u=>u.Id==id).FirstOrDefaultAsync();
            _db.hieuXes.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
