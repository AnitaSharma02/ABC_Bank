using Core.Platform.Booking.Entities;
using Core.Platform.Member.Entites;
using Core.Platform.MemberActivity.Constants;
using Core.Platform.MemberActivity.Entities;
using Core.Platform.OTP.Entities;
using Core.Platform.ProgramMaster.Entities;
using Framework.EnterpriseLibrary.Adapters;
using ABC.Model;
using IBEAPI.ClientEntities;
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

public partial class CarPayment : System.Web.UI.Page
{
    public static string pstrDisplayCurrency = Convert.ToString(ConfigurationManager.AppSettings["ProgramCurrency"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CarSearchRequest"] != null && Session["Cars"] != null)
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
    public static string[] MapCarDetails()
    {
        string[] lobjListOfData = new string[7];
        GetAvailabilityRequest lobjCarSearchRequest = null;
        RateResponse lobjRateResponse = null;
        CarBookingData lobjCarBookingDetails = null;
        Rate lobjVehicle = new Rate();
        string lstrAircon = string.Empty;
        StringBuilder sbcardetails = new StringBuilder();
        StringBuilder sbmoreInfo = new StringBuilder();
        try
        {
            if (HttpContext.Current.Session["CarSearchRequest"] != null)
            {
                lobjCarSearchRequest = HttpContext.Current.Session["CarSearchRequest"] as GetAvailabilityRequest;
            }
            if (HttpContext.Current.Session["CarRateResponse"] != null)
            {
                lobjRateResponse = HttpContext.Current.Session["CarRateResponse"] as RateResponse;
            }
            if (HttpContext.Current.Session["SelectedCar"] != null)
            {
                lobjVehicle = HttpContext.Current.Session["SelectedCar"] as Rate;
            }
            if (HttpContext.Current.Session["CarBookingDetails"] != null)
            {
                lobjCarBookingDetails = HttpContext.Current.Session["CarBookingDetails"] as CarBookingData;
            }

            if (lobjVehicle != null && lobjVehicle.packages.Count > 0)
            {
                //Car Details Left Pannel
                StringBuilder sbinclusions = new StringBuilder();
                foreach (var inclusions in lobjVehicle.packages[0].inclusions)
                {
                    sbinclusions.Append("<div class=\"col-12 col-md-6 d-flex align-items-center mb-3\">");
                    sbinclusions.Append("<i class=\"fa-solid fa-check mx-2 small-icon\"></i>");
                    sbinclusions.Append("<p class=\"heading-medium h7\"> " + inclusions.name + " </p>");
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
                sbcardetails.Append("<div class=\"borderColor\">");
                sbcardetails.Append("<i class=\"fa-solid fa-snowflake\"></i>");
                sbcardetails.Append("</div>");
                sbcardetails.Append("<span class=\"ml-2 heading-medium h8\">" + lstrAircon + "</span>");
                sbcardetails.Append("</div>");

                sbcardetails.Append("<div class=\"col-6 col-md-6 d-flex mb-1 mt-2 align-items-center\">");
                sbcardetails.Append("<div class=\"borderColor\">");
                sbcardetails.Append("<i class=\"fa-solid fa-gas-pump\"></i>");
                sbcardetails.Append("</div>");
                sbcardetails.Append("<span class=\"ml-2 heading-medium h8\">Fair Fuel Policy</span>");
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
                sbmoreInfo.Append("<div class=\"d-flex flex-wrap justify-content-between align-items-center border p-2\">");
                sbmoreInfo.Append("<div class=\"col-12 col-sm-6 col-md-3 order-md-0\">");
                sbmoreInfo.Append("<div class=\"\">");
                sbmoreInfo.Append("<img width=\"50\" class=\"img-fluid mt-auto mb-auto\" src=\"images/logos/giift-logo-blue.svg\" alt=\"Logo\">");
                sbmoreInfo.Append("</div>");
                sbmoreInfo.Append("</div>");
                sbmoreInfo.Append("<div class=\"col-12 col-sm-6 col-md-9 mt-2 mt-md-0 order-md-1 text-left\">");
                sbmoreInfo.Append("<div class=\"\">");
                sbmoreInfo.Append("<a class=\"link1\" href =\"#\" ><i class=\"fa-solid fa-location-dot\"></i><span> Vehicle location:</span></a>");

                Branch lobjBranch = lobjRateResponse.data.branches.Find(x => x.id == lobjRateResponse.data.pickUpBranchId);
                if (lobjBranch != null)
                {
                    sbmoreInfo.Append("<span class=\"h7 text-colour7 ml-2\"> " + lobjBranch.addressData.line1 + "," + lobjBranch.addressData.line3 + "," + lobjBranch.addressData.postalCode + " </span>");
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
            }
            else
            {
                lobjListOfData[0] = "Failed";
            }

            lobjListOfData[0] = sbcardetails.ToString();
            lobjListOfData[1] = JsonConvert.SerializeObject(lobjCarSearchRequest);
            lobjListOfData[2] = JsonConvert.SerializeObject(lobjCarBookingDetails);
            lobjListOfData[3] = sbmoreInfo.ToString();
        }
        catch (Exception ex)
        {
            lobjListOfData[0] = "Failed";
            LoggingAdapter.WriteLog("CarPayment.aspx MapCarDetails Exception-: " + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + ex.StackTrace);
        }

        return lobjListOfData;
    }

    protected void btnMakePayment_Click(object sender, EventArgs e)
    {
        string lstrResponse = string.Empty;
        RateResponse lobjRateResponse = null;
        CarBookingData lobjCarBookingDetails = null;
        MemberDetails lobjMemberDetails = null;
        Rate lobjVehicle = null;
        ABCModel lobjModel = new ABCModel();
        CarBookingRequest lobjCarBookingRequest = new CarBookingRequest();
        try
        {
            if (HttpContext.Current.Session["CarRateResponse"] != null)
            {
                lobjRateResponse = HttpContext.Current.Session["CarRateResponse"] as RateResponse;
            }
            if (HttpContext.Current.Session["SelectedCar"] != null)
            {
                lobjVehicle = HttpContext.Current.Session["SelectedCar"] as Rate;
            }
            if (HttpContext.Current.Session["CarBookingDetails"] != null)
            {
                lobjCarBookingDetails = HttpContext.Current.Session["CarBookingDetails"] as CarBookingData;
            }

            if (HttpContext.Current.Session["MemberDetails"] != null)
            {
                lobjMemberDetails = HttpContext.Current.Session["MemberDetails"] as MemberDetails;
            }
            string lstrCurrency = lobjModel.GetDefaultCurrency();
            lobjCarBookingRequest.extras = new List<object>();

            if (lobjCarBookingDetails.AdditonalCharges.Count > 0)
            {
                foreach (var item in lobjCarBookingDetails.AdditonalCharges)
                {
                    Extra lobjExtra = new Extra();
                    if (item.IsAdditionalEquipments)
                    {
                        lobjExtra.quantity =Convert.ToInt32(item.Quantity);
                        lobjExtra.code = lobjRateResponse.data.package.extras.FindAll(x => x.code == item.Code).FirstOrDefault().code;
                    }
                    else
                    {
                        lobjExtra.quantity = Convert.ToInt32(item.Quantity);
                        lobjExtra.code = lobjRateResponse.data.package.extras.FindAll(x => x.productId == Convert.ToInt32(item.Code)).FirstOrDefault().code;
                    }
                    lobjCarBookingRequest.extras.Add(lobjExtra); 
                }
            }
            int ThreshouldValue = 0;
            ProgramDefinition lobjProgramDefinition = lobjModel.GetProgramMaster();
            List<RedemptionKeys> lobjRedemptionKeys = new List<RedemptionKeys>();
            lobjRedemptionKeys = lobjModel.GetAllRedemptionKeys(lobjProgramDefinition.ProgramId);
            ThreshouldValue = lobjRedemptionKeys.Find(lobj => lobj.RedemptionCode.Equals(RedemptionCodeKeys.CAR.ToString()) && lobj.Currency.Equals(lstrCurrency)).OTPThreshold;
            int lintTotalPoints = Convert.ToInt32(lobjRateResponse.data.package.payments.estimatedTotal.total.display.amount); //Convert.ToInt32(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalPoints);
            float lftAmount = Convert.ToSingle(lobjRateResponse.data.package.payments.payNow.total.payment.amount);//Convert.ToSingle(lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalBaseFare);

            List<RedemptionDetails> lobjListOfRedemptionDetails = new List<RedemptionDetails>();
            RedemptionDetails lobjRedemptionDetails = new RedemptionDetails();
            lobjRedemptionDetails.Currency = lstrCurrency;
            lobjRedemptionDetails.DisplayCurrency = pstrDisplayCurrency;//lobjModel.CurrencyDisplayText(lstrCurrency);
            lobjRedemptionDetails.Points = lintTotalPoints;
            lobjRedemptionDetails.RelationReference = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
            lobjRedemptionDetails.Amount = lftAmount;

            lobjListOfRedemptionDetails.Add(lobjRedemptionDetails);
            Session["RedemptionDetails"] = lobjListOfRedemptionDetails;
            lobjCarBookingRequest.customer = new CustomerDetails();
            lobjCarBookingRequest.customer.titleId = 0;
            lobjCarBookingRequest.customer.firstName = txtFirstName.Text.Trim();
            lobjCarBookingRequest.customer.lastName = txtSurName.Text.Trim();
            lobjCarBookingRequest.customer.email = txtEmailId.Text.Trim();
            lobjCarBookingRequest.customer.phone = txtMobileNo.Text.Trim();
            lobjCarBookingRequest.customer.memberId = lobjMemberDetails.MemberRelationsList[0].RelationReference;
            lobjCarBookingRequest.flightNumber = txtFlightNo.Text.Trim();

            lobjCarBookingRequest.payment = new CarBookingPayment();
            lobjCarBookingRequest.payment.method = "AgencyCredit";

            lobjCarBookingRequest.rateReference = lobjRateResponse.data.package.rateReference;
            lobjCarBookingRequest.lang = "en-gb";

            HttpContext.Current.Session["CarBookingRequest"] = lobjCarBookingRequest;

            HttpContext.Current.Session["BookingFlag"] = "car";
            HttpContext.Current.Session["CarTotalRedeemAmount"] = lobjCarBookingDetails.PayableAmount;

            if (ThreshouldValue <= lintTotalPoints && !ThreshouldValue.Equals(-1))
            {
                bool Status = false;
                OTPDetails lobjOTPDetails = new OTPDetails();
                lobjOTPDetails.UniquerefID = lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference;
                lobjOTPDetails.OtpEnumTypes = OTPEnumTypes.CARREVIEWNCONFIRM;
                lobjOTPDetails.OtpType = Convert.ToString(OTPEnumTypes.CARREVIEWNCONFIRM);

                Status = lobjModel.SendOTPEmailAndSMS(lobjMemberDetails, "redemption_otp", lobjOTPDetails, "Car");

                if (Status)
                {
                    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "CAR", lobjRedemptionDetails.RelationReference, "Success"), ActivityType.ReviewConfirmOTPSuccess);
                    lstrResponse = "ValidateOTP.aspx?flag=Car";
                    Response.Redirect("ValidateOTP.aspx?flag=Car", false);
                }
                else
                {
                    lobjModel.LogActivity(string.Format(ActivityConstants.ReviewConfirmOTP, "CAR", lobjRedemptionDetails.RelationReference, "Failed"), ActivityType.ReviewConfirmOTPFailed);
                    lstrResponse = "BookingFailure.aspx";
                    Response.Redirect("BookingFailure.aspx", false);
                }
            }
            else
            {
                lobjModel.LogActivity(string.Format("Flight Booking {0}: Requested", lobjRedemptionDetails.RelationReference), ActivityType.FlightBooking);
                lstrResponse = "PointGateway.aspx";
                Response.Redirect("PointGateway.aspx", false);
            }

            // lobjModel.LogActivity(string.Format("AirReviewAndConfirm; Flight BookNow click; TotalFare-:{0}; Destination-:{1} Response-:{2};", lobjCreateItineraryResponse.ItineraryDetails.FareDetails.TotalBaseFare, lobjCreateItineraryRequest.ItineraryDetails.OriginLocation + "-" + lobjCreateItineraryRequest.ItineraryDetails.DestinationLocation, lstrResponse), ActivityType.FlightBooking);
        }
        catch (Exception ex)
        {
            lobjModel.LogActivity(string.Format("Flight Booking {0}; Exception", lobjMemberDetails.MemberRelationsList.Find(l => l.RelationType.Equals(RelationType.LBMS)).RelationReference), ActivityType.FlightBooking);
            LoggingAdapter.WriteLog("AirReviewAndConfirm :" + ex.Message + ex.StackTrace);
            Response.Redirect("BookingFailure.aspx", false);
        }


    }

}