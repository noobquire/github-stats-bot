using GithubStatsBot.Models;
using Telegram.Bot;

namespace GithubStatsBot
{
    public class NotificationCenter
    {
        private readonly long receiverId;

        private readonly IConfiguration configuration;
        private readonly TelegramBotClient client;

        public NotificationCenter(IConfiguration configuration)
        {
            this.configuration = configuration;
            client = new TelegramBotClient(configuration.GetSection("MySettings:NotificationCenter:token").Value);

            receiverId = int.Parse(configuration.GetSection("MySettings:NotificationCenter:receiver").Value);
        }

        public async Task SendNotification(NotificationType type, string message)
        {
            await client.SendTextMessageAsync(receiverId, type.ToString() + ": " + message);
        }

        public async void SendStatistics(Statistics statistics)
        {
            var statisticsString = $"Average time before first answer: {statistics.AvgTimeBeforeFirstAnswer}\n" +
                $"Average time before being closed with a solution: {statistics.AvgTimeBeforeBeingClosed}\n" +
                $"Number of issues created per week: {statistics.NumberOfIssuesCreated}\n" +
                $"Number of issues closed per week: {statistics.NumberOfIssuesClosed}\n" +
                $"Unanswered issues: {string.Join(", ", statistics.UnansweredIssues.Select(i => i.Title))}";
            await client.SendTextMessageAsync(receiverId, statisticsString);
        }
    }
}
