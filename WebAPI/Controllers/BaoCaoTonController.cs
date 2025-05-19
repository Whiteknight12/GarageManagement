using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Bao cao ton")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoTonController : BaseController<BaoCaoTon>
    {
        public BaoCaoTonController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
