using Benivo.Demo.IdentityServer4.Common.Extensions;
using Benivo.Demo.IdentityServer4.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;

namespace Benivo.Demo.IdentityServer4.Infrastructure
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public override ResponseModel<object> Ok() => new();

        [NonAction]
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
            => base.Ok(new ResponseModel<object>() { Data = value });

        [NonAction]
        public OkObjectResult Ok(string message)
            => base.Ok(new ResponseModel<object>() { Message = message });

        [NonAction]
        public BadRequestObjectResult BadRequest(string message)
            => new(new ResponseModel<object>() { Message = message });

        public ObjectResult IdentityResponse(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                ResponseModel<object> response = new()
                {
                    Errors = identityResult.Errors.ToDictionary()
                };

                ObjectResult result = new(response)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                return result;
            }

            return Ok(identityResult);
        }
    }
}
