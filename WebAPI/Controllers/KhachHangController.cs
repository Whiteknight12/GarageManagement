using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : BaseController<KhachHang>
    {
        ApplicationDbContext _applicationDbContext; 
        public KhachHangController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("PhoneNumber/{phonenumber}")]
        public async Task<ActionResult<KhachHang>> GetByPhoneNumber(string phonenumber)
        {
            var user = await _applicationDbContext.khachHangs.Where(u => u.SoDienThoai == phonenumber).FirstOrDefaultAsync();
            if (user is not null) return Ok(user);
            return NotFound();
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<KhachHang>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<KhachHang>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<KhachHang>> Create([FromBody] KhachHang entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] KhachHang entity)
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
