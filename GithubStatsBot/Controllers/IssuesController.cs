using Microsoft.AspNetCore.Mvc;

namespace GithubStatsBot.Controllers
{
    [ApiController]
    public class IssuesController : Controller
    {

        public IssuesController()
        {
        }

        [HttpGet]
        [Route("/updates")]
        public IActionResult GetUpdates()
        {
            return Ok();
        }
    }
}
