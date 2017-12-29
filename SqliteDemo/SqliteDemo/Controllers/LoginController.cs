using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;


namespace SqliteDemo.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Credential credent = new Credential();
            return View(credent);
        }

        [HttpPost]
        public ActionResult Index(Credential credent)
        {
            if (credent == null)
            {
                return View(credent);
            }
            if (credent.DoctorID == null || credent.DoctorID.Length == 0)
            {
                TempData["message"] = "Both UserId and Password are required";
                return View(credent);
            }
            if (credent.Password == null || credent.Password.Length == 0)
            {
                TempData["message"] = "Both UserId and Password are required";
                return View(credent);
            }

            bool resultOfAuthenticate = DoctorManager.AuthenticateDoctor(credent, Session);

            if (resultOfAuthenticate == true)
            {
                TempData["message"] = "Login Successful";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "Invalid login credentials";
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {

            DoctorManager.LogoutDoctor(Session);
            return RedirectToAction("Index", "Home");
        }

    }
}