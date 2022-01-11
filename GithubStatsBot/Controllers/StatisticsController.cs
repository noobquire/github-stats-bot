using GithubStatsBot.Services;
using Microsoft.AspNetCore.Mvc;

namespace GithubStatsBot.Controllers
{
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly StatisticsService statisticsService;
        private readonly NotificationCenter notificationCenter;

        public StatisticsController(StatisticsService statisticsService, NotificationCenter notificationCenter)
        {
            this.statisticsService = statisticsService;
            this.notificationCenter = notificationCenter;
        }

        [HttpGet]
        [Route("/weeklystatistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var statistics = await statisticsService.CollectStatistics();
            notificationCenter.SendStatistics(statistics);
            return Ok();
        }
    }
}
