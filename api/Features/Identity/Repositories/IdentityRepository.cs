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

            _collection = database.GetCollection<AppUser>("users");
        }

        public async Task CreateAsync(AppUser user)
        {
            await _collection.InsertOneAsync(user);
        }
    }
}