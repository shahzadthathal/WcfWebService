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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBIITAlumni
    {

        
       /* [OperationContract]
        [WebInvoke(Method = "POST", 
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SignUp/{name}/{email}/{password}/{phone}/{nic}/{userType}/{image}",
            BodyStyle = WebMessageBodyStyle.Bare)]
        ReturnUserData SignUp(String name, String email, String password, String phone, String nic, String userType, String image = null);

        */
 
      /*  [OperationContract]
        [WebInvoke(
            Method = "POST", 
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SignUp")]
        ReturnUserData SignUp(ReturnUserData user);
        */
        /*
        [OperationContract]
        [WebInvoke(
            Method = "POST", 
            RequestFormat = WebMessageFormat.Json, 
            UriTemplate = "createUser")]
        string createUser(string user);
        */

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/UploadImage", 
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string UploadImage(Stream stream);



        [OperationContract]
        [WebInvoke(Method ="GET", 
            UriTemplate ="/Authenticate/{email}/{password}",
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData Authenticate(string email, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/logout/{userid}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool Logout(string userid);


        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/ForgotPassword/{email}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool ForgotPassword(string email);


        /*[OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/Register/{name}/{email}/{password}/{phone}/{nic}/{userType}/{image}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData Register(string name, string email, string password, string phone, string nic, string userType, string image);
        */
        
        /*[OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/UpdateProfile/{userid}/{name}/{email}/{password}/{phone}/{nic}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData UpdateProfile(string userid, string name, string email, string password, string phone, string nic);*/


        [OperationContract]
        [WebInvoke(
                    Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "/Signup"
            )]
        ReturnUserData SignUp(ReturnUserData user);


        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/UpdateProfile",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData UpdateProfile(ReturnUserData returnUserData);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/Saveaddress/{id}/{street}/{city}/{country}/{lat}/{lng}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData SaveAddress(string id, string street, string city, string country, string lat, string lng);


        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/AddVehicle/{name}/{model_name}/{manufacturer_name}/{ownerId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnVehicleData SaveVehicle(string name, string model_name, string manufacturer_name, string ownerId);


        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/UpdateVehicle/{id}/{name}/{model_name}/{manufacturer_name}/{ownerId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnVehicleData UpdateVehicle(string id, string name, string model_name, string manufacturer_name, string ownerId);

        [OperationContract]
        [WebInvoke(Method = "GET",
             UriTemplate = "/GetVehicleDetail/{userid}",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        ReturnVehicleData GetVehicleDetail(string userid);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/GetDrivers/{fromlat}/{fromlng}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Driver> GetDrivers(string fromlat, string fromlng);


        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/AddRide/{passengerID}/{driverID}/{from_destination}/{to_destination}/{from_lat}/{from_lng}/{to_lat}/{to_lng}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnRideData AddRide(string passengerID, string driverID, string from_destination, string to_destination, string from_lat, string from_lng, string to_lat, string to_lng);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/CheckNewRide/{userId}/{userType}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnRideData CheckNewRide(string userId, string userType);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/ChangeRideStatus/{rideId}/{userId}/{status}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnRideData ChangeRideStatus(string rideId, string userId, string status);



        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/FinishRide",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnRideData FinishRide(ReturnRideData postRideData);


        /*
        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/FinishRide/{rideId}/{amount}/{review}/{rating}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnRideData FinishRide(string rideId, string amount, string review, string rating);
         */ 

       

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/GetAllRides/{userId}/{userType}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<ReturnRideData> GetAllRides(string userId, string userType);




       // bool Signup(ReturnUserData user);

    }

}
