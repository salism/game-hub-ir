using api.Features.Identity.Enums;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;


namespace api.Features.Identity.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IIdentityTokenRepository _tokenRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailSenderService _emailSenderService;

        private static IdentityToken CreateEmailConfirmationToken(string userId)
        {
            return new IdentityToken
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Type = IdentityTokenType.EmailConfirmation,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        public EmailConfirmationService(
            IIdentityRepository identityRepository,
            IIdentityTokenRepository tokenRepository,
            ICurrentUserService currentUserService,
            IEmailSenderService emailSenderService)
        {
            _identityRepository = identityRepository;
            _tokenRepository = tokenRepository;
            _currentUserService = currentUserService;
            _emailSenderService = emailSenderService;
        }

        public async Task SendConfirmationEmailAsync(CancellationToken cancellationToken)
        {
            AppUser? user = await _identityRepository.GetByIdAsync(_currentUserService.UserId,
            cancellationToken: cancellationToken);

            if (user is null)
            {
                throw new InvalidOperationException("User is null or email is already confrimed");
            }

            if (user.EmailConfirmed)
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
                activeToken = CreateEmailConfirmationToken(user.Id!);

                await _tokenRepository.CreateAsync(activeToken, cancellationToken);
            }

            await _emailSenderService.SendAsync(
                user.Email,
                "Confirm your email",
                $"""
                <h2>Welcome to Game Hub</h2>

                <p>Your confirmation token is:</p>

                <b>{activeToken.Token}</b>
                """,
                cancellationToken);
        }
        
        public Task ConfirmEmailAsync(string token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}