using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Policy = "Phieu nhap vat tu")]
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapVatTuController : BaseController<PhieuNhapVatTu>
    {
        public PhieuNhapVatTuController(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
