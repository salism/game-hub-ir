namespace api.Features.Identity.DTOs.Requests
{
    public sealed record ResetPasswordRequest(
        string Token,
        string NewPassword
    );
}