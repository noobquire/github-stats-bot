using Octokit.Reactive;
using Octokit;
using System.Linq;

namespace GithubStatsBot.Services
{
    public class GithubObserver : IObserver<Repository>
    {
        private readonly IObservableGitHubClient _gitHubClient;

        public GithubObserver()
        {
            _gitHubClient = new ObservableGitHubClient(new ProductHeaderValue("noobquire"));
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Repository value)
        {
            value.
        }
    }
}
