using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Identity.Enums;
using api.Features.Identity.Models;
using MongoDB.Driver;

namespace api.Features.Identity.Repositories
{
    public interface IIdentityTokenRepository
    {
        Task CreateAsync(
            IdentityToken identityToken,
            CancellationToken cancellationToken);

        Task<IdentityToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken);

        Task UpdateAsync(
            IdentityToken identityToken,
            CancellationToken cancellationToken);

        Task<IdentityToken?> GetActiveTokenAsync(
            string userId,
            IdentityTokenType type,
            CancellationToken cancellationToken);
        
        Task<UpdateResult> UpdateAsync(
            FilterDefinition<IdentityToken> filterDef,
            UpdateDefinition<IdentityToken> updateDef,
            CancellationToken cancellationToken);
    }
}