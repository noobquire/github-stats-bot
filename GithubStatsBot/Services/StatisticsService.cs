using GithubStatsBot.Models;
using System.Linq;
using Octokit;

namespace GithubStatsBot.Services
{
    public class StatisticsService
    {
        private GitHubClient client;

        public StatisticsService(GitHubClient client)
        {
            this.client = client;
        }

        public Statistics CollectStatistics()
        {
            throw new NotImplementedException();
        }

        private async TimeSpan GetAverageTimeBeforeFirstAnswer()
        {
            List<TimeSpan> total = new List<TimeSpan>();
            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot");

            foreach (var issue in issues)
            {
                var comments = await client.Issue.Comment.GetAllForIssue("noobquire", "github-stats-bot", issue.Number);
                var firstComment = comments.FirstOrDefault(c => c.User.Permissions.Admin);

                if (firstComment is null)
                {
                    continue;
                }

                total.Add(firstComment.CreatedAt - issue.CreatedAt);
            }

            return TimeSpan.FromHours(total.Select(s => s.TotalHours).Average());
        }

        private async TimeSpan GetAverageTimeBeforeBeingClosed()
        {
            List<TimeSpan> total = new List<TimeSpan>();
            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot");

            foreach (var issue in issues)
            {

            }
        }

        private ushort GetNumberOfIssuesCreated()
        {
            throw new NotImplementedException();
        }

        private ushort GetNumberOfIssuesClosed()
        {
            throw new NotImplementedException();
        }

        private List<Issue> GetAnsweredIssues()
        {
            throw new NotImplementedException();
        }
    }
}
