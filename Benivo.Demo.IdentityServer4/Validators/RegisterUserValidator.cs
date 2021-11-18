using Benivo.Demo.IdentityServer4.Common.Models.Inputs;
using FluentValidation;

namespace Benivo.Demo.IdentityServer4.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator()
        {
            RuleFor(u => u.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();

            RuleFor(u => u.ExternalId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
