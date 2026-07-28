namespace api.Features.Identity.DTOs.Requests
{
    public sealed record ForgotPasswordRequest(
        string UsernameOrEmail
    );
}