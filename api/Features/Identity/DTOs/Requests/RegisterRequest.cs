namespace api.Features.Identity.DTOs.Requests
{
    public sealed record RegisterRequest(
        string Username,
        string Email,
        string Password
    );
}