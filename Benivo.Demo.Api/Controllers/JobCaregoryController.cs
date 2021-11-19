using Benivo.Demo.Api.Constants;
using Benivo.Demo.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Benivo.Demo.Api.Controllers
{
    [ApiVersion(ApiVersioning.ApiVersion1)]
    public class JobCaregoryController : BaseController
    {
        public JobCaregoryController(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByParentId(short? parentId)
        {
            if (parentId.HasValue && parentId <= 0)
                return BadRequest("Id must be greater than or equal to 1");

            var result = await ServiceFactory.JobCategoryService.GetAllAsync(parentId);

            return Ok(result);
        }
    }
}
