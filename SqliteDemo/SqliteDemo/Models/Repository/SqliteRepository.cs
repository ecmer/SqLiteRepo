using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

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
                string sql = "CREATE TABLE doctor (doctorID VARCHAR(50), doctorName VARCHAR(50), doctorEmail VARCHAR(50)," +
                    "doctorPhone VARCHAR(50), salt VARCHAR(50), hashedPassword VARCHAR(50)," +
                    "doctorSex INTEGER, IsAdmin INTEGER, status INTEGER, PRIMARY KEY(doctorID))";
                DoCommand(sql);
                /*
                sql = "insert into doctor(doctorID, doctorName, doctorEmail, doctorPhone," +
                    "salt, hashedPassword, doctorSex, IsAdmin, status) values "
                    + "('1234567', 'Deniz Merve Gunduz', 'dmerve.gunduz@gmail.com', '0123456789'," +
                    "'blablabla', '23456789sfdgh', 1, 0, 1)";
                DoCommand(sql);*/
            }

            return success;
        }
    }
}
