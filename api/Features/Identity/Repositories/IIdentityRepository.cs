using api.Features.Identity.Models;
using MongoDB.Driver;

namespace api.Features.Identity.Repositories
{
    public interface IIdentityRepository
    {
        Task CreateAsync(
            AppUser user, 
            CancellationToken cancellationToken);

        Task<bool> ExistsByUsernameAsync(
            string username, 
            CancellationToken cancellationToken);

        Task<bool> ExistsByEmailAsync(
            string email,
            CancellationToken cancellationToken);

        Task<AppUser?> GetByUsernameOrEmailAsync(
            string usernameOrEmail, 
            CancellationToken cancellationToken);

        Task<AppUser?> GetByIdAsync(
            string id,
            CancellationToken cancellationToken);

        Task<UpdateResult> UpdateAsync(
            FilterDefinition<AppUser> filterDef,
            UpdateDefinition<AppUser> updateDef,
            CancellationToken cancellationToken);
    }
}