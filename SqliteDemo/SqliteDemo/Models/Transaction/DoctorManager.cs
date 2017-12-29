using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Models.Transaction
{
    public class DoctorManager
    {
        public static bool AuthenticateDoctor(Credential credent, HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;

            Doctor doc = DoctorPersistence.GetDoctorByID(credent.DoctorID);
            if (doc == null)
            {
                return false;
            }

            if(doc.DoctorID == null)
            {
                return false;
            }

            else
            {
                string HashedPassword = EncryptionManager.EncodePassword(credent.Password, doc.Salt);

                if (HashedPassword == doc.HashedPassword)
                {
                    session["LoggedIn"] = true;
                    session["IsAdmin"] = doc.IsAdmin;
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        public static void LogoutDoctor(HttpSessionStateBase session)
        {
            session["LoggedIn"] = null;
            session["IsAdmin"] = false;
        }

        public static Doctor[] GetAllDoctors()
        {
            List<Doctor> doctors = DoctorPersistence.GetAllDoctors();
            if (doctors != null)
            {
                return DoctorPersistence.GetAllDoctors().ToArray();
            }
            else
            {
                return (new Doctor[0]);
            }
        }

        /*
         * Transaction: Add a new doctor to the database
         * Returns true iff the new doctor has a unique ISBN
         * and it was successfully added.
         */
        public static bool AddNewDoctor(Doctor newDoctor)
        {
            // Verify that the doctor doesn't already exist
            Doctor oldDoctor = DoctorPersistence.GetDoctor(newDoctor);
            // oldDoctor should be null, if this is a new doctor
            if (oldDoctor != null)
            {
                return false;
            }
            

            return DoctorPersistence.AddDoctor(newDoctor);
        }

        /*
         * Transaction: Delete a doctor from the database
         * Returns true iff the doctor exists in the database and
         * it was successfully deleted.
         */
        public static bool DeleteDoctor(Doctor delDoctor)
        {
            Doctor checkDoctor = DoctorPersistence.GetDoctor(delDoctor);
            if (checkDoctor == null)
            {
                return false;
            }
            return DoctorPersistence.DeleteDoctor(delDoctor);
        }


        /*
         * Transaction: Update a doctor in the database
         * Returns true iff the doctor exists in the database and
         * it was successfully changed.
         */
        public static bool ChangeDoctor(Doctor changeDoctor)
        {
            Doctor checkDoctor = DoctorPersistence.GetDoctor(changeDoctor);
            if (checkDoctor == null)
            {
                return false;
            }

            return DoctorPersistence.ChangeDoctor(changeDoctor);

        }
    }
}