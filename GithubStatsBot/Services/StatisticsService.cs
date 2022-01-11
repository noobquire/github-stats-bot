using GithubStatsBot.Models;
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

        public async Task<Statistics> CollectStatistics()
        {
            return new Statistics
            {
                AvgTimeBeforeFirstAnswer = await GetAverageTimeBeforeFirstAnswer(),
                AvgTimeBeforeBeingClosed = await GetAverageTimeBeforeBeingClosed(),
                NumberOfIssuesCreated = await GetNumberOfIssuesCreated(),
                NumberOfIssuesClosed = await GetNumberOfIssuesClosed(),
                UnansweredIssues = await GetUnansweredIssues()
            };
        }

        private async Task<TimeSpan> GetAverageTimeBeforeFirstAnswer()
        {
            List<TimeSpan> total = new List<TimeSpan>();
            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot");
            var collaborators = await client.Repository.Collaborator.GetAll("noobquire", "github-stats-bot");

            foreach (var issue in issues)
            {
                var comments = await client.Issue.Comment.GetAllForIssue("noobquire", "github-stats-bot", issue.Number);
                var firstComment = comments.FirstOrDefault(c => collaborators.Select(col => col.Id).Contains(c.User.Id));

                if (firstComment == null)
                {
                    continue;
                }

                total.Add(firstComment.CreatedAt - issue.CreatedAt);
            }

            var spans = total.Select(s => s.TotalHours);
            if(spans.Any())
            {
                return TimeSpan.FromHours(spans.Average());
            }

            return TimeSpan.Zero;
        }

        private async Task<TimeSpan> GetAverageTimeBeforeBeingClosed()
        {
            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot");

            var total = issues.Where(i => i.State == ItemState.Closed).Select(i => i.ClosedAt.Value - i.CreatedAt);

            var spans = total.Select(s => s.TotalHours);
            if (spans.Any())
            {
                return TimeSpan.FromHours(spans.Average());
            }
            return TimeSpan.Zero;
        }

        private async Task<int> GetNumberOfIssuesCreated()
        {
            var issueRequest = new RepositoryIssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemStateFilter.All,
                Since = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7))
            };

            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot", issueRequest);
            return issues.Count();
        }

        private async Task<int> GetNumberOfIssuesClosed()
        {
            var issueRequest = new RepositoryIssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemStateFilter.Closed,
                Since = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7))
            };

            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot", issueRequest);
            return issues.Count();
        }

        private async Task<List<Issue>> GetUnansweredIssues()
        {
            List<Issue> result = new List<Issue>();

            var issueRequest = new RepositoryIssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemStateFilter.Open,
                Since = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7))
            };
            var issues = await client.Issue.GetAllForRepository("noobquire", "github-stats-bot", issueRequest);
            var collaborators = await client.Repository.Collaborator.GetAll("noobquire", "github-stats-bot");

            foreach (var issue in issues)
            {
                var comments = await client.Issue.Comment.GetAllForIssue("noobquire", "github-stats-bot", issue.Number);

                if (!comments.Any(c => collaborators.Any(col => col.Id == c.Id)))
                {
                    result.Add(issue);
                }
            }

            return result;
        }
    }
}
