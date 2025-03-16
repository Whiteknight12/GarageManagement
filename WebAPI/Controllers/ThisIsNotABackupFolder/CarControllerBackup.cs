using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.BackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CarControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            return _db.Cars;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarByID(int? id)
        {
            return await _db.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> CreateCar(Car car)
        {
            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarByID), new { id = car.Id }, car);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCar(Car car)
        {
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int? id)
        {
            var car = await _db.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (car == null) return NotFound(); 
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetByBienSo/{bienso}")]
        public async Task<ActionResult<Car?>> GetCarByBienSo(string bienso)
        {
            var car = await _db.Cars.Where(u=>u.BienSo== bienso).FirstOrDefaultAsync();
            if (car is not null) return Ok(car);
            return NotFound();
        }
        [HttpGet("GetListThroughPhoneNumber/{phoneNumber}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetListThroughPhoneNumber(string phoneNumber)
        {
            var list=await _db.Cars.Where(u=>u.DienThoai==phoneNumber).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
