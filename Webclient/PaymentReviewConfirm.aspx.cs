
using CB.IBE.DomesticFlight.Entities;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Hotels.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Platform.Booking.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.Transactions.Entites;
using Core.WebAPI.ClientHelper;
using Framework.EnterpriseLibrary.Adapters;
using Framework.Integrations.Hotels.Entities;
using Giift.ShopGateway.Client.Entities;
using GiiftPaymentGateway.Entities;
using GiiftShopGateway.Model;
using Holibob.Entities;
using KhaltiInsurance.Entities;
using KhaltiISP.Entities;
using Newtonsoft.Json;
using ABC.Model;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentReviewConfirm : System.Web.UI.Page
{
    List<object> lobject = new List<object>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["MemberDetails"] == null)
            {
                string CallbackUrl = HttpUtility.UrlEncode(Encrypt("PaymentReviewConfirm.aspx"));
                HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
            }
        }
    }

    [WebMethod]
    public static string[] BindPaymentDetails()
    {
        ABCModel lobjPaymentModel = new ABCModel();
        string[] lstrresponse = new string[4];
        string lstrProductAmount = string.Empty;
        string lstrSelectedAbsherpoints = string.Empty;
        string hndSelectedAbsherpoints = string.Empty;
        string lstrRemainingAmount = string.Empty;
        string lstrProductPoint = string.Empty;
        try
        {

            ProgramDefinition lobjProgramDefinition = lobjPaymentModel.GetProgramMaster();
            List<ProgramCurrencyDefinition> lobjProgramCurrencyDefinition = lobjPaymentModel.GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
            var PointRate = lobjProgramCurrencyDefinition[0].RedemptionRate;
            StripePaymentDetails lobjStripePaymentDetails = null;

            string lstrBookingFlag = string.Empty;
            if (HttpContext.Current.Session["BookingFlag"] != null)
            {
                lstrBookingFlag = HttpContext.Current.Session["BookingFlag"].ToString();
            }

            if (HttpContext.Current.Session["StripePaymentDetails"] != null)
            {
                lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];

                lstrProductPoint = lobjPaymentModel.StringToThousandSeperated(lobjStripePaymentDetails.TotalProductAmount);
                lstrProductAmount = Convert.ToString(lobjPaymentModel.CalculateAmount(Convert.ToInt32(lobjStripePaymentDetails.TotalProductAmount), PointRate));
                lstrSelectedAbsherpoints = lobjPaymentModel.StringToThousandSeperated(lobjStripePaymentDetails.ReqRedeemPoint);
                hndSelectedAbsherpoints = lobjStripePaymentDetails.ReqRedeemPoint;

                int RemainingPoint = Convert.ToInt32(lobjStripePaymentDetails.TotalProductAmount) - Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint);

                double selectedPointAmount = lobjPaymentModel.CalculateAmount(Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), PointRate);

                float RemainingAmount = (float)lobjPaymentModel.CalculateAmount(Convert.ToInt32(RemainingPoint), PointRate);

                //string RemainingAmount = (Convert.ToInt32(lstrProductAmount) - Convert.ToInt32(selectedPointAmount)).ToString();

                //lstrRemainingAmount = lobjPaymentModel.FloatToThousandSeperated(RemainingAmount);
                lstrRemainingAmount = lobjPaymentModel.StringToThousandSeperated(RemainingAmount.ToString());
                lobjStripePaymentDetails.ReqRedeemAmount = RemainingAmount.ToString();
                lobjStripePaymentDetails.ReqRedeemPointAmount = selectedPointAmount.ToString();
                HttpContext.Current.Session["StripePaymentDetails"] = lobjStripePaymentDetails;
            }

            lstrresponse[0] = lstrProductPoint;//ProductAmount
            lstrresponse[1] = lstrSelectedAbsherpoints;//Selectedpoints
            lstrresponse[2] = hndSelectedAbsherpoints;//hndSelectedpoints
            lstrresponse[3] = lstrRemainingAmount;//RemainingAmount

            lobjPaymentModel.LogActivity(string.Format("PaymentReviewConfirm; BindPaymentDetails; TotalProductAmount-:{0} USD; SelectedAbsherpoints-{1}; RemainingAmount-:{2} QAR;", lstrProductAmount, lstrSelectedAbsherpoints, lstrRemainingAmount), ActivityType.Payment);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PaymentReviewConfirm BindPaymentDetails Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
        }

        return lstrresponse;
    }

    protected void btnStripePayment_Click(object sender, EventArgs e)
    {
        try
        {
            ABCModel lobjPaymentModel = new ABCModel();
            MemberDetails lobjMemberDetails = null;
            StripePaymentDetails lobjStripePaymentDetails = null;
            string lstrBookingFlag = string.Empty;
            string strRedeemMilesResponse = string.Empty;
     
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {
                lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (HttpContext.Current.Session["StripePaymentDetails"] != null)
                {
                    lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];

                    if (HttpContext.Current.Session["BookingFlag"] != null)
                    {
                        lstrBookingFlag = HttpContext.Current.Session["BookingFlag"].ToString();
                    }
                    bool Status = false;
                    OTPDetails lobjOTPDetails = HttpContext.Current.Session["OtpDetails"] as OTPDetails;
                    LoggingAdapter.WriteLog("PaymentReviewConfirm PaymentDetails Redeem Points -:" + lobjStripePaymentDetails.ReqRedeemPoint);
                    if (lstrBookingFlag.ToLower().Equals("digitalproduct"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            Giift.ShopGateway.Client.Entities.Product lobjProductDetails = new Giift.ShopGateway.Client.Entities.Product();
                            lobjProductDetails = HttpContext.Current.Session["Product"] as Giift.ShopGateway.Client.Entities.Product;
                            if (lobjProductDetails != null)
                            {
                                Status = lobjPaymentModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, lobjProductDetails.Properties.ToList().Find(x => x.Name.Equals("Type")).Value);
                                if (Status)
                                {
                                    lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ShopDigital", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                    Response.Redirect("/ValidateOTP.aspx?flag=ShopDigital&redemptiontype=" + lobjProductDetails.Properties.ToList().Find(x => x.Name.Equals("Type")).Value, false);

                                }
                                else
                                {
                                    lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ShopDigital", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                    Response.Redirect("/OrderStatus.aspx?Status=false", false);
                                }
                            }
                            else
                            {
                                Response.Redirect("/OrderStatus.aspx?Status=false", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("/OrderStatus.aspx?Status=false", false);
                        }
                    }

                    if (lstrBookingFlag.ToLower().Equals("physicalproduct"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            Status = lobjPaymentModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Shop");
                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "SHOP", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("/ValidateOTP.aspx?flag=Shop", false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "SHOP", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("/OrderStatus.aspx?Status=false", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("/OrderStatus.aspx?Status=false", false);
                        }
                    }

                    if (lstrBookingFlag.ToLower().Equals("flight"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;
                            RedemptionDetails lobjRedemptionDetails = lobjListOfRedemptionDetails[0];
                            Status = lobjPaymentModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "International Flight");

                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "AIR", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("ValidateOTP.aspx?flag=Air", false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "AIR", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("BookingFailure.aspx", false);

                            }
                        }
                        else
                        {
                            Response.Redirect("BookingFailure.aspx", false);
                        }
                    }

                    if (lstrBookingFlag.ToLower().Equals("hotel"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;
                            RedemptionDetails lobjRedemptionDetails = lobjListOfRedemptionDetails[0];
                            Status = lobjPaymentModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Hotel");
                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "HOTEL", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("ValidateOTP.aspx?flag=Hotel",false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "HOTEL", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("BookingFailure.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("BookingFailure.aspx", false);
                        }
                    }

                    if (lstrBookingFlag.ToLower().Equals("experience"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            Status = lobjPaymentModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails, "Experience");
                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "EXPERIENCE", lobjMemberDetails.MemberRelationsList[0].RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("ValidateOTP.aspx?flag=Package", false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "EXPERIENCE", lobjMemberDetails.MemberRelationsList[0].RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("BookingFailure.aspx", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("BookingFailure.aspx", false);
                        }
                    }

                    if (lstrBookingFlag.ToLower().Equals("insuranceserviceproviders"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            Status = lobjPaymentModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Insurance");
                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Insurance", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("/ValidateOTP.aspx?flag=Insurance",false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Insurance", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("/OrderStatus.aspx?Status=false", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("/OrderStatus.aspx?Status=false", false);
                        }
                    }

                    if (lstrBookingFlag.ToLower().Equals("internetserviceproviders"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            Status = lobjPaymentModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Internet Service Provider");
                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ISP", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("/ValidateOTP.aspx?flag=ISP",false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ISP", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("/OrderStatus.aspx?Status=false", false);
                            }
                        }
                        else
                        {
                            Response.Redirect("/OrderStatus.aspx?Status=false", false);
                        }
                    }
                    if (lstrBookingFlag.ToLower().Equals("domesticflight"))
                    {
                        if (lobjOTPDetails != null)
                        {
                            List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;
                            RedemptionDetails lobjRedemptionDetails = lobjListOfRedemptionDetails[0];
                            Status = lobjPaymentModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails, "Domestic Flight");
                            if (Status)
                            {
                                lobjPaymentModel.LogActivity(string.Format("KHALTI AIR ReviewConfirm OTP Request For Member {0} Status- {1}", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                Response.Redirect("ValidateOTP.aspx?flag=KhaltiAir", false);
                            }
                            else
                            {
                                lobjPaymentModel.LogActivity(string.Format("KHALTI AIR ReviewConfirm OTP Request For Member {0} Status- {1}", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                Response.Redirect("BookingFailure.aspx", false);

                            }
                        }
                        else
                        {
                            Response.Redirect("BookingFailure.aspx", false);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PaymentReviewConfirm btnStripePayment_Click Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
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
