using api.Features.Identity.DTOs.Requests;
using FluentValidation;

namespace api.Features.Identity.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(property => property.UsernameOrEmail)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();

            RuleFor(property => property.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
        }
    }
}