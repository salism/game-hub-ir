using BCryptHasher = BCrypt.Net.BCrypt;

namespace api.Features.Identity.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string Hash(string password)
        {
            return BCryptHasher.HashPassword(password);
        }

        public bool Verify(string password, string passwordHash)
        {
            return BCryptHasher.Verify(password, passwordHash);
        }
    }
}