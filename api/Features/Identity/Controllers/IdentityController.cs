using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.DTOs.Responses;
using api.Features.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController(
        IIdentityService service,
        ICurrentUserService currentUserService, 
        IEmailConfirmationService emailConfirmationService,
        IResetPasswordService resetPasswordService) : ControllerBase
    {
        private readonly IIdentityService _identityService = service;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IEmailConfirmationService _emailConfirmationService = emailConfirmationService;
        private readonly IResetPasswordService _resetPasswordService = resetPasswordService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody ] RegisterRequest request, CancellationToken cancellationToken)
        {
            await _identityService.RegisterAsync(request, cancellationToken);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            LoginResponse loginResponse = await _identityService
            .LoginAsync(loginRequest, cancellationToken);

            return Ok(loginResponse);
        }

        [Authorize]
        [HttpGet("get-user-account")]
        public IActionResult GetUserAccount()
        {
            return Ok(new CurrentUserResponse(
                Username: _currentUserService.Username,
                Email: _currentUserService.Email
            ));
        }

        [Authorize]
        [HttpPost("send-confirmation-email")]
        public async Task<IActionResult> SendConfirmationEmail(
            CancellationToken cancellationToken
        )
        {
            await _emailConfirmationService
                .SendConfirmationEmailAsync(cancellationToken);

            return Ok();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(
            [FromBody] ConfirmEmailRequest request,
            CancellationToken cancellationToken)
        {
            await _emailConfirmationService.ConfirmEmailAsync(
                request.Token,
                cancellationToken);
            
            return NoContent();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(
            [FromBody] ForgotPasswordRequest forgotPasswordRequest,
            CancellationToken cancellationToken)
        {
            await _resetPasswordService.SendResetPasswordEmailAsync(
                forgotPasswordRequest.UsernameOrEmail,
                cancellationToken);
            
            return NoContent();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(
            [FromBody] ResetPasswordRequest resetPasswordRequest,
            CancellationToken cancellationToken)
        {
            await _resetPasswordService.ResetPasswordAsync(
                resetPasswordRequest,
                cancellationToken);

            return NoContent();
        }
    }
}