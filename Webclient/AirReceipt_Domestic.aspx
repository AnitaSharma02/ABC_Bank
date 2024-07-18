<%@ Page Title="Air Receipt Domestic" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="AirReceipt_Domestic.aspx.cs" Inherits="AirReceipt_Domestic" %>

<%@ Register Src="~/UserControl/UCCTItinerayDetails_Domestic.ascx" TagPrefix="uc1" TagName="UCCTItinerayDetails_Domestic" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CP" Runat="Server">
     <link href="Css/FlightList.css" rel="Stylesheet" type="text/css" />
    <link href="Css/MyAccount.css" rel="stylesheet" type="text/css" />
    <style>
        .BooknowStyle {
            font-family: 'Roboto', sans-serif;
            font-style: inherit;
            font-weight: 600;
            font-size: 16px;
            line-height: 20px;
            color: #fff;
            border: 1px solid #E5A812;
            padding: 10px 25px;
            background: #E5A812 !important;
            border-radius: 5px !important;
        }
        .table-responsive { 
            border: 1px solid #dee2e6;
            margin: 0 0 5px;
        }
        .tickets {border: 1px solid #dee2e6;padding: 10px; border-radius: 5px;margin:0 0 20px;}
        .manageBook table thead, th {
            letter-spacing: normal;
        }

        .body-bdr > h2 {
            font-size: 16px;
            font-weight: bold;
        }

        #carouselOne {
            display: none;
        }

        h4.title {
            border-radius: 10px 10px 0 0;
        }

        .table td, .table th {
            border: 1px solid #dee2e6;
        }
    </style>
     <div style="margin: 0 auto; width: 800px" class="airBox">
        <div class="tickets">
            <div class="wrapper">
                <!--Infi Destination Starts-->
                <div class="body-bdr">
                    <h2>Congratulations for your NIC Asia Ticket!</h2>
                    <p>
                        This is your E-ticket. Do present it with a valid photo identification at the airport
                        check-in counter.
                    </p>
                    <p>
                        For international travel: The check-in counters are open 4 hours prior to departure
                        and close strictly 2 hours prior to departure.
                    </p>
                    <p runat="server" id="ItineraryTimeChanged" style="display: none;">
                        <b>Please check the timings, as there may be change in time from Airlines.</b>
                    </p>
                    <div class="reference">
                        <div class="col1">
                            <div class="txtPNR">Reference No.: <asp:Label ID="lblTransactionRefNo" runat="server" /></div>
                           <%-- <div class="txtPNR pnr2">PNR No.: <asp:Label ID="lblGDSPNR" runat="server" /></div>--%>
                        </div>
                        <div class="col2">
                            <div>
                                Points: 
                            </div>
                            <asp:Label ID="lblTotalMiles" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div>
                        
                        
                    </div>
                    <div class="clr">
                    </div>
                    <uc1:UCCTItinerayDetails_Domestic runat="server" ID="UCCTItinerayDetails_Domestic" />
                    <div class="addi-info">
                        <h3 style="background-color: #202020 !important; color: White;">Additional Details</h3>
                        <table width="100%" style="text-align: left;" cellpadding="0" cellspacing="0">
                            <tr>
                                <th>Membership No.
                                </th>
                                <th>Name
                                </th>
                                <th>Mobile
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMembershipReferenceNo" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerName" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerMobileNo" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2">Address
                                </th>
                                <th>Email
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblCustomerAddress" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustomerEmail" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: none; margin: 15px 0 0;text-align:right">
                        <label class="btn btn-yellow">
                            <asp:Button ID="btnBookNow" runat="server" Text="Print" OnClick="btnBookNow_Click" />
                        </label>
                    </div>
                </div>
                <!--Infi Destination Ends-->
            </div>
        </div>
    </div>
</asp:Content>

