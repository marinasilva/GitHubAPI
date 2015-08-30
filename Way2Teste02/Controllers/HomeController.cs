using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Way2Teste02.Models;

namespace Way2Teste02.Controllers
{
    public class HomeController : Controller
    {
        
        private static List<GitHubRepository> _gitHubRepositories; 
        public ActionResult Index()
        {
            GitHubApiCalls gitHubApiCalls = new GitHubApiCalls();
            //-----------------------
            //Aqui é chamado a classe que retorna uma coleção de objeto do tipo repositorio. 
            //-----------------------
            _gitHubRepositories = gitHubApiCalls.GetUserRepos("luisfernandomoraes");
            
            return View(_gitHubRepositories);
        }

        public ActionResult About()//mudar para Buscar
        {
            return View();
        }

        public ActionResult Contact()//mudar para favoritos
        {
            return View();
        }

        public ActionResult Details(int idRepository)
        {
            var repository = _gitHubRepositories.First(githubRepository => githubRepository.Id == idRepository);
            return View(repository);
        }
    }
}