using GithubStatsBot.Models;
using Octokit.Webhooks;
using Octokit.Webhooks.Events;
using Octokit.Webhooks.Events.Issues;

namespace GithubStatsBot.Services
{
    public class IssuesEventProcessor : WebhookEventProcessor
    {
        private readonly NotificationCenter _notificationCenter;

        public IssuesEventProcessor(NotificationCenter notificationCenter)
        {
            _notificationCenter = notificationCenter;
        }

        protected override async Task ProcessIssuesWebhookAsync(WebhookHeaders headers, IssuesEvent issuesEvent, IssuesAction action)
        {
            Console.WriteLine($"received webhook event {action} for issue {issuesEvent.Issue.Title}");
            if (action == IssuesAction.Opened)
            {
                var msg = $"issue {issuesEvent.Issue.Title} opened by {issuesEvent.Issue.User}";
                await _notificationCenter.SendNotification(NotificationType.IssueCreated, msg);
            }
            else if (action == IssuesAction.Closed)
            {
                var msg = $"issue {issuesEvent.Issue.Title} closed by {issuesEvent.Issue.User}";
                await _notificationCenter.SendNotification(NotificationType.IssueCreated, msg);
            }
        }
    }
}
