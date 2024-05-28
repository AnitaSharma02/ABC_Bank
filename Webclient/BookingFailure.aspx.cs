using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using Framework.Integrations.Hotels.Entities;
using Core.Platform.Member.Entites;
using CB.IBE.Platform.Entities;
using System.IO;
using CB.IBE.Platform.Hotels.ClientEntities;
using CB.IBE.Platform.Car.Entities;
using CB.IBE.Platform.Car.ClientEntities;

public partial class BookingFailure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

 
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string BookingFail()
    {
        ABCModel lobjModel = new ABCModel();
        string lstrTransactionReferenceCode = "NA";
        if (HttpContext.Current.Session["MemberDetails"] != null)
        {
            try
            {
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (HttpContext.Current.Session["FlightBookingResponse"] != null)
                {
                    CB.IBE.Platform.ClientEntities.BookingResponse lobjBookingResponse = new CB.IBE.Platform.ClientEntities.BookingResponse();
                    lobjBookingResponse = (CB.IBE.Platform.ClientEntities.BookingResponse)HttpContext.Current.Session["FlightBookingResponse"];
                    lstrTransactionReferenceCode = lobjBookingResponse.PNRDetails.BookingReference;
                    HttpContext.Current.Session["FlightBookingResponse"] = null;
                    return lstrTransactionReferenceCode;
                }
                else if (HttpContext.Current.Session["BookedHotel"] != null && HttpContext.Current.Session["SearchDetails"] != null && HttpContext.Current.Session["CustomerDetails"] != null)
                {
                    HotelSearchResponse lobjHotelsInfo = HttpContext.Current.Session["BookedHotel"] as HotelSearchResponse;
                    HotelSearchRequest lobjSearchRequest = HttpContext.Current.Session["SearchDetails"] as HotelSearchRequest;
                    Framework.Integrations.Hotels.Entities.Customer lobjCustomer = HttpContext.Current.Session["CustomerDetails"] as Framework.Integrations.Hotels.Entities.Customer;
                    HotelBookingResponse lobjBookingResponse = null;
                    if (HttpContext.Current.Session["BookingResponse"] != null)
                    {
                        lobjBookingResponse = HttpContext.Current.Session["BookingResponse"] as HotelBookingResponse;
                        if (lobjBookingResponse.BookingResponse.TransactionRefCode != null && lobjBookingResponse.BookingResponse.TransactionRefCode != string.Empty)
                        {
                            lstrTransactionReferenceCode = lobjBookingResponse.BookingResponse.TransactionRefCode;
                        }
                    }
                    HttpContext.Current.Session["BookedHotel"] = null;
                    HttpContext.Current.Session["SearchDetails"] = null;
                    HttpContext.Current.Session["BookingResponse"] = null;
                    HttpContext.Current.Session["CustomerDetails"] = null;
                    return lstrTransactionReferenceCode;
                }
                //else if (HttpContext.Current.Session["CarMadeBookingRequest"] != null && HttpContext.Current.Session["MemberDetails"] != null)
                //{
                //    //lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                //    //CarSearchRequest lobjCarSearchRequest = HttpContext.Current.Session["CarSearchRequest"] as CarSearchRequest;
                //    //Match lobjMatch = HttpContext.Current.Session["CarSelected"] as Match;
                //    //CarExtrasListResponse lobjCarExtrasListResponse = HttpContext.Current.Session["CarExtraListResponse"] as CarExtrasListResponse;
                //    //CarMakeBookingRequest lobjCarMakeBookingRequest = HttpContext.Current.Session["CarMadeBookingRequest"] as CarMakeBookingRequest;
                //    CarMakeBookingResponse lobjCarMakeBookingResponse = HttpContext.Current.Session["CarMakeBookingResponse"] as CarMakeBookingResponse;
                //    if (lobjCarMakeBookingResponse != null)
                //    {
                //        if (!string.IsNullOrEmpty(lobjCarMakeBookingResponse.BookingReferenceId))
                //        {
                //            lstrTransactionReferenceCode = lobjCarMakeBookingResponse.BookingReferenceId;
                //        }
                //    }
                //    // lobjUABModel.SendFailureEmail(lobjUABModel.createReceiptForFailCar(lobjCarMakeBookingResponse, lobjCarMakeBookingRequest, lobjMemberDetails, lobjCarSearchRequest, lobjMatch, lobjCarExtrasListResponse, File.ReadAllText(HttpContext.Current.Server.MapPath("Templates/CarFailureEmail.htm"))), "UABRewards Car FAILED Redemption");

                //    HttpContext.Current.Session["CarSearchRequest"] = null;
                //    HttpContext.Current.Session["Cars"] = null;
                //    HttpContext.Current.Session["CarSelected"] = null;
                //    HttpContext.Current.Session["CarExtraListResponse"] = null;
                //    HttpContext.Current.Session["CarMadeBookingRequest"] = null;
                //    HttpContext.Current.Session["CarMakeBookingResponse"] = null;
                //    return lstrTransactionReferenceCode;
                //}
                else
                    return lstrTransactionReferenceCode;
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("Booking Failure " + ex.StackTrace);
                return lstrTransactionReferenceCode;
            }

        }
        else
            return lstrTransactionReferenceCode;
    }
}