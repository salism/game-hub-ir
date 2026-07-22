namespace api.Features.Identity.DTOs.Responses
{
    public record CurrentUserResponse(
        string Username,
        string Email
    );
}