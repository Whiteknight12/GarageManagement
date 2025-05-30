﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuSuaChuaController : BaseController<PhieuSuaChua>
    {
        ApplicationDbContext _applicationDbContext; 
        public PhieuSuaChuaController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("GetListByBienSoXe/{bienso}")]
        public async Task<ActionResult<IEnumerable<PhieuSuaChua>>> GetListByBienSoXe(string bienso)
        {
            var xe = _applicationDbContext.xes.FirstOrDefault(x => x.BienSo == bienso);
            var list = await _applicationDbContext.phieuSuaChuas.Where(u => u.XeId == xe.Id).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet("GetListByMonthAndYear/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<PhieuSuaChua>>> GetListByMonthAndYear(int month, int year)
        {
            var list=await _applicationDbContext.phieuSuaChuas.Where(u => u.NgaySuaChua.Date.Month == month && u.NgaySuaChua.Date.Year == year).ToListAsync();
            if (list is not null) return Ok(list);
            return NotFound();
        }
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<PhieuSuaChua>>> GetAll()
        {
            return await base.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<PhieuSuaChua>> GetById(Guid id)
        {
            return await base.GetById(id);
        }

        [HttpPost]
        public override async Task<ActionResult<PhieuSuaChua>> Create([FromBody] PhieuSuaChua entity)
        {
            return await base.Create(entity);
        }

        [HttpPut]
        public override async Task<ActionResult> Update([FromBody] PhieuSuaChua entity)
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
