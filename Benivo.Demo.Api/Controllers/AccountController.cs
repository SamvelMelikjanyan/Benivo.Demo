using Benivo.Demo.Api.Infrastructure;
using Benivo.Demo.Common.Constants;
using Benivo.Demo.Models.Inputs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace Benivo.Demo.Api.Controllers
{
    [ApiVersion(ApiVersioning.ApiVersion1)]
    public class AccountController : BaseController
    {
        public AccountController(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterInput registerInput)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var user = await ServiceFactory.UserService.RegisterAsync(registerInput);

            await ServiceFactory.BenivoIdentityService.RegisterUser(user.Username, registerInput.Email, registerInput.Password, user.Id);

            transaction.Complete();

            return Ok(user);
        }
    }
}
