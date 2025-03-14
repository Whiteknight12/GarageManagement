using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _db.users;
        }
        [HttpGet("{id}")]
        public async Task<User> GetByID(int id)
        {
            return await _db.users.Where(u => u.UserID == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(User entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = entity.UserID }, entity);
        }
        [HttpPut]
        public async Task<ActionResult> Update(User entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _db.users.Where(u => u.UserID == id).FirstOrDefaultAsync();
            _db.users.Remove(deleted);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("GetThroughPhoneNumber/{phonenumber}")]
        public async Task<ActionResult<User>> GetThroughPhoneNumber(string phonenumber)
        {
            var user = await _db.users.Where(u => u.PhoneNumber == phonenumber).FirstOrDefaultAsync();
            if (user is not null) return Ok(user);
            return NotFound();
        }
        [HttpGet("GetListThroughRole/{role}")]
        public async Task<ActionResult<IEnumerable<User>>> GetListThroughRole(string role)
        {
            var list=await _db.users.Where(u=>u.UserRole == role).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
