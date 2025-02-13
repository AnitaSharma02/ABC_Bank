<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarVoucher.aspx.cs" Inherits="CarVoucher"
    MasterPageFile="~/SiteMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="server">
    <link href="Css/Car.css" rel="stylesheet" type="text/css" />

    <style>
        .dvRedemptionMenu {
            display:none;
        }
    </style>

    <div class="container-xl my-3">
        <div class="row">
            <div class="col-12">
                <div class="border-top border-left border-right px-3 pt-3">
                    <div class="bg-colour2 p-3">
                        <img width="160" src="Images/logos/infinity-logo.svg" alt="" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="OrderDetailsDiv" runat="server">
            <div class="col-12">
                <div class="border-left border-right border-bottom px-3 pb-3">
                    <div class="row">
                        <div class="col-12">
                            <h2 class="h5 heading-semibold text-colour7 my-3">Congratulations for your Infinity Rewards Ticket! Your Current status for Car Booking is <span class="text-colour1">
                                <asp:Label ID="lblBookingStatus" runat="server" Text="NIL"></asp:Label>*.</span>
                            </h2>
                            <p>
                                <span class="h6 heading-semibold text-colour7">Reference No : </span>
                                <asp:Label ID="lblbookingReferenceNo" runat="server" Text="NIL"></asp:Label>
                            </p>
                            <p>
                                <span class="h6 heading-semibold text-colour7">Payment Info : </span>
                                <span id="PaymentInfo" runat="server"></span>
                            </p>
                            <p class="mt-3 heading-semibold text-colour7">
                                Thank you
                          <asp:Label ID="lblMemberName" runat="server" Text="NIL"></asp:Label>
                                (Infinity Rewards ID :-&nbsp;<asp:Label ID="lblMembershipReference" runat="server" Text="NIL"></asp:Label>) for using Infinity Reward Points to book your car. Please use your Reference ID for any communication
                          pertaining to this booking.
                            </p>
                            <p style="display: none">
                                <span class="h6 heading-semibold text-colour7">Supplier ID : </span>
                                <span>
                                    <asp:Label ID="lblbookingId" runat="server" Text="NIL"></asp:Label></span>
                            </p>
                        </div>
                    </div>

                    <div class="dvBookingDetails row mt-3">
                        <div class="col-12">
                            <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Booking Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-sm-6 col-md-3 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Pick-Up</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p>
                                                    <asp:Label ID="lblpickUp" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-3 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Pick-Up Date</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblpickUpDate" runat="server" Text="NIL"></asp:Label>
                                                    at
                                                <asp:Label ID="lblPickUpTime" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-3 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Drop-Off</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lbldropOff" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-3 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Drop-Off Date</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lbldropOffDate" runat="server" Text="NIL"></asp:Label>
                                                    at
                                                <asp:Label ID="lbldropOffTime" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-6 col-md-3">
                                    <div class="row">
                                        <div class="col-12 bg-colour2 p-2">
                                            <h2 class="h7 heading-semibold text-colour7">Email</h2>
                                        </div>
                                        <div class="col-12 p-2">
                                            <p class="text-break">
                                                <asp:Label ID="lblCustomerEmail" runat="server" Text="Label"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="dvCarDetails row mt-3">
                        <div class="col-12">
                            <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Car Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Car Name</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break">
                                                    <asp:Label ID="lblcarName" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Transmission type</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblTransmissionType" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 col-md-3 border-right">
                                        <div class="row" id="Div1" runat="server">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Air Condition</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblAirCondition" runat="server" Text="No"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="dvDriverDetails row mt-3">
                        <div class="col-12">
                            <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Driver Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Name</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break">
                                                    <asp:Label ID="lblDriverTitle" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblDriverFName" runat="server" Text="NIL"></asp:Label>
                                                    <asp:Label ID="lblDriverLName" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Address</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblAddress" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 col-md-3 border-right">
                                        <div class="row" id="Div2" runat="server">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Phone Number</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblPhoneNo" runat="server" Text="No"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="dvPickupLocationDetails row mt-3">
                        <div class="col-12">
                            <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Pick-Up Location Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Pick-Up</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break">
                                                    <asp:Label ID="lblPickupPlace" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">City</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblCityName" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 col-md-3 border-right">
                                        <div class="row" id="Div3" runat="server">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Country</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblCountry" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="dvDropOffLocationDetails row mt-3">
                        <div class="col-12">
                            <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Drop-Off Location Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Drop-Off</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break">
                                                    <asp:Label ID="lblDropOffLoc" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">City</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblDropOffCity" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 col-md-4 col-md-3 border-right">
                                        <div class="row" id="Div4" runat="server">
                                            <div class="col-12 bg-colour2 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Country</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblDropOffCon" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row mt-3">
                        <div class="col-12">
                            <p class="mb-3">
                                In case your booking status is not yet confirmed, you will receive a confirmation message via email within
      the next 24 hours of your booking. If you do not receive the confirmation message, all your Infinity
      Reward Points will be refunded back into your account.
                            </p>
                            <h2 class="h5 heading-semibold text-colour7">We thank you for using Infinity Reward Points and wish you a safe journey.</h2>
                        </div>
                        <div class="col-12 mt-3 d-none">
                            <div class="row">
                                <div class="col-12 text-end">
                                    <a onclick="window.open('PrintCarVoucher.aspx')" class="btn btn-one">PRINT</a>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
