using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.ThisIsNotABackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuSuaChuaControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PhieuSuaChuaControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<PhieuSuaChua> GetAll()
        {
            return _db.phieuSuaChuas;
        }
        [HttpGet("{id}")]
        public async Task<PhieuSuaChua> GetByID(int id)
        {
            return await _db.phieuSuaChuas.Where(u => u.PhieuSuaChuaID == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(PhieuSuaChua entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.PhieuSuaChuaID }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(PhieuSuaChua entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _db.phieuSuaChuas.Where(u => u.PhieuSuaChuaID == id).FirstOrDefaultAsync();
            _db.phieuSuaChuas.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetThroughBienSoXe/{bienso}")]
        public async Task<ActionResult<PhieuSuaChua>> GetThroughBienSoXe(string bienso)
        {
            var obj = await _db.phieuSuaChuas.Where(u => u.BienSoXe == bienso).FirstOrDefaultAsync();
            return Ok(new { data = obj });
        }
    }
}
