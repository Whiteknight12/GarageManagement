using DatabaseWebRESTAPI.Data;
using DatabaseWebRESTAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CarController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetAll()
        {
            return _db.Cars;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetByID(int id)
        {
            return await _db.Cars.Where(u=>u.CarID==id).FirstOrDefaultAsync(); 
        }
        [HttpPost]
        public async Task<ActionResult> Create(Car car)
        {
            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = car.CarID }, car);
        }
        [HttpPut]
        public async Task<ActionResult> Update(Car car)
        {
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletecar=await GetByID(id);
            if (deletecar is null) return NotFound();
            _db.Remove(deletecar.Value);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
