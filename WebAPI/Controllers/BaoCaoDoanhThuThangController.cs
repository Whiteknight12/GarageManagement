using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy ="Bao cao doanh thu thang")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaoCaoDoanhThuThangController : BaseController<BaoCaoDoanhThuThang>
    {
        public BaoCaoDoanhThuThangController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
