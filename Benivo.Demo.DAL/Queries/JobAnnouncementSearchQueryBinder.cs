using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Models.Inputs;
using Benivo.Demo.Models.Outputs;
using System;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Benivo.Demo.DAL.Queries
{
    public class JobAnnouncementSearchBinder : QueryBinder<SearchJobAnnouncementInput, SearchJobAnnouncementOutput>
    {
        private readonly long? _userId;

        public JobAnnouncementSearchBinder(BenivoDbContext benivoDbContext, long? userId)
            : base(benivoDbContext) => _userId = userId;

        public override QueryBinderResult<SearchJobAnnouncementOutput> Bind(SearchJobAnnouncementInput input)
        { 
            var query = DbContext.JobAnnouncements
                .Where(ja => (!ja.ExpirationDate.HasValue || ja.ExpirationDate >= DateTime.UtcNow)
                          && (string.IsNullOrWhiteSpace(input.Title) || ja.Title.Contains(input.Title))
                          && (!input.JobCategoryId.HasValue || ja.JobCategoryId == input.JobCategoryId)
                          && (!input.Location.HasValue || ja.CityId == input.Location)
                          && (!input.EmploymentTypeId.HasValue || ja.EmploymentTypeId == input.EmploymentTypeId));
            
            var totalCount = query.DeferredCount().FutureValue();

            var items = query.Select(ja => new SearchJobAnnouncementOutput
            {
                Id = ja.Id,
                Title = ja.Title,
                CityId = ja.CityId,
                CountryId = ja.City.CountryId,
                EmploymentTypeId = ja.EmploymentTypeId,
                CompanyId = ja.CompanyId,
                IsBookmarked = _userId.HasValue && ja.UserJobAnnouncements.Any(uja => uja.UserId == _userId && uja.IsBookmarked),
                IsApplied = _userId.HasValue && ja.UserJobAnnouncements.Any(uja => uja.UserId == _userId && uja.IsApplied)
            }).OrderByDescending(ja => ja.Id).Skip(input.Skip).Take(input.Take).Future();

            return new() { TotalCount = totalCount, Items = items };
        }
    }
}
