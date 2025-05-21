using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThuTienController : BaseController<PhieuThuTien>
    {
        ApplicationDbContext _applicationdbcontext;
        public PhieuThuTienController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationdbcontext = applicationDbContext;
        }
        [HttpGet("GetListByMonthAndYear/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<PhieuThuTien>>> GetListByMonthAndYear(int month, int year)
        {
            var list = await _applicationdbcontext.phieuThuTiens.Where(u => u.NgayThuTien.Date.Month == month && u.NgayThuTien.Date.Year == year).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet("GetListByBienSo/{bienso}")]
        public async Task<ActionResult<IEnumerable<PhieuThuTien>>> GetListByBienSo(string bienso)
        {
            var xe = _applicationdbcontext.xes.FirstOrDefault(x => x.BienSo == bienso);
            var list = await _applicationdbcontext.phieuThuTiens.Where(u => u.XeId == xe.Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PhieuThuTien>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<PhieuThuTien>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<PhieuThuTien>> Create([FromBody] PhieuThuTien entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] PhieuThuTien entity)
        {
            return await base.Update(entity);
        }

        [HttpDelete("{id}")]
        public override async Task<ActionResult> Delete(Guid id)
        {
            return await base.Delete(id);
        }
    }
}
