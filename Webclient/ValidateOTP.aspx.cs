using System;
using System.Web;
using System.Web.UI;
using Core.Platform.OTP.Entities;
using ABC.Model;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Entities;
using System.Configuration;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.Transactions.Entites;
using Stripe.Climate;
using System.Collections.Generic;
using Giift.ShopGateway.Client.Entities;
using System.Linq;
using GiiftShopGateway.Model;
using Core.Platform.Booking.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using KhaltiInsurance.Entities;
using KhaltiISP.Entities;
using CB.IBE.Platform.Masters.Entities;
using Newtonsoft.Json;
using GiiftPaymentGateway.Entities;
using Stripe.Checkout;

public partial class ValidateOTP : Page
{
    public string ResendOTPEnableTime { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ResendOTPEnableTime = ConfigurationManager.AppSettings["ResendOTPEnableTime"].ToString();
            if (Session["MemberDetails"] == null)
            {
                Response.Redirect("Login.aspx", false);
            }
            if (!IsPostBack)
            {
                Session["OTPCount"] = 0;
                HttpContext.Current.Session["IsForgetPasswordOTPValidated"] = "false";
                HttpContext.Current.Session["ResendOTPEnableTime"] = ResendOTPEnableTime;
                HttpContext.Current.Session["ResendOTPRequestTime"] = DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ValidateOTP.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string CheckOTP(string pstrOTP, string strFlag)
    {
        try
        {
            int lstrOTPCount = Convert.ToInt32(HttpContext.Current.Session["OTPCount"]);
            lstrOTPCount += 1;
            HttpContext.Current.Session["OTPCount"] = lstrOTPCount;
            ABCModel lobjModel = new ABCModel();
            OTPDetails lobjOTPDetails = new OTPDetails();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            lobjOTPDetails.UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
            lobjOTPDetails.OTP = Convert.ToInt32(pstrOTP);
            ShopModel lobjshopmodel = new ShopModel();
            string merchantname = string.Empty;
            if (strFlag == "Air")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.AIRREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.AIRREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx";
            }
            else if (strFlag == "Hotel")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.HOTELREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.HOTELREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=Hotel";
            }
            else if (strFlag == "Car")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.CARREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.CARREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=Car";
            }
            else if (strFlag == "GiftCard" || strFlag == "EventGiftCard" || strFlag == "AirMilesTopUp" || strFlag == "TopUp" || strFlag == "Lounge" || strFlag == "UtilityGiftCard")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM;
            }
            else if (strFlag == "Donation")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM;
            }
            else if (strFlag == "Package")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.PACKAGEREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.PACKAGEREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=Package";
            }
            else if (strFlag == "Shop")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.SHOPREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.SHOPREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=Shop";
            }
            else if (strFlag == "ShopDigital")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=ShopDigital";
            }

            bool Status = lobjModel.CheckRedemptionOTP(lobjOTPDetails);

            if (lstrOTPCount < 5)
            {
                if (Status)
                {
                    lobjModel.LogActivity("Validate ReviewConfirmOTP: Success", ActivityType.ReviewConfirmOTPSuccess);
                    return strFlag;
                }
                else
                {
                    strFlag = "Invalid OTP";
                    lobjModel.LogActivity("Validate ReviewConfirmOTP: " + strFlag, ActivityType.ReviewConfirmOTPFailed);
                }
            }
            else
            {
                strFlag = "Exceed OTP limit";
                lobjModel.LogActivity("Validate ReviewConfirmOTP: " + strFlag, ActivityType.ReviewConfirmOTPFailed);
                HttpContext.Current.Session.Abandon();
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ValidateOTP.aspx CheckOTP Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return strFlag;
    }

    protected void btnResendOTP_Click(object sender, EventArgs e)
    {
        string strFlag = Convert.ToString(Request.QueryString["flag"]);
        string redemptionType = string.Empty;
        bool Status = false;
        lblResendOTPMsg.Text = "";
        try
        {
            if (HttpContext.Current.Session["ResendOTPRequestTime"] == null)
            {
                lblResendOTPMsg.Text = "OTPRequestNULL";
            }
            else
            {
                DateTime dtOTPSendTime = (DateTime)HttpContext.Current.Session["ResendOTPRequestTime"];
                int Timer = Convert.ToInt16(HttpContext.Current.Session["ResendOTPEnableTime"].ToString());
                TimeSpan tsResendOTPtime = TimeSpan.FromSeconds(Timer);
                TimeSpan tstimeDiff = DateTime.Now - dtOTPSendTime;
                double i = tstimeDiff.TotalSeconds;
                if (tsResendOTPtime.TotalSeconds > tstimeDiff.TotalSeconds)
                {
                    lblResendOTPMsg.Text = "Wait for " + Math.Floor((tsResendOTPtime.TotalSeconds - tstimeDiff.TotalSeconds)).ToString() + " seconds to resend OTP.";
                }
                else
                {
                    MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                    if (lobjMemberDetails != null && lobjMemberDetails.FullName != "")
                    {
                        ABCModel lobjModel = new ABCModel();
                        OTPDetails lobjOTPDetails = new OTPDetails();
                        lobjOTPDetails.UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
                        if (strFlag == "Air")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.AIRREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.AIRREVIEWNCONFIRM;
                            redemptionType = "International Flight";
                        }
                        else if (strFlag == "Hotel")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.HOTELREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.HOTELREVIEWNCONFIRM;
                            redemptionType = "Hotel";
                        }
                        else if (strFlag == "Car")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.CARREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.CARREVIEWNCONFIRM;
                            redemptionType = "Car";
                        }
                        else if (strFlag == "GiftCard" || strFlag == "EventGiftCard" || strFlag == "AirMilesTopUp" || strFlag == "TopUp" || strFlag == "Lounge")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM;
                            redemptionType = strFlag;
                        }
                        else if (strFlag == "Donation")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM;
                            redemptionType = "Donation";
                        }
                        else if (strFlag == "Package")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.PACKAGEREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.PACKAGEREVIEWNCONFIRM;
                            redemptionType = "Experience";
                        }
                        else if (strFlag == "Shop")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.SHOPREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.SHOPREVIEWNCONFIRM;
                            redemptionType = "Shop";
                        }
                        else if (strFlag == "ShopDigital")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.SHOPDIGITALREVIEWNCONFIRM;
                            redemptionType = Convert.ToString(Request.QueryString["redemptiontype"]);
                        }

                        Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails, redemptionType);

                        if (Status)
                        {
                            lblResendOTPMsg.Text = "OTP Resend Successful.";
                        }

                        HttpContext.Current.Session["ResendOTPRequestTime"] = DateTime.Now;
                    }
                    else
                    {
                        lblResendOTPMsg.Text = "Session Expired.Please Login and try again.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ValidateOTP.aspx btnResendOTP_Click Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    public static string GenereteRandomNumber()
    {
        Random mobjRandom = new Random();
        return mobjRandom.Next(100000, 200000).ToString();
    }

    public static string CreateStipePayment(StripePaymentDetails pobjStripePaymentDetails, MemberDetails pobjMemberDetails, string orderId, string merchantname)
    {
        ABCModel lobjPaymentModel = new ABCModel();
        PGRequest pgRequest = new PGRequest();
        string redirectPGUrl = string.Empty;
        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
        try
        {
            string lstrSuccessurl = Convert.ToString(ConfigurationManager.AppSettings["PGSuccessurl"]);
            string lstrCancelurl = Convert.ToString(ConfigurationManager.AppSettings["PGCancelurl"]);
            decimal amount = Convert.ToDecimal(pobjStripePaymentDetails.ReqRedeemAmount);
            bool lblCreateStatus = false;
            bool lblUpdateStatus = false;
            PGResponse lobjPGResponse = null;
            pgRequest = new PGRequest()
            {
                orderAmount = Convert.ToInt32(amount),
                orderCurrency = "INR",// Convert.ToString(ConfigurationManager.AppSettings["PGCurrency"])
                orderId = orderId,
                orderMeta = new PGOrderMeta()
                {
                    returnUrl = lstrSuccessurl
                },
                customerDetails = new PGCustomerDetails()
                {
                    customerName = pobjMemberDetails.FullName,
                    customerEmail = pobjMemberDetails.Email,
                    customerId = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                    customerPhone = pobjMemberDetails.MobileNumber
                }
            };
            lblCreateStatus = lobjPaymentModel.InitiatePayment(pgRequest);
            HttpContext.Current.Session["PGPaymentRequest"] = pgRequest;
            if (lblCreateStatus)
            {
                lobjPGResponse = HttpContext.Current.Session["PGPaymentResponse"] as PGResponse;
                redirectPGUrl = lobjPGResponse.data[0].paymentUrlLink;
            }
        }
        catch (Exception ex)
        {
            lobjPaymentModel.RollBackTransaction(pgRequest.orderId, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, merchantname);
            LoggingAdapter.WriteLog("PaymentReviewConfirm CreateStipePayment Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }

        return redirectPGUrl;
    }

}