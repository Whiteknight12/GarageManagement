using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleVariableController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public RuleVariableController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetRule")]
        public async Task<RuleVariable> GetRuleVariables()
        {
            return await _db.ruleVariables.FirstOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> UpdateRuleVariable(RuleVariable entity)
        {
            _db.ruleVariables.Update(entity);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
