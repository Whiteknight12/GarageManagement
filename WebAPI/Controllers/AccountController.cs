using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebAPI.Data;
using WebAPI.IdentityModel;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _service;
        private readonly ApplicationDbContext _dbContext;
        public AccountController(JwtService service, ApplicationDbContext dbContext)
        {
            _service = service;
            _dbContext = dbContext;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            var result = await _service.Authentication(request);
            if (result is null) return Unauthorized();
            return result;
        }
    }
}
