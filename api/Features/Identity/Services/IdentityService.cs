using api.Features.Identity.DTOs.Requests;
using api.Features.Identity.Models;
using api.Features.Identity.Repositories;

namespace api.Features.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _repository;

        public IdentityService(IIdentityRepository repository)
        {
            _repository = repository;
        }
        public async Task RegisterAsync(RegisterRequest request)
        {
            AppUser appUser = new()
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                EmailConfirmed = false,
                IsActive = true
            };

            await _repository.CreateAsync(appUser);
        }
    }
}