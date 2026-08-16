using System.Security.Cryptography;
using api.Features.Identity.DTOs.Responses;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;
using api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.Features.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _options;

        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }

        public RefreshTokenService(
            IRefreshTokenRepository repository,
            IIdentityRepository identityRepository,
            IJwtService jwtService,
            IOptions<JwtSettings> options)
        {
            _repository = repository;
            _identityRepository = identityRepository;
            _jwtService = jwtService;
            _options = options.Value;
        }

        public async Task<string> GenerateAsync(
            string userId,
            CancellationToken cancellationToken)
        {
            RefreshToken refreshToken = new()
            {
                Token = GenerateRefreshToken(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    _options.RefreshTokenExpirationDays)
            };

            await _repository.CreateAsync(refreshToken,
                cancellationToken);
            
            return refreshToken.Token;
        }

        public async Task<LoginResponse> RefreshAsync(
            string refreshToken,
            CancellationToken cancellationToken)
        {
            RefreshToken? storedToken = await _repository
                .GetByTokenAsync(refreshToken,
                    cancellationToken);

            if (storedToken is null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                await _repository.DeleteAsync(storedToken.Token!,
                    cancellationToken);

                throw new UnauthorizedAccessException("Refresh token expired");
            }

            if (storedToken.RevokedAt is not null)
            {
                await _repository.DeleteAsync(storedToken.Token!,
                    cancellationToken);

                throw new UnauthorizedAccessException("Refresh token revoked");
            }

            AppUser? appUser = await _identityRepository.GetByIdAsync(
                storedToken.UserId,
                cancellationToken);
            
            if (appUser is null)
            {
                await _repository.DeleteAsync(storedToken.Token!,
                    cancellationToken);

                throw new UnauthorizedAccessException("User or token is invalid");
            }

            if (!appUser.IsActive)
            {
                throw new UnauthorizedAccessException("User is banned");
            }


            await _repository.UpdateAsync(
                Builders<RefreshToken>.Filter.Eq(doc => doc.Token, refreshToken),
                Builders<RefreshToken>.Update.Set(doc => doc.RevokedAt, DateTime.UtcNow),
                cancellationToken);
            
            string newRefreshToken = await GenerateAsync(
                appUser.Id!,
                cancellationToken);

            string accessToken = _jwtService.GenerateAccessToken(
                appUser);
            
            
            return new LoginResponse
            (
                AccessToken: accessToken,
                RefreshToken: newRefreshToken
            );
        } 
    }
}