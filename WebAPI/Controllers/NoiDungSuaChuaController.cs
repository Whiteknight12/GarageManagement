using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Phieu sua chua")]
    [Route("api/[controller]")]
    [ApiController]
    public class NoiDungSuaChuaController : BaseController<NoiDungSuaChua>
    {
        public NoiDungSuaChuaController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
