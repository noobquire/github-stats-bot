using GithubStatsBot.Services;
using Microsoft.AspNetCore.Mvc;

namespace GithubStatsBot.Controllers
{
    [ApiController]
    public class IssuesController : Controller
    {
        private readonly IssuesObserver issuesObserver;

        public IssuesController(IssuesObserver issuesObserver)
        {
            this.issuesObserver = issuesObserver;
        }

        [HttpGet]
        [Route("/updates")]
        public IActionResult GetUpdates()
        {
            return Ok();
        }
    }
}
