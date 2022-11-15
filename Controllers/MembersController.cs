using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MountHebronAppApi.Context;
using MountHebronAppApi.Contracts;
using MountHebronAppApi.Services;

namespace MountHebronAppApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IApartmentService _service;
        private readonly ILogger<MembersController> _log;

        public MembersController(IApartmentService service, ILogger<MembersController> log)
        {
            _service = service;
            _log = log;
        }

        [Route("join-team")]
        [HttpGet]
        public async Task<IActionResult> JoinMember([FromBody] JoinRequest request)
        {
            try
            {
                var response = await _service.AddNewJoin(request);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _log.LogError(ex.Message);
                return BadRequest($"500 {ex.Message}");
            }
        }
    }
}
