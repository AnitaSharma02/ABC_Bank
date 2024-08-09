
using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Booking.Entities;
using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.Transactions.Entites;
using Core.WebAPI.ClientHelper;
using Framework.EnterpriseLibrary.Adapters;
using Giift.ShopGateway.Client.Entities;
using GiiftPaymentGateway.Entities;
using GiiftShopGateway.Model;
using KhaltiInsurance.Entities;
using KhaltiISP.Entities;
using Newtonsoft.Json;
using ABC.Model;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CB.IBE.Platform.AirClientModel;

public partial class PaymentResponse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["MemberDetails"] == null)
            {
                string CallbackUrl = HttpUtility.UrlEncode(Encrypt("PaymentResponse.aspx"));
                HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
            }
            else
            {
                SuccessPayment();
            }
        }
    }

    private void SuccessPayment()
    {
        string lstrClientReferenceId = string.Empty;
        string lstrBookingFlag = string.Empty;
        ABCModel lobjModel = new ABCModel();
        StripePaymentDetails lobjStripePaymentDetails = Session["StripePaymentDetails"] as StripePaymentDetails;
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        PGRequest pgRequest = null;
        pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
        PGDetails lobjPGDetails = null;
        try
        {
            PaymentGatewayDetails lobjTransactionDetails = null;
            lstrClientReferenceId = lobjStripePaymentDetails.ClientReferenceId;
            bool lblUpdateStatus = false;
            List<object> lobjDict = CachingAdapter.Get(lstrClientReferenceId) as List<object>;

            Session["MemberDetails"] = lobjDict[0] as MemberDetails;
            Session["StripePaymentDetails"] = lobjDict[1] as StripePaymentDetails;
            Session["BookingFlag"] = lobjDict[2];

            Session["ShoppingCart"] = lobjDict[3] as ShoppingCart;
            Session["CheckoutAddress"] = lobjDict[4] as Giift.ShopGateway.Client.Entities.Address;

            Session["ItineraryRequest"] = lobjDict[5] as CreateItineraryRequest;
            Session["ItineraryResponse"] = lobjDict[6] as CreateItineraryResponse;
            Session["FlightSearchDetails"] = lobjDict[7] as SearchRequest;
            Session["FlightBookedFailedResponse"] = lobjDict[8] as BookingResponse;

            Session["BookedHotel"] = lobjDict[09] as HotelSearchResponse;
            Session["CustomerDetails"] = lobjDict[10] as Framework.Integrations.Hotels.Entities.Customer;
            Session["SearchDetails"] = lobjDict[11] as HotelSearchRequest;
            Session["HotelBookingPaymentDetails"] = lobjDict[12] as BookingPaymentDetails;

            //Session["ExperienceBookingDetails"] = lobjDict[13] as OrderStatusResponse;

            Session["InsuranceUserDetails"] = lobjDict[14] as InsuranceUserDetailsResponse;

            Session["ISPUserDetailsResponse"] = lobjDict[15] as ISPUserDetailsResponse;

            Session["CreateDomesticBookingResponse"] = lobjDict[16] as CreateDomesticBookingResponse;
            CachingAdapter.Remove(lstrClientReferenceId);

            string PaymentStatus = string.Empty;
            lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
            if (lobjPGDetails != null
            && lobjPGDetails.code == (int)HttpStatusCode.OK
            && lobjPGDetails.success == 1)
            {
                PaymentStatus = lobjPGDetails.data[0].orderStatus;
                if (HttpContext.Current.Session["BookingFlag"] != null)
                {
                    lstrBookingFlag = HttpContext.Current.Session["BookingFlag"].ToString();
                }
                if (PaymentStatus.ToLower() == "paid")//InProcess
                {
                    if (lstrBookingFlag.ToLower().Equals("digitalproduct"))
                    {
                        ProccedDigital();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("physicalproduct"))
                    {
                        ProccedPhysical();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("flight"))
                    {
                        BookFlight();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("hotel"))
                    {
                        BookHotel();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("experience"))
                    {
                        BookPackage();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("insuranceserviceproviders"))
                    {
                        BookInsurance();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("internetserviceproviders"))
                    {
                        BookISP();
                    }
                    else if (lstrBookingFlag.ToLower().Equals("domesticflight"))
                    {
                        BookDomesticFlight();
                    }
                    HttpContext.Current.Session["AvailablePoints"] = null;
                }
                else
                {
                   
                    LoggingAdapter.WriteLog("PaymentResponse PaymentStatus Failed. Rollback redeem points start");
                    lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjStripePaymentDetails.ProductName);
                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                    HttpContext.Current.Session["AvailablePoints"] = null;
                    if (lstrBookingFlag.ToLower().Equals("digitalproduct"))
                    {
                        Response.Redirect("/OrderStatus.aspx?Status=false", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("physicalproduct"))
                    {
                        Response.Redirect("/OrderStatus.aspx?Status=false", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("flight"))
                    {
                        Response.Redirect("BookingFailure.aspx", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("hotel"))
                    {
                        Response.Redirect("BookingFailure.aspx", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("experience"))
                    {
                        Response.Redirect("BookingFailure.aspx", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("insuranceserviceproviders"))
                    {
                        Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("internetserviceproviders"))
                    {
                        Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                    }
                    else if (lstrBookingFlag.ToLower().Equals("domesticflight"))
                    {
                        Response.Redirect("BookingFailure.aspx", false);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PaymentResponse SuccessPayment Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjStripePaymentDetails.ProductName);
            Response.Redirect("BookingFailure.aspx", false);
        }
    }

    private void BookPackage()
    {
        ABCModel lobjModel = new ABCModel();
        PGRequest pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
        BeMyGuest.Entities.BookingRequest bookingRequest = HttpContext.Current.Session["ExperienceBookingRequest"] as BeMyGuest.Entities.BookingRequest;
        try
        {
            BeMyGuest.Entities.BookingResponse bookingResponse = lobjModel.ExperienceBooking(bookingRequest);
            if (bookingResponse.success == 1)
            {
                HttpContext.Current.Session["PackageBookingId"] = bookingResponse.bookingData.uuid;

                SendExperienceEmail(bookingRequest, bookingResponse);
                Response.Redirect("ExperienceProductStatus.aspx?Success=true", false);
            }
            else
            {
                if (!string.IsNullOrEmpty(pgRequest.orderId) && bookingRequest.totalAmount != 0)
                {
                    lobjModel.RollBackTransaction(pgRequest.orderId, bookingRequest.memberId, bookingRequest.titleName);
                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(bookingRequest.totalAmount), "refund it please");
                }
                Response.Redirect("ExperienceProductStatus.aspx?Success=false", false);
            }
        }
        catch (Exception ex)
        {
            lobjModel.RollBackTransaction(pgRequest.orderId, bookingRequest.memberId, bookingRequest.titleName);
            LoggingAdapter.WriteLog("PaymentResponse BookPackage Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }

    private void SendExperienceEmail(BeMyGuest.Entities.BookingRequest bookingRequest, BeMyGuest.Entities.BookingResponse bookingResponse)
    {
        ABCModel lobjModel = new ABCModel();
        try
        {
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            if (!string.IsNullOrEmpty(bookingRequest.customer.email))
            {
                StringBuilder sbAdditionalInfoHtml = new StringBuilder();
                if (bookingResponse.bookingData.options.Count > 0)
                {
                    int optionsItemIndex = 0;
                    foreach (var optionsItem in bookingResponse.bookingData.options)
                    {
                        if (optionsItemIndex % 2 == 0)
                        {
                            sbAdditionalInfoHtml.Append("<tr>");
                            sbAdditionalInfoHtml.Append("<td width='20%' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            sbAdditionalInfoHtml.Append("<strong>" + optionsItem.label + ":</strong></td>");
                            sbAdditionalInfoHtml.Append("<td width='30%' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("datetime"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("time")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("select")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Equals("freelunch"))
                            {
                                if (Convert.ToInt32(optionsItem.value) == 1)
                                {
                                    sbAdditionalInfoHtml.Append("Yes");
                                }
                                else
                                {
                                    sbAdditionalInfoHtml.Append("No");
                                }
                            }
                            else
                            {
                                sbAdditionalInfoHtml.Append(optionsItem.value);
                            }
                            sbAdditionalInfoHtml.Append("</td>");
                        }
                        else
                        {
                            sbAdditionalInfoHtml.Append("<td width='15%' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            sbAdditionalInfoHtml.Append("<strong>" + optionsItem.label + ":</strong></td>");
                            sbAdditionalInfoHtml.Append("<td width='30%' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("datetime"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("time")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("select")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                sbAdditionalInfoHtml.Append(optionsItem.value);
                            }
                            sbAdditionalInfoHtml.Append("</td>");
                            sbAdditionalInfoHtml.Append("</tr>");
                        }
                        if (optionsItemIndex % 2 == 0 && bookingResponse.bookingData.options.Count == (optionsItemIndex + 1))
                        {
                            sbAdditionalInfoHtml.Append("<td width='45%' colspan='2' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'></td>");
                            sbAdditionalInfoHtml.Append("</tr>");
                        }
                        optionsItemIndex++;
                    }
                }
                else
                {
                    sbAdditionalInfoHtml.Append("<tr>");
                    sbAdditionalInfoHtml.Append("<td width='100%' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>No additional info available.</td>");
                    sbAdditionalInfoHtml.Append("</tr>");
                }
                StringBuilder sbPickup_MeetingPointInformationHtml = new StringBuilder();
                if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingTime)
                   || !string.IsNullOrEmpty(bookingResponse.bookingData.meetingAddress)
                   || !string.IsNullOrEmpty(bookingResponse.bookingData.meetingLocation))
                {
                    if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingTime))
                    {
                        sbPickup_MeetingPointInformationHtml.Append("<tr>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                        sbPickup_MeetingPointInformationHtml.Append("<strong>Time:</strong>");
                        sbPickup_MeetingPointInformationHtml.Append("</td>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='613' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>" + bookingResponse.bookingData.meetingTime + "</td>");
                        sbPickup_MeetingPointInformationHtml.Append("</tr>");
                    }
                    if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingAddress))
                    {
                        sbPickup_MeetingPointInformationHtml.Append("<tr>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                        sbPickup_MeetingPointInformationHtml.Append("<strong>Address:</strong>");
                        sbPickup_MeetingPointInformationHtml.Append("</td>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='613' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>" + bookingResponse.bookingData.meetingAddress + "</td>");
                        sbPickup_MeetingPointInformationHtml.Append("</tr>");
                    }
                    if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingLocation))
                    {
                        sbPickup_MeetingPointInformationHtml.Append("<tr>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                        sbPickup_MeetingPointInformationHtml.Append("<strong>Location:</strong>");
                        sbPickup_MeetingPointInformationHtml.Append("</td>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='613' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>" + bookingResponse.bookingData.meetingLocation + "</td>");
                        sbPickup_MeetingPointInformationHtml.Append("</tr>");
                    }
                }
                else
                {
                    sbPickup_MeetingPointInformationHtml.Append("<tr>");
                    sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                    sbPickup_MeetingPointInformationHtml.Append("No pickup/meeting point information available.");
                    sbPickup_MeetingPointInformationHtml.Append("</td>");
                    sbPickup_MeetingPointInformationHtml.Append("</tr>");
                }
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                List<string> emailParameters = new List<string>();
                string PGTranPct = Convert.ToString(ConfigurationManager.AppSettings["PGTranPct"]);
                string WebsiteUrl = Convert.ToString(ConfigurationManager.AppSettings["WebsiteUrl"]);
                emailParameters.Add(string.Format("{0} {1}", bookingRequest.customer.firstName, bookingRequest.customer.lastName));//0
                emailParameters.Add(bookingResponse.bookingData.code);//1
                emailParameters.Add(bookingResponse.bookingData.prodtitle);//2
                emailParameters.Add(bookingResponse.bookingData.productTypeTitle);//3
                emailParameters.Add(DateTime.Parse(bookingResponse.bookingData.createdAt).ToLocalTime().ToString("dd/MM/yyyy hh:mm tt"));//4
                emailParameters.Add(DateTime.Parse(bookingResponse.bookingData.updatedAt).ToLocalTime().ToString("dd/MM/yyyy hh:mm tt"));//5
                emailParameters.Add(DateTime.Parse(bookingResponse.bookingData.arrivalDate).ToString("dd MMM yyyy"));//6
                emailParameters.Add(string.IsNullOrEmpty(bookingResponse.bookingData.timeSlot) ? "N/A" : string.Format("{0} hrs", bookingResponse.bookingData.timeSlot));//7
                emailParameters.Add(bookingResponse.bookingData.adults > 0 ? string.Format("{0} x {1}", bookingResponse.bookingData.adults,
                                    FormatCurrency(Math.Ceiling(bookingResponse.bookingData.amountBreakdown.FindAll(x => x.name.ToLower().Equals("adult")).FirstOrDefault().convertedAmount)
                                    , bookingResponse.bookingData.convertedCurrency)) : "0");//8
                emailParameters.Add(bookingResponse.bookingData.children > 0 ? string.Format("{0} x {1}", bookingResponse.bookingData.children,
                                    FormatCurrency(Math.Ceiling(bookingResponse.bookingData.amountBreakdown.FindAll(x => x.name.ToLower().Equals("child")).FirstOrDefault().convertedAmount)
                                    , bookingResponse.bookingData.convertedCurrency)) : "0");//9
                emailParameters.Add(bookingResponse.bookingData.seniors > 0 ? string.Format("{0} x {1}", bookingResponse.bookingData.seniors,
                                    FormatCurrency(Math.Ceiling(bookingResponse.bookingData.amountBreakdown.FindAll(x => x.name.ToLower().Equals("senior")).FirstOrDefault().convertedAmount)
                                    , bookingResponse.bookingData.convertedCurrency)) : "0");//10
                emailParameters.Add(FormatCurrency(Math.Ceiling(bookingResponse.bookingData.grandTotalAmount), bookingResponse.bookingData.convertedCurrency));//11
                emailParameters.Add(bookingResponse.bookingData.firstName);//12
                emailParameters.Add(bookingResponse.bookingData.lastName);//13
                emailParameters.Add(bookingResponse.bookingData.email);//14
                emailParameters.Add(bookingResponse.bookingData.phone);//15
                emailParameters.Add("Cancellations are non refundable.");//16
                emailParameters.Add(DateTime.Now.Year.ToString());//17
                emailParameters.Add(textInfo.ToTitleCase(bookingResponse.bookingData.status));//18
                emailParameters.Add(sbAdditionalInfoHtml.ToString());//19
                emailParameters.Add(bookingResponse.bookingData.prodavailaddress);//20
                emailParameters.Add(bookingResponse.bookingData.uuid);//21
                emailParameters.Add(FormatCurrency(bookingResponse.bookingData.grandTotalAmount * (Convert.ToDecimal(PGTranPct) / 100), bookingResponse.bookingData.convertedCurrency, true));//22
                emailParameters.Add(FormatCurrency(bookingResponse.bookingData.grandTotalAmount + (bookingResponse.bookingData.grandTotalAmount * (Convert.ToDecimal(PGTranPct) / 100)), bookingResponse.bookingData.convertedCurrency, true));//23
                emailParameters.Add(WebsiteUrl);//24
                emailParameters.Add(sbPickup_MeetingPointInformationHtml.ToString());//25
                bool ceresponse = lobjModel.InsertEmailDetails(emailParameters,
                     bookingRequest.customer.email, "ExperiencesBooked", bookingRequest.memberId, lobjProgramDefinition.ProgramId);
                if (ceresponse)
                {
                    LoggingAdapter.WriteLog("PaymentResponse Page Experience Email Send Successfully");
                }
                else
                {
                    LoggingAdapter.WriteLog("PaymentResponse Page Experience Email Send Failed");
                }
            }
            else
            {
                #region Logging
                LoggingAdapter.WriteLog(
                string.Format("PaymentResponse Experiences customerEmailId is null; Date - {0} | BookingCode - {1}" +
                " | BookingUUID - {2};",
                DateTime.Now, bookingResponse.bookingData.code, bookingResponse.bookingData.uuid));
                #endregion
            }
        }
        catch (Exception ex)
        {
            #region Logging
            LoggingAdapter.WriteLog(
            string.Format("PaymentResponse Experiences InsertEmailDetails Exception; Date - {0} | EX Message - {1} | EX StackTrace - {2} | EX InnerException - {3};",
            DateTime.Now, ex.Message, ex.StackTrace, ex.InnerException));
            #endregion
        }
    }

    private void BookHotel()
    {
        try
        {
            StripePaymentDetails lobjStripePaymentDetails = Session["StripePaymentDetails"] as StripePaymentDetails;
            ABCModel lobjModel = new ABCModel();
            PaymentGatewayDetails lobjTransactionDetails = null;
            ShopModel lobjShopModel = new ShopModel();

            PGRequest pgRequest = null;
            pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
            PGDetails lobjPGDetails = null;
            lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
            if (HttpContext.Current.Session["BookedHotel"] != null && HttpContext.Current.Session["CustomerDetails"] != null && HttpContext.Current.Session["SearchDetails"] != null)
            {
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                HotelSearchResponse lobjSearchResponse = HttpContext.Current.Session["BookedHotel"] as HotelSearchResponse;
                HotelSearchResponse lobjHotelBooked = new HotelSearchResponse();
                lobjHotelBooked = lobjSearchResponse;
                HotelSearchRequest lobjSearchRequest = HttpContext.Current.Session["SearchDetails"] as HotelSearchRequest;
                Framework.Integrations.Hotels.Entities.Customer lobjCustomer = HttpContext.Current.Session["CustomerDetails"] as Framework.Integrations.Hotels.Entities.Customer;
                List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;
                HotelBookingResponse lobjBookingResponse = lobjModel.BookForHotel(lobjMemberDetails, lobjSearchResponse.SearchResponse.hotels.hotel[0], lobjSearchRequest, lobjCustomer, lobjListOfRedemptionDetails);
                HttpContext.Current.Session["BookingResponse"] = lobjBookingResponse;       
                if (lobjBookingResponse != null && lobjBookingResponse.BookingResponse.bookingid != null && lobjBookingResponse.BookingResponse.confirmationnumber != null && lobjBookingResponse.BookingResponse.bookingid != string.Empty && lobjBookingResponse.BookingResponse.confirmationnumber != string.Empty)
                {
                    lobjModel.LogActivity(string.Format("HotelBooking Success; HotelId-:{0};HotelName-:{1}; BookingId-:{2};Total Amount-:{3};", lobjHotelBooked.SearchResponse.hotels.hotel[0].hotelid, lobjHotelBooked.SearchResponse.hotels.hotel[0].basicinfo.hotelname, lobjBookingResponse.BookingId, lobjHotelBooked.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalBaseAmount), ActivityType.HotelBooking);
                    HttpContext.Current.Session["BookedHotel"] = null;
                    HttpContext.Current.Session["HotelBooked"] = lobjHotelBooked;

                    Response.Redirect("HotelVoucher.aspx", false);
                }
                else
                {
                    if (!string.IsNullOrEmpty(pgRequest.orderId) && lobjStripePaymentDetails.ReqRedeemPoint != "0")
                    {
                        lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.hotelname);
                        lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                    }

                    lobjModel.LogActivity(string.Format("HotelBooking Failed; HotelId-:{0};HotelName-:{1}; BookingId-:{2};Total Amount-:{3};", lobjHotelBooked.SearchResponse.hotels.hotel[0].hotelid, lobjSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.hotelname, lobjBookingResponse.BookingId, lobjHotelBooked.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalBaseAmount), ActivityType.HotelBooking);
                    Response.Redirect("BookingFailure.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {

            LoggingAdapter.WriteLog("PaymentResponse BookHotel Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }

    private void BookFlight()
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            PaymentGatewayDetails lobjTransactionDetails = null;
            ShopModel lobjShopModel = new ShopModel();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            SearchRequest lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetails"] as SearchRequest;
            CreateItineraryResponse lobjCreateItineraryResponse = HttpContext.Current.Session["ItineraryResponse"] as CreateItineraryResponse;
            CreateItineraryRequest lobjCreateItineraryRequest = HttpContext.Current.Session["ItineraryRequest"] as CreateItineraryRequest;
            StripePaymentDetails lobjStripePaymentDetails = null;
            lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];

            PGRequest pgRequest = null;
            pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
            PGDetails lobjPGDetails = null;
            lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
            if (Session["MemberDetails"] != null && Session["ItineraryRequest"] != null && Session["ItineraryResponse"] != null)
            {
                int SupplierID = Convert.ToInt32(ConfigurationManager.AppSettings["SupplierID"]);
                List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;
                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;
                BookingRequest lobjBookingRequest = new BookingRequest();
                lobjBookingRequest.IsItineraryDateChangeAllowed = true;
                lobjBookingRequest.SupplierDetails.Id = lobjRefererDetails.RefererSupplierProperties.SupplierId;
                lobjBookingRequest.ItineraryDetails.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                lobjBookingRequest.ItineraryDetails = lobjCreateItineraryResponse.ItineraryDetails;
                lobjBookingRequest.ItineraryDetails.RefererDetails = lobjSearchRequest.RefererDetails;
                lobjBookingRequest.PointRate = lobjCreateItineraryRequest.PointRate;
                lobjBookingRequest.IPAddress = lobjSearchRequest.IPAddress;
                lobjBookingRequest.SessionId = lobjCreateItineraryRequest.SessionId;
                BookingResponse lobjBookingResponse = lobjModel.BookForFlight(lobjBookingRequest, lobjMemberDetails, lobjListOfRedemptionDetails);

                if (lobjBookingResponse != null && lobjBookingResponse.PNRDetails.TripId != null && lobjBookingResponse.PNRDetails.TripId != string.Empty && lobjBookingResponse.PNRDetails.Status.Equals(1))
                {
                    ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
                    lobjItineraryDetails = lobjBookingResponse.PNRDetails.ItineraryDetails;
                    lobjItineraryDetails.FareDetails = lobjBookingResponse.PNRDetails.ItineraryDetails.FareDetails;
                    lobjItineraryDetails.TravelerInfo = lobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo;
                    lobjItineraryDetails.ItineraryReference = lobjBookingResponse.PNRDetails.BookingReference;
                    HttpContext.Current.Session["FlightBookedResponse"] = lobjBookingResponse;
                    HttpContext.Current.Session["FlightBooked"] = lobjItineraryDetails;
                    HttpContext.Current.Session["ItineraryResponse"] = null;
                    HttpContext.Current.Session["ItineraryRequest"] = null;
                    HttpContext.Current.Session["ReviewFlightDetails"] = null;

                    lobjModel.LogActivity(string.Format("FlightBooking Success; BookingId-{0}; Destination-:{1}; Total Amount-: {2}; ArrivalDate-:{3}; DepartureDate-:{4};Adult-:{5};Children-:{6};Infrant-:{7};", lobjBookingResponse.BookingId, lobjCreateItineraryRequest.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryRequest.ItineraryDetails.DestinationLocation, lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalBaseFare, lobjCreateItineraryResponse.ItineraryDetails.ArrivalDate, lobjCreateItineraryResponse.ItineraryDetails.DepartureDate, lobjCreateItineraryResponse.ItineraryDetails.Adults, lobjCreateItineraryResponse.ItineraryDetails.Childrens, lobjCreateItineraryResponse.ItineraryDetails.Infants), ActivityType.FlightBooking);

                    Response.Redirect("AirReceipt.aspx", false);
                }
                else
                {
                    lobjModel.LogActivity(string.Format("FlightBooking Failed; BookingId-{0}; Destination-:{1}; Total Amount-: {2}; ArrivalDate-:{3}; DepartureDate-:{4};Adult-:{5};Children-:{6};Infrant-:{7};", lobjBookingResponse.BookingId, lobjCreateItineraryRequest.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryRequest.ItineraryDetails.DestinationLocation, lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalBaseFare, lobjCreateItineraryResponse.ItineraryDetails.ArrivalDate, lobjCreateItineraryResponse.ItineraryDetails.DepartureDate, lobjCreateItineraryResponse.ItineraryDetails.Adults, lobjCreateItineraryResponse.ItineraryDetails.Childrens, lobjCreateItineraryResponse.ItineraryDetails.Infants), ActivityType.FlightBooking);

                    if (!string.IsNullOrEmpty(pgRequest.orderId) && lobjStripePaymentDetails.ReqRedeemPoint != "0")
                    {
                        lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjCreateItineraryResponse.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryResponse.ItineraryDetails.DestinationLocation);
                        lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                    }

                    HttpContext.Current.Session["FlightBookedFailedResponse"] = lobjBookingResponse;
                    Response.Redirect("BookingFailure.aspx", false);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(pgRequest.orderId) && lobjStripePaymentDetails.ReqRedeemPoint != "0")
                {
                    lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjCreateItineraryResponse.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryResponse.ItineraryDetails.DestinationLocation);
                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                }

                Session["ItineraryResponse"] = null;
                Session["ItineraryRequest"] = null;
                Session["ReviewFlightDetails"] = null;
                Session["BookingFlag"] = null;
                Response.Redirect("BookingFailure.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PaymentResponse BookFlight Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }

    private void ProccedPhysical()
    {
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
        PGRequest pgRequest = null;
        pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
        StripePaymentDetails lobjStripePaymentDetails = Session["StripePaymentDetails"] as StripePaymentDetails;
        try
        {
            Giift.ShopGateway.Client.Entities.Address lobjdeliveryaddress = HttpContext.Current.Session["CheckoutAddress"] as Giift.ShopGateway.Client.Entities.Address;
          

            PaymentGatewayDetails lobjTransactionDetails = null;
            ShopModel lobjShopModel = new ShopModel();
            bool lblRollback = false;

            var ShippingMethods = lobjShopModel.GetAvailableShippingRates(Cart.Id);
            Cart = lobjShopModel.UpdateCartShipment(Cart, ShippingMethods.FirstOrDefault(), lobjdeliveryaddress);
            var PaymentMethods = lobjShopModel.GetAvailablePaymentMethods(Cart.Id);

            Cart = lobjShopModel.UpdateCartPayment(Cart, PaymentMethods.FirstOrDefault(), lobjStripePaymentDetails.ClientReferenceId, lobjStripePaymentDetails);

            var Order = lobjShopModel.CreateOrderFromCart(Cart, lobjStripePaymentDetails);
            PGDetails lobjPGDetails = null;
            lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
            if (Order == null)
            {
                lblRollback = true;
                LoggingAdapter.WriteLog("CheckoutShop Order Null first call RedeemMilesResponse" + lobjStripePaymentDetails.ClientReferenceId + " Cart " + Cart.ToString());
            }
            else
            {
                CustomerOrder lobjorder = null;
                lobjorder = lobjShopModel.GetOrderByNumber(Order.Number);
                if (lobjorder == null)
                {
                    LoggingAdapter.WriteLog(" CheckoutShop Order Null Second call RedeemMilesResponse" + lobjStripePaymentDetails.ClientReferenceId + " Cart " + Cart.ToString());
                    lblRollback = true;
                }
                else
                {
                    if (lobjorder.Status == "Placed" || lobjorder.Status.ToLower().Equals("completed"))
                    {
                        LoggingAdapter.WriteLog("Checkout Order response not null");
                        string lstrHtmlContent = string.Empty;
                        string lstrShippingAddressContent = string.Empty;
                        List<string> lstrEmailParameters = new List<string>();
                        lstrHtmlContent += "<table border=\"0\" style=\"width:100%;font-size:12px;font-family:arial;padding:2%;\">"
                                        + "<tbody>"
                                        + "<tr style=\"background-color:#b9babe;text-align:center\">"
                                        + "<th>Name</th>"
                                        + "<th>Price</th>"
                                        + "<th>Quantity</th>"
                                        + "<th>Total</th>"
                                        + "</tr>";

                        for (int i = 0; i < Order.Items.Count; i++)
                        {
                            lstrHtmlContent += "<tr style=\"background-color:#ebecee; text-align:center\">"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:left\">" + Order.Items[i].Name + "</td>"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ListPrice.Amount), "Points") + "</td>"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:center\">" + Order.Items[i].Quantity + "</td>"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ExtendedPrice.Amount), "Points") + "</td>"
                                            + "</tr>";
                        }

                        lstrHtmlContent += "<tr style=\"text-align:right\">"
                                        + "<td></td>"
                                        + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Sub-Total:</strong></td>"
                                        + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.SubTotal.Amount), "Points") + "</strong></td>"
                                        + "</tr>"

                                        + "<tr style=\"text-align:right\">"
                                        + "<td></td>"
                                        + "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Shipping:</strong></td>"
                                        + "<td style = \"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.ShippingTotal.Amount), "Points") + "</strong></td>"
                                        + "</tr>"

                                        + "<tr style=\"text-align:right\">"
                                        + "<td></td>"
                                        + "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Tax:</strong></td>"
                                        + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.TaxTotal.Amount), "Points") + "</strong></td>"
                                        + "</tr>"

                                        + "<tr style=\"text-align:right\">"
                                        + "<td></td>"
                                        + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Order Total:</strong></td>"
                                        + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points") + "</strong></td>"
                                        + "</tr>"

                                        + "</tbody>"
                                        + "</table>";

                        string CustomerName = string.Empty;
                        if (string.IsNullOrEmpty(Order.CustomerName))
                        {
                            CustomerName = Convert.ToString(lobjMemberDetails.FullName);
                        }
                        else
                        {
                            CustomerName = Convert.ToString(Order.CustomerName);
                        }
                        lstrShippingAddressContent += "<table  cellpadding='5' cellspacing='0' border='0' style='width:100%;font-size:12px;font-family:arial;padding:2%;border: 1px solid #333333;'>";
                        lstrShippingAddressContent += "<tr style='background: #b9babe' style='background-color:#b9babe;text-align:center'>";
                        lstrShippingAddressContent += "<th colspan='2'>Customer Details</th>";
                        lstrShippingAddressContent += "</tr>";
                        lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";
                        lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>Name</td>";
                        lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + CustomerName + "</td>";
                        lstrShippingAddressContent += "</tr>";

                        //Shipping Address
                        if (Order.ShippingAddress != null)
                        {
                            lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";
                            lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'><strong> Shipping Address </strong></td>";
                            lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + Convert.ToString(Order.ShippingAddress.Line1 + " " + Order.ShippingAddress.Line2 + " " + Order.ShippingAddress.CountryName + " " + Order.ShippingAddress.City + " " + Order.ShippingAddress.PostalCode) + "</td>";
                            lstrShippingAddressContent += "</tr>";
                        }
                        lstrShippingAddressContent += "</table>";
                        DateTime ldtCreatedDate = DateTime.Parse(Convert.ToString(Order.CreatedDate));

                        dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                        string lsrtTemplateLangCode = "";
                        if (lobjMemberDetails.PreferredLanguage == "EN")
                        {
                            dynamicCls.event_name = "Shop_Order";
                        }
                        if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                        {
                            lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                            dynamicCls.event_name = lsrtTemplateLangCode + "Shop_Order";
                        }
                        dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                        dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                        dynamicCls.to_email = lobjMemberDetails.Email;
                        dynamicCls.full_name = lobjMemberDetails.FullName;
                        dynamicCls.order_no = Order.Number;
                        dynamicCls.order_date = Convert.ToString(ldtCreatedDate.ToLocalTime());
                        dynamicCls.order_details = lstrHtmlContent;
                        dynamicCls.order_shipping_details = lstrShippingAddressContent;
                        dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                        dynamicCls.point_issued = lobjModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points");
                        Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                        IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                        foreach (var key in dict)
                        {
                            lobjDictionary.Add(key.Key, key.Value);
                        }
                        string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                        lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                        LoggingAdapter.WriteLog("Checkout Proceed send email ShopOrderPlaced success");
                        HttpContext.Current.Session["ShoppingCart"] = null;
                    }
                    else
                    {
                        lblRollback = true;
                        lobjShopModel.UpdateCustomerOrder(lobjorder, "Failed",lobjStripePaymentDetails);
                        LoggingAdapter.WriteLog("CheckoutShop page RollBackTransaction " + lobjStripePaymentDetails.ClientReferenceId + " Cart " + Cart.Items[0].Name.ToString());
                    }
                }
            }
            if (lblRollback == true)
            {
                if (!string.IsNullOrEmpty(pgRequest.orderId) && lobjStripePaymentDetails.ReqRedeemPoint != "0")
                {
                    lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Cart.Items[0].Name.ToString());
                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                }


                Response.Redirect("/OrderStatus.aspx?Status=false", false);
            }
            else
            {
                Response.Redirect("/OrderStatus.aspx?Status=true", false);
            }
        }
        catch (Exception ex)
        {
            lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Cart.Items[0].Name.ToString());
            LoggingAdapter.WriteLog("PaymentResponse ProccedPhysical Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }

    private void ProccedDigital()
    {
        ABCModel lobjModel = new ABCModel();
        PGRequest pgRequest = null;
        pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
        StripePaymentDetails lobjStripePaymentDetails = Session["StripePaymentDetails"] as StripePaymentDetails;
        try
        {
            PaymentGatewayDetails lobjTransactionDetails = null;
            ShopModel lobjShopModel = new ShopModel();


            
            bool lblRollback = false;

            var ShippingMethods = lobjShopModel.GetAvailableShippingRates(Cart.Id);
            Cart = lobjShopModel.UpdateCartShipment(Cart, ShippingMethods.FirstOrDefault(), null);
            var PaymentMethods = lobjShopModel.GetAvailablePaymentMethods(Cart.Id);

            Cart = lobjShopModel.UpdateCartPayment(Cart, PaymentMethods.FirstOrDefault(), lobjStripePaymentDetails.ClientReferenceId, lobjStripePaymentDetails);

            var Order = lobjShopModel.CreateOrderFromCart(Cart, lobjStripePaymentDetails);


            PGDetails lobjPGDetails = null;
            lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
            if (Order == null)
            {
                lblRollback = true;
                LoggingAdapter.WriteLog("PurchaseShopDigital Order Null first call RedeemMilesResponse" + lobjStripePaymentDetails.ClientReferenceId + " Cart " + Cart.ToString());
            }
            else
            {
                CustomerOrder lobjorder = null;
                lobjorder = lobjShopModel.GetOrderByNumber(Order.Number);
                if (lobjorder.Status == "Placed" || lobjorder.Status.ToLower().Equals("completed"))
                {
                    string lstrHtmlContent = string.Empty;
                    string lstrShippingAddressContent = string.Empty;
                    List<string> lstrEmailParameters = new List<string>();

                    lstrHtmlContent += "<table border=\"0\" style=\"width:100%;font-size:12px;font-family:arial;padding:2%;\">"
                                    + "<tbody>"
                                    + "<tr style=\"background-color:#b9babe;text-align:center\">"
                                    + "<th>Name</th>"
                                    + "<th>Price</th>"
                                    + "<th>Quantity</th>"
                                    + "<th>Total</th>"
                                    + "</tr>";
                    for (int i = 0; i < Order.Items.Count; i++)
                    {
                        lstrHtmlContent += "<tr style=\"background-color:#ebecee; text-align:center\">"
                                        + "<td style=\"padding:0.6em 0.4em; text-align:left\">" + Order.Items[i].Name + "</td>"
                                        + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ListPrice.Amount), "Points") + "</td>"
                                        + "<td style=\"padding:0.6em 0.4em; text-align:center\">" + Order.Items[i].Quantity + "</td>"
                                        + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ExtendedPrice.Amount), "Points") + "</td>"
                                        + "</tr>";
                    }
                    lstrHtmlContent += "<tr style=\"text-align:right\">"
                                    + "<td></td>"
                                    + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Sub-Total:</strong></td>"
                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.SubTotal.Amount), "Points") + "</strong></td>"
                                    + "</tr>"

                                    + "<tr style=\"text-align:right\">"
                                    + "<td></td>"
                                    + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Order Total:</strong></td>"
                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points") + "</strong></td>"
                                    + "</tr>"

                                    + "</tbody>"
                                    + "</table>";
                    string CustomerName = string.Empty;
                    if (string.IsNullOrEmpty(Order.CustomerName))
                    {
                        CustomerName = Convert.ToString(lobjMemberDetails.FullName);
                    }
                    else
                    {
                        CustomerName = Convert.ToString(Order.CustomerName);
                    }
                    lstrShippingAddressContent += "<table  cellpadding='5' cellspacing='0' border='0' style='width:100%;font-size:12px;font-family:arial;padding:2%;border: 1px solid #333333;'>";

                    lstrShippingAddressContent += "<tr style='background: #b9babe' style='background-color:#b9babe;text-align:center'>";
                    lstrShippingAddressContent += "<th colspan='2'>Customer Details</th>";

                    lstrShippingAddressContent += "</tr>";


                    lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";

                    lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>Name</td>";

                    lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + CustomerName + "</td>";

                    lstrShippingAddressContent += "</tr>";

                    //Shipping Address
                    if (Order.ShippingAddress != null)
                    {
                        lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";

                        lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'><strong> Shipping Address </strong></td>";

                        lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + Convert.ToString(Order.ShippingAddress.Line1 + " " + Order.ShippingAddress.Line2 + ", " + ((Order.ShippingAddress.City == "undefined") ? "" : Order.ShippingAddress.City) + ", " + Order.ShippingAddress.CountryName + ", " + Order.ShippingAddress.PostalCode) + "</td>";

                        lstrShippingAddressContent += "</tr>";
                    }


                    lstrShippingAddressContent += "</table>";
                    DateTime ldtCreatedDate = DateTime.Parse(Convert.ToString(Order.CreatedDate));
                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                    string lsrtTemplateLangCode = "";
                    if (lobjMemberDetails.PreferredLanguage == "EN")
                    {
                        dynamicCls.event_name = "Shop_Order";
                    }
                    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                        dynamicCls.event_name = lsrtTemplateLangCode + "Shop_Order";
                    }
                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                    dynamicCls.to_email = lobjMemberDetails.Email;
                    dynamicCls.full_name = lobjMemberDetails.FullName;
                    dynamicCls.order_no = Order.Number;
                    dynamicCls.order_date = Convert.ToString(ldtCreatedDate.ToLocalTime());
                    dynamicCls.order_details = lstrHtmlContent;
                    dynamicCls.order_shipping_details = lstrShippingAddressContent;
                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                    dynamicCls.point_issued = lobjModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points");
                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                    foreach (var key in dict)
                    {
                        lobjDictionary.Add(key.Key, key.Value);
                    }
                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                    HttpContext.Current.Session["ShoppingCart"] = null;

                }
                else
                {
                    lobjShopModel.UpdateCustomerOrder(lobjorder, "Failed",lobjStripePaymentDetails);
                    LoggingAdapter.WriteLog("PurchaseShopDigital Order status != Placed or Completed");

                }
            }

            if (lblRollback == true)
            {
                if (!string.IsNullOrEmpty(pgRequest.orderId) && lobjStripePaymentDetails.ReqRedeemPoint != "0")
                {
                    lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Cart.Items[0].Name.ToString());
                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                }

                Response.Redirect("/OrderStatus.aspx?Status=false", false);
            }
            else
            {
                Response.Redirect("/OrderStatus.aspx?Status=true", false);
            }

        }
        catch (Exception ex)
        {
            lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Cart.Items[0].Name.ToString());
            LoggingAdapter.WriteLog("PaymentResponse ProccedDigital Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }

    private void BookInsurance()
    {
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        PGRequest pgRequest = null;
        pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
        InsuranceUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["InsuranceUserDetails"] as InsuranceUserDetailsResponse;
        try
        {
            string lstrResponse = string.Empty;
            LoggingAdapter.WriteLog("Booking Insurance Start");
            LoggingAdapter.WriteLog((HttpContext.Current.Session["InsuranceUserDetails"] != null).ToString());
            InsurancePaymentRequestResponse lobjPaymentResponse = new InsurancePaymentRequestResponse();
            StripePaymentDetails lobjStripePaymentDetails = null;
            PaymentGatewayDetails lobjTransactionDetails = null;
            ShopModel lobjShopModel = new ShopModel();
            lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];
            PGDetails lobjPGDetails = null;
            string serviceCode = Convert.ToString(HttpContext.Current.Session["InsuranceServiceCode"]);
            lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
            if (lobjMemberDetails != null)
            {
                if (HttpContext.Current.Session["InsuranceUserDetails"] != null)
                {
                    LoggingAdapter.WriteLog("Inside Book Insurance");
                    lobjModel.LogActivity(string.Format("Book Insurance Process"), ActivityType.Insurance);
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    
                    InsuranceServiceProvidersResponse lobjInsuranceServiceProviders = HttpContext.Current.Application["SearchInsuranceProducts"] as InsuranceServiceProvidersResponse;
                    string lstrProductName = string.Empty;
                    try
                    {
                        lstrProductName = lobjUserdetails.results.Key;
                    }
                    catch (Exception ex)
                    {
                        lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjUserdetails.results.Key);
                        LoggingAdapter.WriteLog("PaymentResponse BookInsurance Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
                    }

                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    int lintTotalPrice = lobjModel.ConvertToPoints(float.Parse(lobjUserdetails.results.Amount.ToString())
                             , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
                    try
                    {

                        string MembershipReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
                        switch (lobjUserdetails.results.Key)
                        {
                            case "Nepal Insurance":
                            case "Himalayan Insurance":
                            case "Surya Life Insurance":
                                lobjPaymentResponse = lobjModel.InsurancePaymentRequest(serviceCode, lobjUserdetails.results.Amount, lobjUserdetails.results.SessionId, HttpContext.Current.Session["InsurancePolicyNo"].ToString(), "", lobjUserdetails.results.Key, MembershipReference, lobjUserdetails.results.CustomerName);
                                break;
                            case "Reliance Insurance":
                                lobjPaymentResponse = lobjModel.InsurancePaymentRequest(serviceCode, lobjUserdetails.results.Amount, 0, HttpContext.Current.Session["InsurancePolicyNo"].ToString(), lobjUserdetails.results.TransactionId, lobjUserdetails.results.Key, MembershipReference, lobjUserdetails.results.CustomerName);
                                break;
                            case "NLG Insurance":
                                lobjPaymentResponse = lobjModel.InsurancePaymentRequest(serviceCode, lobjUserdetails.results.Amount, 0, HttpContext.Current.Session["InsurancePolicyNo"].ToString(), "", lobjUserdetails.results.Key, MembershipReference, lobjUserdetails.results.CustomerName);
                                break;
                            default:
                                lobjPaymentResponse = null;
                                break;
                        }

                        if (lobjPaymentResponse != null)
                        {
                            if (lobjPaymentResponse.results.Status)
                            {
                                if (lobjPaymentResponse.results.State.ToLower() == "success")
                                {
                                    HttpContext.Current.Session["InsuranceBookingId"] = lobjPaymentResponse.results.Id;
                                    lstrResponse = JsonConvert.SerializeObject(lobjPaymentResponse.results);
                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                    dynamicCls.event_name = "Insurance_Booked";
                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                    dynamicCls.serviceName = lobjUserdetails.results.Key;
                                    dynamicCls.CustomerName = lobjUserdetails.results.CustomerName;
                                    dynamicCls.PolicyNo = HttpContext.Current.Session["InsurancePolicyNo"].ToString();
                                    dynamicCls.ReferenceId = lobjPaymentResponse.results.ReferenceId;
                                    //dynamicCls.CreditsConsumed = lobjModel.FloatToThousandSeperated(float.Parse(lobjUserdetails.results.Amount.ToString())) + " NPR";
                                    dynamicCls.Points = lobjModel.FloatToThousandSeperated(lintTotalPrice) + " Points";
                                    dynamicCls.TransactionDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt");
                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                    foreach (var key in dict)
                                    {
                                        lobjDictionary.Add(key.Key, key.Value);
                                    }
                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                    Response.Redirect("/InsuranceProductStatus.aspx?Success=true", false);
                                }
                                else
                                {
                                    LoggingAdapter.WriteLog("InsurancePaymentRequest Fail");
                                    bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                    LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
                                    Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                                }
                            }
                            else
                            {
                                lstrResponse = lobjPaymentResponse.results.Message;
                                bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
                                Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                            }
                        }
                        else
                        {
                            bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                            lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                            LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
                            Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                        }

                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("InsurancePaymentRequest Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                        bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                        lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                        LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
                        Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                    }
                }
            }
            lobjModel.LogActivity(string.Format("Book Insurance Process") + "Status:" + lstrResponse, ActivityType.Insurance);
        }
        catch (Exception ex)
        {
            lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjUserdetails.results.Key);
            LoggingAdapter.WriteLog("PaymentResponse BookInsurance Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
        }
    }

    private void BookISP()
    {
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        PGRequest pgRequest = null;
        pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
        string lstrResponse = string.Empty;
        PGDetails lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
        bool lblRollback = false;
        ISPUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["ISPUserDetails"] as ISPUserDetailsResponse;
        try
        {
            if (lobjMemberDetails != null)
            {
                if (HttpContext.Current.Session["ISPUserDetails"] != null)
                {
                    LoggingAdapter.WriteLog("Inside Book ISP");
                    lobjModel.LogActivity(string.Format("Book ISP Process"), ActivityType.Insurance);
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    
                    InsuranceServiceProvidersResponse lobjInsuranceServiceProviders = HttpContext.Current.Application["SearchISPProducts"] as InsuranceServiceProvidersResponse;
                    string serviceCode = Convert.ToString(HttpContext.Current.Session["ISPServiceCode"]);
                    decimal FinalAmountPayable = Convert.ToDecimal(HttpContext.Current.Session["FinalAmountPayable"]);
                    Packages PackageData = HttpContext.Current.Session["ISPSelectedPackageData"] as Packages;
                    Details PackageDetailsData = HttpContext.Current.Session["ISPSelectedPackageDetailsData"] as Details;
                    string RequestId = HttpContext.Current.Session["ISPUserName"].ToString();
                    ISPPaymentResponse lobjPaymentResponse = new ISPPaymentResponse();
                    string lstrProductName = string.Empty;
                    try
                    {
                        lstrProductName = lobjUserdetails.results.Key;
                    }
                    catch (Exception ex)
                    {
                        lblRollback = true;
                        lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjUserdetails.results.Key);
                        LoggingAdapter.WriteLog("PaymentResponse BookISP Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
                    }
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    try
                    {
                        string MembershipReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
                        if (lobjUserdetails.results.Key == "BroadLink" && lobjUserdetails.results.Packages.Count > 0)
                        {
                            //call GetDiscount and then ISPPaymentRequest API
                            GetDiscountRequest lobjGetDiscountRequest = new GetDiscountRequest();
                            GetDiscountResponse lobjGetDiscountResponse = new GetDiscountResponse();
                            lobjGetDiscountRequest.ServiceCode = serviceCode;
                            lobjGetDiscountRequest.SessionId = lobjUserdetails.results.SessionId;
                            lobjGetDiscountRequest.Package = PackageData;
                            lobjGetDiscountResponse = lobjModel.GetDiscount(lobjGetDiscountRequest);
                            if (lobjGetDiscountResponse != null)
                            {
                                if (lobjGetDiscountResponse.results.Status)
                                {
                                    LoggingAdapter.WriteLog("Calling RedeemPoints");
                                    string lstrProgramName = ProgramHelper.ProgramName();

                                    int lintTotalPrice = lobjModel.ConvertToPoints(float.Parse(lobjGetDiscountResponse.results.Amount.ToString())
                                        , lstrCurrency, lobjProgramDefinition.ProgramId, "ISP");
                                    //string lstrRedeemResponse = lobjModel.RedeemPoints(float.Parse(lobjGetDiscountResponse.results.Amount.ToString()),
                                    //    lintTotalPrice,
                                    //    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                                    //    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
                                    //    lobjInsuranceServiceProviders.results.Find(x => x.ServiceCode == serviceCode.ToString()).ServiceName, (int)LoyaltyTxnType.BillPayment, lstrCurrency, "");
                                    //LoggingAdapter.WriteLog("RedeemPointsforISP success - '" + lstrRedeemResponse + "'");
                                    //if (!string.IsNullOrEmpty(lstrRedeemResponse))
                                    //{
                                    try
                                    {
                                        //call ISPPaymentRequest API
                                        ISPPaymentRequest lobjPaymentrequest = new ISPPaymentRequest();
                                        lobjPaymentrequest.ServiceCode = serviceCode;
                                        lobjPaymentrequest.Amount = lintTotalPrice;
                                        lobjPaymentrequest.SessionId = lobjGetDiscountResponse.results.SessionId;
                                        lobjPaymentrequest.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        lobjPaymentrequest.MembershipReference = MembershipReference;
                                        lobjPaymentResponse = lobjModel.ISPPaymentRequest(lobjPaymentrequest);
                                        if (lobjPaymentResponse != null)
                                        {
                                            if (lobjPaymentResponse.results.Status && lobjPaymentResponse.results.State == "Success")
                                            {
                                                HttpContext.Current.Session["ISPBookingId"] = lobjPaymentResponse.results.Id;
                                                lstrResponse = JsonConvert.SerializeObject(lobjPaymentResponse.results);
                                                dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                                dynamicCls.event_name = "ISP_Booked";
                                                dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                                dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                                dynamicCls.to_email = lobjMemberDetails.Email;
                                                dynamicCls.full_name = lobjMemberDetails.FullName;
                                                dynamicCls.serviceName = lobjUserdetails.results.Key;
                                                dynamicCls.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                                dynamicCls.ReferenceId = lobjPaymentResponse.results.ReferenceId;
                                                //dynamicCls.CreditsConsumed = lobjModel.FloatToThousandSeperated(float.Parse(lobjGetDiscountResponse.results.Amount.ToString())) + " NPR";
                                                dynamicCls.Points = lobjModel.FloatToThousandSeperated(lintTotalPrice) + " Points";
                                                dynamicCls.TransactionDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt");
                                                dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                                Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                                IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                                foreach (var key in dict)
                                                {
                                                    lobjDictionary.Add(key.Key, key.Value);
                                                }
                                                string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                                lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                            }
                                            else
                                            {
                                                lblRollback = true;
                                                LoggingAdapter.WriteLog("ISPPaymentRequest Failed; ErrorCode:-{0}", lobjPaymentResponse.results.ErrorCode);
                                                lstrResponse = lobjPaymentResponse.results.Message;
                                                bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                                lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                                LoggingAdapter.WriteLog("ISPPaymentRequest Ex Rollback Success");
                                            }
                                        }
                                        else
                                        {
                                            lblRollback = true;
                                            bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                            lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                            LoggingAdapter.WriteLog("ISPPaymentRequest Ex Rollback Success");
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        lblRollback = true;
                                        LoggingAdapter.WriteLog("ISPPaymentRequest Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                                        bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                        lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                        LoggingAdapter.WriteLog("ISPPaymentRequest Ex Rollback Success");
                                    }
                                    //}

                                }
                                else
                                {
                                    lblRollback = true;
                                    bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                    LoggingAdapter.WriteLog("GetDiscountforISPBroadLink_Packages Failed");
                                }
                            }
                            else
                            {
                                lblRollback = true;
                                bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                LoggingAdapter.WriteLog("GetDiscountforISPBroadLink_Packages Failed");
                            }
                        }
                        else
                        {
                            LoggingAdapter.WriteLog("Calling RedeemPoints");
                            string lstrProgramName = ProgramHelper.ProgramName();
                            //string lstrRedeemResponse = lobjModel.RedeemPoints(float.Parse(FinalAmountPayable.ToString()),
                            //     Convert.ToInt32(FinalAmountPayable),
                            //    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                            //    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
                            //   lobjInsuranceServiceProviders.results.Find(x => x.ServiceCode == serviceCode.ToString()).ServiceName, (int)LoyaltyTxnType.BillPayment, lstrCurrency, "");
                            //LoggingAdapter.WriteLog("RedeemPointsforISP success - '" + lstrRedeemResponse + "'");
                            //if (!string.IsNullOrEmpty(lstrRedeemResponse))
                            //{
                            try
                            {
                                //call ISPPaymentRequest API
                                ISPPaymentRequest lobjPaymentrequest = new ISPPaymentRequest();
                                switch (lobjUserdetails.results.Key)
                                {

                                    case "BroadLink":
                                        lobjPaymentrequest = new ISPPaymentRequest();
                                        lobjPaymentrequest.ServiceCode = serviceCode;
                                        lobjPaymentrequest.Amount = FinalAmountPayable;
                                        lobjPaymentrequest.SessionId = lobjUserdetails.results.SessionId;
                                        lobjPaymentrequest.MembershipReference = MembershipReference;
                                        lobjPaymentrequest.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        break;
                                    case "Techminds":
                                        lobjPaymentrequest = new ISPPaymentRequest();
                                        lobjPaymentrequest.ServiceCode = serviceCode;
                                        lobjPaymentrequest.Amount = FinalAmountPayable;
                                        lobjPaymentrequest.SessionId = lobjUserdetails.results.SessionId;
                                        lobjPaymentrequest.RequestId = RequestId;
                                        lobjPaymentrequest.MembershipReference = MembershipReference;
                                        lobjPaymentrequest.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        break;
                                    case "WorldLink":
                                        lobjPaymentrequest = new ISPPaymentRequest();
                                        lobjPaymentrequest.ServiceCode = serviceCode;
                                        lobjPaymentrequest.Amount = FinalAmountPayable;
                                        lobjPaymentrequest.PackageId = PackageData.PackageId;
                                        lobjPaymentrequest.SessionId = lobjUserdetails.results.SessionId;
                                        lobjPaymentrequest.MembershipReference = MembershipReference;
                                        lobjPaymentrequest.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        break;
                                    case "Broadband Solutions":
                                        lobjPaymentrequest = new ISPPaymentRequest();
                                        lobjPaymentrequest.ServiceCode = serviceCode;
                                        lobjPaymentrequest.Amount = FinalAmountPayable;
                                        lobjPaymentrequest.PackageId = PackageData.PackageId;
                                        lobjPaymentrequest.DurationCode = PackageDetailsData.DurationCode;
                                        lobjPaymentrequest.SessionId = lobjUserdetails.results.SessionId;
                                        lobjPaymentrequest.MembershipReference = MembershipReference;
                                        lobjPaymentrequest.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        break;
                                    case "Chitrawan Unique Net":
                                        lobjPaymentrequest = new ISPPaymentRequest();
                                        lobjPaymentrequest.ServiceCode = serviceCode;
                                        lobjPaymentrequest.Amount = FinalAmountPayable;
                                        lobjPaymentrequest.PackageId = PackageData.PackageId;
                                        lobjPaymentrequest.SessionId = lobjUserdetails.results.SessionId;
                                        lobjPaymentrequest.MembershipReference = MembershipReference;
                                        lobjPaymentrequest.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        break;
                                }
                                lobjPaymentResponse = new ISPPaymentResponse();
                                lobjPaymentResponse = lobjModel.ISPPaymentRequest(lobjPaymentrequest);
                                if (lobjPaymentResponse != null)
                                {
                                    if (lobjPaymentResponse.results.Status && lobjPaymentResponse.results.State == "Success")
                                    {
                                        HttpContext.Current.Session["ISPBookingId"] = lobjPaymentResponse.results.Id;
                                        lstrResponse = JsonConvert.SerializeObject(lobjPaymentResponse.results);
                                        dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                        dynamicCls.event_name = "ISP_Booked";
                                        dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                        dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                        dynamicCls.to_email = lobjMemberDetails.Email;
                                        dynamicCls.full_name = lobjMemberDetails.FullName;
                                        dynamicCls.serviceName = lobjUserdetails.results.Key;
                                        dynamicCls.CustomerName = lobjUserdetails.results.CustomerDetails.CustomerName;
                                        dynamicCls.ReferenceId = lobjPaymentResponse.results.ReferenceId;
                                        //dynamicCls.CreditsConsumed = lobjModel.FloatToThousandSeperated(lobjModel.CalculatePointstoAmount(decimal.Parse(FinalAmountPayable.ToString()), Pointrate)) + " NPR";
                                        dynamicCls.Points = lobjModel.FloatToThousandSeperated(float.Parse(FinalAmountPayable.ToString())) + " Points";
                                        dynamicCls.TransactionDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt");
                                        dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                        Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                        IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                        foreach (var key in dict)
                                        {
                                            lobjDictionary.Add(key.Key, key.Value);
                                        }
                                        string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                        lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                    }
                                    else
                                    {
                                        lblRollback = true;
                                        LoggingAdapter.WriteLog("ISPPaymentRequest Failed; ErrorCode:-{0}", lobjPaymentResponse.results.ErrorCode);
                                        lstrResponse = lobjPaymentResponse.results.Message;
                                        LoggingAdapter.WriteLog("ISPPaymentRequest Rollback " + pgRequest.orderId);
                                        bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                        lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                        LoggingAdapter.WriteLog("ISPPaymentRequest Rollback Success");
                                    }
                                }
                                else
                                {
                                    lblRollback = true;
                                    LoggingAdapter.WriteLog("ISPPaymentRequest Rollback " + pgRequest.orderId);
                                    bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                    LoggingAdapter.WriteLog("ISPPaymentRequest Ex Rollback Success");
                                }
                            }
                            catch (Exception ex)
                            {
                                lblRollback = true;
                                LoggingAdapter.WriteLog("ISPPaymentRequest Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                                bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
                                lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                LoggingAdapter.WriteLog("ISPPaymentRequest Ex Rollback Success");
                            }
                            // }
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggingAdapter.WriteLog("ISP Booking Failed Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                    }

                    if (lblRollback == true)
                    {
                        Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
                    }
                    else
                    {
                        Response.Redirect("/InsuranceProductStatus.aspx?Success=true", false);
                    }
                }
            }
            lobjModel.LogActivity(string.Format("Book ISP Process") + "Status:" + lstrResponse, ActivityType.InternetServiceProvider);
        }
        catch (Exception ex)
        {
            lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjUserdetails.results.Key);
            LoggingAdapter.WriteLog("PaymentResponse BookISP Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
            Response.Redirect("/InsuranceProductStatus.aspx?Success=false", false);
        }
    }

    private void BookDomesticFlight()
    {
        ABCModel lobjModel = new ABCModel();
        bool Result = false;
        try
        {
            LoggingAdapter.WriteLog("Booking Flight for Khalti");
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null)
            {
                if (lobjProgramDefinition != null)
                {
                    if (HttpContext.Current.Session["DomesticFlightBookingRequest"] != null && HttpContext.Current.Session["MemberDetails"] != null && HttpContext.Current.Session["DomesticFlightBookingResponse"] != null)
                    {
                        lobjModel.LogActivity(string.Format("Book Flight Process"), ActivityType.FlightBookingForDomestic);
                        BookingStatusRequestForDomestic lobjBookingRequest = new BookingStatusRequestForDomestic();
                        lobjBookingRequest.Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                        lobjBookingRequest.Reference = HttpContext.Current.Session["KhaltiFlightBookingReferenceId"].ToString();
                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        lobjBookingRequest.PointRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);

                        CreateDomesticBookingResponse lobjBookingDetailsResponse = new CreateDomesticBookingResponse();
                        lobjBookingDetailsResponse = HttpContext.Current.Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;

                        PGRequest pgRequest = null;
                        pgRequest = HttpContext.Current.Session["PGPaymentRequest"] as PGRequest;
                        PGDetails lobjPGDetails = null;
                        lobjPGDetails = lobjModel.GetPaymentStatusByOrderId(pgRequest.orderId);
                        if (lobjPGDetails != null)
                        {
                            try
                            {
                                BookingStatusResponseForDomestic lobjBookingResponse = lobjModel.BookForKhaltiFlight(lobjBookingRequest);
                                HttpContext.Current.Session["BookingStatusResponseForDomestic"] = lobjBookingResponse;
                                if (lobjBookingResponse.Status && !string.IsNullOrEmpty(lobjBookingResponse.Detail.Outbound.Pnrno))
                                {
                                    HttpContext.Current.Session["KhaltiFlightBookingReferenceId"] = null;
                                    HttpContext.Current.Session["FlightSearchDetailsForDomestic"] = null;
                                    HttpContext.Current.Session["FlightsForDomestic"] = null;
                                    HttpContext.Current.Session["DomesticFlightBookingId"] = null;
                                    HttpContext.Current.Session["DomesticInboundFlights"] = null;
                                    HttpContext.Current.Session["DomesticOutboundFlights"] = null;
                                    Result = true;

                                    //communication Engine call for Email send
                                    string strPaxInfo = "";
                                    strPaxInfo += "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr>";
                                    strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='15%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'> Title </td>";
                                    strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='40%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Passenger Name</td>";
                                    strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='22%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Ticket No.</td>";
                                    strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='12%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Gender</td>";
                                    strPaxInfo += "</tr>";
                                    List<CB.IBE.DomesticFlight.Entities.Passengers> lobjListOfPassengerDetails = new List<CB.IBE.DomesticFlight.Entities.Passengers>();
                                    if (lobjBookingDetailsResponse != null)
                                    {
                                        string lstrPaxtype = "";
                                        lobjListOfPassengerDetails = lobjBookingDetailsResponse.Passengers;
                                        for (int k = 0; k < lobjListOfPassengerDetails.Count; k++)
                                        {
                                            strPaxInfo += "<tr>";
                                            strPaxInfo += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'> " + lobjListOfPassengerDetails[k].Title + " </td>";
                                            strPaxInfo += "<td align='left' valign='top' width='40%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + UppercaseFirst(lobjListOfPassengerDetails[k].Lastname) + " " + UppercaseFirst(lobjListOfPassengerDetails[k].Firstname) + "</td>";
                                            strPaxInfo += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjListOfPassengerDetails[k].TicketNo + " </td>";
                                            strPaxInfo += "<td align='left' valign='top' width='12%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjListOfPassengerDetails[k].Gender + " </td>";
                                            strPaxInfo += "</tr>";
                                        }

                                        strPaxInfo += "</table>";
                                    }

                                    // Code for Departure table
                                    BookedFlightDetails lobjFlightSegmentlst = new BookedFlightDetails();
                                    lobjFlightSegmentlst = lobjBookingDetailsResponse.Outbound;
                                    string strDepartute = "";
                                    strDepartute += "<tr>";
                                    strDepartute += "<td align='left' bgcolor='#dd2625' width='10%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Flight</td>";
                                    strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Departure</td>";
                                    strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrival</td>";
                                    strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Depart Time</td>";
                                    strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrial Time</td>";
                                    strDepartute += "</tr>";

                                    strDepartute += "<tr>";
                                    strDepartute += "<td align='center' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst.Flightno + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorFrom + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorTo + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjBookingDetailsResponse.FlightDate).ToString("dd/MM/yyyy") + "<br/>" + Convert.ToDateTime(lobjFlightSegmentlst.DepartureTime).ToString("HH:mm") + "</td>";
                                    strDepartute += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjFlightSegmentlst.ArrivalTime).ToString("HH:mm") + "</td>";
                                    strDepartute += "</tr>";
                                    //For AirLine PNR
                                    strDepartute += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
                                    strDepartute += "<td width='50%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px;'> PNR:&nbsp;" + lobjFlightSegmentlst.Pnrno + "</td>";
                                    strDepartute += "<tr></table></td></tr>";

                                    lobjFlightSegmentlst = lobjBookingDetailsResponse.Inbound;
                                    string strReturn = "";
                                    string strArrival = "";
                                    string InboundFlightId = lobjBookingResponse.Detail.InboundFlightId;
                                    if (!string.IsNullOrEmpty(InboundFlightId))
                                    {
                                        // Code for Arrival Table
                                        strReturn += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
                                        strReturn += "<td width='50%' height='25' valign='top' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-align: left; color: #dd2625; padding: 0px;'>Itinerary Details <span style='color: #231f20;'>(Return)</span></td>";
                                        strReturn += "<tr></table></td></tr>";
                                        // Arrival Header Row
                                        strReturn += "<tr>";
                                        strReturn += "<td align='left' bgcolor='#dd2625' width='10%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Flight</td>";
                                        strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Departure</td>";
                                        strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrival</td>";
                                        strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Depart Time</td>";
                                        strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrial Time</td>";
                                        strReturn += "</tr>";

                                        strArrival += "<tr>";
                                        strArrival += "<td width='15%' align='center' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst.Flightno + "</td>";
                                        strArrival += "<td align='left' valin='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorTo + "</td>";
                                        strArrival += "<td align='left' valin='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorFrom + "</td>";
                                        strArrival += "<td align='left' valin='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjFlightSegmentlst.DepartureTime).ToString("HH:mm") + "</td>";
                                        strArrival += "<td align='left' valin='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjFlightSegmentlst.ArrivalTime).ToString("HH:mm") + "</td>";
                                        strArrival += "</tr>";

                                        //For Arrival AirLine PNR
                                        strArrival += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
                                        strArrival += "<td width='50%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px;'> PNR:&nbsp;" + lobjFlightSegmentlst.Pnrno + "</td>";
                                        strArrival += "<tr></table></td></tr>";
                                    }
                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                    dynamicCls.event_name = "Domestic_Flight_Booked";
                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                    dynamicCls.TransactionReferenceCode = lobjBookingResponse.Detail.Reference;
                                    dynamicCls.PaymentDetails = lobjModel.FloatToThousandSeperated(Convert.ToSingle(lobjBookingDetailsResponse.CreditsConsumed)) + " Points";
                                    dynamicCls.TblPassengerInfo = strPaxInfo;
                                    dynamicCls.TblDeparture = strDepartute;
                                    dynamicCls.ReturnFlight = strReturn;
                                    dynamicCls.TblArrival = strArrival;
                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                    dynamicCls.CreditsConsumed = lobjModel.FloatToThousandSeperated(Convert.ToSingle(lobjBookingDetailsResponse.CreditsConsumed));
                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                    foreach (var key in dict)
                                    {
                                        lobjDictionary.Add(key.Key, key.Value);
                                    }
                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
                                    Response.Redirect("/AirReceipt_Domestic.aspx", false);
                                }
                                else
                                {
                                    lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                    bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo.ToString());
                                    LoggingAdapter.WriteLog("BookForKhaltiFlight Ex Rollback Success");
                                    Result = false;
                                    Response.Redirect("/BookingFailure.aspx", false);
                                }
                            }
                            catch (Exception ex)
                            {
                                LoggingAdapter.WriteLog("BookForKhaltiFlight Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                                lobjModel.InitiatePaymentRefund(pgRequest.orderId, Convert.ToDecimal(lobjPGDetails.data[0].orderAmount), "refund it please");
                                bool lboolRollBackResponse = lobjModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo.ToString());
                                LoggingAdapter.WriteLog("BookForKhaltiFlight Ex Rollback Success");
                                Result = false;
                                Response.Redirect("/BookingFailure.aspx", false);
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx BookForKhaltiFlight Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        lobjModel.LogActivity(string.Format("Book Flight Process") + "Status:" + Result, ActivityType.FlightBookingForDomestic);
    }
    public class PersonListNode
    {
        public string pricingCategoryLabel { get; set; }
        public int count { get; set; }
        public string labelName { get; set; }
    }

    //public static string GetOrderStatus(string pstrBookId)
    //{
    //    string lstrResponse = string.Empty;
    //    StringBuilder lsbLogRequestResponse = new StringBuilder();
    //    ABCModel lobjModel = new ABCModel();
    //    try
    //    {
    //        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
    //        if (lobjMemberDetails != null && !string.IsNullOrEmpty(lobjMemberDetails.LastName))
    //        {
    //            lsbLogRequestResponse.Append(string.Format("GetOrderStatus Request: pstrBookId - {0}", pstrBookId));
    //            OrderStatusResponse lobjOrderStatusResponse = null; //lobjModel.GetOrderStatusByBookingId(pstrBookId);
    //            lsbLogRequestResponse.Append(string.Format(" GetOrderStatus Response: {0}", JsonConvert.SerializeObject(lobjOrderStatusResponse)));
    //            if (lobjOrderStatusResponse != null
    //                && lobjOrderStatusResponse.data != null
    //                && !string.IsNullOrEmpty(lobjOrderStatusResponse.data.getOrderStatus))
    //            {
    //                HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lobjOrderStatusResponse.data.getOrderStatus);
    //                if (lobjOrderStatus != null && lobjOrderStatus.data != null && lobjOrderStatus.status.ToLower() == "success")
    //                {
    //                    lobjModel.LogActivity(string.Format("GetOrderStatus; ActivityType-: {0}; Response-: {1}", ActivityConstants.BookPackage, lobjOrderStatus.status), ActivityType.PackageBooking);

    //                    string lstrCurrency = lobjModel.GetDefaultCurrency();
    //                    string lstrProgramName = ProgramHelper.ProgramName();
    //                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
    //                    if (lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes != null)
    //                    {
    //                        foreach (var lobjAvailabilityListNode in lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes)
    //                        {
    //                            string pstrGrossFormattedText = string.Empty;
    //                            lobjAvailabilityListNode.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(lobjAvailabilityListNode.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
    //                            lobjAvailabilityListNode.totalPrice.grossFormattedText = pstrGrossFormattedText;
    //                            foreach (var lobjPersonListNode in lobjAvailabilityListNode.personList.nodes)
    //                            {
    //                                string pstrFormattedText = string.Empty;
    //                                lobjPersonListNode.totalPrice = lobjModel.ConvertToPointsOrCurrency(lobjPersonListNode.totalPrice, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrFormattedText);
    //                            }
    //                        }
    //                        lobjOrderStatusResponse.data.getOrderStatus = JsonConvert.SerializeObject(lobjOrderStatus);
    //                    }
    //                    lstrResponse = JsonConvert.SerializeObject(lobjOrderStatus);
    //                }
    //                else
    //                {
    //                    lobjModel.LogActivity(string.Format("GetOrderStatus; ActivityType-: {0}; Response-: {1}", ActivityConstants.BookPackage, "Failed"), ActivityType.PackageBooking);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lobjModel.LogActivity(string.Format("GetOrderStatus; ActivityType-: {0}; Response-: {1}", ActivityConstants.BookPackage, "Failed"), ActivityType.PackageBooking);
    //        LoggingAdapter.WriteLog("ExperienceProductBooking GetOrderStatus Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
    //    }
    //    finally
    //    {
    //    }
    //    return lstrResponse;
    //}

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    private static string Encrypt(string clearText)
    {
        try
        {
            string EncryptionKey = "MAKV2SPNIC99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ProductDetails.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return clearText;
    }
    public string FormatCurrency(decimal decValue, string currencyCode, bool requiredDecimal = false)
    {
        NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
        if (requiredDecimal)
        {
            nfo.NumberDecimalDigits = 2;
            if (!string.IsNullOrEmpty(currencyCode))
            {
                return string.Format("{0} {1}", currencyCode, decValue.ToString("N", nfo));
            }
            else
            {
                return string.Format("{0}", decValue.ToString("N", nfo));
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(currencyCode))
            {
                return string.Format("{0} {1}", currencyCode, decValue.ToString("N", nfo).Split('.')[0]);
            }
            else
            {
                return string.Format("{0}", decValue.ToString("N", nfo).Split('.')[0]);
            }
        }
    }

}
