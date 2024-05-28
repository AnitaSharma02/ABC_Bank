using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.EnterpriseLibrary.Common.SerializationHelper;
using Framework.Integrations.Hotels.Entities;
using GiiftShopGateway.Model;
using IBE.Client.FlightFilter;
using ABC.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FlightListForDomestic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ABCModel lobjModel = new ABCModel();
                SearchRequestForDomestic lobjSearchRequest = HttpContext.Current.Session["lobjOnwardFlightList"] as SearchRequestForDomestic;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx PageLoad Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] FlightData()
    {
        string[] lobjListOfData = new string[4];
        string lstrSearchRequest = string.Empty;
        string lstrDepartDate = string.Empty;
        string lstrArrivalDate = string.Empty;
        try
        {
            SearchRequestForDomestic lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetailsForDomestic"] as SearchRequestForDomestic;
            lstrSearchRequest = JSONSerialization.Serialize(lobjSearchRequest);
            lstrDepartDate = DateTime.Parse(lobjSearchRequest.DepartureDate).ToString("ddd dd MMM");
            if (lobjSearchRequest.IsReturn)
            {
                lstrArrivalDate = DateTime.Parse(lobjSearchRequest.ReturnDate).ToString("ddd dd MMM");
            }

            //0
            lobjListOfData[0] = lstrSearchRequest;
            //1
            lobjListOfData[1] = lstrDepartDate;
            //2
            lobjListOfData[2] = lstrArrivalDate;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx FlightData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] LoadData(string pstrIsNext)
    {

        int offset = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FlightOffset"]);
        string[] lobjListOfData = new string[7];
        string lstrOnwardResult = string.Empty;
        string lstrReturnResult = string.Empty;
        string lstrFinalResult = string.Empty;
        string lstrTemplate = string.Empty;
        bool isReturnDomestic = false;
        bool IsLoadNext = false;
        string lstrListCount = string.Empty;

        string lstrCurrency = HttpContext.Current.Session["SearchCurrency"] as string;
        try
        {
            SearchRequestForDomestic lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetailsForDomestic"] as SearchRequestForDomestic;
            SearchResponseForDomestic lobjSearchResponse = HttpContext.Current.Session["FlightsForDomestic"] as SearchResponseForDomestic;
            List<FlightDetails> lobjOnwardFlightList = new List<FlightDetails>();
            List<FlightDetails> lobjReturnFlightList = new List<FlightDetails>();
            FinalFlightResult ObjFinalFlightList = new FinalFlightResult();
            List<FinalFlightResult> lobjFinalFlightList = new List<FinalFlightResult>();

            //SelectedFinalDomesticFlights selectedFinalDomesticFlights = new SelectedFinalDomesticFlights();
            List<FlightDetails> SelectedFlightsOutbound = new List<FlightDetails>();
            List<FlightDetails> SelectedFlightsInbound = new List<FlightDetails>();
            #region Domestic and International Filghts PageLoad
            if (lobjSearchResponse != null)
            {
                lobjOnwardFlightList = lobjSearchResponse.Outbound;
                lobjOnwardFlightList = FilterDomesticData(lobjOnwardFlightList);

                int OnwardCount = lobjOnwardFlightList.Count;
                //No of count oneway domestic
                lstrListCount = "TOTAL FLIGHT FOUND : " + lobjOnwardFlightList.Count;
                //offset
                lobjOnwardFlightList = lobjOnwardFlightList.Take(offset).ToList();
                lstrOnwardResult = JSONSerialization.Serialize(lobjOnwardFlightList);

                if (offset.Equals(lobjOnwardFlightList.Count))
                {
                    IsLoadNext = true;
                }

                if (lobjSearchResponse.Inbound.Count > 0)
                {
                    lobjReturnFlightList = lobjSearchResponse.Inbound;

                    lobjReturnFlightList = FilterDomesticData(lobjReturnFlightList);

                    //No of count onward domestic
                    lstrListCount = "TOTAL FLIGHT FOUND : " + (OnwardCount + lobjReturnFlightList.Count);

                    //offset
                    lobjReturnFlightList = lobjReturnFlightList.Take(offset).ToList();

                    lstrReturnResult = JSONSerialization.Serialize(lobjReturnFlightList);
                    isReturnDomestic = true;

                    if (offset.Equals(lobjOnwardFlightList.Count) || offset.Equals(lobjReturnFlightList.Count))
                    {
                        IsLoadNext = true;
                    }

                    //selected Outbound and Inbound flight deatils for domestic should be added instead of First or default
                    ObjFinalFlightList.Departure_AirlineName = lobjOnwardFlightList.FirstOrDefault().AirlineName;
                    ObjFinalFlightList.Departure_AirlineLogo = lobjOnwardFlightList.FirstOrDefault().AirlineLogo;
                    ObjFinalFlightList.Departure_FlightDate = lobjOnwardFlightList.FirstOrDefault().FlightDate;
                    ObjFinalFlightList.Departure_FlightNo = lobjOnwardFlightList.FirstOrDefault().FlightNo;
                    ObjFinalFlightList.Departure_Departure = lobjOnwardFlightList.FirstOrDefault().Departure;
                    ObjFinalFlightList.Departure_DepartureTime = lobjOnwardFlightList.FirstOrDefault().DepartureTime;
                    ObjFinalFlightList.Departure_Arrival = lobjOnwardFlightList.FirstOrDefault().Arrival;
                    ObjFinalFlightList.Departure_ArrivalTime = lobjOnwardFlightList.FirstOrDefault().ArrivalTime;
                    ObjFinalFlightList.Departure_FreeBaggage = lobjOnwardFlightList.FirstOrDefault().FreeBaggage;
                    ObjFinalFlightList.Departure_FareTotal = lobjOnwardFlightList.FirstOrDefault().FareTotal;
                    ObjFinalFlightList.Departure_FlightId = lobjOnwardFlightList.FirstOrDefault().FlightId;
                    ObjFinalFlightList.Departure_AircraftType = lobjOnwardFlightList.FirstOrDefault().AircraftType;
                    ObjFinalFlightList.Return_AirlineName = lobjReturnFlightList.FirstOrDefault().AirlineName;
                    ObjFinalFlightList.Return_AirlineLogo = lobjReturnFlightList.FirstOrDefault().AirlineLogo;
                    ObjFinalFlightList.Return_FlightDate = lobjReturnFlightList.FirstOrDefault().FlightDate;
                    ObjFinalFlightList.Return_FlightNo = lobjReturnFlightList.FirstOrDefault().FlightNo;
                    ObjFinalFlightList.Return_Departure = lobjReturnFlightList.FirstOrDefault().Departure;
                    ObjFinalFlightList.Return_DepartureTime = lobjReturnFlightList.FirstOrDefault().DepartureTime;
                    ObjFinalFlightList.Return_Arrival = lobjReturnFlightList.FirstOrDefault().Arrival;
                    ObjFinalFlightList.Return_ArrivalTime = lobjReturnFlightList.FirstOrDefault().ArrivalTime;
                    ObjFinalFlightList.Return_FreeBaggage = lobjReturnFlightList.FirstOrDefault().FreeBaggage;
                    ObjFinalFlightList.Return_FareTotal = lobjReturnFlightList.FirstOrDefault().FareTotal;
                    ObjFinalFlightList.Return_FlightId = lobjReturnFlightList.FirstOrDefault().FlightId;
                    ObjFinalFlightList.Return_AircraftType = lobjReturnFlightList.FirstOrDefault().AircraftType;
                    ObjFinalFlightList.FareTotal = lobjOnwardFlightList.FirstOrDefault().FareTotal + lobjReturnFlightList.FirstOrDefault().FareTotal;
                    ObjFinalFlightList.IsReturn = lobjSearchRequest.IsReturn;

                    SelectedFlightsOutbound.Add(lobjOnwardFlightList.FirstOrDefault());//selected Outbound flight deatils for domestic should be added instead of First or default
                    SelectedFlightsInbound.Add(lobjReturnFlightList.FirstOrDefault());//selected Inbound flight deatils for domestic should be added instead of First or default
                    HttpContext.Current.Session["SelectedFlightsDetailsOutbound"] = SelectedFlightsOutbound;
                    HttpContext.Current.Session["SelectedFlightsDetailsInbound"] = SelectedFlightsInbound;
                    HttpContext.Current.Session["FinalBookedFlightDetails"] = ObjFinalFlightList;
                    lobjFinalFlightList.Add(ObjFinalFlightList);

                    lstrFinalResult = JSONSerialization.Serialize(lobjFinalFlightList);
                }

                if (isReturnDomestic)
                {

                    lstrTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticTwoWayOutbound.htm"));
                    lstrTemplate += File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticTwoWayInbound.html"));
                    lstrTemplate += File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticFinalTemplate.htm"));
                    HttpContext.Current.Session["FlightSearchPaymode"] = PaymentType.Points;
                }
                else
                {
                    //selected one way flight deatils for domestic instead of FirstOrDefault;
                    ObjFinalFlightList.Departure_AirlineName = lobjOnwardFlightList.FirstOrDefault().AirlineName;
                    ObjFinalFlightList.Departure_AirlineLogo = lobjOnwardFlightList.FirstOrDefault().AirlineLogo;
                    ObjFinalFlightList.Departure_FlightDate = lobjOnwardFlightList.FirstOrDefault().FlightDate;
                    ObjFinalFlightList.Departure_FlightNo = lobjOnwardFlightList.FirstOrDefault().FlightNo;
                    ObjFinalFlightList.Departure_Departure = lobjOnwardFlightList.FirstOrDefault().Departure;
                    ObjFinalFlightList.Departure_DepartureTime = lobjOnwardFlightList.FirstOrDefault().DepartureTime;
                    ObjFinalFlightList.Departure_Arrival = lobjOnwardFlightList.FirstOrDefault().Arrival;
                    ObjFinalFlightList.Departure_ArrivalTime = lobjOnwardFlightList.FirstOrDefault().ArrivalTime;
                    ObjFinalFlightList.Departure_FreeBaggage = lobjOnwardFlightList.FirstOrDefault().FreeBaggage;
                    ObjFinalFlightList.Departure_FareTotal = lobjOnwardFlightList.FirstOrDefault().FareTotal;
                    ObjFinalFlightList.Departure_FlightId = lobjOnwardFlightList.FirstOrDefault().FlightId;
                    ObjFinalFlightList.Departure_AircraftType = lobjOnwardFlightList.FirstOrDefault().AircraftType;
                    ObjFinalFlightList.FareTotal = lobjOnwardFlightList.FirstOrDefault().FareTotal;
                    ObjFinalFlightList.IsReturn = lobjSearchRequest.IsReturn;

                    SelectedFlightsOutbound.Add(lobjOnwardFlightList.FirstOrDefault()); //selected one way flight deatils for domestic

                    HttpContext.Current.Session["SelectedFlightsDetailsOutbound"] = SelectedFlightsOutbound;
                    HttpContext.Current.Session["SelectedFlightsDetailsInbound"] = null;
                    HttpContext.Current.Session["FinalBookedFlightDetails"] = ObjFinalFlightList;
                    lstrTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticOneWayOutbound.htm"));

                }
            }

            #endregion

            //0
            lobjListOfData[0] = lstrOnwardResult;
            //1
            lobjListOfData[1] = lstrReturnResult;
            //2
            lobjListOfData[2] = lstrTemplate;
            //3
            lobjListOfData[3] = Convert.ToString(IsLoadNext);
            //4
            lobjListOfData[4] = lstrListCount;
            //5
            lobjListOfData[5] = lstrFinalResult;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx LoadData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

    public static List<FlightDetails> FilterDomesticData(List<FlightDetails> lobjFlightList)
    {
        List<FlightDetails> lobjListOfFlightDetails = new List<FlightDetails>();
        try
        {

            for (int i = 0; i < lobjFlightList.Count; i++)
            {

                lobjListOfFlightDetails.Add(lobjFlightList[i]);

            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx FilterDomesticData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfFlightDetails;

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] ShowTripSummary(string pstrReturnFlightId,string pstrOnwardFlightId)
    {
        string[] lstrListOfResult = new string[3];
        string lstrResult = string.Empty;
        string lstrCurrency = HttpContext.Current.Session["SearchCurrency"] as string;
        string lstrOnwardFlightId = Convert.ToString(pstrOnwardFlightId);
        string lstrReturnFlightId= Convert.ToString(pstrReturnFlightId);
        string lstrTemplate = string.Empty;
        try
        {
            SearchRequestForDomestic lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetailsForDomestic"] as SearchRequestForDomestic;
            SearchResponseForDomestic lobjSearchResponse = HttpContext.Current.Session["FlightsForDomestic"] as SearchResponseForDomestic;

            FinalFlightResult ObjFinalFlightList = new FinalFlightResult();
            List<FinalFlightResult> lobjFinalFlightList = new List<FinalFlightResult>();
            List<FlightDetails> SelectedFlightsOutbound = new List<FlightDetails>();
            List<FlightDetails> SelectedFlightsInbound = new List<FlightDetails>();
            if (lobjSearchResponse != null)
            {
                if (!string.IsNullOrEmpty(lstrReturnFlightId))
                {
                    //Two way flight
                    FlightDetails lobjOnwardFlight = lobjSearchResponse.Outbound.Find(lobj => lobj.FlightId.Equals(lstrOnwardFlightId));
                    FlightDetails lobjReturnFlight = lobjSearchResponse.Inbound.Find(lobj => lobj.FlightId.Equals(lstrReturnFlightId));
                    ObjFinalFlightList.Departure_AirlineName = lobjOnwardFlight.AirlineName;
                    ObjFinalFlightList.Departure_AirlineLogo =lobjOnwardFlight.AirlineLogo;
                    ObjFinalFlightList.Departure_FlightDate =lobjOnwardFlight.FlightDate;
                    ObjFinalFlightList.Departure_FlightNo =lobjOnwardFlight.FlightNo;
                    ObjFinalFlightList.Departure_Departure =lobjOnwardFlight.Departure;
                    ObjFinalFlightList.Departure_DepartureTime =lobjOnwardFlight.DepartureTime;
                    ObjFinalFlightList.Departure_Arrival =lobjOnwardFlight.Arrival;
                    ObjFinalFlightList.Departure_ArrivalTime =lobjOnwardFlight.ArrivalTime;
                    ObjFinalFlightList.Departure_FreeBaggage =lobjOnwardFlight.FreeBaggage;
                    ObjFinalFlightList.Departure_FareTotal =lobjOnwardFlight.FareTotal;
                    ObjFinalFlightList.Departure_FlightId =lobjOnwardFlight.FlightId;
                    ObjFinalFlightList.Departure_AircraftType = lobjOnwardFlight.AircraftType;
                    ObjFinalFlightList.Return_AirlineName = lobjReturnFlight.AirlineName;
                    ObjFinalFlightList.Return_AirlineLogo = lobjReturnFlight.AirlineLogo;
                    ObjFinalFlightList.Return_FlightDate = lobjReturnFlight.FlightDate;
                    ObjFinalFlightList.Return_FlightNo = lobjReturnFlight.FlightNo;
                    ObjFinalFlightList.Return_Departure = lobjReturnFlight.Departure;
                    ObjFinalFlightList.Return_DepartureTime = lobjReturnFlight.DepartureTime;
                    ObjFinalFlightList.Return_Arrival = lobjReturnFlight.Arrival;
                    ObjFinalFlightList.Return_ArrivalTime = lobjReturnFlight.ArrivalTime;
                    ObjFinalFlightList.Return_FreeBaggage = lobjReturnFlight.FreeBaggage;
                    ObjFinalFlightList.Return_FareTotal = lobjReturnFlight.FareTotal;
                    ObjFinalFlightList.Return_FlightId = lobjReturnFlight.FlightId;
                    ObjFinalFlightList.Return_AircraftType =    lobjReturnFlight.AircraftType;
                    ObjFinalFlightList.FareTotal = lobjOnwardFlight.FareTotal + lobjReturnFlight.FareTotal;
                    ObjFinalFlightList.IsReturn = lobjSearchRequest.IsReturn;

                    SelectedFlightsOutbound.Add(lobjOnwardFlight);//selected Outbound flight deatils for domestic should be added instead of First or default
                    SelectedFlightsInbound.Add(lobjReturnFlight);//selected Inbound flight deatils for domestic should be added instead of First or default
                    HttpContext.Current.Session["SelectedFlightsDetailsOutbound"] = SelectedFlightsOutbound;
                    HttpContext.Current.Session["SelectedFlightsDetailsInbound"] = SelectedFlightsInbound;
                    HttpContext.Current.Session["FinalBookedFlightDetails"] = ObjFinalFlightList;
                    lobjFinalFlightList.Add(ObjFinalFlightList);

                    lstrResult = JSONSerialization.Serialize(lobjFinalFlightList);
                    lstrTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticFinalTemplate.htm"));
                    //lobjOnwardFlight = lobjSearchResponse.Outbound;
                    //lstrResult = JSONSerialization.Serialize(lobjOnwardFlight);
                }
                else
                {
                    //Oneway flight
                    FlightDetails lobjOnwardFlight = lobjSearchResponse.Outbound.Find(lobj => lobj.FlightId.Equals(lstrOnwardFlightId));
                    ObjFinalFlightList.Departure_AirlineName = lobjOnwardFlight.AirlineName;
                    ObjFinalFlightList.Departure_AirlineLogo = lobjOnwardFlight.AirlineLogo;
                    ObjFinalFlightList.Departure_FlightDate = lobjOnwardFlight.FlightDate;
                    ObjFinalFlightList.Departure_FlightNo = lobjOnwardFlight.FlightNo;
                    ObjFinalFlightList.Departure_Departure = lobjOnwardFlight.Departure;
                    ObjFinalFlightList.Departure_DepartureTime = lobjOnwardFlight.DepartureTime;
                    ObjFinalFlightList.Departure_Arrival = lobjOnwardFlight.Arrival;
                    ObjFinalFlightList.Departure_ArrivalTime = lobjOnwardFlight.ArrivalTime;
                    ObjFinalFlightList.Departure_FreeBaggage = lobjOnwardFlight.FreeBaggage;
                    ObjFinalFlightList.Departure_FareTotal = lobjOnwardFlight.FareTotal;
                    ObjFinalFlightList.Departure_FlightId = lobjOnwardFlight.FlightId;
                    ObjFinalFlightList.Departure_AircraftType = lobjOnwardFlight.AircraftType;
                    ObjFinalFlightList.FareTotal = lobjOnwardFlight.FareTotal;
                    ObjFinalFlightList.IsReturn = lobjSearchRequest.IsReturn;

                    SelectedFlightsOutbound.Add(lobjOnwardFlight); //selected one way flight deatils for domestic

                    HttpContext.Current.Session["SelectedFlightsDetailsOutbound"] = SelectedFlightsOutbound;
                    HttpContext.Current.Session["SelectedFlightsDetailsInbound"] = null;
                    HttpContext.Current.Session["FinalBookedFlightDetails"] = ObjFinalFlightList;
                    lobjFinalFlightList.Add(ObjFinalFlightList);
                    lstrResult = JSONSerialization.Serialize(lobjFinalFlightList);                 
                }
            }
            lstrListOfResult[0] = lstrResult;
            lstrListOfResult[1] = lstrCurrency;
            lstrListOfResult[2] = lstrTemplate;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx ShowTripSummary Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrListOfResult;
    }

    [WebMethod]
    public static bool CheckAvailability(decimal pntamount)
    {
        try
        {
            ABCModel lobjmodel = new ABCModel();
            ShopModel lmodel = new ShopModel();
            string lstrCurrency = lobjmodel.GetDefaultCurrency();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
            ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
            int MemberMiles = lobjmodel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(lobjMemberRelation.RelationType), lstrCurrency, lobjProgramDefinition.ProgramId);

            decimal lintTotalPrice = pntamount;
            if ((lintTotalPrice) > MemberMiles)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx CheckAvailability Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
        return true;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] ModifySearchData()
    {
        string[] lobjListOfData = new string[3];
        string lstrModifySearch = string.Empty;
        string lstrDepartDate = string.Empty;
        string lstrArrivalDate = string.Empty;
        try
        {
            SearchRequestForDomestic lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetailsForDomestic"] as SearchRequestForDomestic;
            lstrModifySearch = JSONSerialization.Serialize(lobjSearchRequest);
            lstrDepartDate = DateTime.Parse(lobjSearchRequest.DepartureDate).ToString("dd/MM/yyyy");
            if (lobjSearchRequest.IsReturn)
            {
                lstrArrivalDate = DateTime.Parse(lobjSearchRequest.ReturnDate).ToString("dd/MM/yyyy");
            }
            lobjListOfData[0] = lstrModifySearch;
            lobjListOfData[1] = lstrDepartDate;
            lobjListOfData[2] = lstrArrivalDate;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightListForDomestic.aspx ModifySearchData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }
}