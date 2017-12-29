using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class Patient
    {
        public string PatientID { get; set; }
        public string DoctorID { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public string PatientSex { get; set; }
        public string PatientAge { get; set; }
        public string HealthProblem { get; set; }

        /*
         * Default constructor - no initialization.
         */
        public Patient()
        {
            PatientID = "";
            DoctorID = "";
            PatientName = "";
            PatientEmail = "";
            PatientPhone = "";
            PatientSex = "";
            PatientAge = "";
            HealthProblem = "";
        }

        /*
         * Parameterized constructor
         */
        public Patient(String patientID, String doctorID, String patientName,
            String patientEmail, String patientPhone, String patientSex,
            String patientAge, String healthProblem)
        {
            PatientID = patientID;
            DoctorID = doctorID;
            PatientName = patientName;
            PatientEmail = patientEmail;
            PatientPhone = patientPhone;
            PatientSex = patientSex;
            PatientAge = patientAge;
            HealthProblem = healthProblem;

        }

    }
}