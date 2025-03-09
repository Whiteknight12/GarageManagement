using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietTienCongController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ChiTietTienCongController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ChiTietTienCong>> Get()
        {
            return _db.chiTietTienCongs;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChiTietTienCong>> GetByID(int? id)
        {
            return await _db.chiTietTienCongs.Where(x => x.ID == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(ChiTietTienCong entity)
        {
            await _db.chiTietTienCongs.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.ID }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(ChiTietTienCong entity)
        {
            _db.chiTietTienCongs.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            var deleted = await _db.chiTietTienCongs.Where(x => x.ID == id).FirstOrDefaultAsync();
            if (deleted == null) return NotFound();
            _db.chiTietTienCongs.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
