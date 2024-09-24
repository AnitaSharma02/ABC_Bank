<%@ Page Title="Car Details" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarDetails.aspx.cs" Inherits="CarDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
      <%--<link href="Css/dropDown.css" type="text/css" rel="stylesheet" />
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />--%>
    <link href="Css/Car.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/CarResultScript.js" type="text/javascript"></script>
    <style>
     #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
         display: none;
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

    <div class="dvCarDetails py-3 pb-lg-5">
        <div class="container-xl">
            <div class="row dvDeliveryTrack">
                <div class="col-4 mb-lg-3">
                    <div class="dvLine border d-none d-md-block px-3"></div>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">1</span>
                                <a class="h7 bg-colour6 px-3 text-center text-colour7">Your Car</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4 mb-lg-3">
                    <div class="dvLine border d-none d-md-block px-3"></div>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center active">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">2</span>
                                <a class="h7 heading-bold bg-colour6 px-3 text-center text-colour1" id="hrefBookingDetailsId" runat="server">Deal</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4 mb-lg-3">
                    <%--<div class="dvLine border d-none d-md-block px-3"></div>--%>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">3</span>
                                <a class="h7 bg-colour6 px-3 text-center text-colour7">Payment</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="border b-radius p-0">
                        <div class="dvCommonAccordion accordion" id="accordionExample">
                            <div class="card">
                                <div class="card-header p-0" id="headingOne">
                                    <h2 class="mb-0">
                                        <button class="btn btn-block text-left p-3" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            <span class="heading-bold h6">Your booking summary</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up" aria-hidden="true"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>

                                <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                                    <div class="card-body p-3">
                                        <div class="row">
                                            <div class="col-12 mb-3">
                                                <h2 class="heading6"><i class="fa-solid fa-location-dot mr-2"></i><span>Pick up from:</span></h2>
                                                <p id="spnpickupDetails" class="h7"></p>
                                                <p class="h7" id="spnpickupDate"></p>
                                            </div>
                                            <div class="col-12">
                                                <h2 class="heading6"><i class="fa-solid fa-location-dot mr-2"></i><span>Drop off at:</span></h2>
                                                <p id="spndropoffDetails" class="h7"></p>
                                                <p class="h7" id="spndropoffDate"></p>
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
                                        More info
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
                            <p class="heading6">Pricing Summary</p>
                        </div>
                        <div id="divPricingSummary">
                            <div class="d-flex justify-content-between align-items-center pt-2 pb-2">
                                <div class="col-6">
                                    <p class="h7">Car Hire:</p>
                                </div>
                                <div class="col-6">
                                    <p class="text-right h7"><span id="spncarhireAmount">0</span> <span>Points</span></p>
                                </div>
                            </div>

                            <div id="divAdditionaCharges">
                            </div>

                            <div id="AdditionalChrg" class="d-flex justify-content-between align-items-center pt-2 pb-3">
                                <div class="col-6">
                                    <p class="h7">Total:</p>
                                </div>
                                <div class="col-6">
                                    <p class="text-right h7"><span id="spncarTotalAmount">0</span>  Points</p>
                                </div>
                            </div>
                            <div class="dvPayable">
                                <div class="border-top p-3">
                                    <div class="d-flex justify-content-between align-items-center">
                                        <div class="col-7 pl-0">
                                            <p class="heading6 text-colour1">Payable today:</p>
                                        </div>
                                        <div class="col-5 pr-0">
                                            <p class="text-right heading6 text-colour1"><span class="text-right" id="spnPayableAmount">0</span>  <span>Points</span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <div id="AdditionalChrgtotal" class="d-flex justify-content-between align-items-center pt-2 pb-3">
                        <div class="col-6">
                            <p>Remainder to pay for car hire on arrival is made in LOCAL currency:</p>
                        </div>
                        <div class="col-6">
                            <p class="heading-bold"><span id="spnAdditionalChargetotal">0</span> <i class="fa fa-usd" aria-hidden="true"></i></p>
                        </div>
                    </div>--%>
                        </div>

                    </div>
                </div>
                <div class="col-lg-8">
                    <div class="row">
                        <div class="col-12 mt-3 mt-md-0 dvPayOption">
                            <h2 class="heading6"><i class="fa fa-lock mr-2" aria-hidden="true"></i><span>Payment Options</span></h2>
                        </div>
                    </div>
                    <div class="row" id="divpaymentOptionContainer">
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-12 mt-4 d-none">
                            <input type="hidden" name="Ratereference" id="hndRatereference" value="" />
                            <button type="button" class="btn btn-one" onclick="CreateCarPayment();">Continue to Payment</button>
                        </div>
                    </div>
                    <input type="hidden" name="IsExcessprotectionAdded" id="hndIsExcessprotectionAdded" value="" />
                    <div class="row" id="divExtrascontainer">
                    </div>
                    <div class="row mt-4">
                        <div class="col-12">
                            <div class="border b-radius p-3">
                                <h2 class="heading6 mb-2">Additional Equipment</h2>
                                <p>Please note these additional extras are payable locally and do not form part of the rental price shown. Prices are displayed by pressing the title of each extra.</p>
                                <input type="hidden" name="Totaladitionalchargeamount" id="hndTotaladitionalchargeamount" value="0" />
                                <div id="divAdditionalEquipment">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 mt-4 text-center text-md-left">
                            <button type="button" class="btn btn-one" onclick="CreateCarPayment();"><span>Continue to Payment</span></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="dvCommonModal dvMoreInfoModal modal fade" id="dvMoreInfoModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title">
                        <span>Important information</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body mb-2" id="divmoreInfoDetails"></div>
            </div>
        </div>
    </div>

    <div class="dvCommonModal modal fade" id="dvAdditionalEquipmentModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">
                 <div class="modal-header border-0">
                      <h5 class="modal-title">
                        <span>Info</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="divMoreinfoAdditionalEquipment"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="dvCommonModal modal fade" id="dvExcessProtectionModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title">
                        <span>Info</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12 col-md-12 dvWarning text-center">
                            <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                            <h1>Warning! You do not have Excess Protection</h1>
                            <p>
                                <i class="fa fa-exclamation-triangle" aria-hidden="true"></i><span>Excess:</span>
                                <p id="spnExcessAmount">0</p>
                            </p>
                            <span>Have peace of mind and protect yourself against any charges that maybe applied if you damage the vehicle by taking Enjoy's excess protection.</span>
                        </div>
                        <div class="col-12 col-md-12 ml-3 dvExPoint text-center">
                            <p><i class="fa-solid fa-shield-halved"></i> <span>Excess:</span><span> <span id="spnExcessAmount1">0</span> <i class="fa fa-usd" aria-hidden="true"></i></span></p>
                        </div>
                    </div>
                    <div class="d-flex justify-content-center align-items-center mb-4">
                        <div class="col-6 col-md-6 col-lg-3 mt-4 travelGrayBtn">
                            <button type="button" class="hvr-sweep-to-right w-100" onclick="window.location.href='CarPayment.aspx'" data-dismiss="modal">No Thanks</button>
                        </div>
                        <div class="col-6 col-md-6 col-lg-3 mt-4 travelBtn">
                            <button type="button" class="hvr-sweep-to-right" onclick="AddExcessprotection();">ADD</button>
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
        });
    </script>

</asp:Content>
