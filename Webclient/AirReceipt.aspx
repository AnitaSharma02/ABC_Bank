<%@ Page Title="Air Receipt" Language="C#" AutoEventWireup="true" CodeFile="AirReceipt.aspx.cs" Inherits="AirReceipt"
    MasterPageFile="~/SiteMaster.master" %>

<%@ Register Src="~/UserControl/UCCTItinerayDetails.ascx" TagName="ItineraryDetails"
    TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Css/root.css" rel="stylesheet" type="text/css" />
    <link href="Css/global.css" rel="stylesheet" type="text/css" />

    <style>
        .dvRedemptionMenu {
            display:none !important;
        }
    </style>

    <div class="container-xl my-3">
        <div class="row">
            <div class="col-12">
                <div class="border p-3">
                    <div class="row">
                        <div class="col-12">
                            <div class="bg-colour3 p-3">
                                <img src="Images/logos/infinity-logo.svg" alt="" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <h2 class="h5 heading-semibold text-colour7 my-3">Congratulations for your Infinity Rewards Ticket!</h2>
                            <p>
                                This is your E-ticket. Do present it with a valid photo identification at the airport check-in counter.<br />
                                <span class="heading-bold">For international travel:</span><br />
                                The check-in counters are open 4 hours prior to departure and close strictly 2 hours prior to departure.<br />

                                <span runat="server" id="ItineraryTimeChanged" style="display: none;">Please check the timings, as there may be change in time from Airlines.</span>

                                <span class="heading-bold">Reference No.:</span><br />
                                <asp:Label ID="lblTransactionRefNo" runat="server" /><br />

                                <span class="heading-bold">PNR No.:</span><br />
                                <asp:Label ID="lblGDSPNR" runat="server" /><br />

                                <span class="heading-bold">Points:</span><br />
                                <asp:Label ID="lblTotalMiles" runat="server"></asp:Label><br />
                            </p>
                        </div>
                    </div>
                    <UC:ItineraryDetails ID="ucItinarary" runat="server" />
                    <div class="row dvAdditionalDetails mt-3">
                        <div class="col-12">
                            <h2 class="h7 heading-semibold text-colour7 bg-colour2 p-3 border">Additional Details</h2>         
                                    <div class="px-3 border">
                                        <div class="row">        
                                    <div class="col-4 col-sm-4 col-md-2 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour3 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Membership No.</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="text-break">
                                                    <asp:Label ID="lblMembershipReferenceNo" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-8 col-sm-8 col-md-2 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour3 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Name</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-sm-12 col-md-4 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour3 p-2">
                                                <h2 class="h7 heading-semibold text-colour7">Mobile</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblCustomerMobileNo" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4 col-sm-4 col-md-2 border-right">
                                        <div class="row">
                                            <div class="col-12 bg-colour3 p-2">
                                                <h2 class="h7 heading-semibold text-colour7" data-i18n="flightpassenger-gender">Address</h2>
                                            </div>
                                            <div class="col-12 p-2">
                                                <p class="">
                                                    <asp:Label ID="lblCustomerAddress" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-8 col-sm-8 col-md-2">
                                        <div class="row">
                                            <div class="col-12 bg-colour3 p-2">
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
                            <div class="row">
                                <div class="col-12 mt-3 text-right">
                                    <label>
                                        <asp:Button CssClass="btn btn-one" ID="btnBookNow" runat="server" OnClientClick="var retvalue = redirectLocation('AirPrintReceipt.aspx'); event.returnValue= retvalue;event.preventDefault(); return retvalue;"
                                            Text="Print" />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
