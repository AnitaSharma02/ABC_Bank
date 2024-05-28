using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Integrations.Hotels.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using Core.Platform.Member.Entites;
using Core.Platform.OTP.Entities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.Booking.Entities;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using System.Text.RegularExpressions;
using ABC.Model;
using Framework.EnterpriseLibrary.Adapters;
using System.Web;

public partial class HotelBookingDetails : Page
{
    ABCModel lobjModel = new ABCModel();
    public int count = 0;
    static string lstrCurrency = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["HotelSelected"] = "HotelBookingDetails.aspx?hotelid=" + Request.QueryString["hotelid"].ToString() + "&roomtypecode=" + Request.QueryString["roomtypecode"];
                if (Session["MemberDetails"] != null)
                {
                    if (Session["hotels"] != null)
                    {
                        HotelSearchResponse lobjSearchResponse = Session["hotels"] as HotelSearchResponse;
                        HotelSearchResponse lobjSelectedSearchResponse = this.GetSelectedHotel(lobjSearchResponse);
                        Hotel lobjHotel = lobjSelectedSearchResponse.SearchResponse.hotels.hotel[0];
                        MemberDetails lobjMemberDetails = new MemberDetails();
                        lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                        HotelRepriceRequest hotelRepriceRequest = new HotelRepriceRequest();
                        hotelRepriceRequest.Hotel = lobjHotel;
                        hotelRepriceRequest.RefererDetails = Application["RefererData"] as RefererDetails;
                        lstrCurrency = lobjModel.GetDefaultCurrency();
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                        hotelRepriceRequest.RedemptionRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.HOT.ToString(), lobjProgramDefinition.ProgramId);
                        HotelRepriceResponse hotelRepriceResponse = lobjModel.HotelReprice(hotelRepriceRequest);
                        if (hotelRepriceResponse != null)
                        {
                            lobjHotel = hotelRepriceResponse.Hotel;
                            List<Hotel> hotels = new List<Hotel>();
                            hotels.Add(hotelRepriceResponse.Hotel);
                            lobjSelectedSearchResponse.SearchResponse.hotels.hotel = hotels.ToArray();
                            Session["SelectedHotel"] = lobjSelectedSearchResponse;
                            Session["MemberMiles"] = Convert.ToString(lobjModel.CheckAvailbility(lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToInt32(RelationType.LBMS), lstrCurrency, lobjProgramDefinition.ProgramId));
                            if (string.IsNullOrEmpty(lobjMemberDetails.Email))
                            {
                                divError.Style.Add("display", "block");
                                lblError.Text = "You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.";
                                btnBook.Enabled = false;
                                Bookbtn.Visible = false;
                            }
                            else
                            {
                                if (Convert.ToDouble(Session["MemberMiles"]) >= Convert.ToDouble(lobjHotel.roomrates.RoomRate[0].TotalPoints))
                                {
                                    divError.Style.Add("display", "none");
                                    lblError.Text = "";
                                    btnBook.Enabled = true;
                                    Bookbtn.Visible = true;
                                }
                                else
                                {
                                    divError.Style.Add("display", "block");
                                    double ldblAmount = lobjHotel.roomrates.RoomRate[0].TotalPoints;
                                    lblError.Text = "You need " + lobjModel.FloatToThousandSeperated(Convert.ToSingle(lobjHotel.roomrates.RoomRate[0].TotalPoints)) + " points to book this hotel. Your available points is " + lobjModel.FloatToThousandSeperated(Convert.ToSingle(Session["MemberMiles"])) + ".";
                                    btnBook.Enabled = false;
                                    Bookbtn.Visible = false;
                                }
                            }
                            lblTotalCharge.Text = lobjModel.FloatToThousandSeperated(lobjHotel.roomrates.RoomRate[0].TotalPoints) + " <span data-i18n='car-points-label'>points </span>";
                            lblHotelName.Text = lobjHotel.basicinfo.hotelname;
                            lblAddress.Text = lobjHotel.basicinfo.address + " - " + lobjHotel.basicinfo.city + ", " + lobjHotel.basicinfo.state + ", " + lobjHotel.basicinfo.country + " " + lobjHotel.basicinfo.countrycode + ".";
                            string strbaseurl = lobjSearchResponse.SearchResponse.baseurl;
                            if (strbaseurl.Equals(string.Empty))
                            {
                                imgHotel.ImageUrl = lobjHotel.basicinfo.thumbnailimage;
                            }
                            else if (strbaseurl.Contains("http://www.cleartrip.com"))
                            {
                                imgHotel.ImageUrl = lobjSearchResponse.SearchResponse.baseurl + lobjHotel.basicinfo.thumbnailimage;
                            }
                            else
                            {
                                imgHotel.ImageUrl = "http://www.cleartrip.com" + lobjSearchResponse.SearchResponse.baseurl + lobjHotel.basicinfo.thumbnailimage;
                            }
                            HotelSearchRequest lobjSearchRequest = Session["SearchDetails"] as HotelSearchRequest;
                            lblTotalMiles.Text = lobjModel.FloatToThousandSeperated(lobjHotel.roomrates.RoomRate[0].TotalPoints);
                            DateTime Chkin = Convert.ToDateTime(lobjSearchResponse.SearchResponse.searchcriteria.checkindate);
                            lblCheckinDate.Text = Chkin.ToString("MMM dd");
                            DateTime ChkOut = Convert.ToDateTime(lobjSearchResponse.SearchResponse.searchcriteria.checkoutdate);
                            lblCheckoutDate.Text = ChkOut.ToString("MMM dd");
                            lblNoofNights.Text = lobjSearchResponse.SearchResponse.searchcriteria.numberofnights + " Night(s)";
                            int TotalAdult = 0;
                            string[] arrayAdultPerRoom = lobjSearchRequest.SearchRequest.AdultPerRoom.Split(',');
                            for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
                            {
                                TotalAdult = TotalAdult + Convert.ToInt32(arrayAdultPerRoom[i]);
                            }
                            int TotalChild = 0;
                            string[] arrayChildPerRoom = lobjSearchRequest.SearchRequest.ChildrenPerRoom.Split(',');
                            for (int i = 0; i < arrayChildPerRoom.Count(); i++)
                            {
                                TotalChild = TotalChild + Convert.ToInt32(arrayChildPerRoom[i]);
                            }
                            if (TotalChild.Equals(0))
                                lblNoOfAdult.Text = Convert.ToString(TotalAdult) + " Adult(s)";
                            else
                                lblNoOfAdult.Text = Convert.ToString(TotalAdult) + " Adult(s)<br/>" + Convert.ToString(TotalChild) + " Child(ren)";
                            lobjModel.LogActivity(string.Format("HotelBookingDetails page load; HotelName-:{0}; TotalCharge-:{1} NPR;", lobjHotel.basicinfo.hotelname, lobjHotel.roomrates.RoomRate[0].TotalBaseAmount), ActivityType.HotelBooking);
                        }
                        else
                        {
                            Response.Redirect("BookingFailure.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("Index.aspx", false);
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
            LoggingAdapter.WriteLog("HotelBookingDetails.aspx- Page_Load Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public HotelSearchResponse GetSelectedHotel(HotelSearchResponse pobjSearchResponse)
    {
        Hotel lobjHotel = new Hotel();
        HotelSearchResponse lobjSearchResponse = new HotelSearchResponse();
        try
        {
            if (Request.QueryString["hotelid"] != null && Request.QueryString["hotelid"].ToString() != "" && Request.QueryString["roomtypecode"] != null && Request.QueryString["roomtypecode"].ToString() != "")
            {
                lobjHotel = pobjSearchResponse.SearchResponse.hotels.hotel.Where(lobjSelectedHotel => lobjSelectedHotel.hotelid.Equals(Request.QueryString["hotelid"].ToString())).FirstOrDefault();
                RoomRate lobjRoom = lobjHotel.roomrates.RoomRate.Where(lobjRoomRate => lobjRoomRate.roomtype.roomtypecode.Equals(Request.QueryString["roomtypecode"])).FirstOrDefault();
                List<RoomRate> lobjListOfRoomRate = new List<RoomRate>();
                lobjListOfRoomRate.Add(lobjRoom);
                RoomRates lobjRoomRates = new RoomRates();
                lobjRoomRates.RoomRate = lobjListOfRoomRate.ToArray();
                lobjHotel.roomrates = lobjRoomRates;
                Framework.Integrations.Hotels.Entities.Hotels lobjHotels = new Framework.Integrations.Hotels.Entities.Hotels();
                List<Hotel> lobjListOfHotel = new List<Hotel>();
                lobjListOfHotel.Add(lobjHotel);
                lobjHotels.hotel = lobjListOfHotel.ToArray();
                lobjSearchResponse.SearchResponse.hotels = lobjHotels;
                lobjSearchResponse.SearchResponse.searchcriteria = pobjSearchResponse.SearchResponse.searchcriteria;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelBookingDetails.aspx- GetSelectedHotel Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjSearchResponse;
    }
    public Customer CustomerInfo(CustomerDetail objCustomerInfo)
    {
        ABCModel lobjModel = new ABCModel();
        Customer lobjCustomer = lobjModel.CustomerInfo(objCustomerInfo);
        return lobjCustomer;
    }
    protected void custom_NameValidate(object sender, ServerValidateEventArgs e)
    {
        Regex r = new Regex("^[a-zA-Z]+$");
        if (r.IsMatch(e.Value))
            e.IsValid = true;
        else
            e.IsValid = false; ;
    }
    protected void btnBook_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Session["MemberDetails"] != null && Session["Hotels"] != null && Session["SearchDetails"] != null)
                {
                    MemberDetails lobjMemberDetails = new MemberDetails();
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    HotelSearchResponse lobjSearchResponse = Session["Hotels"] as HotelSearchResponse;
                    HotelSearchResponse lobjSelectedSearchResponse = Session["SelectedHotel"] as HotelSearchResponse;
                    Hotel lobjHotel = lobjSelectedSearchResponse.SearchResponse.hotels.hotel[0];
                    lobjHotel.SpecialRequest = txtSpecialRequest.Text.ToString();
                    lobjSelectedSearchResponse.SearchResponse.hotels.hotel[0] = lobjHotel;
                    CustomerDetail objCustomerInfo = new CustomerDetail();
                    objCustomerInfo.PersonalTitle = ddlPersonalTitle.SelectedItem.Text;
                    objCustomerInfo.FirstName = txtFirstName.Text.ToString();
                    objCustomerInfo.Lastname = txtLastname.Text.ToString();
                    objCustomerInfo.City = txtCity.Text.ToString();
                    objCustomerInfo.State = txtState.Text.ToString();
                    objCustomerInfo.Address = txtCity.Text.ToString();
                    objCustomerInfo.Address2 = txtState.Text.ToString();
                    objCustomerInfo.Country = txtCountry.Text.ToString();
                    objCustomerInfo.PostalCode = txtPostalCode.Text.ToString();
                    objCustomerInfo.PhoneNo = txtPhoneNo.Text.ToString();
                    objCustomerInfo.MobileNo = txtMobileNo.Text.ToString();
                    objCustomerInfo.EmailID = txtEmailID.Text.ToString();
                    Customer lobjCustomer = new Customer();
                    lobjCustomer = this.CustomerInfo(objCustomerInfo);
                    Session["CustomerDetails"] = lobjCustomer;
                    Session["Hotels"] = null;
                    Session["BookedHotel"] = lobjSelectedSearchResponse;
                    Session["BookingFlag"] = ServiceType.HOTEL;
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                    lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                    int ThreshouldValue = 0;
                    ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.HOT.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                    int lintTotalPoints = Convert.ToInt32(lobjSelectedSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalPoints);
                    float lftAmount = Convert.ToSingle(lobjSelectedSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalBaseAmount);
                    List<RedemptionDetails> lobjListOfRedemptionDetails = new List<RedemptionDetails>();
                    RedemptionDetails lobjRedemptionDetails = new RedemptionDetails();
                    lobjRedemptionDetails.Currency = lstrCurrency;
                    lobjRedemptionDetails.DisplayCurrency = lobjModel.CurrencyDisplayText(lstrCurrency);
                    lobjRedemptionDetails.Points = lintTotalPoints;
                    lobjRedemptionDetails.RelationReference = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                    lobjRedemptionDetails.Amount = lftAmount;
                    lobjListOfRedemptionDetails.Add(lobjRedemptionDetails);
                    Session["RedemptionDetails"] = lobjListOfRedemptionDetails;
                    if (ThreshouldValue <= lintTotalPoints && !ThreshouldValue.Equals(-1))
                    {
                        bool Status = false;
                        OTPDetails lobjOTPDetails = new OTPDetails();
                        lobjOTPDetails.UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
                        lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.HOTELREVIEWNCONFIRM;
                        lobjOTPDetails.OtpType = Convert.ToString(OTPEnumTypes.HOTELREVIEWNCONFIRM);
                        HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                        //Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Hotel");
                        ////Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails);
                        //if (Status)
                        //{
                        //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "HOTEL", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                        //    Response.Redirect("ValidateOTP.aspx?flag=Hotel", false);
                        //}
                        //else
                        //{
                        //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "HOTEL", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                        //    Response.Redirect("BookingFailure.aspx", false);
                        //}
                    }
                    //else
                    //{
                    //    lobjModel.LogActivity(string.Format("Hotel Booking {0}: Requested", lobjRedemptionDetails.RelationReference), ActivityType.HotelBooking);
                    //    Response.Redirect("PointGateway.aspx?flag=Hotel", false);
                    //}
                    HttpContext.Current.Session["BookingFlag"] = "hotel";
                    HttpContext.Current.Session["HotelTotalRedeemAmount"] = lobjHotel.roomrates.RoomRate[0].TotalPoints;

                    Response.Redirect("PaymentOptions.aspx");
                }
                else
                {
                    Response.Redirect("BookingFailure.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("HotelBookingDetails.aspx- btnBook_Click Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
}