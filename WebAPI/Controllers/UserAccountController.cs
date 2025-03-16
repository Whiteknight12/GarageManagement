using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : BaseController<UserAccount>
    {
        public UserAccountController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
