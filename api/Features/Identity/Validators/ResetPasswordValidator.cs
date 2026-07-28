using FluentValidation;
using api.Features.Identity.DTOs.Requests;

namespace api.Features.Identity.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator(){
            RuleFor(x => x.Token)
            .NotEmpty();

            RuleFor(x => x.NewPassword)
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