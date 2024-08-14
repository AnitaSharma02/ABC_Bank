using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ABC.Model;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using CB.IBE.Platform.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using IBEAPIGateway.Model;

public partial class FlightPassenger : System.Web.UI.Page
{
    ABCModel objmodel = new ABCModel();
    SearchRequest lobjSearchFlight;

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
                if (Session["SelectedItinerary"] != null && Session["MemberDetails"] != null)
                {
                    int lintAdults;
                    int lintChild;
                    int lintInfants;

                    lobjSearchFlight = (SearchRequest)Session["SearchFlight"];

                    lintAdults = lobjSearchFlight.SearchDetails.Adults;
                    lintChild = lobjSearchFlight.SearchDetails.Childrens;
                    lintInfants = lobjSearchFlight.SearchDetails.Infants;
                    int Total = lintAdults + lintChild + lintInfants;

                    List<int> lintAdultGenerator = new List<int>();
                    for (int i = 0; i < lintAdults; i++)
                    {
                        lintAdultGenerator.Add(i);
                    }
                    rptAdultControl.DataSource = lintAdultGenerator;
                    rptAdultControl.DataBind();
                    rptAdultControl.Visible = true;
                    AdultInfo.Visible = true;


                    //rptAdultControlMob.DataSource = lintAdultGenerator;
                    //rptAdultControlMob.DataBind();
                    //rptAdultControlMob.Visible = true;
                    //AdultInfoMob.Visible = true;

                    List<int> lintChildGenerator = new List<int>();
                    for (int i = 0; i < lintChild; i++)
                    {
                        lintChildGenerator.Add(i);
                        rptChildControl.Visible = true;
                        ChildInfo.Visible = true;
                        //rptChildControlMob.Visible = true;
                        //ChildInfoMob.Visible = true;
                    }

                    rptChildControl.DataSource = lintChildGenerator;
                    rptChildControl.DataBind();


                    //rptChildControlMob.DataSource = lintChildGenerator;
                    //rptChildControlMob.DataBind();


                    List<int> lintInfantGenerator = new List<int>();
                    for (int i = 0; i < lintInfants; i++)
                    {
                        lintInfantGenerator.Add(i);
                        rptInfantControl.Visible = true;
                        InfantInfo.Visible = true;
                        //rptInfantControlMob.Visible = true;
                        //InfantInfoMob.Visible = true;
                    }
                    rptInfantControl.DataSource = lintInfantGenerator;
                    rptInfantControl.DataBind();


                    MemberDetails lobjMemberDetails = new MemberDetails();
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
                    lobjItineraryDetails = (ItineraryDetails)Session["SelectedItinerary"];
                    int lintTotalPoints = 0;
                    if (lobjItineraryDetails != null && lobjItineraryDetails.ListOfFlightDetails[0] != null)
                    {
                        rptDeparture.DataSource = lobjItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments;
                        rptDeparture.DataBind();
                    }
                    if (lobjItineraryDetails != null && lobjItineraryDetails.ListOfFlightDetails.Count > 1)
                    {
                        lblarrival.Visible = true;
                        rptArrival.DataSource = lobjItineraryDetails.ListOfFlightDetails[1].ListOfFlightSegments;
                        rptArrival.DataBind();
                    }
                    if (Session["DomesticOnwardFlights"] != null)
                    {
                        lblTotalPoints.Text = Convert.ToString(lobjItineraryDetails.ListOfFlightDetails[0].FareDetails.TotalPoints);
                        lintTotalPoints = Convert.ToInt32(lobjItineraryDetails.ListOfFlightDetails[0].FareDetails.TotalPoints);
                        if (Session["DomesticReturnFlights"] != null)
                        {
                            lblTotalPoints.Text = Convert.ToString(lobjItineraryDetails.ListOfFlightDetails[0].FareDetails.TotalPoints + lobjItineraryDetails.ListOfFlightDetails[1].FareDetails.TotalPoints);
                            lintTotalPoints = Convert.ToInt32(lobjItineraryDetails.ListOfFlightDetails[0].FareDetails.TotalPoints + lobjItineraryDetails.ListOfFlightDetails[1].FareDetails.TotalPoints);
                        }
                        Session["DomesticOnwardFlights"] = null;
                        Session["DomesticReturnFlights"] = null;
                    }
                    else
                    {
                        lblTotalPoints.Text = Convert.ToString(lobjItineraryDetails.FareDetails.TotalPoints);
                        lintTotalPoints = Convert.ToInt32(lobjItineraryDetails.FareDetails.TotalPoints);
                    }
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
            LoggingAdapter.WriteLog("FlightPassenger.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public bool FFPValidator(string drpVal, string pstrFFPno)
    {
        if (drpVal == string.Empty && pstrFFPno != string.Empty)
            return false;
        else if (drpVal != string.Empty && pstrFFPno == string.Empty)
            return false;
        else if (drpVal == string.Empty && pstrFFPno == string.Empty)
            return false;
        else if (drpVal != string.Empty && pstrFFPno != string.Empty)
        {
            if (pstrFFPno.Length < 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
            return false;
    }

    protected void btnBookFlight_Click(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            IBEAPIModel lobjIBEAPIModel = new IBEAPIModel();
            RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;
            //    int SupplierID = lobjRefererDetails.RefererSupplierProperties.SupplierId;
            int SupplierID = 0;
            if (Page.IsValid)
            {
                if (Session["SelectedItinerary"] != null && Session["MemberDetails"] != null)
                {

                    ItineraryDetails lobjItineraryDetails = (ItineraryDetails)Session["SelectedItinerary"];
                    List<PassengerDetails> lobjListOfPassengerDetails = new List<PassengerDetails>();
                    lobjModel.LogActivity(string.Format("Selected Flight for Booking; Destination-:{0}; Total Amount-{1};Airline Name-:{2}", lobjItineraryDetails.OriginLocation + "-" + lobjItineraryDetails.DestinationLocation, lobjItineraryDetails.FareDetails.TotalBaseFare, lobjItineraryDetails.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName), ActivityType.FlightBooking);
                    for (int i = 0; i < rptAdultControl.Items.Count; i++)
                    {
                        Panel lobjAdultControlHolder = rptAdultControl.Items[i].FindControl("panelAdultControlHolder") as Panel;
                        UserControl lobjAdultControl = lobjAdultControlHolder.FindControl("adultdetails") as UserControl;
                        DropDownList ddlTitle = lobjAdultControl.FindControl("ddlTitle") as DropDownList;

                        TextBox txtFirstName = lobjAdultControl.FindControl("txtFirstName") as TextBox;
                        TextBox txtLastName = lobjAdultControl.FindControl("txtLastName") as TextBox;
                        TextBox txtDOB = lobjAdultControl.FindControl("txtDOB") as TextBox;
                        TextBox txtPassportNumber = lobjAdultControl.FindControl("txtPassportNo") as TextBox;

                        TextBox txtEmailID = lobjAdultControl.FindControl("txtEmailID") as TextBox;
                        DropDownList ddlNationality = lobjAdultControl.FindControl("drpNationality") as DropDownList;
                        TextBox txtTelephone = lobjAdultControl.FindControl("txtTelephone") as TextBox;
                        TextBox txtPassportIssueLocation = lobjAdultControl.FindControl("txtPassportIssueLocation") as TextBox;
                        TextBox txtEffectiveDate = lobjAdultControl.FindControl("txtEffectiveDate") as TextBox;
                        TextBox txtExpiryDate = lobjAdultControl.FindControl("txtExpiryDate") as TextBox;

                        PassengerDetails lobjADTPassengerDetails = new PassengerDetails();
                        lobjADTPassengerDetails.Prefix = ddlTitle.SelectedItem.Text.ToString();
                        lobjADTPassengerDetails.FirstName = txtFirstName.Text;
                        lobjADTPassengerDetails.LastName = txtLastName.Text;
                        lobjADTPassengerDetails.FFPNo = string.Empty;

                        if (txtEffectiveDate.Text != "")
                        {
                            lobjADTPassengerDetails.PassportIssueDate = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtEffectiveDate.Text));
                        }
                        lobjADTPassengerDetails.Nationality = ddlNationality.SelectedItem.Text.ToString();

                        if (txtExpiryDate.Text != "")
                        {
                            lobjADTPassengerDetails.PassportExpiryDate = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtExpiryDate.Text));
                        }

                        lobjADTPassengerDetails.EmailId = txtEmailID.Text;
                        lobjADTPassengerDetails.MobileNo = txtTelephone.Text;
                        lobjADTPassengerDetails.DOB = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtDOB.Text));
                        lobjADTPassengerDetails.PaxType = "ADT";
                        lobjADTPassengerDetails.Age = CalculateAge(lobjIBEAPIModel.StringToDateTime(txtDOB.Text));
                        lobjADTPassengerDetails.PassportNumber = txtPassportNumber.Text.ToString();
                        lobjADTPassengerDetails.Gender = ddlTitle.SelectedItem.Value.ToString();
                        lobjADTPassengerDetails.Country = ddlNationality.SelectedItem.Text.ToString();
                        lobjADTPassengerDetails.CountryCode = ddlNationality.SelectedValue;

                        lobjADTPassengerDetails.Address = ddlNationality.SelectedValue;
                        lobjADTPassengerDetails.CityName = ddlNationality.SelectedValue;

                        lobjListOfPassengerDetails.Add(lobjADTPassengerDetails);
                    }

                    for (int i = 0; i < rptChildControl.Items.Count; i++)
                    {
                        Panel lobjChildControlHolder = rptChildControl.Items[i].FindControl("panelChildControlHolder") as Panel;
                        UserControl lobjChildControl = lobjChildControlHolder.FindControl("childdetails") as UserControl;
                        DropDownList ddlTitle = lobjChildControl.FindControl("ddlTitle") as DropDownList;
                        DropDownList ddlGender = lobjChildControl.FindControl("DropDownListGender") as DropDownList;
                        TextBox txtFirstName = lobjChildControl.FindControl("txtFirstName") as TextBox;
                        TextBox txtLastName = lobjChildControl.FindControl("txtLastName") as TextBox;
                        TextBox txtDOB = lobjChildControl.FindControl("txtDOB") as TextBox;
                        TextBox txtPassportNumber = lobjChildControl.FindControl("txtPassportNo") as TextBox;

                        TextBox txtEmailID = lobjChildControl.FindControl("txtEmailID") as TextBox;
                        DropDownList ddlNationality = lobjChildControl.FindControl("drpNationality") as DropDownList;
                        TextBox txtTelephone = lobjChildControl.FindControl("txtTelephone") as TextBox;
                        TextBox txtPassportIssueLocation = lobjChildControl.FindControl("txtPassportIssueLocation") as TextBox;
                        TextBox txtEffectiveDate = lobjChildControl.FindControl("txtEffectiveDate") as TextBox;
                        TextBox txtExpiryDate = lobjChildControl.FindControl("txtExpiryDate") as TextBox;

                        PassengerDetails lobjCNNPassengerDetails = new PassengerDetails();
                        lobjCNNPassengerDetails.Prefix = ddlTitle.SelectedItem.Text.ToString();
                        lobjCNNPassengerDetails.FirstName = txtFirstName.Text;
                        lobjCNNPassengerDetails.LastName = txtLastName.Text;
                        lobjCNNPassengerDetails.FFPNo = string.Empty;

                        if (txtEffectiveDate.Text != "")
                        {
                            lobjCNNPassengerDetails.PassportIssueDate = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtEffectiveDate.Text));
                        }
                        lobjCNNPassengerDetails.Nationality = ddlNationality.SelectedItem.Text.ToString();

