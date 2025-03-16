using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers.ThisIsNotABackupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAccountControllerBackup : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public UserAccountControllerBackup(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<UserAccount>> Get()
        {
            return _db.UserAccounts;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetUserAccountByID(int? id)
        {
            return await _db.UserAccounts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> CreateUserAccount(UserAccount useraccount)
        {
            await _db.UserAccounts.AddAsync(useraccount);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserAccountByID), new { id = useraccount.Id }, useraccount);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUserAccount(UserAccount useraccount)
        {
            _db.UserAccounts.Update(useraccount);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAccount(int? id)
        {
            var useraccount = await _db.UserAccounts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (useraccount == null) return NotFound();
            _db.UserAccounts.Remove(useraccount);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
