using GithubStatsBot.Models;
using Octokit.Webhooks;
using Octokit.Webhooks.Events;
using Octokit.Webhooks.Events.Issues;
using Octokit.Webhooks.Events.PullRequest;
using Octokit.Webhooks.Events.Release;

namespace GithubStatsBot.Services
{
    public class EventProcessor : WebhookEventProcessor
    {
        private readonly NotificationCenter _notificationCenter;

        public EventProcessor(NotificationCenter notificationCenter)
        {
            _notificationCenter = notificationCenter;
        }

        protected override async Task ProcessIssuesWebhookAsync(WebhookHeaders headers, IssuesEvent issuesEvent, IssuesAction action)
        {
            Console.WriteLine($"received webhook event {action} for issue {issuesEvent.Issue.Title}");
            if (action == IssuesAction.Opened)
            {
                var msg = $"issue {issuesEvent.Issue.Title} opened by {issuesEvent.Issue.User.Login}";
                await _notificationCenter.SendNotification(NotificationType.IssueCreated, msg);
            }
            else if (action == IssuesAction.Closed)
            {
                var msg = $"issue {issuesEvent.Issue.Title} closed by {issuesEvent.Issue.User.Login}";
                await _notificationCenter.SendNotification(NotificationType.IssueCreated, msg);
            }
        }

        protected override async Task ProcessReleaseWebhookAsync(WebhookHeaders headers, ReleaseEvent releaseEvent, ReleaseAction action)
        {
            Console.WriteLine($"received webhook event {action} for release {releaseEvent.Release.Name}");
            if (action == ReleaseAction.Published)
            {
                var msg = $"release {releaseEvent.Release.Name} published by {releaseEvent.Release.Author.Login}";
                await _notificationCenter.SendNotification(NotificationType.ReleasePublished, msg);
            }
        }

        protected override async Task ProcessPullRequestWebhookAsync(WebhookHeaders headers, PullRequestEvent pullRequestEvent, PullRequestAction action)
        {
            Console.WriteLine($"received webhook event {action} for PR {pullRequestEvent.PullRequest.Title}");
            if (action == PullRequestAction.Opened)
            {
                var msg = $"PR {pullRequestEvent.PullRequest.Title} opened by {pullRequestEvent.PullRequest.User.Login}";
                await _notificationCenter.SendNotification(NotificationType.PullRequestCreated, msg);
            }
            else if (action == PullRequestAction.Edited)
            {
                var msg = $"PR {pullRequestEvent.PullRequest.Title} updated by {pullRequestEvent.PullRequest.User.Login}";
                await _notificationCenter.SendNotification(NotificationType.PullRequestCreated, msg);
            }
            else if (action == PullRequestAction.Closed)
            {
                var msg = $"PR {pullRequestEvent.PullRequest.Title} closed by {pullRequestEvent.PullRequest.User.Login}";
                await _notificationCenter.SendNotification(NotificationType.PullRequestCreated, msg);
            }
        }
    }
}
