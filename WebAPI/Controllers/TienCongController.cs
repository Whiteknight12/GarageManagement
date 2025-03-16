using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TienCongController : BaseController<TienCong>
    {
        public TienCongController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
