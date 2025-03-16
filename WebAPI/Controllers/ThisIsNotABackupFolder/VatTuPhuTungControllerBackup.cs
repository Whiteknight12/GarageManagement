using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.ThisIsNotABackupFolder
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatTuPhuTungControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VatTuPhuTungControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<VatTuPhuTung> GetAll()
        {
            return _db.vatTuPhuTungs;
        }
        [HttpGet("{id}")]
        public async Task<VatTuPhuTung> GetByID(int id)
        {
            return await _db.vatTuPhuTungs.Where(u => u.VTPTID == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(VatTuPhuTung v)
        {
            _db.vatTuPhuTungs.Add(v);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = v.VTPTID }, v);
        }
        [HttpPut]
        public async Task<ActionResult> Update(VatTuPhuTung v)
        {
            _db.vatTuPhuTungs.Update(v);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted=_db.vatTuPhuTungs.Where(u=>u.VTPTID==id).FirstOrDefaultAsync();
            _db.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
    } 
}
