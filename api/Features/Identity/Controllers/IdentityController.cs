using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(IIdentityService service) : ControllerBase
    {
        private readonly IIdentityService _identityService = service;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody ] RegisterRequest request, CancellationToken cancellationToken)
        {
            await _identityService.RegisterAsync(request, cancellationToken);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            await _identityService.LoginAsync(loginRequest, cancellationToken);

            return Ok();
        }

    }
}