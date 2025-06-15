using Azure.Core;
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
    public class ChiTietBaoCaoTonController : BaseController<ChiTietBaoCaoTon>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChiTietBaoCaoTonController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ChiTietBaoCaoTon>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<ChiTietBaoCaoTon>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpGet("month-and-year/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<ChiTietBaoCaoTon>>> GetByMonthAndYear(int month, int year)
        {
            var baoCaoTon=await _applicationDbContext.baoCaoTons.FirstOrDefaultAsync(b => b.Thang == month && b.Nam == year);
            if (baoCaoTon is null) return NotFound();
            var results = await _applicationDbContext.chiTietBaoCaoTons
                .Where(c => c.BaoCaoTonId==baoCaoTon.Id)
                .ToListAsync();
            if (!results.Any()) return NotFound();
            return Ok(results);
        }

        [HttpPost]
        public override async Task<ActionResult<ChiTietBaoCaoTon>> Create([FromBody] ChiTietBaoCaoTon entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] ChiTietBaoCaoTon entity)
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
