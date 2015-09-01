using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace Way2Teste02.Models
{
    public class GitHubApiCalls
    {
        private const string GithubApiBaseUrl = "https://api.github.com/";

        private T GetJson<T>(string route, params object[] routeArgs)
        {
            return GithubApiBaseUrl.AppendPath(route.Fmt(routeArgs))
                .GetJsonFromUrl()
                .FromJson<T>();
        }

        public List<GitHubRepository> GetUserRepos(string gitHubUsername)
        {
            return GetJson<List<GitHubRepository>>("users/{0}/repos", gitHubUsername);
        }

        public List<GitHubRepository> SearchGitHubRepositories(string query)
        {
            return GetJson<List<GitHubRepository>>("search/repositories?q={0}",query);
        } 

        public List<GitHubRepository> GetOrgRepos(string gitHubOrgName)
        {
            return GetJson<List<GitHubRepository>>("orgs/{0}/repos", gitHubOrgName);
        }

        public GitHubRepository GetUserRepo(string gitHubUsername, string projectName)
        {
            return GetJson<GitHubRepository>("repos/{0}/{1}", gitHubUsername, projectName);
        }

        public List<GithubUser> GetUserRepoContributors(string gitHubUsername, string projectName)
        {
            return GetJson<List<GithubUser>>("repos/{0}/{1}/contributors", gitHubUsername, projectName);
        }

        public List<GithubUser> GetUserRepoWatchers(string gitHubUsername, string projectName)
        {
            return GetJson<List<GithubUser>>("repos/{0}/{1}/watchers", gitHubUsername, projectName);
        }

        public List<GitHubRepository> GetReposUserIsWatching(string gitHubUsername)
        {
            return GetJson<List<GitHubRepository>>("users/{0}/watched", gitHubUsername);
        }

        public List<GitHubOrg> GetUserOrgs(string gitHubUsername)
        {
            return GetJson<List<GitHubOrg>>("users/{0}/orgs", gitHubUsername);
        }

        public List<GithubUser> GetUserFollowers(string gitHubUsername)
        {
            return GetJson<List<GithubUser>>("users/{0}/followers", gitHubUsername);
        }

        public List<GithubUser> GetOrgMembers(string gitHubOrgName)
        {
            return GetJson<List<GithubUser>>("orgs/{0}/members", gitHubOrgName);
        }

        public List<GitHubRepository> GetAllUserAndOrgsReposFor(string gitHubUsername)
        {
            var map = new Dictionary<int, GitHubRepository>();
            GetUserRepos(gitHubUsername).ForEach(x => map[x.Id] = x);
            GetUserOrgs(gitHubUsername).ForEach(org =>
                GetOrgRepos(org.Login)
                    .ForEach(repo => map[repo.Id] = repo));

            return map.Values.ToList();
        }
    }
}