using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Platform.Member.Entites;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.ClientEntities;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using CB.IBE.Platform.Masters.Entities;
using ABC.Model;
using Core.Platform.Booking.Entities;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;

public partial class AirReviewAndConfirm : System.Web.UI.Page
{
    CreateItineraryResponse lobjCreateItineraryResponse = null;
    CreateItineraryRequest lobjCreateItineraryRequest = null;
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
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                }

                if (Session["ItineraryResponse"] != null)
                {
                    lobjCreateItineraryResponse = Session["ItineraryResponse"] as CreateItineraryResponse;
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    Session["MemberMiles"] = Convert.ToString(lobjModel.CheckAvailbility(lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToInt32(RelationType.LBMS), lstrCurrency, lobjProgramDefinition.ProgramId));
                    if (lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints <= 0)
                    {
                        Response.Redirect("BookingFailure.aspx", false);
                    }
                    else
                    {
                        lblTotalPoints.Text = lobjModel.IntToThousandSeperated(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints);
                    }
                    if (Convert.ToDouble(Session["MemberMiles"]) >= Convert.ToDouble(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints))
                    {
                        btnBookNow.Enabled = true;
                        divError.Style.Add("display", "none");
                        lblError.Text = "";
                    }
                    else
                    {
                        btnBookNow.Enabled = false;
                        divError.Style.Add("display", "block");
                        lblError.Text = "You need " + Convert.ToDouble(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints).ToString() + " Points to book this flight. Your available Points is " + Convert.ToDouble(Session["MemberMiles"]).ToString() + ".";
                    }
                    Session["ItineraryResponse"] = lobjCreateItineraryResponse;
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("AirReviewAndConfirm.aspx Pageload Exception:" + ex.Message + Environment.NewLine + "InnerException:" + ex.InnerException + Environment.NewLine + "StackTrace:" + ex.StackTrace);
        }

    }

    protected void btnBookNow_Click(object sender, EventArgs e)
    {
        ABCModel lobjModel = new ABCModel();
        MemberDetails lobjMemberDetails = new MemberDetails();
        lobjMemberDetails = Session["MemberDetails"] as MemberDetails;

        try
        {

            lobjCreateItineraryRequest = Session["ItineraryRequest"] as CreateItineraryRequest;
            lobjCreateItineraryResponse = Session["ItineraryResponse"] as CreateItineraryResponse;
            Session["FlightSearchPaymode"] = null;
            Session["BookingFlag"] = ServiceType.FLIGHT;

            string lstrCurrency = lobjModel.GetDefaultCurrency();

            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
            lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
            int ThreshouldValue = 0;

            ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.AIR.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
            int lintTotalPoints = Convert.ToInt32(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints);
            float lftAmount = Convert.ToSingle(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalBaseFare);

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
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.AIRREVIEWNCONFIRM;
                lobjOTPDetails.OtpType = Convert.ToString(OTPEnumTypes.AIRREVIEWNCONFIRM);
                HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails);
                //Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "International Flight");

                //if (Status)
                //{
                //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "AIR", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                //    lstrResponse = "ValidateOTP.aspx?flag=Air";
                //    Response.Redirect("ValidateOTP.aspx?flag=Air", false);
                //}
                //else
                //{
                //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "AIR", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                //    lstrResponse = "BookingFailure.aspx";
                //    Response.Redirect("BookingFailure.aspx", false);

                //}
            }
            //else
            //{
            //    lobjModel.LogActivity(string.Format("Flight Booking {0}: Requested", lobjRedemptionDetails.RelationReference), ActivityType.FlightBooking);
            //    lstrResponse = "PointGateway.aspx";
            //    Response.Redirect("PointGateway.aspx", false);
            //}

            HttpContext.Current.Session["BookingFlag"] = "flight";
            HttpContext.Current.Session["FlightTotalRedeemAmount"] = lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints;

            Response.Redirect("PaymentOptions.aspx", false);
            lobjModel.LogActivity(string.Format("AirReviewAndConfirm; Flight BookNow click; TotalFare-:{0}; Destination-:{1} Response-:{2};", lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalBaseFare, lobjCreateItineraryRequest.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryRequest.ItineraryDetails.DestinationLocation, lstrResponse), ActivityType.FlightBooking);

        }
        catch (Exception ex)
        {
            lobjModel.LogActivity(string.Format("Flight Booking {0}; Exception", lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference), ActivityType.FlightBooking);
            LoggingAdapter.WriteLog("AirReviewAndConfirm :" + ex.Message + ex.StackTrace);
            Response.Redirect("BookingFailure.aspx", false);
        }
    }
}