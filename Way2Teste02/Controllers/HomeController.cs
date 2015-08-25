using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Way2Teste02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()//mudar para Buscar
        {
            return View();
        }

        public ActionResult Contact()//mudar para favoritos
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}