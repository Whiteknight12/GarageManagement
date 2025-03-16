using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.ThisIsNotABackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThuTienControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PhieuThuTienControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<PhieuThuTien> GetAll()
        {
            return _db.phieuThuTiens;
        }
        [HttpGet("{id}")]
        public async Task<PhieuThuTien> GetByID(int id)
        {
            return await _db.phieuThuTiens.Where(u => u.PhieuThuTienId == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(PhieuThuTien entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.PhieuThuTienId }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(PhieuThuTien entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _db.phieuThuTiens.Where(u => u.PhieuThuTienId == id).FirstOrDefaultAsync();
            _db.phieuThuTiens.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
