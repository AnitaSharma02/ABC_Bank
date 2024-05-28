using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.Integrations.Hotels.Entities;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using KhaltiInsurance.Entities;
using Newtonsoft.Json;
using ABC.Model;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InsuranceListDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int lstrServiceCode = 0;

            if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["code"])))
            {
                lstrServiceCode = Convert.ToInt32(Request.QueryString["code"]);
                if (Session["MemberDetails"] == null)
                {
                    string CallbackUrl = HttpUtility.UrlEncode(Encrypt("InsuranceListDetails.aspx?code=" + lstrServiceCode));
                    HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                    Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
                }
                else
                {
                    ABCModel lobjModel = new ABCModel();
                    Session["PageName"] = this.Title;
                    lobjModel.LogActivity(string.Format("Visit InsuranceListDetails.aspx; ServiceCode-:{0};", lstrServiceCode), ActivityType.PageLoad);
                    StringBuilder lstrHtmlContent = new StringBuilder();
                    if (!Page.IsPostBack)
                    {
                        //lstrHtmlContent.Append("<ul class=\"d-flex\">" +
                        //    "<li><a href=\"\\\"><img class=\"pr-3\" src=\"../images/arrow-left.svg\"></a></</li>" +
                        //    "<li><a class=\"custom-text\"  href =\"\\\">Home</a></li>" +
                        //    "<li class=\"px-2\">/</li>" +
                        //    "<li><a href =\"InsuranceList.aspx \">" + "Insurance" + "</a></li>" +
                        //    "<li class=\"px-2\">/</li>" +
                        //    "<li class=\"brd-bold\">Insurance Details</li>" +
                        //    "</ul>");
                        btnBack.HRef = "InsuranceList.aspx";
                        InsuranceServiceProvidersResponse result = HttpContext.Current.Application["SearchInsuranceProducts"] as InsuranceServiceProvidersResponse;
                        var imageurl = result.results.Find(x => x.ServiceCode == lstrServiceCode.ToString()).ImageUrl;
                        imgProductImageMain.Src = imageurl;
                        divThumbnailServiceName.InnerHtml = result.results.Find(x => x.ServiceCode == lstrServiceCode.ToString()).ServiceName;
                        MemberDetails lobjMemberDetails = new MemberDetails();
                        lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                        if (string.IsNullOrEmpty(lobjMemberDetails.Email))
                        {
                            divEmailErrorMsg.Visible = true;
                            fetchUserdetails.Visible = false;
                        }
                        else
                        {
                            divEmailErrorMsg.Visible = false;
                            fetchUserdetails.Visible = true;
                        }
                    }
                    //divBreadbrums.InnerHtml = lstrHtmlContent.ToString();
                }
            }
            else
            {
                if (Session["MemberDetails"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Response.Redirect("Index.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceListDetails.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [WebMethod]
    public static string BindInsuranceProductDetails(int pstrServiceCode)
    {
        string lstrResponse = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel shopModel = new ShopModel();
            HttpContext.Current.Session["InsuranceServiceCode"] = pstrServiceCode;
            InsuranceRequiredDetailsResponse lobjProduct = new InsuranceRequiredDetailsResponse();
            string PageName = string.Empty;
            PageName = HttpContext.Current.Session["PageName"].ToString();
            lobjProduct = lobjModel.GetRequiredDetails(pstrServiceCode, PageName);
            if (lobjProduct != null)
            {
                lobjModel.LogActivity(string.Format("BindInsuranceProductDetails; ServiceCode-:{0};", pstrServiceCode), ActivityType.Insurance);
                lstrResponse = JsonConvert.SerializeObject(lobjProduct.results);
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceListDetails.aspx BindInsuranceProductDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrResponse;
    }


    [WebMethod]
    public static string FetchUserDetails(int pstrServiceCode, string PolicyNo, string DOB)
    {
        string lstrResponse = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel shopModel = new ShopModel();
            InsuranceUserDetailsResponse lobjUserdetails = new InsuranceUserDetailsResponse();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            string MembershipReference = string.Empty;
            if (lobjMemberDetails != null)
            {
                MembershipReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                string lstrCurrency = lobjModel.GetDefaultCurrency();
                float Pointrate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.INSURANCE.ToString(), lobjProgramDefinition.ProgramId);
                lobjUserdetails = lobjModel.GetUserDetails(pstrServiceCode, PolicyNo, DOB, MembershipReference);
                HttpContext.Current.Session["InsurancePolicyNo"] = PolicyNo;
                HttpContext.Current.Session["InsuranceUserDetails"] = lobjUserdetails;
                if (lobjUserdetails != null)
                {
                    if (string.IsNullOrEmpty(lobjUserdetails.results.ErrorCode))
                    {
                        lobjUserdetails.results.PointRate = Pointrate;
                        lstrResponse = JsonConvert.SerializeObject(lobjUserdetails.results);
                    }
                    else
                    {
                        lstrResponse = lobjUserdetails.results.Message;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceListDetails.aspx FetchUserDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrResponse;
    }

    [WebMethod]
    public static bool CheckAvailability(decimal pntamount)
    {
        try
        {
            ABCModel lobjmodel = new ABCModel();
            ShopModel lmodel = new ShopModel();
            string lstrCurrency = lobjmodel.GetDefaultCurrency();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
            ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
            int MemberMiles = lobjmodel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(lobjMemberRelation.RelationType), lstrCurrency, lobjProgramDefinition.ProgramId);

            //int lintTotalPrice = lobjmodel.ConvertToPoints(float.Parse(pntamount.ToString())
            //               , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
            if ((pntamount) > MemberMiles)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceListDetails.aspx CheckAvailability Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
        return true;
    }

    [WebMethod]
    public static string CheckoutGenerateOTP()
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        try
        {
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {

                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                InsuranceUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["InsuranceUserDetails"] as InsuranceUserDetailsResponse;
                if (HttpContext.Current.Session["InsuranceUserDetails"] != null)
                {
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    int lintPoints = lobjModel.ConvertToPoints(float.Parse(lobjUserdetails.results.Amount.ToString())
                      , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
                    MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                    HttpContext.Current.Session["BookingFlag"] = "InsuranceServiceProviders";
                    List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                    lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                    int ThreshouldValue = lobjRedemptionKeys == null ? -1 : lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.INSURANCE.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                    lstrResponse = "/PaymentOptions.aspx";
                    if (ThreshouldValue <= lintPoints && !ThreshouldValue.Equals(-1) && HttpContext.Current.Session["RelationshipManager"] == null)
                    {
                        bool Status = false;
                        OTPDetails lobjOTPDetails = new OTPDetails
                        {
                            UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference,
                            OtpEnumTypes = OTPEnumTypes.INSURANCEREVIEWNCONFIRM,
                            OtpType = Convert.ToString(OTPEnumTypes.INSURANCEREVIEWNCONFIRM),
                            AdditionalDetails = lobjModel.FloatToThousandSeperated((float)lobjModel.CalculateAmount(lintPoints,
                            Convert.ToDouble(lobjModel.GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId).FindAll(x => x.Currency.ToLower() == lstrCurrency.ToLower())[0].RedemptionRate)))
                        };
                        HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                        //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails);
                        //Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Insurance");
                        //if (Status)
                        //{
                        //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Insurance", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                        //    lstrResponse = "/ValidateOTP.aspx?flag=Insurance";
                        //}
                        //else
                        //{
                        //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "Insurance", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                        //    lstrResponse = "/OrderStatus.aspx?Status=false";
                        //}
                    }
                    //else
                    //{
                    //    lstrResponse = "/PointGateway.aspx?flag=Insurance";
                    //}
                }
            }
            else
            {
                lstrResponse = "SESSION_TIME_OUT";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InsuranceListDetails.aspx CheckoutGenerateOTP Ex-" + ex.InnerException + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Message);
        }
        return lstrResponse;
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
