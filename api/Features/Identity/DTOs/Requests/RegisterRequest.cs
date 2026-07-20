namespace api.Features.Identity.DTOs.Requests
{
    public record RegisterRequest(
        string Username,
        string Email,
        string Password
    );
}