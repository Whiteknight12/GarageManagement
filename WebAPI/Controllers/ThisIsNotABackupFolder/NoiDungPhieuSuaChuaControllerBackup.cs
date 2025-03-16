using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.ThisIsNotABackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoiDungPhieuSuaChuaControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public NoiDungPhieuSuaChuaControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<NoiDungPhieuSuaChua> GetAll()
        {
            return _db.noiDungPhieuSuaChuas;
        }
        [HttpGet("{id}")]
        public async Task<NoiDungPhieuSuaChua> GetByID(int id)
        {
            return await _db.noiDungPhieuSuaChuas.Where(u => u.NoiDungID== id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(NoiDungPhieuSuaChua entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.NoiDungID }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(NoiDungPhieuSuaChua entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _db.noiDungPhieuSuaChuas.Where(u => u.NoiDungID == id).FirstOrDefaultAsync();
            _db.noiDungPhieuSuaChuas.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetListThroughPhieuSuaChuaID/{ID}")]
        public async Task<ActionResult<IEnumerable<NoiDungPhieuSuaChua>>> GetListThroughPhieuSuaChuaID(int ID)
        {
            var list = await _db.noiDungPhieuSuaChuas.Where(u => u.PhieuSuaChuaID == ID).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
