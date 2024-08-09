<%@ Page Title="Car Details" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarDetails.aspx.cs" Inherits="CarDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
      <%--<link href="Css/dropDown.css" type="text/css" rel="stylesheet" />
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />--%>
    <link href="Css/car.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/CarResultScript.js" type="text/javascript"></script>
    <style>
     /*     .navdiv {
         background: #00425F;
     }*/
     #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
         display: none;
     }

     /*.innerHeader {
         height: auto;
         padding: 0 70px;
         background: #00425F !important;
         box-shadow: rgb(0 0 0 / 25%) 0px 5px 15px;
         height: auto !important;
     }*/

     .fade-in {
         animation: fadeIn 0.5s ease-in-out forwards;
         display: block;
     }

     .fade-out {
         animation: fadeOut 0.5s ease-in-out forwards;
         display: none;
     }

     @keyframes fadeIn {
         from {
             opacity: 0;
         }

         to {
             opacity: 1;
         }
     }

     @keyframes fadeOut {
         from {
             opacity: 1;
         }

         to {
             opacity: 0;
         }
     }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            var SelectedCarId = getQuerystring("uniqueRefId");
            GetCarDetails(SelectedCarId);
        });
        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(decodeURIComponent(window.location.href));
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

    </script>

<div class="dvCarList">
  <div class="container-lg">
    <div class="dvCarSteps row py-4 justify-content-between justify-content-sm-center">
      <div class="col-3 d-flex flex-column flex-lg-row text-center align-items-center justify-content-center">
        <p class="circle mr-lg-2 heading-regular">1</p>
        <p class="d-lg-flex align-items-lg-center">
          Choose <span class="d-none d-sm-block ml-lg-1">Your Car</span>
        </p>
      </div>
      <div class="col-sm-1 d-sm-flex align-items-sm-center justify-content-sm-center d-none d-sm-block px-sm-0">
        <div class="border w-100"></div>
      </div>
      <div
        class="col-6 col-sm-3 col-lg-3 d-flex flex-column flex-lg-row text-center align-items-center justify-content-center"
      >
        <p class="circle mr-lg-2 circle-active heading-semibold">2</p>
        <p class="d-lg-flex text-active heading-semibold align-items-lg-center"><span class="d-none d-sm-block mr-lg-1">View</span> Deal</p>
      </div>
      <div class="col-sm-1 d-sm-flex align-items-sm-center justify-content-sm-center d-none d-sm-block px-sm-0">
        <div class="border w-100"></div>
      </div>
      <div class="col-3 d-flex flex-column flex-lg-row text-center align-items-center justify-content-center">
        <p class="circle mr-lg-2 heading-regular">3</p>
        <p class="d-lg-flex align-items-lg-center">
          <span class="d-none d-sm-block mr-lg-1">Booking &amp;</span> Payment
        </p>
      </div>
    </div>
  </div>
 </div>
    <div class="dvCarDetails pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-md-5 col-lg-4">
                    <div class="border b-radius">
                        <div class="dvAccordian">
                            <div class="dvCommonAccordion accordion" id="accordionExample">
                                <div class="card">
                                    <div class="card-header p-0" id="headingOne">
                                        <h2 class="mb-0">
                                            <button class="btn btn-block text-left p-3" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                <span class="heading-bold h6" data-i18n="car-your-booking-summary">Your booking summary</span>
                                                <span class="arrow-icon">
                                                    <i class="fa fa-caret-up" aria-hidden="true"></i>
                                                </span>
                                            </button>
                                        </h2>
                                    </div>

                                    <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                                        <div class="card-body dvBooking">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="row">
                                                    <div class="col-1 pr-0 dvMap">
                                                        <i class="fa-solid fa-location-dot"></i>
                                                    </div>
                                                    <div class="col-11 mb-3 dvAddress">
                                                        <h2 class="mb-2 heading-semibold h6" data-i18n="car-pickup-from">Pick up from:</h2>
                                                        <p id="spnpickupDetails" class="h7"></p>
                                                        <p class="mb-2 heading-medium h8" id="spnpickupDate"></p>
                                                       
                                                     </div>   
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                     <div class="row">
                                                      <div class="col-1 pr-0 dvMap">
                                                          <i class="fa-solid fa-location-dot"></i>
                                                      </div>
                                                     <div class="col-11 mb-3 dvAddress">
                                                          <h2 class="mb-2 heading-semibold h7" data-i18n="car-drop-off-at"> Drop off at: </h2>
                                                          <p id="spndropoffDetails" class="h7">  </p>
                                                          <p class="mb-2 heading-medium h8" id="spndropoffDate"></p>
                                                     </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card" id="divCarDetailsLeftpannel">
                                     <%-- <div class="card-header" id="headingTwo">
                                        <h2 class="mb-0">
                                            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                Volkswagen Polo <span>(Economy)</span> <i class="fa fa-caret-down rotate" aria-hidden="true"></i>
                                            </button>
                                        </h2>
                                    </div>
                                    <div id="collapseTwo" class="collapse show" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                        <div class="card-body row">
                                            <div class="col-12 ">
                                                <div class="moreInfo" data-toggle="modal" data-target="#dvAdditionalEquipmentModal">
                                                    <i class="fa fa-info-circle" aria-hidden="true"></i>More info
                                                </div>
                                                <div class="carPicBox" data-toggle="modal" data-target="#dvAdditionalEquipmentModal">
                                                    <img class="img-fluid mt-auto mb-auto" src="https://cdn.enjoytravel.com/img/vehicleimages/volkswagen_polo.jpg" />
                                                </div>
                                                <div class="dvProvider">
                                                    <img class="img-fluid mt-auto mb-auto" src="https://cdn.enjoytravel.com/img/logos/ace-logo.svg" width="100" />
                                                </div>
                                            </div>
                                            <div class="col-12 mt-4">
                                                <div class="row">
                                                    <div class="col-6 pb-2">
                                                        <div class="cardDetails">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                            <p>Unlimited Mileage</p>
                                                        </div>
                                                    </div>
                                                    <div class="col-6 pb-2">
                                                        <div class="cardDetails">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                            <p>Stay Safe Initiative</p>
                                                        </div>
                                                    </div>
                                                    <div class="col-6 pb-2">
                                                        <div class="cardDetails">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                            <p>Other taxes and service charges</p>
                                                        </div>
                                                    </div>
                                                    <div class="col-6 pb-2">
                                                        <div class="cardDetails">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                            <p>Collision damage waiver</p>
                                                        </div>
                                                    </div>
                                                    <div class="col-6 pb-2">
                                                        <div class="cardDetails">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                            <p>Theft protection</p>
                                                        </div>
                                                    </div>
                                                    <div class="col-6 pb-2">
                                                        <div class="cardDetails">
                                                            <i class="fa fa-check" aria-hidden="true"></i>
                                                            <p>Unlimited Travel</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-2">
                                                <div class="row dviconBox">
                                                    <div class="col-6 col-md-6 d-flex mb-1 mt-2 align-items-center">
                                                        <div class="borderColor">
                                                            <img src="images/icon/seat-icon.svg" class="img-fluid" />
                                                        </div>
                                                        <span class="ml-2">x 5</span>
                                                    </div>
                                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                                        <div class="borderColor">
                                                            <img src="images/icon/gear-icon.svg" class="img-fluid" />
                                                        </div>
                                                        <span class="ml-2">Automatic</span>
                                                    </div>
                                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                                        <div class="borderColor">
                                                            <img src="images/icon/door-icon.svg" class="img-fluid" />
                                                        </div>
                                                        <span class="ml-2">2-4</span>
                                                    </div>
                                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                                        <div class="borderColor">
                                                            <i class="fa fa-bus" aria-hidden="true"></i>
                                                        </div>
                                                        <span class="ml-2">Shuttle </span>
                                                    </div>
                                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                                        <div class="borderColor">
                                                            <img src="images/icon/ac-icon.svg" class="img-fluid" />
                                                        </div>
                                                        <span class="ml-2">AirCon</span>
                                                    </div>
                                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                                        <div class="borderColor">
                                                            <img src="images/icon/fuel-icon.svg" class="img-fluid" />
                                                        </div>
                                                        <span class="ml-2">Fair Fuel Policy</span>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>

                            </div>
                            <div class="dvPricing py-2 px-3">
                                <p class="heading-semibold" data-i18n="car-pricing-summary">Pricing Summary</p>
                            </div>
                            <div id="divPricingSummary">
                                <div class="d-flex justify-content-between align-items-center pt-2 pb-2">
                                    <div class="col-6">
                                        <p class="heading-medium h7" data-i18n="car-hire">Car Hire:</p>
                                    </div>
                                    <div class="col-6">
                                        <p class="text-right heading-medium h7"><span id="spncarhireAmount">0</span> <span data-i18n="car-points">Points</span></p>
                                    </div>
                                </div>

                                <div id="divAdditionaCharges">
                                </div>

                                <div id="AdditionalChrg" class="d-flex justify-content-between align-items-center pt-2 pb-3">
                                    <div class="col-6">
                                        <p class="heading-medium h7" data-i18n="car-Total">Total:</p>
                                    </div>
                                    <div class="col-6">
                                        <p class="text-right heading-medium h7"><span id="spncarTotalAmount">0</span>  Points</p>
                                    </div>
                                </div>
                                <div class="dvPayable">
                                    <div class="border-top p-3">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <div class="col-7 pl-0">
                                                <p class="heading-bold text-colour1" data-i18n="car-payable-today">Payable today:</p>
                                            </div>
                                            <div class="col-5 pr-0">
                                                <p class="text-right heading-bold text-colour1"><span class="text-right" id="spnPayableAmount">0</span>  <span data-i18n="car-points">Points</span></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               <%-- <div id="AdditionalChrgtotal" class="d-flex justify-content-between align-items-center pt-2 pb-3">
                                    <div class="col-6">
                                        <p>Remainder to pay for car hire on arrival is made in LOCAL currency:</p>
                                    </div>
                                    <div class="col-6">
                                        <p class="font-weight-bold"><span id="spnAdditionalChargetotal">0</span> <i class="fa fa-usd" aria-hidden="true"></i></p>
                                    </div>
                                </div>--%>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-7 col-lg-8">
                    <div class="row">
                        <div class="col-12 mt-3 mt-md-0 dvPayOption">
                            <h2 class="heading-bold h5"><i class="fa fa-lock" aria-hidden="true"></i> <span data-i18n="car-payment-options">Payment Options</span></h2>
                        </div>

                    </div>

                    <div>
                        <div class="row" id="divpaymentOptionContainer">
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-12 col-md-12 mt-4 ">
                            <input type="hidden" name="Ratereference" id="hndRatereference" value="" />
                            <div class="travelBtn">
                                <button type="button" class="btn btn-one" onclick="CreateCarPayment();" data-i18n="car-continue-payment">Continue to Payment <i class="fa fa-caret-right" aria-hidden="true"></i></button>
                            </div>
                        </div>
                    </div>
                    <input type="hidden" name="IsExcessprotectionAdded" id="hndIsExcessprotectionAdded" value="" />
                    <div id="divExtrascontainer">
                    </div>


                    <div class="row mt-4">
                        <div class="col-12 dvAddit">
                            <p class="heading-bold h6" data-i18n="car-addiotional-equipment">Additional Equipment</p>
                        </div>
                    </div>
                    <div class="mt-1 border boxShadow b-radius">
                        <div class="row">
                            <div class="col-12">
                                <div class="p-3">
                                    <div class="row">
                                        <div class="col-12 productInfo position-relative">
                                            <div class="row">
                                                <div class="col-12 dvExcess">
                                                    <div class="row pt-2">
                                                        <div class="col-12 dvPara pb-3">
                                                            <p data-i18n="car-addiotional-info">Please note these additional extras are payable locally and do not form part of the rental price shown. Prices are displayed by pressing the title of each extra.</p>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <input type="hidden" name="Totaladitionalchargeamount" id="hndTotaladitionalchargeamount" value="0" />
                                            <div id="divAdditionalEquipment">
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 col-md-12 mt-4 ">
                            <div class="travelBtn">
                                <button type="button" class="btn btn-one" onclick="CreateCarPayment();"><span data-i18n="car-continue-payment">Continue to Payment</span> <i class="fa fa-caret-right" aria-hidden="true"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- CarLarge modal pop up start-->
    <div class="dvCommonModal dvMoreInfoModal modal fade pr-lg-0" id="dvMoreInfoModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header border-0">
                    <h5 class="modal-title">
                        <i class="fa-solid fa-circle-info"></i>
                        <span data-i18n="carlist-important-information">Important information</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                  </div>

                <!-- Modal body -->
                <div class="modal-body mb-2" id="divmoreInfoDetails">

                    <%--<div class="modal-body mb-2">
                        <div class="row">
                            <div class="col-12 col-md-6">
                                <div class="carPicBox">
                                    <img class="img-fluid mt-auto mb-auto" src="https://cdn.enjoytravel.com/img/vehicleimages/volkswagen_polo.jpg" />
                                </div>

                            </div>
                            <div class="col-12 col-md-6">
                                <div class="carHead">
                                    <p>Volkswagen Polo</p>
                                    <span>or similar (Small)</span>
                                </div>
                                <div class="row dviconBox">
                                    <div class="col-6 col-md-6 d-flex mb-1 mt-2 align-items-center">
                                        <div class="borderColor">
                                            <img src="images/icon/seat-icon.svg" class="img-fluid" />
                                        </div>
                                        <span class="ml-2">x 5</span>
                                    </div>
                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                        <div class="borderColor">
                                            <img src="images/icon/gear-icon.svg" class="img-fluid" />
                                        </div>
                                        <span class="ml-2">Automatic</span>
                                    </div>
                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                        <div class="borderColor">
                                            <img src="images/icon/door-icon.svg" class="img-fluid" />
                                        </div>
                                        <span class="ml-2">2-4</span>
                                    </div>
                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                        <div class="borderColor">
                                            <i class="fa fa-bus" aria-hidden="true"></i>
                                        </div>
                                        <span class="ml-2">Shuttle </span>
                                    </div>
                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                        <div class="borderColor">
                                            <img src="images/icon/ac-icon.svg" class="img-fluid" />
                                        </div>
                                        <span class="ml-2">AirCon</span>
                                    </div>
                                    <div class="col-6 col-md-6 mb-1 mt-2 d-flex align-items-center">
                                        <div class="borderColor">
                                            <img src="images/icon/fuel-icon.svg" class="img-fluid" />
                                        </div>
                                        <span class="ml-2">Fair Fuel Policy</span>
                                    </div>

                                </div>
                                <div class="dvDeal mt-3">
                                    <h1>YOUR DEAL</h1>
                                    <p>154,93 <i class="fa fa-inr" aria-hidden="true"></i><span>(22,17 € a day)</span></p>
                                </div>

                            </div>
                        </div>
                        <div class="col-12 border-top pt-3 mt-3 pr-0 pl-0 productDetails">
                            <div class="d-flex flex-wrap justify-content-between align-items-center">
                                <div class="col-6 col-md-6 order-md-0">
                                    <div class="carLogo">
                                        <img class="img-fluid mt-auto mb-auto" src="\images/giift-logo.svg" alt="Logo">
                                    </div>
                                </div>
                                <div class="col-6 col-md-6 mt-2 mt-md-0 order-md-1 text-left">
                                    <div class="vehicleLocation">
                                        <a href="#"><i class="fa fa-map-marker" aria-hidden="true"></i>Vehicle location:</a>
                                        <span>DUBAI INTL AIRPORT TERMINAL 3, 154 AIRPORT ROAD, DUBAI, 21971</span>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-12 mt-4">
                                    <div class="row">
                                        <div class="col-6 col-md-4 pb-2">
                                            <div class="cardDetails">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                <p>Unlimited Mileage</p>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md-4 pb-2">
                                            <div class="cardDetails">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                <p>Stay Safe Initiative</p>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md-4 pb-2">
                                            <div class="cardDetails">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                <p>Other taxes and service charges</p>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md-4 pb-2">
                                            <div class="cardDetails">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                <p>Collision damage waiver</p>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md-4 pb-2">
                                            <div class="cardDetails">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                <p>Theft protection</p>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md-4 pb-2">
                                            <div class="cardDetails">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                <p>Unlimited Travel</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>

    <!-- child popup modal pop up end-->
    <div class="dvCommonModal modal fade" id="dvAdditionalEquipmentModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                 <div class="modal-header border-0">
                      <h5 class="modal-title">
                        <i class="fa-solid fa-circle-info"></i>
                        <span>Info</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
             
                <!-- Modal body -->
                <div class="modal-body">
                    <%--<div class="row">
                        <div class="col-12 col-md-6 ml-3">
                            <div class="carPicBox">
                                <img class="img-fluid mt-auto mb-auto" src="" />
                            </div>
                        </div>
                    </div>--%>

                    <div id="divMoreinfoAdditionalEquipment"></div>
                    
                   <%-- <div class="col-12 pt-3 mt-3 pr-0 pl-0 productDetails">
                        <div class="d-flex dvpopupBooster flex-wrap justify-content-between align-items-center mt-2">
                            <div class="col-12 col-md-6 order-md-0 ">
                                <p><i class="fa fa-clipboard" aria-hidden="true"></i>Infant Seat (0-1 year)</p>
                            </div>
                            <div class="col-12 col-md-6 mt-2 mt-md-0 order-md-1 text-left">
                                <span>44,10 <i class="fa fa-inr" aria-hidden="true"></i></span>
                            </div>
                        </div>
                        <div class="row dvSeat mt-2">
                            <div class="col-12">
                                <p class="p-3">Infant Seat (0-1 year)</p>
                            </div>

                        </div>
                    </div>--%>

                </div>
            </div>
        </div>
    </div>

    <!-- Add popup modal pop up end-->
    <div class="dvCommonModal modal fade" id="dvExcessProtectionModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                 <div class="modal-header border-0">
                      <h5 class="modal-title">
                            <i class="fa-solid fa-circle-info"></i>
                            <span>Info</span>
                        </h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <i class="fa-solid fa-xmark"></i>
                        </button>
                </div>
               
                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12 col-md-12 dvWarning text-center">
                            <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                            <h1 data-i18n="car-data-warning">Warning! You do not have Excess Protection</h1>
                            <p>
                                <i class="fa fa-exclamation-triangle" aria-hidden="true"></i><span data-i18n="car-excess">Excess:</span>
                                <p id="spnExcessAmount">0</p>
                            </p>
                            <span data-i18n="car-policy-text">Have peace of mind and protect yourself against any charges that maybe applied if you damage the vehicle by taking Enjoy's excess protection.</span>
                        </div>
                        <div class="col-12 col-md-12 ml-3 dvExPoint text-center">
                            <p><i class="fa-solid fa-shield-halved"></i> <span data-i18n="car-excess">Excess:</span><span> <span id="spnExcessAmount1">0</span> <i class="fa fa-usd" aria-hidden="true"></i></span></p>
                        </div>

                    </div>
                    <div class="d-flex justify-content-center align-items-center mb-4">
                        <div class="col-6 col-md-6 col-lg-3 mt-4 travelGrayBtn">
                            <button type="button" class="hvr-sweep-to-right w-100" onclick="window.location.href='CarPayment.aspx'" data-dismiss="modal" data-i18n="car-no-thanks">No Thanks</button>
                        </div>

                        <div class="col-6 col-md-6 col-lg-3 mt-4 travelBtn">
                            <button type="button" class="hvr-sweep-to-right" onclick="AddExcessprotection();" data-i18n="car-add">ADD</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        // JavaScript logic to show/hide the image based on radio button state
        document.addEventListener("DOMContentLoaded", function () {
            var radioButtons = document.querySelectorAll('input[type="radio"]');
            var images = document.querySelectorAll(".correctIcon");

            radioButtons.forEach(function (radioButton, index) {
                radioButton.addEventListener("change", function () {
                    images.forEach(function (image, i) {
                        image.style.display = index === i && radioButton.checked ? "inline-block" : "none";
                    });
                });
            });
            //if (document.getElementById('myRadiobox').checked) {
            //    document.getElementById('correctIcon').style.display = 'block';
            //}
            //else {
            //    document.getElementById('correctIcon').style.display = 'none';
            //}
        });
    </script>

</asp:Content>
