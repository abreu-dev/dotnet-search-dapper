using Microsoft.AspNetCore.Mvc;

namespace DotNetSearch.API.Controllers.V2
{
    [ApiVersion("2")]
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
