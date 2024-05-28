using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Booking.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AirReviewAndConfirmDomestic : System.Web.UI.Page
{
    BookingDetailsRequest lobjBookingDetailsRequest = new BookingDetailsRequest();
    CreateDomesticBookingResponse lobjCreateDomesticBookingResponse = new CreateDomesticBookingResponse();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ABCModel lobjModel = new ABCModel();

                if (Session["PageTitle"] != null)
                {
                    this.Title = Session["PageTitle"].ToString();
                }
                MemberDetails lobjMemberDetails = new MemberDetails();
                if (Session["MemberDetails"].Equals(null))
                {
                    Response.Redirect("Index.aspx", false);
                }
                else
                {
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                }
                if (Session["DomesticFlightBookingResponse"] != null)
                {
                    lobjCreateDomesticBookingResponse= Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    Session["MemberMiles"] = Convert.ToString(lobjModel.CheckAvailbility(lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToInt32(RelationType.LBMS), lstrCurrency, lobjProgramDefinition.ProgramId));
                    if (lobjCreateDomesticBookingResponse.CreditsConsumed <= 0)
                    {
                        Response.Redirect("BookingFailure.aspx", false);
                    }
                    else
                    {
                        lblTotalPoints.Text = lobjModel.IntToThousandSeperated(lobjCreateDomesticBookingResponse.CreditsConsumed);
                    }
                    if (Convert.ToDouble(Session["MemberMiles"]) >= Convert.ToDouble(lobjCreateDomesticBookingResponse.CreditsConsumed))
                    {
                        btnBookNow.Enabled = true;
                        divError.Style.Add("display", "none");
                        lblError.Text = "";
                    }
                    else
                    {
                        btnBookNow.Enabled = false;
                        divError.Style.Add("display", "block");
                        lblError.Text = "You need " + Convert.ToDouble(lobjCreateDomesticBookingResponse.CreditsConsumed).ToString() + " NPoints to book this flight. Your available NPoints is " + Convert.ToDouble(Session["MemberMiles"]).ToString() + ".";
                    }

                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("AirReviewAndConfirmDomestic.aspx Pageload Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
        }
    }

    protected void btnBookNow_Click(object sender, EventArgs e)
    {
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = new MemberDetails();
        lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
        try
        {
            lobjBookingDetailsRequest = Session["DomesticFlightBookingRequest"] as BookingDetailsRequest;
            lobjCreateDomesticBookingResponse = Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;
            Session["FlightSearchPaymode"] = null;
            Session["BookingFlag"] = ServiceType.FLIGHT;

            string lstrCurrency = lobjModel.GetDefaultCurrency();

            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
            lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
            int ThreshouldValue = 0;

            ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.AIR.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
            int lintTotalPoints = Convert.ToInt32(lobjCreateDomesticBookingResponse.CreditsConsumed);
            float lftAmount = Convert.ToSingle(lobjCreateDomesticBookingResponse.CreditsConsumed);

            List<RedemptionDetails> lobjListOfRedemptionDetails = new List<RedemptionDetails>();
            RedemptionDetails lobjRedemptionDetails = new RedemptionDetails();
            lobjRedemptionDetails.Currency = lstrCurrency;
            lobjRedemptionDetails.DisplayCurrency = lobjModel.CurrencyDisplayText(lstrCurrency);
            lobjRedemptionDetails.Points = lintTotalPoints;
            lobjRedemptionDetails.RelationReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
            lobjRedemptionDetails.Amount = lftAmount;

            lobjListOfRedemptionDetails.Add(lobjRedemptionDetails);
            Session["RedemptionDetails"] = lobjListOfRedemptionDetails;
            string lstrResponse = string.Empty;

            if (ThreshouldValue <= lintTotalPoints && !ThreshouldValue.Equals(-1))
            {
                bool Status = false;
                OTPDetails lobjOTPDetails = new OTPDetails();
                lobjOTPDetails.UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM;
                lobjOTPDetails.OtpType = Convert.ToString(OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM);
                HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails, "Domestic Flight");
                //if (Status)
                //{
                //    lobjModel.LogActivity(string.Format("KHALTI AIR ReviewConfirm OTP Request For Member {0} Status- {1}", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                //    lstrResponse = "ValidateOTP.aspx?flag=KhaltiAirKhaltiAir";
                //    Response.Redirect("ValidateOTP.aspx?flag=KhaltiAir", false);
                //}
                //else
                //{
                //    lobjModel.LogActivity(string.Format("KHALTI AIR ReviewConfirm OTP Request For Member {0} Status- {1}", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                //    lstrResponse = "BookingFailure.aspx";
                //    Response.Redirect("BookingFailure.aspx", false);

                //}
            }
            //else
            //{
            //    lobjModel.LogActivity(string.Format("Flight Booking Domestic {0}: Requested", lobjRedemptionDetails.RelationReference), ActivityType.FlightBookingForDomestic);
            //    lstrResponse = "/PointGateway.aspx?flag=KhaltiAir";
            //    Response.Redirect("PointGateway.aspx?flag=KhaltiAir", false);
            //}
            HttpContext.Current.Session["BookingFlag"] = "Domesticflight";
            HttpContext.Current.Session["FlightTotalRedeemAmount"] = lobjCreateDomesticBookingResponse.CreditsConsumed;
            Response.Redirect("PaymentOptions.aspx", false);
            lobjModel.LogActivity(string.Format("AirReviewAndConfirm; Flight BookNow click; TotalFare-:{0}; Response-:{1};", lobjCreateDomesticBookingResponse.CreditsConsumed, lstrResponse), ActivityType.FlightBookingForDomestic);
          

        }
        catch (Exception ex)
        {
            Response.Redirect("BookingFailure.aspx", false);
        }
    }
}