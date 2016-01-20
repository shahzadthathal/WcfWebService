using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;


namespace AlumniService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BIITAlumni : IBIITAlumni
    {
        vehicle_navigationEntities db = new vehicle_navigationEntities();



        public void FileUpload(string fileName, Stream fileStream)
        {
            string path = "C:\\Users\\shahzad\\Documents\\Visual Studio 2013\\Projects\\AlumniService";
            FileStream fileToupload = new FileStream(path + fileName, FileMode.Create);

            byte[] bytearray = new byte[10000];
            int bytesRead, totalBytesRead = 0;
            do
            {
                bytesRead = fileStream.Read(bytearray, 0, bytearray.Length);
                totalBytesRead += bytesRead;
            } while (bytesRead > 0);

            fileToupload.Write(bytearray, 0, bytearray.Length);
            fileToupload.Close();
            fileToupload.Dispose();

        }      



        public bool ForgotPassword(string email) 
        {

            SmtpClient client = new SmtpClient("imap-mail.outlook.com");
            MailAddress from = new MailAddress("mehreenyaqub91@hotmail.com", "WCF Service Test" + (char)0xD8 + " for Android", System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress("shahzadthathal@gmail.com");
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test e-mail message sent by an application. ";
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "test message 1" + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
           
            // The userState can be any object that allows your callback 
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";
            client.SendAsync(message, userState);

            return true;
        }

        

       public ReturnUserData Authenticate(string email, string password)
       {   
           ReturnUserData returnUserData;
           var usr = db.users.Where(u => u.email == email && u.password == password).FirstOrDefault();
           if (usr != null)
           {
               usr.is_login = 1;
               db.SaveChanges();
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
           ReturnUserData returnUserData = new ReturnUserData { 
               isError = "0", errorMessage = null, id = uid, name = usr.name, email = usr.email,
               password = usr.password, phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country,
               lat = "333.5555", lng = "79.5554", is_login = usr.is_login.ToString(), is_vehicle_added = usr.is_vehicle_added.ToString(), 
               reg_id = "asdfa1234asdfdf", nic = "5555" 
           };
           return returnUserData;
           // return usr;
       }

       public ReturnUserData UpdateProfile(string usrid, string name, string email, string password, string phone, string nic)
       {
           
           ReturnUserData returnUserData = new ReturnUserData();

           int userid = Convert.ToInt32(usrid);
           var usr = (from u in db.users where u.id == userid select u).FirstOrDefault();
           if (usr != null)
           {
               //usr.id = userid;
               usr.name = System.Web.HttpUtility.UrlDecode(name);
               usr.email = email;
               usr.password = System.Web.HttpUtility.UrlDecode(password);
               usr.phone = phone;
               usr.nic = nic;
               db.SaveChanges();
               returnUserData = new ReturnUserData { 
                   isError = "0", errorMessage = null, id = userid.ToString(), name = usr.name, email = usr.email, password = usr.password,
                   phone = usr.phone, userType = usr.userType, street = usr.street, city = usr.city, country = usr.country, 
                   lat = usr.lat.ToString(), lng = usr.lng.ToString(), is_login = usr.is_login.ToString(), 
                   is_vehicle_added = usr.is_vehicle_added.ToString(), reg_id = "asdfa1234asdfdf", nic = usr.nic 
               };
           }
          
           return returnUserData;
           // return usr;
       }

       public ReturnUserData SaveAddress(string id, string street, string city, string country, string lat, string lng)
       {
           ReturnUserData returnUserData = new ReturnUserData();

           int userid = Convert.ToInt32(id);
           var usr = (from c in db.users where c.id == userid select c).FirstOrDefault();
           if (usr != null)
           {
               usr.id = userid;
               usr.street = System.Web.HttpUtility.UrlDecode(street);
               usr.city = System.Web.HttpUtility.UrlDecode(city);
               usr.country = System.Web.HttpUtility.UrlDecode(country);
               usr.lat = Convert.ToDouble(lat);
               usr.lng = Convert.ToDouble(lng);
               db.SaveChanges();
               returnUserData = new ReturnUserData { 
                   isError = "0", errorMessage = null, id = usr.id.ToString(), name = usr.name, 
                   email = usr.email, password = usr.password, phone = usr.phone, userType = usr.userType,
                   street = usr.street, city = usr.city, country = usr.country, lat = usr.lat.ToString(), lng = usr.lng.ToString(),
                   is_login = usr.is_login.ToString(), is_vehicle_added = usr.is_vehicle_added.ToString(),
                   reg_id = "asdfa1234asdfdf", nic = usr.nic 
               };
           }
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


       

       public ReturnRideData AddRide(string passengerID, string driverID, string from_destination, string to_destination, string from_lat, string from_lng, string to_lat, string to_lng)
       {
           ReturnRideData returnRideData = new ReturnRideData();

           ride_detail ride = new ride_detail();

           int pID = Convert.ToInt32(passengerID); // passenger
           int dID = Convert.ToInt32(driverID); //driver
           ride.passengerID = pID;
           ride.driverID = dID;
           ride.from_destination = System.Web.HttpUtility.UrlDecode(from_destination);
           ride.to_destination = System.Web.HttpUtility.UrlDecode(to_destination);
           ride.from_lat = Convert.ToDouble(from_lat);
           ride.from_lng = Convert.ToDouble(from_lng);
           ride.to_lat = Convert.ToDouble(to_lat);
           ride.to_lng = Convert.ToDouble(to_lng);
           ride.created_date = DateTime.Now;
           ride.status = 0;
           ride.amount = 0;
           ride.rating = 0;
           db.ride_detail.Add(ride);
           db.SaveChanges();

           var usr = db.users.Where(v => v.id == ride.driverID).FirstOrDefault();
           usr.is_available = 0;
           db.SaveChanges();

           string rid = ride.id.ToString(); //ride id
           string rpid = ride.passengerID.ToString(); // passengerID
           string rdid = ride.driverID.ToString(); //driverID
           returnRideData = new ReturnRideData
           {
               id = rid,
               passengerID = rpid,
               driverID = rdid,
               from_destination = ride.from_destination,
               to_destination = ride.to_destination,
               from_lat = ride.from_lat.ToString(),
               from_lng = ride.from_lng.ToString(),
               to_lat = ride.to_lat.ToString(),
               to_lng = ride.to_lng.ToString(),
               status = ride.status.ToString(),
               amount = ride.amount.ToString(),
               review = ride.review,
               rating = ride.rating.ToString(),
               driver_name = "Driver Name",
               passenger_name = "Passenger Name"
           };
           return returnRideData;

       }

       public ReturnRideData CheckNewRide(string userId, string userType) 
       {
           ReturnRideData returnRideData = new ReturnRideData();

           int userID = Convert.ToInt32(userId); //driver

           if (userType == "Passenger") 
           {
               var ride = db.ride_detail.Where(r => r.passengerID == userID && r.status == 1).FirstOrDefault();
               if (ride != null)
               {
                   returnRideData = new ReturnRideData
                   {
                       id = ride.id.ToString(),
                       passengerID = ride.passengerID.ToString(),
                       driverID = ride.driverID.ToString(),
                       from_destination = ride.from_destination,
                       to_destination = ride.to_destination,
                       from_lat = ride.from_lat.ToString(),
                       from_lng = ride.from_lng.ToString(),
                       to_lat = ride.to_lat.ToString(),
                       to_lng = ride.to_lng.ToString(),
                       status = ride.status.ToString(),
                       amount = ride.amount.ToString(),
                       review = ride.review,
                       rating = ride.rating.ToString(),
                       driver_name = "Driver Name",
                       passenger_name = "Passenger Name"
                   };
               }
           }
           else if (userType == "Driver")
           {

               var ride = db.ride_detail.Where(r => r.driverID == userID && r.status == 0).FirstOrDefault();
               if (ride != null)
               {
                   returnRideData = new ReturnRideData
                   {
                       id = ride.id.ToString(),
                       passengerID = ride.passengerID.ToString(),
                       driverID = ride.driverID.ToString(),
                       from_destination = ride.from_destination,
                       to_destination = ride.to_destination,
                       from_lat = ride.from_lat.ToString(),
                       from_lng = ride.from_lng.ToString(),
                       to_lat = ride.to_lat.ToString(),
                       to_lng = ride.to_lng.ToString(),
                       status = ride.status.ToString(),
                       amount = ride.amount.ToString(),
                       review = ride.review,
                       rating = ride.rating.ToString(),
                       driver_name = "Driver Name",
                       passenger_name = "Passenger Name"
                   };
               }
           }
           
           
           return returnRideData;
       }

       public ReturnRideData ChangeRideStatus(string rideId, string userId, string status) 
       {
           ReturnRideData returnRideData = new ReturnRideData();

           int rID = Convert.ToInt32(rideId); //rideid
           int dID = Convert.ToInt32(userId); //driver
           int rstatus = Convert.ToInt32(status); //driver

           var ride = (from r in db.ride_detail where r.id == rID && r.driverID == dID select r).FirstOrDefault(); //AND c.ownerId = userid
           ride.status = rstatus;
           db.SaveChanges();

           if (ride.status == 1)
           {
               var usr = db.users.Where(v => v.id == ride.driverID).FirstOrDefault();
               usr.is_available = 0;
               db.SaveChanges();
           
           }
           if (ride.status == 3) 
           {
               var usr = db.users.Where(v => v.id == ride.driverID).FirstOrDefault();
               usr.is_available = 1;
               db.SaveChanges();
           
           }
           
           returnRideData = new ReturnRideData {
                id = ride.id.ToString(), 
                passengerID = ride.passengerID.ToString(), 
                driverID = ride.driverID.ToString(),
                from_destination = ride.from_destination, 
                to_destination = ride.to_destination, 
                from_lat = ride.from_lat.ToString(), 
                from_lng = ride.from_lng.ToString(), 
                to_lat = ride.to_lat.ToString(), 
                to_lng = ride.to_lng.ToString(), 
                status = ride.status.ToString(),
                amount = ride.amount.ToString(), 
                review = ride.review, 
                rating = ride.rating.ToString(),
                driver_name = "Driver Name",
                passenger_name = "Passenger Name"
            };
           return returnRideData;
       }

       public ReturnRideData FinishRide(string rideId, string amount, string review, string rating) 
       {
           ReturnRideData returnRideData = new ReturnRideData();
           int rID = Convert.ToInt32(rideId); //rideid

          // string replaceWith = "";
          // string reviewString = review.Replace("\r\n", replaceWith).Replace("\n", replaceWith).Replace("\r", replaceWith);

           

           var ride = (from r in db.ride_detail where r.id == rID select r).FirstOrDefault(); //AND c.ownerId = userid
           ride.status = 2;
           ride.amount = Convert.ToDouble(amount);
           ride.review = System.Web.HttpUtility.UrlDecode(review);
           ride.rating = Convert.ToDouble(rating);
           db.SaveChanges();

          
           var ratingAverage = db.ride_detail.Where(r => r.driverID == ride.driverID).Average(r => r.rating);

           var usr = db.users.Where(v => v.id == ride.driverID).FirstOrDefault();
           usr.is_available = 1;
          // usr.rating = ratingAverage;
           db.SaveChanges();
           
           returnRideData = new ReturnRideData { 
               id = ride.id.ToString(), 
               passengerID = ride.passengerID.ToString(),
               driverID = ride.driverID.ToString(), 
               from_destination = ride.from_destination,
               to_destination = ride.to_destination,
               from_lat = ride.from_lat.ToString(),  
               from_lng = ride.from_lng.ToString(), 
               to_lat = ride.to_lat.ToString(), 
               to_lng = ride.to_lng.ToString(), 
               status = ride.status.ToString(),
               amount = ride.amount.ToString(), 
               review = ride.review, 
               rating = ride.rating.ToString(),
               driver_name = "Driver Name",
               passenger_name = "Passenger Name"
           };
           return returnRideData;
       }
        


       public List<ReturnRideData> GetAllRides(string userId, string userType)
       {
           List<ReturnRideData> rides = new List<ReturnRideData>();
           
           int? uid = Convert.ToInt32(userId);

           /*var result = db.ride_detail.OrderByDescending(r => r.id);
           foreach (var r in result)
           {
               rides.Add(new ReturnRideData { id = r.id.ToString(), passengerID = r.passengerID.ToString(), driverID = r.driverID.ToString(), from_destination = r.from_destination, to_destination = r.to_destination, from_lat = r.from_lat.ToString(), from_lng = r.from_lng.ToString(), to_lat = r.to_lat.ToString(), to_lng = r.to_lng.ToString(), status = r.status.ToString() });
           }
           return rides;
           */

           //var result = null;

           if (userType == "Driver")
           {

               // SELECT ride_detail.*, users.*
               // FROM ride_detail
               // INNER JOIN users
               // ON ride_detail.driverID=users.id AND ride_detail.driverID = 3085

             /*  var restul1 = db.ride_detail.Where(r => r.driverID == uid).Join(db.users, r => r.passengerID, u => u.id, (r, u) => new { r = r, username = u.name })
                   .OrderByDescending(r => r.r.id);
               foreach (var s in restul1) 
               {
                   rides.Add(new ReturnRideData
                   {
                       id = s.r.id.ToString(),
                       rating = s.r.rating.ToString(),
                       //driver_name = "",
                       passenger_name = s.username
                   });
               }
               return rides;
               */

               var result = db.ride_detail.Where(r => r.driverID == uid).Join(db.users, r => r.passengerID, u => u.id, (r, u) => new { r = r, username = u.name })
                  .OrderByDescending(r => r.r.id); ; 
               if (result != null)
               {
                   foreach (var r in result)
                   {

                       rides.Add(new ReturnRideData
                       {
                           id = r.r.id.ToString(),
                           passengerID = r.r.passengerID.ToString(),
                           driverID = r.r.driverID.ToString(),
                           from_destination = r.r.from_destination,
                           to_destination = r.r.to_destination,
                           from_lat = r.r.from_lat.ToString(),
                           from_lng = r.r.from_lng.ToString(),
                           to_lat = r.r.to_lat.ToString(),
                           to_lng = r.r.to_lng.ToString(),
                           status = r.r.status.ToString(),
                           amount = r.r.amount.ToString(),
                           review = r.r.review,
                           rating = r.r.rating.ToString(),
                           driver_name = "",
                           passenger_name = r.username
                       });
                   }
               }
               else
               {
                   rides = null;
               }

                /*var result = db.ride_detail.Where(r => r.driverID == uid).OrderByDescending(r => r.id);
                if (result != null)
                {
                    foreach (var r in result)
                    {

                        rides.Add(new ReturnRideData { 
                            id = r.id.ToString(), passengerID = r.passengerID.ToString(), 
                            driverID = r.driverID.ToString(), from_destination = r.from_destination, 
                            to_destination = r.to_destination, from_lat = r.from_lat.ToString(), 
                            from_lng = r.from_lng.ToString(), to_lat = r.to_lat.ToString(), 
                            to_lng = r.to_lng.ToString(), status = r.status.ToString(), 
                            amount = r.amount.ToString(), review = r.review, 
                            rating = r.rating.ToString(), 
                            driver_name = "DriverName", 
                            passenger_name = "PassengerName" 
                        });
                    }
                }
                else
                {
                    rides = null;
                }
                 */

           }
           else if (userType == "Passenger")
           {
               var result = db.ride_detail.Where(r => r.passengerID == uid).Join(db.users, r => r.driverID, u => u.id, (r, u) => new { r = r, username = u.name })
                  .OrderByDescending(r => r.r.id); ;
               if (result != null)
               {
                   foreach (var r in result)
                   {
                       rides.Add(new ReturnRideData
                       {
                           id = r.r.id.ToString(),
                           passengerID = r.r.passengerID.ToString(),
                           driverID = r.r.driverID.ToString(),
                           from_destination = r.r.from_destination,
                           to_destination = r.r.to_destination,
                           from_lat = r.r.from_lat.ToString(),
                           from_lng = r.r.from_lng.ToString(),
                           to_lat = r.r.to_lat.ToString(),
                           to_lng = r.r.to_lng.ToString(),
                           status = r.r.status.ToString(),
                           amount = r.r.amount.ToString(),
                           review = r.r.review,
                           rating = r.r.rating.ToString(),
                           driver_name = r.username,
                           passenger_name ="", 
                       });
                   }
               }
               else
               {
                   rides = null;
               }


               /*
                var result = db.ride_detail.Where(r => r.passengerID == uid).OrderByDescending(r => r.id);
               if (result != null)
               {
                   foreach (var r in result)
                   {
                       rides.Add(new ReturnRideData { id = r.id.ToString(), passengerID = r.passengerID.ToString(), 
                           driverID = r.driverID.ToString(), from_destination = r.from_destination,
                           to_destination = r.to_destination, from_lat = r.from_lat.ToString(), 
                           from_lng = r.from_lng.ToString(), to_lat = r.to_lat.ToString(), 
                           to_lng = r.to_lng.ToString(), status = r.status.ToString(), 
                           amount = r.amount.ToString(), review = r.review, 
                           rating = r.rating.ToString(), 
                           driver_name = "DriverName", passenger_name = "PassengerName"
                       });
                   }
               }
               else{    
                   rides = null;
               }
                */

           }
           else
           {
               rides = null;
           }
                      
           return rides;
            
       }

       public List<Driver> GetDrivers(string fromlat, string fromlng)
       {
           List<Driver> drivers = new List<Driver>();

           decimal? lat = Convert.ToDecimal(fromlat);
           decimal? lng = Convert.ToDecimal(fromlng);

           int? dist = 50;

           var result = db.GetNearestDrivers(lat, lng, dist);

           foreach (var r in result)
           {
               string did = r.id.ToString();
               decimal distance = Math.Round((Decimal)r.distance, 2);
               string ddistance = distance.ToString();
               drivers.Add(new Driver { id = did, name = r.name, phone = r.phone, distance = ddistance, rating= "3.5", image="pic1" });
           }

           return drivers;
       }

    
        public bool SignUp(user user)
        {
           return true;
        }

        
    }
}
