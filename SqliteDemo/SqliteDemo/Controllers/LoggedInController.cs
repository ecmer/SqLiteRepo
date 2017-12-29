using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Controllers
{
    public class LoggedInController : Controller
    {
        public ActionResult Admin()
        {
            if (this.Session["LoggedIn"] == null)
            {
                TempData["message"] = "You should login!";
                return RedirectToAction("Index", "Login");
            }

            if ((bool)this.Session["IsAdmin"])
            {
                Doctor[] doctors = DoctorManager.GetAllDoctors();
                return RedirectToAction("Admin", "LoggedIn");
            }
            else
            {
                return RedirectToAction("Doctor","LoggedIn");
            }
            return RedirectToAction("Admin", "LoggedIn");
        }

    }
}