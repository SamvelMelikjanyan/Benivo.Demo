using Benivo.Demo.ApiModels.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Benivo.Demo.Api.Infrastructure
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected readonly ServiceFactory ServiceFactory;

        public BaseController(ServiceFactory serviceFactory) => ServiceFactory = serviceFactory;

        [NonAction]
        public override ResponseModel<object> Ok() => new();

        [NonAction]
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
            => base.Ok(new ResponseModel<object>() { Data = value });

        [NonAction]
        public OkObjectResult Ok(string message)
            => base.Ok(new ResponseModel<object>() { Message = message });
    }
}
