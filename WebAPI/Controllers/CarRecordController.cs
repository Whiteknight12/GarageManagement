using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRecordController : BaseController<CarRecord>
    {
        public CarRecordController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
