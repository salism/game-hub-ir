using api.Features.Identity.Models;

namespace api.Features.Identity.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(AppUser user);
    }
}