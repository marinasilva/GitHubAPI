using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace Way2Teste02.Models
{
    public class GitHubApiCalls
    {
        public const string GithubApiBaseUrl = "https://api.github.com/";

        public T GetJson<T>(string route, params object[] routeArgs)
        {
            return GithubApiBaseUrl.AppendPath(route.Fmt(routeArgs))
                .GetJsonFromUrl()
                .FromJson<T>();
        }

        public List<GitHubRepository> GetUserRepos(string gitHubUsername)
        {
            return GetJson<List<GitHubRepository>>("users/{0}/repos", gitHubUsername);
        }

        public List<GitHubRepository> GetOrgRepos(string GitHubOrgName)
        {
            return GetJson<List<GitHubRepository>>("orgs/{0}/repos", GitHubOrgName);
        }

        public GitHubRepository GetUserRepo(string gitHubUsername, string projectName)
        {
            return GetJson<GitHubRepository>("repos/{0}/{1}", gitHubUsername, projectName);
        }

        public List<GitHubUser> GetUserRepoContributors(string gitHubUsername, string projectName)
        {
            return GetJson<List<GitHubUser>>("repos/{0}/{1}/contributors", gitHubUsername, projectName);
        }

        public List<GitHubUser> GetUserRepoWatchers(string gitHubUsername, string projectName)
        {
            return GetJson<List<GitHubUser>>("repos/{0}/{1}/watchers", gitHubUsername, projectName);
        }

        public List<GitHubRepository> GetReposUserIsWatching(string gitHubUsername)
        {
            return GetJson<List<GitHubRepository>>("users/{0}/watched", gitHubUsername);
        }

        public List<GitHubOrg> GetUserOrgs(string gitHubUsername)
        {
            return GetJson<List<GitHubOrg>>("users/{0}/orgs", gitHubUsername);
        }

        public List<GitHubUser> GetUserFollowers(string gitHubUsername)
        {
            return GetJson<List<GitHubUser>>("users/{0}/followers", gitHubUsername);
        }

        public List<GitHubUser> GetOrgMembers(string GitHubOrgName)
        {
            return GetJson<List<GitHubUser>>("orgs/{0}/members", GitHubOrgName);
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