using Microsoft.AspNetCore.Mvc;

namespace DotNetSearch.API.Controllers.V1
{
    [ApiVersion("1")]
    public class HealthCheckController : BaseController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
