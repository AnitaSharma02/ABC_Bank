using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AirReceipt_Domestic : System.Web.UI.Page
{
    ABCModel objmodel = new ABCModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string lstrRelativePath = this.Page.AppRelativeVirtualPath;
                int lintActivityId = objmodel.LogActivity(lstrRelativePath.Replace("~", string.Empty).Replace("/", string.Empty), ActivityType.PageLoad);
                if (Session["PageTitle"] != null)
                {
                    this.Title = Convert.ToString(Session["PageTitle"]);     
                }
                if ((Session["BookingStatusResponseForDomestic"] != null || Session["DomesticFlightBookingResponse"] != null) && Session["MemberDetails"] != null)
                {
                    string lstrSourceCurrency = Convert.ToString(ConfigurationManager.AppSettings["SourceCurrency"]);
                    CreateDomesticBookingResponse lobjItineraryDetails = new CreateDomesticBookingResponse();
                    string strPaymentDetails = string.Empty;
                    if (Session["DomesticFlightBookingResponse"] != null)
                    {
                        lobjItineraryDetails = Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;
                        
                            lblTotalMiles.Text = Convert.ToString(objmodel.FloatToThousandSeperated(lobjItineraryDetails.CreditsConsumed));
                    }
                    else
                    {
                        lobjItineraryDetails = null;
                    }
                    MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    lblMembershipReferenceNo.Text = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
                    lblCustomerName.Text = lobjMemberDetails.FirstName + " " + lobjMemberDetails.FullName;
                    lblCustomerMobileNo.Text = lobjMemberDetails.MobileNumber;
                    lblCustomerAddress.Text = lobjMemberDetails.Address;
                    lblCustomerEmail.Text = lobjMemberDetails.Email;

                    BookingStatusResponseForDomestic lobjBookingStatusResponseForDomestic = Session["BookingStatusResponseForDomestic"] as BookingStatusResponseForDomestic;
                    lblTransactionRefNo.Text = lobjBookingStatusResponseForDomestic.Detail.Reference;

                    //lblGDSPNR.Text = lobjBookingStatusResponseForDomestic.Detail.Outbound.Pnrno;

                }
                else
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("AirReceipt_Domestic.aspx Pageload Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
        }
    }

    protected void btnBookNow_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["DomesticFlightBookingResponse"] != null)
            {
                CreateDomesticBookingResponse lobjBookingStatusResponseForDomestic = Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;
                TicketDownloadRequest lobjTicketDownloadRequest = new TicketDownloadRequest();
                lobjTicketDownloadRequest.LogId = lobjBookingStatusResponseForDomestic.LogIds[0];
                lobjTicketDownloadRequest.Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                lobjTicketDownloadRequest.isBase64 = true;
                TicketDownloadResponse lobjTicketDownloadResponse = objmodel.TicketDownload(lobjTicketDownloadRequest);
                if (lobjTicketDownloadResponse.Status && !string.IsNullOrEmpty(lobjTicketDownloadResponse.Data))
                {
                    string base64String = lobjTicketDownloadResponse.Data; 
                    Response.Clear();
                    Response.AddHeader("Content-Type", "application/pdf");
                    Response.AddHeader("Content-Disposition", "inline;");
                    Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
                    Response.AddHeader("Pragma", "public");
                    Response.BinaryWrite(Convert.FromBase64String(base64String));
                    Response.Flush();
                    Response.Close();

                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("AirReceipt_Domestic.aspx btnBookNow_Click Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
        }
    }
}