                        if (txtExpiryDate.Text != "")
                        {
                            lobjCNNPassengerDetails.PassportExpiryDate = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtExpiryDate.Text));
                        }

                        lobjCNNPassengerDetails.EmailId = txtEmailID.Text;
                        lobjCNNPassengerDetails.MobileNo = txtTelephone.Text;
                        lobjCNNPassengerDetails.DOB = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtDOB.Text));

                        if (SupplierID.Equals(6))
                        {
                            lobjCNNPassengerDetails.PaxType = "CNN";
                        }
                        else
                        {
                            lobjCNNPassengerDetails.PaxType = "CHD";
                        }

                        lobjCNNPassengerDetails.Age = CalculateAge(lobjIBEAPIModel.StringToDateTime(txtDOB.Text));
                        lobjCNNPassengerDetails.PassportNumber = txtPassportNumber.Text.ToString();
                        lobjCNNPassengerDetails.Gender = ddlTitle.SelectedItem.Value.ToString();
                        lobjCNNPassengerDetails.Country = ddlNationality.SelectedValue;
                        lobjCNNPassengerDetails.Address = ddlNationality.SelectedValue;
                        lobjCNNPassengerDetails.CityName = ddlNationality.SelectedValue;

                        lobjListOfPassengerDetails.Add(lobjCNNPassengerDetails);
                    }

                    for (int i = 0; i < rptInfantControl.Items.Count; i++)
                    {
                        Panel lobjInfantControlHolder = rptInfantControl.Items[i].FindControl("panelInfantControlHolder") as Panel;
                        UserControl lobjInfantControl = lobjInfantControlHolder.FindControl("infantdetails") as UserControl;
                        DropDownList ddlGender = lobjInfantControl.FindControl("DropDownListGender") as DropDownList;
                        DropDownList ddlTitle = lobjInfantControl.FindControl("ddlTitle") as DropDownList;
                        TextBox txtFirstName = lobjInfantControl.FindControl("txtFirstName") as TextBox;
                        TextBox txtLastName = lobjInfantControl.FindControl("txtLastName") as TextBox;
                        TextBox txtDOB = lobjInfantControl.FindControl("txtDOB") as TextBox;
                        TextBox txtPassportNumber = lobjInfantControl.FindControl("txtPassportNo") as TextBox;

                        TextBox txtEmailID = lobjInfantControl.FindControl("txtEmailID") as TextBox;
                        DropDownList ddlNationality = lobjInfantControl.FindControl("drpNationality") as DropDownList;
                        TextBox txtTelephone = lobjInfantControl.FindControl("txtTelephone") as TextBox;
                        TextBox txtPassportIssueLocation = lobjInfantControl.FindControl("txtPassportIssueLocation") as TextBox;
                        TextBox txtEffectiveDate = lobjInfantControl.FindControl("txtEffectiveDate") as TextBox;
                        TextBox txtExpiryDate = lobjInfantControl.FindControl("txtExpiryDate") as TextBox;

                        PassengerDetails lobjINFPassengerDetails = new PassengerDetails();
                        lobjINFPassengerDetails.Prefix = ddlTitle.SelectedItem.Text.ToString();
                        lobjINFPassengerDetails.FirstName = txtFirstName.Text;
                        lobjINFPassengerDetails.LastName = txtLastName.Text;
                        lobjINFPassengerDetails.FFPNo = string.Empty;

                        if (txtEffectiveDate.Text != "")
                        {
                            lobjINFPassengerDetails.PassportIssueDate = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtEffectiveDate.Text));
                        }
                        lobjINFPassengerDetails.Nationality = ddlNationality.SelectedItem.Text.ToString();

                        if (txtExpiryDate.Text != "")
                        {
                            lobjINFPassengerDetails.PassportExpiryDate = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtExpiryDate.Text));
                        }

                        lobjINFPassengerDetails.EmailId = txtEmailID.Text;
                        lobjINFPassengerDetails.MobileNo = txtTelephone.Text;
                        lobjINFPassengerDetails.DOB = Convert.ToDateTime(lobjIBEAPIModel.StringToDateTime(txtDOB.Text));
                        lobjINFPassengerDetails.PaxType = "INF";
                        lobjINFPassengerDetails.Age = CalculateAge(lobjIBEAPIModel.StringToDateTime(txtDOB.Text));
                        lobjINFPassengerDetails.PassportNumber = txtPassportNumber.Text.ToString();
                        lobjINFPassengerDetails.Gender = ddlTitle.SelectedItem.Value.ToString();
                        lobjINFPassengerDetails.Country = ddlNationality.SelectedItem.Text.ToString();
                        lobjINFPassengerDetails.CountryCode = ddlNationality.SelectedValue;
                        lobjINFPassengerDetails.Address = ddlNationality.SelectedValue;
                        lobjINFPassengerDetails.CityName = ddlNationality.SelectedValue;

                        lobjListOfPassengerDetails.Add(lobjINFPassengerDetails);
                    }

                    lobjItineraryDetails.TravelerInfo = lobjListOfPassengerDetails;

                    MemberDetails lobjMemberDetails = new MemberDetails();
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;

                    lobjItineraryDetails.BillingInfo = lobjListOfPassengerDetails[0];
                    lobjSearchFlight = (SearchRequest)Session["SearchFlight"];
                    lobjItineraryDetails.CabinType = lobjSearchFlight.SearchDetails.Cabin;

                    lobjItineraryDetails.Adults = lobjSearchFlight.SearchDetails.Adults;
                    lobjItineraryDetails.Childrens = lobjSearchFlight.SearchDetails.Childrens;
                    lobjItineraryDetails.Infants = lobjSearchFlight.SearchDetails.Infants;
                    // lobjItineraryDetails.RefererDetails = lobjSearchFlight.RefererDetails;
                    // lobjItineraryDetails.AirSearchId = lobjSearchFlight.SearchId;
                    lobjItineraryDetails.MemberId = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);

                    if (lobjMemberDetails != null)
                    {
                        PassengerDetails lobjDelivaryDetails = new PassengerDetails();

                        lobjDelivaryDetails.Prefix = lobjMemberDetails.Gender.Equals("M") ? "Mr" : "Mrs";
                        lobjDelivaryDetails.FirstName = lobjMemberDetails.FirstName;
                        lobjDelivaryDetails.LastName = lobjMemberDetails.FullName;
                        lobjDelivaryDetails.Address = lobjMemberDetails.Address;
                        lobjDelivaryDetails.Address += lobjMemberDetails.Address2;
                        lobjDelivaryDetails.Country = lobjMemberDetails.Nationality;
                        lobjDelivaryDetails.EmailId = lobjMemberDetails.Email;
                        lobjDelivaryDetails.MobileNo = lobjMemberDetails.MobileNumber;
                        lobjItineraryDetails.DeliveryInfo = lobjDelivaryDetails;
                    }
                    else
                    {
                        lobjItineraryDetails.DeliveryInfo = lobjListOfPassengerDetails[0];
                    }

                    Session["SelectedItinerary"] = null;

                    SearchResponse lobjSearchResponse = new SearchResponse();
                    lobjSearchResponse = HttpContext.Current.Session["Flights"] as SearchResponse;

                    CreateItineraryRequest lobjCreateItineraryRequest = new CreateItineraryRequest();
                    lobjCreateItineraryRequest.SupplierDetails.Id = SupplierID;
                    lobjCreateItineraryRequest.SessionId = lobjSearchResponse.SessionId;
                    lobjCreateItineraryRequest.ItineraryDetails = lobjItineraryDetails;
                    //    lobjCreateItineraryRequest.ItineraryDetails.RefererDetails = lobjSearchFlight.RefererDetails;
                    lobjCreateItineraryRequest.PointRate = Convert.ToSingle(lobjSearchFlight.PointRate);
                    Session["ItineraryRequest"] = lobjCreateItineraryRequest;

                    CreateItineraryResponse lobjCreateItineraryResponse = lobjIBEAPIModel.CreateItinerary(lobjCreateItineraryRequest);
                    if (lobjCreateItineraryResponse != null)
                    {
                        Session["ReviewFlightDetails"] = lobjCreateItineraryResponse.ItineraryDetails;
                        Session["ItineraryResponse"] = lobjCreateItineraryResponse;
                        lobjModel.LogActivity("Create Itinerary : Success", ActivityType.FlightBooking);
                        Response.Redirect("AirReviewAndConfirm.aspx", false);
                    }
                    else
                    {
                        lobjModel.LogActivity("Create Itinerary : Failed", ActivityType.FlightBooking);
                        Response.Redirect("ErrorPage.aspx", false);
                    }

                }
                else
                {
                    lobjModel.LogActivity("Create Itinerary : Failed", ActivityType.FlightBooking);
                    Response.Redirect("ErrorPage.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("FlightPassenger.aspx btnBookFlight_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    public static int CalculateAge(DateTime birthDate)
    {
        DateTime now = DateTime.Now;
        int age = now.Year - birthDate.Year;
        if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            age--;
        return age;
    }
}