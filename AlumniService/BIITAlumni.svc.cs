using System;
using System.Collections.Generic;
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

     /*  public user Authenticate(string email, string password)
        {
           user user = new user { id = 1, name = "Shahzad", email = "shahzad@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Passenger" };
           return user;
        }
      */


        public ReturnRideData AddRide(string passengerID, string driverID, string from_destination, string to_destination, string from_lat, string from_lng, string to_lat, string to_lng)
        {
            int pID = Convert.ToInt32(passengerID); // passenger
            int dID = Convert.ToInt32(driverID); //driver

            ride_detail rd = new ride_detail();
            rd.passengerID = pID;
            rd.driverID = dID;
            rd.from_destination = from_destination;
            rd.to_destination = to_destination;
            rd.from_lat = Convert.ToDouble(from_lat);
            rd.from_lng = Convert.ToDouble(from_lat);
            rd.to_lat = Convert.ToDouble(from_lat);
            rd.to_lng = Convert.ToDouble(from_lat);
            rd.status = 0;
            db.SaveChanges();
            string rid = rd.id.ToString(); //ride id
            string rpid = rd.passengerID.ToString(); // passengerID
            string rdid = rd.driverID.ToString(); //driverID
            ReturnRideData returnRideData = new ReturnRideData { id = rid, passengerID = rpid, driverID = rdid, from_destination = rd.from_destination, to_destination = rd.to_destination, from_lat = rd.from_lat.ToString(), from_lng = rd.from_lng.ToString(), to_lat = rd.to_lat.ToString(), to_lng = rd.to_lng.ToString(), status = rd.status.ToString() };
            return returnRideData;
            
        }




       public ReturnUserData Authenticate(string email, string password)
       {
           
           
           ReturnUserData returnUserData;

           var usr = db.users.Where(u => u.email == email && u.password == password).FirstOrDefault();

           //var usr = (from u in db.users where u.email.Equals(email) && u.password.Equals(password) select u).First();
          
           if (usr != null)
           {
               returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = usr.id.ToString(), name = usr.name, email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, lat = usr.lat.ToString(), lng = usr.lng.ToString(), is_login = "yes", is_vehicle_added = usr.is_vehicle_added, reg_id = "asdfa1234asdfdf", nic = "5555" };
           }
           else
           {
               returnUserData = new ReturnUserData { isError = "1", errorMessage = "Invalid credentinals", id = "", name = "", email = "", password = "", phone = "", userType = "", street = "", city = "", country = "", lat = "", lng = "", is_login = "", is_vehicle_added = "", reg_id = "", nic = "" };
           }
          
           return returnUserData;


           /*
           string id = "1";
           ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = id, name = "Shahzad", email = "shahzad@app.com", password = "123456", phone = "3016973691", userType = "Passenger", nic = "5555", lat = "333.5555", lng = "333.5555" };
          // user user = new user { id = 1, name = "Shahzad", email = "shahzad@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Passenger" };
           return returnUserData;
            */
       }


       public ReturnUserData Register(string name, string email, string password, string phone, string nic, string userType)
       {
           user usr = new user();
           usr.name = name;
           usr.email = email;
           usr.password = password;
           usr.phone = phone;
           usr.nic = nic;
           usr.userType = userType;
           db.users.Add(usr);
           db.SaveChanges();
           string uid = usr.id.ToString();
           ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = uid, name = usr.name, email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, lat = "333.5555", lng = "79.5554", is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf", nic = "5555" };
           return returnUserData;
           // return usr;
       }

       public ReturnUserData SaveAddress(string id, string street, string city, string country, string lat, string lng)
       {
           int userid = Convert.ToInt32(id);
           var usr = (from c in db.users where c.id == userid select c).First();
           usr.id = userid;
           usr.street = street;
           usr.city = city;
           usr.country = country;
           usr.lat = Convert.ToDouble(lat);
           usr.lng = Convert.ToDouble(lng);
           db.SaveChanges();
           // return u;
           string uid = usr.id.ToString();
           ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = uid, name = usr.name, email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, lat = usr.lat.ToString(), lng =usr.lng.ToString(), is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf", nic = "5555" };
          // ReturnUserData returnUserData = new ReturnUserData { isError = "0", errorMessage = null, id = uid, name = "ShahzadAhmed", email = "shahzad@app.com", password = "123456", phone = "3016973691", userType = "Passenger", street = "street104", city = "Isb", country = "Paksitan", lat = "33.5555", lng = "79.5554", is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf", nic = "5555" };
           return returnUserData;
       }

       public ReturnVehicleData SaveVehicle(string name, string modelName, string manufacturer_name, string ownerId) 
       {
               int userid = Convert.ToInt32(ownerId);

               var usr = (from c in db.users where c.id == userid select c).First();

               vehicle v = new vehicle();
               v.name = name;
               v.model_name = modelName;
               v.manufacturer_name = manufacturer_name;
               v.ownerId = userid;
               db.vehicles.Add(v);
               db.SaveChanges();

               usr.is_vehicle_added = "yes";
               db.SaveChanges();

               string uid = v.ownerId.ToString();
               ReturnVehicleData returnVehicleData = new ReturnVehicleData { id = uid, name = v.name, model_name = v.model_name, manufacturer_name = v.manufacturer_name, ownerId = uid };
               return returnVehicleData;
           //}
       }


       public ReturnVehicleData UpdateVehicle(string vid, string name, string modelName, string manufacturer_name, string ownerId)
       {
           int id = Convert.ToInt32(vid);
           var vm = (from c in db.vehicles where c.id == id select c).First(); //AND c.ownerId = userid
           vm.name = name;
           vm.model_name = modelName;
           vm.manufacturer_name = manufacturer_name;
           db.SaveChanges();
           ReturnVehicleData returnVehicleData = new ReturnVehicleData { id = id.ToString(), name = vm.name, model_name = vm.model_name, manufacturer_name = vm.manufacturer_name, ownerId = vm.ownerId.ToString() };
           return returnVehicleData;
       }


       public ReturnVehicleData GetVehicleDetail(string userid)
       {
           int ownerId = Convert.ToInt32(userid);
           var vm = db.vehicles.Where(v => v.ownerId == ownerId).FirstOrDefault();          
           ReturnVehicleData returnVehicleData = new ReturnVehicleData { id = vm.id.ToString(), name = vm.name, model_name = vm.model_name, manufacturer_name = vm.manufacturer_name, ownerId = vm.ownerId.ToString() };
           return returnVehicleData;
       }


       /*public user Register(string name, string email, string password, string phone, string nic, string userType)
       {         
           user usr = new user();          
           usr.name = name;
           usr.email = email;
           usr.password = password;
           usr.phone = phone;
           usr.nic = nic;
           usr.userType = userType;
           
           db.users.Add(usr);
           db.SaveChanges();
           user userNew = new user { id = 1, name = "Shahzad3", email = "shahzad@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Passenger", street = "street104", city = "Isb", country = "Paksitan", lat = 333.5555, lng = 79.5554, is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf" };
           return userNew;
          // return usr;
       }
        */

       /*public user SaveAddress(string id, string street, string city, string country, string lat, string lng) 
       {
           int userid = Convert.ToInt32(id);
           var u = (from c in db.users where c.id == userid select c).First();
           u.id = userid;
           u.street = street;
           u.city = city;
           u.country = country;
           u.lat = Convert.ToDouble(lat);
           u.lng = Convert.ToDouble(lng);
           db.SaveChanges();
          // return u;
           user userNew = new user { id = 1, name = "ShahzadAhmed", email = "shahzad@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Passenger", street="street104", city="Isb", country="Paksitan", lat=333.5555, lng=79.5554, is_login="yes", is_vehicle_added="no", reg_id="asdfa1234asdfdf" };
          return userNew;
       } 
        */




       public List<Driver> GetDrivers(string fromlat, string fromlng)
       {
           List<Driver> drivers = new List<Driver>();
           //decimal? lat= Convert.ToDecimal(33.5);
           //decimal? lng = Convert.ToDecimal(73.5);

           decimal? lat = Convert.ToDecimal(fromlat);
           decimal? lng = Convert.ToDecimal(fromlng);

           int? dist=5;
           var result= db.GetNearestDrivers(lat,lng,dist);
           foreach (var r in result)
           {
               string did = r.id.ToString();
               decimal distance = Math.Round((Decimal)r.distance, 2);
               string ddistance = distance.ToString();
               drivers.Add(new Driver { id = did, name = r.name, phone = r.phone, distance = ddistance });
              // drivers.Add(new Driver { User = new user {id=r.id, name=r.name, phone=r.phone, city=r.city},Distance=r.distance });
           }
           //List<user> list= new List<user>();
          /* list.Add(new user { id = 1, name = "ShahzadAhmed", email = "d1@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Driver", street = "street104", city = "Isb", country = "Paksitan", lat = 333.5555, lng = 79.5554, is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf" });
           list.Add(new user { id = 2, name = "ShahzadAhmed", email = "d2@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Driver", street = "street104", city = "Isb", country = "Paksitan", lat = 333.5555, lng = 79.5554, is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf" });
           list.Add(new user { id = 3, name = "ShahzadAhmed", email = "d3@app.com", password = "123456", phone = "3016973691", nic = "3230371013297", userType = "Driver", street = "street104", city = "Isb", country = "Paksitan", lat = 333.5555, lng = 79.5554, is_login = "yes", is_vehicle_added = "no", reg_id = "asdfa1234asdfdf" });
            return list.ToArray();
           */
          return drivers;
       }


        public bool SignUp(user user)
        {
           return true;
        }

    }
}
