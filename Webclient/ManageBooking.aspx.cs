using System;
using System.Collections.Generic;
using System.Web;
using ABC.Model;
using Core.Platform.Member.Entites;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using CB.IBE.Platform.ClientEntities;
using Core.Platform.Transactions.Entites;
using Framework.EnterpriseLibrary.Adapters;
using System.Text;
using System.Web.UI;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.InfiVoucher.Entities;
using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.AirClientModel;
using System.Configuration;
using System.Web.Services;
using System.IO;
using System.Linq;
using Giift.ShopGateway.Client.Entities;
using Core.Platform.ProgramMaster.Entities;
using System.Security.Cryptography;
using IBEAPI.ClientEntities;
using IBEAPIGateway.Model;
using BeMyGuest.Entities;

public partial class ManageBooking : Page
{
    ABCModel lobjModel = new ABCModel();
    IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["MemberDetails"] != null)
            {
                MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                CB.IBE.Platform.Masters.Entities.RefererDetails lobjRefererData = Application["RefererData"] as CB.IBE.Platform.Masters.Entities.RefererDetails;
                BindFlightBooking(lobjMemberDetails);
                BindHotelBooking(lobjMemberDetails);
                BindExperienceBookingDetails(lobjMemberDetails);
                BindCarBooking(lobjMemberDetails);
                lobjModel.LogActivity(string.Format("Visited ManageBooking.aspx; MemberId-:{0}", lobjMemberDetails.MemberRelationsList[0].RelationReference), ActivityType.PageLoad);
            }
            else
            {
                string CallbackUrl = HttpUtility.UrlEncode(Encrypt("ManageBooking.aspx"));
                HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                Response.Redirect("Index.aspx?CallbackUrl=" + CallbackUrl, false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- Page_Load Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void BindFlightBooking(MemberDetails pobjMemberDetails)
    {
        try
        {
            if (Session["MemberDetails"] != null)
            {
                List<ItineraryDetails> lobjListItineraryDetails = lobjIBEAPIModel.GetFlightBookingListForMember(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                if (lobjListItineraryDetails != null && lobjListItineraryDetails.Count > 0)
                {
                    rptBookingDetails.DataSource = lobjListItineraryDetails;
                    //divrptflight.Attributes.Add("style", "Display:block");
                }
                else
                {
                    rptBookingDetails.DataSource = null;
                    lblFlightrecord.Visible = true;
                    lblFlightrecord.Text = "<span class=\"heading-regular\">No Records Found.</span>";
                    // divFlight.Visible = false;
                    divFlightrecord.Visible = true;
                    //divrptflight.Attributes.Add("style", "Display:none");
                }
                rptBookingDetails.DataBind();
            }
            else
            {
                Response.Redirect("Index.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindFlightBooking Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void BindHotelBooking(MemberDetails pobjMemberDetails)
    {
        try
        {
            GetHotelInfoDetails lobjGetHotelInfoDetails = new GetHotelInfoDetails();

            lobjGetHotelInfoDetails = lobjIBEAPIModel.GetMemberBookedHotelInfoList(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);

            if (lobjGetHotelInfoDetails.HotelDetails != null && lobjGetHotelInfoDetails.HotelDetails.Count > 0)
            {
                rptHotelCancelBookingDetails.DataSource = lobjGetHotelInfoDetails.HotelDetails;
                //divrpthotel.Attributes.Add("style", "Display:block");
            }
            else
            {
                rptHotelCancelBookingDetails.DataSource = null;
                lblHotelrecord.Visible = true;
                lblHotelrecord.Text = "<span class=\"heading-regular\">No Records Found.</span>";
                divHotelrecord.Visible = true;
                //divrpthotel.Visible = false;
                //divrpthotel.Attributes.Add("style", "Display:none");
            }
            rptHotelCancelBookingDetails.DataBind();
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindHotelBooking Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public void BindExperienceBookingDetails(MemberDetails pobjMemberDetails)
    {
        StringBuilder lsbTrExperienceBookingDetailsHtml = new StringBuilder();
        try
        {
            if (pobjMemberDetails != null)
            {
                ABCModel lobjModel = new ABCModel();
                BookingByUserResponse lobjlstBookingDetails = lobjModel.GetAllExperiences(pobjMemberDetails.MemberRelationsList[0].RelationReference);
                if (lobjlstBookingDetails != null && lobjlstBookingDetails.data.Count > 0)
                {
                    rptExperienceBookingDetails.DataSource = lobjlstBookingDetails.data;
                }
                else
                {
                    rptExperienceBookingDetails.DataSource = null;
                    lblExperiencerecord.Visible = true;
                    lblExperiencerecord.Text = "<span class=\"heading-regular\">No Records Found.</span>";
                    divExperiencerecord.Visible = true;
                }
                rptExperienceBookingDetails.DataBind();
            }
            else
            {
                Response.Redirect("Index.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("BindExperienceBookingDetails Exception :" + ex.Message + Environment.NewLine + "StackTrace :" + ex.StackTrace);
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool ShowAirReceipt(string TripId)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();

            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            HttpContext.Current.Session["RetriveBookingInfo"] = null;
            CB.IBE.Platform.Masters.Entities.RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as CB.IBE.Platform.Masters.Entities.RefererDetails;
            RetriveItineraryDetails lobjRetriveItineraryDetails = lobjIBEAPIModel.GetBookedFlightItinerary(TripId);
            if (lobjRetriveItineraryDetails != null)
            {
                HttpContext.Current.Session["RetriveBookingInfo"] = lobjRetriveItineraryDetails.ItineraryDetails;
                HttpContext.Current.Session["FlightBooked"] = lobjRetriveItineraryDetails.ItineraryDetails;
            }
            lobjModel.LogActivity(string.Format("ShowAirReceipt; MemberId-:{0} TripId-:{1}", lobjMemberDetails.MemberRelationsList[0].RelationReference, TripId), ActivityType.FlightSearch);
            return true;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- ShowAirReceipt Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool ShowHotelVoucher(string TransactionReferenceCode)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();

            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            HttpContext.Current.Session["HotelBooked"] = null;
            HttpContext.Current.Session["CustomerDetails"] = null;
            HttpContext.Current.Session["BookingResponse"] = null;

            HotelItineraryResponse lobjHotelItineraryResponse = lobjIBEAPIModel.GetMemberBookedHotelInfo(TransactionReferenceCode);
            if (lobjHotelItineraryResponse != null)
            {
                HttpContext.Current.Session["HotelBooked"] = lobjHotelItineraryResponse.HotelSearchResponse;
                HttpContext.Current.Session["CustomerDetails"] = lobjHotelItineraryResponse.Customer;
                HttpContext.Current.Session["BookingResponse"] = lobjHotelItineraryResponse.HotelBookingResponse;
                HttpContext.Current.Session["ItenaryDetailsResponse"] = lobjHotelItineraryResponse;

            }
            lobjModel.LogActivity(string.Format("ShowHotelVoucher; MemberId-:{0} TransactionReferenceCode-:{1}", lobjMemberDetails.MemberRelationsList[0].RelationReference, TransactionReferenceCode), ActivityType.HotelSearch);
            return true;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- ShowHotelVoucher Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
    }
    protected string GetFormattedPoints(string pobjpoints)
    {
        if (string.IsNullOrEmpty(pobjpoints))
        {
            return "0";
        }
        else
        {
            float flpoint = float.Parse(pobjpoints);
            ABCModel lobjModel = new ABCModel();
            string res = lobjModel.FloatToThousandSeperated(flpoint);
            return res;
        }
    }

    public void BindDomesticBookingDetails(MemberDetails pobjMemberDetails)
    {
        StringBuilder lsbTrDomesticBookingDetailsHtml = new StringBuilder();
        try
        {
            if (Session["MemberDetails"] != null)
            {
                List<DomesticItineraryDetails> lobjListItineraryDetails = lobjModel.GetDomesticFlightBookingDetails(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                lobjListItineraryDetails = lobjListItineraryDetails.OrderByDescending(x => x.LogIds[0]).ToList();
                if (lobjListItineraryDetails != null && lobjListItineraryDetails.Count > 0)
                {
                    int i = 1;
                    foreach (DomesticItineraryDetails item in lobjListItineraryDetails)
                    {

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"row mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-12\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"bg-colour6 p-3\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");



                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Flight Date</span> <span class=\"h6 d-block\">" + Convert.ToDateTime(item.FlightDate).ToString("dd/MM/yyyy") + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Return Date</span> <span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.ReturnDate) ? "NA" : Convert.ToDateTime(item.ReturnDate).ToString("dd/MM/yyyy")) + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">From</span> <span class=\"h6 d-block\">" + item.SectorFrom.ToString() + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">To</span> <span class=\"h6 d-block\">" + item.SectorTo.ToString() + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Outbound Flight PNR</span> <span class=\"h6 d-block\">" + item.OutboundPNR.ToString() + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Inbound Flight PNR</span> <span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.InboundPNR.ToString()) ? "NA" : item.InboundPNR.ToString()) + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Points</span> <span class=\"h6 d-block\">" + lobjModel.StringToThousandSeperated(item.CreditsConsumed.ToString()) + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-12 col-md-3 col-lg-3 col-xl-3 mt-2 mt-md-0\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div onclick=\\\"ShowDomesticFlightDetails(\" + item.LogIds[0] + \")" + item.LogIds[0] + "\" class=\"btn btn-one w-100\">View Details</div>");
                        lsbTrDomesticBookingDetailsHtml.Append("<a target=\"_blank\" href=\"/DomesticAirFlightbookingDetails.ashx?LogId=" + item.LogIds[0] + "\" class=\"btn btn-one w-100\">View Details</a>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("</div>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        i++;
                    }
                    //lsbTrDomesticBookingDetailsHtml.Append("</table>");
                }
                else
                {
                    lsbTrDomesticBookingDetailsHtml.Append("No Records Found.");
                }
                divDomesticFlightBookingDetails.InnerHtml = lsbTrDomesticBookingDetailsHtml.ToString();
            }
            else
            {
                Response.Redirect("Index.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindDomesticBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public void BindCarBooking(MemberDetails pobjMemberDetails)
    {
        IBEAPIModel lobjApimodel = new IBEAPIModel();
        try
        {
            if (Session["MemberDetails"] != null)
            {
                UserBookingRequest lobjUserBookingRequest = new UserBookingRequest();
                lobjUserBookingRequest.member_id = pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                List<ProgramCurrencyDefinition> lobjProgramCurrencyDefinition = lobjModel.GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
                var PointRate = lobjProgramCurrencyDefinition[0].RedemptionRate;
                UserBookingResponse lobjUserBookingResponse = lobjApimodel.GetUserBookings(lobjUserBookingRequest);

                if (lobjUserBookingResponse != null && lobjUserBookingResponse.data.Count > 0)
                {
                    rptCarBookingDetails.DataSource = lobjUserBookingResponse.data;

                }
                else
                {
                    rptCarBookingDetails.DataSource = null;
                    lblCarrecord.Visible = true;
                    lblCarrecord.Text = "<span>No Records Found.</span>";
                    divCarrecord.Visible = true;

                }
                rptCarBookingDetails.DataBind();
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindFlightBooking Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool ShowCarVoucher(string BookingReferenceId, string accessToken, string reservationNumber)
    {
        IBEAPIModel lobjModel = new IBEAPIModel();
        string EnjoyTraveldisplayCurrency = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTraveldisplayCurrency"]);

        CarBookingRoot lobjCarBookingResponse = lobjModel.GetCarBookingDetailsbyRefId(BookingReferenceId, accessToken, reservationNumber, EnjoyTraveldisplayCurrency);
        if (lobjCarBookingResponse.success == 1)
        {
            HttpContext.Current.Session["CarSearchPrint"] = lobjCarBookingResponse;
            return true;
        }
        else
        {
            return false;
        }
    }

    //public void BindInsuranceBookingDetails(MemberDetails pobjMemberDetails)
    //{
    //    StringBuilder lsbTrInsuranceBookingDetailsHtml = new StringBuilder();
    //    try
    //    {
    //        if (Session["MemberDetails"] != null)
    //        {
    //            List<InsuranceBookingResponse> lobjListInsuranceDetails = lobjModel.GetBookedInsuranceListForMember(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
    //            ABCModel model = new ABCModel();
    //            string lstrCurrency = model.GetDefaultCurrency();
    //            ProgramDefinition lobjProgramDefinition = model.GetProgramMaster();
    //            Session["InsuranceBookingDetailsofMember"] = lobjListInsuranceDetails;
    //            lobjListInsuranceDetails = lobjListInsuranceDetails.OrderByDescending(x => x.PaymentDate).ToList();
    //            if (lobjListInsuranceDetails != null && lobjListInsuranceDetails.Count > 0)
    //            {

    //                int i = 1;
    //                foreach (InsuranceBookingResponse item in lobjListInsuranceDetails)
    //                {
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"row mb-1\"><div class=\"col-12\"><div class=\"bg-colour6 p-3\">");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");
    //                    //lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-12 col-xl-3 col-lg-4 col-md-6 mb-3\"><p>");
    //                    //lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">No.</span>");
    //                    //lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + i + "</span></p>");
    //                    //lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Service Name</span>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ServiceName.ToString() + "</span></p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-2 col-xl-2 mb-1\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Payment Date</span>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + Convert.ToDateTime(item.PaymentDate).ToString("dd/MM/yyyy") + "</span></p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-2 col-xl-2 mb-1\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Next Due Date</span>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.NextDueDate) ? "NA" :item.NextDueDate) + "</span></p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-5 col-xl-5 mb-1\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Reference Id</span>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ReferenceId.ToString() + "</span></p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Payment Id</span>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.PaymentId.ToString() + "</span></p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    int lintTotalPrice = model.ConvertToPoints(float.Parse(item.Amount.ToString())
    //                            , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Points</span>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + lobjModel.FloatToThousandSeperated(lintTotalPrice) + "</span></p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-12 offset-md-3 col-md-3 col-lg-3 col-xl-3 mt-2 mt-md-0\"><p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("<a id=\"ViewDetails\" class=\"btn btn-one w-100\" onclick=\"return ViewDetails('" + item.ReferenceId.ToString()+"');\" data-toggle=\"modal\">View Details</a>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</p>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div>");
    //                    lsbTrInsuranceBookingDetailsHtml.Append("</div></div></div></div>");
    //                    i++;
    //                }
    //            }
    //            else
    //            {
    //                lsbTrInsuranceBookingDetailsHtml.Append("No Records Found.");
    //            }
    //            divInsuranceFlightBookingDetails.InnerHtml = lsbTrInsuranceBookingDetailsHtml.ToString();
    //        }
    //        else
    //        {
    //            Response.Redirect("Index.aspx", false);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LoggingAdapter.WriteLog("ManageBooking.aspx- BindInsuranceBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
    //    }
    //}

    //public void BindISPBookingDetails(MemberDetails pobjMemberDetails)
    //{
    //    StringBuilder lsbTrISPBookingDetailsHtml = new StringBuilder();
    //    try
    //    {
    //        if (Session["MemberDetails"] != null)
    //        {
    //            List<ISPBookingResponse> lobjListISPDetails = lobjModel.GetBookedISPListForMember(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
    //            lobjListISPDetails = lobjListISPDetails.OrderByDescending(x => x.PaymentDate).ToList();
    //            if (lobjListISPDetails != null && lobjListISPDetails.Count > 0)
    //            {

    //                int i = 1;
    //                foreach (ISPBookingResponse item in lobjListISPDetails)
    //                {
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"row mb-1\"><div class=\"col-12\"><div class=\"bg-colour6 p-3\">");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");
    //                    //lsbTrISPBookingDetailsHtml.Append("<div class=\"col-12 col-xl-3 col-lg-4 col-md-6 mb-3\"><p>");
    //                    //lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">No.</span>");
    //                    //lsbTrISPBookingDetailsHtml.Append("<span class=\"d-inline-block heading-regular\">" + i + "</span></p>");
    //                    //lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Customer Name</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.CustomerName.ToString() + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Service Name</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ServiceName.ToString() + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Payment Date</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + Convert.ToDateTime(item.PaymentDate).ToString("dd/MM/yyyy") + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Next Due Date</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.NextDueDate) ? "NA" : Convert.ToDateTime(item.NextDueDate).ToString("dd/MM/yyyy")) + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-12 col-sm-6 col-md-6 col-xl-6 mb-1\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Reference Id</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ReferenceId.ToString() + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Payment ID</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.PaymentId.ToString() + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3\"><p>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold text-colour7\">Points</span>");
    //                    lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + lobjModel.FloatToThousandSeperated(item.Amount) + "</span></p>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div>");
    //                    lsbTrISPBookingDetailsHtml.Append("</div></div></div></div>");
    //                    i++;
    //                }
    //            }
    //            else
    //            {
    //                lsbTrISPBookingDetailsHtml.Append("No Records Found.");
    //            }
    //            divISPFlightBookingDetails.InnerHtml = lsbTrISPBookingDetailsHtml.ToString();
    //        }
    //        else
    //        {
    //            Response.Redirect("Index.aspx", false);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LoggingAdapter.WriteLog("ManageBooking.aspx- BindISPBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
    //    }

    //}

    //[WebMethod]
    //public static string ShowInsuranceBookingDetails(string ReferenceId)
    //{
    //    StringBuilder lstrBookingdetailshtml = new StringBuilder();
    //    try
    //    {
    //        List<InsuranceBookingResponse> lobjListInsuranceDetails = HttpContext.Current.Session["InsuranceBookingDetailsofMember"] as List<InsuranceBookingResponse>;
    //        ABCModel lobjmodel = new ABCModel();
    //        string lstrCurrency = lobjmodel.GetDefaultCurrency();
    //        ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
    //        if (lobjListInsuranceDetails != null && lobjListInsuranceDetails.Count > 0)
    //        {
    //            InsuranceBookingResponse lobjInsuranceDetails = lobjListInsuranceDetails.Find(x => x.ReferenceId == ReferenceId);
    //            if(lobjInsuranceDetails != null)
    //            {
    //                lstrBookingdetailshtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
    //                lstrBookingdetailshtml.Append("<tr>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Service Name</td>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ lobjInsuranceDetails.ServiceName+ "</td></tr>");

    //                lstrBookingdetailshtml.Append("<tr>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Customer Name</td>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ lobjInsuranceDetails .CustomerName+ "</td></tr>");

    //                if (Convert.ToString(lobjInsuranceDetails.PolicyNo).IsNullOrEmpty())
    //                {
    //                    lstrBookingdetailshtml.Append("<tr>");
    //                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Request Id</td>");
    //                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">" + lobjInsuranceDetails.RequestId + "</td></tr>");
    //                }
    //                else {
    //                    lstrBookingdetailshtml.Append("<tr>");
    //                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Policy No</td>");
    //                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">" + lobjInsuranceDetails.PolicyNo + "</td></tr>");
    //                }
    //                lstrBookingdetailshtml.Append("<tr>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Reference Id</td>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ lobjInsuranceDetails.ReferenceId+ "</td></tr>");
    //                int lintTotalPrice = lobjmodel.ConvertToPoints(float.Parse(lobjInsuranceDetails.Amount.ToString())
    //                                 , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
    //                lstrBookingdetailshtml.Append("<tr>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Points</td>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">" + lobjmodel.FloatToThousandSeperated(lintTotalPrice) + " Points" + "</td></tr>");

    //                lstrBookingdetailshtml.Append("<tr>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Transaction Date</td>");
    //                lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")+"</td></tr>");
    //                lstrBookingdetailshtml.Append("</table>");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LoggingAdapter.WriteLog("ManageBooking.aspx- ShowInsuranceBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
    //    }
    //    return lstrBookingdetailshtml.ToString();
    //}
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
}