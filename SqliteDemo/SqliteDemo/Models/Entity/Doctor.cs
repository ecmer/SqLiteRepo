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
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
        public string DoctorSex { get; set; }
        public string IsAdmin { get; set; }
        public string Status { get; set; }

        /*
         * Default constructor - no initialization.
         */
        public Doctor()
        {
            DoctorID = "";
            DoctorName = "";
            DoctorEmail = "";
            Salt = "";
            HashedPassword = "";
            DoctorSex = "";
            IsAdmin = "";
            Status = "";
        }

        /*
         * Parameterized constructor
         */
        public Doctor(String doctorID, String doctorName, String doctorEmail, 
            String salt, String hashedPassword, String doctorSex,
            String isadmin, String status)
        {
            DoctorID = doctorID;
            DoctorName = doctorName;
            DoctorEmail = doctorEmail;
            Salt = salt;
            HashedPassword = hashedPassword;
            DoctorSex = doctorSex;
            IsAdmin = isadmin;
            Status = status;
            
        }
    }
}