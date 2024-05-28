using System;
using System.Collections.Generic;
using System.Web;
using ABC.Model;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.ClientEntities;
using Core.Platform.MemberActivity.Entities;
using System.Web.Script.Services;
using System.Web.Services;
using Framework.EnterpriseLibrary.Adapters;

public partial class FlightLBMSServices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [ScriptMethod()]
    [WebMethod]
    public static List<string> GetAirfields(string prefixText)
    {
        ABCModel lobjModel = new ABCModel();
        prefixText = prefixText.ToLower();
        List<AirField> lobjAirfieldList = HttpContext.Current.Application["AllAirfields"] as List<AirField>;
        List<string> lobjRetValue = new List<string>();
        List<string> lobjDubai = new List<string>();
        List<string> lobjLondon = new List<string>();
        List<string> lobjROW = new List<string>();
        try
        {
            lobjAirfieldList.ForEach(airfield =>
            {
                if (airfield.City.Equals("Dubai") && airfield.IATACode.ToLower().IndexOf(prefixText) >= 0 && airfield.IsActive)
                {
                    lobjRetValue.Add(airfield.SearchAirfieldDetails);
                }
                else if (airfield.City.Equals("London") && airfield.IATACode.ToLower().IndexOf(prefixText) >= 0 && airfield.IsActive)
                {
                    lobjRetValue.Add(airfield.SearchAirfieldDetails);
                }
                else if (airfield.City.Equals("Dubai") && airfield.SearchAirfieldDetails.ToLower().IndexOf(prefixText) >= 0 && airfield.IsActive)
                {
                    if (airfield.SearchAirfieldDetails == "DXB, Dubai International, Dubai, UNITED ARAB EMIRATES")
                    {
                        lobjDubai.Insert(0, airfield.SearchAirfieldDetails);
                    }
                    else if (airfield.SearchAirfieldDetails == "DWC, Dubai Al Maktoum International Airport, Dubai, UNITED ARAB EMIRATES")
                    {
                        lobjDubai.Insert(1, airfield.SearchAirfieldDetails);
                    }
                    else
                    {
                        lobjDubai.Add(airfield.SearchAirfieldDetails);
                    }
                }
                else if (airfield.City.Equals("London") && airfield.SearchAirfieldDetails.ToLower().IndexOf(prefixText) >= 0 && airfield.IsActive)
                {
                    if (airfield.SearchAirfieldDetails == "LHR, Heathrow International, London, UNITED KINGDOM")
                    {
                        lobjLondon.Insert(0, airfield.SearchAirfieldDetails);
                    }
                    else
                    {
                        lobjLondon.Add(airfield.SearchAirfieldDetails);
                    }
                }
                else if (airfield.SearchAirfieldDetails.ToLower().IndexOf(prefixText) >= 0 && airfield.IsActive)
                {
                    lobjROW.Add(airfield.SearchAirfieldDetails);
                }
            });
            lobjRetValue.AddRange(lobjDubai);
            lobjRetValue.AddRange(lobjLondon);
            lobjRetValue.AddRange(lobjROW);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightLBMSServices.aspx GetAirfields Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjRetValue;
    }
    [ScriptMethod()]
    [WebMethod]
    public static List<string> GetCarriers(string prefixText)
    {
        ABCModel lobjModel = new ABCModel();
        prefixText = prefixText.ToLower();
        List<Carrier> lobjCarrierList = HttpContext.Current.Application["CarrierList"] as List<Carrier>;
        List<string> lobjRetValue = new List<string>();
        try
        {
            if (lobjCarrierList != null && lobjCarrierList.Count > 0)
            {
                lobjCarrierList.ForEach(Carrierfield =>
                {
                    if (Carrierfield.CarrierName.ToLower().IndexOf(prefixText) >= 0)
                    {
                        lobjRetValue.Add(string.Format("{0}-{1}", Carrierfield.CarrierName, Carrierfield.CarrierCode));
                    }
                });
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightLBMSServices.aspx GetCarriers Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjRetValue;
    }
    [ScriptMethod()]
    [WebMethod]
    public static List<string> GetAllHotelCities(string prefixText)
    {

        List<string> lobjListOfCity = HttpContext.Current.Application["HotelCities"] as List<string>;
        prefixText = prefixText.ToLower();
        List<string> lobjRetValue = new List<string>();
        try
        {
            lobjListOfCity.ForEach(x => { if (x.ToLower().IndexOf(prefixText) >= 0) { lobjRetValue.Add(x); } });
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightLBMSServices.aspx GetAllHotelCities Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjRetValue;
    }
    [ScriptMethod()]
    [WebMethod]
    public static bool BookNowClick(string pstrSequenceNo)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            lobjModel.LogActivity(string.Format("Flight selected for booking"), ActivityType.FlightBooking);
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;
            ItineraryDetails lobjSelectedFlights = lobjSearchResponse.ItineraryDetailsList.Find(lobj => lobj.SequenceNo.Equals(Convert.ToInt32(pstrSequenceNo)));
            HttpContext.Current.Session["SelectedItinerary"] = lobjSelectedFlights;
            if (HttpContext.Current.Session["MemberDetails"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightLBMSServices.aspx BookNowClick Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
    [ScriptMethod()]
    [WebMethod]
    public static bool BookNowClickDomestic(string pstrOnwardSequenceNo, string pstrReturnSequnceNo)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            lobjModel.LogActivity(string.Format("Flight selected for booking"), ActivityType.FlightBooking);
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;
            ItineraryDetails lobjSelectedFlights = new ItineraryDetails();
            FlightDetails lobjOnwardSelectedFlights = lobjSearchResponse.OnwardFlightList.Find(lobjOnward => lobjOnward.SequenceNo.Equals(Convert.ToInt32(pstrOnwardSequenceNo)));
            FlightDetails lobjReturnelectedFlights = lobjSearchResponse.ReturnFlightList.Find(lobjReturn => lobjReturn.SequenceNo.Equals(Convert.ToInt32(pstrReturnSequnceNo)));
            if (lobjOnwardSelectedFlights != null)
            {
                HttpContext.Current.Session["DomesticOnwardFlights"] = lobjOnwardSelectedFlights;
            }
            if (lobjReturnelectedFlights != null)
            {
                HttpContext.Current.Session["DomesticReturnFlights"] = lobjReturnelectedFlights;
            }
            if (lobjOnwardSelectedFlights != null)
            {
                lobjSelectedFlights.ListOfFlightDetails.Add(lobjOnwardSelectedFlights);

                if (lobjReturnelectedFlights != null)
                {
                    lobjSelectedFlights.ListOfFlightDetails.Add(lobjReturnelectedFlights);
                }
                lobjSelectedFlights.OriginLocation = lobjOnwardSelectedFlights.ListOfFlightSegments[0].OriginLocation;
                lobjSelectedFlights.DestinationLocation = lobjOnwardSelectedFlights.ListOfFlightSegments[lobjOnwardSelectedFlights.ListOfFlightSegments.Count - 1].DestinationLocation;
                lobjSelectedFlights.DepartureDate = lobjOnwardSelectedFlights.ListOfFlightSegments[0].DepartureDate;
                lobjSelectedFlights.ArrivalDate = lobjReturnelectedFlights.ListOfFlightSegments[0].DepartureDate;
                lobjSelectedFlights.FareKey = lobjOnwardSelectedFlights.PaxPricingInfoList.PaxPricingInfo[0].PricingInfoList.PricingInfo[0].farekey;
                lobjSelectedFlights.Type = lobjOnwardSelectedFlights.Type;
            }
            if (lobjSelectedFlights != null)
            {
                HttpContext.Current.Session["SelectedItinerary"] = lobjSelectedFlights;
            }
            if (HttpContext.Current.Session["MemberDetails"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightLBMSServices.aspx BookNowClickDomestic Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
    [ScriptMethod()]
    [WebMethod]
    public static bool BookNowClickDomesticOneWay(string pstrSequenceNo)
    {
        ABCModel lobjModel = new ABCModel();
        try
        {
            lobjModel.LogActivity(string.Format("Flight selected for booking"), ActivityType.FlightBooking);
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;
            FlightDetails lobjOnwardSelectedFlights = lobjSearchResponse.OnwardFlightList.Find(lobjOnward => lobjOnward.SequenceNo.Equals(Convert.ToInt32(pstrSequenceNo)));
            ItineraryDetails lobjSelectedFlights = new ItineraryDetails();
            if (lobjOnwardSelectedFlights != null)
            {
                lobjSelectedFlights.ListOfFlightDetails.Add(lobjOnwardSelectedFlights);
                lobjSelectedFlights.OriginLocation = lobjOnwardSelectedFlights.ListOfFlightSegments[0].OriginLocation;
                lobjSelectedFlights.DestinationLocation = lobjOnwardSelectedFlights.ListOfFlightSegments[lobjOnwardSelectedFlights.ListOfFlightSegments.Count - 1].DestinationLocation;
                lobjSelectedFlights.DepartureDate = lobjOnwardSelectedFlights.ListOfFlightSegments[0].DepartureDate;
                lobjSelectedFlights.FareKey = lobjOnwardSelectedFlights.PaxPricingInfoList.PaxPricingInfo[0].PricingInfoList.PricingInfo[0].farekey;
                lobjSelectedFlights.Type = lobjOnwardSelectedFlights.Type;
            }
            if (lobjOnwardSelectedFlights != null)
            {
                HttpContext.Current.Session["DomesticOnwardFlights"] = lobjOnwardSelectedFlights;
            }
            if (lobjSelectedFlights != null)
            {
                HttpContext.Current.Session["SelectedItinerary"] = lobjSelectedFlights;
            }
            if (HttpContext.Current.Session["MemberDetails"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightLBMSServices.aspx BookNowClickDomesticOneWay Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
}