using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniService
{
    public class ReturnUserData
    {

        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        
        public string userType { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string is_login { get; set; }
        public string is_vehicle_added { get; set; }
        public string reg_id { get; set; }

        public string isError { get; set; }

        public string errorMessage { get; set; }
        public string nic { get; set; }

        public string avg_rating { get; set; }
        public string image { get; set; }
    
    }
}