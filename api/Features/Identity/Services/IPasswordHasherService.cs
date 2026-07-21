namespace api.Features.Identity.Services
{
    public interface IPasswordHasherService
    {
        string Hash(string password);

        bool Verify(string password, string passwordHash);
    }
}