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
    public class RegisterController : Controller
    {

        [HttpGet]
        public ActionResult Register()
        {
            bool result = RepositoryManager.Repository.Initialize();
            return View(new Doctor());
        }

        /*
         * Handle the POST request from the Add Doctor form. The form parameters
         * are encapsulated in a Doctor object.
         */
        [HttpPost]
        public ActionResult Register(Doctor newDoctor)
        {
            // Validate doctor data from the transaction
            if (newDoctor == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new Doctor());
            }
            if (newDoctor.DoctorID == null || newDoctor.DoctorID.Length == 0)
            {
                ViewBag.message = "Error: ID is required";
                return View(newDoctor);
            }
            if (newDoctor.Password == null || newDoctor.Password.Length == 0)
            {
                ViewBag.message = "Error: Name is required";
                return View(newDoctor);
            }

            // Add the doctor
            bool result = DoctorManager.AddNewDoctor(newDoctor);
            if (result)
            {
                ViewBag.message = "Doctor is added";
            }
            else
            {
                ViewBag.message = "That doctor could not be added";
            }

            Doctor[] doctors = DoctorManager.GetAllDoctors();
            return View("List", doctors);
        }
    }
}