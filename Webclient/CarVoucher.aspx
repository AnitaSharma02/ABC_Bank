<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarVoucher.aspx.cs" Inherits="CarVoucher"
    MasterPageFile="~/SiteMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="server">
    <link href="Css/Car.css" rel="stylesheet" type="text/css" />

    <style>
        .dvRedemptionMenu {
            display:none;
        }
    </style>

    <div class="container-lg my-3">
        <div class="row">
            <div class="col-12">
                <div class="border p-3">
                   <%-- <div class="row">
                        <div class="col-12">
                            <div class="bg-colour3 p-3">
                                <img class="img-fluid" src="../images/logos/infinity-logo.svg" alt="" />
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-12">
                            <h2 class="h5 heading-semibold text-colour7 mb-3">Congratulations for your Infinity Rewards Ticket!</h2>
                            <p>
                                Reference No:
                                <asp:Label ID="lblbookingReferenceNo" runat="server" Text="NIL" CssClass="heading-bold"></asp:Label>
                            </p>
                            <p>Payment Info: <span id="PaymentInfo" runat="server" class="heading-bold"></span></p>
                            <p class="my-3">
                                Thank you
                                <asp:Label ID="lblMemberName" runat="server" Text="NIL"></asp:Label>
                                (Infinity Rewards ID-:
                                    <asp:Label ID="lblMembershipReference" runat="server" Text="NIL"></asp:Label>) for
                                using Infinity Reward Points to book your car. Please use your Reference ID for any communication
                                pertaining to this booking.
                            </p>
                            <p>
                                Your Current status for Car Booking is <span class="heading-bold">
                                <asp:Label ID="lblBookingStatus" runat="server" Text="NIL"></asp:Label>*.</span>
                            </p>
                            <p style="display: none;">
                                Supplier ID: <span class="heading-bold">
                                    <asp:Label ID="lblbookingId" runat="server" Text="NIL"></asp:Label></span>
                            </p>
                        </div>
                    </div>
                    <%--<UC:ItineraryDetails ID="ucItinarary" runat="server" />--%>
                    <div class="row dvAdditionalDetails mt-3">
                        <div class="col-12 mb-3">
                            <h2 class="h7 heading-semibold text-colour7 bg-colour3 py-3 px-2 border">Booking Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-12 col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Pick-Up</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break h7">
                                                    <asp:Label ID="lblpickUp" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 col-sm-8 col-md-2 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Pick-Up Date</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblpickUpDate" runat="server" Text="NIL"></asp:Label>
                                                    at
                                                    <asp:Label ID="lblPickUpTime" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-6 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Drop-Off</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lbldropOff" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-6 col-md-2 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7" data-i18n="flightpassenger-gender">Drop-Off Date</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lbldropOffDate" runat="server" Text="NIL"></asp:Label>
                                                    at
                                                    <asp:Label ID="lbldropOffTime" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-8 col-sm-8 col-md-2">
                                     <div class="row">
                                         <div class="col-12 bg-colour4 p-2">
                                             <h2 class="h7 heading-semibold text-colour7" data-i18n="flightpassenger-age">Email</h2>
                                         </div>
                                         <div class="col-12 p-2">
                                             <p class="text-break h7">
                                                 <asp:Label ID="lblCustomerEmail" runat="server" Text="Label"></asp:Label>
                                             </p>
                                         </div>
                                     </div>
                                 </div>--%>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-3">
                            <h2 class="h7 heading-semibold text-colour7 bg-colour3 py-3 px-2 border">Car Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-5 col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Car Name</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break h7">
                                                    <asp:Label ID="lblcarName" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-7 col-sm-8 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Transmission type</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblTransmissionType" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Air Condition</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblAirCondition" runat="server" Text="No"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-3 d-none">
                            <h2 class="h7 heading-semibold text-colour7 bg-colour3 py-3 px-2 border">Driver Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-4 col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Name</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break h7">
                                                    <asp:Label ID="lblDriverTitle" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblDriverFName" runat="server" Text="NIL"></asp:Label>
                                                    <asp:Label ID="lblDriverLName" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-8 col-sm-8 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Address</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblAddress" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Phone Number</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblPhoneNo" runat="server" Text="No"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-3" style="display:none;">>
                            <h2 class="h7 heading-semibold text-colour7 bg-colour3 p-3 border">Pick-Up Location Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-4 col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Pick-Up</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break h7">
                                                    <asp:Label ID="lblPickupPlace" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-8 col-sm-8 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">City</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblCityName" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Country</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblCountry" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-3" style="display:none;">>
                            <h2 class="h7 heading-semibold text-colour7 bg-colour3 p-3 border">Drop-Off Location Details</h2>
                            <div class="px-3 border">
                                <div class="row">
                                    <div class="col-4 col-sm-4 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Drop-Off</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break h7">
                                                    <asp:Label ID="lblDropOffLoc" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-8 col-sm-8 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">City</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblDropOffCity" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour4 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Country</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="h7">
                                                    <asp:Label ID="lblDropOffCon" runat="server" Text="NIL"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <p>
                                In case your booking status is not yet confirmed, you will receive a confirmation message via email within the next 24 hours of your booking. If you do not receive the confirmation message, all your Infinity Reward Points will be refunded back into your account.
                            </p>
                            <h2 class="h6 heading-semibold text-colour7 my-3">We thank you for using Giift-Points and wish you a safe journey.
                            </h2>
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="col-12 mt-3 text-right">
                                     <a onclick="window.open('PrintCarVoucher.aspx')" class="btn btn-one">PRINT
 </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
