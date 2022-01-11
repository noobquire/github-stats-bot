using Octokit.Webhooks;
using Octokit.Webhooks.Events;
using Octokit.Webhooks.Events.Issues;

namespace GithubStatsBot.Services
{
    public class IssuesEventProcessor : WebhookEventProcessor
    {
        protected override Task ProcessIssuesWebhookAsync(WebhookHeaders headers, IssuesEvent issuesEvent, IssuesAction action)
        {
            Console.WriteLine($"received webhook event {action} for issue {issuesEvent.Issue.Title}");
            return Task.CompletedTask;
        }
    }
}
