using api.Features.Identity.Models;
using api.Settings;
using MongoDB.Driver;

namespace api.Features.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly IMongoCollection<RefreshToken> _collection;

        public RefreshTokenRepository(
            IMongoClient client,
            IMongoDbSettings dbSettings)
        {
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _collection = database.GetCollection<RefreshToken>("RefreshTokens");
        }
        public async Task CreateAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(
                refreshToken,
                cancellationToken: cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _collection
            .Find(
                doc => doc.Token == token).FirstOrDefaultAsync(
                cancellationToken: cancellationToken);
            
            return refreshToken;
        }

        public async Task<UpdateResult> UpdateAsync(
            FilterDefinition<RefreshToken> filterDef,
            UpdateDefinition<RefreshToken> updateDef,
            CancellationToken cancellationToken)
        {
            UpdateResult updateResult = await _collection.UpdateOneAsync(
                filterDef,
                updateDef,
                cancellationToken: cancellationToken);
            
            return updateResult;
        }

        public async Task<DeleteResult> DeleteAsync(
            string token,
            CancellationToken cancellationToken)
        {
            DeleteResult deleteResult = await _collection.DeleteOneAsync(
                doc => 
                doc.Token == token,
                cancellationToken: cancellationToken);
            
            return deleteResult;
        }
    }
}