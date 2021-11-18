using Benivo.Demo.Models.Inputs;
using FluentValidation;

namespace Benivo.Demo.Api.Validators.JobAnnouncements
{
    public class SearchJobAnnouncementInputValidator : BasePaginationInputValidator<SearchJobAnnouncementInput>
    {
        public SearchJobAnnouncementInputValidator()
        {
            RuleFor(s => s.Location)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1)
                .When(s => s.Location.HasValue, ApplyConditionTo.AllValidators);

            RuleFor(s => s.JobCategoryId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo((short)1)
                .When(s => s.JobCategoryId.HasValue, ApplyConditionTo.AllValidators);

            RuleFor(s => s.EmploymentTypeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo((byte)1)
                .When(s => s.EmploymentTypeId.HasValue, ApplyConditionTo.AllValidators);
        }
    }
}
