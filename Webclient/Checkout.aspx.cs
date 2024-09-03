using Core.Platform.Member.Entites;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Services;
using Framework.EnterpriseLibrary.Adapters;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using ABC.Model;
using System.Web.UI;
using System.Security.Cryptography;
using System.IO;

public partial class Checkout : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["MemberDetails"] == null)
            {
                string CallbackUrl = HttpUtility.UrlEncode(Encrypt(Request.RawUrl));
                HttpContext.Current.Session["CallbackUrl"] = CallbackUrl;
                Response.Redirect("Index.aspx?CallbackUrl=" + CallbackUrl, false);
            }
            else
            {
                ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                if (Cart != null && Cart.Items.Count > 0)
                {
                    if (Cart.Items.FindAll(lobj => lobj.ProductType == null || lobj.ProductType == "Physical").Count > 0)
                    {
                        divCheckoutPhysical.Visible = true;
                        divCheckoutDigital.Visible = false;
                    }
                    else
                    {
                        divCheckoutPhysical.Visible = false;
                        divCheckoutDigital.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Checkout.aspx Page_Load Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
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
            LoggingAdapter.WriteLog("Cart.aspx Encrypt Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return clearText;
    }
    [WebMethod]
    public static string LoadCart()
    {
        string lstrHtmlResponse = string.Empty;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
            string HeaderTemplate = "<div class=\"row\"><div class=\"col-12 dvYourCart\"><div class=\"bg-colour1 d-flex justify-content-between align-items-center\"><h2 class=\"h6 heading-bold text-colour6 p-3\" data-i18n=\"shopcheckout-yourcart\">Your Cart</h2><span class=\"badge badge-1 mr-3\">{0}</span></div></div> <div class=\"col-12 dvCartDetails mb-3\"><div class=\"bg-colour2 px-3\"><div class=\"row\">";
            string BodyTemplate = "<div class=\"col-12 my-2\"><div class=\"row align-items-center mt-md-1\"><div class=\"col-5\"><h6 class=\"my-0 text-mute text-colour7 h7\">{0}</h6> <small style=\"display:none;\" class=\"text-mute\">{1}</small> </div><span class=\"col-3 text-center text-colour7 h7\">Qty: {3}</span> <span class=\"col-4 text-right text-colour7 h7\">{2}</span></div> </div>";
            string TotalTemplate = "<div class=\"col-12 my-2\"><div class=\"border-top py-2 d-flex justify-content-between\"><span class=\"h6 heading-bold text-colour7\" data-i18n=\"shopcheckout-total\">Total</span><strong class=\"h6 heading-bold text-colour7\">{0}</strong></div> </div>";
            StringBuilder sb = new StringBuilder();
            if (Cart != null && Cart.Items.Count > 0)
            {
                sb.Append(string.Format(HeaderTemplate, Cart.ItemsQuantity));
                foreach (var item in Cart.Items)
                {
                    sb.Append(string.Format(BodyTemplate, item.Name, item.Name, lobjModel.FormatPoints(Math.Ceiling(item.Price.SalePrice.Amount), "Points"), item.Quantity));
                }
                sb.Append(string.Format(TotalTemplate, lobjModel.FormatPoints(Math.Ceiling(Cart.Price.Total.Amount), "Points")));
            }
            lstrHtmlResponse = sb.ToString();
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Checkout.aspx LoadCart Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrHtmlResponse;
    }

    [WebMethod]
    public static string Proceed(string firstName, string lastName, string email, string phone, string address, string address2, string city, string country, string countrycode, string zip)
    {
        string lstrStatus = string.Empty;
        try
        {
            ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            try
            {
                HttpContext.Current.Session["BookingFlag"] = "physicalproduct";

                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
                lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
                string lstrCurrency = lobjModel.GetDefaultCurrency();
                MemberRelation lobjMemberRelation = new MemberRelation();
                lobjMemberRelation = lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS));
                Address deliveryaddress = new Address
                {
                    Type = AddressType.BillingAndShipping,
                    City = city,
                    CountryCode = countrycode,
                    CountryName = country,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Line1 = address,
                    Line2 = address2,
                    Phone = phone,
                    Zip = zip,
                    PostalCode = zip
                };
                int lintPoints = Convert.ToInt32(Cart.Price.Total.Amount);
                int pintAvailablePoints = lobjModel.CheckAvailbility(lobjMemberRelation.RelationReference, Convert.ToInt32(lobjMemberRelation.RelationType), lstrCurrency, lobjProgramDefinition.ProgramId);
                HttpContext.Current.Session["CheckoutAddress"] = deliveryaddress;
                int ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.GIFTCARD.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
                if ((pintAvailablePoints - lintPoints) >= 0)
                {
                    if (ThreshouldValue <= lintPoints && !ThreshouldValue.Equals(-1) && HttpContext.Current.Session["RelationshipManager"] == null)
                    {
                        bool Status = false;
                        OTPDetails lobjOTPDetails = new OTPDetails
                        {
                            UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference,
                            OtpEnumTypes = OTPEnumTypes.SHOPREVIEWNCONFIRM,
                            OtpType = Convert.ToString(OTPEnumTypes.SHOPREVIEWNCONFIRM),
                            AdditionalDetails = lobjModel.FloatToThousandSeperated((float)lobjModel.CalculateAmount(lintPoints,
                                        Convert.ToDouble(lobjModel.GetAllCurrencyDefinition(lobjProgramDefinition.ProgramId).FindAll(x => x.Currency.ToLower() == lstrCurrency.ToLower())[0].RedemptionRate)))
                        };
                        HttpContext.Current.Session["OtpDetails"] = lobjOTPDetails as OTPDetails;
                        //Status = lobjModel.GenerateReviewnConfirmOTP(lobjOTPDetails, lobjMemberDetails);
                        Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Shop");
                        if (Status)
                        {
                            lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "SHOP", lobjOTPDetails.UniquerefID, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                            lstrStatus = "/ValidateOTP.aspx?flag=Shop";
                        }
                        else
                        {
                            lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "SHOP", lobjOTPDetails.UniquerefID, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                            lstrStatus = "/OrderStatus.aspx?Status=false";
                        }
                    }
                    else
                    {
                        lobjModel.LogActivity(string.Format("Topup Card {0}: Requested", lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference), ActivityType.Voucher);
                        lstrStatus = "/PointGateway.aspx?flag=Shop";
                    }

                    // lstrStatus = "/PaymentOptions.aspx";
                }
                else
                {
                    lstrStatus = "INSUFFICIENT_POINTS";
                }
               
            }
            catch (Exception ex)
            {
                LoggingAdapter.WriteLog("CheckoutShop ex - " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                lstrStatus = "/OrderStatus.aspx?Status=false";
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("Checkout.aspx Proceed Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lstrStatus;
    }
}