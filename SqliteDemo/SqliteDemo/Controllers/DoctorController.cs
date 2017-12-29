using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;

namespace SqliteTest.Controllers
{
    /*
     * This Controller handles requests related to Doctors.
     */
    public class DoctorController : Controller
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

        /*
		 * Handle a GET request for the Add Doctor form.
         */
        [HttpGet]
        public ActionResult AddDoctor()
        {
            return View(new Doctor());
        }

        /*
         * Handle the POST request from the Add Doctor form. The form parameters
         * are encapsulated in a Doctor object.
         */
        [HttpPost]
        public ActionResult AddDoctor(Doctor newDoctor)
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