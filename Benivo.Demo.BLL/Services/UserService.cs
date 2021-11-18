using Benivo.Demo.BLL.Infrastructure;
using Benivo.Demo.BLL.Interfaces.Services;
using Benivo.Demo.BLL.Validators.Users;
using Benivo.Demo.DAL.Infrastructure;
using Benivo.Demo.Entities.Entities;
using Benivo.Demo.Mapper.Infrastructure;
using Benivo.Demo.Models.Inputs;
using Benivo.Demo.Models.Outputs;
using System.Threading.Tasks;

namespace Benivo.Demo.BLL.Services
{
    public class UserService : Service, IUserService
    {
        public UserService(BenivoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RegisterOutput> RegisterAsync(RegisterInput registerInput)
        {
            var user = registerInput.MapTo<User>();

            var validator = new CreateUserValidator(DbContext);
            await validator.ValidateAsync(user);

            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();

            return user.MapTo<RegisterOutput>();
        }
    }
}
