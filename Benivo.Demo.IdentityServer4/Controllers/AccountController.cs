using Benivo.Demo.IdentityServer4.Common.Models.Inputs;
using Benivo.Demo.IdentityServer4.Data.Entities;
using Benivo.Demo.IdentityServer4.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace Benivo.Demo.IdentityServer4.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager) => _userManager = userManager;

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var user = new ApplicationUser
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                ExternalId = registerUser.ExternalId
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
                return IdentityResponse(result);

            transactionScope.Complete();

            return Ok(user.Id);
        }
    }
}
