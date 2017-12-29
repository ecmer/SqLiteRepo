using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Controllers
{
    public class PatientController : Controller
    {
        /*
         * Handle a request for a listing of patients.
         */
        public ActionResult List()
        {
            Patient[] patients = PatientManager.GetAllPatients();
            return View(patients);
        }

        /*
		 * Handle a GET request for the Add Patient form.
         */
        [HttpGet]
        public ActionResult AddPatient()
        {
            return View(new Patient());
        }

        /*
         * Handle the POST request from the Add Patient form. The form parameters
         * are encapsulated in a Patient object.
         */
        [HttpPost]
        public ActionResult AddPatient(Patient newPatient)
        {
            // Validate patient data from the transaction
            if (newPatient == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new Patient());
            }
            if (newPatient.PatientID == null || newPatient.PatientID.Length == 0)
            {
                ViewBag.message = "Error: ID is required";
                return View(newPatient);
            }
            if (newPatient.PatientName == null || newPatient.PatientName.Length == 0)
            {
                ViewBag.message = "Error: Name is required";
                return View(newPatient);
            }

            // Add the patient
            bool result = PatientManager.AddNewPatient(newPatient);
            if (result)
            {
                ViewBag.message = "Patient is added";
            }
            else
            {
                ViewBag.message = "That patient could not be added";
            }

            Patient[] patients = PatientManager.GetAllPatients();
            return View("List", patients);
        }

        [HttpGet]
        public ActionResult DeletePatient()
        {
            return View(new Patient());
        }
        [HttpPost]
        public ActionResult DeletePatient(Patient delPatient)
        {
            if (delPatient.PatientID == null || delPatient.PatientID.Length == 0)
            {
                ViewBag.message = "Error: An ID is required";
                return View(delPatient);
            }
            bool result = PatientManager.DeletePatient(delPatient);
            if (result == false)
            {
                ViewBag.message = "Error: ID is wrong";
                return View(delPatient);
            }
            ViewBag.message = "Patient deleted";
            Patient[] patients = PatientManager.GetAllPatients();
            return View("List", patients);
        }

        [HttpGet]
        public ActionResult ChangePatient()
        {
            return View(new Patient());
        }
        [HttpPost]
        public ActionResult ChangePatient(Patient changePatient)
        {
            bool result = PatientManager.ChangePatient(changePatient);
            if (result == false)
            {
                ViewBag.message = "Error: Patient not found";
                return View(changePatient);
            }
            ViewBag.message = "Patient changed";
            Patient[] patients = PatientManager.GetAllPatients();
            return View("List", patients);
        }
    }
}