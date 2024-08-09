using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Holibob.Entities;
using ABC.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using Core.Platform.OTP.Entities;

public partial class ExperienceProductBooking : Page
{
    public int pintAvailablePoints { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string lstrBookId = Convert.ToString(Request.QueryString["Id"]);
            if (!string.IsNullOrEmpty(lstrBookId))
            {
                if (!Page.IsPostBack)
                {
                    ABCModel lobjModel = new ABCModel();
                    MemberDetails lobjMemberDetails = Session["MemberDetails"] as MemberDetails;
                    if (lobjMemberDetails == null)
                    {
                        string CallbackUrl = string.Format("{0}?Id={1}", "ExperienceProductBooking.aspx", lstrBookId);
                        HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                        Response.Redirect("Login.aspx?CallbackUrl=" + HttpUtility.UrlEncode(Encrypt(CallbackUrl)), false);
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format(" Visit Experience Product Booking; lstrBookId: {0}", lstrBookId), ActivityType.PageLoad);
                    }
                }
            }
            else
            {
                Response.Redirect("ExperienceProductBooking.aspx", false);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking.aspx Pageload Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }
    [WebMethod]
    public static string GetOrderStatus(string pstrBookId)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null && !string.IsNullOrEmpty(lobjMemberDetails.FullName))
            {
                lNICogRequestResponse.Append(string.Format("GetOrderStatus Request: pstrBookId - {0}", pstrBookId));
                OrderStatusResponse lobjOrderStatusResponse = lobjModel.GetOrderStatus(pstrBookId, string.Empty);
                lNICogRequestResponse.Append(string.Format(" GetOrderStatus Response: {0}", JsonConvert.SerializeObject(lobjOrderStatusResponse)));
                if (lobjOrderStatusResponse != null
                    && lobjOrderStatusResponse.data != null
                    && !string.IsNullOrEmpty(lobjOrderStatusResponse.data.getOrderStatus))
                {
                    HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lobjOrderStatusResponse.data.getOrderStatus);
                    if (lobjOrderStatus != null && lobjOrderStatus.data != null && lobjOrderStatus.status.ToLower() == "success")
                    {
                        lobjModel.LogActivity(string.Format("GetOrderStatus; pstrBookId:{0}; Status:{1};", pstrBookId, "Success"), ActivityType.PackageBooking);

                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        string lstrProgramName = ProgramHelper.ProgramName();
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                        if (lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes != null)
                        {
                            foreach (var item in lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes)
                            {
                                string pstrGrossFormattedText = string.Empty;
                                item.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(item.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                                item.totalPrice.grossFormattedText = pstrGrossFormattedText;
                            }
                            lobjOrderStatusResponse.data.getOrderStatus = JsonConvert.SerializeObject(lobjOrderStatus);
                        }
                        lstrResponse = JsonConvert.SerializeObject(lobjOrderStatus.data);
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format("GetOrderStatus; pstrBookId:{0}; Status:{1};", pstrBookId, "Failed"), ActivityType.PackageBooking);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking GetOrderStatus Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking GetOrderStatus RequestResponse: " + lNICogRequestResponse);
            //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    [WebMethod]
    public static string SubmitBookingAnswer(string pstrBookId, List<ExperienceBookingAnswerList> plstobjAnswerList)
    {
        string lstrResponse = string.Empty;
        StringBuilder lNICogRequestResponse = new StringBuilder();
        ABCModel lobjModel = new ABCModel();
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if (lobjMemberDetails != null && !string.IsNullOrEmpty(lobjMemberDetails.FullName))
            {
                lNICogRequestResponse.Append(string.Format("SubmitBookingAnswer Request: pstrBookId - {0}, plstobjAnswerList - {1}", pstrBookId, JsonConvert.SerializeObject(plstobjAnswerList)));
                OrderStatusResponse lobjOrderStatusResponse = lobjModel.SubmitBookingAnswer(pstrBookId, plstobjAnswerList);
                lNICogRequestResponse.Append(string.Format(" SubmitBookingAnswer Response: {0}", JsonConvert.SerializeObject(lobjOrderStatusResponse)));
                if (lobjOrderStatusResponse != null
                    && lobjOrderStatusResponse.data != null
                    && !string.IsNullOrEmpty(lobjOrderStatusResponse.data.getOrderStatus))
                {
                    HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lobjOrderStatusResponse.data.getOrderStatus);
                    if (lobjOrderStatus != null && lobjOrderStatus.data != null && lobjOrderStatus.status.ToLower() == "success")
                    {
                        lobjModel.LogActivity(string.Format("SubmitBookingAnswer; pstrBookId:{0}; Status:{1};", pstrBookId, "Success"), ActivityType.PackageBooking);
                        string lstrCurrency = lobjModel.GetDefaultCurrency();
                        string lstrProgramName = ProgramHelper.ProgramName();
                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                        int lintTotalPoints = 0;
                        if (lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes != null)
                        {

                            foreach (var item in lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes)
                            {
                                string pstrGrossFormattedText = string.Empty;
                                item.totalPrice.gross = lobjModel.ConvertToPointsOrCurrency(item.totalPrice.gross, lstrCurrency, lobjProgramDefinition.ProgramId, out pstrGrossFormattedText);
                                item.totalPrice.grossFormattedText = pstrGrossFormattedText;
                                lintTotalPoints = (int)item.totalPrice.gross;
                            }
                            lobjOrderStatusResponse.data.getOrderStatus = JsonConvert.SerializeObject(lobjOrderStatus);

                        }

                        HttpContext.Current.Session["ExperienceBookingDetails"] = lobjOrderStatusResponse;
                        HttpContext.Current.Session["BookingFlag"] = "experience";
                       

                        List<ProgramCurrencyDefinition> lobjlistProgramCurrencyDefinition = lobjModel.GetProductProgramCurrencyDefinition(lobjMemberDetails);
                        int lintAvailablePoints = lobjModel.CheckAvailbility(lobjMemberDetails.MemberRelationsList[0].RelationReference, Convert.ToInt32(RelationType.LBMS), lobjlistProgramCurrencyDefinition[0].Currency, lobjProgramDefinition.ProgramId);
                        if (lintAvailablePoints >= lintTotalPoints)
                        {
                            lstrResponse = "PaymentOptions.aspx";
                            HttpContext.Current.Session["ExperienceBookingDetails"] = lobjOrderStatusResponse;
                            List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                            lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                            int ThreshouldValue = 0;
                            ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals("EXPERIENCE") && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                            if (ThreshouldValue <= lintTotalPoints && !ThreshouldValue.Equals(-1))
                            {
                                bool Status = false;
                                OTPDetails lobjOTPDetails = new OTPDetails
                                {
                                    UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference,
                                    OtpEnumTypes = OTPEnumTypes.PACKAGEREVIEWNCONFIRM,
                                    OtpType = Convert.ToString(OTPEnumTypes.PACKAGEREVIEWNCONFIRM)
                                };
                                HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                                //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails, "Experience");
                                //if (Status)
                                //{
                                //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "EXPERIENCE", lobjMemberDetails.MemberRelationsList[0].RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                                //    lstrResponse = string.Format("ValidateOTP.aspx?flag=Package", false);
                                //}
                                //else
                                //{
                                //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "EXPERIENCE", lobjMemberDetails.MemberRelationsList[0].RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                                //    lstrResponse = string.Format("BookingFailure.aspx", false);
                                //}
                            }
                            //else
                            //{
                            //    lstrResponse = string.Format("PointGateway.aspx?flag=Package");
                            //}
                        }
                        else
                        {
                            lstrResponse = "Insufficient Points.";
                        }

                    }
                    else if (lobjOrderStatus != null && lobjOrderStatus.data != null && lobjOrderStatus.status.ToLower() == "rejected")
                    {
                        lobjModel.LogActivity(string.Format("SubmitBookingAnswer; pstrBookId:{0}; Status:{1};", pstrBookId, "Failed"), ActivityType.PackageBooking);
                        LoggingAdapter.WriteLog("ExperienceProductBooking SubmitBookingAnswer Response error-: " + lobjOrderStatus.errors);
                        lstrResponse = lobjOrderStatus.errors;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking SubmitBookingAnswer Exception: " + ex.Message + Environment.NewLine + ex.StackTrace);
        }
        finally
        {
            LoggingAdapter.WriteLog("ExperienceProductBooking SubmitBookingAnswer RequestResponse: " + lNICogRequestResponse);
            //lobjModel.LogActivity(string.Format("ActivityType: {0} - RequestResponse: {1}", ActivityConstants.BookPackage, lNICogRequestResponse), ActivityType.PackageBooking);
        }
        return lstrResponse;
    }
    private static string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPSOBHA99212";
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
        return clearText;
    }
}