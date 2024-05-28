<%@ Page Title="Flight Passenger Domestic" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="FlightPassengerForDomestic.aspx.cs" Inherits="FlightPassengerForDomestic" %>

<%@ Register Src="~/UserControl/AdultPassangerDetails_Domestic.ascx" TagPrefix="uc" TagName="AdultPassangerDetails_Domestic" %>
<%@ Register Src="~/UserControl/ChildPassangerDetails_Domestic.ascx" TagPrefix="uc" TagName="ChildPassangerDetails_Domestic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <%--<link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="Css/FlightList.css" rel="Stylesheet" type="text/css" />--%>
    <link href="Css/flight.css" rel="Stylesheet" type="text/css" />
    <style>
        .dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu {
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function numbersOnly(obj, e) {
            if (e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 9) {
                return true;
            }
            else {
                if ((e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105)) {
                    return false;
                }
                return true;
            }
        }
        function fnShowLoaderOnSubmitClick() {
            //debugger
            $("#btnBookFlightDomestic").hide();
            //$("#updProgress").css("display", "block");
            //setTimeout(function () {
            //    $("#updProgress").css("display", "none");
            //}, 6000);
        }
    </script>

    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="FlightSearch.aspx">Domestic Flight Search</a></li>
                    <li class="breadcrumb-item active">Domestic Flight Passenger</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvFlightPassengerForDomestic">
        <div class="container-lg">
            <div class="row">
                <div class="ErrorMsgContainer" id="errorDiv" runat="server"></div>
                <div class="col-lg-7 col-md-12">
                    <div class="row">
                        <div class="col-12">
                            <div class="bg-colour1 d-flex justify-content-between align-items-center">
                                <h1 data-i18n="flightpassenger-passenger" class="h6 heading-semibold text-colour6 p-3">Passenger Details</h1>
                            </div>
                        </div>
                    </div>
                    <div class="dvForm row mb-3">
                        <div class="col-12">
                            <div class="bg-colour2">
                                <div class="col-12" id="AdultInfo" runat="server">
                                    <asp:Repeater ID="rptAdultControl" runat="server">
                                        <ItemTemplate>
                                            <h2 data-i18n="flightpassenger-adult" class="h6 heading-semibold py-2">Adult: <%# (Container.ItemIndex + 1) %></h2>
                                            <asp:Panel ID="panelAdultControlHolder" CssClass="Flight_Ticket_holder" runat="server">
                                                <uc:AdultPassangerDetails_Domestic runat="server" ID="AdultPassangerDetails_Domestic" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-12" id="ChildInfo" runat="server" visible="false">
                                    <asp:Repeater ID="rptChildControl" runat="server">
                                        <ItemTemplate>
                                            <h2 data-i18n="flightpassenger-child" class="h6 heading-semibold py-2">Child: <%# (Container.ItemIndex + 1) %></h2>
                                            <asp:Panel ID="panelChildControlHolder" CssClass="Flight_Ticket_holder" runat="server">
                                                <uc:ChildPassangerDetails_Domestic runat="server" ID="ChildPassangerDetails_Domestic" />
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-12 text-center text-lg-left pb-3" id="btnBookFlightDomestic">
                                    <asp:Button ValidationGroup="WebValidation" ID="btnSubmit" runat="server" OnClientClick="fnShowLoaderOnSubmitClick();" OnClick="btnBookFlightDomestic_Click" Text="Proceed to Pay" class="btn btn-one" data-i18n="flightpassenger-proceed-topay"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="dvOrderSummary col-lg-5 col-md-12 mb-3">
                    <div class="row">
                        <div class="col-12">
                            <div class="bg-colour1 d-flex justify-content-between align-items-center">
                                <h2 data-i18n="flightpassenger-itinerary" class="h6 heading-semibold bg-colour1 text-colour6 p-3">Itinerary</h2>
                                <a href="FlightListForDomestic.aspx?edit=1" data-i18n="flightpassenger-edit" class="btn btn-two mr-1">Edit</a>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="bg-colour2 p-3">
                                <div class="row">
                                    <div class="col-12">
                                        <h2 class="h6 heading-semibold text-colour7" data-i18n="flightpassenger-departure">Departure Flight</h2>
                                    </div>
                                </div>
                                <div class="border my-3"></div>
                                        <asp:Repeater ID="rptDeparture" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-4 text-center">
                                                    <img width="60" src='<%#Eval("AirlineLogo")%>' />
                                                    <p class="h7 heading-regular"><%#Eval("AirlineName")%></p>
                                                    <p class="h7 heading-regular"><%#Eval("FlightNo")%></p>
                                                </div>
                                                <div class="col-4 text-center">
                                                    <%--<i class="fa-regular fa-clock"></i>--%>
                                                    <p class="h7 heading-regular"><%#Eval("Departure")%></p>
                                                    <p class="h7 heading-regular"><%#Convert.ToDateTime(Eval("FlightDate").ToString()).ToString("dd/MM/yyyy")%></p>
                                                    <p class="h7 heading-regular"><%#Convert.ToDateTime(Eval("DepartureTime").ToString()).ToString("hh:mm tt")%></p>
                                                </div>
                                                <%-- <div class="col-md-3 col-4 text-blk">
                                                    <i class="fa-regular fa-clock"></i><%#Eval("TotalDurationHrs")%> hr <%#Eval("TotalDurationMins")%> m
       
                                                </div>--%>
                                                <div class="col-4 text-center">
                                                    <p class="h7 heading-regular"><%#Eval("Arrival")%></p>
                                                    <p class="h7 heading-regular"><%#Convert.ToDateTime(Eval("ArrivalTime").ToString()).ToString("hh:mm tt")%></p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                <%--<div class="border my-3"></div>--%>
                                <div class="row">
                                    <div class="col-12">
                                        <h2 class="h6 heading-semibold text-colour7">
                                            <asp:Label ID="lblarrival" class="d-block heading-semibold border-bottom border-top py-3 my-3" runat="server" Visible="false" data-i18n="flightpassenger-arrival">Arrival Flight</asp:Label>
                                        </h2>                                        
                                    </div>
                                </div>
                                <%--<div class="border my-3"></div>--%>
                                    <asp:Repeater ID="rptArrival" runat="server">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-4 text-center">
                                                    <img width="60" class="img-fluid" src='<%#Eval("AirlineLogo")%>' />
                                                    <p class="h7 heading-regular"><%#Eval("AirlineName")%></p>
                                                    <p class="h7 heading-regular"><%#Eval("FlightNo")%></p>
                                                </div>
                                                <div class="col-4 text-center">
                                                    <%--<i class="fa-regular fa-clock"></i>--%>
                                                    <p class="h7 heading-regular"><%#Eval("Departure")%></p>
                                                    <p class="h7 heading-regular"><%#Convert.ToDateTime(Eval("FlightDate").ToString()).ToString("dd/MM/yyyy")%></p>
                                                    <p class="h7 heading-regular"><%#Convert.ToDateTime(Eval("DepartureTime").ToString()).ToString("hh:mm tt")%></p>
                                                </div>
                                                <%-- <div class="col-md-3 col-4 text-blk">
                                                    <i class="fa-regular fa-clock"></i><%#Eval("TotalDurationHrs")%> hr <%#Eval("TotalDurationMins")%> m
       
                                                </div>--%>
                                                <div class="col-4 text-center">
                                                    <p class="h7 heading-regular"><%#Eval("Arrival")%></p>
                                                    <p class="h7 heading-regular"><%#Convert.ToDateTime(Eval("ArrivalTime").ToString()).ToString("hh:mm tt")%></p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                <div class="border my-3"></div>
                                <div class="d-flex justify-content-between align-items-center">
                                    <span class="heading-bold" data-i18n="flightpassenger-total-points">Total NPoints: </span>
                                    <asp:Label CssClass="heading-bold" ID="lblTotalPoints" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

