using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //them role de phan quyen tham so 
    [Authorize(Roles = "")]
    [Route("api/[controller]")]
    [ApiController]
    public class ThamSoController : BaseController<ThamSo>
    {
        public ThamSoController(ApplicationDbContext db) : base(db) { }
    }
}
