namespace api.Features.Identity.DTOs.Requests
{    
    public sealed record LoginRequest(
        string UsernameOrEmail,
        string Password
    );
}