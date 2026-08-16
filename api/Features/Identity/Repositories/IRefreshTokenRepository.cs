using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Identity.Models;
using MongoDB.Driver;

namespace api.Features.Identity.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task CreateAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken);
        
        Task<RefreshToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken);
        
        Task<UpdateResult> UpdateAsync(
            FilterDefinition<RefreshToken> filterDef,
            UpdateDefinition<RefreshToken> updateDef,
            CancellationToken cancellationToken);
        
        Task<DeleteResult> DeleteAsync(
            string token,
            CancellationToken cancellationToken);
    }
}