using System;
using System.Collections.Generic;
using SqliteDemo.Models.Entity;

namespace SqliteDemo.Models.Repository
{
    /*
     * This class manages CRUD (create, retrieve, update, delete) operations
     * for doctors.
     */
    public class DoctorPersistence
    {
        /*
         * Retrieve from the database the doctor matching the ISBN field of
         * the parameter.
         * Return null if the doctor can't be found.
         */
        public static Doctor getDoctor(Doctor keyDoctor)
        {
            string sqlQuery = "select * from doctor where doctorID=" + keyDoctor.doctorID;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Doctor.
            object[] dataRow = rows[0];
            Doctor doctor = new Doctor { doctorID = (string)dataRow[0], doctorName = (string)dataRow[1],
                doctorEmail = (string)dataRow[2], doctorPhone = (string)dataRow[3], salt = (string)dataRow[4],
                hashedPassword = (string)dataRow[5], doctorSex = (int)dataRow[6], IsAdmin = (int)dataRow[7],
                status = (int)dataRow[8]};
            return doctor;
        }

        /*
         * Add a Doctor to the database.
         * Return true iff the add succeeds.
         */
        public static bool AddDoctor(Doctor doctor)
        {
            string sql = "insert into doctor (doctorID, doctorName, doctorEmail, doctorPhone, salt, hashedPassword, " +
                "doctorSex, IsAdmin, status) values ('"
                + doctor.doctorID + "', "
                + doctor.doctorName + ", '"
                + doctor.doctorEmail + ", '"
                + doctor.doctorPhone + ", '"
                + doctor.salt + ", '"
                + doctor.hashedPassword + ", '"
                + doctor.doctorSex + ", '"
                + doctor.IsAdmin + ", '"
                + doctor.status + ", ')";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }
        public static bool DeleteDoctor(Doctor doctor)
        {
            string sql = "delete from doctor where doctorID =" + doctor.doctorID;
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        /*
         * Update a doctor that is in the database, replacing all field values except
         * the key field.
         * Return false if the doctor is not found, based on key field match.
         */
        public static bool ChangeDoctor(Doctor doctor)
        {
            string sql = "update doctor set doctorName = '" + doctor.doctorName + "' where doctorID =  " + doctor.doctorID;
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        /*
         * Get all Doctor data from the database and return an array of Doctors.
         */
        public static List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();

            string sqlQuery = "select * from doctor";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
                Doctor doctor = new Doctor
                {
                    doctorID = (string)dataRow[0],
                    doctorName = (string)dataRow[1],
                    doctorEmail = (string)dataRow[2],
                    doctorPhone = (string)dataRow[3],
                    salt = (string)dataRow[4],
                    hashedPassword = (string)dataRow[5],
                    doctorSex = (int)dataRow[6],
                    IsAdmin = (int)dataRow[7],
                    status = (int)dataRow[8],
                    
                };
                doctors.Add(doctor);
            }

            return doctors;
        }
    }
}