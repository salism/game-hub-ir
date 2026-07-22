using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.Validators;

namespace api.Features.Identity.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken);

        Task LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}