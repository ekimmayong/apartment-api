using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MountHebronAppApi.Context;

namespace MountHebronAppApi.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public readonly ApartmentContext _context;

        public TestController(ApartmentContext context)
        {
            _context = context;
        }

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Accessing your Model

            var response = await _context.Apartments.ToListAsync();


            return Ok(response);
        }
    }
}
