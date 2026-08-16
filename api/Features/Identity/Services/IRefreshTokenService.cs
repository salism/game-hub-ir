using api.Features.Identity.DTOs.Responses;

namespace api.Features.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateAsync(
            string userId,
            CancellationToken cancellationToken);
        
        Task<LoginResponse> RefreshAsync(
            string refreshToken,
            CancellationToken cancellationToken);
    }
}