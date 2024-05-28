using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.Integrations.Hotels.Entities;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FlightServicesDomestic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [ScriptMethod()]
    [WebMethod]
    public static List<string> GetAirfieldsforDomestic(string prefixText)
    {
        ABCModel lobjModel = new ABCModel();
        prefixText = prefixText.ToLower();
        AirFieldsForDomestic lobjAirfieldList = HttpContext.Current.Application["AllAirfieldsforDomestic"] as AirFieldsForDomestic;
        List<string> lobjRetValue = new List<string>();
        List<string> lobjROW = new List<string>();
        try
        {
            lobjAirfieldList.Sectors.ForEach(airfield =>
            {
                if (airfield.Name.ToLower().IndexOf(prefixText) >= 0 && airfield.IsActive)
                {
                    lobjROW.Add(airfield.Name+","+ airfield.Code);
                }
            });
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
    public static bool BookNowClick_Domestic()
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            lobjModel.LogActivity(string.Format("Flight selected for booking"), ActivityType.FlightBookingForDomestic);
            FinalFlightResult lobjFinalBookedFlightDetails = HttpContext.Current.Session["FinalBookedFlightDetails"] as FinalFlightResult;
            if (HttpContext.Current.Session["MemberDetails"] == null)
            {
                return false;
            }
            else
            {
                CreateDomesticItineraryRequest lobjItineraryRequest = new CreateDomesticItineraryRequest();
                lobjItineraryRequest.Token= ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                lobjItineraryRequest.FlightId = lobjFinalBookedFlightDetails.Departure_FlightId;
                lobjItineraryRequest.BookingId = Convert.ToInt32(HttpContext.Current.Session["DomesticFlightBookingId"]);
                if (lobjFinalBookedFlightDetails.IsReturn) {
                    lobjItineraryRequest.ReturnFlightId = lobjFinalBookedFlightDetails.Return_FlightId;
                }
                else
                {
                    lobjItineraryRequest.ReturnFlightId = string.Empty;
                }
                string lstrCurrency = lobjModel.GetDefaultCurrency();
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                lobjItineraryRequest.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                lobjItineraryRequest.PointRate= lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);
                CreateDomesticItineraryResponse lobjItineraryResponse = new CreateDomesticItineraryResponse();
                lobjItineraryResponse = lobjModel.CreateItineraryForDomesticFlights(lobjItineraryRequest);
                if(lobjItineraryResponse == null)
                {
                    return false;
                }
                else
                {
                    return lobjItineraryResponse.Status;
                }
                
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightServicesDomestic.aspx BookNowClick_Domestic Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
}