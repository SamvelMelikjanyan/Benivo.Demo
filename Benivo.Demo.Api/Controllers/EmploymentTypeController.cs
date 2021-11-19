using Benivo.Demo.Api.Constants;
using Benivo.Demo.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Benivo.Demo.Api.Controllers
{
    [ApiVersion(ApiVersioning.ApiVersion1)]
    public class EmploymentTypeController : BaseController
    {
        public EmploymentTypeController(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await ServiceFactory.EmploymentTypeService.GetAllAsync();

            return Ok(result);
        }
    }
}
