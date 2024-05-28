using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CB.IBE.Platform.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using ABC.Model;
using CB.IBE.Platform.Masters.Entities;
using System.Configuration;
using Core.Platform.MemberActivity.Constants;
using Framework.EnterpriseLibrary.Adapters;

public partial class AirPrintReceipt : System.Web.UI.Page
{
    ABCModel lobjModel = new ABCModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                string lstrRelativePath = this.Page.AppRelativeVirtualPath;
                int lintActivityId = lobjModel.LogActivity(lstrRelativePath.Replace("~", string.Empty).Replace("/", string.Empty), ActivityType.PageLoad);

            }
            if (Session["PageTitle"] != null)
            {
                this.Title = Session["PageTitle"].ToString();
            }
            if (!IsPostBack)
            {
                if (Session["PageTitle"] != null)
                {
                    this.Title = Convert.ToString(Session["PageTitle"]);
                }

                if ((Session["RetriveBookingInfo"] != null || Session["FlightBooked"] != null) && Session["MemberDetails"] != null)
                {
                    string lstrSourceCurrency = Convert.ToString(ConfigurationManager.AppSettings["SourceCurrency"]);
                    ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
                    string strPaymentDetails = string.Empty;
                    if (Session["FlightBooked"] != null)
                    {
                        lobjItineraryDetails = Session["FlightBooked"] as ItineraryDetails;
                        if (lobjItineraryDetails.BookingPaymentDetails.PaymentType.Equals(PaymentType.Points))
                        {
                            lblTotalMiles.Text = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjItineraryDetails.BookingPaymentDetails.Points));
                        }
                    }
                    else if (Session["RetriveBookingInfo"] != null)
                    {
                        lobjItineraryDetails = Session["RetriveBookingInfo"] as ItineraryDetails;

                        if (lobjItineraryDetails.BookingPaymentDetails.PaymentType.Equals(PaymentType.Points))
                        {
                            lblTotalMiles.Text = Convert.ToString(lobjModel.FloatToThousandSeperated(lobjItineraryDetails.BookingPaymentDetails.Points));
                        }
                    }
                    else
                    {
                        lobjItineraryDetails = null;
                    }
                    MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    lblMembershipReferenceNo.Text = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference;
                    lblCustomerName.Text = lobjMemberDetails.FirstName + " " + lobjMemberDetails.FullName;
                    //lblMemberName.Text = lobjMemberDetails.FirstName + " " + lobjMemberDetails.FullName;
                    lblCustomerMobileNo.Text = lobjMemberDetails.MobileNumber;
                    lblCustomerAddress.Text = lobjMemberDetails.Address;
                    lblCustomerEmail.Text = lobjMemberDetails.Email;
                    lblTransactionRefNo.Text = lobjItineraryDetails.ItineraryReference;
                    lblGDSPNR.Text = lobjItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].AirlinePNR;
                    //lblGDSPNR.Text = lobjItineraryDetails.ItineraryTripId;
                }
                else
                {
                    Response.Redirect("Index.aspx",false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("AirPrintReceipt.aspx Pageload Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
        }
    }
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
}