using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuController : BaseController<LichSu>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LichSuController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<LichSu>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<LichSu>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("LoaiThucThe/{loaiThucThe}")]
        public async Task<ActionResult<IEnumerable<LichSu>>> GetByLoaiThucThe(string loaiThucThe)
        {
            var result = await _applicationDbContext.lichSus
            .Where(ls => ls.LoaiThucTheLienQuan == loaiThucThe)
            .OrderByDescending(ls => ls.ThoiDiemThucHien)
            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("ThucTheId/{thucTheId}")]
        public async Task<ActionResult<IEnumerable<LichSu>>> GetByThucTheId(Guid thucTheId)
        {
            var result = await _applicationDbContext.lichSus
            .Where(ls => ls.ThucTheLienQuanId == thucTheId)
            .OrderByDescending(ls => ls.ThoiDiemThucHien)
            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("HanhDong/{hanhDong}")]
        public async Task<ActionResult<IEnumerable<LichSu>>> GetByHanhDong(string hanhDong)
        {
            var result = await _applicationDbContext.lichSus
            .Where(ls => ls.HanhDong == hanhDong)
            .OrderByDescending(ls => ls.ThoiDiemThucHien)
            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("ThoiGian")]
        public async Task<ActionResult<IEnumerable<LichSu>>> GetByTimeRange([FromQuery] DateTime from,
                                                                            [FromQuery] DateTime to)
        {
            var result = await _applicationDbContext.lichSus
                .Where(ls => ls.ThoiDiemThucHien >= from && ls.ThoiDiemThucHien <= to)
                .OrderByDescending(ls => ls.ThoiDiemThucHien)
                .ToListAsync();
            return Ok(result);
        }
    }
}
