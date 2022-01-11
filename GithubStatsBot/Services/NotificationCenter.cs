using GithubStatsBot.Models;
using Telegram.Bot;

namespace GithubStatsBot
{
    public class NotificationCenter
    {
        private readonly TelegramBotClient client;

        public NotificationCenter()
        {
            client = new TelegramBotClient("2084592091:AAHBMkIauYXO941cn_Wff_3FsHRZNj6aCHc");
        }

        public async Task SendNotification(NotificationType type, string message)
        {
            await client.SendTextMessageAsync(430728363, type.ToString() + ": " + message);
        }

        public async void SendStatistics(Statistics statistics)
        {
            var statisticsString = $"Average time before first answer: {statistics.AvgTimeBeforeFirstAnswer}\n" +
                $"Average time before being closed with a solution: {statistics.AvgTimeBeforeBeingClosed}\n" +
                $"Number of issues created per week: {statistics.NumberOfIssuesCreated}\n" +
                $"Number of issues closed per week: {statistics.NumberOfIssuesClosed}\n" +
                $"Unanswered issues: {string.Join(" ", statistics.UnansweredIssues.Select(i => i.Title))}";
            await client.SendTextMessageAsync(430728363, statisticsString);
        }
    }
}
