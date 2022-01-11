using Octokit.Reactive;
using Octokit;
using System.Linq;

namespace GithubStatsBot.Services
{
    public class GithubObserver : IObserver<Issue>
    {
        private readonly IObservableGitHubClient _observableGitHubClient;
        private readonly IGitHubClient _gitHubClient;

        public GithubObserver()
        {
            _observableGitHubClient = new ObservableGitHubClient(new ProductHeaderValue("noobquire"));
            _observableGitHubClient.Issue
                .GetAllForRepository("noobquire", "github-stats-bot").Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Issue value)
        {
            var pastIssues = _gitHubClient.Issue.GetAllForCurrent().Result
                .Where(i => i.CreatedAt < value.CreatedAt);
            // search issues with similar body...
            // post reply with issue links...
        }
    }
}
