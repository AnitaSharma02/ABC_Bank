<%@ Page Title="Car Payment" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarPayment.aspx.cs" Inherits="CarPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/dropDown.css" type="text/css" rel="stylesheet" />
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/car.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/CarResultScript.js" type="text/javascript"></script>
    <style> 

        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        } 
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            MapCarDetails();

        });

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
                    class="col-6 col-sm-3 col-lg-3 d-flex flex-column flex-lg-row text-center align-items-center justify-content-center">
                    <p class="circle mr-lg-2 heading-semibold">2</p>
                    <p class="d-lg-flex align-items-lg-center"><span class="d-none d-sm-block mr-lg-1">View</span> Deal</p>
                </div>
                <div class="col-sm-1 d-sm-flex align-items-sm-center justify-content-sm-center d-none d-sm-block px-sm-0">
                    <div class="border w-100"></div>
                </div>
                <div class="col-3 d-flex flex-column flex-lg-row text-center align-items-center justify-content-center">
                    <p class="circle mr-lg-2 circle-active heading-regular">3</p>
                    <p class="d-lg-flex text-active heading-semibold align-items-lg-center">
                        <span class="d-none d-sm-block mr-lg-1">Booking &amp;</span> Payment
                    </p>
                </div>
            </div>
        </div>
    </div>
    <div class="dvCarPayment dvCarDetails">
        <div class="container-lg">
            <div class="row">
                <div class="col-md-5 col-lg-4 mb-3">
                    <div class="border rounded-lg">
                        <div class="dvAccordian">
                            <div class="dvCommonAccordion accordion" id="accordionExample">
                                <div class="card">
                                    <div class="card-header p-0" id="headingOne">
                                        <h2 class="mb-0">
                                            <button class="btn btn-block text-left p-3" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                <span class="heading-bold h7" data-i18n="car-your-booking-summary">Your booking summary</span>
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
                                                            <h2 class="mb-2 heading-semibold h7"><span data-i18n="car-pickup-from">Pick up from</span>:</h2>
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
                                                            <h2 class="mb-2 heading-semibold h7" data-i18n="car-drop-off-at">Drop off at: </h2>
                                                            <p id="spndropoffDetails" class="h7"></p>
                                                            <p class="mb-2 heading-medium h8" id="spndropoffDate"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card" id="divCarDetailsLeftpannel">
                                </div>

                            </div>
                            <div class="dvPricing py-2 px-3">
                                <p class="heading-semibold" data-i18n="car-pricing-summary">Pricing Summary</p>
                            </div>
                            <div class="d-flex justify-content-between align-items-center pt-2 pb-2">
                                <div class="col-6">
                                    <p class="heading-medium h7" data-i18n="car-hire">Car Hire:</p>
                                </div>
                                <div class="col-6">
                                    <p class="text-right heading-medium h7"><span id="spncarhireAmount">0</span> Points</p>
                                </div>
                            </div>

                            <div id="divAdditionaCharges">
                            </div>

                            <div class="d-flex justify-content-between align-items-center pt-2 pb-2">
                                <div class="col-6">
                                    <p class="heading-medium h7" data-i18n="car-Total">Total:</p>
                                </div>
                                <div class="col-6">
                                    <p class="text-right heading-medium h7"><span id="spncarTotalAmount">0</span> Points</p>
                                </div>
                            </div>
                            <div class="dvPayable ">
                                <div class="border-top p-3">
                                    <div class="d-flex justify-content-between align-items-center pt-2 pb-2">
                                        <div class="col-6 pl-0">
                                            <p class="heading-bold text-colour1" data-i18n="car-payable-today">Payable today:</p>
                                        </div>
                                        <div class="col-6 pr-0">
                                            <p class="text-right heading-bold text-colour1"><span class="text-right" id="spnPayableAmount">0</span> Points</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-7 col-lg-8">

                    <div class="border boxShadow rounded-lg my-3 mt-md-0">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour2 rounded-top p-3">
                                    <h2 class="heading-bold h6"><i class="fa-regular fa-circle-user text-colour8"></i>  <span data-i18n="car-driver-details">Driver Details</span></h2>
                                </div>
                                <div class="p-3">
                                    <div class="row">
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label" data-i18n="car-first-name">First name*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="text-danger" id="errorFirtsname"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label" data-i18n="car-surname">Surname*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtSurName" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="text-danger" id="errorSurName"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label" data-i18n="car-email-address">E-mail Address*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtEmailId" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="text-danger" id="errorEmailId"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label" data-i18n="car-phone-number">Phone number*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="text-danger" id="errorMobileNo"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label" data-i18n="car-flight-number">Flight number*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtFlightNo" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="dvLabel d-flex justify-content-between mb-2">
                                                <label class="checkbox-container d-flex align-items-center">
                                                    <span class="d-inline-block">
                                                        <input type="checkbox" id="chkTnC" />
                                                        <span class="checkmark"></span>
                                                    </span>
                                                    <span class="d-inline-block ml-2"><span data-i18n="car-have-read-accept">I have read and accept the</span>
                                                  <a href="TermsAndConditions.aspx" target="_blank" class="heading-semibold text-colour7" data-i18n="car-terms">Terms & Conditions</a></span>
                                                </label>
                                            </div>
                                            <p class="text-danger mb-2" id="errorTnC"></p>
                                            <asp:Button ID="btnMakePayment" runat="server" data-i18n="[value]btn-make-payment" Value="Make Payment" OnClick="btnMakePayment_Click" OnClientClick="return MakePayment();" CssClass="btn btn-one"> </asp:Button>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


        </div>
    </div>
    </div>
    </div>
    <!-- CarLarge modal pop up start-->
    <div class="dvCommonModal dvMoreInfoModal modal fade pr-lg-0" id="myModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                 <!-- Modal Header -->
                 <div class="modal-header border-0">
                      <div class="modal-title border-0">
                          <h5 class="h6 heading-semibold text-colour1" data-i18n="carlist-important-information">Important information</h5>
                       </div>
                          <button type="button" class="close px-3" data-dismiss="modal" aria-label="Close">
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
    <!-- CarLarge modal pop up end-->

</asp:Content>
