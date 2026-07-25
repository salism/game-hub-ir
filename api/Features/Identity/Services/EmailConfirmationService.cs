using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Features.Identity.Enums;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace api.Features.Identity.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IIdentityTokenRepository _tokenRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<EmailConfirmationService> _logger;

        public EmailConfirmationService(
            IIdentityRepository identityRepository,
            IIdentityTokenRepository tokenRepository,
            ICurrentUserService currentUserService,
            ILogger<EmailConfirmationService> logger)
        {
            _identityRepository = identityRepository;
            _tokenRepository = tokenRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task SendConfirmationEmailAsync(CancellationToken cancellationToken)
        {
            AppUser? user = await _identityRepository.GetByIdAsync(_currentUserService.UserId,
            cancellationToken: cancellationToken);

            if (user is null)
            {
                throw new InvalidOperationException("User is null or email is already confrimed");
            }

            if (user.EmailConfirmed == true)
            {
                throw new InvalidOperationException("User is null or email is already confrimed");
            }

            IdentityToken? activeToken = await _tokenRepository.GetActiveTokenAsync(
                user.Id!,
                IdentityTokenType.EmailConfirmation,
                cancellationToken
            );

            if (activeToken is null)
            {
                activeToken = new IdentityToken{
                    UserId = user.Id!,
                    Token = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddHours(24)
                };

                await _tokenRepository.CreateAsync(activeToken, cancellationToken);
            }

            _logger.LogInformation(
                "Email confirmation token for user {UserId}: {Token}",
                user.Id,
                activeToken.Token
            );
        }
        
        public Task ConfirmEmailAsync(string token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}