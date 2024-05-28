using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using GiiftShopGateway.Model;
using KhaltiInsurance.Entities;
using KhaltiISP.Entities;
using Newtonsoft.Json;
using ABC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InternetServiceProviderListDetails : System.Web.UI.Page
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
                    string CallbackUrl = HttpUtility.UrlEncode(Encrypt("InternetServiceProviderListDetails.aspx?code=" + lstrServiceCode));
                    HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                    Response.Redirect("Login.aspx?CallbackUrl=" + CallbackUrl, false);
                }
                else
                {
                    ABCModel lobjModel = new ABCModel();
                    Session["PageName"] = this.Title;
                    lobjModel.LogActivity(string.Format("Visit InternetServiceProviderListDetails.aspx; ServiceCode-:{0};", lstrServiceCode), ActivityType.PageLoad);
                    StringBuilder lstrHtmlContent = new StringBuilder();
                    if (!Page.IsPostBack)
                    {
                        //lstrHtmlContent.Append("<ul class=\"d-flex\">" +
                        //    "<li><a href=\"\\\"><img class=\"pr-3\" src=\"../images/arrow-left.svg\"></a></</li>" +
                        //    "<li><a class=\"custom-text\"  href =\"\\\">Home</a></li>" +
                        //    "<li class=\"px-2\">/</li>" +
                        //    "<li><a href =\"InternetServiceProviders.aspx \">" + "Internet Service Providers" + "</a></li>" +
                        //    "<li class=\"px-2\">/</li>" +
                        //    "<li class=\"brd-bold\">Internet Service Provider Details</li>" +
                        //    "</ul>");
                        btnBack.HRef = "InternetServiceProviders.aspx";
                        InsuranceServiceProvidersResponse result = HttpContext.Current.Application["SearchISPProducts"] as InsuranceServiceProvidersResponse;
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
            LoggingAdapter.WriteLog("InternetServiceProviderListDetails.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
    }

    [WebMethod]
    public static string BindISPProductDetails(int pstrServiceCode)
    {
        string lstrResponse = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel shopModel = new ShopModel();
            HttpContext.Current.Session["ISPServiceCode"] = pstrServiceCode;
            InsuranceRequiredDetailsResponse lobjProduct = new InsuranceRequiredDetailsResponse();
            string PageName = string.Empty;
            PageName = HttpContext.Current.Session["PageName"].ToString();
            lobjProduct = lobjModel.GetRequiredDetails(pstrServiceCode, PageName);
            if (lobjProduct != null)
            {
                lobjModel.LogActivity(string.Format("BindISPProductDetails; ServiceCode-:{0};", pstrServiceCode), ActivityType.InternetServiceProvider);
                if (lobjProduct != null)
                {
                    lstrResponse = JsonConvert.SerializeObject(lobjProduct.results);
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InternetServiceProviderListDetails.aspx BindISPProductDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrResponse;
    }

    [WebMethod]
    public static string FetchUserDetails(int pstrServiceCode, string UserId)
    {
        string lstrResponse = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel shopModel = new ShopModel();
            ISPUserDetailsResponse lobjUserdetails = new ISPUserDetailsResponse();

            string ServiceName = string.Empty;
            InsuranceServiceProvidersResponse lobjISPresponse = HttpContext.Current.Application["SearchISPProducts"] as InsuranceServiceProvidersResponse;
            //ServiceName=lobjISPresponse.results.AsEnumerable().Where(c => Convert.ToInt32(c.ServiceCode) == pstrServiceCode).Select(c => Convert.ToString(c.ServiceName)).ToString();
            dynamic dyndata = lobjISPresponse.results.Find(x=> Convert.ToInt32(x.ServiceCode)== pstrServiceCode);
            string MembershipReference = string.Empty;
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            if(lobjMemberDetails != null)
            {
                MembershipReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                string lstrCurrency = lobjModel.GetDefaultCurrency();
                float Pointrate = lobjModel.GetProgramRedemptionRate(lstrCurrency, "ISP", lobjProgramDefinition.ProgramId);
                lobjUserdetails = lobjModel.GetISPUserDetails(pstrServiceCode, UserId, dyndata.ServiceName, MembershipReference);
                HttpContext.Current.Session["ISPUserName"] = UserId;
                HttpContext.Current.Session["ISPUserDetails"] = lobjUserdetails;
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
            LoggingAdapter.WriteLog("InternetServiceProviderListDetails.aspx FetchUserDetails Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
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
            //               , lstrCurrency, lobjProgramDefinition.ProgramId, "ISP");
            if ((pntamount) > MemberMiles)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("InternetServiceProviderListDetails.aspx CheckAvailability Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return false;
        }
        return true;
    }

    [WebMethod]
    public static string CheckoutGenerateOTP(decimal pntamount , string PackageId,string DurationCode)
    {
        string lstrResponse = string.Empty;
        ABCModel lobjModel = new ABCModel();
        try
        {
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {

                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                ISPUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["ISPUserDetails"] as ISPUserDetailsResponse;
                // ISPUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["ISPUserDetails"] as ISPUserDetailsResponse;
                if (HttpContext.Current.Session["ISPUserDetails"] != null)
                {
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                    //int lintPoints = lobjModel.ConvertToPoints(float.Parse(pntamount.ToString())
                    //  , lstrCurrency, lobjProgramDefinition.ProgramId, "ISP");
                    MemberRelation lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                    HttpContext.Current.Session["BookingFlag"] = "InternetServiceProviders";
                    List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                    lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                    int ThreshouldValue = lobjRedemptionKeys == null ? -1 : lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.INSURANCE.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                    Packages PackageData= new Packages();
                    Details PackageDetailsData=new Details();
                    if (lobjUserdetails.results.Packages.Count > 0)
                    {
                        PackageData = lobjUserdetails.results.Packages.Find(x => x.Id == Convert.ToInt32(PackageId));
                        if (PackageData.Details != null)
                        {
                            PackageDetailsData = PackageData.Details.Find(x => x.DurationCode == DurationCode);
                        }
                    }
                    HttpContext.Current.Session["ISPSelectedPackageData"] = PackageData;
                    HttpContext.Current.Session["ISPSelectedPackageDetailsData"] = PackageDetailsData;
                    HttpContext.Current.Session["FinalAmountPayable"] = pntamount;
                    lstrResponse = "/PaymentOptions.aspx";
                    if (ThreshouldValue <= pntamount && !ThreshouldValue.Equals(-1) && HttpContext.Current.Session["RelationshipManager"] == null)
                    {
                        bool Status = false;
                        OTPDetails lobjOTPDetails = new OTPDetails
                        {
                            UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference,
                            OtpEnumTypes = OTPEnumTypes.ISPREVIEWNCONFIRM,
                            OtpType = Convert.ToString(OTPEnumTypes.ISPREVIEWNCONFIRM),
                            AdditionalDetails = lobjModel.FloatToThousandSeperated((float)lobjModel.CalculateAmount(Convert.ToInt32(pntamount),
                            Convert.ToDouble(lobjModel.GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId).FindAll(x => x.Currency.ToLower() == lstrCurrency.ToLower())[0].RedemptionRate)))
                        };
                        HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                        //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails);
                        //Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Internet Service Provider");
                        //if (Status)
                        //{
                        //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ISP", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                        //    lstrResponse = "/ValidateOTP.aspx?flag=ISP";
                        //}
                        //else
                        //{
                        //    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "ISP", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                        //    lstrResponse = "/OrderStatus.aspx?Status=false";
                        //}
                    }
                    //else
                    //{
                    //    lstrResponse = "/PointGateway.aspx?flag=ISP";
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
            LoggingAdapter.WriteLog("InternetServiceProviderListDetails.aspx CheckoutGenerateOTP Ex-" + ex.InnerException + Environment.NewLine + ex.StackTrace + Environment.NewLine + ex.Message);
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