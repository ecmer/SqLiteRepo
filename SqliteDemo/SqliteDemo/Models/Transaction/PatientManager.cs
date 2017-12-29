using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Models.Transaction
{
    public class PatientManager
    {

        public static Patient[] GetAllPatients()
        {
            List<Patient> patients = PatientPersistence.GetAllPatients();
            if (patients != null)
            {
                return PatientPersistence.GetAllPatients().ToArray();
            }
            else
            {
                return (new Patient[0]);
            }
        }

        /*
         * Transaction: Add a new patient to the database
         * Returns true iff the new patient has a unique ISBN
         * and it was successfully added.
         */
        public static bool AddNewPatient(Patient newPatient)
        {
            // Verify that the patient doesn't already exist
            Patient oldPatient = PatientPersistence.GetPatient(newPatient);
            // oldPatient should be null, if this is a new patient
            if (oldPatient != null)
            {
                return false;
            }


            return PatientPersistence.AddPatient(newPatient);
        }

        /*
         * Transaction: Delete a patient from the database
         * Returns true iff the patient exists in the database and
         * it was successfully deleted.
         */
        public static bool DeletePatient(Patient delPatient)
        {
            Patient checkPatient = PatientPersistence.GetPatient(delPatient);
            if (checkPatient == null)
            {
                return false;
            }
            return PatientPersistence.DeletePatient(delPatient);
        }


        /*
         * Transaction: Update a patient in the database
         * Returns true iff the patient exists in the database and
         * it was successfully changed.
         */
        public static bool ChangePatient(Patient changePatient)
        {
            Patient checkPatient = PatientPersistence.GetPatient(changePatient);
            if (checkPatient == null)
            {
                return false;
            }

            return PatientPersistence.ChangePatient(changePatient);

        }

    }
}