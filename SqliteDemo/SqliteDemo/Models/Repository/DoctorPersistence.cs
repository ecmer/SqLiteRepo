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
            string sqlQuery = "select * from doctor where DoctorID=" + keyDoctor.DoctorID;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Doctor.
            object[] dataRow = rows[0];
            Doctor doctor = new Doctor { DoctorID = (string)dataRow[0], DoctorName = (string)dataRow[1],
                DoctorEmail = (string)dataRow[2], Salt = (string)dataRow[3],
                HashedPassword = (string)dataRow[4], DoctorSex = (string)dataRow[5], IsAdmin = (string)dataRow[6],
                Status = (string)dataRow[7]};
            return doctor;
        }

        /*
         * Add a Doctor to the database.
         * Return true iff the add succeeds.
         */
        public static bool AddDoctor(Doctor doctor)
        {
            string sql = "insert into doctor (DoctorID, DoctorName, DoctorEmail, Salt, HashedPassword, " +
                "DoctorSex, Isadmin, Status) values ('"
                + doctor.DoctorID + "', '"
                + doctor.DoctorName + "', '"
                + doctor.DoctorEmail + "', '"
                + doctor.Salt + "', '"
                + doctor.HashedPassword + "', '"
                + doctor.DoctorSex + "', '"
                + doctor.IsAdmin + "', '"
                + doctor.Status + "')";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }
        public static bool DeleteDoctor(Doctor doctor)
        {
            string sql = "delete from doctor where DoctorID =" + doctor.DoctorID;
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
            string sql = "update doctor set DoctorName = '" + doctor.DoctorName + "' where DoctorID =  " + doctor.DoctorID;
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
                    DoctorID = (string)dataRow[0],
                    DoctorName = (string)dataRow[1],
                    DoctorEmail = (string)dataRow[2],
                    Salt = (string)dataRow[3],
                    HashedPassword = (string)dataRow[4],
                    DoctorSex = (string)dataRow[5],
                    IsAdmin = (string)dataRow[6],
                    Status = (string)dataRow[7]
                    
                };
                doctors.Add(doctor);
            }

            return doctors;
        }
    }
}