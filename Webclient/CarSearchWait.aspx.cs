using CB.IBE.Platform.Car.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using IBEAPI.ClientEntities;
using IBEAPIGateway.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DropOff = IBEAPI.ClientEntities.DropOff;
using Location = IBEAPI.ClientEntities.Location;
using PickUp = IBEAPI.ClientEntities.PickUp;

public partial class CarSearchWait : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod()]
    public static bool CarSearch()
    {
        IBEAPIModel lobjModel = new IBEAPIModel();
        ABCModel lobjGIMModel = new ABCModel();
        ProgramDefinition lobjProgramDefinition = lobjGIMModel.GetProgramMaster();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

        string EnjoyTravelsource = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelsource"]);
        string EnjoyTraveldisplayCurrency = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTraveldisplayCurrency"]);
        string EnjoyTravelresidenceCountry = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelresidenceCountry"]);
        string EnjoyTravelresidenceCountryName = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelresidenceCountryName"]);

        if (lobjProgramDefinition != null)
        {
            string strPickupLocation = string.Empty;
            string strPickupLocationId = string.Empty;
            string strDropoffLocation = string.Empty;
            string strDropoffLocationId = string.Empty;
            string strPickupDate = string.Empty;
            string strPickupTime = string.Empty;
            string strDropoffDate = string.Empty;
            string strDropoffTime = string.Empty;
            string strDriverresidenceCode = string.Empty;
            string strDriverAge = string.Empty;
            string strDriverResidenceName = string.Empty;

            if (HttpContext.Current.Request.QueryString["PickupLocation"] != null && HttpContext.Current.Request.QueryString["PickupLocation"] != "")
                strPickupLocation = HttpContext.Current.Request.QueryString["PickupLocation"].ToString();
            if (HttpContext.Current.Request.QueryString["PickupLocationId"] != null && HttpContext.Current.Request.QueryString["PickupLocationId"] != "") { strPickupLocationId = HttpContext.Current.Request.QueryString["PickupLocationId"]; }
            if (HttpContext.Current.Request.QueryString["DropoffLocation"] != null && HttpContext.Current.Request.QueryString["DropoffLocation"] != "") { strDropoffLocation = HttpContext.Current.Request.QueryString["DropoffLocation"]; }
            if (HttpContext.Current.Request.QueryString["DropoffLocationId"] != null && HttpContext.Current.Request.QueryString["DropoffLocationId"] != "") { strDropoffLocationId = HttpContext.Current.Request.QueryString["DropoffLocationId"]; }
            if (HttpContext.Current.Request.QueryString["PickupDate"] != null && HttpContext.Current.Request.QueryString["PickupDate"] != "") { strPickupDate = HttpContext.Current.Request.QueryString["PickupDate"]; }
            if (HttpContext.Current.Request.QueryString["PickupTime"] != null && HttpContext.Current.Request.QueryString["PickupTime"] != "") { strPickupTime = HttpContext.Current.Request.QueryString["PickupTime"]; }
            if (HttpContext.Current.Request.QueryString["DropoffDate"] != null && HttpContext.Current.Request.QueryString["DropoffDate"] != "") { strDropoffDate = HttpContext.Current.Request.QueryString["DropoffDate"]; }
            if (HttpContext.Current.Request.QueryString["DropoffTime"] != null && HttpContext.Current.Request.QueryString["DropoffTime"] != "") { strDropoffTime = HttpContext.Current.Request.QueryString["DropoffTime"]; }
            if (HttpContext.Current.Request.QueryString["DriverResidenceCode"] != null && HttpContext.Current.Request.QueryString["DriverResidenceCode"] != "") { strDriverresidenceCode = HttpContext.Current.Request.QueryString["DriverResidenceCode"]; }
            if (HttpContext.Current.Request.QueryString["DriverAge"] != null && HttpContext.Current.Request.QueryString["DriverAge"] != "") { strDriverAge = HttpContext.Current.Request.QueryString["DriverAge"]; }
            if (HttpContext.Current.Request.QueryString["DriverResidenceName"] != null && HttpContext.Current.Request.QueryString["DriverResidenceName"] != "") { strDriverResidenceName = HttpContext.Current.Request.QueryString["DriverResidenceName"]; }

            if (strPickupLocation != string.Empty && strPickupLocationId != string.Empty && strDropoffLocation != string.Empty && strDropoffLocationId != string.Empty && strPickupDate != string.Empty && strDropoffDate != string.Empty && strPickupTime != string.Empty && strDropoffTime != string.Empty)
            {
                LoggingAdapter.WriteLog("CarSearch Request -:strPickupLocation-:" + strPickupLocationId + "_" + strPickupLocation + "|" + "strDropoffLocation-:" + strDropoffLocationId + "_" + strDropoffLocation + "|" + "strPickupDate-:" + strPickupDate + "|" + "strPickupTime-:" + strPickupTime + "|" + "strDropoffDate-:" + strDropoffDate + "|" + "strDropoffTime=:" + strDropoffTime + "|" + "strDriverAge-:" + strDriverAge);

                GetAvailabilityRequest lobjCarSearchRequest = new GetAvailabilityRequest();

                //PickUp                
                PickUp lobjPickUp = new PickUp();
                Location lobjPickUpLocation = new Location();
                lobjPickUpLocation.id = Convert.ToInt32(strPickupLocationId);
                lobjPickUpLocation.name = strPickupLocation;

                lobjPickUp.date = strPickupDate;
                lobjPickUp.Time = strPickupTime;
                lobjPickUp.dateTime = strPickupDate + "T" + strPickupTime + ":00";
                lobjPickUp.location = lobjPickUpLocation;

                //Drop Off
                DropOff lobjDropOff = new DropOff();
                Location lobjDropOffLocation = new Location();
                lobjDropOffLocation.id = Convert.ToInt32(strDropoffLocationId);
                lobjDropOffLocation.name = strDropoffLocation;

                lobjDropOff.date = strDropoffDate;
                lobjDropOff.Time = strDropoffTime;
                lobjDropOff.dateTime = strDropoffDate + "T" + strDropoffTime + ":00";
                lobjDropOff.location = lobjDropOffLocation;

                ResidenceCountry lobjResidenceCountry = new ResidenceCountry();
                if (strDriverresidenceCode!="" && strDriverresidenceCode!="-1")
                {
                    lobjResidenceCountry.code = strDriverresidenceCode;
                }
                else
                {
                    lobjResidenceCountry.code = EnjoyTravelresidenceCountry;
                }
                if (strDriverResidenceName!="" && strDriverResidenceName != "Please Select")
                {
                    lobjResidenceCountry.name = strDriverResidenceName;
                }
                else
                {
                    lobjResidenceCountry.name = EnjoyTravelresidenceCountryName;
                }
                lobjCarSearchRequest.lang = "en-gb";
                lobjCarSearchRequest.source = EnjoyTravelsource;
                lobjCarSearchRequest.pickUp = lobjPickUp;
                lobjCarSearchRequest.dropOff = lobjDropOff;
                if (strDriverAge == null || strDriverAge.Length == 0)
                {
                    lobjCarSearchRequest.driverAge = 0;
                }
                else
                {
                    lobjCarSearchRequest.driverAge = Convert.ToInt32(strDriverAge);
                }

                lobjCarSearchRequest.displayCurrency = EnjoyTraveldisplayCurrency;
                lobjCarSearchRequest.residenceCountry = lobjResidenceCountry;
                lobjCarSearchRequest.vehicleType = "";

                AvailabilityResponse lobjCarAvailabilityResponse = lobjModel.GetAvailability(lobjCarSearchRequest);
                HttpContext.Current.Session["Cars"] = lobjCarAvailabilityResponse;
                HttpContext.Current.Session["CarSearchRequest"] = lobjCarSearchRequest;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}