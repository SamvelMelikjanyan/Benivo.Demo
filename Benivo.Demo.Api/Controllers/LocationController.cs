using Benivo.Demo.Api.Constants;
using Benivo.Demo.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Benivo.Demo.Api.Controllers
{
    [ApiVersion(ApiVersioning.ApiVersion1)]
    public class LocationController : BaseController
    {
        public LocationController(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await ServiceFactory.CityService.GetLocationsAsync();

            return Ok(result);
        }
    }
}
