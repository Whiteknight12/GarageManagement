using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

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
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAll()
        {
            return Ok(await _dbSet.ToListAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            await _dbSet.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            Guid thucTheId = Guid.Empty;
            var idProperty = typeof(T).GetProperty("Id");
            var specialIdProperty = typeof(T).GetProperty("VatTuPhuTungId");
            if (idProperty != null && idProperty.PropertyType == typeof(Guid))
            {
                var value = idProperty.GetValue(entity);
                if (value is Guid guidValue)
                {
                    thucTheId = guidValue;
                }
            }
            else if (specialIdProperty != null && specialIdProperty.PropertyType == typeof(Guid))
            {
                var value = specialIdProperty.GetValue(entity);
                if (value is Guid guidValue)
                {
                    thucTheId = guidValue;
                }
            }
            var properties = typeof(T).GetProperties();
            var noiDung = string.Join("\n", properties.Select(p =>
            {
                var value = p.GetValue(entity);
                return $"{p.Name}: {value}";
            }));
            LichSu lichSu = new LichSu
            {
                Id = Guid.NewGuid(),
                ThoiDiemThucHien = DateTime.Now.ToLocalTime(),
                ThucTheLienQuanId= thucTheId,
                LoaiThucTheLienQuan = typeof(T).Name,
                NoiDung=noiDung,
                HanhDong="Tạo mới"
            };
            _applicationDbContext.lichSus.Add(lichSu);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update([FromBody] T entity)
        {
            Guid entityId = Guid.Empty;
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                idProperty = typeof(T).GetProperty("VatTuPhuTungId"); 
            if (idProperty == null || idProperty.PropertyType != typeof(Guid))
                return BadRequest("Entity must have a Guid Id property");
            var idValue = idProperty.GetValue(entity);
            if (idValue is not Guid guidValue)
                return BadRequest("Invalid Id value");
            entityId = guidValue;
            var existingEntity = await _dbSet.FindAsync(entityId);
            if (existingEntity == null)
                return NotFound();
            var changedProperties = new List<string>();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite) continue; 
                var oldValue = prop.GetValue(existingEntity)?.ToString();
                var newValue = prop.GetValue(entity)?.ToString();
                if (oldValue != newValue)
                {
                    changedProperties.Add($"{prop.Name}: {oldValue} -> {newValue}");
                }
            }
            if (changedProperties.Count == 0) return Ok("Không có thay đổi.");
            var noiDung = string.Join("\n", changedProperties);
            var lichSu = new LichSu
            {
                Id = Guid.NewGuid(),
                ThoiDiemThucHien = DateTime.Now.ToLocalTime(),
                ThucTheLienQuanId = entityId,
                LoaiThucTheLienQuan = typeof(T).Name,
                NoiDung = noiDung,
                HanhDong = "Cập nhật"
            };
            try
            {
                _applicationDbContext.Entry(existingEntity).State = EntityState.Detached;
                _dbSet.Update(entity);
                _applicationDbContext.lichSus.Add(lichSu);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Đã có thay đổi khác trên thực thể này. Vui lòng tải lại và thử lại.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi khi cập nhật: {ex.Message}");
            }
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return NotFound();
            else
            {
                Guid thucTheId = Guid.Empty;
                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty != null && idProperty.PropertyType == typeof(Guid))
                {
                    var value = idProperty.GetValue(entity);
                    if (value is Guid guidValue)
                    {
                        thucTheId = guidValue;
                    }
                }
                var properties = typeof(T).GetProperties();
                var noiDung = string.Join("\n", properties.Select(p =>
                {
                    var value = p.GetValue(entity);
                    return $"{p.Name}: {value}";
                }));
                LichSu lichSu = new LichSu
                {
                    Id = Guid.NewGuid(),
                    ThoiDiemThucHien = DateTime.Now.ToLocalTime(),
                    ThucTheLienQuanId = thucTheId,
                    LoaiThucTheLienQuan = typeof(T).Name,
                    NoiDung = noiDung,
                    HanhDong = "Xóa"
                };
                _applicationDbContext.lichSus.Add(lichSu);
            }
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
