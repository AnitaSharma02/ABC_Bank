using Framework.EnterpriseLibrary.Adapters;
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

public partial class CarList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] SetCarTemplate()
    {
        string[] lobjListOfData = new string[10];
        AvailabilityResponse lobjCarAvailabilityResponse = null;
        GetAvailabilityRequest lobjCarSearchRequest = null;
        StringBuilder sb = new StringBuilder();
        StringBuilder sbPickupfilter = new StringBuilder();
        StringBuilder sbSupplierfilter = new StringBuilder();
        List<VehicleAt> lobjlistVehicleAt = new List<VehicleAt>();
        string EnjoyTravelresidenceCountryName = Convert.ToString(ConfigurationManager.AppSettings["EnjoyTravelresidenceCountryName"]);

        string lstrAircon = string.Empty;
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
                sb = GenerateCarListHTML(lobjCarAvailabilityResponse);

                foreach (var branch in lobjCarAvailabilityResponse.data.branches)
                {
                    if (lobjlistVehicleAt.FindAll(x => x.id.Equals(branch.vehicleAt.id)).Count == 0)
                    {
                        lobjlistVehicleAt.Add(branch.vehicleAt);
                    }
                    //Supplierfilter HTML
                    sbSupplierfilter.Append("<div class=\"row\">");
                    sbSupplierfilter.Append("<div class=\"col-12\">");
                    sbSupplierfilter.Append("<div class=\"dvLabel\">");
                    sbSupplierfilter.Append("<label class=\"checkbox-container d-flex\" for=\"gridChkPikup" + branch.supplier.id + "\">");
                    sbSupplierfilter.Append("<span class=\"d-inline-block\">");
                    sbSupplierfilter.Append("<input type=\"checkbox\" id=\"gridChkPikup" + branch.supplier.id + "\" onchange=\"SupplierFilter(" + branch.supplier.id + ");\">");
                    sbSupplierfilter.Append("<span class=\"checkmark\">");
                    sbSupplierfilter.Append("</span>");
                    sbSupplierfilter.Append("</span>");
                    sbSupplierfilter.Append("<span class=\"d-inline-block ml-2\">");
                    sbSupplierfilter.Append("<img class=\"mr-2\" src=" + branch.supplier.logoSvgUrl + " width=\"80\" alt=\"Supplier Logo\" />");
                    sbSupplierfilter.Append(branch.supplier.name);
                    sbSupplierfilter.Append("</span>");
                    sbSupplierfilter.Append("</label>");
                    sbSupplierfilter.Append("</div>");
                    sbSupplierfilter.Append("</div>");
                    sbSupplierfilter.Append("</div>");
                }
                //Pickupfilter HTML
                foreach (var vehicleat in lobjlistVehicleAt)
                {
                    sbPickupfilter.Append("<div class=\"row\">");
                    sbPickupfilter.Append("<div class=\"col-12\">");
                    sbPickupfilter.Append("<div class=\"dvLabel\">");
                    sbPickupfilter.Append("<label class=\"checkbox-container d-flex\" for=\"gridChkPikup" + vehicleat.id + "\">");
                    sbPickupfilter.Append("<span class=\"d-inline-block\">");
                    sbPickupfilter.Append("<input type=\"checkbox\" id=\"gridChkPikup" + vehicleat.id + "\" onchange=\"PickupFilter(" + vehicleat.id + ");\">");
                    sbPickupfilter.Append("<span class=\"checkmark\">");
                    sbPickupfilter.Append("</span>");
                    sbPickupfilter.Append("</span>");
                    sbPickupfilter.Append("<span class=\"d-inline-block ml-2\">");
                    sbPickupfilter.Append(" <i class=\"fa fa-home small-icon mr-2\"></i>");
                    sbPickupfilter.Append(vehicleat.name);
                    sbPickupfilter.Append("</span>");
                    sbPickupfilter.Append("</label>");
                    sbPickupfilter.Append("</div>");
                    sbPickupfilter.Append("</div>");
                    sbPickupfilter.Append("</div>");


                }
            }
            else
            {
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<span> No Records found. </span>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            lobjListOfData[0] = sb.ToString();
            lobjListOfData[1] = JsonConvert.SerializeObject(lobjCarSearchRequest);
            lobjListOfData[2] = Convert.ToString(lobjCarAvailabilityResponse.data.rates.Count);
            lobjListOfData[3] = sbPickupfilter.ToString();
            lobjListOfData[4] = sbSupplierfilter.ToString();
            lobjListOfData[5] = lobjCarSearchRequest.driverAge.ToString();
            lobjListOfData[6] = lobjCarSearchRequest.residenceCountry.code.ToString();
            if (lobjCarSearchRequest.residenceCountry.name!=null)
            {
                lobjListOfData[7] = lobjCarSearchRequest.residenceCountry.name.ToString();
            }
            else { lobjListOfData[7] = EnjoyTravelresidenceCountryName; }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CarList.aspx SetCarTemplate Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    private static StringBuilder MapCarlistHTML(AvailabilityResponse lobjCarAvailabilityResponse)
    {
        string lstrAircon = string.Empty;
        StringBuilder sb = new StringBuilder();
        try
        {
            foreach (var item in lobjCarAvailabilityResponse.data.rates)
            {
                sb.Append("<div class=\"border dvProductContainer b-radius mb-4\">");
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<div class=\"p-3\">");
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"ribbon\">");
                sb.Append("<span>CHEAPEST!</span>");
                sb.Append("</div>");
                sb.Append("<div class=\"dvProductInfo col-12\">");
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-md-5 col-lg-4\">");
                sb.Append("<div class=\"dvCarImage\" onclick=\"ViewMoreInfo('" + item.vehicle.uniqueRef + "');\">");
                sb.Append("<img class=\"img-fluid mt-auto mb-auto\" src=" + item.vehicle.images[0].url + " />");
                sb.Append("</div>");
                
                sb.Append("</div>");
                sb.Append("<div class=\"col-md-7 col-lg-4 mb-3 mb-md-0\">");
                sb.Append("<div class=\"dvHeading\">");
                sb.Append("<h2 class=\"heading6 text-colour1\">" + item.vehicle.name + "</h2>");
                sb.Append("</div>");

                sb.Append(" <div class=\"row dvIcons\">");
                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-couch\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\">x " + item.vehicle.seats + "</span>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-life-ring\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\"> " + item.vehicle.transmission + "</span>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-door-closed\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\"> " + item.vehicle.doors + "</span>");
                sb.Append("</div>");

                if (item.vehicle.airco)
                {
                    lstrAircon = "AirCon";
                }
                else
                {
                    lstrAircon = "Non AirCon";
                }

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-snowflake\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\">" + lstrAircon + "</span>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-gas-pump\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\">Fair Fuel Policy</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-lg-4\">");
                sb.Append("<div class=\"row align-items-sm-center flex-lg-column align-items-lg-start\">");
                sb.Append("<div class=\"col-lg-12 mb-3 mb-lg-3\">");
                sb.Append("<div class=\"dvPrice\">");
                sb.Append("<p class=\"heading6 text-colour1\"> " + item.packages[0].payments.estimatedTotal.vehicle.display.amount + " <i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                sb.Append("<p class=\"h7\"><span>(mandatory fees included)</span></cite>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-sm-6 col-lg-7 mb-3 mb-sm-0 mb-lg-3\">");
                sb.Append("<button type=\"button\" class=\"btn btn-one w-100\" onclick=\"ViewDeal('" + item.vehicle.uniqueRef + "');\"><span>View Deal</span></button>");
                sb.Append("</div>");
                sb.Append("<div class=\"col-sm-6 col-lg-12\">");
                sb.Append("<div class=\"btn btn-two w-100\" onclick=\"ViewMoreInfo('" + item.vehicle.uniqueRef + "');\">");
                sb.Append("<span>More info</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class=\"col -12 border-top pt-3 mt-3 pr-0 pl-0\">");
                sb.Append("<div class=\"d-flex flex-wrap justify-content-between align-items-lg-center\">");
                sb.Append("<div class=\"col-lg-4 order-1\">");
                sb.Append("<button data-toggle =\"collapse\" type=\"button\" class=\"btn b-radius border w-100 d-flex justify-content-center collapsed\" href=\"#" + item.vehicle.uniqueRef + "\" role=\"button\" aria-expanded=\"false\" aria-controls=\"collapseExample\"><span class=\"d-inline-block\">Included in the price</span> <span class=\"arrow-icon ml-2\"><i class=\"fa fa-caret-up\"></i></span></button>");
                sb.Append("</div>");
                sb.Append("<div class=\"col-lg-8 order-0 mb-3 mb-lg-0\">");
                sb.Append("<div class=\"p-2 border b-radius\">");

                Branch lobjBranch = lobjCarAvailabilityResponse.data.branches.Find(x => x.id == item.pickUpBranchId);

                sb.Append("<a class=\"link1 text-decoration-none text-colour7\"><i class=\"fa-solid fa-location-dot\"></i> <span>Vehicle location:</span></a>");
                sb.Append("<span class=\"h7 text-colour7 ml-1\"> " + lobjBranch.addressData.line1 + "," + lobjBranch.addressData.line3 + "," + lobjBranch.addressData.postalCode + " </span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                StringBuilder sbinclusions = new StringBuilder();
                foreach (var inclusions in item.packages[0].inclusions)
                {
                    sbinclusions.Append("<div class=\"col-12 col-md-4 d-flex align-items-center mb-3\">");
                    sbinclusions.Append("<i class=\"fa-solid fa-check mx-2 h7\"></i>");
                    sbinclusions.Append("<p class=\"h7\"> " + inclusions.name + " </p>");
                    sbinclusions.Append("</div>");
                }

                sb.Append("<div class=\"row mt-2\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<div class=\"collapse\" id=" + item.vehicle.uniqueRef + ">");
                sb.Append("<div class=\"border p-3\">");
                sb.Append("<div class=\"row\">");
                sb.Append(sbinclusions.ToString());
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CarList.aspx MapCarlistHTML Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return sb;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] FilterCarList(string pstrTransmissionvalues, string pstrPassengersvalues, string pstrVehicleTypevalues)
    {
        string[] lobjListOfData = new string[4];
        AvailabilityResponse lobjCarAvailabilityResponse = null;
        AvailabilityResponse lobjFilterCarAvailabilityResponse = new AvailabilityResponse();
        StringBuilder sb = new StringBuilder();
        string lstrAircon = string.Empty;
        string[] Transmissionvalues = null;
        string[] Passengersvalues = null;
        string[] VehicleTypevalues = null;
        lobjFilterCarAvailabilityResponse.data = new AvailabilityData();
        lobjFilterCarAvailabilityResponse.data.rates = new List<Rate>();

        List<Rate> lstrTransmissionList = new List<Rate>();
        List<Rate> lstrPassengersList = new List<Rate>();
        List<Rate> lstrVehicleTypeList = new List<Rate>();
        try
        {
            if (HttpContext.Current.Session["Cars"] != null)
            {
                lobjCarAvailabilityResponse = HttpContext.Current.Session["Cars"] as AvailabilityResponse;
            }

            if (!string.IsNullOrEmpty(pstrTransmissionvalues))
            {
                Transmissionvalues = pstrTransmissionvalues.Split(',');

                foreach (var item in Transmissionvalues)
                {
                    lstrTransmissionList.AddRange(lobjCarAvailabilityResponse.data.rates.FindAll(x => x.vehicle.transmission.Contains(item)).ToList());
                }
            }
            else
            {
                lstrTransmissionList = lobjCarAvailabilityResponse.data.rates;
            }

            if (!string.IsNullOrEmpty(pstrPassengersvalues))
            {
                Passengersvalues = pstrPassengersvalues.Split(',');

                foreach (var item in Passengersvalues)
                {
                    lstrPassengersList.AddRange(lstrTransmissionList.FindAll(x => x.vehicle.seats.Contains(item)).ToList());
                }
            }
            else
            {
                lstrPassengersList = lstrTransmissionList;
            }

            if (!string.IsNullOrEmpty(pstrVehicleTypevalues))
            {
                VehicleTypevalues = pstrVehicleTypevalues.Split(',');

                foreach (var item in VehicleTypevalues)
                {
                    lstrVehicleTypeList.AddRange(lstrPassengersList.FindAll(x => x.vehicleType.Contains(item)).ToList());
                }
            }
            else
            {
                lstrVehicleTypeList = lstrPassengersList;
            }


            lobjFilterCarAvailabilityResponse.data.rates = lstrVehicleTypeList;
            if (lobjFilterCarAvailabilityResponse.data.rates.Count > 0)
            {
                sb = GenerateCarListHTML(lobjFilterCarAvailabilityResponse);

                lobjListOfData[0] = sb.ToString();
                lobjListOfData[1] = Convert.ToString(lobjFilterCarAvailabilityResponse.data.rates.Count);
            }
            else
            {
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<span> No Records found. </span>");
                sb.Append("</div>");
                sb.Append("</div>");
                lobjListOfData[0] = sb.ToString();
                lobjListOfData[1] = "0";
            }
        }
        catch (Exception ex)
        {
            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-12\">");
            sb.Append("<span> No Records found. </span>");
            sb.Append("</div>");
            sb.Append("</div>");
            lobjListOfData[0] = sb.ToString();
            lobjListOfData[1] = "0";

            LoggingAdapter.WriteLog("CarList.aspx FilterTransmissionCarList Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] GetMoreInfoDetails(string pstruniqueRefNo)
    {
        string[] lobjListOfData = new string[2];
        AvailabilityResponse lobjCarAvailabilityResponse = null;
        Rate lobjVehicle = new Rate();
        StringBuilder sbmoreInfo = new StringBuilder();
        string lstrAircon = string.Empty;
        try
        {
            if (HttpContext.Current.Session["Cars"] != null)
            {
                lobjCarAvailabilityResponse = HttpContext.Current.Session["Cars"] as AvailabilityResponse;
            }

            if (lobjCarAvailabilityResponse != null)
            {
                lobjVehicle = lobjCarAvailabilityResponse.data.rates.Find(x => x.vehicle.uniqueRef.Equals(pstruniqueRefNo));

                if (lobjVehicle != null)
                {
                    StringBuilder sbinclusions = new StringBuilder();
                    foreach (var inclusions in lobjVehicle.packages[0].inclusions)
                    {
                        sbinclusions.Append("<div class=\"col-12 col-md-4 d-flex align-items-center mb-3\">");
                        sbinclusions.Append("<i class=\"fa-solid fa-check mx-2 h7\"></i>");
                        sbinclusions.Append("<p class=\"h7\"> " + inclusions.name + " </p>");
                        sbinclusions.Append("</div>");
                    }

                    if (lobjVehicle.vehicle.airco)
                    {
                        lstrAircon = "AirCon";
                    }
                    else
                    {
                        lstrAircon = "Non AirCon";
                    }

                    //Moreinfo popup html

                    sbmoreInfo.Append("<div class=\"mb-2\">");
                    sbmoreInfo.Append("<div class=\"row\">");
                    sbmoreInfo.Append("<div class=\"col-md-5 col-lg-4\">");
                    sbmoreInfo.Append("<div class=\"dvCarImage\">");
                    sbmoreInfo.Append("<img class=\"img-fluid mt-auto mb-auto\" src=" + lobjVehicle.vehicle.images[0].url + " />");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"col-md-7 col-lg-4 mb-3 mb-md-0\">");
                    sbmoreInfo.Append("<div class=\"carHead\">");
                    sbmoreInfo.Append("<p class=\"heading6 text-colour1\">" + lobjVehicle.vehicle.name + "</p>");
                    sbmoreInfo.Append("<span class='h7'> or similar(Small) </span>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"row dvIcons\">");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"borderColor\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-couch\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\">x " + lobjVehicle.vehicle.seats + "</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"borderColor\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-life-ring\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\"> " + lobjVehicle.vehicle.transmission + "</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"borderColor\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-door-closed\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\"> " + lobjVehicle.vehicle.doors + "</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"borderColor\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-gas-pump\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\">Fair Fuel Policy</span>");
                    sbmoreInfo.Append("</div>");

                    sbmoreInfo.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                    sbmoreInfo.Append("<div class=\"borderColor\">");
                    sbmoreInfo.Append("<i class=\"fa-solid fa-snowflake\"></i>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<span class=\"ml-2 h7\">" + lstrAircon + "</span>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("</div>");
                    sbmoreInfo.Append("<div class=\"travelBtn mt-4\">");
                    sbmoreInfo.Append("<a href=\"CarDetails.aspx?uniqueRefId=" + pstruniqueRefNo + "\" class=\"btn btn-one\">BOOK NOW</a>");
                    //sbmoreInfo.Append("<p>" + lobjVehicle.packages[0].payments.payNow.vehicle.display.amount + "<i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");payNow.vehicle.display.amount   " + lobjVehicle.packages[0].payments.estimatedTotal.total.display.amount + "
                    sbmoreInfo.Append("</div>");
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
                    sbmoreInfo.Append("<a class=\"link1 text-decoration-none text-colour7\"><i class=\"fa-solid fa-location-dot\"></i><span> Vehicle location:</span></a>");

                    Branch lobjBranch = lobjCarAvailabilityResponse.data.branches.Find(x => x.id == lobjVehicle.pickUpBranchId);

                    sbmoreInfo.Append("<span class=\"h7 text-colour7 ml-1\"> " + lobjBranch.addressData.line1 + "," + lobjBranch.addressData.line3 + "," + lobjBranch.addressData.postalCode + " </span>");
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
                }
                else
                {
                    lobjListOfData[0] = "Failed";
                }
            }

            lobjListOfData[0] = sbmoreInfo.ToString();
        }
        catch (Exception ex)
        {
            lobjListOfData[0] = "Failed";
            LoggingAdapter.WriteLog("CarList.aspx GetMoreInfoDetails Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    private static StringBuilder GenerateCarListHTML(AvailabilityResponse lobjFilterCarAvailabilityResponse)
    {
        AvailabilityResponse lobjCarAvailabilityResponse = null;
        StringBuilder sb = new StringBuilder();
        string lstrAircon = string.Empty;
        try
        {
            if (HttpContext.Current.Session["Cars"] != null)
            {
                lobjCarAvailabilityResponse = HttpContext.Current.Session["Cars"] as AvailabilityResponse;
            }

            foreach (var item in lobjFilterCarAvailabilityResponse.data.rates)
            {
                sb.Append("<div class=\"border dvProductContainer b-radius mb-4\">");
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<div class=\"p-3\">");
                sb.Append("<div class=\"dvProductInfo row\">");
                sb.Append("<div class=\"ribbon\">");
                sb.Append("<span>CHEAPEST!</span>");
                sb.Append("</div>");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-md-5 col-lg-4\">");
                sb.Append("<div class=\"dvCarImage\" onclick=\"ViewMoreInfo('" + item.vehicle.uniqueRef + "');\">");
                sb.Append("<img class=\"img-fluid mt-auto mb-auto\" src=" + item.vehicle.images[0].url + " />");
                sb.Append("</div>");
                
                sb.Append("</div>");
                sb.Append("<div class=\"col-md-7 col-lg-4 mb-3 mb-md-0\">");
                sb.Append("<div class=\"dvHeading\">");
                sb.Append("<h2 class=\"heading6 text-colour1\">" + item.vehicle.name + "</h2>");
                sb.Append("<span class='h7'>or similar (Small)</span>");
                sb.Append("</div>");

                sb.Append(" <div class=\"row dvIcons\">");
                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-couch\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\">x " + item.vehicle.seats + "</span>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-life-ring\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\"> " + item.vehicle.transmission + "</span>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-door-closed\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\"> " + item.vehicle.doors + "</span>");
                sb.Append("</div>");

                if (item.vehicle.airco)
                {
                    lstrAircon = "AirCon";
                }
                else
                {
                    lstrAircon = "Non AirCon";
                }

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-snowflake\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\">" + lstrAircon + "</span>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sb.Append("<div class=\"borderColor\">");
                sb.Append("<i class=\"fa-solid fa-gas-pump\"></i>");
                sb.Append("</div>");
                sb.Append("<span class=\"ml-2 h7\">Fair Fuel Policy</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-lg-4\">");
                sb.Append("<div class=\"row align-items-sm-center flex-lg-column align-items-lg-start\">");
                sb.Append("<div class=\"col-lg-12 mb-3 mb-lg-3\">");
                sb.Append("<div class=\"dvPrice\">");
                //sb.Append("<p> " + item.packages[0].payments.estimatedTotal.vehicle.display.amount + " <i class=\"fa fa-usd\" aria-hidden=\"true\"></i></p>");
                sb.Append("<p class=\"heading6 text-colour1\"> " + item.packages[0].payments.estimatedTotal.total.display.amount + " Points </p>");
                sb.Append("<p class=\"h7\"><span>(mandatory fees included)</span></cite>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class=\"col-sm-6 col-lg-7 mb-3 mb-sm-0 mb-lg-3\">");
                sb.Append("<button type=\"button\" class=\"btn btn-one w-100\" onclick=\"ViewDeal('" + item.vehicle.uniqueRef + "');\">View Deal</button>");
                sb.Append("</div>");
                sb.Append("<div class=\"col-sm-6 col-lg-12\">");
                sb.Append("<div class=\"btn btn-two w-100\" onclick=\"ViewMoreInfo('" + item.vehicle.uniqueRef + "');\">");
                sb.Append("<span>More info</span>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"dvLocation col-12 mt-3\">");
                sb.Append("<div class=\"row justify-content-between align-items-lg-center\">");
                sb.Append("<div class=\"col-lg-4 order-1\">");
                sb.Append("<button data-toggle=\"collapse\" type=\"button\" class=\"btn b-radius border w-100 d-flex justify-content-center collapsed\" href=\"#" + item.vehicle.uniqueRef + "\" role=\"button\" aria-expanded=\"false\" aria-controls=\"collapseExample\"><span class=\"d-inline-block\">Included in the price</span> <span class=\"arrow-icon ml-2\"><i class=\"fa fa-caret-up\"></i></span></button>");
                sb.Append("</div>");
                sb.Append("<div class=\"col-lg-8 order-0 mb-3 mb-lg-0\">");
                sb.Append("<div class=\"p-2 border b-radius\">");

                Branch lobjBranch = lobjCarAvailabilityResponse.data.branches.Find(x => x.id == item.pickUpBranchId);
                if (lobjBranch != null)
                {
                    sb.Append("<a class=\"link1 text-decoration-none text-colour7\"><i class=\"fa-solid fa-location-dot\"></i> <span>Vehicle location:</span></a>");
                    sb.Append("<span class=\"h7 text-colour7 ml-1\"> " + lobjBranch.addressData.line1 + "," + lobjBranch.addressData.line3 + "," + lobjBranch.addressData.postalCode + " </span>");
                }
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");

                StringBuilder sbinclusions = new StringBuilder();
                foreach (var inclusions in item.packages[0].inclusions)
                {
                    sbinclusions.Append("<div class=\"col-12 col-md-4 d-flex align-items-center mb-3\">");
                    sbinclusions.Append("<i class=\"fa-solid fa-check mx-2 h7\"></i>");
                    sbinclusions.Append("<p class=\"h7\"> " + inclusions.name + " </p>");
                    sbinclusions.Append("</div>");
                }

                sb.Append("<div class=\"row mt-2\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<div class=\"collapse\" id=" + item.vehicle.uniqueRef + ">");
                sb.Append("<div class=\"border p-3\">");
                sb.Append("<div class=\"row\">");
                sb.Append(sbinclusions.ToString());
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");//this is a row above dvLocation close
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
        }
        catch (Exception ex)
        {

            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-12\">");
            sb.Append("<span> No Records found. </span>");
            sb.Append("</div>");
            sb.Append("</div>");
            LoggingAdapter.WriteLog("CarList.aspx GenerateCarListHTML Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return sb;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] PickupFilter(int pstrvehicleAtId)
    {
        string[] lobjListOfData = new string[2];
        GetAvailabilityRequest lobjCarSearchRequest = null;
        Rate lobjVehicle = new Rate();
        StringBuilder sb = new StringBuilder();
        IBEAPIModel lobjModel = new IBEAPIModel();
        try
        {
            if (HttpContext.Current.Session["CarSearchRequest"] != null)
            {
                lobjCarSearchRequest = HttpContext.Current.Session["CarSearchRequest"] as GetAvailabilityRequest;
            }
            lobjCarSearchRequest.vehicleAtId = pstrvehicleAtId;
            AvailabilityResponse lobjCarAvailabilityResponse = lobjModel.GetAvailability(lobjCarSearchRequest);

            if (lobjCarAvailabilityResponse != null)
            {
                sb = GenerateCarListHTML(lobjCarAvailabilityResponse);
            }
            else
            {
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<span> No Records found. </span>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
            lobjListOfData[0] = sb.ToString();
            lobjListOfData[1] = Convert.ToString(lobjCarAvailabilityResponse.data.rates.Count);
        }
        catch (Exception ex)
        {
            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-12\">");
            sb.Append("<span> No Records found. </span>");
            sb.Append("</div>");
            sb.Append("</div>");
            lobjListOfData[0] = sb.ToString();
            lobjListOfData[1] = "0";

            LoggingAdapter.WriteLog("CarList.aspx PickupFilter Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    [ScriptMethod()]
    [WebMethod]
    public static string[] SupplierFilter(int pstrsupplierId)
    {
        string[] lobjListOfData = new string[2];
        GetAvailabilityRequest lobjCarSearchRequest = null;
        Rate lobjVehicle = new Rate();
        StringBuilder sb = new StringBuilder();
        IBEAPIModel lobjModel = new IBEAPIModel();
        try
        {
            if (HttpContext.Current.Session["CarSearchRequest"] != null)
            {
                lobjCarSearchRequest = HttpContext.Current.Session["CarSearchRequest"] as GetAvailabilityRequest;
            }
            lobjCarSearchRequest.supplierId = pstrsupplierId;
            AvailabilityResponse lobjCarAvailabilityResponse = lobjModel.GetAvailability(lobjCarSearchRequest);

            if (lobjCarAvailabilityResponse != null)
            {
                sb = GenerateCarListHTML(lobjCarAvailabilityResponse);
            }
            else
            {
                sb.Append("<div class=\"row\">");
                sb.Append("<div class=\"col-12\">");
                sb.Append("<span> No Records found. </span>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
            lobjListOfData[0] = sb.ToString();
            lobjListOfData[1] = Convert.ToString(lobjCarAvailabilityResponse.data.rates.Count);
        }
        catch (Exception ex)
        {
            sb.Append("<div class=\"row\">");
            sb.Append("<div class=\"col-12\">");
            sb.Append("<span> No Records found. </span>");
            sb.Append("</div>");
            sb.Append("</div>");
            lobjListOfData[0] = sb.ToString();
            lobjListOfData[1] = "0";

            LoggingAdapter.WriteLog("CarList.aspx SupplierFilter Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    [WebMethod]
    public static List<string> GetDriverResidence()
    {
        try
        {
            List<string> lobjListOfLocations = new List<string>();
            string lstrLocation = string.Empty;
           
            IBEAPIModel lobjmodel = new IBEAPIModel();
            CarCountryResponse lobjCarCountryResponse = null;
            if (HttpContext.Current.Application["CarCountries"] != null)
            {
                lobjCarCountryResponse = HttpContext.Current.Application["CarCountries"] as CarCountryResponse;
            }
            if (lobjCarCountryResponse != null && lobjCarCountryResponse.data.Count > 0)
            {
                CarCountryResponse lobjfilterCarCountryResponse = new CarCountryResponse();
                lobjfilterCarCountryResponse.data = lobjCarCountryResponse.data.OrderBy(x => x.name).ToList();

                if (lobjfilterCarCountryResponse != null && lobjfilterCarCountryResponse.data.Count > 0)
                {
                    foreach (var location in lobjfilterCarCountryResponse.data)
                    {
                        lobjListOfLocations.Add(location.name + "|" + location.code);
                    }
                }
                else
                {
                    lobjListOfLocations.Add("Location not found.");
                }
            }
            else
            {
                lobjListOfLocations.Add("Location not found.");
            }
            return lobjListOfLocations;
        }
        catch (Exception ex)
        {
            LoggingAdapter.WriteLog("CarSearch GetPickupLocation Ex-: " + ex.Message + Environment.NewLine + "Stack Trace-" + ex.StackTrace + Environment.NewLine + "Inner Exception-:" + ex.InnerException);
            return null;
        }
    }
}