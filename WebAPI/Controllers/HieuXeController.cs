using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy ="Hieu xe")]
    [Route("api/[controller]")]
    [ApiController]
    public class HieuXeController : BaseController<HieuXe>
    {
        public HieuXeController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
