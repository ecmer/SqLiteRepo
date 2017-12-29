using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using SqliteDemo.Models.Transaction;
using System.Linq;
using System.Web;

namespace SqliteTest.Models.Repository
{
    /*
     * This class creates and accesses an SQLite database.
     */
    public class SqliteRepository : IRepository
    {
        // Location of the database file 
        private string databaseFile = "C:\\Users\\denizmerve\\Database.sqlite";

        private SQLiteConnection dbConnection;

        public bool IsOpen { get { return isOpen; } }
        private bool isOpen = false;

        /*
         * When the Repository shuts down, it should close the DB if it's open.
         */
        ~SqliteRepository()
        {
            if (IsOpen)
            {
                Close();
            }
        }

        /*
         * Open the database. Return true iff the open succeeds, or it was
         * already open.
         */
        public bool Open()
        {
            if (IsOpen)
            {
                return true;
            }
            dbConnection =
                new SQLiteConnection("Data Source=" + databaseFile + ";Version=3;");
            if (dbConnection == null) { return false; }
            dbConnection.Open();
            isOpen = true;
            return true;
        }

        /*
         * Close the database, if it's open.
         */
        public void Close()
        {
            if (!IsOpen)
            {
                return;
            }
            isOpen = false;
            dbConnection.Close();
        }

        /*
         * Execute an SQL command. 
         * The return value is the number of rows affected by the command.
         */
        public int DoCommand(string sqlCommand)
        {
            if (!IsOpen)
            {
                return -1;
            }
            SQLiteCommand command = new SQLiteCommand(sqlCommand, dbConnection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        /*
         * Execute an SQL query. 
         * The return value is a List of object arrays, in which each array 
         * represents one row of data returned.
         */
        public List<object[]> DoQuery(string sqlQuery)
        {
            if (!IsOpen)
            {
                return null;
            }
            List<object[]> rows = new List<object[]>();
            SQLiteCommand command = new SQLiteCommand(sqlQuery, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object[] row = new object[reader.FieldCount];
                reader.GetValues(row);
                rows.Add(row);
            }
            return rows;
        }

        /*
         * Recreate and reinitialize the database.
         * The return value is true iff the initialization succeeds.
         */
        public bool Initialize()
        {
            bool success = true;

            Close();

            try
            {
                SQLiteConnection.CreateFile(databaseFile);
            }
            catch (IOException e)
            {
                success = false;
            }

            bool openResult = Open();
            if (success & openResult)
            {
                string salt = EncryptionManager.PasswordSalt;
                string HashedPassword = EncryptionManager.EncodePassword("deneme1234", salt);

                string sql = "CREATE TABLE doctor (doctorID VARCHAR(50), doctorName VARCHAR(50), doctorEmail VARCHAR(50)," +
                    "password VARCHAR(50), salt VARCHAR(50), hashedPassword VARCHAR(50)," +
                    "doctorSex VARCHAR(50), isadmin VARCHAR(50), status VARCHAR(50), PRIMARY KEY(doctorID))";
                DoCommand(sql);
                
                sql = "insert into doctor(doctorID, doctorName, doctorEmail, password," +
                    "salt, hashedPassword, doctorSex, isadmin, status) values "
                    + "('1234567', 'Deniz Merve Gunduz', 'dmerve.gunduz@gmail.com', 'pass', '"+salt+"', '"+HashedPassword+"', '1', '1', '1')";
                DoCommand(sql);

                string sql1 = "CREATE TABLE patient (patientID VARCHAR(50), doctorID VARCHAR(50), patientName VARCHAR(50), patientEmail VARCHAR(50)," +
                    "patientPhone VARCHAR(50), patientSex VARCHAR(50)," +
                    "patientAge VARCHAR(50), healthProblem VARCHAR(50), PRIMARY KEY(patientID))";
                DoCommand(sql1);

                sql1 = "insert into patient(patientID, doctorID, patientName, patientEmail," +
                    "patientPhone, patientSex, patientAge, healthProblem) values "
                    + "('1234567', '1', 'Deniz', 'dmerve.gunduz@gmail.com', '050666666666', 'female', '22', 'Crazy')";
                DoCommand(sql1);
            }

            return success;
        }
    }
}
