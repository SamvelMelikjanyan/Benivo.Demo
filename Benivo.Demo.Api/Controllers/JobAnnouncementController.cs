using Benivo.Demo.Api.Constants;
using Benivo.Demo.Api.Infrastructure;
using Benivo.Demo.Models.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Benivo.Demo.Api.Controllers
{
    [ApiVersion(ApiVersioning.ApiVersion1)]
    public class JobAnnouncementController : BaseController
    {
        public JobAnnouncementController(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchJobAnnouncementInput input)
        {
            var result = await ServiceFactory.JobAnnouncementService.SearchAsync(input, UserId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than or equal to 1");

            var result = await ServiceFactory.JobAnnouncementService.GetByIdAsync(id, UserId);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Bookmark(long id)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than or equal to 1");

            await ServiceFactory.JobAnnouncementService.ChangeBookmarkStatusAsync(new()
            {
                Id = id,
                UserId = UserId.Value,
                IsBookmarked = true
            });

            return Ok();
        }
    }
}
