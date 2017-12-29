using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class Credential
    {
        public string DoctorID { get; set; }
        public string Password { get; set; }


        public Credential()
        {

        }
    }
}