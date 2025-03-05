using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebAPI.IdentityModel;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _service;
        public AccountController(JwtService service)
        {
            _service = service;
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
