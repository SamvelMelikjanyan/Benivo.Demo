using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.Common.Helpers;
using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Validators.UserJobAnnouncements
{
    internal class ChangeBookmarkStatusValidator : Validator<UserJobAnnouncement>
    {
        public ChangeBookmarkStatusValidator(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task ValidateAsync(UserJobAnnouncement entity)
        {
            if (!await DbContext.JobAnnouncements.AnyAsync(ja => ja.Id == entity.JobAnnouncementId))
                throw ExceptionHelper.CreateFaultException("Not Found", StatusCodes.Status400BadRequest);

            if (!await DbContext.Users.AnyAsync(ja => ja.Id == entity.UserId))
                throw ExceptionHelper.CreateFaultException("Not Found", StatusCodes.Status400BadRequest);
        }
    }
}
