<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCarVoucher.aspx.cs"
    Inherits="PrintCarVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Receipt</title>
    <link href="Css/main.css" rel="stylesheet" type="text/css" />
    <link href="Css/Car.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        window.print();
    </script>
    <style>
        .bkreference .col1, .bkreference .col2 {
            float: left;
            margin-top: 10px;
            width: 50%;
        }

        ul {
            display: inline;
            float: left;
            margin-top: 3% !important;
        }

        .w50P {
            float: left;
            width: 48%;
        }

        .bkreference {
            float: left;
            margin: 5% 0 0;
            width: 100%;
        }

        .logoCover {
            float: left;
            width: 48%;
        }

        .body-bdr {
            margin-bottom: 2%;
        }

        .FL {
            float: left;
        }
    </style>
</head>
<body style="margin: 0 auto; width: 800px">
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
                        (Verve ID:
                        <asp:Label ID="lblMembershipReference" runat="server" Text="NIL"></asp:Label>) for
                        using points to book your car. Please use your Reference ID for any communication
                        pertaining to this booking.
                    </p>
                    <p>
                        Your Current booking status is Car <strong>
                            <asp:Label ID="lblBookingStatus" runat="server" Text="NIL"></asp:Label>*.</strong>
                    </p>
                    <p>
                        Supplier ID: <strong>
                            <asp:Label ID="lblbookingId" runat="server" Text="NIL"></asp:Label></strong>
                    </p>
                    <h3 class="CarHeading">Booking Details</h3>
                    <div class="w50P">
                        <table class="commonTable" cellspacing="0" cellpadding="1">
                            <tr>
                                <th>Pick-Up
                                </th>
                                <td>
                                    <asp:Label ID="lblpickUp" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Pick-Up Date
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
                                <th>Drop-Off
                                </th>
                                <td>
                                    <asp:Label ID="lbldropOff" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Drop-Off Date
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
                                <th>Car Name
                                </th>
                                <td>
                                    <asp:Label ID="lblcarName" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Transmission type
                                </th>
                                <td>
                                    <asp:Label ID="lblTransmissionType" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Air Condition
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
                                <th>Name
                                </th>
                                <td>
                                    <asp:Label ID="lblDriverTitle" runat="server" Text="NIL"></asp:Label>
                                    <asp:Label ID="lblDriverFName" runat="server" Text="NIL"></asp:Label>
                                    <asp:Label ID="lblDriverLName" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Address
                                </th>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" Text="NIL"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Phone Number
                                </th>
                                <td>
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="No"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="w50P">
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
                    <div class="w50P imFR">
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
                        In case your booking status is not yet confirmed, you will receive a confirmation message via email within the next 24 hours of your booking. If you do not receive the confirmation message, all your  points will be refunded back into your account.
                    </p>
                    <h3 style="color: #666; margin: 2% 0; text-align: center;">We thank you for using points and wish you a safe journey.</h3>

                    <br class="clr" />
                    <br class="clr" />
                </div>
                <!--Infi Destination Ends-->
            </div>
        </div>
    </form>
</body>
</html>
