using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.Common.Helpers;
using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Validators.Users
{
    internal class CreateUserValidator : Validator<User>
    {
        public CreateUserValidator(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task ValidateAsync(User user)
        {
            var errors = new Dictionary<string, string[]>();

            if (await DbContext.Users.AnyAsync(u => u.Username == user.Username))
                errors.Add(nameof(user.Username), new string[] { "Username already exists" });

            if (await DbContext.Users.AnyAsync(u => u.Email == user.Email))
                errors.Add(nameof(user.Email), new string[] { "Email already exists" });

            if (errors.Any())
                throw ExceptionHelper.CreateFaultException("Bad request", StatusCodes.Status400BadRequest, errors);
        }
    }
}
