using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        ApplicationDbContext _applicationDbContext; 
        public UserController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("GetByPhoneNumber/{phonenumber}")]
        public async Task<ActionResult<User>> GetByPhoneNumber(string phonenumber)
        {
            var user = await _applicationDbContext.users.Where(u => u.PhoneNumber == phonenumber).FirstOrDefaultAsync();
            if (user is not null) return Ok(user);
            return NotFound();
        }
        [HttpGet("GetListThroughRole/{role}")]
        public async Task<ActionResult<IEnumerable<User>>> GetListThroughRole(string role)
        {
            var list = await _applicationDbContext.users.Where(u => u.UserRole == role).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
    }
}
