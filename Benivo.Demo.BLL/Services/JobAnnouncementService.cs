using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.BLL.Interfaces.Services;
using Benivo.Demo.BLL.Validators.UserJobAnnouncements;
using Benivo.Demo.Common.Helpers;
using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.DAL.Queries;
using Benivo.Demo.Entities.Entities;
using Benivo.Demo.Mapper.Infrastructure;
using Benivo.Demo.Models.Infrastructure;
using Benivo.Demo.Models.Inputs;
using Benivo.Demo.Models.Outputs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Services
{
    public class JobAnnouncementService : Service, IJobAnnouncementService
    {
        public JobAnnouncementService(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<GetJobAnnouncementByIdOutput> GetByIdAsync(long id, long? userId)
        {
            var result = await DbContext.JobAnnouncements.Select(ja => new GetJobAnnouncementByIdOutput
            {
                Id = ja.Id,
                Title = ja.Title,
                JobCategoryId = ja.JobCategoryId,
                CompanyId = ja.CompanyId,
                Description = ja.Description,
                EmploymentTypeId = ja.EmploymentTypeId,
                ExpirationDate = ja.ExpirationDate,
                CityId = ja.CityId,
                IsBookmarked = userId.HasValue && ja.UserJobAnnouncements.Any(uja => uja.UserId == userId && uja.IsBookmarked),
                IsApplied = userId.HasValue && ja.UserJobAnnouncements.Any(uja => uja.UserId == userId && uja.IsApplied)
            }).AsNoTracking().FirstOrDefaultAsync(ja => ja.Id == id);

            if (result == default)
                throw ExceptionHelper.CreateFaultException("Not Found", StatusCodes.Status400BadRequest);

            return result;
        }
        public async Task<CollectionPage<SearchJobAnnouncementOutput>> SearchAsync(SearchJobAnnouncementInput input, long? userId)
        {
            var queryBinder = new JobAnnouncementSearchBinder(DbContext, userId);

            var collectionPageBinder =
                new CollectionPageBinder<SearchJobAnnouncementInput, SearchJobAnnouncementOutput>(queryBinder);

            return await collectionPageBinder.BindAsync(input);
        }

        public async Task ChangeBookmarkStatusAsync(ChangeBookmarkStatusInput input)
        {
            var userJobAnnouncement = await DbContext.UserJobAnnouncements
                .FirstOrDefaultAsync(ja => ja.JobAnnouncementId == input.Id && ja.UserId == input.UserId);

            if (userJobAnnouncement == default)
            {
                userJobAnnouncement = input.MapTo<UserJobAnnouncement>();
                await DbContext.UserJobAnnouncements.AddAsync(userJobAnnouncement);
            }
            else
            {
                userJobAnnouncement.IsBookmarked = input.IsBookmarked;
                DbContext.UserJobAnnouncements.Update(userJobAnnouncement);
            }

            var validator = new ChangeBookmarkStatusValidator(DbContext);
            await validator.ValidateAsync(userJobAnnouncement);

            await DbContext.SaveChangesAsync();
        }
    }
}
