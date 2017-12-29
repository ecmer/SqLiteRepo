using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SqliteDemo.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return RedirectToAction("AddDoctor","Doctor");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(String DoctorID, String DoctorName, String DoctorEmail, String Password,
            String DoctorSex)
        {
            return View();
        }

    }
}