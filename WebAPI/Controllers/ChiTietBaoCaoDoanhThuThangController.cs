using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Bao cao doanh thu thang")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietBaoCaoDoanhThuThangController : BaseController<ChiTietBaoCaoDoanhThuThang>
    {
        public ChiTietBaoCaoDoanhThuThangController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
