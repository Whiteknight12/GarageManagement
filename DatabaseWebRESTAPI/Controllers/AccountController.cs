using DatabaseWebRESTAPI.Domain.Api;
using DatabaseWebRESTAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseWebRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public AccountController(JwtService jwtservice)
        {
            _jwtService = jwtservice;
        }
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel request)
        {
            var result=await _jwtService.Authenticate(request);
            if (result == null) return Unauthorized();
            return result;
        }
    }
}
