using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.DTOs.Responses;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;

namespace api.Features.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _repository;
        private readonly IPasswordHasherService _passwordHasher;

        private readonly IJwtService _jwtService;

        public IdentityService(IIdentityRepository repository, IPasswordHasherService passwordHasher, IJwtService jwtService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
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

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _repository.GetByUsernameOrEmailAsync(loginRequest.UsernameOrEmail, cancellationToken);
            
            if (appUser is null)
            {
                throw new InvalidOperationException("Invalid credentials");
            }

            bool isValid = _passwordHasher.Verify(loginRequest.Password, appUser.PasswordHash);

            if (!isValid)
            {
                throw new InvalidOperationException("Invalid credentials");
            }

            if (!appUser.IsActive)
            {
                throw new InvalidOperationException("Account is inactive");
            }

            string accessToken = _jwtService.GenerateAccessToken(appUser);

            return new LoginResponse(accessToken);
        }
    }
}