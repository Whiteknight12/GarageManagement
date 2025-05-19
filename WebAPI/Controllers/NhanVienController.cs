using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Nhan vien")]
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : BaseController<NhanVien>
    {
        private readonly ApplicationDbContext _db;

        public NhanVienController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _db = applicationDbContext;
        }
    }
}
