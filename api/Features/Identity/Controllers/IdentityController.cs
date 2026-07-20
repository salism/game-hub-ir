using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _service;

        public IdentityController(IIdentityService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody ] RegisterRequest request)
        {
            await _service.RegisterAsync(request);

            return Ok();
        }

    }
}