using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThuTienController : BaseController<PhieuThuTien>
    {
        public PhieuThuTienController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
