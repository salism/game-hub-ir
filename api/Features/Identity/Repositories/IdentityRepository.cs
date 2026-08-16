using api.Features.Identity.Models;
using api.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Features.Identity.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IMongoCollection<AppUser> _collection;

        public IdentityRepository(
            IMongoClient client,
            IMongoDbSettings dbSettings)
        {
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<AppUser>("Users");
        }

        public async Task CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(user, options: null, cancellationToken: cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _collection
            .Find(doc => doc.Email == email).FirstOrDefaultAsync(cancellationToken);

            return appUser != null;
        }

        public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _collection
            .Find(doc => doc.Username == username).FirstOrDefaultAsync(cancellationToken);

            return appUser != null;
        }

        public async Task<AppUser?> GetByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken)
        {
            return await _collection
            .Find(doc => doc.Username == usernameOrEmail || doc.Email == usernameOrEmail)
            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _collection
            .Find(doc => doc.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UpdateResult> UpdateAsync(
            FilterDefinition<AppUser> filterDef,
            UpdateDefinition<AppUser> updateDef,
            CancellationToken cancellationToken)
        {
            return await _collection.UpdateOneAsync(
                filterDef,
                updateDef,
                cancellationToken: cancellationToken
            );
        }
    }
}