namespace api.Features.Identity.DTOs.Requests
{
    public sealed record ConfirmEmailRequest(
        string Token
    );
}