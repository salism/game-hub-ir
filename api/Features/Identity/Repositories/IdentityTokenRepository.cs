using api.Features.Identity.Enums;
using api.Features.Identity.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Features.Identity.Repositories
{
    public class IdentityTokenRepository : IIdentityTokenRepository
    {
        private readonly IMongoCollection<IdentityToken> _collection;

        public IdentityTokenRepository(
        IMongoClient client,
        IMongoDbSettings settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<IdentityToken>("IdentityTokens");
        }

        public async Task CreateAsync(
        IdentityToken identityToken,
        CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(identityToken, cancellationToken: cancellationToken);
        }

        public async Task<IdentityToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken)
        {
            return await _collection.Find(doc => doc.Token == token).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(
        IdentityToken identityToken,
        CancellationToken cancellationToken)
        {
            await _collection
            .ReplaceOneAsync(doc => doc.UserId == identityToken.UserId, identityToken, cancellationToken: cancellationToken);
        }

        public async Task<IdentityToken?> GetActiveTokenAsync(
            string userId, 
            IdentityTokenType type, 
            CancellationToken cancellationToken)
        {
            return await _collection.Find(doc =>
            doc.UserId == userId &&
            doc.Type == type && 
            doc.UsedAt == null &&
            doc.ExpiresAt > DateTime.UtcNow)
            .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UpdateResult> UpdateAsync(
            FilterDefinition<IdentityToken> filterDef,
            UpdateDefinition<IdentityToken> updateDef,
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