using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Tai khoan")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : BaseController<TaiKhoan>
    {
        public TaiKhoanController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        
    }
}
