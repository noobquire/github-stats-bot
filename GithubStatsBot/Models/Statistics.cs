using Octokit;

namespace GithubStatsBot.Models
{
    public class Statistics
    {
        public TimeSpan AvgTimeBeforeFirstAnswer { get; set; }
        public TimeSpan AvgTimeBeforeBeingClosed { get; set; }
        public ushort NumberOfIssuesCreated { get; set; }
        public ushort NumberOfIssuesClosed { get; set; }
        public Issue MostMentionedTopic { get; set; }
        public IList<Issue> UnansweredIssues { get; set; }

    }
}
