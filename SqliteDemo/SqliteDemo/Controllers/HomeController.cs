using System;
using System.Web.Mvc;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Controllers
{
    /*
     * This Controller presents the default view, and handles the request
     * for database initialization.
     */
    public class HomeController : Controller
    {
        /*
         * View Home Page
         */
        public ActionResult Index()
        {
            bool result = RepositoryManager.Repository.Initialize();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
    }
}