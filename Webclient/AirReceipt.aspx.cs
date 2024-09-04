using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Platform.Member.Entites;
using CB.IBE.Platform.Masters.Entities;
using CB.IBE.Platform.Entities;
using System.Configuration;
using ABC.Model;
using Core.Platform.Booking.Entities;
using Core.Platform.MemberActivity.Entities;
using CB.IBE.Platform.ClientEntities;
using Framework.EnterpriseLibrary.Adapters;

public partial class AirReceipt : System.Web.UI.Page
{
    ABCModel objmodel = new ABCModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

                string lstrRelativePath = this.Page.AppRelativeVirtualPath;
                int lintActivityId = objmodel.LogActivity(lstrRelativePath.Replace("~", string.Empty).Replace("/", string.Empty), ActivityType.PageLoad);

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
                    if (Session["FlightBookedResponse"] != null)
                    {
                        BookingResponse lobjBookingResponse = Session["FlightBookedResponse"] as BookingResponse;

                        if (lobjBookingResponse.PNRDetails.IsItineraryDateChange)
                        {
                            ItineraryTimeChanged.Style["display"] = "block";
                        }
                        else
                        {
                            ItineraryTimeChanged.Style["display"] = "none";
                        }
                    }
                    else
                    {
                        ItineraryTimeChanged.Style["display"] = "none";
                    }

                    string lstrSourceCurrency = Convert.ToString(ConfigurationManager.AppSettings["SourceCurrency"]);
                    ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
                    string strPaymentDetails = string.Empty;
                    if (Session["FlightBooked"] != null)
                    {
                        lobjItineraryDetails = Session["FlightBooked"] as ItineraryDetails;
                        if (lobjItineraryDetails.BookingPaymentDetails.PaymentType.Equals(PaymentType.Points))
                        {
                            lblTotalMiles.Text = Convert.ToString(objmodel.FloatToThousandSeperated(lobjItineraryDetails.BookingPaymentDetails.Points));
                        }
                    }
                    else if (Session["RetriveBookingInfo"] != null)
                    {
                        lobjItineraryDetails = Session["RetriveBookingInfo"] as ItineraryDetails;

                        if (lobjItineraryDetails.BookingPaymentDetails.PaymentType.Equals(PaymentType.Points))
                        {
                            lblTotalMiles.Text = Convert.ToString(objmodel.FloatToThousandSeperated(lobjItineraryDetails.BookingPaymentDetails.Points));
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

                    lblGDSPNR.Text = lobjItineraryDetails.ItineraryTripId;


                    //  lblGDSPNR.Text = lobjItineraryDetails.ItineraryTripId;

                }
                else
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("AirReceipt.aspx Pageload Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
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