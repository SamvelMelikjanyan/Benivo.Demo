using FluentValidation;
using System.Text.RegularExpressions;

namespace Benivo.Demo.Api.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
            => ruleBuilder.Must(password =>
            {
                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[\s\S]{8,20}$");
                return regex.IsMatch(password);
            }).WithMessage("The password must contain min. 8 and max. 20 symbols including at least one uppercase, one lowercase Latin letters and one digit!");
    }
}
