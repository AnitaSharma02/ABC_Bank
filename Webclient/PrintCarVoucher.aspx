<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCarVoucher.aspx.cs"
    Inherits="PrintCarVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Car Receipt</title>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Css/root.css" rel="stylesheet" type="text/css" />
    <link href="Css/global.css" rel="stylesheet" type="text/css" />
    <link href="Css/Car.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        window.print();
    </script>

</head>
<body>
    <form id="form1" runat="server">        
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
                                    <div class="col-12 text-right">
                                        <a onclick="window.open('PrintCarVoucher.aspx')" class="btn btn-one">PRINT</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>

<%--<body style="margin: 0 auto; width: 800px">
    <form id="form1" runat="server">
        <div class="tickets">
            <div class="wrapper">
                <div class="hdr-bg">
                    <div class="logomenu">
                        <img src="images/logo.svg" alt="" />
                    </div>
                </div>
                <div class="clr">
                </div>

                <!--referece no Starts-->
                <div class="bkreference">
                    <div class="col1">
                        <div style="float: left; font-weight: bold; width: 30%">
                            Reference No.: 
                        </div>
                        <asp:Label ID="lblbookingReferenceNo" runat="server" Text="NIL"></asp:Label>
                    </div>
                    <div class="col2">
                        <div id="PaymentInfo" runat="server">
                        </div>
                    </div>
                    <div class="clr">
                    </div>
                    <!--referece no Ends-->
                </div>
                <div class="clr">
                </div>
                <!--Infi Destination Starts-->
                <div class="body-bdr">
                    <p>
                        Thank you
                        <asp:Label ID="lblMemberName" runat="server" Text="NIL"> </asp:Label>
                        (Giift ID-:
                        <asp:Label ID="lblMembershipReference" runat="server" Text="NIL"></asp:Label>) for
                        using Giift-Points to book your car. Please use your Reference ID for any communication
                        pertaining to this booking.
                    </p>
                    <p>
                        Your Current booking status is Car  <strong>
                            <asp:Label ID="lblBookingStatus" runat="server" Text="NIL"></asp:Label>*.</strong>
                    </p>
                    <p style="display:none;">
                        Supplier ID: <strong>
                            <asp:Label ID="lblbookingId" runat="server" Text="NIL"></asp:Label></strong>
                    </p>
                    <h3 class="CarHeading">Booking Details</h3>
                    <div class="w50P">
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Pick-Up-:
                                </th>
                                <td>
                                    <asp:Label ID="lblpickUp" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Pick-Up Date-:
                                </th>
                                <td>
                                    <asp:Label ID="lblpickUpDate" runat="server" Text="NIL"></asp:Label>
                                    at
                                    <asp:Label ID="lblPickUpTime" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="w50P imFR">
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Drop-Off-:
                                </th>
                                <td>
                                    <asp:Label ID="lbldropOff" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Drop-Off Date-:
                                </th>
                                <td>
                                    <asp:Label ID="lbldropOffDate" runat="server" Text="NIL"></asp:Label>
                                    at
                                    <asp:Label ID="lbldropOffTime" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="w50P imFR">
                        <h3 class="CarHeading">Car Details</h3>
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Car Name-:
                                </th>
                                <td>
                                    <asp:Label ID="lblcarName" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Transmission type-:
                                </th>
                                <td>
                                    <asp:Label ID="lblTransmissionType" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Air Condition-:
                                </th>
                                <td>
                                    <asp:Label ID="lblAirCondition" runat="server" Text="No"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="w50P">
                        <h3 class="CarHeading">Driver Details</h3>
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Name-:
                                </th>
                                <td>
                                    <asp:Label ID="lblDriverTitle" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblDriverFName" runat="server" Text="NIL"></asp:Label>
                                    <asp:Label ID="lblDriverLName" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Address-:
                                </th>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Phone Number-:
                                </th>
                                <td>
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="No"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="w50P" style="display:none;">
                        <h3 class="CarHeading">Pick-Up location details</h3>
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Pick-Up
                                </th>
                                <td>
                                    <asp:Label ID="lblPickupPlace" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>City
                                </th>
                                <td>
                                    <asp:Label ID="lblCityName" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Country
                                </th>
                                <td>
                                    <asp:Label ID="lblCountry" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="w50P imFR" style="display:none;">
                        <h3 class="CarHeading">Drop-Off location details</h3>
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Drop-Off
                                </th>
                                <td>
                                    <asp:Label ID="lblDropOffLoc" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>City
                                </th>
                                <td>
                                    <asp:Label ID="lblDropOffCity" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Country
                                </th>
                                <td>
                                    <asp:Label ID="lblDropOffCon" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <p>
                        In case your booking status is not yet confirmed, you will receive a confirmation message via email within the next 24 hours of your booking. If you do not receive the confirmation message, all your  Giift-Points will be refunded back into your account.
                    </p>
                    <h3 style="color: #666; margin: 2% 0; text-align: center;">We thank you for using Giift-Points and wish you a safe journey.</h3>

                    <br class="clr" />
                    <br class="clr" />
                </div>
                <!--Infi Destination Ends-->
            </div>
        </div>
    </form>
</body>--%>
</html>
