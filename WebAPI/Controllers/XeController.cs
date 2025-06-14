using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class XeController : BaseController<Xe>
    {
        ApplicationDbContext _applicationDbContext; 
        public XeController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("BienSo/{bienso}")]
        public async Task<ActionResult<Xe?>> GetXeByBienSo(string bienso)
        {
            var xe = await _applicationDbContext.xes.Where(u => u.BienSo == bienso).FirstOrDefaultAsync();
            if (xe is not null) return Ok(xe);
            return NotFound();
        }

        [HttpGet("PhoneNumber/{sdt}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByPhoneNumber(string sdt)
        {
            var kh = _applicationDbContext.khachHangs.FirstOrDefault(kh => kh.SoDienThoai == sdt);
            var list = await _applicationDbContext.xes.Where(u => u.KhachHangId == kh.Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }

        [HttpGet("Name/{hovaten}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByName(string hovaten)
        {
            var listkh = _applicationDbContext.khachHangs.Where(u => u.HoVaTen == hovaten);
            if (!listkh.IsNullOrEmpty())
            {
                var result = new List<Xe>();
                foreach (var kh in listkh)
                {
                    var list=await _applicationDbContext.xes.Where(u=>u.KhachHangId==kh.Id).ToListAsync();
                    if (list is not null) result.AddRange(list);
                }
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("customer-id/{customerId}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByCustomerId(Guid customerId)
        {
            var xes = await _applicationDbContext.xes.Where(u => u.KhachHangId == customerId).ToListAsync();
            if (xes is not null) return Ok(xes);
            return NotFound();
        }

        [HttpGet("Email/{email}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByEmail(string email)
        {
            var kh = await _applicationDbContext.khachHangs.FirstOrDefaultAsync(u => u.Email == email);
            if (kh is not null)
            {
                var list = await _applicationDbContext.xes.Where(u => u.KhachHangId == kh.Id).ToListAsync();
                if (list is not null) return Ok(list);
            }
            return NotFound();
        }

        [HttpGet("GetListByHieuXe/{hieuxe}")]
        public async Task<ActionResult<IEnumerable<Xe>>> GetListByHieuXe(string hieuxe)
        {
            var hx = _applicationDbContext.hieuXes.First(hx => string.Compare(hieuxe.ToLower(), hx.TenHieuXe.ToLower()) == 0);
            var list = await _applicationDbContext.xes.Where(u => u.HieuXeId == hx.Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Xe>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Xe>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<Xe>> Create([FromBody] Xe entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] Xe entity)
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
