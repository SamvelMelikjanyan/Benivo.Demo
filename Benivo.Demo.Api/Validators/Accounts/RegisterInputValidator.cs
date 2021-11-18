using Benivo.Demo.Models.Inputs;
using FluentValidation;

namespace Benivo.Demo.Api.Validators.Accounts
{
    public class RegisterInputValidator : AbstractValidator<RegisterInput>
    {
        public RegisterInputValidator()
        {
            RuleFor(u => u.Username)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .EmailAddress();

            RuleFor(u => u.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(u => u.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(u => u.Password)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Password();
        }
    }
}
