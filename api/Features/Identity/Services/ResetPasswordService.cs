using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.Enums;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;
using MongoDB.Driver;

namespace api.Features.Identity.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IIdentityTokenRepository _tokenRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IEmailSenderService _emailSenderService;

        private static IdentityToken CreatePasswordResetToken(string userId)
        {
            return new IdentityToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Type = IdentityTokenType.PasswordReset,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public ResetPasswordService(
            IIdentityRepository identityRepository,
            IIdentityTokenRepository tokenRepository,
            IPasswordHasherService passwordHasherService,
            IEmailSenderService emailSenderService)
        {
            _identityRepository = identityRepository;
            _tokenRepository = tokenRepository;
            _passwordHasherService = passwordHasherService;
            _emailSenderService = emailSenderService;
        }

        public async Task SendResetPasswordEmailAsync(
            string emailOrUsername,
            CancellationToken cancellationToken)
        {
            AppUser? user = await _identityRepository
            .GetByUsernameOrEmailAsync(emailOrUsername, cancellationToken);

            if (user is null)
            {
                throw new InvalidOperationException("Email is not confirmed or no user found");
            }

            if (!user.EmailConfirmed)
            {
                throw new InvalidOperationException("Email is not confirmed or no user found");
            }

            IdentityToken? activeToken = await _tokenRepository.GetActiveTokenAsync(
                user.Id!,
                IdentityTokenType.PasswordReset,
                cancellationToken);
            
            if (activeToken is null)
            {
                activeToken = CreatePasswordResetToken(user.Id!);

                await _tokenRepository.CreateAsync(
                    activeToken,
                    cancellationToken);
            }

            await _emailSenderService.SendAsync(
                user.Email,
                "Reset your password",
                $"""
                <h2>Reset your password</h2>

                <p>Your password reset token is:</p>

                <b>{activeToken.Token}</b>
                """,
                cancellationToken);
        }

        public async Task ResetPasswordAsync(
            ResetPasswordRequest request,
            CancellationToken cancellationToken)
        {
            IdentityToken? identityToken = await _tokenRepository.GetByTokenAsync(
                request.Token,
                cancellationToken);
            
            if (identityToken is null)
            {
                throw new InvalidOperationException("Invalid password reset token");
            }

            if (identityToken.Type != IdentityTokenType.PasswordReset)
            {
                throw new InvalidOperationException("Invalid password reset token");
            }

            if (identityToken.UsedAt is not null)
            {
                throw new InvalidOperationException("Invalid password reset token");
            }

            if (identityToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Invalid password reset token");
            }

            AppUser? user = await _identityRepository.GetByIdAsync(
                identityToken.UserId,
                cancellationToken);
            
            if (user is null)
            {
                throw new InvalidOperationException("Invalid password reset token");
            }

            string passwordHash = _passwordHasherService.Hash(request.NewPassword);

            await _identityRepository.UpdateAsync(
                Builders<AppUser>.Filter.Eq(doc => doc.Id, user.Id!),
                Builders<AppUser>.Update.Combine(
                    Builders<AppUser>.Update.Set(doc => doc.PasswordHash, passwordHash),
                    Builders<AppUser>.Update.Set(doc => doc.UpdatedAt, DateTime.UtcNow)
                ),
                cancellationToken);
            
            await _tokenRepository.UpdateAsync(
            Builders<IdentityToken>.Filter.Eq(doc => doc.Token, identityToken.Token),
            Builders<IdentityToken>.Update.Set(doc => doc.UsedAt, DateTime.UtcNow),
            cancellationToken);
        }
    }
}