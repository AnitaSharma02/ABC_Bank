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
using KhaltiInsurance.Entities;
using KhaltiISP.Entities;
using CB.IBE.Platform.AirClientModel;
using System.Configuration;
using System.Web.Services;
using System.IO;
using System.Linq;
using Giift.ShopGateway.Client.Entities;
using Core.Platform.ProgramMaster.Entities;
using System.Security.Cryptography;

public partial class ManageBooking : Page
{
    ABCModel lobjModel = new ABCModel();
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
                BindDomesticBookingDetails(lobjMemberDetails);
                BindInsuranceBookingDetails(lobjMemberDetails);
                BindISPBookingDetails(lobjMemberDetails);
                lobjModel.LogActivity(string.Format("Visited ManageBooking.aspx; MemberId-:{0}", lobjMemberDetails.MemberRelationsList[0].RelationReference), ActivityType.PageLoad);
            }
            else
            {
                string CallbackUrl = HttpUtility.UrlEncode(Encrypt("ManageBooking.aspx"));
                HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
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
                List<ItineraryDetails> lobjListItineraryDetails = lobjModel.GetFlightBookingDetails(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                if (lobjListItineraryDetails != null && lobjListItineraryDetails.Count > 0)
                {
                    rptBookingDetails.DataSource = lobjListItineraryDetails;
                    //divrptflight.Attributes.Add("style", "Display:block");
                }
                else
                {
                    rptBookingDetails.DataSource = null;
                    lblFlightrecord.Visible = true;
                    lblFlightrecord.Text = "<span data-i18n='managebooking-norecords-label'>No Records Found.</span>";
                    // divFlight.Visible = false;
                    divFlightrecord.Visible = true;
                    //divrptflight.Attributes.Add("style", "Display:none");
                }
                rptBookingDetails.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
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

            lobjGetHotelInfoDetails = lobjModel.GetHotelDetails(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);

            if (lobjGetHotelInfoDetails.HotelDetails != null && lobjGetHotelInfoDetails.HotelDetails.Count > 0)
            {
                rptHotelCancelBookingDetails.DataSource = lobjGetHotelInfoDetails.HotelDetails;
                //divrpthotel.Attributes.Add("style", "Display:block");
            }
            else
            {
                rptHotelCancelBookingDetails.DataSource = null;
                lblHotelrecord.Visible = true;
                lblHotelrecord.Text = "<span data-i18n='managebooking-norecords-label'>No Records Found.</span>";
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
                List<TransactionDetails> lobjlstTransactionDetails = lobjModel.GetAllTransactions(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference).FindAll(lobj => lobj.LoyaltyTxnType == LoyaltyTxnType.Packages);
                //if (lobjlstTransactionDetails != null && lobjlstTransactionDetails.Count > 0)
                //{
                //    lsbTrExperienceBookingDetailsHtml.Append("<table width=\"100%\" bozrder=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"table amey\">");
                //    lsbTrExperienceBookingDetailsHtml.Append("<tr class=\"tbl_th\"><th class=\"vmid\"><div class=\"th-block-left\">No.</div></th>");
                //    lsbTrExperienceBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Product Title</div></th>");
                //    lsbTrExperienceBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Booking Ref ID</div></th>");
                //    lsbTrExperienceBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Order Date</div></th>");
                //    lsbTrExperienceBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">NPR</div></th>");
                //    lsbTrExperienceBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Action</div></th>");
                //    lsbTrExperienceBookingDetailsHtml.Append("</tr>");
                //    int i = 1;
                //    foreach (TransactionDetails item in lobjlstTransactionDetails)
                //    {
                //        lsbTrExperienceBookingDetailsHtml.Append("<tr style=\"color: #000000\" align=\"center\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<td class=\"bord_top\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + i + "</div>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</td>");
                //        lsbTrExperienceBookingDetailsHtml.Append("<td class=\"bord_top\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + item.MerchantName.Split('#')[0] + "</div>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</td>");
                //        lsbTrExperienceBookingDetailsHtml.Append("<td class=\"bord_top\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + item.MerchantName.Split('#')[2] + "</div>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</td>");
                //        lsbTrExperienceBookingDetailsHtml.Append("<td class=\"bord_top\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + item.TransactionDate.ToString("dd/MM/yyyy") + "</div>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</td>");
                //        lsbTrExperienceBookingDetailsHtml.Append("<td class=\"bord_top\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + lobjModel.StringToThousandSeperated(item.Points.ToString()) + "</div>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</td>");
                //        lsbTrExperienceBookingDetailsHtml.Append("<td class=\"bord_top\">");
                //        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"td-block-left fl\"><a href=\"/ExperienceProductBookingDetails.aspx?Id=" + item.MerchantName.Split('#')[1] + "\" class=\"view_detail\">View Details</a> </div>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</td>");
                //        lsbTrExperienceBookingDetailsHtml.Append("</tr>");
                //        i++;
                //    }
                //    lsbTrExperienceBookingDetailsHtml.Append("</table>");
                //}

                if (lobjlstTransactionDetails != null && lobjlstTransactionDetails.Count > 0)
                {
                    int i = 1;
                    foreach (TransactionDetails item in lobjlstTransactionDetails)
                    {
                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"row mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-12\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"bg-white p-3\">");

                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");
                        
                        /*
                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">No.</span> <span class=\"h6 d-block\">" + i + "</span>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");
                        */

                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-12 col-xl-4 mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Product Title</span> <span class=\"h6 d-block\">" + item.MerchantName.Split('#')[0] + "</span>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");

                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-2 mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Booking Ref ID</span> <span class=\"h6 d-block\">" + item.MerchantName.Split('#')[2] + "</span>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");

                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-2 mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Order Date</span> <span class=\"h6 d-block\">" + item.TransactionDate.ToString("dd/MM/yyyy") + "</span>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");

                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-2 mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Points</span> <span class=\"h6 d-block\">" + lobjModel.StringToThousandSeperated(item.Points.ToString()) + "</span>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");

                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-12 col-lg-3 col-xl-2 mt-2 mt-lg-0\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<a target=\"_blank\" href =\"/ExperienceProductBookingDetails.aspx?Id=" + item.MerchantName.Split('#')[1] + "\" class=\"btn btn-one w-100\">View Details</a>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");

                        lsbTrExperienceBookingDetailsHtml.Append("</div>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");

                        i++;
                    }
                    //lsbTrExperienceBookingDetailsHtml.Append("</table>");
                }
                else
                {
                    lsbTrExperienceBookingDetailsHtml.Append("No Records Found.");
                }
                divExperienceBookingDetails.InnerHtml = lsbTrExperienceBookingDetailsHtml.ToString();
            }
            else
            {
                Response.Redirect("SessionTimeout.aspx", false);
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
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            HttpContext.Current.Session["RetriveBookingInfo"] = null;
            CB.IBE.Platform.Masters.Entities.RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererData"] as CB.IBE.Platform.Masters.Entities.RefererDetails;
            RetriveItineraryDetails lobjRetriveItineraryDetails = lobjModel.RetriveItineraryDetails(TripId, Convert.ToInt32(lobjRefererDetails.Id));
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
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            HttpContext.Current.Session["HotelBooked"] = null;
            HttpContext.Current.Session["CustomerDetails"] = null;
            HttpContext.Current.Session["BookingResponse"] = null;

            HotelItineraryResponse lobjHotelItineraryResponse = lobjModel.GetBookedHotelInfo(TransactionReferenceCode);
            if (lobjHotelItineraryResponse != null)
            {
                HttpContext.Current.Session["HotelBooked"] = lobjHotelItineraryResponse.HotelSearchResponse;
                HttpContext.Current.Session["CustomerDetails"] = lobjHotelItineraryResponse.Customer;
                HttpContext.Current.Session["BookingResponse"] = lobjHotelItineraryResponse.HotelBookingResponse;

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
                    //lsbTrDomesticBookingDetailsHtml.Append("<table width=\"100%\" bozrder=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"table\">");
                    //lsbTrDomesticBookingDetailsHtml.Append("<tr class=\"tbl_th\"><th class=\"vmid\"><div class=\"th-block-left\">No.</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Flight Date</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Return Date</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">From</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">To</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Outbound Flight PNR</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Inbound Flight PNR</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Points</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("<th class=\"vmid\"><div class=\"th-block-left\">Action</div></th>");
                    //lsbTrDomesticBookingDetailsHtml.Append("</tr>");
                    int i = 1;
                    foreach (DomesticItineraryDetails item in lobjListItineraryDetails)
                    {
                        //lsbTrDomesticBookingDetailsHtml.Append("<tr style=\"color: #000000\" align=\"center\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + i + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + Convert.ToDateTime(item.FlightDate).ToString("dd/MM/yyyy") + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + (string.IsNullOrEmpty(item.ReturnDate) ? "NA" : Convert.ToDateTime(item.ReturnDate).ToString("dd/MM/yyyy")) + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + item.SectorFrom.ToString() + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + item.SectorTo.ToString() + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + item.OutboundPNR.ToString() + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + (string.IsNullOrEmpty(item.InboundPNR.ToString()) ? "NA" : item.InboundPNR.ToString()) + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\">" + lobjModel.StringToThousandSeperated(item.CreditsConsumed.ToString()) + "</div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<td class=\"bord_top\">");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td - block - left fl view_detail\" onclick=\"ShowDomesticFlightDetails(" + item.LogIds[0] + ")\">View Details </div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("<div class=\"td-block-left fl\"><a href=\"/DomesticAirFlightbookingDetails.ashx?LogId=" + item.LogIds[0] + "\" class=\"view_detail\">View Details</a> </div>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</td>");
                        //lsbTrDomesticBookingDetailsHtml.Append("</tr>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"row mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-12\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"bg-white p-3\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");

                        /*
                        lsbTrExperienceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 mb-1\">");
                        lsbTrExperienceBookingDetailsHtml.Append("<p>");
                        lsbTrExperienceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">No.</span> <span class=\"h6 d-block\">" + i + "</span>");
                        lsbTrExperienceBookingDetailsHtml.Append("</p>");
                        lsbTrExperienceBookingDetailsHtml.Append("</div>");
                        */

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Flight Date</span> <span class=\"h6 d-block\">" + Convert.ToDateTime(item.FlightDate).ToString("dd/MM/yyyy") + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Return Date</span> <span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.ReturnDate) ? "NA" : Convert.ToDateTime(item.ReturnDate).ToString("dd/MM/yyyy")) + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">From</span> <span class=\"h6 d-block\">" + item.SectorFrom.ToString() + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">To</span> <span class=\"h6 d-block\">" + item.SectorTo.ToString() + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Outbound Flight PNR</span> <span class=\"h6 d-block\">" + item.OutboundPNR.ToString() + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Inbound Flight PNR</span> <span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.InboundPNR.ToString()) ? "NA" : item.InboundPNR.ToString()) + "</span>");
                        lsbTrDomesticBookingDetailsHtml.Append("</p>");
                        lsbTrDomesticBookingDetailsHtml.Append("</div>");

                        lsbTrDomesticBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\">");
                        lsbTrDomesticBookingDetailsHtml.Append("<p>");
                        lsbTrDomesticBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Points</span> <span class=\"h6 d-block\">" + lobjModel.StringToThousandSeperated(item.CreditsConsumed.ToString()) + "</span>");
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
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindDomesticBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public void BindInsuranceBookingDetails(MemberDetails pobjMemberDetails)
    {
        StringBuilder lsbTrInsuranceBookingDetailsHtml = new StringBuilder();
        try
        {
            if (Session["MemberDetails"] != null)
            {
                List<InsuranceBookingResponse> lobjListInsuranceDetails = lobjModel.GetBookedInsuranceListForMember(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                ABCModel model = new ABCModel();
                string lstrCurrency = model.GetDefaultCurrency();
                ProgramDefinition lobjProgramDefinition = model.GetProgramMaster();
                Session["InsuranceBookingDetailsofMember"] = lobjListInsuranceDetails;
                lobjListInsuranceDetails = lobjListInsuranceDetails.OrderByDescending(x => x.PaymentDate).ToList();
                if (lobjListInsuranceDetails != null && lobjListInsuranceDetails.Count > 0)
                {

                    int i = 1;
                    foreach (InsuranceBookingResponse item in lobjListInsuranceDetails)
                    {
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"row mb-1\"><div class=\"col-12\"><div class=\"bg-white p-3\">");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");
                        //lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-12 col-xl-3 col-lg-4 col-md-6 mb-3\"><p>");
                        //lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">No.</span>");
                        //lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + i + "</span></p>");
                        //lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Service Name</span>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ServiceName.ToString() + "</span></p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-2 col-xl-2 mb-1\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Payment Date</span>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + Convert.ToDateTime(item.PaymentDate).ToString("dd/MM/yyyy") + "</span></p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-2 col-xl-2 mb-1\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Next Due Date</span>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.NextDueDate) ? "NA" :item.NextDueDate) + "</span></p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-5 col-xl-5 mb-1\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Reference Id</span>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ReferenceId.ToString() + "</span></p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Payment Id</span>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.PaymentId.ToString() + "</span></p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        int lintTotalPrice = model.ConvertToPoints(float.Parse(item.Amount.ToString())
                                , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Points</span>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + lobjModel.FloatToThousandSeperated(lintTotalPrice) + "</span></p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<div class=\"col-12 offset-md-3 col-md-3 col-lg-3 col-xl-3 mt-2 mt-md-0\"><p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("<a id=\"ViewDetails\" class=\"btn btn-one w-100\" onclick=\"return ViewDetails('" + item.ReferenceId.ToString()+"');\" data-i18n=\"btn-view-details\" data-toggle=\"modal\">View Details</a>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</p>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div>");
                        lsbTrInsuranceBookingDetailsHtml.Append("</div></div></div></div>");
                        i++;
                    }
                }
                else
                {
                    lsbTrInsuranceBookingDetailsHtml.Append("No Records Found.");
                }
                divInsuranceFlightBookingDetails.InnerHtml = lsbTrInsuranceBookingDetailsHtml.ToString();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindInsuranceBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public void BindISPBookingDetails(MemberDetails pobjMemberDetails)
    {
        StringBuilder lsbTrISPBookingDetailsHtml = new StringBuilder();
        try
        {
            if (Session["MemberDetails"] != null)
            {
                List<ISPBookingResponse> lobjListISPDetails = lobjModel.GetBookedISPListForMember(pobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                lobjListISPDetails = lobjListISPDetails.OrderByDescending(x => x.PaymentDate).ToList();
                if (lobjListISPDetails != null && lobjListISPDetails.Count > 0)
                {

                    int i = 1;
                    foreach (ISPBookingResponse item in lobjListISPDetails)
                    {
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"row mb-1\"><div class=\"col-12\"><div class=\"bg-white p-3\">");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"row justify-content-between\">");
                        //lsbTrISPBookingDetailsHtml.Append("<div class=\"col-12 col-xl-3 col-lg-4 col-md-6 mb-3\"><p>");
                        //lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">No.</span>");
                        //lsbTrISPBookingDetailsHtml.Append("<span class=\"d-inline-block heading-regular\">" + i + "</span></p>");
                        //lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Customer Name</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.CustomerName.ToString() + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Service Name</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ServiceName.ToString() + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Payment Date</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + Convert.ToDateTime(item.PaymentDate).ToString("dd/MM/yyyy") + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Next Due Date</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + (string.IsNullOrEmpty(item.NextDueDate) ? "NA" : Convert.ToDateTime(item.NextDueDate).ToString("dd/MM/yyyy")) + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-12 col-sm-6 col-md-6 col-xl-6 mb-1\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Reference Id</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.ReferenceId.ToString() + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3 mb-1\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Payment ID</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + item.PaymentId.ToString() + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("<div class=\"col-6 col-md-3 col-lg-3 col-xl-3\"><p>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h7 d-block heading-semibold\">Points</span>");
                        lsbTrISPBookingDetailsHtml.Append("<span class=\"h6 d-block\">" + lobjModel.FloatToThousandSeperated(item.Amount) + "</span></p>");
                        lsbTrISPBookingDetailsHtml.Append("</div>");
                        lsbTrISPBookingDetailsHtml.Append("</div></div></div></div>");
                        i++;
                    }
                }
                else
                {
                    lsbTrISPBookingDetailsHtml.Append("No Records Found.");
                }
                divISPFlightBookingDetails.InnerHtml = lsbTrISPBookingDetailsHtml.ToString();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- BindISPBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

    }

    [WebMethod]
    public static string ShowInsuranceBookingDetails(string ReferenceId)
    {
        StringBuilder lstrBookingdetailshtml = new StringBuilder();
        try
        {
            List<InsuranceBookingResponse> lobjListInsuranceDetails = HttpContext.Current.Session["InsuranceBookingDetailsofMember"] as List<InsuranceBookingResponse>;
            ABCModel lobjmodel = new ABCModel();
            string lstrCurrency = lobjmodel.GetDefaultCurrency();
            ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
            if (lobjListInsuranceDetails != null && lobjListInsuranceDetails.Count > 0)
            {
                InsuranceBookingResponse lobjInsuranceDetails = lobjListInsuranceDetails.Find(x => x.ReferenceId == ReferenceId);
                if(lobjInsuranceDetails != null)
                {
                    lstrBookingdetailshtml.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    lstrBookingdetailshtml.Append("<tr>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Service Name</td>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ lobjInsuranceDetails.ServiceName+ "</td></tr>");

                    lstrBookingdetailshtml.Append("<tr>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Customer Name</td>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ lobjInsuranceDetails .CustomerName+ "</td></tr>");

                    if (Convert.ToString(lobjInsuranceDetails.PolicyNo).IsNullOrEmpty())
                    {
                        lstrBookingdetailshtml.Append("<tr>");
                        lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Request Id</td>");
                        lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">" + lobjInsuranceDetails.RequestId + "</td></tr>");
                    }
                    else {
                        lstrBookingdetailshtml.Append("<tr>");
                        lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Policy No</td>");
                        lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">" + lobjInsuranceDetails.PolicyNo + "</td></tr>");
                    }
                    lstrBookingdetailshtml.Append("<tr>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Reference Id</td>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ lobjInsuranceDetails.ReferenceId+ "</td></tr>");
                    int lintTotalPrice = lobjmodel.ConvertToPoints(float.Parse(lobjInsuranceDetails.Amount.ToString())
                                     , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
                    lstrBookingdetailshtml.Append("<tr>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Points</td>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">" + lobjmodel.FloatToThousandSeperated(lintTotalPrice) + " Points" + "</td></tr>");

                    lstrBookingdetailshtml.Append("<tr>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">Transaction Date</td>");
                    lstrBookingdetailshtml.Append("<td width=\"50%\" style=\"font-family: Calibri; font-size: 14px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #231f20; padding: 10px; border:1px solid #dddddd; border-bottom: 2px solid #dddddd;\">"+ DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")+"</td></tr>");
                    lstrBookingdetailshtml.Append("</table>");
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ManageBooking.aspx- ShowInsuranceBookingDetails Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrBookingdetailshtml.ToString();
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
}