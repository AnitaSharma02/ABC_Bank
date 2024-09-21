<%@ Page Title="Air Print Receipt" Language="C#" AutoEventWireup="true" CodeFile="AirPrintReceipt.aspx.cs"
    Inherits="AirPrintReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/UCCTItinerayDetails.ascx" TagName="ItineraryDetails"
    TagPrefix="UC" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Air Print Receipt</title>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Css/root.css" rel="stylesheet" type="text/css" />
    <link href="Css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script language="javascript" type="text/javascript">
        window.print();
    </script>
    <form id="form1" runat="server">

        <div class="container-xl my-3">
            <div class="row">
                <div class="col-12">
                    <div class="border p-3">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour2 p-3">
                                    <img width="160" src="Images/logos/infinity-logo.svg" alt="" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <h2 class="h5 heading-semibold text-colour7 my-3">Congratulations for your Infinity Rewards Ticket!</h2>
                                
                                <p>This is your E-ticket. Do present it with a valid photo identification at the airport check-in counter.</p>
                                <p class="heading-bold">For international travel:</p>
                                <p>The check-in counters are open 4 hours prior to departure and close strictly 2 hours prior to departure.</p>

                                <p class="heading-bold">Reference No.:</p>
                                <asp:Label ID="lblTransactionRefNo" runat="server" />

                                <p class="heading-bold">PNR No.:</p>
                                <asp:Label ID="lblGDSPNR" runat="server" />

                                <p class="heading-bold">Points:</p>
                                <asp:Label ID="lblTotalMiles" runat="server"></asp:Label>                                
                            </div>
                        </div>

                        <UC:ItineraryDetails ID="ucItinarary" runat="server" />


                        <div class="row dvAdditionalDetails">
                            <div class="col-12">
                                <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Additional Details</h2>
                                        <div class="px-3 border">
                                            <div class="row">
                                
                                        <div class="col-sm-4 col-md-3 border-right">
                                            <div class="row">
                                                <div class="col-12 bg-colour2 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Membership No.</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="text-break">
                                                        <asp:Label ID="lblMembershipReferenceNo" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-8 col-md-2 border-right">
                                            <div class="row">
                                                <div class="col-12 bg-colour2 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Name</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="">
                                                        <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-sm-12 col-md-3 border-right">
                                            <div class="row">
                                                <div class="col-12 bg-colour2 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Mobile</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="">
                                                        <asp:Label ID="lblCustomerMobileNo" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 col-md-2 border-right">
                                            <div class="row">
                                                <div class="col-12 bg-colour2 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7" data-i18n="flightpassenger-gender">Address</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="">
                                                        <asp:Label ID="lblCustomerAddress" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-8 col-md-2">
                                            <div class="row">
                                                <div class="col-12 bg-colour2 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7" data-i18n="flightpassenger-age">Email</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="text-break">
                                                        <asp:Label ID="lblCustomerEmail" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                  
                                            </div>
                                        </div>
                                   
                            </div>
                        </div>

                        <div class="dvFairConditions">
                            <h2 class="h7 heading-semibold text-colour7 pt-3 pb-2">Fair Conditions</h2>
                            <ul class="pl-3">
                                <li>1. Use your reference number for all communication with us on this booking.</li>
                                <li>2. Your PNR number serves as a comfirmation of your ticket status.</li>
                                <li>3. Carry a print out of this E-Ticket and present it at the airlines counter at
            the time of check-in.</li>
                                <li>4. Kindly carry a valid photo identification along with your E-Ticket.</li>
                                <li>5. No Cancellation and modification is allowed on a Ticket.</li>
                            </ul>
                            <h2 class="h5 heading-semibold text-colour7 mt-3">Infinity Rewards wishes you a pleasant journey and hopes to serve you again in the future.</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
