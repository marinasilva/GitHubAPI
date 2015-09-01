using Microsoft.VisualStudio.TestTools.UnitTesting;
using Way2Teste02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Way2Teste02.Models.Tests
{
    [TestClass()]
    public class GitHubApiCallsTests
    {
        [TestMethod()]
        public void GetUserReposTest()
        {
            string url = "https://api.github.com/users/marinasilva/repos";
            string response = HttpUtils.SendStringToUrl(url);
            if (!response.Contains("marinasilva/ADPopulationSolution"))
            {
                Assert.Fail("O teste falhou, pois o JSON retornado não contém as informações corretas");
            }
        }

        [TestMethod()]
        public void SearchGitHubRepositoriesTest()
        {
            string parameter = "BusRoute";
            GitHubApiCalls apiCalls = new GitHubApiCalls();
            List<GitHubRepository> repositories = apiCalls.SearchGitHubRepositories(parameter);
            if (repositories.Count == 0)
            {
                Assert.Fail("O teste falhou pois não retornou nenhum repositório com o filtro!");
            }
        }

        [TestMethod()]
        public void GetOrgReposTest()
        {
            string parameter = "autodesk";
            GitHubApiCalls apiCalls = new GitHubApiCalls();
            List<GitHubRepository> repositories = apiCalls.GetOrgRepos(parameter);
            if (repositories.Count == 0)
            {
                Assert.Fail("O teste falhou pois não retornou nenhum repositório para a organização!");
            }
        }

        [TestMethod()]
        public void GetUserRepoTest()
        {
            string gitHubUserName = "marinasilva";
            string projectName = "busroute";
            GitHubApiCalls apiCalls = new GitHubApiCalls();
            GitHubRepository repository = apiCalls.GetUserRepo(gitHubUserName, projectName);
            if (repository.Id != 24512477)
            {
                Assert.Fail("O teste falhou pois não retornou o repositório correto!");
            }
        }

        [TestMethod()]
        public void GetUserRepoContributorsTest()
        {
            string gitHubUserName = "marinasilva";
            string projectName = "MSDS-Helper";
            GitHubApiCalls apiCalls = new GitHubApiCalls();
            List<GithubUser> githubUsers = apiCalls.GetUserRepoContributors(gitHubUserName, projectName);
            if (githubUsers.Count != 2)
            {
                Assert.Fail("O teste falhou pois não retornou corretamente os usuários contribuidores do projeto!");
            }
        }

        
    }
}