using Benivo.Demo.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;

namespace Benivo.Demo.Api.Infrastructure
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected long? UserId
        {
            get
            {
                var externalId = User?.Claims?.FirstOrDefault(u => u.Type == Jwt.ExternalId)?.Value ?? string.Empty;

                if (long.TryParse(externalId, out long id))
                    return id;

                return null;
            }
        }
        protected readonly ServiceFactory ServiceFactory;

        public BaseController(ServiceFactory serviceFactory) => ServiceFactory = serviceFactory;

        [NonAction]
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
            => base.Ok(new ResponseModel<object>() { Data = value });

        [NonAction]
        public OkObjectResult Ok(string message)
            => base.Ok(new ResponseModel<object>() { Message = message });

        [NonAction]
        public BadRequestObjectResult BadRequest(string message) 
            => base.BadRequest(new ResponseModel<object>() { Message = message });
    }
}
