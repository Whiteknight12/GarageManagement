using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> _dbSet;

        public BaseController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = applicationDbContext.Set<T>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            return Ok(await _dbSet.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> GetById(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            await _dbSet.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] T entity)
        {
            _dbSet.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return NotFound();

            _dbSet.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }

        private object GetEntityId(T entity)
        {
            var property = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty($"{typeof(T).Name}ID");
            return property?.GetValue(entity);
        }
    }
}
