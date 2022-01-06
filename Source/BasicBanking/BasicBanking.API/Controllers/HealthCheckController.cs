using Microsoft.AspNetCore.Mvc;

namespace BasicBanking.API.Controllers
{
    public class HealthCheckController : ApiController
    {
        [HttpGet]
        public ActionResult<string> IsAlive()
        {
            return "Service is alive!";
        }
    }
}
