using ABC.Model;
using CB.IBE.Platform.ClientEntities;
using CB.IBE.Platform.Entities;
using CB.IBE.Platform.Hotels.ClientEntities;
using CB.IBE.Platform.Masters.Entities;
using Core.Framework.Booking.Model;
using Core.Platform.Booking.Entities;
using Core.Platform.Helper.ProgramName;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.ProgramMaster.Entities;
using Core.Platform.RedemptionAuditTrail.Entities;
using Core.Platform.Transactions.Entites;
using Framework.EnterpriseLibrary.Adapters;
using Framework.Integrations.Hotels.Entities;
using Giift.ShopGateway.Client.Entities;
using GiiftShopGateway.Model;
using IBEAPI.ClientEntities;
using IBEAPIGateway.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.UI.WebControls;

public partial class PointGateway : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool BookFlight()
    {
        ABCModel lobjModel = new ABCModel();
        BookingIntegrationModel lobjBookingIntegrationModel = new BookingIntegrationModel();
        bool Result = false;
        try
        {
            LoggingAdapter.WriteLog("Booking Flight");

            if (HttpContext.Current.Session["ItineraryRequest"] != null && HttpContext.Current.Session["MemberDetails"] != null && HttpContext.Current.Session["ItineraryResponse"] != null)
            {
                lobjModel.LogActivity(string.Format(ActivityConstants.BookFlight), ActivityType.FlightBooking);

                SearchRequest lobjSearchRequest = HttpContext.Current.Session["FlightSearchDetails"] as SearchRequest;

                CreateItineraryResponse lobjCreateItineraryResponse = HttpContext.Current.Session["ItineraryResponse"] as CreateItineraryResponse;

                CreateItineraryRequest lobjCreateItineraryRequest = HttpContext.Current.Session["ItineraryRequest"] as CreateItineraryRequest;

                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;

                List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;

                RefererDetails lobjRefererDetails = HttpContext.Current.Application["RefererSupplierDetails"] as RefererDetails;

                BookingRequest lobjBookingRequest = new BookingRequest();
                lobjBookingRequest.IsItineraryDateChangeAllowed = true;
                lobjBookingRequest.ItineraryDetails.MemberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
                lobjBookingRequest.ItineraryDetails = lobjCreateItineraryResponse.ItineraryDetails;

                lobjBookingRequest.PointRate = lobjCreateItineraryRequest.PointRate;
                lobjBookingRequest.IPAddress = lobjSearchRequest.IPAddress;
                lobjBookingRequest.SessionId = lobjCreateItineraryRequest.SessionId;

                BookingResponse lobjBookingResponse = lobjBookingIntegrationModel.BookFlight(lobjBookingRequest, lobjMemberDetails, lobjListOfRedemptionDetails);

                if (lobjBookingResponse != null && lobjBookingResponse.PNRDetails.TripId != null && lobjBookingResponse.PNRDetails.TripId != string.Empty && lobjBookingResponse.PNRDetails.Status.Equals(1))
                {
                    ItineraryDetails lobjItineraryDetails = new ItineraryDetails();
                    lobjItineraryDetails = lobjBookingResponse.PNRDetails.ItineraryDetails;
                    lobjItineraryDetails.FareDetails = lobjBookingResponse.PNRDetails.ItineraryDetails.FareDetails;
                    lobjItineraryDetails.TravelerInfo = lobjBookingResponse.PNRDetails.ItineraryDetails.TravelerInfo;
                    lobjItineraryDetails.ItineraryReference = lobjBookingResponse.PNRDetails.BookingReference;
                    HttpContext.Current.Session["FlightBookedResponse"] = lobjBookingResponse;
                    HttpContext.Current.Session["FlightBooked"] = lobjItineraryDetails;
                    HttpContext.Current.Session["ItineraryResponse"] = null;
                    HttpContext.Current.Session["ItineraryRequest"] = null;
                    HttpContext.Current.Session["ReviewFlightDetails"] = null;
                    Result = true;
                }
                else
                {
                    HttpContext.Current.Session["FlightBookedFailedResponse"] = lobjBookingResponse;
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx BookForFlight Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        lobjModel.LogActivity(string.Format(ActivityConstants.BookFlight) + "Status:" + Result, ActivityType.FlightBooking);
        return Result;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool BookHotel()
    {
        bool Result = false;
        ABCModel lobjModel = new ABCModel();
        BookingIntegrationModel lobjBookingIntegrationModel = new BookingIntegrationModel();
        try
        {
            LoggingAdapter.WriteLog("Booking Hotel");
            LoggingAdapter.WriteLog((HttpContext.Current.Session["BookedHotel"] != null && HttpContext.Current.Session["CustomerDetails"] != null && HttpContext.Current.Session["SearchDetails"] != null).ToString());
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (HttpContext.Current.Session["BookedHotel"] != null && HttpContext.Current.Session["CustomerDetails"] != null && HttpContext.Current.Session["SearchDetails"] != null)
                {
                    lobjModel.LogActivity(string.Format(ActivityConstants.BookHotel), ActivityType.HotelBooking);
                    HotelSearchResponse lobjSearchResponse = HttpContext.Current.Session["BookedHotel"] as HotelSearchResponse;
                    HotelSearchResponse lobjHotelBooked = new HotelSearchResponse();
                    lobjHotelBooked = lobjSearchResponse;
                    HotelsSearchRequest lobjSearchRequest = HttpContext.Current.Session["SearchDetails"] as HotelsSearchRequest;
                    Customer lobjCustomer = HttpContext.Current.Session["CustomerDetails"] as Customer;
                    List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["RedemptionDetails"] as List<RedemptionDetails>;

                    List<int> lobjListOfAdult = new List<int>();
                    string[] arrayAdultPerRoom = lobjSearchRequest.AdultPerRoom.Split(',');
                    for (int i = 0; i < arrayAdultPerRoom.Count(); i++)
                    {
                        lobjListOfAdult.Add(Convert.ToInt32(arrayAdultPerRoom[i]));
                    }
                    List<int> lobjListOfChild = new List<int>();
                    string[] arrayChildPerRoom = lobjSearchRequest.ChildrenPerRoom.Split(',');
                    for (int i = 0; i < arrayChildPerRoom.Count(); i++)
                    {
                        lobjListOfChild.Add(Convert.ToInt32(arrayChildPerRoom[i]));
                    }
                    HotelBookingRequest lobjBookingRequest = new HotelBookingRequest();
                    lobjBookingRequest.BookRequest.customer = lobjCustomer;
                    lobjBookingRequest.BookRequest.checkindate = lobjSearchRequest.CheckInDate;
                    lobjBookingRequest.BookRequest.checkoutdate = lobjSearchRequest.CheckOutDate;
                    lobjBookingRequest.BookRequest.numberofrooms = lobjSearchRequest.NoOfRooms;
                    lobjBookingRequest.BookRequest.nri = false;
                    lobjBookingRequest.BookRequest.adultsperroom = lobjListOfAdult.ToArray();
                    lobjBookingRequest.BookRequest.childrenperroom = lobjListOfChild.ToArray();
                    lobjBookingRequest.BookRequest.bookingcode = lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].bookingcode;
                    lobjBookingRequest.BookRequest.roomtypecode = lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].roomtype.roomtypecode;
                    lobjBookingRequest.BookRequest.TotalPoints = lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalPoints;
                    lobjBookingRequest.BookRequest.customeripaddress = lobjSearchRequest.IpAddress;
                    lobjBookingRequest.BookRequest.hotelid = lobjSearchResponse.SearchResponse.hotels.hotel[0].hotelid;
                    lobjBookingRequest.BookRequest.bookingamount = Convert.ToDouble(lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalDefaultAmount);
                    lobjBookingRequest.BookRequest.totalBaseFare = Convert.ToDouble(lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalBaseAmount);
                    lobjBookingRequest.BookRequest.totalDefaulFare = Convert.ToDouble(lobjSearchResponse.SearchResponse.hotels.hotel[0].roomrates.RoomRate[0].TotalDefaultAmount);
                    lobjSearchRequest.MembershipReference = lobjMemberDetails.MemberRelationsList[0].RelationReference;

                    HotelBookingResponse lobjBookingResponse = lobjBookingIntegrationModel.BookHotel(lobjBookingRequest, lobjSearchResponse.SearchResponse.hotels.hotel[0], lobjSearchRequest, lobjMemberDetails, lobjCustomer, lobjListOfRedemptionDetails, lobjSearchResponse.SearchId);

                    HttpContext.Current.Session["BookingResponse"] = lobjBookingResponse;
                    if (lobjBookingResponse != null && lobjBookingResponse.BookingResponse.bookingid != null && lobjBookingResponse.BookingResponse.confirmationnumber != null && lobjBookingResponse.BookingResponse.bookingid != string.Empty && lobjBookingResponse.BookingResponse.confirmationnumber != string.Empty)
                    {
                        HttpContext.Current.Session["BookedHotel"] = null;
                        HttpContext.Current.Session["HotelBooked"] = lobjHotelBooked;
                        Result = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx BookHotel Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        lobjModel.LogActivity(string.Format(ActivityConstants.BookHotel) + "Status:" + Result, ActivityType.HotelBooking);
        return Result;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool BookCar()
    {
        IBEAPIModel lobjModel = new IBEAPIModel();
        ABCModel lobjGimModel = new ABCModel();
        bool Result = false;
        string strRedeemMilesResponse = string.Empty;
        try
        {
            if (HttpContext.Current.Session["MemberDetails"] != null && HttpContext.Current.Session["CarSearchRequest"] != null && HttpContext.Current.Session["CarBookingRequest"] != null && HttpContext.Current.Session["SelectedCar"] != null)
            {
                CarBookingResponse lobjCarBookingResponse = new CarBookingResponse();
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                GetAvailabilityRequest lobjCarSearchRequest = HttpContext.Current.Session["CarSearchRequest"] as GetAvailabilityRequest;
                CarBookingRequest lobjCarBookingRequest = HttpContext.Current.Session["CarBookingRequest"] as CarBookingRequest;
                IBEAPI.ClientEntities.Rate lobjMatch = HttpContext.Current.Session["SelectedCar"] as IBEAPI.ClientEntities.Rate;

                ProgramDefinition lobjProgramDefinition = lobjGimModel.GetProgramMaster();
                List<ProgramCurrencyDefinition> lobjProgramCurrencyDefinition = lobjGimModel.GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
                var PointRate = lobjProgramCurrencyDefinition[0].RedemptionRate;
                string lstrCurrency = lobjGimModel.GetDefaultCurrency();
                double RequierdRedeemPoint = Convert.ToDouble(HttpContext.Current.Session["CarTotalRedeemAmount"]);
                double ldblAmount = lobjGimModel.CalculateCarAmount(Convert.ToDouble(HttpContext.Current.Session["CarTotalRedeemAmount"]), PointRate);

                strRedeemMilesResponse = lobjGimModel.RedeemPoints(Convert.ToSingle(ldblAmount), Convert.ToInt32(RequierdRedeemPoint),
                       lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                       lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
                       "Car Redemption " + lobjCarSearchRequest.pickUp.location.name + "-" + lobjCarSearchRequest.dropOff.location.name, (int)LoyaltyTxnType.Car, lstrCurrency, "");

                if (!string.IsNullOrEmpty(strRedeemMilesResponse))
                {
                    lobjCarBookingRequest.brokerReference = strRedeemMilesResponse;
                    lobjCarBookingRequest.pointrate = Convert.ToString(PointRate);
                    lobjCarBookingResponse = lobjModel.CreateCarBooking(lobjCarBookingRequest);
                    LoggingAdapter.WriteLog("PointGateway.aspx bookcar lobjCarBookingResponse : " + lobjCarBookingResponse);

                    if (lobjCarBookingResponse != null && lobjCarBookingResponse.data.reference_id != string.Empty)
                    {
                        HttpContext.Current.Session["CarBookingResponse"] = lobjCarBookingResponse;
                        SendCarBookingEmailSMS("Success");
                        lobjGimModel.LogActivity(string.Format("CarBooking Success; Car Pickup Drop Loc-:{0}; BookingId-:{1};Total Amount-:{2};", lobjCarSearchRequest.pickUp.location.name + "-" + lobjCarSearchRequest.dropOff.location.name, lobjCarBookingResponse.data.reference_id, RequierdRedeemPoint), ActivityType.CarBooking);
                        Result = true;
                    }
                    else
                    {
                        lobjGimModel.RollBackTransaction(strRedeemMilesResponse, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, "Rollback Car Redemption " + lobjCarSearchRequest.pickUp.location.name + "-" + lobjCarSearchRequest.dropOff.location.name);
                        LoggingAdapter.WriteLog("PointGateway.aspx RollBackTransaction redeemmilesresponse : " + strRedeemMilesResponse);
                        Result = false;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx BookCar Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        lobjGimModel.LogActivity(string.Format(ActivityConstants.BookCar) + "Status:" + Result, ActivityType.CarBooking);
        return Result;
    }
    private static void SendCarBookingEmailSMS(string status)
    {
        try
        {
            ABCModel lobjModel = new ABCModel();
            List<string> lstrEmailParameters = new List<string>();
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            GetAvailabilityRequest lobjGetAvailabilityRequest = HttpContext.Current.Session["CarSearchRequest"] as GetAvailabilityRequest;
            CarBookingRequest lobjCarBookingRequest = HttpContext.Current.Session["CarBookingRequest"] as CarBookingRequest;
            CarBookingResponse lobjCarBookingResponse = HttpContext.Current.Session["CarBookingResponse"] as CarBookingResponse;
            IBEAPI.ClientEntities.Rate lobjSelectedCar = HttpContext.Current.Session["SelectedCar"] as IBEAPI.ClientEntities.Rate;
            double RequierdRedeemPoint = Convert.ToDouble(HttpContext.Current.Session["CarTotalRedeemAmount"]);
            dynamic dynamicCls = new System.Dynamic.ExpandoObject();
            string lsrtTemplateLangCode = "";
            if (HttpContext.Current.Session["MemberDetails"] != null && HttpContext.Current.Session["CarBookingResponse"] != null && HttpContext.Current.Session["CarSearchRequest"] != null && HttpContext.Current.Session["CarBookingRequest"] != null && HttpContext.Current.Session["SelectedCar"] != null)
            {
                dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                dynamicCls.LastName = lobjMemberDetails.LastName;
                dynamicCls.program_id = lobjMemberDetails.ProgramId;
                dynamicCls.to_email = lobjMemberDetails.Email;
                dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                dynamicCls.program_id = lobjMemberDetails.ProgramId;
                dynamicCls.reference_id = lobjCarBookingResponse.data.reference_id;
                dynamicCls.pickuplocationname = lobjGetAvailabilityRequest.pickUp.location.name;
                dynamicCls.dropofflocationname = lobjGetAvailabilityRequest.dropOff.location.name;
                dynamicCls.pickupdate = lobjGetAvailabilityRequest.pickUp.date;
                dynamicCls.dropoffdate = lobjGetAvailabilityRequest.dropOff.date;
                dynamicCls.dropofftime = lobjGetAvailabilityRequest.dropOff.Time;
                dynamicCls.Pickuptime = lobjGetAvailabilityRequest.pickUp.Time;
                dynamicCls.vehiclename = lobjSelectedCar.vehicle.name;
                dynamicCls.vehicletransmission = lobjSelectedCar.vehicle.transmission;
                dynamicCls.vehicleairco = (lobjSelectedCar.vehicle.airco.ToString() == "1" ? "Yes" : "No");
                dynamicCls.vehicletype = lobjSelectedCar.vehicle.type;
                dynamicCls.fueltype = lobjSelectedCar.vehicle.fuelType;
                dynamicCls.firstName = lobjCarBookingRequest.customer.firstName + " " + lobjCarBookingRequest.customer.lastName;
                dynamicCls.vehicleprice = lobjSelectedCar.vehicle.payment;
                dynamicCls.vehicleseat = lobjSelectedCar.vehicle.seats;
                dynamicCls.TotalPoints = RequierdRedeemPoint;
            }
            if (status.ToLower().Equals("success"))
            {
                if (lobjMemberDetails.PreferredLanguage == "EN")
                {
                    dynamicCls.event_name = "Car_Booked";
                }
                if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                {
                    lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                    dynamicCls.event_name = "Car_Booked";
                }
                // dynamicCls.Status = "Confirmed";
            }
            else
            {
                if (lobjMemberDetails.PreferredLanguage == "EN")
                {
                    dynamicCls.event_name = "Car_Booking_Failed";
                }
                if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                {
                    lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                    dynamicCls.event_name = "Car_Booking_Failed";
                }
                // dynamicCls.Status = "Failed";
            }
            Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
            IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
            foreach (var key in dict)
            {
                lobjDictionary.Add(key.Key, key.Value);
            }
            string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
            lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
            //Communication Engine Call for Sms Sending
            List<string> lstSMSparameter = JsonConvert.DeserializeObject<List<string>>(jsonParameters);
            lobjModel.SendSMS(lstSMSparameter, lobjMemberDetails, dynamicCls.event_name);
            LoggingAdapter.WriteLog("Car SMS Sent parameters=" + lstSMSparameter);
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway SendCarBookingEmailSMS Ex-:" + ex.InnerException + ex.StackTrace + ex.Message);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool PurchaseShopDigital()
    {
        bool lblnResponse = false;
        try
        {
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();
            if (HttpContext.Current.Session["MemberDetails"] != null)
            {
                Store lobjStore = model.GetStoreDetails();
                MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                if (HttpContext.Current.Session["Product"] != null)
                {
                    string lstrProductId = string.Empty; int lintQty = 0; string lstrUserInputMetas = string.Empty;
                    Dictionary<string, string> ldictobjParameter = HttpContext.Current.Session["CheckoutMethodParameter"] as Dictionary<string, string>;
                    HttpContext.Current.Session["CheckoutMethodParameter"] = null;
                    if (ldictobjParameter.ContainsKey("lstrProductId"))
                    {
                        lstrProductId = ldictobjParameter["lstrProductId"];
                    }
                    if (ldictobjParameter.ContainsKey("lintQty"))
                    {
                        lintQty = Convert.ToInt32(ldictobjParameter["lintQty"]);
                    }
                    if (ldictobjParameter.ContainsKey("lstrProductId"))
                    {
                        lstrUserInputMetas = ldictobjParameter["lstrUserInputMetas"];
                    }
                    //ShoppingCart lobjShoppingCart = lobjModel.ExpressCheckout(lstrProductId, lintQty, lobjMemberDetails, lstrUserInputMetas);
                    //HttpContext.Current.Session["ShoppingCart"] = lobjShoppingCart;
                    Product lobjProductDetails = HttpContext.Current.Session["Product"] as Product;
                    if (lobjProductDetails.ProductType == "Digital")
                    {
                        lblnResponse = PlaceOrder();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx PurchaseShopDigital Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lblnResponse;
    }
    public static bool PlaceOrder()
    {
        bool lblnResponse = false;
        StripePaymentDetails lobjStripePaymentDetails = new StripePaymentDetails();
        if (HttpContext.Current.Session["StripePaymentDetails"] != null)
        {
            lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];
        }
        try
        {
            ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
            ABCModel lobjModel = new ABCModel();
            ShopModel model = new ShopModel();

            var ShippingMethods = model.GetAvailableShippingRates(Cart.Id);
            Cart = model.UpdateCartShipment(Cart, ShippingMethods.FirstOrDefault(), null);
            var PaymentMethods = model.GetAvailablePaymentMethods(Cart.Id);
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            string lstrCurrency = lobjModel.GetDefaultCurrency();
            float lfltPointRate = 0.0f;
            List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
            lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
            double ldblAmount = Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount)) * lfltPointRate;
            Product lobjProduct = new Product();
            lobjProduct = model.GetProductById(Cart.Items[0].ProductId);
            string lstrDigitalProductType = lobjProduct.Properties.ToList().Find(lobj => lobj.Name.Equals("Type")).Value;
            int lintMerchant = (int)LoyaltyTxnType.Merchant;
            switch (lstrDigitalProductType.ToLower())
            {
                case "donation":
                    lintMerchant = (int)LoyaltyTxnType.Charity;
                    break;
                case "giftcard":
                    lintMerchant = (int)LoyaltyTxnType.GiftCard;
                    break;
                case "game":
                    lintMerchant = (int)LoyaltyTxnType.Game;
                    break;
                case "topup":
                    lintMerchant = (int)LoyaltyTxnType.Topup;
                    break;
                case "lounge":
                    lintMerchant = (int)LoyaltyTxnType.Lounge;
                    break;
                case "miles exchange":
                case "milesexchange":
                    lintMerchant = (int)LoyaltyTxnType.Partner;
                    break;
                case "giftboxoffer":
                    lintMerchant = (int)LoyaltyTxnType.Miscellaneous;
                    break;
                case "utiliy":
                case "bill pay":
                case "billpay":
                case "utilites":
                    lintMerchant = (int)LoyaltyTxnType.BillPayment;
                    break;
            }
            string strRedeemMilesResponse = lobjModel.RedeemPoints(Convert.ToSingle(ldblAmount), Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount)),
                lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
                lobjProduct.Name, lintMerchant, lstrCurrency, lobjProduct.VendorId);

            Cart = model.UpdateCartPayment(Cart, PaymentMethods.FirstOrDefault(), strRedeemMilesResponse, lobjStripePaymentDetails);
            bool lblRollback = false;
            if (!string.IsNullOrEmpty(strRedeemMilesResponse))
            {
                var Order = model.CreateOrderFromCart(Cart, lobjStripePaymentDetails);
                if (Order == null)
                {
                    lblRollback = true;
                    LoggingAdapter.WriteLog("PurchaseShopDigital Order Null first call RedeemMilesResponse" + strRedeemMilesResponse + " Cart " + Cart.ToString());
                }
                else
                {
                    CustomerOrder lobjorder = null;
                    lobjorder = model.GetOrderByNumber(Order.Number);
                    if (lobjorder.Status == "Placed" || lobjorder.Status.ToLower().Equals("completed"))
                    {
                        string lstrHtmlContent = string.Empty;
                        string lstrShippingAddressContent = string.Empty;
                        List<string> lstrEmailParameters = new List<string>();

                        lstrHtmlContent += "<table border=\"0\" style=\"width:100%;font-size:12px;font-family:arial;padding:2%;\">"
                                        + "<tbody>"
                                        + "<tr style=\"background-color:#b9babe;text-align:center\">"
                                        + "<th>Name</th>"
                                        + "<th>Price</th>"
                                        + "<th>Quantity</th>"
                                        + "<th>Total</th>"
                                        + "</tr>";
                        for (int i = 0; i < Order.Items.Count; i++)
                        {
                            lstrHtmlContent += "<tr style=\"background-color:#ebecee; text-align:center\">"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:left\">" + Order.Items[i].Name + "</td>"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ListPrice.Amount), "Points") + "</td>"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:center\">" + Order.Items[i].Quantity + "</td>"
                                            + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ExtendedPrice.Amount), "Points") + "</td>"
                                            + "</tr>";
                        }
                        lstrHtmlContent += "<tr style=\"text-align:right\">"
                                        + "<td></td>"
                                        + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Sub-Total:</strong></td>"
                                        + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.SubTotal.Amount), "Points") + "</strong></td>"
                                        + "</tr>"

                                        //+ "<tr style=\"text-align:right\">"
                                        //+ "<td></td>"
                                        //+ "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Shipping:</strong></td>"
                                        //+ "<td style = \"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.ShippingTotal.Amount)) + "</strong></td>"
                                        //+ "</tr>"

                                        //+ "<tr style=\"text-align:right\">"
                                        //+ "<td></td>"
                                        //+ "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Tax:</strong></td>"
                                        //+ "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.TaxTotal.Amount)) + "</strong></td>"
                                        //+ "</tr>"

                                        + "<tr style=\"text-align:right\">"
                                        + "<td></td>"
                                        + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Order Total:</strong></td>"
                                        + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points") + "</strong></td>"
                                        + "</tr>"

                                        + "</tbody>"
                                        + "</table>";
                        string CustomerName = string.Empty;
                        if (string.IsNullOrEmpty(Order.CustomerName))
                        {
                            CustomerName = Convert.ToString(lobjMemberDetails.FullName);
                        }
                        else
                        {
                            CustomerName = Convert.ToString(Order.CustomerName);
                        }
                        lstrShippingAddressContent += "<table  cellpadding='5' cellspacing='0' border='0' style='width:100%;font-size:12px;font-family:arial;padding:2%;border: 1px solid #333333;'>";

                        lstrShippingAddressContent += "<tr style='background: #b9babe' style='background-color:#b9babe;text-align:center'>";
                        lstrShippingAddressContent += "<th colspan='2'>Customer Details</th>";

                        lstrShippingAddressContent += "</tr>";


                        lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";

                        lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'><strong>Name</strong></td>";

                        lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + CustomerName + "</td>";

                        lstrShippingAddressContent += "</tr>";

                        //Shipping Address
                        if (Order.ShippingAddress != null)
                        {
                            lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";

                            lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'><strong> Shipping Address </strong></td>";

                            lstrShippingAddressContent += "		<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + Convert.ToString(Order.ShippingAddress.Line1 + " " + Order.ShippingAddress.Line2 + ", " + ((Order.ShippingAddress.City == "undefined") ? "" : Order.ShippingAddress.City) + ", " + Order.ShippingAddress.CountryName + ", " + Order.ShippingAddress.PostalCode) + "</td>";

                            lstrShippingAddressContent += "</tr>";
                        }


                        lstrShippingAddressContent += "</table>";
                        DateTime ldtCreatedDate = DateTime.Parse(Convert.ToString(Order.CreatedDate));
                        lstrEmailParameters.Add(Order.Number); //0
                        lstrEmailParameters.Add(Convert.ToString(ldtCreatedDate.ToLocalTime()));//1
                        lstrEmailParameters.Add(lstrHtmlContent);//2 order Details
                        lstrEmailParameters.Add(lstrShippingAddressContent);//3 customer/shipping Address
                        lstrEmailParameters.Add(lobjMemberDetails.FullName); //4 LastName

                        dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                        string lsrtTemplateLangCode = "";
                        if (lobjMemberDetails.PreferredLanguage == "EN")
                        {
                            dynamicCls.event_name = "Shop_Order";
                        }
                        if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                        {
                            lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                            dynamicCls.event_name = lsrtTemplateLangCode + "Shop_Order";
                        }
                        dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                        dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                        dynamicCls.to_email = lobjMemberDetails.Email;
                        dynamicCls.full_name = lobjMemberDetails.FullName;
                        dynamicCls.order_no = Order.Number;
                        dynamicCls.order_date = Convert.ToString(ldtCreatedDate.ToLocalTime());
                        dynamicCls.order_details = lstrHtmlContent;
                        dynamicCls.order_shipping_details = lstrShippingAddressContent;
                        dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                        dynamicCls.point_issued = lobjModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points");
                        Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                        IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                        foreach (var key in dict)
                        {
                            lobjDictionary.Add(key.Key, key.Value);
                        }
                        string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                        lobjModel.SendEmails(jsonParameters, lobjMemberDetails);

                        HttpContext.Current.Session["ShoppingCart"] = null;
                        try
                        {
                            if (HttpContext.Current.Session["RelationshipManager"] != null)
                            {
                                AuditTrailForRedemption lobjAuditTrailForRedemption = new AuditTrailForRedemption
                                {
                                    RedemptionTypeEnum = RedemptionTypeEnum.MERCHANT,
                                    RedemptionReference = strRedeemMilesResponse,
                                    PointsRedeemed = Convert.ToString(Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount))),
                                    RedemptionDetails = "Redemption Shop Digital: " + Cart.ToString(),
                                    CreatedBy = Convert.ToString(HttpContext.Current.Session["RelationshipManager"])
                                };
                                bool status = lobjModel.InsertRedemptionAuditTrail(lobjAuditTrailForRedemption);
                            }
                        }
                        catch (Exception ex)
                        {
                            LoggingAdapter.WriteLog("PointGateway InsertRedemptionAuditTrail Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                        }
                        lblnResponse = true;
                    }
                    else
                    {
                        model.UpdateCustomerOrder(lobjorder, "Failed", lobjStripePaymentDetails);
                        LoggingAdapter.WriteLog("PurchaseShopDigital Order Null second call RedeemMilesResponse" + strRedeemMilesResponse + " Cart " + Cart.ToString());
                    }
                }
            }
            if (lblRollback == true)
            {
                LoggingAdapter.WriteLog("PurchaseShopDigital Order Null rolback call RedeemMilesResponse" + strRedeemMilesResponse + " Cart " + Cart.ToString());
            }
            HttpContext.Current.Session["AvailablePoints"] = null;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx PlaceOrder Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lblnResponse;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static bool CheckoutShop()
    {
        bool lblnResponse = false;
        StripePaymentDetails lobjStripePaymentDetails = new StripePaymentDetails();
        if (HttpContext.Current.Session["StripePaymentDetails"] != null)
        {
            lobjStripePaymentDetails = (StripePaymentDetails)HttpContext.Current.Session["StripePaymentDetails"];
        }
        try
        {
            if (HttpContext.Current.Session["ShoppingCart"] != null)
            {
                ShoppingCart Cart = HttpContext.Current.Session["ShoppingCart"] as ShoppingCart;
                if (HttpContext.Current.Session["CheckoutAddress"] != null)
                {
                    Giift.ShopGateway.Client.Entities.Address deliveryaddress = HttpContext.Current.Session["CheckoutAddress"] as Giift.ShopGateway.Client.Entities.Address;
                    ShopModel lobjModel = new ShopModel();
                    ABCModel lobjVerveModel = new ABCModel();
                    var ShippingMethods = lobjModel.GetAvailableShippingRates(Cart.Id);
                    Cart = lobjModel.UpdateCartShipment(Cart, ShippingMethods.FirstOrDefault(), deliveryaddress);
                    var PaymentMethods = lobjModel.GetAvailablePaymentMethods(Cart.Id);
                    MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
                    string lstrCurrency = lobjModel.GetDefaultCurrency();
                    float lfltPointRate = 0.0f;
                    List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
                    lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
                    Product lobjProduct = new Product();
                    lobjProduct = lobjModel.GetProductById(Cart.Items[0].ProductId);
                    double ldblAmount = Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount)) * lfltPointRate;
                    string lstrAllProductNames = string.Join(", ", Cart.Items.Select(x => x.Name).ToList());
                    string strRedeemMilesResponse = lobjModel.RedeemPoints(Convert.ToSingle(ldblAmount), Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount)),
                        lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                        lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
                        lstrAllProductNames.Length > 100 ? lstrAllProductNames.Substring(0, 99) : lstrAllProductNames
                        , (int)LoyaltyTxnType.Merchant
                        , lstrCurrency, lobjProduct.VendorId);
                    Cart = lobjModel.UpdateCartPayment(Cart, PaymentMethods.FirstOrDefault(), strRedeemMilesResponse, lobjStripePaymentDetails);
                    bool lblRollback = false;
                    if (!string.IsNullOrEmpty(strRedeemMilesResponse))
                    {
                        var Order = lobjModel.CreateOrderFromCart(Cart, lobjStripePaymentDetails);
                        if (Order == null)
                        {
                            lblRollback = true;
                            LoggingAdapter.WriteLog("CheckoutShop Order Null first call RedeemMilesResponse" + strRedeemMilesResponse + " Cart " + Cart.ToString());
                        }
                        else
                        {
                            CustomerOrder lobjorder = null;
                            lobjorder = lobjModel.GetOrderByNumber(Order.Number);
                            if (lobjorder == null)
                            {
                                LoggingAdapter.WriteLog(" CheckoutShop Order Null Second call RedeemMilesResponse" + strRedeemMilesResponse + " Cart " + Cart.ToString());
                                lblRollback = true;
                            }
                            else
                            {
                                if (lobjorder.Status == "Placed" || lobjorder.Status.ToLower().Equals("completed"))
                                {
                                    LoggingAdapter.WriteLog("Checkout Order response not null");
                                    string lstrHtmlContent = string.Empty;
                                    string lstrShippingAddressContent = string.Empty;
                                    List<string> lstrEmailParameters = new List<string>();
                                    lstrHtmlContent += "<table border=\"0\" style=\"width:100%;font-size:12px;font-family:arial;padding:2%;\">"
                                                    + "<tbody>"
                                                    + "<tr style=\"background-color:#b9babe;text-align:center\">"
                                                    + "<th>Name</th>"
                                                    + "<th>Price</th>"
                                                    + "<th>Quantity</th>"
                                                    + "<th>Total</th>"
                                                    + "</tr>";

                                    for (int i = 0; i < Order.Items.Count; i++)
                                    {
                                        lstrHtmlContent += "<tr style=\"background-color:#ebecee; text-align:center\">"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:left\">" + Order.Items[i].Name + "</td>"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjVerveModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ListPrice.Amount), "Points") + "</td>"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:center\">" + Order.Items[i].Quantity + "</td>"
                                                        + "<td style=\"padding:0.6em 0.4em; text-align:right\">" + lobjVerveModel.FormatPoints(Math.Ceiling(Order.Items[i].Price.ExtendedPrice.Amount), "Points") + "</td>"
                                                        + "</tr>";
                                    }

                                    lstrHtmlContent += "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Sub-Total:</strong></td>"
                                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjVerveModel.FormatPoints(Math.Ceiling(Order.Price.SubTotal.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Shipping:</strong></td>"
                                                    + "<td style = \"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjVerveModel.FormatPoints(Math.Ceiling(Order.Price.ShippingTotal.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan = \"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Tax:</strong></td>"
                                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjVerveModel.FormatPoints(Math.Ceiling(Order.Price.TaxTotal.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "<tr style=\"text-align:right\">"
                                                    + "<td></td>"
                                                    + "<td colspan=\"2\" style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>Order Total:</strong></td>"
                                                    + "<td style=\"background-color:#dde2e6; padding:0.6em 0.4 em\"><strong>" + lobjVerveModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points") + "</strong></td>"
                                                    + "</tr>"

                                                    + "</tbody>"
                                                    + "</table>";

                                    string CustomerName = string.Empty;
                                    if (string.IsNullOrEmpty(Order.CustomerName))
                                    {
                                        CustomerName = Convert.ToString(lobjMemberDetails.FullName);
                                    }
                                    else
                                    {
                                        CustomerName = Convert.ToString(Order.CustomerName);
                                    }
                                    lstrShippingAddressContent += "<table  cellpadding='5' cellspacing='0' border='0' style='width:100%;font-size:12px;font-family:arial;padding:2%;border: 1px solid #333333;'>";
                                    lstrShippingAddressContent += "<tr style='background: #b9babe' style='background-color:#b9babe;text-align:center'>";
                                    lstrShippingAddressContent += "<th colspan='2'>Customer Details</th>";
                                    lstrShippingAddressContent += "</tr>";
                                    lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";
                                    lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'><strong>Name</strong></td>";
                                    lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + CustomerName + "</td>";
                                    lstrShippingAddressContent += "</tr>";

                                    //Shipping Address
                                    if (Order.ShippingAddress != null)
                                    {
                                        lstrShippingAddressContent += "<tr style='background-color:#ebecee; text-align:center'>";
                                        lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'><strong> Shipping Address </strong></td>";
                                        lstrShippingAddressContent += "<td style='padding:0.6em 0.4em; text-align:left; border:1px solid #333;'>" + Convert.ToString(Order.ShippingAddress.Line1 + " " + Order.ShippingAddress.Line2 + " " + Order.ShippingAddress.CountryName + " " + Order.ShippingAddress.City + " " + Order.ShippingAddress.PostalCode) + "</td>";
                                        lstrShippingAddressContent += "</tr>";
                                    }
                                    lstrShippingAddressContent += "</table>";
                                    DateTime ldtCreatedDate = DateTime.Parse(Convert.ToString(Order.CreatedDate));
                                    lstrEmailParameters.Add(Order.Number); //0
                                    lstrEmailParameters.Add(Convert.ToString(ldtCreatedDate.ToLocalTime()));//1
                                    lstrEmailParameters.Add(lstrHtmlContent);//2 order Details
                                    lstrEmailParameters.Add(lstrShippingAddressContent);//3 customer/shipping Address
                                    lstrEmailParameters.Add(lobjMemberDetails.FullName); //4 LastName

                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                                    string lsrtTemplateLangCode = "";
                                    if (lobjMemberDetails.PreferredLanguage == "EN")
                                    {
                                        dynamicCls.event_name = "Shop_Order";
                                    }
                                    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                                    {
                                        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                                        dynamicCls.event_name = lsrtTemplateLangCode + "Shop_Order";
                                    }
                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
                                    dynamicCls.to_email = lobjMemberDetails.Email;
                                    dynamicCls.full_name = lobjMemberDetails.FullName;
                                    dynamicCls.order_no = Order.Number;
                                    dynamicCls.order_date = Convert.ToString(ldtCreatedDate.ToLocalTime());
                                    dynamicCls.order_details = lstrHtmlContent;
                                    dynamicCls.order_shipping_details = lstrShippingAddressContent;
                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                                    dynamicCls.point_issued = lobjVerveModel.FormatPoints(Math.Ceiling(Order.Price.Total.Amount), "Points");
                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                                    foreach (var key in dict)
                                    {
                                        lobjDictionary.Add(key.Key, key.Value);
                                    }
                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                                    lobjVerveModel.SendEmails(jsonParameters, lobjMemberDetails);
                                    LoggingAdapter.WriteLog("Checkout Proceed send email ShopOrderPlaced success");


                                    //if (lobjMemberDetails.PreferredLanguage == "EN")
                                    //{
                                    //    lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, "ShopOrderPlaced");
                                    //    LoggingAdapter.WriteLog("Checkout Proceed send email ShopOrderPlaced success");
                                    //}
                                    //else
                                    //{
                                    //    string lsrtTemplateLangCode = "";
                                    //    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                                    //    {
                                    //        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                                    //    }
                                    //    lobjModel.SendEmail(lstrEmailParameters, lobjMemberDetails, lsrtTemplateLangCode + "ShopOrderPlaced");
                                    //    LoggingAdapter.WriteLog("Checkout Proceed send email ShopOrderPlaced success");
                                    //}
                                    HttpContext.Current.Session["ShoppingCart"] = null;
                                    try
                                    {
                                        if (HttpContext.Current.Session["RelationshipManager"] != null)
                                        {
                                            ABCModel lobjABLModel = new ABCModel();
                                            AuditTrailForRedemption lobjAuditTrailForRedemption = new AuditTrailForRedemption
                                            {
                                                RedemptionTypeEnum = RedemptionTypeEnum.MERCHANT,
                                                RedemptionReference = strRedeemMilesResponse,
                                                PointsRedeemed = Convert.ToString(Convert.ToInt32(Math.Ceiling(Cart.Price.Total.Amount))),
                                                RedemptionDetails = "Redemption Shop: " + Cart.ToString(),
                                                CreatedBy = Convert.ToString(HttpContext.Current.Session["RelationshipManager"])
                                            };
                                            bool status = lobjABLModel.InsertRedemptionAuditTrail(lobjAuditTrailForRedemption);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggingAdapter.WriteLog("PointGateway InsertRedemptionAuditTrail Ex-" + ex.InnerException + ex.StackTrace + ex.Message);
                                    }
                                    lblnResponse = true;
                                }
                                else
                                {
                                    lblRollback = true;
                                    lobjModel.UpdateCustomerOrder(lobjorder, "Failed", lobjStripePaymentDetails);
                                    LoggingAdapter.WriteLog("CheckoutShop page RollBackTransaction " + strRedeemMilesResponse + " Cart " + Cart.ToString());
                                }
                            }
                        }
                        if (lblRollback == true)
                        {
                            lobjModel.RollBackTransaction(strRedeemMilesResponse, lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference, Cart.ToString());
                            return false;
                        }
                    }
                }
                else
                {
                    LoggingAdapter.WriteLog("PointGateway.aspx CheckoutShop - CheckoutAddress is null");
                }
            }
            else
            {
                LoggingAdapter.WriteLog("PointGateway.aspx CheckoutShop - ShoppingCart is null");
            }
            HttpContext.Current.Session["AvailablePoints"] = null;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx CheckoutShop Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lblnResponse;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string BookPackage()
    {
        ABCModel lobjModel = new ABCModel();
        string lblnResult = string.Empty;
        try
        {
            StringBuilder lNICogRequestResponse = new StringBuilder();
            LoggingAdapter.WriteLog("Booking Package");
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            BeMyGuest.Entities.BookingRequest bookingRequest = HttpContext.Current.Session["ExperienceBookingRequest"] as BeMyGuest.Entities.BookingRequest;
            List<RedemptionDetails> lobjListOfRedemptionDetails = HttpContext.Current.Session["ExperienceRedemptionDetails"] as List<RedemptionDetails>;
            BeMyGuest.Entities.ProductInfoResponse productInfoResponse = new BeMyGuest.Entities.ProductInfoResponse();
            if (HttpContext.Current.Session["ProductInfo"] != null)
            {
                productInfoResponse = HttpContext.Current.Session["ProductInfo"] as BeMyGuest.Entities.ProductInfoResponse;
            }
            if (lobjMemberDetails != null)
            {
                if (HttpContext.Current.Session["ExperienceBookingRequest"] != null)
                {
                    LoggingAdapter.WriteLog("Inside Book Purchase");
                    lobjModel.LogActivity(string.Format(ActivityConstants.BookPackage), ActivityType.PackageBooking);
                    string lstrCurrency = lobjModel.GetDefaultCurrency();

                    string lstrProductName = string.Empty;

                    string lstrProgramName = ProgramHelper.ProgramName();
                    ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
                    float Pointrate = lobjModel.GetProgramRedemptionRate(lstrCurrency, "EXPERIENCE", lobjProgramDefinition.ProgramId);

                    string lstrRedeemResponse = lobjModel.RedeemPoints((float)(lobjListOfRedemptionDetails[0].Amount),
                     lobjListOfRedemptionDetails[0].Points,
                     lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
                     lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
                     productInfoResponse.data.title, Convert.ToInt32(LoyaltyTxnType.Packages), lobjListOfRedemptionDetails[0].Currency,
                     lobjMemberDetails.MemberRelationsList[0].RelationReference);
                    
                    LoggingAdapter.WriteLog("RedeemPointsforPackage success - '" + lstrRedeemResponse + "'");

                    if (!string.IsNullOrEmpty(lstrRedeemResponse))
                    {
                        try
                        {
                            BeMyGuest.Entities.BookingResponse bookingResponse = lobjModel.ExperienceBooking(bookingRequest);
                            if (bookingResponse != null)
                            {
                                if (bookingResponse.success == 1)
                                {
                                    HttpContext.Current.Session["PackageBookingId"] = bookingResponse.bookingData.uuid;
                                    SendExperienceEmail(bookingRequest, bookingResponse);
                                    lblnResult = "ExperienceProductStatus.aspx?Success=true";
                                }
                                else
                                {
                                    if ((bookingResponse.success != 1) && bookingRequest.totalAmount != 0)
                                    {
                                        lobjModel.RollBackTransaction(lstrRedeemResponse, bookingRequest.memberId, bookingRequest.titleName);
                                    }
                                    lblnResult = "ExperienceProductStatus.aspx?Success=false";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LoggingAdapter.WriteLog("BookingPurchase Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                            lobjModel.RollBackTransaction(lstrRedeemResponse, bookingRequest.memberId, bookingRequest.titleName);
                            LoggingAdapter.WriteLog("BookingPurchase Ex Rollback Success");
                        }
                    }
                    HttpContext.Current.Session["AvailablePoints"] = null;
                }
            }
            lobjModel.LogActivity(string.Format(ActivityConstants.BookPackage) + "Status:" + lblnResult + " - " + lNICogRequestResponse, ActivityType.PackageBooking);
            return lblnResult;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("PointGateway.aspx- BookPackage Ex: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
            return null;
        }
    }
    private static void SendExperienceEmail(BeMyGuest.Entities.BookingRequest bookingRequest, BeMyGuest.Entities.BookingResponse bookingResponse)
    {
        ABCModel lobjModel = new ABCModel();
        try
        {
            MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            if (!string.IsNullOrEmpty(bookingRequest.customer.email))
            {
                StringBuilder sbAdditionalInfoHtml = new StringBuilder();
                if (bookingResponse.bookingData.options.Count > 0)
                {
                    int optionsItemIndex = 0;
                    foreach (var optionsItem in bookingResponse.bookingData.options)
                    {
                        if (optionsItemIndex % 2 == 0)
                        {
                            sbAdditionalInfoHtml.Append("<tr>");
                            sbAdditionalInfoHtml.Append("<td width='20%' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            sbAdditionalInfoHtml.Append("<strong>" + optionsItem.label + ":</strong></td>");
                            sbAdditionalInfoHtml.Append("<td width='30%' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("datetime"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("time")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("select")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Equals("freelunch"))
                            {
                                if (Convert.ToInt32(optionsItem.value) == 1)
                                {
                                    sbAdditionalInfoHtml.Append("Yes");
                                }
                                else
                                {
                                    sbAdditionalInfoHtml.Append("No");
                                }
                            }
                            else
                            {
                                sbAdditionalInfoHtml.Append(optionsItem.value);
                            }
                            sbAdditionalInfoHtml.Append("</td>");
                        }
                        else
                        {
                            sbAdditionalInfoHtml.Append("<td width='15%' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            sbAdditionalInfoHtml.Append("<strong>" + optionsItem.label + ":</strong></td>");
                            sbAdditionalInfoHtml.Append("<td width='30%' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                            if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("datetime"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("time")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("select")
                                && !optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("HH:mm tt"));
                            }
                            else if (optionsItem.label.Replace(" ", "").Replace("/", "").ToLower().Contains("date"))
                            {
                                sbAdditionalInfoHtml.Append(Convert.ToDateTime(optionsItem.value).ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                sbAdditionalInfoHtml.Append(optionsItem.value);
                            }
                            sbAdditionalInfoHtml.Append("</td>");
                            sbAdditionalInfoHtml.Append("</tr>");
                        }
                        if (optionsItemIndex % 2 == 0 && bookingResponse.bookingData.options.Count == (optionsItemIndex + 1))
                        {
                            sbAdditionalInfoHtml.Append("<td width='45%' colspan='2' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'></td>");
                            sbAdditionalInfoHtml.Append("</tr>");
                        }
                        optionsItemIndex++;
                    }
                }
                else
                {
                    sbAdditionalInfoHtml.Append("<tr>");
                    sbAdditionalInfoHtml.Append("<td width='100%' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>No additional info available.</td>");
                    sbAdditionalInfoHtml.Append("</tr>");
                }
                StringBuilder sbPickup_MeetingPointInformationHtml = new StringBuilder();
                if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingTime)
                   || !string.IsNullOrEmpty(bookingResponse.bookingData.meetingAddress)
                   || !string.IsNullOrEmpty(bookingResponse.bookingData.meetingLocation))
                {
                    if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingTime))
                    {
                        sbPickup_MeetingPointInformationHtml.Append("<tr>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                        sbPickup_MeetingPointInformationHtml.Append("<strong>Time:</strong>");
                        sbPickup_MeetingPointInformationHtml.Append("</td>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='613' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>" + bookingResponse.bookingData.meetingTime + "</td>");
                        sbPickup_MeetingPointInformationHtml.Append("</tr>");
                    }
                    if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingAddress))
                    {
                        sbPickup_MeetingPointInformationHtml.Append("<tr>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                        sbPickup_MeetingPointInformationHtml.Append("<strong>Address:</strong>");
                        sbPickup_MeetingPointInformationHtml.Append("</td>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='613' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>" + bookingResponse.bookingData.meetingAddress + "</td>");
                        sbPickup_MeetingPointInformationHtml.Append("</tr>");
                    }
                    if (!string.IsNullOrEmpty(bookingResponse.bookingData.meetingLocation))
                    {
                        sbPickup_MeetingPointInformationHtml.Append("<tr>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                        sbPickup_MeetingPointInformationHtml.Append("<strong>Location:</strong>");
                        sbPickup_MeetingPointInformationHtml.Append("</td>");
                        sbPickup_MeetingPointInformationHtml.Append("<td width='613' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>" + bookingResponse.bookingData.meetingLocation + "</td>");
                        sbPickup_MeetingPointInformationHtml.Append("</tr>");
                    }
                }
                else
                {
                    sbPickup_MeetingPointInformationHtml.Append("<tr>");
                    sbPickup_MeetingPointInformationHtml.Append("<td width='115' height='35' valign='middle' bgcolor='#FFFFFF' style='font-family: arial; font-size: 13px'>");
                    sbPickup_MeetingPointInformationHtml.Append("No pickup/meeting point information available.");
                    sbPickup_MeetingPointInformationHtml.Append("</td>");
                    sbPickup_MeetingPointInformationHtml.Append("</tr>");
                }
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string PGTranPct = Convert.ToString(ConfigurationManager.AppSettings["PGTranPct"]);
                string WebsiteUrl = Convert.ToString(ConfigurationManager.AppSettings["WebsiteUrl"]);

                dynamic dynamicCls = new System.Dynamic.ExpandoObject();
                string lsrtTemplateLangCode = "";

                dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList[0].RelationReference);
                dynamicCls.LastName = lobjMemberDetails.FullName;
                dynamicCls.program_id = lobjMemberDetails.ProgramId;
                dynamicCls.to_email = lobjMemberDetails.Email;
                dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
                dynamicCls.program_id = lobjMemberDetails.ProgramId;
                dynamicCls.BookingUUId = bookingResponse.bookingData.uuid;
                dynamicCls.BookingCode = bookingResponse.bookingData.code;
                dynamicCls.ProductName = bookingResponse.bookingData.prodtitle;
                dynamicCls.Option = bookingResponse.bookingData.productTypeTitle;
                dynamicCls.Address = bookingResponse.bookingData.prodavailaddress;
                dynamicCls.status = bookingResponse.bookingData.status;
                dynamicCls.BookingDate = (DateTime.Parse(bookingResponse.bookingData.createdAt).ToString("dd MMM yyyy"));//.ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
                dynamicCls.ArrivalDate = (DateTime.Parse(bookingResponse.bookingData.arrivalDate).ToString("dd MMM yyyy"));
                dynamicCls.Timeslot = (string.IsNullOrEmpty(bookingResponse.bookingData.timeSlot) ? "N/A" : string.Format("{0} hrs", bookingResponse.bookingData.timeSlot));
                dynamicCls.Adult = (bookingResponse.bookingData.adults > 0 ? string.Format("{0} x {1}", bookingResponse.bookingData.adults,
                                    FormatCurrency(Math.Ceiling(bookingResponse.bookingData.amountBreakdown.FindAll(x => x.name.ToLower().Equals("adult")).FirstOrDefault().convertedAmount)
                                    , bookingResponse.bookingData.convertedCurrency)) : "0");
                dynamicCls.Children = (bookingResponse.bookingData.children > 0 ? string.Format("{0} x {1}", bookingResponse.bookingData.children,
                                    FormatCurrency(Math.Ceiling(bookingResponse.bookingData.amountBreakdown.FindAll(x => x.name.ToLower().Equals("child")).FirstOrDefault().convertedAmount)
                                    , bookingResponse.bookingData.convertedCurrency)) : "0");
                dynamicCls.Seniors = (bookingResponse.bookingData.seniors > 0 ? string.Format("{0} x {1}", bookingResponse.bookingData.seniors,
                                    FormatCurrency(Math.Ceiling(bookingResponse.bookingData.amountBreakdown.FindAll(x => x.name.ToLower().Equals("senior")).FirstOrDefault().convertedAmount)
                                    , bookingResponse.bookingData.convertedCurrency)) : "0");
                dynamicCls.TotalPrice = (FormatCurrency(Math.Ceiling(bookingResponse.bookingData.grandTotalAmount), bookingResponse.bookingData.convertedCurrency));
                dynamicCls.PGCharge = (FormatCurrency(bookingResponse.bookingData.grandTotalAmount * (Convert.ToDecimal(PGTranPct) / 100), bookingResponse.bookingData.convertedCurrency, true));
                dynamicCls.TotalPaid = (FormatCurrency(bookingResponse.bookingData.grandTotalAmount + (bookingResponse.bookingData.grandTotalAmount * (Convert.ToDecimal(PGTranPct) / 100)), bookingResponse.bookingData.convertedCurrency, true));
                dynamicCls.AdditionalInfo = (sbPickup_MeetingPointInformationHtml.ToString());
                dynamicCls.CancellationPolicy = bookingResponse.bookingData.cancellationPolicySummary;
                dynamicCls.MeetingPoint = sbPickup_MeetingPointInformationHtml.ToString();
                if (bookingResponse.bookingData.status.ToLower().Equals("success"))
                {
                    if (lobjMemberDetails.PreferredLanguage == "EN")
                    {
                        dynamicCls.event_name = "package_booking";
                    }
                    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                        dynamicCls.event_name = "package_booking";
                    }
                }
                else
                {
                    if (lobjMemberDetails.PreferredLanguage == "EN")
                    {
                        dynamicCls.event_name = "package_booking";
                    }
                    if (lobjMemberDetails.PreferredLanguage.ToUpper() != "EN")
                    {
                        lsrtTemplateLangCode = lobjMemberDetails.PreferredLanguage.ToUpper();
                        dynamicCls.event_name = "package_booking";
                    }
                    // dynamicCls.Status = "Failed";
                }
                Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
                IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
                foreach (var key in dict)
                {
                    lobjDictionary.Add(key.Key, key.Value);
                }
                string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
                bool Email = lobjModel.SendEmails(jsonParameters, lobjMemberDetails);




                if (Email)
                {
                    LoggingAdapter.WriteLog("PaymentResponse Page Experience Email Send Successfully");
                }
                else
                {
                    LoggingAdapter.WriteLog("PaymentResponse Page Experience Email Send Failed");
                }
            }
            else
            {
                #region Logging
                LoggingAdapter.WriteLog(
                string.Format("PaymentResponse Experiences customerEmailId is null; Date - {0} | BookingCode - {1}" +
                " | BookingUUID - {2};",
                DateTime.Now, bookingResponse.bookingData.code, bookingResponse.bookingData.uuid));
                #endregion
            }
        }
        catch (Exception ex)
        {
            #region Logging
            LoggingAdapter.WriteLog(
            string.Format("PaymentResponse Experiences InsertEmailDetails Exception; Date - {0} | EX Message - {1} | EX StackTrace - {2} | EX InnerException - {3};",
            DateTime.Now, ex.Message, ex.StackTrace, ex.InnerException));
            #endregion
        }
    }
    public static string FormatCurrency(decimal decValue, string currencyCode, bool requiredDecimal = false)
    {
        NumberFormatInfo nfo = new CultureInfo("en-US", false).NumberFormat;
        if (requiredDecimal)
        {
            nfo.NumberDecimalDigits = 2;
            if (!string.IsNullOrEmpty(currencyCode))
            {
                return string.Format("{0} {1}", currencyCode, decValue.ToString("N", nfo));
            }
            else
            {
                return string.Format("{0}", decValue.ToString("N", nfo));
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(currencyCode))
            {
                return string.Format("{0} {1}", currencyCode, decValue.ToString("N", nfo).Split('.')[0]);
            }
            else
            {
                return string.Format("{0}", decValue.ToString("N", nfo).Split('.')[0]);
            }
        }
    }

    //[WebMethod]
    //public static bool InsurancePaymentRequest()
    //{
    //    string lstrResponse = string.Empty;
    //    bool lblnResult = false;
    //    ABCModel lobjModel = new ABCModel();
    //    InsurancePaymentRequestResponse lobjPaymentResponse = new InsurancePaymentRequestResponse();
    //    ShopModel shopModel = new ShopModel();
    //    try
    //    {

    //        LoggingAdapter.WriteLog("Booking Insurance");
    //        InsuranceUserDetailsResponse lobjUserdetails = HttpContext.Current.Session["InsuranceUserDetails"] as InsuranceUserDetailsResponse;

    //        MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
    //        string serviceCode = Convert.ToString(HttpContext.Current.Session["InsuranceServiceCode"]);
    //        InsuranceServiceProvidersResponse lobjInsuranceServiceProviders = HttpContext.Current.Application["SearchInsuranceProducts"] as InsuranceServiceProvidersResponse;
    //        if (lobjMemberDetails != null)
    //        {
    //            if (lobjUserdetails != null)
    //            {
    //                LoggingAdapter.WriteLog("Calling RedeemPoints");
    //                string lstrProgramName = ProgramHelper.ProgramName();
    //                ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);
    //                string lstrCurrency = lobjModel.GetDefaultCurrency();
    //                int lintTotalPrice = lobjModel.ConvertToPoints(float.Parse(lobjUserdetails.results.Amount.ToString())
    //                    , lstrCurrency, lobjProgramDefinition.ProgramId, "INSURANCE");
    //                string lstrRedeemResponse = lobjModel.RedeemPoints(float.Parse(lobjUserdetails.results.Amount.ToString()),
    //                    lintTotalPrice,
    //                    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
    //                    lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
    //                    lobjInsuranceServiceProviders.results.Find(x => x.ServiceCode == serviceCode.ToString()).ServiceName, (int)LoyaltyTxnType.Insurance, lstrCurrency, "");
    //                LoggingAdapter.WriteLog("RedeemPointsforInsurance success - '" + lstrRedeemResponse + "'");
    //                if (!string.IsNullOrEmpty(lstrRedeemResponse))
    //                {
    //                    try
    //                    {

    //                        string MembershipReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(Core.Platform.Member.Entites.RelationType.LBMS)).RelationReference;
    //                        switch (lobjUserdetails.results.Key)
    //                        {
    //                            case "Nepal Insurance":
    //                            case "Himalayan Insurance":
    //                            case "Surya Life Insurance":
    //                                lobjPaymentResponse = lobjModel.InsurancePaymentRequest(serviceCode, lobjUserdetails.results.Amount, lobjUserdetails.results.SessionId, HttpContext.Current.Session["InsurancePolicyNo"].ToString(), "", lobjUserdetails.results.Key, MembershipReference, lobjUserdetails.results.CustomerName);
    //                                break;
    //                            case "Reliance Insurance":
    //                                lobjPaymentResponse = lobjModel.InsurancePaymentRequest(serviceCode, lobjUserdetails.results.Amount, 0, HttpContext.Current.Session["InsurancePolicyNo"].ToString(), lobjUserdetails.results.TransactionId, lobjUserdetails.results.Key, MembershipReference, lobjUserdetails.results.CustomerName);
    //                                break;
    //                            case "NLG Insurance":
    //                                lobjPaymentResponse = lobjModel.InsurancePaymentRequest(serviceCode, lobjUserdetails.results.Amount, 0, HttpContext.Current.Session["InsurancePolicyNo"].ToString(), "", lobjUserdetails.results.Key, MembershipReference, lobjUserdetails.results.CustomerName);
    //                                break;
    //                            default:
    //                                lobjPaymentResponse = null;
    //                                break;
    //                        }

    //                        if (lobjPaymentResponse != null)
    //                        {
    //                            if (lobjPaymentResponse.results.Status)
    //                            {
    //                                if (lobjPaymentResponse.results.State.ToLower() == "success")
    //                                {
    //                                    HttpContext.Current.Session["InsuranceBookingId"] = lobjPaymentResponse.results.Id;
    //                                    lstrResponse = JsonConvert.SerializeObject(lobjPaymentResponse.results);
    //                                    dynamic dynamicCls = new System.Dynamic.ExpandoObject();
    //                                    dynamicCls.event_name = "Insurance_Booked";
    //                                    dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
    //                                    dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
    //                                    dynamicCls.to_email = lobjMemberDetails.Email;
    //                                    dynamicCls.full_name = lobjMemberDetails.FullName;
    //                                    dynamicCls.serviceName = lobjUserdetails.results.Key;
    //                                    dynamicCls.CustomerName = lobjUserdetails.results.CustomerName;
    //                                    dynamicCls.PolicyNo = HttpContext.Current.Session["InsurancePolicyNo"].ToString();
    //                                    dynamicCls.ReferenceId = lobjPaymentResponse.results.ReferenceId;
    //                                    //dynamicCls.CreditsConsumed = lobjModel.FloatToThousandSeperated(float.Parse(lobjUserdetails.results.Amount.ToString())) + " MUR";
    //                                    dynamicCls.Points = lobjModel.FloatToThousandSeperated(lintTotalPrice) + " Points";
    //                                    dynamicCls.TransactionDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt");
    //                                    dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
    //                                    Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
    //                                    IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
    //                                    foreach (var key in dict)
    //                                    {
    //                                        lobjDictionary.Add(key.Key, key.Value);
    //                                    }
    //                                    string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
    //                                    lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
    //                                    lblnResult = true;
    //                                }
    //                                else
    //                                {
    //                                    LoggingAdapter.WriteLog("InsurancePaymentRequest Fail");
    //                                    bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
    //                                    LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
    //                                    lblnResult = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                lstrResponse = lobjPaymentResponse.results.Message;
    //                                bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
    //                                LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
    //                                lblnResult = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
    //                            LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
    //                            lblnResult = false;
    //                        }

    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        LoggingAdapter.WriteLog("InsurancePaymentRequest Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
    //                        bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjInsuranceServiceProviders.results.Select(x => x.ServiceName).ToString());
    //                        LoggingAdapter.WriteLog("InsurancePaymentRequest Ex Rollback Success");
    //                        lblnResult = false;
    //                    }
    //                }
    //                HttpContext.Current.Session["AvailablePoints"] = null;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LoggingAdapter.WriteLog("PointGateway.aspx InsurancePaymentRequest Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
    //        return false;
    //    }
    //    lobjModel.LogActivity("Booking Insurance Status:" + lobjPaymentResponse.results.Status, ActivityType.Insurance);
    //    return lblnResult;
    //}

    //[WebMethod]
    //public static bool BookForKhaltiFlight()
    //{
    //    ABCModel lobjModel = new ABCModel();
    //    bool Result = false;
    //    MemberDetails lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
    //    CreateDomesticBookingResponse lobjBookingDetailsResponse = new CreateDomesticBookingResponse();
    //    lobjBookingDetailsResponse = HttpContext.Current.Session["DomesticFlightBookingResponse"] as CreateDomesticBookingResponse;
    //    string lstrRedeemResponse = string.Empty;
    //    try
    //    {
    //        LoggingAdapter.WriteLog("Booking Flight for Khalti");
    //        ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
    //        if (lobjProgramDefinition != null)
    //        {
    //            if (HttpContext.Current.Session["DomesticFlightBookingRequest"] != null && HttpContext.Current.Session["MemberDetails"] != null && HttpContext.Current.Session["DomesticFlightBookingResponse"] != null)
    //            {
    //                lobjModel.LogActivity(string.Format("Book Flight Process"), ActivityType.FlightBookingForDomestic);
    //                BookingStatusRequestForDomestic lobjBookingRequest = new BookingStatusRequestForDomestic();
    //                lobjBookingRequest.Token = ConfigurationManager.AppSettings["KhaltiIBEDomesticFlightToken"].ToString();
    //                lobjBookingRequest.Reference = HttpContext.Current.Session["KhaltiFlightBookingReferenceId"].ToString();
    //                string lstrCurrency = lobjModel.GetDefaultCurrency();
    //                lobjBookingRequest.PointRate = lobjModel.GetProgramRedemptionRate(lstrCurrency, RedemptionCodeKeys.AIR.ToString(), lobjProgramDefinition.ProgramId);

    //                LoggingAdapter.WriteLog("Calling RedeemPoints");
    //                string lstrProgramName = ProgramHelper.ProgramName();
    //                lobjProgramDefinition = lobjModel.GetProgramDetails(lstrProgramName);

    //                float lfltPointRate = 0.0f;

    //                List<ProgramCurrencyDefinition> lobjProgramCurrency = lobjModel.GetProgramCurrencyDefinition(lobjMemberDetails.ProgramId);
    //                lfltPointRate = lobjProgramCurrency[0].RedemptionRate;
    //                float ldblAmount = Convert.ToInt32(Math.Ceiling(float.Parse(lobjBookingDetailsResponse.CreditsConsumed.ToString()))) * lfltPointRate;

    //                lstrRedeemResponse = lobjModel.RedeemPoints(ldblAmount, Int32.Parse(lobjBookingDetailsResponse.CreditsConsumed.ToString()),
    //                   lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference,
    //                   lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).WebPassword,
    //                   lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo,
    //                   Convert.ToInt32(LoyaltyTxnType.Air),
    //                   lstrCurrency, "");
    //                LoggingAdapter.WriteLog("RedeemPointsforDomesticFlight success - '" + lstrRedeemResponse + "'");
    //                if (!string.IsNullOrEmpty(lstrRedeemResponse))
    //                {
    //                    try
    //                    {
    //                        BookingStatusResponseForDomestic lobjBookingResponse = lobjModel.BookForKhaltiFlight(lobjBookingRequest);
    //                        HttpContext.Current.Session["BookingStatusResponseForDomestic"] = lobjBookingResponse;
    //                        if (lobjBookingResponse.Status && !string.IsNullOrEmpty(lobjBookingResponse.Detail.Outbound.Pnrno))
    //                        {
    //                            HttpContext.Current.Session["KhaltiFlightBookingReferenceId"] = null;
    //                            HttpContext.Current.Session["FlightSearchDetailsForDomestic"] = null;
    //                            HttpContext.Current.Session["FlightsForDomestic"] = null;
    //                            HttpContext.Current.Session["DomesticFlightBookingId"] = null;
    //                            HttpContext.Current.Session["DomesticInboundFlights"] = null;
    //                            HttpContext.Current.Session["DomesticOutboundFlights"] = null;
    //                            Result = true;

    //                            //communication Engine call for Email send
    //                            string strPaxInfo = "";
    //                            strPaxInfo += "<table cellpadding='0' cellspacing='0' width='100 %' border='0'><tr>";
    //                            strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='15%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'> Title </td>";
    //                            strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='40%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Passenger Name</td>";
    //                            strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='22%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Ticket No.</td>";
    //                            strPaxInfo += "<td align='left' valign='top' bgcolor='#dd2625' width='12%' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: normal; text-transform: capitalize; text-align: left; color: #ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Gender</td>";
    //                            strPaxInfo += "</tr>";
    //                            List<CB.IBE.DomesticFlight.Entities.Passengers> lobjListOfPassengerDetails = new List<CB.IBE.DomesticFlight.Entities.Passengers>();
    //                            if (lobjBookingDetailsResponse != null)
    //                            {
    //                                string lstrPaxtype = "";
    //                                lobjListOfPassengerDetails = lobjBookingDetailsResponse.Passengers;
    //                                for (int k = 0; k < lobjListOfPassengerDetails.Count; k++)
    //                                {
    //                                    strPaxInfo += "<tr>";
    //                                    strPaxInfo += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'> " + lobjListOfPassengerDetails[k].Title + " </td>";
    //                                    strPaxInfo += "<td align='left' valign='top' width='40%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + UppercaseFirst(lobjListOfPassengerDetails[k].Lastname) + " " + UppercaseFirst(lobjListOfPassengerDetails[k].Firstname) + "</td>";
    //                                    strPaxInfo += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjListOfPassengerDetails[k].TicketNo + " </td>";
    //                                    strPaxInfo += "<td align='left' valign='top' width='12%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjListOfPassengerDetails[k].Gender + " </td>";
    //                                    strPaxInfo += "</tr>";
    //                                }

    //                                strPaxInfo += "</table>";
    //                            }

    //                            // Code for Departure table
    //                            BookedFlightDetails lobjFlightSegmentlst = new BookedFlightDetails();
    //                            lobjFlightSegmentlst = lobjBookingDetailsResponse.Outbound;
    //                            string strDepartute = "";
    //                            strDepartute += "<tr>";
    //                            strDepartute += "<td align='left' bgcolor='#dd2625' width='10%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Flight</td>";
    //                            strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Departure</td>";
    //                            strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrival</td>";
    //                            strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Depart Time</td>";
    //                            strDepartute += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrial Time</td>";
    //                            strDepartute += "</tr>";

    //                            strDepartute += "<tr>";
    //                            strDepartute += "<td align='center' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst.Flightno + "</td>";
    //                            strDepartute += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorFrom + "</td>";
    //                            strDepartute += "<td align='left' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorTo + "</td>";
    //                            strDepartute += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjBookingDetailsResponse.FlightDate).ToString("dd/MM/yyyy") + "<br/>" + Convert.ToDateTime(lobjFlightSegmentlst.DepartureTime).ToString("HH:mm") + "</td>";
    //                            strDepartute += "<td align='left' valign='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjFlightSegmentlst.ArrivalTime).ToString("HH:mm") + "</td>";
    //                            strDepartute += "</tr>";
    //                            //For AirLine PNR
    //                            strDepartute += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
    //                            strDepartute += "<td width='50%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px;'> PNR:&nbsp;" + lobjFlightSegmentlst.Pnrno + "</td>";
    //                            strDepartute += "<tr></table></td></tr>";

    //                            lobjFlightSegmentlst = lobjBookingDetailsResponse.Inbound;
    //                            string strReturn = "";
    //                            string strArrival = "";
    //                            string InboundFlightId = lobjBookingResponse.Detail.InboundFlightId;
    //                            if (!string.IsNullOrEmpty(InboundFlightId))
    //                            {
    //                                // Code for Arrival Table
    //                                strReturn += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
    //                                strReturn += "<td width='50%' height='25' valign='top' style='font-family: Arial; font-size: 13px; letter-spacing: normal; line-height: 18px; font-weight: bold; text-align: left; color: #dd2625; padding: 0px;'>Itinerary Details <span style='color: #231f20;'>(Return)</span></td>";
    //                                strReturn += "<tr></table></td></tr>";
    //                                // Arrival Header Row
    //                                strReturn += "<tr>";
    //                                strReturn += "<td align='left' bgcolor='#dd2625' width='10%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Flight</td>";
    //                                strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Departure</td>";
    //                                strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='22%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrival</td>";
    //                                strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Depart Time</td>";
    //                                strReturn += "<td align='left' bgcolor='#dd2625' valign='top' width='14%' style='font-family:Arial; font-size:12px; color:#ffffff; padding: 10px; border: 1px solid #dddddd; border-bottom: 1px solid #dddddd;'>Arrial Time</td>";
    //                                strReturn += "</tr>";

    //                                strArrival += "<tr>";
    //                                strArrival += "<td width='15%' align='center' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjFlightSegmentlst.Flightno + "</td>";
    //                                strArrival += "<td align='left' valin='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorTo + "</td>";
    //                                strArrival += "<td align='left' valin='top' width='22%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + lobjBookingDetailsResponse.SectorFrom + "</td>";
    //                                strArrival += "<td align='left' valin='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjFlightSegmentlst.DepartureTime).ToString("HH:mm") + "</td>";
    //                                strArrival += "<td align='left' valin='top' width='15%' style='font-family:Arial; font-size:12px; color:#231f20;padding: 10px; border: 1px solid #dddddd; border-bottom: 2px solid #dddddd;'>" + Convert.ToDateTime(lobjFlightSegmentlst.ArrivalTime).ToString("HH:mm") + "</td>";
    //                                strArrival += "</tr>";

    //                                //For Arrival AirLine PNR
    //                                strArrival += "<tr><td colspan='6'><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
    //                                strArrival += "<td width='50%' style='font-family:Arial; font-size:12px; color:#231f20; padding: 10px 0px;'> PNR:&nbsp;" + lobjFlightSegmentlst.Pnrno + "</td>";
    //                                strArrival += "<tr></table></td></tr>";
    //                            }
    //                            dynamic dynamicCls = new System.Dynamic.ExpandoObject();
    //                            dynamicCls.event_name = "Domestic_Flight_Booked";
    //                            dynamicCls.relation_reference = Convert.ToString(lobjMemberDetails.MemberRelationsList.Find(lobj => lobj.RelationType.Equals(RelationType.LBMS)).RelationReference);
    //                            dynamicCls.program_id = Convert.ToInt32(lobjMemberDetails.ProgramId); ;
    //                            dynamicCls.to_email = lobjMemberDetails.Email;
    //                            dynamicCls.full_name = lobjMemberDetails.FullName;
    //                            dynamicCls.TransactionReferenceCode = lobjBookingResponse.Detail.Reference;
    //                            dynamicCls.PaymentDetails = lobjModel.FloatToThousandSeperated(Convert.ToSingle(lobjBookingDetailsResponse.CreditsConsumed)) + " Points";
    //                            dynamicCls.TblPassengerInfo = strPaxInfo;
    //                            dynamicCls.TblDeparture = strDepartute;
    //                            dynamicCls.ReturnFlight = strReturn;
    //                            dynamicCls.TblArrival = strArrival;
    //                            dynamicCls.to_mobile = lobjMemberDetails.MobileNumber;
    //                            dynamicCls.CreditsConsumed = lobjModel.FloatToThousandSeperated(Convert.ToSingle(lobjBookingDetailsResponse.CreditsConsumed));
    //                            Dictionary<string, dynamic> lobjDictionary = new Dictionary<string, dynamic>();
    //                            IDictionary<string, object> dict = (IDictionary<string, object>)dynamicCls;
    //                            foreach (var key in dict)
    //                            {
    //                                lobjDictionary.Add(key.Key, key.Value);
    //                            }
    //                            string jsonParameters = JsonConvert.SerializeObject(lobjDictionary);
    //                            lobjModel.SendEmails(jsonParameters, lobjMemberDetails);
    //                        }
    //                        else
    //                        {
    //                            bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo.ToString());
    //                            LoggingAdapter.WriteLog("BookForKhaltiFlight Ex Rollback Success");
    //                            Result = false;
    //                        }
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        LoggingAdapter.WriteLog("BookForKhaltiFlight Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
    //                        bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo.ToString());
    //                        LoggingAdapter.WriteLog("BookForKhaltiFlight Ex Rollback Success");
    //                        Result = false;
    //                    }
    //                }
    //                HttpContext.Current.Session["AvailablePoints"] = null;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        LoggingAdapter.WriteLog("BookForKhaltiFlight Ex- " + ex.StackTrace + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
    //        bool lboolRollBackResponse = lobjModel.RollBackTransaction(lstrRedeemResponse, lobjMemberDetails.MemberRelationsList.Find(lob => lob.RelationType.Equals(RelationType.LBMS)).RelationReference, lobjBookingDetailsResponse.SectorFrom + "-" + lobjBookingDetailsResponse.SectorTo.ToString());
    //        LoggingAdapter.WriteLog("BookForKhaltiFlight Ex Rollback Success");
    //        LoggingAdapter.WriteLog("PointGateway.aspx BookForKhaltiFlight Exception: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
    //        Result = false;
    //    }
    //    lobjModel.LogActivity(string.Format("Book Flight Process") + "Status:" + Result, ActivityType.FlightBookingForDomestic);
    //    return Result;
    //}

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}