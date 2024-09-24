<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCCTItinerayDetails_Domestic.ascx.cs"
    Inherits="UserControl_UCCTItinerayDetails_Domestic" %>

<div class="row mt-3 dvPassengerDetails">
    <div class="col-12">

        <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3">Passenger Details</h2>
        <asp:Repeater ID="rptPassanger" runat="server">
            <HeaderTemplate>
                <div class="px-3">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-6 col-sm-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Title</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("Title")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Passenger Type</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("Type").ToString()%></p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Passenger Name</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="text-break"><%#Eval("LastName")%>&nbsp;<%#Eval("FirstName")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Gender</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("Gender")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Nationality</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("Nationality")%></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<div class="row mt-3 dvFlightInformation">
    <div class="col-12">
        <div class="row">
            <div class="col-8 pr-0">
                <h2 class="h7 bg-colour2 p-3 border border-right-0">
                    <span class="h7 heading-semibold text-colour6 bg-colour1">Departure Flight</span>
                    <i class="h7 fa fa-plane" aria-hidden="true"></i>
                </h2>
            </div>
            <div class="col-4 pl-0">
                <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3 border border-left-0 text-right">
                    <asp:Label ID="LabelClass" runat="server"></asp:Label>
                </h2>
            </div>
            <div class="col-12">
                <h2 class="h7 heading-semibold text-colour7 bg-colour2 p-3 border text-right">
                    <span>PNR No.:<asp:Label ID="lblOutboundPNR" runat="server" /></span>
                </h2>
            </div>
        </div>
        <asp:Repeater ID="rptDeparture" runat="server">
            <HeaderTemplate>
                <div class="px-3">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Flight</h2>
                        </div>
                        <div class="col-12 p-2">
                            <div>
                                <img src='<%#Eval("AirlineLogo")%>' />
                                <p class=""><%#Eval("AirlineName")%><span class="ml-2"><%#Eval("FlightNo")%></span></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("Departure")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("Arrival")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Convert.ToDateTime(Eval("FlightDate").ToString()).ToString("dd/MM/yyyy")%></p>
                            <p class=""><%#Convert.ToDateTime(Eval("DepartureTime").ToString()).ToString("HH:mm")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Convert.ToDateTime(Eval("ArrivalTime").ToString()).ToString("HH:mm")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Aircraft Type</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Eval("AircraftType")%></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<div class="row mt-3 dvFlightArrivalInfo">
    <div class="col-12">
        <div class="row">
            <div class="col-6 pr-0">
                <h2 class="h7 bg-colour2 p-3 border border-right-0" runat="server" visible="false" id="dvReturnFlight">
                    <span class="h7 heading-semibold text-colour7">Return Flights</span>
                    <i class="h7 fa fa-plane" aria-hidden="true" style="-webkit-transform: scaleX(-1); transform: scaleX(-1);"></i>
                </h2>
            </div>
            <div class="col-6 pl-0">
                <h2 class="h7 heading-semibold text-colour7 bg-colour2 p-3 border text-right">
                    <span>PNR No.:<asp:Label ID="lblInboundPNR" runat="server" /></span>
                </h2>
            </div>
        </div>
        <asp:Repeater ID="rptArrival" runat="server">
            <HeaderTemplate>
                <div class="px-3">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Flight</h2>
                        </div>
                        <div class="col-12 p-2">
                            <div>
                                <img src='<%#Eval("AirlineLogo")%>' />
                                <p><%#Eval("AirlineName")%><span class="ml-2"><%#Eval("FlightNo")%></span></p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p><%#Eval("Departure")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p><%#Eval("Arrival")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p><%#Convert.ToDateTime(Eval("FlightDate").ToString()).ToString("dd/MM/yyyy")%></p>
                            <p><%#Convert.ToDateTime(Eval("DepartureTime").ToString()).ToString("HH:mm")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class=""><%#Convert.ToDateTime(Eval("ArrivalTime").ToString()).ToString("HH:mm")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-sm-4 col-md-2 border">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Aircraft Type</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p><%#Eval("AircraftType")%></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                    </div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
