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
using Holibob.Entities;
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
            //else if (strFlag == "Car")
            //{
            //    lobjOTPDetails.OtpType = OTPEnumTypes.CARREVIEWNCONFIRM.ToString();
            //    lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.CARREVIEWNCONFIRM;
            //    strFlag = "PointGateway.aspx?flag=Car";
            //}
            //else if (strFlag == "GiftCard" || strFlag == "EventGiftCard" || strFlag == "AirMilesTopUp" || strFlag == "TopUp" || strFlag == "Lounge" || strFlag == "UtilityGiftCard")
            //{
            //    lobjOTPDetails.OtpType = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM.ToString();
            //    lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM;
            //}
            //else if (strFlag == "Donation")
            //{
            //    lobjOTPDetails.OtpType = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM.ToString();
            //    lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.GIFTCARDREVIEWNCONFIRM;
            //}
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
            else if (strFlag == "Insurance")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.INSURANCEREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.INSURANCEREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=Insurance";
            }
            else if (strFlag == "KhaltiAir")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=KhaltiAir";
            }
            else if (strFlag == "ISP")
            {
                lobjOTPDetails.OtpType = OTPEnumTypes.ISPREVIEWNCONFIRM.ToString();
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.ISPREVIEWNCONFIRM;
                strFlag = "PointGateway.aspx?flag=ISP";
            }
            bool Status = lobjModel.CheckRedemptionOTP(lobjOTPDetails);
            if (lstrOTPCount < 5)
            {
                if (Status)
                {

                    StripePaymentDetails lobjStripePaymentDetails = null;
                    if (HttpContext.Current.Session["StripePaymentDetails"] != null)
                    {
                        lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];
                        ShoppingCart Cart = null;
                        Giift.ShopGateway.Client.Entities.Product lobjProduct = new Giift.ShopGateway.Client.Entities.Product();
                        Giift.ShopGateway.Client.Entities.Address deliveryaddress = null;

                        //Flight
                        CreateItineraryRequest lobjCreateItineraryRequest = HttpContext.Current.Session["ItineraryRequest"] as CreateItineraryRequest;
                        CreateItineraryResponse lobjCreateItineraryResponse = HttpContext.Current.Session["ItineraryResponse"] as CreateItineraryResponse;
                        SearchRequest lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetails"] as SearchRequest;
                        BookingResponse lobjBookingResponse = HttpContext.Current.Session["FlightBookedFailedResponse"] as BookingResponse;

                        //Hotel
                        HotelSearchResponse lobjHotelSearchResponse = HttpContext.Current.Session["BookedHotel"] as HotelSearchResponse;
                        Framework.Integrations.Hotels.Entities.Customer lobjCustomer = HttpContext.Current.Session["CustomerDetails"] as Framework.Integrations.Hotels.Entities.Customer;
                        HotelSearchRequest lobjHotelSearchRequest = HttpContext.Current.Session["SearchDetails"] as HotelSearchRequest;
                        BookingPaymentDetails lobjBookingPaymentDetails = HttpContext.Current.Session["HotelBookingPaymentDetails"] as BookingPaymentDetails;

                        //experience
                        OrderStatusResponse lobjOrderStatusResponse = HttpContext.Current.Session["ExperienceBookingDetails"] as OrderStatusResponse;

                        //Insurance
                        InsuranceUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["InsuranceUserDetails"] as InsuranceUserDetailsResponse;

                        //ISP
                        ISPUserDetailsResponse lobjISPUserdetails = HttpContext.Current.Session["ISPUserDetails"] as ISPUserDetailsResponse;

                        //Domestic Flight
                        CreateDomesticBookingResponse lobjBookingDetailsResponse = HttpContext.Current.Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;

                        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
                        if (Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemAmount) > 0)
                        {
                            string orderId = string.Format("GIIFT-{0}", GenereteRandomNumber());
                            string lstrBookingFlag = string.Empty;
                            if (HttpContext.Current.Session["BookingFlag"] != null)
                            {
                                lstrBookingFlag = HttpContext.Current.Session["BookingFlag"].ToString();
                            }
                            if (lstrBookingFlag.ToLower().Equals("flight"))
                            {
                                lobjStripePaymentDetails.ProductName = "Redemption Flight";
                                List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;

                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                double ldblAmount = Convert.ToInt32(lobjListOfRedemptionDetails[0].Amount);
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = lobjCreateItineraryResponse.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryResponse.ItineraryDetails.DestinationLocation;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                     Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.Air, orderId,
                                     lobjCreateItineraryResponse.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryResponse.ItineraryDetails.DestinationLocation, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("BookingId:{0}|BookingRefCode:{1}", lobjBookingResponse.BookingId, lobjBookingResponse.PNRDetails.BookingReference),
                                     Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("hotel"))
                            {

                                lobjStripePaymentDetails.ProductName = "Redemption Hotel";

                                List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;

                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                double ldblAmount = Convert.ToInt32(lobjListOfRedemptionDetails[0].Amount);
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = lobjHotelSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.hotelname;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                    Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.Hotel, orderId,
                                    lobjHotelSearchResponse.SearchResponse.hotels.hotel[0].basicinfo.hotelname, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("BookingId:{0}|BookingRefCode:{1}", lobjBookingPaymentDetails.Id, lobjBookingPaymentDetails.BookingRefererence),
                                    Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("experience"))
                            {
                                lobjStripePaymentDetails.ProductName = "Redemption Package";
                                HolibobOrderStatus lobjOrderStatus = JsonConvert.DeserializeObject<HolibobOrderStatus>(lobjOrderStatusResponse.data.getOrderStatus);

                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                double ldblAmount = lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes.Select(x => x.totalPrice.gross).FirstOrDefault();
                                string lstrProductName = string.Empty;
                                try
                                {
                                    lstrProductName = lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes.Select(x => x.product.name).FirstOrDefault().Length > 150 ?
                                        lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes.Select(x => x.product.name).FirstOrDefault().Substring(0, 150)
                                        : lobjOrderStatus.data.rawData.data.booking.availabilityList.nodes.Select(x => x.product.name).FirstOrDefault();
                                }
                                catch(Exception ex) 
                                {
                                    LoggingAdapter.WriteLog("ValidateOTP - lstrProductName Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
                                }
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = string.Format("{0} #{1}#{2}", lstrProductName, lobjOrderStatus.data.id, lobjOrderStatus.data.code);
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                   Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.Packages, orderId,
                                   string.Format("{0} #{1}#{2}", lstrProductName, lobjOrderStatus.data.id, lobjOrderStatus.data.code), string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("BookingCode:{0}|BookingUUID:{1}", lobjOrderStatus.data.rawData.data.booking.code, lobjOrderStatus.data.rawData.data.booking.id),
                                   Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("physicalproduct"))
                            {
                                lobjStripePaymentDetails.ProductName = "Redemption Merchant";

                                if (HttpContext.Current.Session["ShoppingCart"] as ShoppingCart != null)
                                {
                                    Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                                }

                                if (HttpContext.Current.Session["CheckoutAddress"] != null)
                                {
                                    deliveryaddress = HttpContext.Current.Session["CheckoutAddress"] as Giift.ShopGateway.Client.Entities.Address;
                                }

                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                float lfltPointRate = 0.0f;
                                List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
                                lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
                                double ldblAmount = Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount)) * lfltPointRate;
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = Cart.Items[0].Name;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                     Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.Merchant, orderId,
                                    Cart.Items[0].Name, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("CartId:{0}|ProductName:{1}|ProductId:{2}", Cart.Id, lobjProduct.Name, lobjProduct.Id),
                                     Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("digitalproduct"))
                            {
                                lobjStripePaymentDetails.ProductName = "Redemption Merchant";

                                if (HttpContext.Current.Session["ShoppingCart"] as ShoppingCart != null)
                                {
                                    Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                                }

                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                float lfltPointRate = 0.0f;
                                List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
                                lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
                                double ldblAmount = Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount)) * lfltPointRate;
                                lobjProduct = lobjshopmodel.GetProductById(Cart.Items[0].ProductId);
                                string lstrDigitalProductType = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("Type")).Value;
                                LoyaltyTxnType lintMerchant = LoyaltyTxnType.Merchant;
                                switch (lstrDigitalProductType.ToLower())
                                {
                                    case "donation":
                                        lintMerchant = LoyaltyTxnType.Charity;
                                        break;
                                    case "giftcard":
                                        lintMerchant = LoyaltyTxnType.GiftCard;
                                        break;
                                    case "game":
                                        lintMerchant = LoyaltyTxnType.Game;
                                        break;
                                    case "topup":
                                        lintMerchant = LoyaltyTxnType.Topup;
                                        break;
                                    case "lounge":
                                        lintMerchant = LoyaltyTxnType.Lounge;
                                        break;
                                    case "miles exchange":
                                    case "milesexchange":
                                        lintMerchant = LoyaltyTxnType.Partner;
                                        break;
                                    case "utiliy":
                                    case "bill pay":
                                    case "billpay":
                                    case "utilites":
                                        lintMerchant = LoyaltyTxnType.BillPayment;
                                        break;
                                }
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = lobjProduct.Name;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                      Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, lintMerchant, orderId,
                                      lobjProduct.Name, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("CartId:{0}|ProductName:{1}|ProductId:{2}", Cart.Id, lobjProduct.Name, lobjProduct.Id),
                                      Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), Cart.Price.Currency.Code);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("insuranceserviceproviders"))
                            {
                                lobjStripePaymentDetails.ProductName = lobjUserdetails.results.Key;
                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                double ldblAmount = Convert.ToInt32(lobjUserdetails.results.Amount);
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = lobjUserdetails.results.Key;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                    Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.Insurance, orderId,
                                    lobjUserdetails.results.Key, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), string.Format("TransactionId:{0}", lobjUserdetails.results.TransactionId),
                                    Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("domesticflight"))
                            {
                                lobjStripePaymentDetails.ProductName = "Redemption Domestic Flight";
                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                float lfltPointRate = 0.0f;
                                List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
                                lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
                                float ldblAmount = Convert.ToInt32(Math.Ceiling(float.Parse(lobjBookingDetailsResponse.CreditsConsumed.ToString()))) * lfltPointRate;
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                     Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.Air, orderId,
                                     lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), "",
                                     Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }
                            else if (lstrBookingFlag.ToLower().Equals("internetserviceproviders"))
                            {
                                lobjStripePaymentDetails.ProductName = lobjISPUserdetails.results.Key;
                                string lstrCurrency = lobjModel.GetDefaultCurrency();
                                string serviceCode = Convert.ToString(HttpContext.Current.Session["ISPServiceCode"]);
                                decimal FinalAmountPayable = Convert.ToDecimal(HttpContext.Current.Session["FinalAmountPayable"]);
                                Packages PackageData = HttpContext.Current.Session["ISPSelectedPackageData"] as Packages;
                                Details PackageDetailsData = HttpContext.Current.Session["ISPSelectedPackageDetailsData"] as Details;
                                string RequestId = HttpContext.Current.Session["ISPUserName"].ToString();
                                int lintPoints = 0;
                                if (lobjISPUserdetails != null)
                                {

                                    if (lobjISPUserdetails.results.Key == "BroadLink" && lobjISPUserdetails.results.Packages.Count > 0)
                                    {
                                        //call GetDiscount and then ISPPaymentRequest API
                                        GetDiscountRequest lobjGetDiscountRequest = new GetDiscountRequest();
                                        GetDiscountResponse lobjGetDiscountResponse = new GetDiscountResponse();
                                        lobjGetDiscountRequest.ServiceCode = serviceCode;
                                        lobjGetDiscountRequest.SessionId = lobjISPUserdetails.results.SessionId;
                                        lobjGetDiscountRequest.Package = PackageData;
                                        lobjGetDiscountResponse = lobjModel.GetDiscount(lobjGetDiscountRequest);
                                        if (lobjGetDiscountResponse != null)
                                        {
                                            if (lobjGetDiscountResponse.results.Status)
                                            {

                                                lintPoints = lobjModel.ConvertToPoints(float.Parse(lobjGetDiscountResponse.results.Amount.ToString())
                                                   , lstrCurrency, lobjProgramDefinition.ProgramId, "ISP");
                                            }
                                            else
                                            {
                                                LoggingAdapter.WriteLog("GetDiscountforISPBroadLink_Packages Failed");
                                            }
                                        }
                                        else
                                        {
                                            LoggingAdapter.WriteLog("GetDiscountforISPBroadLink_Packages Failed");
                                        }
                                    }
                                    else
                                    {
                                        lintPoints = Convert.ToInt32(HttpContext.Current.Session["FinalAmountPayable"]);
                                    }
                                }
                                // int lintPoints = Convert.ToInt32(HttpContext.Current.Session["FinalAmountPayable"]);
                                List<ProgramCurrencyDefinition> lobjProgramCurrencyDefinition = lobjModel.GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
                                var PointRate = lobjProgramCurrencyDefinition[0].RedemptionRate;
                                double ldblAmount = lobjModel.CalculateAmount(lintPoints, PointRate);
                                if (lobjStripePaymentDetails.ReqRedeemPoint != "0")
                                {
                                    merchantname = lobjISPUserdetails.results.Key;
                                    bool response = lobjModel.InsertTransactionDetails(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Convert.ToDecimal(ldblAmount),
                                    Convert.ToInt32(lobjStripePaymentDetails.ReqRedeemPoint), TransactionType.Debit, LoyaltyTxnType.BillPayment, orderId,
                                    lobjISPUserdetails.results.Key, string.Format("{0}|{1}", lobjMemberDetails.Email, lobjMemberDetails.MobileNumber), "",
                                    Convert.ToDecimal(lobjStripePaymentDetails.ReqRedeemAmount), lstrCurrency);
                                }
                            }

                            string lstrClientRefId = Convert.ToString(Guid.NewGuid());
                            lobjStripePaymentDetails.ClientReferenceId = lstrClientRefId;
                            lobjStripePaymentDetails.orderId = orderId;

                            List<object> lobject = new List<object>();
                            lobject.Add(lobjMemberDetails); //[0]
                            lobject.Add(lobjStripePaymentDetails); //[1]
                            lobject.Add(lstrBookingFlag); //[2]

                            lobject.Add(Cart);//[3]
                            lobject.Add(deliveryaddress);//[4]

                            lobject.Add(lobjCreateItineraryRequest);//[5]
                            lobject.Add(lobjCreateItineraryResponse);//[6]
                            lobject.Add(lobjSearchRequest);//[7]
                            lobject.Add(lobjBookingResponse);//[8]

                            lobject.Add(lobjHotelSearchResponse); //[09]
                            lobject.Add(lobjCustomer); //[10]
                            lobject.Add(lobjHotelSearchRequest); //[11]
                            lobject.Add(lobjBookingPaymentDetails); //[12]

                            lobject.Add(lobjOrderStatusResponse); //[13]
                            lobject.Add(lobjUserdetails); //[14]
                            lobject.Add(lobjISPUserdetails);//[15]
                            lobject.Add(lobjBookingDetailsResponse);//[16]
                            LoggingAdapter.WriteLog("PaymentReviewConfirm CachingAdapter ClientRefId-:" + lstrClientRefId);
                            CachingAdapter.Add(lstrClientRefId, lobject);

                            strFlag = CreateStipePayment(lobjStripePaymentDetails, lobjMemberDetails, orderId, merchantname);
                        }
                    }
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
                        else if (strFlag == "Insurance")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.INSURANCEREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.INSURANCEREVIEWNCONFIRM;
                            redemptionType = "Insurance";
                        }
                        else if (strFlag == "KhaltiAir")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.DOMESTICFLIGHTREVIEWNCONFIRM;
                            redemptionType = "Domestic Flight";
                        }
                        else if (strFlag == "ISP")
                        {
                            lobjOTPDetails.OtpType = OTPEnumTypes.ISPREVIEWNCONFIRM.ToString();
                            lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.ISPREVIEWNCONFIRM;
                            redemptionType = "Internet Service Provider";
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