using FluentValidation;
using api.Features.Identity.DTOs.Requests;

namespace api.Features.Identity.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(property => property.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(30);

            RuleFor(property => property.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();

            RuleFor(property => property.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(64)
            .Matches(@"(?=.*[a-z])")
            .Matches(@"(?=.*[A-Z])")
            .Matches(@"(?=.*\d)")
            .Matches(@"(?=.*[!@#$%^&*])");
        }
    }
}