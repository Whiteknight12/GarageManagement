using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Phan quyen")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyenController : BaseController<PhanQuyen>
    {
        public PhanQuyenController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
