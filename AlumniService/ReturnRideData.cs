﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlumniService
{
    public class ReturnRideData
    {
        public string id { get; set; }
        public string passengerID { get; set; }
        public string driverID { get; set; }
        public string from_destination { get; set; }
        public string to_destination { get; set; }

        public string from_lat { get; set; }

        public string from_lng { get; set; }
    
        public string  to_lat{ get; set; }

        public string to_lng { get; set; }

        public string status { get; set; }

        public string message { get; set; }

        public string amount { get; set; }

        public string review { get; set; }

        public string rating { get; set; }

        public string driver_name { get; set; }

        public string passenger_name { get; set; }


    }
}