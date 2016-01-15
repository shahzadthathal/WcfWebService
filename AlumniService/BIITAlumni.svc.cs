using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace AlumniService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BIITAlumni : IBIITAlumni
    {
        vehicle_navigationEntities db = new vehicle_navigationEntities();
      

       public ReturnUserData Authenticate(string email, string password)
       {   
           ReturnUserData returnUserData;
           var usr = db.users.Where(u => u.email == email && u.password == password).FirstOrDefault();
           usr.is_login = 1;
           db.SaveChanges();
           if (usr != null)
           {
               returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = usr.id.ToString(), name = usr.name, email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, lat = usr.lat.ToString(), lng = usr.lng.ToString(), is_login = usr.is_login.ToString(), is_vehicle_added = usr.is_vehicle_added.ToString(), reg_id = "asdfa1234asdfdf", nic = "5555" };
           }
           else
           {
               returnUserData = new ReturnUserData { isError = "1", errorMessage = "Invalid credentinals", id = "", name = "", email = "", password = "", phone = "", userType = "", street = "", city = "", country = "", lat = "", lng = "", is_login = "", is_vehicle_added = "", reg_id = "", nic = "" };
           }

           return returnUserData;
       }

        
       public bool Logout(string userid) 
       {
           int uid = Convert.ToInt32(userid);
           var usr = db.users.Where(u => u.id == uid).FirstOrDefault();
           usr.is_login = 0;
           db.SaveChanges();
           return true;
       }


       public ReturnUserData Register(string name, string email, string password, string phone, string nic, string userType)
       {
           //int? login = 1;
           //int? vehicleadded = 0;
           //int? isavailable = 0;


           user usr = new user();
           usr.name = System.Web.HttpUtility.UrlDecode(name);
           usr.email = email;
           usr.password = System.Web.HttpUtility.UrlDecode(password);
           usr.phone = phone;
           usr.nic = nic;
           usr.userType = userType;
           usr.is_login = 1;
           usr.is_vehicle_added = 0;
           usr.is_available = 0;
           db.users.Add(usr);
           db.SaveChanges();
           string uid = usr.id.ToString();
           ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = uid, name = usr.name, email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, lat = "333.5555", lng = "79.5554", is_login = usr.is_login.ToString(), is_vehicle_added = usr.is_vehicle_added.ToString(), reg_id = "asdfa1234asdfdf", nic = "5555" };
           return returnUserData;
           // return usr;
       }

       public ReturnUserData SaveAddress(string id, string street, string city, string country, string lat, string lng)
       {
           int userid = Convert.ToInt32(id);
           var usr = (from c in db.users where c.id == userid select c).FirstOrDefault();
           usr.id = userid;
           usr.street = System.Web.HttpUtility.UrlDecode(street);
           usr.city = System.Web.HttpUtility.UrlDecode(city);
           usr.country = System.Web.HttpUtility.UrlDecode(country);
           usr.lat = Convert.ToDouble(lat);
           usr.lng = Convert.ToDouble(lng);
           db.SaveChanges();
           // return u;
           string uid = usr.id.ToString();
           ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = uid, name = usr.name, email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, lat = usr.lat.ToString(), lng =usr.lng.ToString(), is_login = usr.is_login.ToString(), is_vehicle_added = usr.is_vehicle_added.ToString(), reg_id = "asdfa1234asdfdf", nic = "5555" };
          // ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = uid, name = "ShahzadAhmed", email = "shahzad@app.com", password = "123456", phone = "3016973691", userType = "Passenger", street = "street104", city = "Isb", country = "Paksitan", lat = "33.5555", lng = "79.5554", is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf", nic = "5555" };
           return returnUserData;
       }

       public ReturnVehicleData SaveVehicle(string name, string modelName, string manufacturer_name, string ownerId) 
       {
               int userid = Convert.ToInt32(ownerId);

               //int? available = 1;
               //int? vehicle_added = 1;

               var usr = (from c in db.users where c.id == userid select c).FirstOrDefault();

               vehicle v = new vehicle();
               v.name = System.Web.HttpUtility.UrlDecode(name);
               v.model_name = System.Web.HttpUtility.UrlDecode(modelName);
               v.manufacturer_name = System.Web.HttpUtility.UrlDecode(manufacturer_name);
               v.ownerId = userid;
               db.vehicles.Add(v);
               db.SaveChanges();

               usr.is_available = 1;
               usr.is_vehicle_added = 1;
               db.SaveChanges();

               string uid = v.ownerId.ToString();
               ReturnVehicleData returnVehicleData = new ReturnVehicleData { id = v.id.ToString(), name = v.name, model_name = v.model_name, manufacturer_name = v.manufacturer_name, ownerId = uid };
               return returnVehicleData;
           //}
       }


       public ReturnVehicleData UpdateVehicle(string vid, string name, string modelName, string manufacturer_name, string ownerId)
       {
           int id = Convert.ToInt32(vid);
           var vm = (from c in db.vehicles where c.id == id select c).FirstOrDefault(); //AND c.ownerId = userid
           vm.name = System.Web.HttpUtility.UrlDecode(name);
           vm.model_name = System.Web.HttpUtility.UrlDecode(modelName);
           vm.manufacturer_name = System.Web.HttpUtility.UrlDecode(manufacturer_name);
           db.SaveChanges();
           ReturnVehicleData returnVehicleData = new ReturnVehicleData { id = vm.id.ToString(), name = vm.name, model_name = vm.model_name, manufacturer_name = vm.manufacturer_name, ownerId = vm.ownerId.ToString() };
           return returnVehicleData;
       }


       public ReturnVehicleData GetVehicleDetail(string userid)
       {
           int ownerId = Convert.ToInt32(userid);
           var vm = db.vehicles.Where(v => v.ownerId == ownerId).FirstOrDefault();          
           ReturnVehicleData returnVehicleData = new ReturnVehicleData { id = vm.id.ToString(), name = vm.name, model_name = vm.model_name, manufacturer_name = vm.manufacturer_name, ownerId = vm.ownerId.ToString() };
           return returnVehicleData;
       }


       public List<Driver> GetDrivers(string fromlat, string fromlng)
       {
           List<Driver> drivers = new List<Driver>();
           //decimal? lat= Convert.ToDecimal(33.5);
           //decimal? lng = Convert.ToDecimal(73.5);

           decimal? lat = Convert.ToDecimal(fromlat);
           decimal? lng = Convert.ToDecimal(fromlng);

           int? dist= 2;
           var result= db.GetNearestDrivers(lat,lng,dist);
           foreach (var r in result)
           {
               string did = r.id.ToString();
               decimal distance = Math.Round((Decimal)r.distance, 2);
               string ddistance = distance.ToString();
               drivers.Add(new Driver { id = did, name = r.name, phone = r.phone, distance = ddistance });
           }
          return drivers;
       }

       public ReturnRideData AddRide(string passengerID, string driverID, string from_destination, string to_destination, string from_lat, string from_lng, string to_lat, string to_lng)
       {
           ReturnRideData returnRideData;

           ride_detail rd = new ride_detail();

           int pID = Convert.ToInt32(passengerID); // passenger
           int dID = Convert.ToInt32(driverID); //driver
           rd.passengerID = pID;
           rd.driverID = dID;
           rd.from_destination = System.Web.HttpUtility.UrlDecode(from_destination);
           rd.to_destination = System.Web.HttpUtility.UrlDecode(to_destination);
           rd.from_lat = Convert.ToDouble(from_lat);
           rd.from_lng = Convert.ToDouble(from_lng);
           rd.to_lat = Convert.ToDouble(to_lat);
           rd.to_lng = Convert.ToDouble(to_lng);
           //rd.created_date = DateTime();
           //rd.status = 0;
           db.ride_detail.Add(rd);
           db.SaveChanges();

           var usr = db.users.Where(v => v.id == rd.driverID).FirstOrDefault();
           usr.is_available = 0;
           db.SaveChanges();
          
           string rid = rd.id.ToString(); //ride id
           string rpid = rd.passengerID.ToString(); // passengerID
           string rdid = rd.driverID.ToString(); //driverID
           returnRideData = new ReturnRideData { id = rid, passengerID = rpid, driverID = rdid, from_destination = rd.from_destination, to_destination = rd.to_destination, from_lat = rd.from_lat.ToString(), from_lng = rd.from_lng.ToString(), to_lat = rd.to_lat.ToString(), to_lng = rd.to_lng.ToString(), status = rd.status.ToString() };
           return returnRideData;

       }

       public ReturnRideData CheckNewRide(string userId, string userType) 
       {
           ReturnRideData returnRideData;
           int dID = Convert.ToInt32(userId); //driver
           var ride = db.ride_detail.Where(r => r.driverID == dID && r.status == 0).FirstOrDefault();
           if (ride != null)
           {
               returnRideData = new ReturnRideData { id = ride.id.ToString(), passengerID = ride.passengerID.ToString(), driverID = ride.driverID.ToString(), from_destination = ride.from_destination, to_destination = ride.to_destination, from_lat = ride.from_lat.ToString(), from_lng = ride.from_lng.ToString(), to_lat = ride.to_lat.ToString(), to_lng = ride.to_lng.ToString(), status = ride.status.ToString() };
           }
           else {
               returnRideData = null;
           }
           
           return returnRideData;
       }

       public ReturnRideData ChangeRideStatus(string rideId, string userId, string status) 
       {
           ReturnRideData returnRideData;
           int rID = Convert.ToInt32(rideId); //rideid
           int dID = Convert.ToInt32(userId); //driver
           int rstatus = Convert.ToInt32(status); //driver

           var ride = (from r in db.ride_detail where r.id == rID && r.driverID == dID select r).FirstOrDefault(); //AND c.ownerId = userid
           ride.status = rstatus;
           db.SaveChanges();

           //var ride = db.ride_detail.Where(r => r.id == dID && r.driverID == dID).FirstOrDefault();
           //ride.status = 1;
           //db.SaveChanges();
           
           returnRideData = new ReturnRideData { id = ride.id.ToString(), passengerID = ride.passengerID.ToString(), driverID = ride.driverID.ToString(), from_destination = ride.from_destination, to_destination = ride.to_destination, from_lat = ride.from_lat.ToString(), from_lng = ride.from_lng.ToString(), to_lat = ride.to_lat.ToString(), to_lng = ride.to_lng.ToString(), status = ride.status.ToString() };
           return returnRideData;
       }
        


       public List<ReturnRideData> GetAllRides(string userId, string userType)
       {
           List<ReturnRideData> rides = new List<ReturnRideData>();
           
           int? uid = Convert.ToInt32(userId);
           //var result = null;

           if (userType == "Driver")
           {
                var result = db.ride_detail.Where(r => r.driverID == uid).OrderByDescending(r => r.id);
                if (result != null)
                {
                    foreach (var r in result)
                    {
                        rides.Add(new ReturnRideData { id = r.id.ToString(), passengerID = r.passengerID.ToString(), driverID = r.driverID.ToString(), from_destination = r.from_destination, to_destination = r.to_destination, from_lat = r.from_lat.ToString(), from_lng = r.from_lng.ToString(), to_lat = r.to_lat.ToString(), to_lng = r.to_lng.ToString(), status = r.status.ToString() });
                    }
                }
                else
                {
                    rides = null;
                }
           }
           else if (userType == "Passenger")
           {
               var result = db.ride_detail.Where(r => r.passengerID == uid).OrderByDescending(r => r.id);
               if (result != null)
               {
                   foreach (var r in result)
                   {
                       rides.Add(new ReturnRideData { id = r.id.ToString(), passengerID = r.passengerID.ToString(), driverID = r.driverID.ToString(), from_destination = r.from_destination, to_destination = r.to_destination, from_lat = r.from_lat.ToString(), from_lng = r.from_lng.ToString(), to_lat = r.to_lat.ToString(), to_lng = r.to_lng.ToString(), status = r.status.ToString() });
                   }
               }
               {
                   rides = null;
               }
           }
           else
           {
               rides = null;
           }
                      
           return rides;
       }

    
        public bool SignUp(user user)
        {
           return true;
        }

        
    }
}
