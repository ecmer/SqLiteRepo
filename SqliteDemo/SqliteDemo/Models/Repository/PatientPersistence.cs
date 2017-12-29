using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Models.Repository
{
    public class PatientPersistence
    {
        /*
         * Retrieve from the database the patient matching the ISBN field of
         * the parameter.
         * Return null if the patient can't be found.
         */
        public static Patient GetPatient(Patient keyPatient)
        {
            string sqlQuery = "select * from patient where PatientID=" + keyPatient.PatientID;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Patient.
            object[] dataRow = rows[0];
            Patient patient = new Patient
            {
                PatientID = (string)dataRow[0],
                DoctorID = (string)dataRow[1],
                PatientName = (string)dataRow[2],
                PatientEmail = (string)dataRow[3],
                PatientPhone = (string)dataRow[4],
                PatientSex = (string)dataRow[5],
                PatientAge = (string)dataRow[6],
                HealthProblem = (string)dataRow[7]
            };
            return patient;
        }

        /*
         * Add a Patient to the database.
         * Return true iff the add succeeds.
         */
        public static bool AddPatient(Patient patient)
        {
            string sql = "insert into patient (PatientID, DoctorID, PatientName, PatientEmail, PatientPhone, " +
                "PatientSex, PatientAge, HealthProblem) values ('"
                + patient.PatientID + "', '"
                + patient.DoctorID + "', '"
                + patient.PatientName + "', '"
                + patient.PatientEmail + "', '"
                + patient.PatientPhone + "', '"
                + patient.PatientSex + "', '"
                + patient.PatientAge + "', '"
                + patient.HealthProblem + "')";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }
        public static bool DeletePatient(Patient patient)
        {
            string sql = "delete from patient where PatientID =" + patient.PatientID;
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        /*
         * Update a patient that is in the database, replacing all field values except
         * the key field.
         * Return false if the patient is not found, based on key field match.
         */
        public static bool ChangePatient(Patient patient)
        {
            string sql = "update patient set PatientName = '" + patient.PatientName + "' where PatientID =  " + patient.PatientID;
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        /*
         * Get all Patient data from the database and return an array of Patients.
         */
        public static List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            string sqlQuery = "select * from patient";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {

                Patient patient = new Patient
                {
                    PatientID = (string)dataRow[0],
                    DoctorID = (string)dataRow[1],
                    PatientName = (string)dataRow[2],
                    PatientEmail = (string)dataRow[3],
                    PatientPhone = (string)dataRow[4],
                    PatientSex = (string)dataRow[5],
                    PatientAge = (string)dataRow[6],
                    HealthProblem = (string)dataRow[7]

                };
                patients.Add(patient);
            }

            return patients;
        }

    }
}