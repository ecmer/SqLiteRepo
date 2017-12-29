using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            bool result = RepositoryManager.Repository.Initialize();
            return View();
        }

        /*
         * Handle a request for a listing of doctors.
         */
        public ActionResult List()
        {
            Doctor[] doctors = DoctorManager.GetAllDoctors();
            return View(doctors);
        }

        [HttpGet]
        public ActionResult DeleteDoctor()
        {
            return View(new Doctor());
        }
        [HttpPost]
        public ActionResult DeleteDoctor(Doctor delDoctor)
        {
            if (delDoctor.DoctorID == null || delDoctor.DoctorID.Length == 0)
            {
                ViewBag.message = "Error: An ID is required";
                return View(delDoctor);
            }
            bool result = DoctorManager.DeleteDoctor(delDoctor);
            if (result == false)
            {
                ViewBag.message = "Error: ID is wrong";
                return View(delDoctor);
            }
            ViewBag.message = "Doctor deleted";
            Doctor[] doctors = DoctorManager.GetAllDoctors();
            return View("List", doctors);
        }

        [HttpGet]
        public ActionResult ChangeDoctor()
        {
            return View(new Doctor());
        }
        [HttpPost]
        public ActionResult ChangeDoctor(Doctor changeDoctor)
        {
            bool result = DoctorManager.ChangeDoctor(changeDoctor);
            if (result == false)
            {
                ViewBag.message = "Error: Doctor not found";
                return View(changeDoctor);
            }
            ViewBag.message = "Doctor changed";
            Doctor[] doctors = DoctorManager.GetAllDoctors();
            return View("List", doctors);
        }
    }
}
