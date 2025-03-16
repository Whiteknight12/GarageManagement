using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.ThisIsNotABackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TienCongControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TienCongControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<TienCong> GetAll()
        {
            return _db.tienCongs;
        }
        [HttpGet("{id}")]
        public async Task<TienCong> GetByID(int id)
        {
            return await _db.tienCongs.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(TienCong entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.Id }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(TienCong entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _db.tienCongs.Where(u => u.Id == id).FirstOrDefaultAsync();
            _db.tienCongs.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
