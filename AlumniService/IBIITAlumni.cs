using System;
using System.Collections.Generic;
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

        [OperationContract]
        [WebInvoke(Method ="GET", 
            UriTemplate ="/Authenticate/{email}/{password}",
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
       // user Authenticate(string email, string password);
        ReturnUserData Authenticate(string email, string password);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/Register/{name}/{email}/{password}/{phone}/{nic}/{userType}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData Register(string name, string email, string password, string phone, string nic, string userType);

        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/Saveaddress/{id}/{street}/{city}/{country}/{lat}/{lng}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnUserData SaveAddress(string id, string street, string city, string country, string lat, string lng);


        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/SaveVehicle/{name}/{model_name}/{manufacturer_name}/{ownerId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ReturnVehicleData SaveVehicle(string name, string model_name, string manufacturer_name, string ownerId);




        [OperationContract]
        [WebInvoke(Method = "GET",
            UriTemplate = "/GetDrivers/{fromlat}/{fromlng}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Driver> GetDrivers(string fromlat, string fromlng);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/SignUp",
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        bool SignUp(user user);
    }

}
