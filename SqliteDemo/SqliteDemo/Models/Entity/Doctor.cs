using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    /*
     * This class represents one Book.
     */
    public class Doctor
    {
        public string DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string DoctorSex { get; set; }
        public bool IsAdmin { get; set; }
        public bool Status { get; set; }

        /*
         * Default constructor - no initialization.
         */
        public Doctor()
        {
            DoctorID = "";
            DoctorName = "";
            DoctorEmail = "";
            Password = "";
            Salt = "";
            HashedPassword = "";
            DoctorSex = "";
        }

        /*
         * Parameterized constructor
         */
        public Doctor(String doctorID, String doctorName, String doctorEmail, String password,
            String salt, String hashedPassword, String doctorSex,
            bool isadmin, bool status)
        {
            DoctorID = doctorID;
            DoctorName = doctorName;
            DoctorEmail = doctorEmail;
            Password = password;
            Salt = salt;
            HashedPassword = hashedPassword;
            DoctorSex = doctorSex;
            IsAdmin = isadmin;
            Status = status;
            
        }
    }
}