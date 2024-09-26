using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Framework.EnterpriseLibrary.Common.SerializationHelper;
using System.Xml.Serialization;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using System.Web.UI.HtmlControls;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.ClientEntities;
using System.Text;
using Framework.Integration.Flight.Entites.CT;
using IBE.Client.FlightFilter;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.ProgramMaster.Entities;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;

public partial class FlightList : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        disableCachingOnBrowsers();
    }

    private void disableCachingOnBrowsers()
    {
        // Do any of these result in META tags e.g. <META HTTP-EQUIV="Expire" CONTENT="-1">
        // HTTP Headers or both?
        // Does this only work for IE?
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // Is this required for FireFox? Would be good to do this without magic strings.
        // Won't it overwrite the previous setting
        Response.AddHeader("Cache-Control", "no-cache, no-store");
        // Why is it necessary to explicitly call SetExpires. Presume it is still better than calling
        // Response.Headers.Add( directly
        Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-1));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SearchRequest lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetails"] as SearchRequest;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx PageLoad Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    private string SetviewDate(DateTime dateTime)
    {
        string CalendarDate;
        string Day = dateTime.ToString("dddd");
        string NowDate = dateTime.ToString("dd");
        string Month = dateTime.ToString("MMM");
        CalendarDate = "<a class='cal'>" + Month + "<em>" + NowDate + "</em> " + Day + "</a>";
        return CalendarDate;
    }

    public static List<FlightDetails> FilterDomesticData(int PageSize, FilterCriteria lobjFilterCriteria, List<FlightDetails> lobjFlightList)
    {
        List<FlightDetails> lobjListOfFlightDetails = new List<FlightDetails>();
        try
        {
            bool isOptiontoBeDisplayed = false;

            for (int i = 0; i < lobjFlightList.Count; i++)
            {
                isOptiontoBeDisplayed = FilterByStops(lobjFlightList[i], lobjFilterCriteria.FilteredNoofStops) && FilterByDuration(lobjFlightList[i], lobjFilterCriteria.CurrentMinDuration, lobjFilterCriteria.CurrentMaxDuration) && FilterByPointsDomestic(lobjFlightList[i], lobjFilterCriteria.CurrentMinPoints, lobjFilterCriteria.CurrentMaxPoints) && FilterByPriceDomestic(lobjFlightList[i], lobjFilterCriteria.CurrentMinPrice, lobjFilterCriteria.CurrentMaxPrice) && FilterByDeptTime(lobjFlightList[i], lobjFilterCriteria.CurrentMinDepTime, lobjFilterCriteria.CurrentMaxDepTime) && FilterByArrTime(lobjFlightList[i], lobjFilterCriteria.CurrentMinArrTime, lobjFilterCriteria.CurrentMaxArrTime) && (lobjFilterCriteria.AllAirlines || FilterByAirline(lobjFlightList[i], lobjFilterCriteria.FilteredAirlines));

                if (isOptiontoBeDisplayed)
                {
                    lobjListOfFlightDetails.Add(lobjFlightList[i]);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterDomesticData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfFlightDetails;

    }

    public static List<ItineraryDetails> FilterInternationalData(int PageSize, FilterCriteria lobjFilterCriteria, List<ItineraryDetails> lobjListOfInternationalFlights)
    {
        List<ItineraryDetails> lobjListOfItineraryDetails = new List<ItineraryDetails>();
        bool isOptiontoBeDisplayed = false;
        try
        {
            for (int i = 0; i < lobjListOfInternationalFlights.Count; i++)
            {
                isOptiontoBeDisplayed = FilterByStops(lobjListOfInternationalFlights[i].ListOfFlightDetails[0], lobjFilterCriteria.FilteredNoofStops) && FilterByDuration(lobjListOfInternationalFlights[i].ListOfFlightDetails[0], lobjFilterCriteria.CurrentMinDuration, lobjFilterCriteria.CurrentMaxDuration) && FilterByPoints(lobjListOfInternationalFlights[i], lobjFilterCriteria.CurrentMinPoints, lobjFilterCriteria.CurrentMaxPoints) && FilterByPrice(lobjListOfInternationalFlights[i], lobjFilterCriteria.CurrentMinPrice, lobjFilterCriteria.CurrentMaxPrice) && FilterByDeptTime(lobjListOfInternationalFlights[i].ListOfFlightDetails[0], lobjFilterCriteria.CurrentMinDepTime, lobjFilterCriteria.CurrentMaxDepTime) && FilterByArrTime(lobjListOfInternationalFlights[i].ListOfFlightDetails[0], lobjFilterCriteria.CurrentMinArrTime, lobjFilterCriteria.CurrentMaxArrTime) && (lobjFilterCriteria.AllAirlines || FilterByAirline(lobjListOfInternationalFlights[i].ListOfFlightDetails[0], lobjFilterCriteria.FilteredAirlines));

                if (isOptiontoBeDisplayed)
                {
                    lobjListOfItineraryDetails.Add(lobjListOfInternationalFlights[i]);
                }

                //if (lobjListOfItineraryDetails.Count.Equals(PageSize))
                //    break;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterInternationalData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfItineraryDetails;
    }

    //FilterByAirline
    private static bool FilterByAirline(FlightDetails pobjFlightDetails, List<string> ListOfAirlines)
    {
        bool Result = false;
        try
        {
            if (ListOfAirlines.Count != 0)
            {
                Result = !ListOfAirlines.Contains(pobjFlightDetails.ListOfFlightSegments[0].Carrier.CarrierName);
            }
            else
            {
                Result = true;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterByAirline Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return Result;
    }

    //FilterByStops
    private static bool FilterByStops(FlightDetails pobjFlightDetails, List<int> ListOfStops)
    {
        bool Result = false;
        try
        {
            if (ListOfStops.Count != 0)
            {
                Result = !ListOfStops.Contains(pobjFlightDetails.ListOfFlightSegments.Count - 1);
            }
            else
            {
                Result = true;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterByStops Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return Result;
    }

    //FilterByDuration
    private static bool FilterByDuration(FlightDetails pobjFlightDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjFlightDetails.TotalDuration / 60 >= pintMinValue && pobjFlightDetails.TotalDuration / 60 <= pintMaxValue;
    }

    //FilterByPoints
    private static bool FilterByPoints(ItineraryDetails pobjItineraryDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjItineraryDetails.FareDetails.TotalPoints >= pintMinValue && pobjItineraryDetails.FareDetails.TotalPoints <= pintMaxValue;
    }

    //FilterByPrice
    private static bool FilterByPrice(ItineraryDetails pobjItineraryDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjItineraryDetails.FareDetails.DiscountedAmount >= pintMinValue && pobjItineraryDetails.FareDetails.DiscountedAmount <= pintMaxValue;
    }

    //FilterByDeptTime
    private static bool FilterByDeptTime(FlightDetails pobjFlightDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjFlightDetails.ListOfFlightSegments[0].DepartureTimeInMinutes >= pintMinValue && pobjFlightDetails.ListOfFlightSegments[0].DepartureTimeInMinutes <= pintMaxValue;
    }

    //FilterByArrTime
    private static bool FilterByArrTime(FlightDetails pobjFlightDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjFlightDetails.ListOfFlightSegments[pobjFlightDetails.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes >= pintMinValue && pobjFlightDetails.ListOfFlightSegments[pobjFlightDetails.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes <= pintMaxValue;
    }

    //FilterByPointsDomestic
    private static bool FilterByPointsDomestic(FlightDetails pobjFlightDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjFlightDetails.FareDetails.TotalPoints >= pintMinValue && pobjFlightDetails.FareDetails.TotalPoints <= pintMaxValue;
    }

    //FilterByPriceDomestic
    private static bool FilterByPriceDomestic(FlightDetails pobjFlightDetails, int pintMinValue, int pintMaxValue)
    {
        return pobjFlightDetails.FareDetails.DiscountedAmount >= pintMinValue && pobjFlightDetails.FareDetails.DiscountedAmount <= pintMaxValue;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] LoadData(string pstrIsNext)
    {

        int offset = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FlightOffset"]);
        string[] lobjListOfData = new string[7];
        string lstrResult = string.Empty;
        string lstrOnwardResult = string.Empty;
        string lstrReturnResult = string.Empty;
        string lstrTemplate = string.Empty;
        bool isReturnDomestic = false;
        bool IsLoadNext = false;
        string lstrListCount = string.Empty;

        string lstrCurrency = HttpContext.Current.Session["SearchCurrency"] as string;
        try
        {
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;
            FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;
            List<FlightDetails> lobjOnwardFlightList = new List<FlightDetails>();
            List<FlightDetails> lobjReturnFlightList = new List<FlightDetails>();
            List<ItineraryDetails> lobjListOfInternationalFlights = new List<ItineraryDetails>();

            #region Domestic and International Filghts PageLoad
            if (lobjSearchResponse != null)
            {
                if (Convert.ToBoolean(Convert.ToInt32(pstrIsNext)))
                {
                    lobjFilterCriteria.PageSize += offset;
                }

                if (lobjSearchResponse.IsDomestic)
                {
                    lobjOnwardFlightList = lobjSearchResponse.OnwardFlightList;
                    lobjOnwardFlightList = FilterDomesticData(lobjFilterCriteria.PageSize, lobjFilterCriteria, lobjOnwardFlightList);

                    int OnwardCount = lobjOnwardFlightList.Count;
                    //No of count oneway domestic
                    lstrListCount = "TOTAL FLIGHT FOUND : " + lobjOnwardFlightList.Count;
                    //offset
                    lobjOnwardFlightList = lobjOnwardFlightList.Take(lobjFilterCriteria.PageSize).ToList();
                    lstrOnwardResult = JSONSerialization.Serialize(lobjOnwardFlightList);

                    if (lobjFilterCriteria.PageSize.Equals(lobjOnwardFlightList.Count))
                    {
                        IsLoadNext = true;
                    }

                    if (lobjSearchResponse.ReturnFlightList.Count > 0)
                    {
                        lobjReturnFlightList = lobjSearchResponse.ReturnFlightList;

                        lobjReturnFlightList = FilterDomesticData(lobjFilterCriteria.PageSize, lobjFilterCriteria, lobjReturnFlightList);

                        //No of count onward domestic
                        lstrListCount = "TOTAL FLIGHT FOUND : " + (OnwardCount + lobjReturnFlightList.Count);

                        //offset
                        lobjReturnFlightList = lobjReturnFlightList.Take(lobjFilterCriteria.PageSize).ToList();

                        lstrReturnResult = JSONSerialization.Serialize(lobjReturnFlightList);
                        isReturnDomestic = true;

                        if (lobjFilterCriteria.PageSize.Equals(lobjOnwardFlightList.Count) || lobjFilterCriteria.PageSize.Equals(lobjReturnFlightList.Count))
                        {
                            IsLoadNext = true;
                        }

                    }

                    if (isReturnDomestic)
                    {

                        lstrTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticTwoWayOnward.htm"));
                        lstrTemplate += File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticTwoWayReturn.htm"));

                        HttpContext.Current.Session["FlightSearchPaymode"] = PaymentType.Points;
                    }
                    else
                    {


                        lstrTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/DomesticOneWayOnward.htm"));

                    }
                }
                else
                {
                    lobjListOfInternationalFlights = new List<ItineraryDetails>();
                    lobjListOfInternationalFlights = lobjSearchResponse.ItineraryDetailsList;

                    lobjListOfInternationalFlights = FilterInternationalData(lobjFilterCriteria.PageSize, lobjFilterCriteria, lobjListOfInternationalFlights);

                    //No of count International
                    lstrListCount = lobjListOfInternationalFlights.Count.ToString();

                    //offset
                    lobjListOfInternationalFlights = lobjListOfInternationalFlights.Take(lobjFilterCriteria.PageSize).ToList();

                    lstrResult = JSONSerialization.Serialize(lobjListOfInternationalFlights);


                    lstrTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/InternationalTwoWay.htm"));


                    if (lobjFilterCriteria.PageSize.Equals(lobjListOfInternationalFlights.Count))
                    {
                        IsLoadNext = true;
                    }

                }

                HttpContext.Current.Session["FilterCriteria"] = lobjFilterCriteria;
            }

            #endregion

            //0
            lobjListOfData[0] = lstrOnwardResult;
            //1
            lobjListOfData[1] = lstrReturnResult;
            //2
            lobjListOfData[2] = lstrResult;
            //3
            lobjListOfData[3] = lstrTemplate;
            //4
            lobjListOfData[4] = Convert.ToString(IsLoadNext);
            //5
            lobjListOfData[5] = lstrListCount;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx LoadData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] FlightData()
    {
        string[] lobjListOfData = new string[4];
        string lstrResult = string.Empty;
        string lstrSearchRequest = string.Empty;
        string lstrDepartDate = string.Empty;
        string lstrArrivalDate = string.Empty;
        try
        {
            SearchRequest lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetails"] as SearchRequest;
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;

            if (lobjSearchResponse != null)
            {
                if (lobjSearchResponse.IsDomestic)
                {
                    List<FlightDetails> lobjOnwardFlightList = lobjSearchResponse.OnwardFlightList;
                    lstrResult = Convert.ToString(lobjOnwardFlightList.Count);

                    if (lobjSearchResponse.ReturnFlightList.Count > 0)
                    {
                        List<FlightDetails> lobjReturnFlightList = lobjSearchResponse.ReturnFlightList;
                        lstrResult = Convert.ToString(lobjOnwardFlightList.Count + lobjReturnFlightList.Count);
                    }
                }
                else
                {
                    List<ItineraryDetails> lobjListOfInternationalFlights = lobjSearchResponse.ItineraryDetailsList;
                    lstrResult = Convert.ToString(lobjListOfInternationalFlights.Count);
                }
            }
            lstrSearchRequest = JSONSerialization.Serialize(lobjSearchRequest);
            lstrDepartDate = lobjSearchRequest.SearchDetails.DepartureDate.ToString("ddd dd MMM");
            lstrArrivalDate = lobjSearchRequest.SearchDetails.ArrivalDate.ToString("ddd dd MMM");


            //0
            lobjListOfData[0] = lstrResult;
            //1
            lobjListOfData[1] = lstrSearchRequest;
            //2
            lobjListOfData[2] = lstrDepartDate;
            //3
            lobjListOfData[3] = lstrArrivalDate;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FlightData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string[] ShowTripSummary(string pstrSummaryType, string pstrSequenceNo)
    {
        string[] lstrListOfResult = new string[2];
        string lstrResult = string.Empty;
        string lstrCurrency = HttpContext.Current.Session["SearchCurrency"] as string;
        int lintSequenceNo = Convert.ToInt32(pstrSequenceNo);
        try
        {
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;
            if (lobjSearchResponse != null)
            {
                if (pstrSummaryType.Equals("Onward"))
                {
                    FlightDetails lobjOnwardFlight = lobjSearchResponse.OnwardFlightList.Find(lobj => lobj.SequenceNo.Equals(lintSequenceNo));
                    lstrResult = JSONSerialization.Serialize(lobjOnwardFlight);
                }
                else
                {
                    FlightDetails lobjReturnFlight = lobjSearchResponse.ReturnFlightList.Find(lobj => lobj.SequenceNo.Equals(lintSequenceNo));
                    lstrResult = JSONSerialization.Serialize(lobjReturnFlight);
                }
            }
            lstrListOfResult[0] = lstrResult;
            lstrListOfResult[1] = lstrCurrency;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx ShowTripSummary Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrListOfResult;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string PaintAirlines()
    {
        FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;
        string lstrAirLineList = string.Empty;

        try
        {
            lstrAirLineList += "<label class=\"checkbox-container d-flex heading-regular\" id='Airstop'>";
            lstrAirLineList += "<span class=\"d-inline-block ml-1 heading-regular\"><input type='checkbox' onclick='return FilterAirlines(&quot;SelectAll&quot;)' checked='checked'  ID='chkSelectAll' class='checkbox_pading ArialNarrow' /></div>";
            lstrAirLineList += "<span class=\"checkmark\"></span>";
            lstrAirLineList += "<span>Select All</span>";
            lstrAirLineList += "</span>";
            lstrAirLineList += "</label>";
            for (int count = 0; count < lobjFilterCriteria.Airlines.Count; count++)
            {
                if (lobjFilterCriteria.Airlines[count] != "Multiple Carrier")
                {
                    lstrAirLineList += "<label class=\"checkbox-container d-flex heading-regular\" id='Airstop'>";
                    lstrAirLineList += "<span class=\"d-inline-block heading-regular ml-1\"><input type='checkbox' onclick='return FilterAirlines(&quot;" + lobjFilterCriteria.Airlines[count] + "&quot;);' checked='checked' ID='chk" + lobjFilterCriteria.Airlines[count].ToString().Replace(" ", "").Replace("(", "").Replace(")", "") + "' class='checkbox_pading ArialNarrow' /></div>";
                    lstrAirLineList += "<span class=\"checkmark\"></span><span>";
                    lstrAirLineList += lobjFilterCriteria.Airlines[count].ToString();
                    lstrAirLineList += "</span></span>";
                    lstrAirLineList += "</label>";
                }

            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx PaintAirlines Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrAirLineList;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string PaintStops()
    {
        FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;
        string lstrStop = string.Empty;
        try
        {
            for (int countStops = 0; countStops <= lobjFilterCriteria.NoOFStops - 1; countStops++)
            {
                if (countStops == 0)
                {
                    lstrStop += "<label class=\"checkbox-container d-flex heading-regular\" id='Airstop'>";
                    lstrStop += "<span class=\"d-inline-block\">";
                    lstrStop += "<input type='checkbox' id='chkStops" + countStops + "' checked onclick='return FilterNoOfStops(" + countStops + ");'>&nbsp;" + 0 + "<span>Stop</span>";
                    lstrStop += "<span class=\"checkmark\"></span>";
                    lstrStop += "</span>";
                    lstrStop += "</label>";
                }
                else
                {
                    lstrStop += "<label class=\"checkbox-container d-flex heading-regular\" id='Airstop'>";
                    lstrStop += "<span class=\"d-inline-block\">";
                    lstrStop += "<input type='checkbox' id='chkStops" + countStops + "' checked onclick='return FilterNoOfStops(" + countStops + ");'>&nbsp;" + countStops + "<span>Stop</span>";
                    lstrStop += "<span class=\"checkmark\"></span>";
                    lstrStop += "</span>";
                    lstrStop += "</label>";

                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx PaintStops Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrStop;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static void GetFilterCriteria()
    {
        FilterCriteria lobjFilterCriteria = new FilterCriteria();
        try
        {
            SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;

            if (lobjSearchResponse != null)
            {
                if (lobjSearchResponse.IsDomestic)
                {
                    List<FlightDetails> lobjOnwardFlightList = lobjSearchResponse.OnwardFlightList;
                    lobjFilterCriteria = GetFilterCriteriaForFlights(lobjOnwardFlightList);

                    if (lobjSearchResponse.ReturnFlightList.Count > 0)
                    {
                        List<FlightDetails> lobjReturnFlightList = lobjSearchResponse.ReturnFlightList;
                        lobjFilterCriteria = GetFilterCriteriaForReturnFlights(lobjReturnFlightList, lobjOnwardFlightList);
                    }
                }
                else
                {
                    List<ItineraryDetails> lobjListOfInternationalFlights = lobjSearchResponse.ItineraryDetailsList;

                    lobjFilterCriteria.MaxPoints = lobjListOfInternationalFlights.Max(flight => flight.FareDetails.TotalPoints);
                    lobjFilterCriteria.MinPoints = lobjListOfInternationalFlights.Min(flight => flight.FareDetails.TotalPoints);
                    lobjFilterCriteria.CurrentMaxPoints = lobjListOfInternationalFlights.Max(flight => flight.FareDetails.TotalPoints);
                    lobjFilterCriteria.CurrentMinPoints = lobjListOfInternationalFlights.Min(flight => flight.FareDetails.TotalPoints);

                    lobjFilterCriteria.MaxPrice = Convert.ToInt32(lobjListOfInternationalFlights.Max(flight => flight.FareDetails.DiscountedAmount));
                    lobjFilterCriteria.MinPrice = Convert.ToInt32(lobjListOfInternationalFlights.Min(flight => flight.FareDetails.DiscountedAmount));
                    lobjFilterCriteria.CurrentMaxPrice = Convert.ToInt32(lobjListOfInternationalFlights.Max(flight => flight.FareDetails.DiscountedAmount));
                    lobjFilterCriteria.CurrentMinPrice = Convert.ToInt32(lobjListOfInternationalFlights.Min(flight => flight.FareDetails.DiscountedAmount));

                    lobjFilterCriteria.MinDuration = lobjListOfInternationalFlights.Min(flight => flight.ListOfFlightDetails[0].ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
                    lobjFilterCriteria.MaxDuration = lobjListOfInternationalFlights.Max(flight => flight.ListOfFlightDetails[0].ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
                    lobjFilterCriteria.CurrentMinDuration = lobjListOfInternationalFlights.Min(flight => flight.ListOfFlightDetails[0].ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
                    lobjFilterCriteria.CurrentMaxDuration = lobjListOfInternationalFlights.Max(flight => flight.ListOfFlightDetails[0].ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;

                    lobjFilterCriteria.CurrentMinArrTime = 0;
                    lobjFilterCriteria.CurrentMaxArrTime = 1440;
                    lobjFilterCriteria.CurrentMinDepTime = 0;
                    lobjFilterCriteria.CurrentMaxDepTime = 1440;

                    lobjFilterCriteria.MinArrTime = 0;
                    lobjFilterCriteria.MaxArrTime = 1440;
                    lobjFilterCriteria.MinDepTime = 0;
                    lobjFilterCriteria.MaxDepTime = 1440;

                    List<string> lobjUniqueListOfAirLine = new List<string>();
                    foreach (ItineraryDetails flight in lobjListOfInternationalFlights) { lobjUniqueListOfAirLine.Add(flight.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName); }
                    lobjFilterCriteria.Airlines = lobjUniqueListOfAirLine.Distinct().ToList();
                    lobjFilterCriteria.AllAirlines = true;
                    lobjFilterCriteria.NoOFStops = lobjListOfInternationalFlights.Max(flight => flight.ListOfFlightDetails[0].ListOfFlightSegments.Count);

                }
                lobjFilterCriteria.PageSize = 0;
                HttpContext.Current.Session["FilterCriteria"] = lobjFilterCriteria;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx GetFilterCriteria Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string GetFilterData()
    {
        FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;
        string lstrFilter = string.Empty;
        try
        {
            lstrFilter = JSONSerialization.Serialize(lobjFilterCriteria);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx GetFilterData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrFilter;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static void GetSortedFlights(string pstrparameter, string pstrdirection, string pstrparamobj)
    {
        SearchResponse lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;
        List<ItineraryDetails> lobjListofItineraryDetails = new List<ItineraryDetails>();
        List<FlightDetails> lobjOnwardFlightDetails = new List<FlightDetails>();
        List<FlightDetails> lobjReturnFlightDetails = new List<FlightDetails>();
        try
        {
            lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList;
            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList;
            lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList;

            if (pstrparameter.Equals("name"))
            {
                if (pstrdirection.Equals("up"))
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderBy(x => x.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderBy(x => x.ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                            }
                        }

                    }

                }
                else
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderByDescending(x => x.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].Carrier.CarrierName).ToList();
                            }
                        }

                    }
                }
            }
            else if (pstrparameter.Equals("departuretime"))
            {
                if (pstrdirection.Equals("up"))
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderBy(x => x.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderBy(x => x.ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                            }
                        }

                    }
                }
                else
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderByDescending(x => x.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].DepartureTimeInMinutes).ToList();
                            }
                        }

                    }
                }
            }
            else if (pstrparameter.Equals("arrivaltime"))
            {
                if (pstrdirection.Equals("up"))
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderBy(x => x.ListOfFlightDetails[0].ListOfFlightSegments[x.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[x.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[x.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderBy(x => x.ListOfFlightSegments[x.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                            }
                        }

                    }
                }
                else
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderByDescending(x => x.ListOfFlightDetails[0].ListOfFlightSegments[x.ListOfFlightDetails[0].ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[x.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[x.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderByDescending(x => x.ListOfFlightSegments[x.ListOfFlightSegments.Count - 1].ArrivalTimeInMinutes).ToList();
                            }
                        }

                    }
                }
            }
            else if (pstrparameter.Equals("miles"))
            {
                if (pstrdirection.Equals("up"))
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderBy(x => x.FareDetails.TotalPoints).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.FareDetails.TotalPoints).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.FareDetails.TotalPoints).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderBy(x => x.FareDetails.TotalPoints).ToList();
                            }
                        }

                    }
                }
                else
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderByDescending(x => x.FareDetails.TotalPoints).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.FareDetails.TotalPoints).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.FareDetails.TotalPoints).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderByDescending(x => x.FareDetails.TotalPoints).ToList();
                            }
                        }

                    }
                }
            }
            else if (pstrparameter.Equals("duration"))
            {
                if (pstrdirection.Equals("up"))
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderBy(x => x.ListOfFlightDetails[0].TotalDuration).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.TotalDuration).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.TotalDuration).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderBy(x => x.TotalDuration).ToList();
                            }
                        }

                    }
                }
                else
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderByDescending(x => x.ListOfFlightDetails[0].TotalDuration).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.TotalDuration).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.TotalDuration).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderByDescending(x => x.TotalDuration).ToList();
                            }
                        }

                    }
                }
            }
            else if (pstrparameter.Equals("trip"))
            {
                if (pstrdirection.Equals("up"))
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderBy(x => x.ListOfFlightDetails[0].ListOfFlightSegments[0].Stops).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[0].Stops).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderBy(x => x.ListOfFlightSegments[0].Stops).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderBy(x => x.ListOfFlightSegments[0].Stops).ToList();
                            }
                        }

                    }
                }
                else
                {
                    if (lobjSearchResponse.ItineraryDetailsList.Count != 0)
                    {
                        lobjListofItineraryDetails = lobjSearchResponse.ItineraryDetailsList.OrderByDescending(x => x.ListOfFlightDetails[0].ListOfFlightSegments[0].Stops).ToList();
                    }
                    if (pstrparamobj.Equals(""))
                    {
                        if (lobjSearchResponse.OnwardFlightList.Count != 0)
                        {
                            lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].Stops).ToList();
                        }
                    }
                    else
                    {
                        if (pstrparamobj.Equals("DomesticTwoWayOnward"))
                        {
                            if (lobjSearchResponse.OnwardFlightList.Count != 0)
                            {
                                lobjOnwardFlightDetails = lobjSearchResponse.OnwardFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].Stops).ToList();
                            }
                        }
                        else if (pstrparamobj.Equals("DomesticTwoWayReturn"))
                        {
                            if (lobjSearchResponse.ReturnFlightList.Count != 0)
                            {
                                lobjReturnFlightDetails = lobjSearchResponse.ReturnFlightList.OrderByDescending(x => x.ListOfFlightSegments[0].Stops).ToList();
                            }
                        }

                    }
                }
            }
            lobjSearchResponse.ItineraryDetailsList = lobjListofItineraryDetails;
            lobjSearchResponse.OnwardFlightList = lobjOnwardFlightDetails;
            lobjSearchResponse.ReturnFlightList = lobjReturnFlightDetails;

            HttpContext.Current.Session["Flights"] = lobjSearchResponse;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx GetSortedFlights Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public static FilterCriteria GetFilterCriteriaForFlights<T>(List<T> pobjFlightList) where T : FlightDetails
    {
        FilterCriteria lobjFilterCriteria = new FilterCriteria();
        try
        {
            lobjFilterCriteria.MaxPoints = pobjFlightList.Max(flight => flight.FareDetails.TotalPoints);
            lobjFilterCriteria.MinPoints = pobjFlightList.Min(flight => flight.FareDetails.TotalPoints);
            lobjFilterCriteria.CurrentMaxPoints = pobjFlightList.Max(flight => flight.FareDetails.TotalPoints);
            lobjFilterCriteria.CurrentMinPoints = pobjFlightList.Min(flight => flight.FareDetails.TotalPoints);

            lobjFilterCriteria.MaxPrice = Convert.ToInt32(pobjFlightList.Max(flight => flight.FareDetails.DiscountedAmount));
            lobjFilterCriteria.MinPrice = Convert.ToInt32(pobjFlightList.Min(flight => flight.FareDetails.DiscountedAmount));
            lobjFilterCriteria.CurrentMaxPrice = Convert.ToInt32(pobjFlightList.Max(flight => flight.FareDetails.DiscountedAmount));
            lobjFilterCriteria.CurrentMinPrice = Convert.ToInt32(pobjFlightList.Min(flight => flight.FareDetails.DiscountedAmount));

            lobjFilterCriteria.MinDuration = pobjFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
            lobjFilterCriteria.MaxDuration = pobjFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
            lobjFilterCriteria.CurrentMinDuration = pobjFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
            lobjFilterCriteria.CurrentMaxDuration = pobjFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;

            lobjFilterCriteria.CurrentMinArrTime = 0;
            lobjFilterCriteria.CurrentMaxArrTime = 2400;
            lobjFilterCriteria.CurrentMinDepTime = 0;
            lobjFilterCriteria.CurrentMaxDepTime = 2400;

            lobjFilterCriteria.MinArrTime = 0;
            lobjFilterCriteria.MaxArrTime = 2400;
            lobjFilterCriteria.MinDepTime = 0;
            lobjFilterCriteria.MaxDepTime = 2400;

            List<string> lobjUniqueOnwardListOfAirLine = new List<string>();
            foreach (T flightDetails in pobjFlightList) { lobjUniqueOnwardListOfAirLine.Add(flightDetails.ListOfFlightSegments[0].Carrier.CarrierName); }
            lobjFilterCriteria.Airlines = lobjUniqueOnwardListOfAirLine.Distinct().ToList();
            lobjFilterCriteria.AllAirlines = true;
            lobjFilterCriteria.NoOFStops = pobjFlightList.Max(flight => flight.ListOfFlightSegments.Count);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx GetFilterCriteriaForFlights Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjFilterCriteria;
    }

    public static FilterCriteria GetFilterCriteriaForReturnFlights<T>(List<T> pobjFlightList, List<T> pobjOnwardFlightList) where T : FlightDetails
    {
        FilterCriteria lobjFilterCriteria = new FilterCriteria();
        try
        {
            lobjFilterCriteria.MaxPoints = pobjFlightList.Max(flight => flight.FareDetails.TotalPoints) > pobjOnwardFlightList.Max(flight => flight.FareDetails.TotalPoints) ? pobjFlightList.Max(flight => flight.FareDetails.TotalPoints) : pobjOnwardFlightList.Max(flight => flight.FareDetails.TotalPoints);
            lobjFilterCriteria.MinPoints = pobjFlightList.Min(flight => flight.FareDetails.TotalPoints) < pobjOnwardFlightList.Min(flight => flight.FareDetails.TotalPoints) ? pobjFlightList.Min(flight => flight.FareDetails.TotalPoints) : pobjOnwardFlightList.Min(flight => flight.FareDetails.TotalPoints);
            lobjFilterCriteria.CurrentMaxPoints = pobjFlightList.Max(flight => flight.FareDetails.TotalPoints) > pobjOnwardFlightList.Max(flight => flight.FareDetails.TotalPoints) ? pobjFlightList.Max(flight => flight.FareDetails.TotalPoints) : pobjOnwardFlightList.Max(flight => flight.FareDetails.TotalPoints);
            lobjFilterCriteria.CurrentMinPoints = pobjFlightList.Min(flight => flight.FareDetails.TotalPoints) < pobjOnwardFlightList.Min(flight => flight.FareDetails.TotalPoints) ? pobjFlightList.Min(flight => flight.FareDetails.TotalPoints) : pobjOnwardFlightList.Min(flight => flight.FareDetails.TotalPoints);

            lobjFilterCriteria.MaxPrice = pobjFlightList.Max(flight => flight.FareDetails.DiscountedAmount) > pobjOnwardFlightList.Max(flight => flight.FareDetails.DiscountedAmount) ? Convert.ToInt32(pobjFlightList.Max(flight => flight.FareDetails.DiscountedAmount)) : Convert.ToInt32(pobjOnwardFlightList.Max(flight => flight.FareDetails.DiscountedAmount));
            lobjFilterCriteria.MinPrice = pobjFlightList.Min(flight => flight.FareDetails.DiscountedAmount) < pobjOnwardFlightList.Min(flight => flight.FareDetails.DiscountedAmount) ? Convert.ToInt32(pobjFlightList.Min(flight => flight.FareDetails.DiscountedAmount)) : Convert.ToInt32(pobjOnwardFlightList.Min(flight => flight.FareDetails.DiscountedAmount));
            lobjFilterCriteria.CurrentMaxPrice = pobjFlightList.Max(flight => flight.FareDetails.DiscountedAmount) > pobjOnwardFlightList.Max(flight => flight.FareDetails.DiscountedAmount) ? Convert.ToInt32(pobjFlightList.Max(flight => flight.FareDetails.DiscountedAmount)) : Convert.ToInt32(pobjOnwardFlightList.Max(flight => flight.FareDetails.DiscountedAmount));
            lobjFilterCriteria.CurrentMinPrice = pobjFlightList.Min(flight => flight.FareDetails.DiscountedAmount) < pobjOnwardFlightList.Min(flight => flight.FareDetails.DiscountedAmount) ? Convert.ToInt32(pobjFlightList.Min(flight => flight.FareDetails.DiscountedAmount)) : Convert.ToInt32(pobjOnwardFlightList.Min(flight => flight.FareDetails.DiscountedAmount));

            lobjFilterCriteria.MinDuration = pobjFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 < pobjOnwardFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 ? pobjFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 : pobjOnwardFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
            lobjFilterCriteria.MaxDuration = pobjFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 > pobjOnwardFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 ? pobjFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 : pobjOnwardFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
            lobjFilterCriteria.CurrentMinDuration = pobjFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 < pobjOnwardFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 ? pobjFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 : pobjOnwardFlightList.Min(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;
            lobjFilterCriteria.CurrentMaxDuration = pobjFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 > pobjOnwardFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 ? pobjFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60 : pobjOnwardFlightList.Max(flight => flight.ListOfFlightSegments.Sum(flight_segment => flight_segment.JourneyTime)) / 60;

            lobjFilterCriteria.CurrentMinArrTime = 0;
            lobjFilterCriteria.CurrentMaxArrTime = 1440;
            lobjFilterCriteria.CurrentMinDepTime = 0;
            lobjFilterCriteria.CurrentMaxDepTime = 1440;

            lobjFilterCriteria.MinArrTime = 0;
            lobjFilterCriteria.MaxArrTime = 1440;
            lobjFilterCriteria.MinDepTime = 0;
            lobjFilterCriteria.MaxDepTime = 1440;

            List<string> lobjUniqueOnwardListOfAirLine = new List<string>();
            foreach (T flightDetails in pobjFlightList) { lobjUniqueOnwardListOfAirLine.Add(flightDetails.ListOfFlightSegments[0].Carrier.CarrierName); }
            foreach (T flightDetails in pobjOnwardFlightList) { lobjUniqueOnwardListOfAirLine.Add(flightDetails.ListOfFlightSegments[0].Carrier.CarrierName); }
            lobjFilterCriteria.Airlines = lobjUniqueOnwardListOfAirLine.Distinct().ToList();
            lobjFilterCriteria.AllAirlines = true;
            lobjFilterCriteria.NoOFStops = pobjFlightList.Max(flight => flight.ListOfFlightSegments.Count) > pobjOnwardFlightList.Max(flight => flight.ListOfFlightSegments.Count) ? pobjFlightList.Max(flight => flight.ListOfFlightSegments.Count) : pobjOnwardFlightList.Max(flight => flight.ListOfFlightSegments.Count);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx GetFilterCriteriaForReturnFlights Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjFilterCriteria;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static void FilterAirlines(string pstrAirline, string pstrischecked)
    {
        try
        {
            FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;
            if (pstrAirline.Equals("SelectAll"))
            {
                if (Convert.ToBoolean(pstrischecked))
                {
                    lobjFilterCriteria.FilteredAirlines = new List<string>();
                    lobjFilterCriteria.AllAirlines = true;
                }
                else
                {
                    List<string> lstrListOfAirlines = new List<string>();

                    for (int i = 0; i < lobjFilterCriteria.Airlines.Count; i++)
                    {
                        lstrListOfAirlines.Add(lobjFilterCriteria.Airlines[i]);
                    }

                    lobjFilterCriteria.FilteredAirlines = lstrListOfAirlines;
                    lobjFilterCriteria.AllAirlines = false;
                }
            }
            else
            {
                if (Convert.ToBoolean(pstrischecked))
                {
                    lobjFilterCriteria.FilteredAirlines.Remove(pstrAirline);
                    if (lobjFilterCriteria.FilteredAirlines.Count.Equals(0))
                    {
                        lobjFilterCriteria.AllAirlines = true;
                    }
                }
                else
                {
                    lobjFilterCriteria.FilteredAirlines.Add(pstrAirline);
                    lobjFilterCriteria.AllAirlines = false;
                }
            }
            HttpContext.Current.Session["FilterCriteria"] = lobjFilterCriteria;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterAirlines Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static void FilterNoOfStops(string pstrid, string pstrischecked)
    {
        try
        {
            FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;

            if (Convert.ToBoolean(pstrischecked))
            {
                lobjFilterCriteria.FilteredNoofStops.Remove(Convert.ToInt32(pstrid));
            }
            else
            {
                lobjFilterCriteria.FilteredNoofStops.Add(Convert.ToInt32(pstrid));
            }
            HttpContext.Current.Session["FilterCriteria"] = lobjFilterCriteria;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterNoOfStops Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static void FilterSlider(string pstrid, string pstrMinVal, string pstrMaxVal)
    {
        try
        {
            FilterCriteria lobjFilterCriteria = HttpContext.Current.Session["FilterCriteria"] as FilterCriteria;

            switch (Convert.ToInt32(pstrid))
            {
                case 1:
                    lobjFilterCriteria.CurrentMinPoints = Convert.ToInt32(pstrMinVal);
                    lobjFilterCriteria.CurrentMaxPoints = Convert.ToInt32(pstrMaxVal);
                    break;

                case 2:

                    lobjFilterCriteria.CurrentMinPrice = Convert.ToInt32(pstrMinVal);
                    lobjFilterCriteria.CurrentMaxPrice = Convert.ToInt32(pstrMaxVal);
                    break;

                case 3:
                    lobjFilterCriteria.CurrentMinDuration = Convert.ToInt32(pstrMinVal);
                    lobjFilterCriteria.CurrentMaxDuration = Convert.ToInt32(pstrMaxVal);
                    break;

                case 4:
                    lobjFilterCriteria.CurrentMinDepTime = Convert.ToInt32(pstrMinVal);
                    lobjFilterCriteria.CurrentMaxDepTime = Convert.ToInt32(pstrMaxVal);
                    break;

                case 5:
                    lobjFilterCriteria.CurrentMinArrTime = Convert.ToInt32(pstrMinVal);
                    lobjFilterCriteria.CurrentMaxArrTime = Convert.ToInt32(pstrMaxVal);
                    break;
            }
            HttpContext.Current.Session["FilterCriteria"] = lobjFilterCriteria;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx FilterSlider Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
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
            SearchRequest lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetails"] as SearchRequest;
            lstrModifySearch = JSONSerialization.Serialize(lobjSearchRequest);
            lstrDepartDate = lobjSearchRequest.SearchDetails.DepartureDate.ToString("dd/MM/yyyy");
            lstrArrivalDate = lobjSearchRequest.SearchDetails.ArrivalDate.ToString("dd/MM/yyyy");

            lobjListOfData[0] = lstrModifySearch;
            lobjListOfData[1] = lstrDepartDate;
            lobjListOfData[2] = lstrArrivalDate;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx ModifySearchData Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }
    public static HtmlLink CreateCssLink(string cssFilePath, string media)
    {
        var link = new HtmlLink();
        try
        {
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            link.Href = link.ResolveUrl(cssFilePath);
            if (string.IsNullOrEmpty(media))
            {
                media = "all";
            }

            link.Attributes.Add("media", media);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx CreateCssLink Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return link;
    }

    public static HtmlGenericControl CreateJavaScriptLink(string scriptFilePath)
    {
        var script = new HtmlGenericControl();
        try
        {
            script.TagName = "script";
            script.Attributes.Add("type", "text/javascript");
            script.Attributes.Add("src", script.ResolveUrl(scriptFilePath));
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightList.aspx CreateJavaScriptLink Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return script;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string SetPaymentMode(string pstrPaymentMode)
    {
        return HttpContext.Current.Session["SearchCurrency"].ToString();
    }
}
