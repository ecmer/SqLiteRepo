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
        public string doctorID { get; set; }
        public string doctorName { get; set; }
        public string doctorEmail { get; set; }
        public string doctorPhone { get; set; }
        public string salt { get; set; }
        public string hashedPassword { get; set; }
        public int doctorSex { get; set; }
        public int IsAdmin { get; set; }
        public int status { get; set; }

        /*
         * Default constructor - no initialization.
         */
        public Doctor()
        {
        }

        /*
         * Parameterized constructor
         */
        public Doctor(String DoctorID, String DoctorName, String DoctorEmail, 
            String DoctorPhone, String Salt, String HashedPassword, int DoctorSex,
            int Isadmin, int Status, DateTime DateAdded)
        {
            doctorID = DoctorID;
            doctorName = DoctorName;
            doctorEmail = DoctorEmail;
            doctorPhone = DoctorPhone;
            salt = Salt;
            hashedPassword = HashedPassword;
            doctorSex = DoctorSex;
            IsAdmin = Isadmin;
            status = Status;
            
        }
    }
}