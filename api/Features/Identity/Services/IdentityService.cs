using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;

namespace api.Features.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _repository;
        private readonly IPasswordHasherService _passwordHasher;

        public IdentityService(IIdentityRepository repository, IPasswordHasherService passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }
        public async Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await _repository.ExistsByUsernameAsync(request.Username, cancellationToken))
            {
                throw new InvalidOperationException("Username already exists");
            }

            if (await _repository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                throw new InvalidOperationException("Email already exists");
            }

            AppUser appUser = new()
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                EmailConfirmed = false,
                IsActive = true
            };

            await _repository.CreateAsync(appUser, cancellationToken);
        }
    }
}