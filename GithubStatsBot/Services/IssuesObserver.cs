using Octokit.Reactive;
using Octokit;
using System.Linq;

namespace GithubStatsBot.Services
{
    public class IssuesObserver : IObserver<IssueEvent>
    {
        private readonly IObservableGitHubClient _observableGitHubClient;
        private readonly IGitHubClient _gitHubClient;

        public IssuesObserver()
        {
            _observableGitHubClient = new ObservableGitHubClient(new ProductHeaderValue("noobquire"));
            _observableGitHubClient.Issue.Events
                .GetAllForRepository("noobquire", "github-stats-bot").Subscribe(this);
            Console.WriteLine("started listening for issue updates from github...");
        }

        public void OnCompleted()
        {
            Console.WriteLine("completed receiving issues from github api...");
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(IssueEvent value)
        {
            Console.WriteLine($"received issue event: {value.Event} for issue {value.Issue.Title}");
            if (value.Event == EventInfoState.AddedToProject)
            {
                var pastIssues = _gitHubClient.Issue.GetAllForCurrent().Result
                .Where(i => i.CreatedAt < value.CreatedAt);
                // search issues with similar body...
                // post reply with issue links
                // OR add awaiting-triage label
                // send notification to maintainer
            }
            else if (value.Event == EventInfoState.Commented /*and first comment from maintainer*/)
            {
                // - calculate time between creation and answer
                // - remove from list of unanswered issues
            }
            else if (value.Event == EventInfoState.Closed)
            {
                // - calculate time between creation and closure
                // - increment number of closed issues
                // if unanswered, remove from unanswered issues...
            }
        }
    }
}
