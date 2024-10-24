using CB.IBE.Platform.Car.Entities;
using Core.Platform.MemberActivity.Entities;
using Framework.EnterpriseLibrary.Adapters;
using Framework.Integrations.Hotels.Entities;
using ABC.Model;
using IBEAPI.ClientEntities;
using IBEAPIGateway.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rate = IBEAPI.ClientEntities.Rate;
using Core.Platform.ProgramMaster.Entities;

public partial class CarDetails : System.Web.UI.Page
{
    public static string EnjoyTraveldisplayCurrency = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTraveldisplayCurrency"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["MemberDetails"] != null &&
        if (Session["MemberDetails"] != null && Session["CarSearchRequest"] != null && Session["Cars"] != null)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("Index.aspx", false);
        }
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] GetCarDetails(string pstruniqueRefNo)
    {
        string[] lobjListOfData = new string[8];
        AvailabilityResponse lobjCarAvailabilityResponse = null;
        GetAvailabilityRequest lobjCarSearchRequest = null;
        RateRequest lobjRateRequest = new RateRequest();
        CarBookingData lobjCarBookingDetails = new CarBookingData();
        RateResponse lobjRateResponse = null;
        IBEAPIModel lobjAPImodel = new IBEAPIModel();
        ABCModel lobjmodel = new ABCModel();
        Rate lobjVehicle = new Rate();
        string lstrPaymentType = string.Empty;
        string lstrAircon = string.Empty;
        StringBuilder sb = new StringBuilder();
        StringBuilder sbextras = new StringBuilder();
        StringBuilder sbcardetails = new StringBuilder();
        StringBuilder sbAdditionalEquipment = new StringBuilder();
        StringBuilder sbmoreInfo = new StringBuilder();
        double TotalAmount = 0.0f;
        ProgramDefinition lobjProgramDefinition = lobjmodel.GetProgramMaster();
        List<ProgramCurrencyDefinition> lobjProgramCurrencyDefinition = lobjmodel.GetProgramCurrencyDefinition(lobjProgramDefinition.ProgramId);
        var PointRate = lobjProgramCurrencyDefinition[0].RedemptionRate;

        try
        {
            if (HttpContext.Current.Session["Cars"] != null)
            {
                lobjCarAvailabilityResponse = HttpContext.Current.Session["Cars"] as AvailabilityResponse;
            }
            if (HttpContext.Current.Session["CarSearchRequest"] != null)
            {
                lobjCarSearchRequest = HttpContext.Current.Session["CarSearchRequest"] as GetAvailabilityRequest;
            }

            if (lobjCarAvailabilityResponse != null)
            {
                lobjVehicle = lobjCarAvailabilityResponse.data.rates.Find(x => x.vehicle.uniqueRef.Equals(pstruniqueRefNo));

                if (lobjVehicle != null)
                {
                    lobjmodel.LogActivity(string.Format("Car selected for booking; Car Name-:{0}; Car Id-:{1}", lobjVehicle.vehicle.name, lobjVehicle.vehicle.uniqueRef), ActivityType.CarBooking);
                    HttpContext.Current.Session["SelectedCar"] = lobjVehicle;

                    lobjCarBookingDetails.CaruniqueRef = pstruniqueRefNo;

                    //Car Details Left Pannel
                    StringBuilder sbinclusions = new StringBuilder();
                    foreach (var inclusions in lobjVehicle.packages[0].inclusions)
                    {
                        sbinclusions.Append("<div class=\"col-12 col-md-6 d-flex align-items-center mb-3\">");
                        sbinclusions.Append("<i class=\"fa-solid fa-check mx-2 h7\"></i>");
                        sbinclusions.Append("<p class=\"h7\"> " + inclusions.name + " </p>");
                        sbinclusions.Append("</div>");
                       
                    }

                    sbcardetails.Append("<div class=\"card-header p-0\" id=\"headingTwo\">");
                    sbcardetails.Append("<h2 class=\"mb-0\">");
                    sbcardetails.Append("<button class=\"btn btn-block text-left p-3 collapsed\" type=\"button\" data-toggle=\"collapse\" data-target=\"#collapseTwo\" aria-expanded=\"false\" aria-controls=\"collapseTwo\">");
                    sbcardetails.Append("<span class=\"heading-bold h6\">" + lobjVehicle.vehicle.name + "</span" + "<span> (Similar)</span > <span class=\"arrow-icon\"> <i class=\"fa fa-caret-up\" aria-hidden=\"true\"></i> </span>");
                    sbcardetails.Append("</button></h2></div>");
                    sbcardetails.Append("<div id=\"collapseTwo\" class=\"collapse\" aria-labelledby=\"headingTwo\" data-parent=\"#accordionExample\">");
                    sbcardetails.Append("<div class=\"card-body p-3\">");
                    sbcardetails.Append("<div class=\"row\">");
                    sbcardetails.Append("<div class=\"col-12\">");                    
                    sbcardetails.Append("<div class=\"dvCarImage\" data-toggle=\"modal\" data-target=\"#dvMoreInfoModal\">");
                    sbcardetails.Append("<img class=\"img-fluid mt-auto mb-auto\" src=" + lobjVehicle.vehicle.images[0].url + " />");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<div class=\"btn btn-two w-100 my-3\" data-toggle=\"modal\" data-target=\"#dvMoreInfoModal\">");
                    sbcardetails.Append("More info");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<div class=\"dvProvider text-center\">");
                    sbcardetails.Append("<img class=\"img-fluid mt-auto mb-auto\" src=\"images/logos/giift-logo-blue.svg\" width=\"50\" />");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<div class=\"col-12 mt-4\">");
                    sbcardetails.Append("<div class=\"row\">");
                    sbcardetails.Append(sbinclusions.ToString());
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<div class=\"col-12\">");
                    sbcardetails.Append(" <div class=\"row dvIcons\">");

                    sbcardetails.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbcardetails.Append("<div class=\"\">");
                    sbcardetails.Append("<i class=\"fa-solid fa-couch\"></i>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<span class=\"ml-2 h7\">x " + lobjVehicle.vehicle.seats + "</span>");
                    sbcardetails.Append("</div>");

                    sbcardetails.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbcardetails.Append("<div class=\"\">");
                    sbcardetails.Append("<i class=\"fa-solid fa-life-ring\"></i>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<span class=\"ml-2 h7\"> " + lobjVehicle.vehicle.transmission + "</span>");
                    sbcardetails.Append("</div>");

                    sbcardetails.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbcardetails.Append("<div class=\"\">");
                    sbcardetails.Append("<i class=\"fa-solid fa-door-closed\"></i>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<span class=\"ml-2 h7\"> " + lobjVehicle.vehicle.doors + "</span>");
                    sbcardetails.Append("</div>");

                    if (lobjVehicle.vehicle.airco)
                    {
                        lstrAircon = "AirCon";
                    }
                    else
                    {
                        lstrAircon = "Non AirCon";
                    }

                    sbcardetails.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbcardetails.Append("<div class=\"\">");
                    sbcardetails.Append("<i class=\"fa-solid fa-snowflake\"></i>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<span class=\"ml-2 h7\">" + lstrAircon + "</span>");
                    sbcardetails.Append("</div>");

                    sbcardetails.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbcardetails.Append("<div class=\"\">");
                    sbcardetails.Append("<i class=\"fa-solid fa-gas-pump\"></i>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("<span class=\"ml-2 h7\">Fair Fuel Policy</span>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");
                    sbcardetails.Append("</div>");

                    //Car Details Left Pannel html

                    //Moreinfo popup html

                    sbmoreInfo.Append("<div class=\"mb-2\">");
                    sbmoreInfo.Append("<div class=\"row\">");
                    sbmoreInfo.Append("<div class=\"col-md-5 col-lg-4\">");
                    sbmoreInfo.Append("<div class=\"dvCarImage\">");
                    sbmoreInfo.Append("<img class=\"img-fluid mt-auto mb-auto\" src=" + lobjVehicle.vehicle.images[0].url + " />");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"col-md-7 col-lg-4 mb-3 mb-md-0\">");
                    sbmoreInfo.Append("<div class=\"dvHeading\">");
                    sbmoreInfo.Append("<p class=\"heading6 text-colour1\">" + lobjVehicle.vehicle.name + "</p>");
                    sbmoreInfo.Append("<span class='h7'> or similar(Small) </span>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"row dvIcons\">");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-couch\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\">x " + lobjVehicle.vehicle.seats + "</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-life-ring\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\"> " + lobjVehicle.vehicle.transmission + "</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-door-closed\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\"> " + lobjVehicle.vehicle.doors + "</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-gas-pump\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\">Fair Fuel Policy</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-snowflake\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\">" + lstrAircon + "</span>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    //sbmoreInfo.Append("<div class=\"travelBtn mt-4\">");
                    //sbmoreInfo.Append("<a href=\"CarDetails.aspx?uniqueRefId=" + pstruniqueRefNo + "\" class=\"btn btn-one\">BOOK NOW</a>");
                    //sbmoreInfo.Append("<p>" + lobjVehicle.packages[0].payments.payNow.vehicle.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");payNow.vehicle.display.amount   " + lobjVehicle.packages[0].payments.estimatedTotal.total.display.amount + "
                    //sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"row\">");
                    sbmoreInfo.Append("<div class=\"dvLocation col-12 mt-3\">");
                    sbmoreInfo.Append("<div class=\"d-flex flex-wrap justify-content-between align-items-center border b-radius p-2\">");
                    sbmoreInfo.Append("<div class=\"col-12 col-sm-6 col-md-3 order-md-0\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<img width=\"50\" class=\"img-fluid mt-auto mb-auto\" src=\"images/logos/giift-logo-blue.svg\" alt=\"Logo\">");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"col-12 col-sm-6 col-md-9 mt-2 mt-md-0 order-md-1 text-left\">");
                    sbmoreInfo.Append("<div class=\"\">");
                    sbmoreInfo.Append("<a class=\"link1 text-decoration-none text-colour7\" ><i class=\"fa-solid fa-location-dot\"></i><span> Vehicle location:</span></a>");

                    Branch lobjBranch = lobjCarAvailabilityResponse.data.branches.Find(x => x.id == lobjVehicle.pickUpBranchId);
                    if (lobjBranch != null)
                    {
                        sbmoreInfo.Append("<span class=\"h7 text-colour7 ml-1\"> " + lobjBranch.addressData.line1 + "," + lobjBranch.addressData.line3 + "," + lobjBranch.addressData.postalCode + " </span>");
                    }
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"row\">");
                    sbmoreInfo.Append("<div class=\"col-12 mt-4\">");
                    sbmoreInfo.Append("<div class=\"row\">");

                    sbmoreInfo.Append(sbinclusions.ToString());

                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");

                    //Moreinfo popup html

                    //Car Payment Options html
                    foreach (var PaymentOption in lobjVehicle.packages)
                    {
                        if (PaymentOption.paymentType.ToLower().Equals("fullprepay"))
                        {
                            lobjRateRequest.rateReference = PaymentOption.rateReference;
                            lobjRateRequest.displayCurrency = EnjoyTraveldisplayCurrency;
                            lobjRateRequest.lang = "en-gb";
                            lobjRateRequest.getTerms = false;
                            lobjRateRequest.debugMode = false;
                            lobjRateRequest.pointrate = Convert.ToString(PointRate);

                            lobjRateResponse = lobjAPImodel.GetRates(lobjRateRequest);
                            if (lobjRateResponse != null)
                            {
                                HttpContext.Current.Session["CarRateResponse"] = lobjRateResponse;
                                foreach (var extras in lobjRateResponse.data.package.extras)
                                {
                                    if ((extras.name != "Excess Protection") && (extras.name != "Cancellation Protection (Prepay)"))
                                    {
                                        sbAdditionalEquipment.Append("<div class=\"row my-3 align-items-md-center\">");
                                        sbAdditionalEquipment.Append("<div class=\"col-sm-2\">");
                                        sbAdditionalEquipment.Append("<select class=\"form-control\" name= \"cars\" class=\"\" id = \"" + extras.code + "\" onchange=\"AddAdditionalCharges('" + extras.code + "');\">");
                                        sbAdditionalEquipment.Append("<option value=\"0\"> 0 </option>");
                                        sbAdditionalEquipment.Append("<option value=\"1\"> 1 </option>");
                                        sbAdditionalEquipment.Append("<option value=\"2\"> 2 </option>");
                                        sbAdditionalEquipment.Append("<option value=\"3\"> 3 </option>");
                                        sbAdditionalEquipment.Append("<option value=\"4\"> 4 </option>");
                                        sbAdditionalEquipment.Append("</select>");
                                        sbAdditionalEquipment.Append("</div>");
                                        sbAdditionalEquipment.Append("<div class=\"col-sm-6 col-md-4 mt-2 mt-sm-0\">");
                                        sbAdditionalEquipment.Append("<p class=\"heading-semibold text-colour7\" id=\"spnaditionalchargename_" + extras.code + "\"> " + extras.name + "</p>");
                                        
                                        sbAdditionalEquipment.Append("</div>");
                                        sbAdditionalEquipment.Append("<div class=\"col-sm-4 col-md-3\">");
                                        sbAdditionalEquipment.Append("<p class=\"heading-semibold text-colour7\"> <span id = \"spnaditionalchargeamount_" + extras.code + "\">" + extras.rentalPrice.display.amount + "</span> Points <span class=\"h8 heading-semibold text-colour7\"> (per rental)</span></p>");

                                        //sbAdditionalEquipment.Append("<p><span id = \"spnaditionalchargeamount_" + extras.code + "\">  100 </span> <i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                                        sbAdditionalEquipment.Append("<input type=\"hidden\" name=\"aditionalcharge\" id=\"hndTotaladitionalchargeamount_" + extras.code + "\" value=\"0\" />");
                                        //sbAdditionalEquipment.Append("");
                                        sbAdditionalEquipment.Append("</div>");
                                        sbAdditionalEquipment.Append("<div class=\"col-sm-12 col-md-3 mt-2\">");
                                        sbAdditionalEquipment.Append("<button type=\"button\" class=\"btn btn-two w-100\" onclick=\"ViewMoreInfoAdditionalCharges('" + extras.code + "');\">");
                                        sbAdditionalEquipment.Append("More info");
                                        sbAdditionalEquipment.Append("</button>");
                                        sbAdditionalEquipment.Append("</div>");
                                        sbAdditionalEquipment.Append("</div>");
                                    }
                                }
                            }

                            TotalAmount = PaymentOption.payments.estimatedTotal.total.display.amount;


                            sb.Append("<div class=\"dvPayOption col-12 col-lg-6\">");
                            sb.Append("<div class=\"dvLabel\">");
                            sb.Append("<label class=\"checkbox-container border b-radius mt-3 pr-3\">");
                            sb.Append("<div class=\"col-12 d-flex align-items-center p-3\">");
                            sb.Append("<span class=\"d-inline-block\">");
                            sb.Append("<input type=\"radio\" value=\"" + PaymentOption.rateReference + "\" onchange=\"GetRateDetails('" + PaymentOption.rateReference + "');\" name=\"payment\" id=" + PaymentOption.paymentType + " checked>");
                            sb.Append("<span class=\"checkmark\">");
                            sb.Append("</span>");
                            sb.Append("</span>");
                            //sb.Append("<span class=\"PayCont\">PAY NOW:</span> <span>" + PaymentOption.payments.payNow.vehicle.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></span>");
                            sb.Append("<span class=\"PayCont heading-bold text-colour1 text-uppercase ml-2\">PAY NOW:</span> <span class=\"heading-bold text-colour1 mx-2\">" + PaymentOption.payments.estimatedTotal.total.display.amount + " Points</span>");
                            sb.Append("<span class=\"tickmark\"><i class=\"fa fa-check correctIcon\" id=\"correctIcon\"></i></span>");
                            sb.Append("</div>");
                            sb.Append("<div class=\"col-12 border-top pl-0 py-3\">");
                            //sb.Append("<div class=\"\">");
                            sb.Append("<p><span>Select this option to pay your car hire balance in full..Inc.</span> <strong class=\"heading-bold\">free cancellation</strong> <span>up to 48 hours before pick up!</span></p>");
                            //sb.Append("</div>");
                            sb.Append("</div>");
                            sb.Append("</label>");
                            sb.Append("</div>");
                            sb.Append("</div>");


                            //Mapping Car extas details html//
                            foreach (var extras in PaymentOption.extras)
                            {
                                if (extras.name.Equals("Excess Protection"))
                                {
                                    sbextras.Append("<div class=\"col-12 b-radius mt-4\">");
                                    sbextras.Append("<input type=\"hidden\" name=\"ExessProtection\" id=\"hndExessProtection\" value=" + extras.rentalPrice.display.amount + " />");
                                    sbextras.Append("<input type=\"hidden\" name=\"ExessProtectiondata\" id=\"hndExessProtectiondata\" value=" + extras.productId + "|" + extras.rentalPrice.display.amount + " />");
                                    sbextras.Append("<div class=\"row\">");
                                    sbextras.Append("<div class=\"col-12\">");
                                    sbextras.Append("<div class=\"border b-radius p-3\">");
                                    sbextras.Append("<div class=\"row\">");
                                    sbextras.Append("<div class=\"ribbon\">");
                                    sbextras.Append("<span> RECOMMENDED </span>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("<div class=\"col-12 productInfo position-relative\">");
                                    sbextras.Append("<div class=\"row\">");
                                    //sbextras.Append("<div class=\"col-1 col-md-1 dvShield text-colour8\">");
                                    //sbextras.Append("<i class=\"fa-solid fa-shield-halved\"></i>");
                                    //sbextras.Append("</div>");
                                    sbextras.Append("<div class=\"col-12\">");
                                    /*sbextras.Append("<div class=\"row\">");
                                    sbextras.Append("<div class=\"col-12\">");*/
                                    sbextras.Append("<h2 class=\"heading6 mb-2\"><i class=\"fa-solid fa-shield-halved\"></i><span> " + extras.name + " </span></h2>");
                                    //sbextras.Append("<div class=\"moreInfo mt-md-1\" data-toggle=\"modal\" data-target=\"#dvMoreInfoModal\">");
                                    //sbextras.Append("More info");
                                    //sbextras.Append("</div>");
                                    /*sbextras.Append("</div>");
                                    sbextras.Append("</div>");*/
                                    /*sbextras.Append("<div class=\"row\">");
                                    sbextras.Append("<div class=\"col-12\">");*/
                                    sbextras.Append("<p>" + extras.description + " </p>");
                                    /*sbextras.Append("</div>");
                                    sbextras.Append("</div>");*/
                                    sbextras.Append("<div class=\"row align-items-center mt-3\">");
                                    sbextras.Append("<div class=\"col-6 col-md-7 col-lg-8\">");
                                    /*sbextras.Append("<div class=\"dvPrice\">");*/
                                    //sbextras.Append("<p> " + extras.rentalPrice.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                                    sbextras.Append("<p class=\"heading6 text-colour1\"> " + extras.rentalPrice.display.amount + " Points</p>");
                                    /*sbextras.Append("</div>");*/
                                    sbextras.Append("</div>");
                                    sbextras.Append("<div class=\"col-6 col-md-5 col-lg-4 text-right\">");
                                    /*sbextras.Append("<div class=\"travelBtn\">");*/
                                    sbextras.Append("<button type =\"button\" id=ADD_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('ADD','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + PaymentOption.rateReference + "','" + extras.productId + "');\" class=\"btn btn-one dvAdd\"><span>+ ADD</span></button>");
                                    sbextras.Append("<button type =\"button\" id=REMOVE_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('REMOVE','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + PaymentOption.rateReference + "','" + extras.productId + "');\" class=\"d-none btn btn-one dvRemove\">- REMOVE</button>");
                                    /*sbextras.Append("</div>");*/
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                }
                                else
                                {
                                    sbextras.Append("<div class=\"col-12 b-radius mt-4\">");
                                    sbextras.Append("<div class=\"row\">");
                                    sbextras.Append("<div class=\"col-12\">");
                                    sbextras.Append("<div class=\"border b-radius p-3\">");
                                    sbextras.Append("<div class=\"row\">");
                                    //sbextras.Append("<div class=\"ribbon\">");
                                    //sbextras.Append("<span> RECOMMENDED </span>");
                                    //sbextras.Append("</div>");
                                    sbextras.Append("<div class=\"col-12 productInfo position-relative\">");
                                    sbextras.Append("<div class=\"row\">");
                                    /*sbextras.Append("<div class=\"col-1 col-md-1 dvShield text-colour8\">");
                                    sbextras.Append("<i class=\"fa-solid fa-shield-halved\"></i>");
                                    sbextras.Append("</div>");*/
                                    sbextras.Append("<div class=\"col-12\">");
                                    /*sbextras.Append("<div class=\"row\">");
                                    sbextras.Append("<div class=\"col-12\">");*/
                                    sbextras.Append("<h2 class=\"heading6 mb-2\"><i class=\"fa-solid fa-shield-halved\"></i><span> " + extras.name + " </span></h2>");
                                    //sbextras.Append("<div class=\"moreInfo mt-md-1\" data-toggle=\"modal\" data-target=\"#dvMoreInfoModal\">");
                                    //sbextras.Append("More info");
                                    //sbextras.Append("</div>");
                                    /*sbextras.Append("</div>");
                                    sbextras.Append("</div>");*/
                                    /*sbextras.Append("<div class=\"row pt-2\">");
                                    sbextras.Append("<div class=\"col-12\">");*/
                                    sbextras.Append("<p>" + extras.description + " </p>");
                                    /*sbextras.Append("</div>");
                                    sbextras.Append("</div>");*/
                                    sbextras.Append("<div class=\"row align-items-center mt-3\">");
                                    sbextras.Append("<div class=\"col-6 col-md-7 col-lg-8\">");
                                    /*sbextras.Append("<div class=\"dvPrice\">");*/
                                    //sbextras.Append("<p> " + extras.rentalPrice.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                                    sbextras.Append("<p class=\"heading6 text-colour1\"> " + extras.rentalPrice.display.amount + " Points</p>");
                                    /*sbextras.Append("</div>");*/
                                    sbextras.Append("</div>");
                                    sbextras.Append("<div class=\"col-6 col-md-5 col-lg-4 text-right\">");
                                    /*sbextras.Append("<div class=\"travelBtn\">");*/
                                    sbextras.Append("<button type =\"button\" id=ADD_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('ADD','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + PaymentOption.rateReference + "','" + extras.productId + "');\" class=\"btn btn-one dvAdd\">+ ADD</button>");
                                    sbextras.Append("<button type =\"button\" id=REMOVE_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('REMOVE','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + PaymentOption.rateReference + "','" + extras.productId + "');\" class=\"d-none btn btn-one dvRemove\">- REMOVE</button>");
                                    /*sbextras.Append("</div>");*/
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                    sbextras.Append("</div>");
                                }
                            }
                            //Mapping Car extas details html//
                        }
                    }
                    //Car Payment Options html

                    lobjCarBookingDetails.CarHireAmount = TotalAmount.ToString();
                    lobjCarBookingDetails.PayableAmount = TotalAmount.ToString();
                    HttpContext.Current.Session["CarBookingDetails"] = lobjCarBookingDetails;

                    lobjListOfData[0] = sb.ToString();
                    lobjListOfData[1] = sbextras.ToString();
                    lobjListOfData[2] = JsonConvert.SerializeObject(lobjCarSearchRequest);
                    lobjListOfData[3] = sbcardetails.ToString();
                    lobjListOfData[4] = sbAdditionalEquipment.ToString();
                    lobjListOfData[5] = TotalAmount.ToString();
                    lobjListOfData[6] = JsonConvert.SerializeObject(lobjVehicle);
                    lobjListOfData[7] = sbmoreInfo.ToString();
                }
                else
                {
                    lobjListOfData[0] = "Failed";
                }
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CarDetails.aspx GetCarDetails Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] GetRateDetails(string pstrRateReference)
    {
        string[] lobjListOfData = new string[7];
        RateRequest lobjRateRequest = new RateRequest();
        RateResponse lobjRateResponse = new RateResponse();
        IBEAPIModel lobjAPImodel = new IBEAPIModel();
        ABCModel lobjmodel = new ABCModel();
        StringBuilder sbextras = new StringBuilder();
        StringBuilder sbAdditionalEquipment = new StringBuilder();
        CarBookingData lobjCarBookingDetails = null;
        double TotalAmount = 0.0f;
        try
        {
            if (HttpContext.Current.Session["CarBookingDetails"] != null)
            {
                lobjCarBookingDetails = HttpContext.Current.Session["CarBookingDetails"] as CarBookingData;
                lobjCarBookingDetails.rateReference = pstrRateReference;
            }

            lobjmodel.LogActivity(string.Format("Change Payment Option click; RateReference-:{0};", pstrRateReference), ActivityType.CarBooking);

            lobjRateRequest.rateReference = pstrRateReference;
            lobjRateRequest.displayCurrency = EnjoyTraveldisplayCurrency;
            lobjRateRequest.lang = "en-gb";
            lobjRateRequest.getTerms = false;
            lobjRateRequest.debugMode = false;

            lobjRateResponse = lobjAPImodel.GetRates(lobjRateRequest);

            if (lobjRateResponse != null)
            {
                HttpContext.Current.Session["CarRateResponse"] = lobjRateResponse;

                TotalAmount = lobjRateResponse.data.package.payments.payNow.vehicle.display.amount;
                lobjCarBookingDetails.PayableAmount = TotalAmount.ToString();
                //Mapping Car extas details html//
                foreach (var extras in lobjRateResponse.data.package.extras)
                {
                    if (extras.name.Equals("Excess Protection"))
                    {
                        sbextras.Append("<div class=\"col-12 b-radius mt-4\">");
                        sbextras.Append("<input type=\"hidden\" name=\"ExessProtection\" id=\"hndExessProtection\" value=" + extras.rentalPrice.display.amount + " />");
                        sbextras.Append("<input type=\"hidden\" name=\"ExessProtectiondata\" id=\"hndExessProtectiondata\" value=" + extras.productId + "|" + extras.rentalPrice.display.amount + " />");
                        sbextras.Append("<div class=\"row\">");
                        sbextras.Append("<div class=\"col-12\">");
                        sbextras.Append("<div class=\"border b-radius p-3\">");
                        sbextras.Append("<div class=\"row\">");
                        sbextras.Append("<div class=\"ribbon\">");
                        sbextras.Append("<span> RECOMMENDED </span>");
                        sbextras.Append("</div>");
                        sbextras.Append("<div class=\"col-12 productInfo position-relative\">");
                        sbextras.Append("<div class=\"row\">");
                        /*sbextras.Append("<div class=\"col-1 col-md-1 dvShield text-colour8\">");
                        sbextras.Append("<i class=\"fa-solid fa-shield-halved\"></i>");
                        sbextras.Append("</div>");*/
                        sbextras.Append("<div class=\"col-12\">");
                        /*sbextras.Append("<div class=\"row\">");
                        sbextras.Append("<div class=\"col-12\">");*/
                        sbextras.Append("<h2 class=\"heading6 mb-2\"><i class=\"fa-solid fa-shield-halved\"></i><span> " + extras.name + " </span></h2>");
                        //sbextras.Append("<div class=\"moreInfo mt-md-1\" data-toggle=\"modal\" data-target=\"#dvMoreInfoModal\">");
                        //sbextras.Append("More info");
                        //sbextras.Append("</div>");
                        /*sbextras.Append("</div>");
                        sbextras.Append("</div>");*/
                        /*sbextras.Append("<div class=\"row pt-2\">");
                        sbextras.Append("<div class=\"col-12 dvPara\">");*/
                        sbextras.Append("<p>" + extras.description + " </p>");
                        /*sbextras.Append("</div>");
                        sbextras.Append("</div>");*/
                        sbextras.Append("<div class=\"row align-items-center mt-3\">");
                        sbextras.Append("<div class=\"col-6 col-md-7 col-lg-8\">");
                        /*sbextras.Append("<div class=\"dvPrice\">");*/
                        sbextras.Append("<p class=\"heading6 text-colour1\"> " + extras.rentalPrice.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                        /*sbextras.Append("</div>");*/
                        sbextras.Append("</div>");
                        sbextras.Append("<div class=\"col-6 col-md-5 col-lg-4 text-right\">");
                        /*sbextras.Append("<div class=\"travelBtn\">");*/
                        sbextras.Append("<button type =\"button\" id=ADD_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('ADD','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + lobjRateResponse.data.package.rateReference + "','" + extras.productId + "');\" class=\"btn btn-one dvAdd\">+ ADD</button>");
                        sbextras.Append("<button type =\"button\" id=REMOVE_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('REMOVE','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + lobjRateResponse.data.package.rateReference + "','" + extras.productId + "');\" class=\"d-none btn btn-one dvRemove\">- REMOVE</button>");
                        /*sbextras.Append("</div>");*/
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                    }
                    else if (extras.name.Equals("Cancellation Protection (Prepay)"))
                    {
                        sbextras.Append("<div class=\"col-12 b-radius mt-4\">");
                        sbextras.Append("<div class=\"row\">");
                        sbextras.Append("<div class=\"col-12\">");
                        sbextras.Append("<div class=\"border b-radius p-3\">");
                        sbextras.Append("<div class=\"row\">");
                        //sbextras.Append("<div class=\"ribbon\">");
                        //sbextras.Append("<span> RECOMMENDED </span>");
                        //sbextras.Append("</div>");
                        sbextras.Append("<div class=\"col-12 productInfo position-relative\">");
                        sbextras.Append("<div class=\"row\">");
                        /*sbextras.Append("<div class=\"col-1 col-md-1 dvShield text-colour8\">");
                        sbextras.Append("<i class=\"fa-solid fa-shield-halved\"></i>");
                        sbextras.Append("</div>");*/
                        sbextras.Append("<div class=\"col-12\">");
                        /*sbextras.Append("<div class=\"row\">");
                        sbextras.Append("<div class=\"col-12\">");*/
                        sbextras.Append("<h2 class=\"heading6 mb-2\"><i class=\"fa-solid fa-shield-halved\"></i><span> " + extras.name + " </span></h2>");
                        //sbextras.Append("<div class=\"moreInfo mt-md-1\" data-toggle=\"modal\" data-target=\"#dvMoreInfoModal\">");
                        //sbextras.Append("More info");
                        //sbextras.Append("</div>");
                        /*sbextras.Append("</div>");
                        sbextras.Append("</div>");*/
                        /*sbextras.Append("<div class=\"row pt-2\">");
                        sbextras.Append("<div class=\"col-12 dvPara\">");*/
                        sbextras.Append("<p>" + extras.description + " </p>");
                        /*sbextras.Append("</div>");
                        sbextras.Append("</div>");*/
                        sbextras.Append("<div class=\"row align-items-center mt-3\">");
                        sbextras.Append("<div class=\"col-6 col-md-7 col-lg-8\">");
                        /*sbextras.Append("<div class=\"dvPrice\">");*/
                        sbextras.Append("<p class=\"heading6 text-colour1\"> " + extras.rentalPrice.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                        /*sbextras.Append("</div>");*/
                        sbextras.Append("</div>");
                        sbextras.Append("<div class=\"col-6 col-md-5 col-lg-4 text-right\">");
                        /*sbextras.Append("<div class=\"travelBtn\">");*/
                        sbextras.Append("<button type =\"button\" id=ADD_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('ADD','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + lobjRateResponse.data.package.rateReference + "','" + extras.productId + "');\" class=\"btn btn-one dvAdd\">+ ADD</button>");
                        sbextras.Append("<button type =\"button\" id=REMOVE_" + extras.productId + " onclick=\"AddRemoveAditionalCharges('REMOVE','" + extras.name + "','" + extras.rentalPrice.display.amount + "','" + lobjRateResponse.data.package.rateReference + "','" + extras.productId + "');\" class=\"d-none btn btn-one dvRemove\">- REMOVE</button>");
                        /*sbextras.Append("</div>");*/
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                        sbextras.Append("</div>");
                    }
                    else
                    {
                        sbAdditionalEquipment.Append("<div class=\"row mb-3\">");
                        sbAdditionalEquipment.Append("<div class=\"col-3 col-md-3 col-lg-2 dvSelectForm pl-0\">");
                        sbAdditionalEquipment.Append("<select class=\"form-control\" name= \"cars\" class=\"\" id = \"" + extras.code + "\" onchange=\"AddAdditionalCharges(this);\">");
                        sbAdditionalEquipment.Append("<option value=\"0\"> 0 </option>");
                        sbAdditionalEquipment.Append("<option value=\"1\"> 1 </option>");
                        sbAdditionalEquipment.Append("<option value=\"2\"> 2 </option>");
                        sbAdditionalEquipment.Append("<option value=\"3\"> 3 </option>");
                        sbAdditionalEquipment.Append("<option value=\"4\"> 4 </option>");
                        sbAdditionalEquipment.Append("</select>");
                        sbAdditionalEquipment.Append("</div>");
                        sbAdditionalEquipment.Append("<div class=\"col-5 col-md-5 col-lg-4 dvChildName pl-md-2 pl-lg-5\">");
                        sbAdditionalEquipment.Append("<p id = \"spnaditionalchargename_" + extras.code + "\"> " + extras.name + "</p>");
                        sbAdditionalEquipment.Append("<div class=\"btn btn-two\" data-toggle=\"modal\" data-target=\"#dvAdditionalEquipmentModal\">");
                        sbAdditionalEquipment.Append("More info");
                        sbAdditionalEquipment.Append("</div>");
                        sbAdditionalEquipment.Append("</div>");
                        sbAdditionalEquipment.Append("<div class=\"col-4 col-md-5 col-lg-4 dvPrice\">");
                        //sbAdditionalEquipment.Append("<p> " + extras.rentalPrice.display.amount + " <i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                        sbAdditionalEquipment.Append("<p><span id = \"spnaditionalchargeamount_" + extras.code + "\">  100 </span> <i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                        sbAdditionalEquipment.Append("<span class=\"heading-semibold text-colour7\"> (per rental)</span>");
                        sbAdditionalEquipment.Append("</div>");
                        sbAdditionalEquipment.Append("</div>");
                    }
                }
            }
            else
            {
                lobjListOfData[0] = "Failed";
            }

            HttpContext.Current.Session["CarBookingDetails"] = lobjCarBookingDetails;

            lobjListOfData[0] = sbextras.ToString();
            lobjListOfData[1] = sbAdditionalEquipment.ToString();
            lobjListOfData[2] = TotalAmount.ToString();
        }
        catch (Exception ex)
        {
            lobjListOfData[0] = "Failed";
            LoggingAdapter.WriteLog("CarDetails.aspx GetRateDetails Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] AddRemoveAditionalCharges(string pstrAction, string pstrname, string pstramount, string pstrProductId)
    {
        string[] lobjListOfData = new string[7];
        CarBookingData lobjCarBookingDetails = null;
        List<AdditonalCharges> lobjAdditonalChargeslist = new List<AdditonalCharges>();
        AdditonalCharges lobjAdditonalCharges = new AdditonalCharges();
        ABCModel lobjmodel = new ABCModel();
        try
        {
            if (HttpContext.Current.Session["CarBookingDetails"] != null)
            {
                lobjCarBookingDetails = HttpContext.Current.Session["CarBookingDetails"] as CarBookingData;
            }
            if (lobjCarBookingDetails != null)
            {
                if (pstrAction.Equals("ADD"))
                {
                    lobjmodel.LogActivity(string.Format("AddRemove Additional Charges; Charge name-:{0}; Charge Amount-:{1}; Action-:{2}", pstrname, pstramount, pstrAction), ActivityType.CarBooking);

                    lobjCarBookingDetails.PayableAmount = Convert.ToString(Convert.ToDecimal(lobjCarBookingDetails.PayableAmount) + Convert.ToDecimal(pstramount));

                    lobjAdditonalCharges.Name = pstrname;
                    lobjAdditonalCharges.Code = pstrProductId;
                    lobjAdditonalCharges.amount = pstramount;
                    lobjAdditonalCharges.IsAdditionalEquipments = false;
                    lobjAdditonalCharges.TotalChargeamount = pstramount;

                    lobjCarBookingDetails.AdditonalCharges.Add(lobjAdditonalCharges);

                    if (lobjCarBookingDetails.AdditonalCharges.Count > 0)
                    {
                        double value = lobjCarBookingDetails.AdditonalCharges.FindAll(x => x.IsAdditionalEquipments == false).Sum(x => Convert.ToDouble(x.TotalChargeamount));
                        lobjCarBookingDetails.TotalAdditionalchargesAmount = Convert.ToString(value);
                    }
                    else
                    {
                        lobjCarBookingDetails.TotalAdditionalchargesAmount = "0";
                    }
                }
                else
                {

                    lobjmodel.LogActivity(string.Format("AddRemove Additional Charges; Charge name-:{0}; Charge Amount-:{1}; Action-:{2}", pstrname, pstramount, pstrAction), ActivityType.CarBooking);
                    lobjCarBookingDetails.PayableAmount = Convert.ToString(Convert.ToDecimal(lobjCarBookingDetails.PayableAmount) - Convert.ToDecimal(pstramount));

                    AdditonalCharges lobjAdditonalCharge = lobjCarBookingDetails.AdditonalCharges.Where(note => note.Code == pstrProductId).FirstOrDefault();

                    lobjCarBookingDetails.AdditonalCharges.Remove(lobjAdditonalCharge);

                    if (lobjCarBookingDetails.AdditonalCharges.Count > 0)
                    {
                        double value = lobjCarBookingDetails.AdditonalCharges.FindAll(x => x.IsAdditionalEquipments == false).Sum(x => Convert.ToDouble(x.TotalChargeamount));
                        lobjCarBookingDetails.TotalAdditionalchargesAmount = Convert.ToString(value);
                    }
                    else
                    {
                        lobjCarBookingDetails.TotalAdditionalchargesAmount = "0";
                    }
                }

                HttpContext.Current.Session["CarBookingDetails"] = lobjCarBookingDetails;
            }

            lobjListOfData[0] = JsonConvert.SerializeObject(lobjCarBookingDetails);
        }
        catch (Exception ex)
        {
            lobjListOfData[0] = "Failed";
            LoggingAdapter.WriteLog("CarDetails.aspx AddRemoveAditionalCharges Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] AddRemoveAditionalEquipment(string pstrname, string pstramount, string pstrProductId, int pstrQnty)
    {
        string[] lobjListOfData = new string[7];
        CarBookingData lobjCarBookingDetails = null;
        List<AdditonalCharges> lobjAdditonalChargeslist = new List<AdditonalCharges>();
        AdditonalCharges lobjAdditonalCharges = new AdditonalCharges();
        ABCModel lobjmodel = new ABCModel();
        try
        {
            lobjmodel.LogActivity(string.Format("AddRemove Aditional Equipment;  name-:{0};  Amount-:{1}; Quantity-:{2}", pstrname, pstramount, pstrQnty), ActivityType.CarBooking);

            if (HttpContext.Current.Session["CarBookingDetails"] != null)
            {
                lobjCarBookingDetails = HttpContext.Current.Session["CarBookingDetails"] as CarBookingData;
            }
            if (lobjCarBookingDetails != null)
            {
                if (pstrQnty > 0)
                {
                    if (lobjCarBookingDetails.AdditonalCharges.FindAll(x => x.Code.Equals(pstrProductId)).Count > 0)
                    {
                        AdditonalCharges lobjAdditonalCharge = lobjCarBookingDetails.AdditonalCharges.Where(note => note.Code == pstrProductId).FirstOrDefault();

                        lobjCarBookingDetails.AdditonalCharges.Remove(lobjAdditonalCharge);
                    }
                   
                    lobjAdditonalCharges.Name = pstrname;
                    lobjAdditonalCharges.Code = pstrProductId;
                    lobjAdditonalCharges.amount = pstramount;
                    lobjAdditonalCharges.IsAdditionalEquipments = true;
                    lobjAdditonalCharges.Quantity = pstrQnty.ToString();
                    lobjAdditonalCharges.TotalChargeamount = Convert.ToString(Convert.ToDecimal(pstramount) * pstrQnty);

                    lobjCarBookingDetails.AdditonalCharges.Add(lobjAdditonalCharges);

                    if (lobjCarBookingDetails.AdditonalCharges.Count > 0)
                    {
                        double value = lobjCarBookingDetails.AdditonalCharges.FindAll(x => x.IsAdditionalEquipments).Sum(x => Convert.ToDouble(x.TotalChargeamount));
                        lobjCarBookingDetails.TotalAdditionalequipmentAmount = Convert.ToString(value);
                        lobjCarBookingDetails.PayableAmount = Convert.ToString(Convert.ToDecimal(lobjCarBookingDetails.PayableAmount) + Convert.ToDecimal(lobjAdditonalCharges.TotalChargeamount));

                    }
                    else
                    {
                        lobjCarBookingDetails.TotalAdditionalequipmentAmount = "0";
                    }
                }
                else
                {
                    //lobjCarBookingDetails.PayableAmount = Convert.ToString(Convert.ToDecimal(lobjCarBookingDetails.PayableAmount) - Convert.ToDecimal(pstramount));

                    AdditonalCharges lobjAdditonalCharge = lobjCarBookingDetails.AdditonalCharges.Where(note => note.Code == pstrProductId).FirstOrDefault();
                    lobjCarBookingDetails.AdditonalCharges.Remove(lobjAdditonalCharge);
                   
                    if (lobjCarBookingDetails.AdditonalCharges.Count > 0)
                    {
                        double value = lobjCarBookingDetails.AdditonalCharges.FindAll(x => x.IsAdditionalEquipments).Sum(x => Convert.ToDouble(x.TotalChargeamount));
                        lobjCarBookingDetails.TotalAdditionalequipmentAmount = Convert.ToString(value);
                        lobjCarBookingDetails.PayableAmount = Convert.ToString(Convert.ToDecimal(lobjCarBookingDetails.PayableAmount) - Convert.ToDecimal(lobjAdditonalCharges.TotalChargeamount));

                    }
                    else
                    {
                        lobjCarBookingDetails.TotalAdditionalequipmentAmount = "0";
                    }
                }

                HttpContext.Current.Session["CarBookingDetails"] = lobjCarBookingDetails;
            }

            lobjListOfData[0] = JsonConvert.SerializeObject(lobjCarBookingDetails);
        }
        catch (Exception ex)
        {
            lobjListOfData[0] = "Failed";
            LoggingAdapter.WriteLog("CarDetails.aspx AddRemoveAditionalEquipment Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] GetMoreInfoAdditionalCharges(string pstrCodeId)
    {
        string[] lobjListOfData = new string[3];
        IBEAPI.ClientEntities.Extra lobjExtra = null;
        RateResponse lobjRateResponse = null;
        try
        {
            if (HttpContext.Current.Session["CarRateResponse"] != null)
            {
                lobjRateResponse = HttpContext.Current.Session["CarRateResponse"] as RateResponse;
            }
            if (lobjRateResponse != null)
            {
                lobjExtra = lobjRateResponse.data.package.extras.Where(note => note.code == pstrCodeId).FirstOrDefault();
           }
            if (lobjExtra != null)
            {
                lobjListOfData[0] = JsonConvert.SerializeObject(lobjExtra);
            }
            else
            {
                lobjListOfData[0] = "Failed";
            }
        }
        catch (Exception ex)
        {
            lobjListOfData[0] = "Failed";
            LoggingAdapter.WriteLog("CarDetails.aspx GetMoreInfoAdditionalCharges Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }
        return lobjListOfData;
    }

}