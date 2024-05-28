using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.Car.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using Newtonsoft.Json;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchPageDomestic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool FlightSearchforDomestic(string departureCity, string departure, string arrivalCity, string arrival, string departuredate, string isReturn, string arrivaldate, string adult, string child)
    {
        ABCModel lobjModel = new ABCModel();
        try
        {
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            if (lobjProgramDefinition != null)
            {

                //string departure = string.Empty;
                //if (HttpContext.Current.Request.QueryString["DepCity"] != null && HttpContext.Current.Request.QueryString["DepCity"] != "")
                //    departure = HttpContext.Current.Request.QueryString["DepCity"].ToString();
                //string arrival = string.Empty;
                //if (HttpContext.Current.Request.QueryString["arrCity"] != null && HttpContext.Current.Request.QueryString["arrCity"] != "")
                //    arrival = HttpContext.Current.Request.QueryString["arrCity"].ToString();
                //string departuredate = string.Empty;
                //if (HttpContext.Current.Request.QueryString["departuredate"] != null && HttpContext.Current.Request.QueryString["departuredate"] != "")
                //    departuredate = HttpContext.Current.Request.QueryString["departuredate"].ToString();
                //departuredate = departuredate.Replace(" ", string.Empty);

                //string isReturn = string.Empty;
                //if (HttpContext.Current.Request.QueryString["isReturn"] != null && HttpContext.Current.Request.QueryString["isReturn"] != "")
                //    isReturn = HttpContext.Current.Request.QueryString["isReturn"].ToString();
                //string arrivaldate = string.Empty;
                //if (isReturn == "true")
                //{
                //    if (HttpContext.Current.Request.QueryString["arrivaldate"] != null && HttpContext.Current.Request.QueryString["arrivaldate"] != "")
                //        arrivaldate = HttpContext.Current.Request.QueryString["arrivaldate"].ToString();
                //    arrivaldate = arrivaldate.Replace(" ", string.Empty);
                //}
                //string adult = string.Empty;
                //if (HttpContext.Current.Request.QueryString["adult"] != null && HttpContext.Current.Request.QueryString["adult"] != "")
                //    adult = HttpContext.Current.Request.QueryString["adult"].ToString();
                //string child = string.Empty;
                //if (HttpContext.Current.Request.QueryString["child"] != null && HttpContext.Current.Request.QueryString["child"] != "")
                //    child = HttpContext.Current.Request.QueryString["child"].ToString();

                if (departure != string.Empty && arrival != string.Empty && departuredate != string.Empty && isReturn != string.Empty && adult != string.Empty && child != string.Empty)
                {
                    AirFieldsForDomestic lobjListOfAirfield = HttpContext.Current.Application["AllAirfieldsforDomestic"] as AirFieldsForDomestic;
                    Random r = new Random();
                    int randomNumber = r.Next(100000);
                    SearchRequestForDomestic lobjSearchRequest = new SearchRequestForDomestic();
                    lobjSearchRequest.ReferenceId = Convert.ToString(randomNumber);
                    HttpContext.Current.Session["KhaltiFlightBookingReferenceId"] = lobjSearchRequest.ReferenceId;
                    MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

                    if (lobjMemberDetails != null)
                    {
                        lobjSearchRequest.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                    }
                    lobjSearchRequest.Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                    lobjSearchRequest.Adults = Convert.ToInt32(adult);
                    if (Convert.ToBoolean(isReturn))
                    {
                        lobjSearchRequest.ReturnDate = arrivaldate;
                        lobjSearchRequest.IsReturn = true;
                    }
                    lobjSearchRequest.Childrens = Convert.ToInt32(child);
                    lobjSearchRequest.DepartureDate = departuredate;
                    lobjSearchRequest.DestinationLocation = arrival;
                    lobjSearchRequest.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    lobjSearchRequest.OriginLocation = departure;
                    lobjSearchRequest.DeptCity = departureCity;
                    lobjSearchRequest.ArrivalCity = arrivalCity;
                    HttpContext.Current.Session["FlightSearchPaymode"] = PaymentType.Points;
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    HttpContext.Current.Session["SearchCurrency"] = lstrCurrency;
                    lobjSearchRequest.PointRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);

                    LoggingAdapter.WriteLog("SearchPageDomestic.aspx lobjSearchRequest: " + JsonConvert.SerializeObject(lobjSearchRequest));

                    SearchResponseForDomestic lobjSearchResponse = new SearchResponseForDomestic();

                    lobjModel.LogActivity(string.Format("FlightSearchforDomestic Request; OriginLocation-:{0};DestinationLocation-:{1};ArrivalDate-:{2};DepartureDate-:{3};" +
                 "IsReturn-:{4};Adults-:{5};Childrens-:{6};PointRate:{7}",
                 lobjSearchRequest.OriginLocation, lobjSearchRequest.DestinationLocation, lobjSearchRequest.ReturnDate,
                 lobjSearchRequest.DepartureDate, lobjSearchRequest.IsReturn, lobjSearchRequest.Adults, lobjSearchRequest.Childrens, lobjSearchRequest.PointRate), ActivityType.FlightSearchForDomestic);
                    lobjSearchResponse = lobjModel.MapSearchResponseForDomesticFlights(lobjSearchRequest);
                    HttpContext.Current.Session["SearchFlightForDomestic"] = lobjSearchRequest;

                    HttpContext.Current.Session["FlightSearchDetailsForDomestic"] = lobjSearchRequest;

                    if (lobjSearchResponse.BookingId != 0 || lobjSearchResponse.Inbound.Count() > 0 || lobjSearchResponse.Outbound.Count() > 0)
                    {
                        HttpContext.Current.Session["FlightsForDomestic"] = lobjSearchResponse;
                        HttpContext.Current.Session["DomesticFlightBookingId"] = lobjSearchResponse.BookingId;
                        if (lobjSearchResponse.Inbound.Count() > 0)
                        {
                            HttpContext.Current.Session["DomesticInboundFlights"] = lobjSearchResponse.Inbound;
                        }
                        if (lobjSearchResponse.Outbound.Count() > 0) {
                            HttpContext.Current.Session["DomesticOutboundFlights"] = lobjSearchResponse.Outbound;
                            lobjModel.LogActivity(string.Format("FlightSearchForDomestic : Success"), ActivityType.FlightSearchForDomestic);
                            return true;
                        }
                        else
                        {
                            lobjModel.LogActivity(string.Format("FlightSearchForDomestic : Failed"), ActivityType.FlightSearchForDomestic);
                            return false;
                        }
                       
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format("FlightSearchForDomestic : Failed"), ActivityType.FlightSearchForDomestic);
                        return false;
                    }
                }
                else
                {
                    lobjModel.LogActivity(string.Format("FlightSearchForDomestic : Invalid request"), ActivityType.FlightSearchForDomestic);
                    return false;
                }
            }
            else
            {
                lobjModel.LogActivity(string.Format("FlightSearchForDomestic : Failed"), ActivityType.FlightSearchForDomestic);
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SearchPageDomestic.aspx FlightSearchForDomestic Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lobjModel.LogActivity(string.Format("FlightSearchForDomestic : Failed"), ActivityType.FlightSearchForDomestic);
            return false;
        }
    }
}