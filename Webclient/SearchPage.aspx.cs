using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CB.IBE.Platform.ClientEntities;
using ABC.Model;
using Core.Platform.ProgramMaster.Entities;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using IBEAPI.ClientEntities;
using System.Configuration;
using IBEAPIGateway.Model;
using System.Net;
public partial class SearchPage : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool FlightSearch()
    {
        ABCModel lobjModel = new ABCModel();
        IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();
        try
        {
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            if (lobjProgramDefinition != null)
            {

                string departure = string.Empty;
                if (HttpContext.Current.Request.QueryString["departure"] != null && HttpContext.Current.Request.QueryString["departure"] != "")
                    departure = HttpContext.Current.Request.QueryString["departure"].ToString();
                string arrival = string.Empty;
                if (HttpContext.Current.Request.QueryString["arrival"] != null && HttpContext.Current.Request.QueryString["arrival"] != "")
                    arrival = HttpContext.Current.Request.QueryString["arrival"].ToString();
                string departuredate = string.Empty;
                if (HttpContext.Current.Request.QueryString["departuredate"] != null && HttpContext.Current.Request.QueryString["departuredate"] != "")
                    departuredate = HttpContext.Current.Request.QueryString["departuredate"].ToString();
                departuredate = departuredate.Replace(" ", string.Empty);

                string isReturn = string.Empty;
                if (HttpContext.Current.Request.QueryString["isReturn"] != null && HttpContext.Current.Request.QueryString["isReturn"] != "")
                    isReturn = HttpContext.Current.Request.QueryString["isReturn"].ToString();
                string arrivaldate = string.Empty;
                if (isReturn == "true")
                {
                    if (HttpContext.Current.Request.QueryString["arrivaldate"] != null && HttpContext.Current.Request.QueryString["arrivaldate"] != "")
                        arrivaldate = HttpContext.Current.Request.QueryString["arrivaldate"].ToString();
                    arrivaldate = arrivaldate.Replace(" ", string.Empty);
                }

                string airline = string.Empty;
                if (HttpContext.Current.Request.QueryString["airline"] != null && HttpContext.Current.Request.QueryString["airline"] != "")
                    airline = HttpContext.Current.Request.QueryString["airline"].ToString();
                string airlineIATACode = string.Empty;
                if (HttpContext.Current.Request.QueryString["airlineIATACode"] != null && HttpContext.Current.Request.QueryString["airlineIATACode"] != "")
                    airlineIATACode = HttpContext.Current.Request.QueryString["airlineIATACode"].ToString();
                string adult = string.Empty;
                if (HttpContext.Current.Request.QueryString["adult"] != null && HttpContext.Current.Request.QueryString["adult"] != "")
                    adult = HttpContext.Current.Request.QueryString["adult"].ToString();
                string child = string.Empty;
                if (HttpContext.Current.Request.QueryString["child"] != null && HttpContext.Current.Request.QueryString["child"] != "")
                    child = HttpContext.Current.Request.QueryString["child"].ToString();
                string infant = string.Empty;
                if (HttpContext.Current.Request.QueryString["infant"] != null && HttpContext.Current.Request.QueryString["infant"] != "")
                    infant = HttpContext.Current.Request.QueryString["infant"].ToString();
                string economy = string.Empty;
                if (HttpContext.Current.Request.QueryString["economy"] != null && HttpContext.Current.Request.QueryString["economy"] != "")
                    economy = HttpContext.Current.Request.QueryString["economy"].ToString();

                if (departure != string.Empty && arrival != string.Empty && departuredate != string.Empty && isReturn != string.Empty && adult != string.Empty && child != string.Empty && infant != string.Empty && economy != string.Empty)
                {
                    List<AirField> lobjListOfAirfield = HttpContext.Current.Application["AllAirfields"] as List<AirField>;
                    AirField lobjDepartureAirField = lobjListOfAirfield.Find(airfileds => airfileds.IATACode.Equals(departure));
                    AirField lobjArrivalAirField = lobjListOfAirfield.Find(airfileds => airfileds.IATACode.Equals(arrival));

                    RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as RefererDetails;

                    SearchRequest lobjSearchRequest = new SearchRequest();

                    lobjSearchRequest.SearchDetails.OriginLocation = departure;
                    lobjSearchRequest.SearchDetails.DestinationLocation = arrival;
                    lobjSearchRequest.RefererDetails = lobjRefererDetails;
                    lobjSearchRequest.SearchDetails.DepartureDate = Convert.ToDateTime(lobjModel.StringToDateTime(departuredate));
                    if (Convert.ToBoolean(isReturn))
                    {
                        lobjSearchRequest.SearchDetails.ArrivalDate = Convert.ToDateTime(lobjModel.StringToDateTime(arrivaldate));
                        lobjSearchRequest.SearchDetails.IsReturn = true;
                    }
                    HttpContext.Current.Session["FlightSearchPaymode"] = PaymentType.Points;

                    lobjSearchRequest.SearchDetails.Adults = Convert.ToInt32(adult);
                    lobjSearchRequest.SearchDetails.Childrens = Convert.ToInt32(child);
                    lobjSearchRequest.SearchDetails.Infants = Convert.ToInt32(infant);
                    lobjSearchRequest.SearchDetails.Cabin = Convert.ToString(economy);
                    lobjSearchRequest.SearchDetails.AirlinePrefCode = airlineIATACode.Equals(string.Empty) ? "Any" : airlineIATACode;
                    lobjSearchRequest.SearchDetails.DepCountryName = lobjDepartureAirField.CountryName;
                    lobjSearchRequest.SearchDetails.ArrCountryName = lobjArrivalAirField.CountryName;
                    lobjSearchRequest.SearchDetails.ArrCode.AirportName = lobjArrivalAirField.AirportName;
                    lobjSearchRequest.SearchDetails.DepCode.AirportName = lobjDepartureAirField.AirportName;
                    lobjSearchRequest.SearchDetails.DepCode.City = lobjDepartureAirField.City;
                    lobjSearchRequest.SearchDetails.ArrCode.City = lobjArrivalAirField.City;
                    lobjSearchRequest.SearchDetails.FlightType = airline;
                    lobjSearchRequest.IPAddress = HttpContext.Current.Request.UserHostAddress;

                    lobjSearchRequest.SearchDetails.SessionId = Convert.ToString(HttpContext.Current.Session["SessionId"]);

                    AirSearchRequest lobjAirSearchRequest = new AirSearchRequest();
                    MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

                    lobjAirSearchRequest.Adults = Convert.ToInt32(adult);
                    lobjAirSearchRequest.Childrens = Convert.ToInt32(child);
                    lobjAirSearchRequest.Infants = Convert.ToInt32(infant);
                    lobjAirSearchRequest.Cabin = Convert.ToString(economy);
                    lobjAirSearchRequest.AirlinePrefCode = airlineIATACode.Equals(string.Empty) ? "Any" : airlineIATACode;
                    lobjAirSearchRequest.DepartureDate = lobjIBEAPIModel.StringToDateTime(departuredate);
                    lobjAirSearchRequest.OriginLocation = departure;
                    lobjAirSearchRequest.DestinationLocation = arrival;

                    if (lobjMemberDetails != null)
                    {
                        lobjAirSearchRequest.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                        lobjSearchRequest.SearchDetails.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                    }

                    lobjAirSearchRequest.ResultCount = string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["FlightResultCount"])) ? "500" : Convert.ToString(ConfigurationManager.AppSettings["FlightResultCount"]);

                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    HttpContext.Current.Session["SearchCurrency"] = lstrCurrency;
                    lobjAirSearchRequest.PointRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);
                    lobjSearchRequest.PointRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);
                    if (Convert.ToBoolean(isReturn))
                    {
                        lobjAirSearchRequest.ReturnDate = lobjIBEAPIModel.StringToDateTime(arrivaldate.ToString());
                        lobjAirSearchRequest.IsReturn = true;
                    }
                    else
                    {
                        lobjAirSearchRequest.ReturnDate = lobjAirSearchRequest.DepartureDate.AddDays(-1);
                    }

                    //lobjAirSearchRequest.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    lobjAirSearchRequest.IPAddress = HttpContext.Current.Request.UserHostAddress.Replace(":", string.Empty);
                    HttpContext.Current.Session["FlightSearchPaymode"] = PaymentType.Points;
                    
                    lobjModel.LogActivity(string.Format("FlightSearch Request; OriginLocation-:{0};DestinationLocation-:{1};ArrivalDate-:{2};DepartureDate-:{3};" +
                                     "IsReturn-:{4};Adults-:{5};Childrens-:{6};Infants-:{7};Cabin-:{8};AirlinePrefCode-:{9};FlightType-:{10};",
                                     lobjSearchRequest.SearchDetails.OriginLocation, lobjSearchRequest.SearchDetails.DestinationLocation, lobjSearchRequest.SearchDetails.ArrivalDate,
                                     lobjSearchRequest.SearchDetails.DepartureDate, lobjSearchRequest.SearchDetails.IsReturn, lobjSearchRequest.SearchDetails.Adults, lobjSearchRequest.SearchDetails.Childrens,
                                     lobjSearchRequest.SearchDetails.Infants, lobjSearchRequest.SearchDetails.Cabin, lobjSearchRequest.SearchDetails.AirlinePrefCode, lobjSearchRequest.SearchDetails.FlightType), ActivityType.FlightSearch);

                    SearchResponse lobjSearchResponse = new SearchResponse();
                    lobjSearchResponse = lobjIBEAPIModel.AirSearchRequest(lobjAirSearchRequest);

                    if (lobjSearchResponse.ItineraryDetailsList.Count().Equals(0))//Domestic
                    {
                        lobjSearchRequest.SearchDetails.SearchType = "0";
                    }
                    else
                    {
                        lobjSearchRequest.SearchDetails.SearchType = "1";
                    }

                    HttpContext.Current.Session["SearchFlight"] = lobjSearchRequest;
                    HttpContext.Current.Session["FlightSearchDetails"] = lobjSearchRequest;

                    if (lobjSearchResponse.ItineraryDetailsList.Count() > 0 || lobjSearchResponse.OnwardFlightList.Count() > 0 || lobjSearchResponse.ReturnFlightList.Count() > 0)
                    {
                        HttpContext.Current.Session["Flights"] = lobjSearchResponse;
                        lobjModel.LogActivity(string.Format("FlightSearch : Success"), ActivityType.FlightSearch);
                        return true;
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format("FlightSearch : Failed"), ActivityType.FlightSearch);
                        return false;
                    }
                }
                else
                {
                    lobjModel.LogActivity(string.Format("FlightSearch : Invalid request"), ActivityType.FlightSearch);
                    return false;
                }
            }
            else
            {
                lobjModel.LogActivity(string.Format("FlightSearch ProgramDefination null : Failed"), ActivityType.FlightSearch);
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("SearchPage.aspx FlightSearch Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            lobjModel.LogActivity(string.Format("FlightSearch : Failed"), ActivityType.FlightSearch);
            return false;
        }
    }
    private static string getAirfieldName(string psrtPar)
    {
        if (psrtPar.Length > 3)
            return psrtPar.Substring(0, 3);
        else
            return psrtPar.ToUpper();
    }
}