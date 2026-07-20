using api.Features.Identity.DTOs.Requests;

namespace api.Features.Identity.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(RegisterRequest registerRequest);
    }
}