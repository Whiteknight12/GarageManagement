using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : BaseController<Car>
    {
        ApplicationDbContext _applicationDbContext; 
        public CarController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("GetByBienSo/{bienso}")]
        public async Task<ActionResult<Car?>> GetCarByBienSo(string bienso)
        {
            var car = await _applicationDbContext.Cars.Where(u => u.BienSo == bienso).FirstOrDefaultAsync();
            if (car is not null) return Ok(car);
            return NotFound();
        }

        [HttpGet("GetListByPhoneNumber/{phoneNumber}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetListByPhoneNumber(string phoneNumber)
        {
            var list = await _applicationDbContext.Cars.Where(u => u.DienThoai == phoneNumber).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
