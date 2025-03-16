using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.BackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRecordControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CarRecordControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<CarRecord> GetAllCarRecords()
        {
            return _db.CarRecords;
        }
        [HttpGet("{id}")]
        public async Task<CarRecord> GetCarRecordByID(int id)
        {
            return await _db.CarRecords.Where(u => u.CarRecordID == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> CreateNewCarRecord(CarRecord entity)
        {
            await _db.CarRecords.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarRecordByID), new {id=entity.CarRecordID}, entity);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCarRecord(CarRecord entity)
        {
            _db.CarRecords.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCarRecord(int id)
        {
            var deletecarrecord=await _db.CarRecords.Where(u=>u.CarRecordID==id).FirstOrDefaultAsync();
            if (deletecarrecord is null) return NotFound();
            _db.CarRecords.Remove(deletecarrecord);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
