using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatTuPhuTungController : BaseController<VatTuPhuTung>
    {
        public VatTuPhuTungController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
