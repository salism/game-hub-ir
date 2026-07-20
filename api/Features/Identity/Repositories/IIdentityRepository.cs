using api.Features.Identity.Models;

namespace api.Features.Identity.Repositories
{
    public interface IIdentityRepository
    {
        Task CreateAsync(AppUser user);
    }
}