using Benivo.Demo.Models.Infrastructure;
using FluentValidation;

namespace Benivo.Demo.Api.Validators
{
    public class BasePaginationInputValidator<T> : AbstractValidator<T> where T : BasePaginationInput
    {
        public BasePaginationInputValidator()
        {
            RuleFor(p => p.Skip)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Take)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0);
        }
    }
}
