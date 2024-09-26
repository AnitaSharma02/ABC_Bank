<%@ Page Title="Car Payment" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarPayment.aspx.cs" Inherits="CarPayment" %>

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
            MapCarDetails();
        });
    </script>


    <div class="dvCarPayment py-3 pb-lg-5">
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
                            <div class="d-flex flex-column flex-sm-row align-items-center">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">2</span>
                                <a class="h7 bg-colour6 px-3 text-center text-colour7" id="hrefBookingDetailsId" runat="server">Deal</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4 mb-lg-3">
                    <%--<div class="dvLine border d-none d-md-block px-3"></div>--%>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center active">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">3</span>
                                <a class="h7 heading-bold bg-colour6 px-3 text-center text-colour1">Payment</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5 col-lg-4 mb-3">
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
                                <div class="dvVcData card" id="divCarDetailsLeftpannel">
                                </div>

                            </div>
                            <div class="dvPricing py-2 px-3">
                                <p class="heading6">Pricing Summary</p>
                            </div>
                            <div class="d-flex justify-content-between align-items-center pt-2 pb-2">
                                <div class="col-6">
                                    <p class="h7">Car Hire:</p>
                                </div>
                                <div class="col-6">
                                    <p class="text-right h7"><span id="spncarhireAmount">0</span> Points</p>
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
                    </div>
                </div>
                <div class="col-md-7 col-lg-8">

                    <div class="border b-radius my-3 mt-md-0">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour1 b-radius-top-right p-3">
                                    <h2 class="heading6 text-colour6"><i class="fa-regular fa-circle-user mr-2"></i><span>Driver Details</span></h2>
                                </div>
                                <div class="p-3">
                                    <div class="row">
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label">First name*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="dvErrors text-danger" id="errorFirtsname"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label">Surname*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtSurName" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="dvErrors text-danger" id="errorSurName"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label">E-mail Address*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtEmailId" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="dvErrors text-danger" id="errorEmailId"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label">Phone number*</label>
                                            <div class="dvInput input-group">
                                                <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <p class="dvErrors text-danger" id="errorMobileNo"></p>
                                        </div>
                                        <div class="col-12 col-sm-6 mb-3">
                                            <label for="#" class="label">Flight number*</label>
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
                                                    <span class="d-inline-block ml-2"><span>I have read and accept the</span>
                                                        <a href="TermsAndConditions.aspx" target="_blank" class="link1">Terms & Conditions</a></span>
                                                </label>
                                            </div>
                                            <p class="dvErrors text-danger mb-2" id="errorTnC"></p>
                                            <asp:Button ID="btnMakePayment" runat="server" Value="Make Payment" OnClick="btnMakePayment_Click" OnClientClick="return MakePayment();" CssClass="btn btn-one"></asp:Button>

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
    <div class="dvCommonModal dvMoreInfoModal modal fade" id="dvMoreInfoModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">
                 <!-- Modal Header -->
                 <div class="modal-header border-0">
                       <h5 class="modal-title">
                            <span>Important Information</span>
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
    <!-- CarLarge modal pop up end-->

</asp:Content>
