using DatabaseWebRESTAPI.Data;
using DatabaseWebRESTAPI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UserAccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserAccount>> GetAll()
        {
            return _db.UserAccounts;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetByID(int id)
        {
            return await _db.UserAccounts.Where(u => u.UserAccountID == id).FirstOrDefaultAsync();
        }
        [HttpPut]
        public async Task<ActionResult> Update(UserAccount useraccount)
        {
            _db.UserAccounts.Update(useraccount);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserAccount useraccount)
        {
            await _db.UserAccounts.AddAsync(useraccount);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByID), new { id = useraccount.UserAccountID }, useraccount);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteuseraccount = await GetByID(id);
            if (deleteuseraccount is null) return NotFound();
            _db.Remove(deleteuseraccount.Value);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
