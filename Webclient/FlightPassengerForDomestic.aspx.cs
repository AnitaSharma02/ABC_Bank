using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FlightPassengerForDomestic : System.Web.UI.Page
{
    ABCModel objmodel = new ABCModel();
    SearchRequestForDomestic lobjSearchFlight;
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
        if (Session["PageTitle"] != null)
        {
            this.Title = Session["PageTitle"].ToString();
        }

        try
        {
            if (!IsPostBack)
            {
                ABCModel lobjModel = new ABCModel();
                if (Session["FinalBookedFlightDetails"] != null && Session["MemberDetails"] != null)
                {
                    int lintAdults;
                    int lintChild;
                    lobjSearchFlight = (SearchRequestForDomestic)Session["SearchFlightForDomestic"]; ;

                    lintAdults = lobjSearchFlight.Adults;
                    lintChild = lobjSearchFlight.Childrens;
                    int Total = lintAdults + lintChild;

                    List<int> lintAdultGenerator = new List<int>();
                    for (int i = 0; i < lintAdults; i++)
                    {
                        lintAdultGenerator.Add(i);
                    }
                    rptAdultControl.DataSource = lintAdultGenerator;
                    rptAdultControl.DataBind();
                    rptAdultControl.Visible = true;
                    AdultInfo.Visible = true;

                    List<int> lintChildGenerator = new List<int>();
                    for (int i = 0; i < lintChild; i++)
                    {
                        lintChildGenerator.Add(i);
                        rptChildControl.Visible = true;
                        ChildInfo.Visible = true;
                    }

                    rptChildControl.DataSource = lintChildGenerator;
                    rptChildControl.DataBind();


                    MemberDetails lobjMemberDetails = new MemberDetails();
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    FinalFlightResult lobjItineraryDetails = new FinalFlightResult();
                    lobjItineraryDetails = (FinalFlightResult)Session["FinalBookedFlightDetails"];

                    List<FlightDetails> SelectedFlightsOutbound = (List<FlightDetails>)Session["SelectedFlightsDetailsOutbound"];
                    List<FlightDetails> SelectedFlightsInbound = (List<FlightDetails>)Session["SelectedFlightsDetailsInbound"];
                    int lintTotalPoints = 0;
                    if (lobjItineraryDetails != null && SelectedFlightsOutbound[0] != null)
                    {
                        rptDeparture.DataSource = SelectedFlightsOutbound;
                        rptDeparture.DataBind();
                    }
                    if (Session["SelectedFlightsDetailsInbound"] != null)
                    {
                        if (lobjItineraryDetails != null && SelectedFlightsInbound[0] != null)
                        {
                            lblarrival.Visible = true;
                            rptArrival.DataSource = SelectedFlightsInbound;
                            rptArrival.DataBind();
                        }
                    }
                    lblTotalPoints.Text = objmodel.IntToThousandSeperated(Convert.ToInt32(lobjItineraryDetails.FareTotal));
                    lintTotalPoints = Convert.ToInt32(lobjItineraryDetails.FareTotal);


                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    int lintComBankPoints = lobjModel.CheckAvailbility(lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToInt32(RelationType.LBMS), lstrCurrency, lobjProgramDefinition.ProgramId);
                    if (string.IsNullOrEmpty(lobjMemberDetails.Email))
                    {
                        errorDiv.InnerHtml = "You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.";
                        errorDiv.Visible = true;
                        btnSubmit.Visible = false;
                    }
                    else
                    {
                        if (lintComBankPoints > lintTotalPoints)
                        {
                            errorDiv.InnerHtml = " ";
                            errorDiv.Visible = false;
                            btnSubmit.Visible = true;
                        }
                        else
                        {
                            errorDiv.InnerHtml = "Insufficient balance ";
                            errorDiv.Visible = true;
                            btnSubmit.Visible = false;
                        }
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightPassengerForDomestic.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    protected void btnBookFlightDomestic_Click(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;
            int SupplierID = lobjRefererDetails.RefererSupplierProperties.SupplierId;
            if (Page.IsValid)
            {
                if (Session["FinalBookedFlightDetails"] != null && Session["MemberDetails"] != null)
                {
                    FinalFlightResult lobjItineraryDetails = (FinalFlightResult)Session["FinalBookedFlightDetails"];

                    BookingDetailsRequest lobjBookingDetailsRequest = new BookingDetailsRequest();
                    PassengersContactInfo passengersContactInfo = new PassengersContactInfo();
                    List<PassengerDetailsForDomestic> lobjListOfPassengerDetails = new List<PassengerDetailsForDomestic>();
                    lobjModel.LogActivity(string.Format("Selected Flight for Booking Domestic; Destination-:{0}; Total Amount-{1};Airline Name-:{2}", lobjItineraryDetails.Departure_Departure + "-" + lobjItineraryDetails.Departure_Arrival, lobjItineraryDetails.FareTotal, lobjItineraryDetails.Departure_AirlineName), ActivityType.FlightBookingForDomestic);
                    for (int i = 0; i < rptAdultControl.Items.Count; i++)
                    {
                        Panel lobjAdultControlHolder = rptAdultControl.Items[i].FindControl("panelAdultControlHolder") as Panel;
                        UserControl lobjAdultControl = lobjAdultControlHolder.FindControl("AdultPassangerDetails_Domestic") as UserControl;
                        DropDownList ddlTitle = lobjAdultControl.FindControl("ddlTitle") as DropDownList;

                        TextBox txtFirstName = lobjAdultControl.FindControl("txtFirstName") as TextBox;
                        TextBox txtLastName = lobjAdultControl.FindControl("txtLastName") as TextBox;
                        TextBox txtType = lobjAdultControl.FindControl("txtType") as TextBox;
                        DropDownList ddlNationality = lobjAdultControl.FindControl("drpNationality") as DropDownList;
                        DropDownList ddlGender = lobjAdultControl.FindControl("ddlGender") as DropDownList;


                        PassengerDetailsForDomestic lobjADTPassengerDetails = new PassengerDetailsForDomestic();
                        lobjADTPassengerDetails.Title = ddlTitle.SelectedItem.Text.ToString();
                        lobjADTPassengerDetails.FirstName = txtFirstName.Text;
                        lobjADTPassengerDetails.LastName = txtLastName.Text;
                        lobjADTPassengerDetails.Type = txtType.Text;
                        lobjADTPassengerDetails.Nationality = ddlNationality.SelectedItem.Value.ToString();
                        lobjADTPassengerDetails.Gender = ddlGender.SelectedItem.Value.ToString();
                       
                        lobjListOfPassengerDetails.Add(lobjADTPassengerDetails);
                    }

                    for (int i = 0; i < rptChildControl.Items.Count; i++)
                    {
                        Panel lobjChildControlHolder = rptChildControl.Items[i].FindControl("panelChildControlHolder") as Panel;
                        UserControl lobjChildControl = lobjChildControlHolder.FindControl("ChildPassangerDetails_Domestic") as UserControl;
                        DropDownList ddlTitle = lobjChildControl.FindControl("ddlTitle") as DropDownList;

                        TextBox txtFirstName = lobjChildControl.FindControl("txtFirstName") as TextBox;
                        TextBox txtLastName = lobjChildControl.FindControl("txtLastName") as TextBox;
                        TextBox txtType = lobjChildControl.FindControl("txtType") as TextBox;
                        DropDownList ddlNationality = lobjChildControl.FindControl("drpNationality") as DropDownList;
                        DropDownList ddlGender = lobjChildControl.FindControl("ddlGender") as DropDownList;

                        PassengerDetailsForDomestic lobjCNNPassengerDetails = new PassengerDetailsForDomestic();
                        lobjCNNPassengerDetails.Title = ddlTitle.SelectedItem.Text.ToString();
                        lobjCNNPassengerDetails.FirstName = txtFirstName.Text;
                        lobjCNNPassengerDetails.LastName = txtLastName.Text;
                        lobjCNNPassengerDetails.Type = txtType.Text;
                        lobjCNNPassengerDetails.Nationality = ddlNationality.SelectedItem.Value.ToString();
                        lobjCNNPassengerDetails.Gender = ddlGender.SelectedItem.Value.ToString();                    
                        lobjListOfPassengerDetails.Add(lobjCNNPassengerDetails);
                    }

                    MemberDetails lobjMemberDetails = new MemberDetails();
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    if (lobjMemberDetails != null)
                    {
                        passengersContactInfo.ContactName = lobjMemberDetails.FullName;
                        passengersContactInfo.ContactPhone = lobjMemberDetails.MobileNumber;
                        passengersContactInfo.ContactEmail = lobjMemberDetails.Email;
                    }

                    lobjBookingDetailsRequest.Token= ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
                    lobjBookingDetailsRequest.FlightId = lobjItineraryDetails.Departure_FlightId;
                    lobjBookingDetailsRequest.BookingId = Convert.ToInt32(HttpContext.Current.Session["DomesticFlightBookingId"]);
                    if (lobjItineraryDetails.IsReturn)
                    {
                        lobjBookingDetailsRequest.ReturnFlightId = lobjItineraryDetails.Return_FlightId;
                    }
                    else
                    {
                        lobjBookingDetailsRequest.ReturnFlightId = string.Empty;
                    }
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    lobjBookingDetailsRequest.PointRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);
                    if (lobjMemberDetails != null)
                    {
                        lobjBookingDetailsRequest.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                    }
                    lobjBookingDetailsRequest.PassengersContactInfo = passengersContactInfo;
                    lobjBookingDetailsRequest.PassengersInfo = lobjListOfPassengerDetails;

                    Session["DomesticFlightBookingRequest"] = lobjBookingDetailsRequest;
                    CreateDomesticBookingResponse lobjCreateDomesticBookingResponse = lobjModel.GetDomesticBookingResponse(lobjBookingDetailsRequest);
                    if (lobjCreateDomesticBookingResponse != null)
                    {
                       // Session["ReviewFlightDetails"] = lobjCreateDomesticBookingResponse.ItineraryDetails;
                        Session["DomesticFlightBookingResponse"] = lobjCreateDomesticBookingResponse;
                        lobjModel.LogActivity("Create Domestic Flight Booking : Success", ActivityType.FlightBookingForDomestic);
                        Response.Redirect("AirReviewAndConfirmDomestic.aspx", false);
                    }
                    else
                    {
                        lobjModel.LogActivity("Create Domestic Flight Booking : Failed", ActivityType.FlightBookingForDomestic);
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                }
                else
                {
                    lobjModel.LogActivity("Create Domestic Flight Booking : Failed", ActivityType.FlightBookingForDomestic);
                    Response.Redirect("ErrorPage.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightPassenger.aspx btnBookFlight_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}