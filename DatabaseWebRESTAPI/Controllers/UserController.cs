using DatabaseWebRESTAPI.Data;
using DatabaseWebRESTAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebRESTAPI.Controllers
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
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return _db.Users;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetByID(int id)
        {
            return await _db.Users.Where(u => u.UserID == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = user.UserID }, user);
        }
        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteuser= await GetByID(id);
            if (deleteuser is null) return NotFound();
            _db.Remove(deleteuser.Value);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
