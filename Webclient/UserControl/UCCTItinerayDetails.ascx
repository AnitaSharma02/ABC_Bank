<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControl/UCCTItinerayDetails.ascx.cs"
    Inherits="UserControl_UCItinerayDetails" %>
<div class="row mt-3 dvPassengerDetails">
    <div class="col-12">
        <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Passenger Details</h2>
        <asp:Repeater ID="rptPassanger" runat="server">
            <HeaderTemplate>
                <div class="px-3 border">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Title</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("Prefix")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-8 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Passenger Type</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("PaxType").ToString() == "ADT" ? "Adult":""%><%#Eval("PaxType").ToString() == "CHD" ? "Child":""%><%#Eval("PaxType").ToString() == "INF" ? "Infant":""%></p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-12 col-md-4 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Passenger Name</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("LastName")%>,&nbsp;<%#Eval("FirstName")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Gender</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("Gender")%></p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-8 col-md-2">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Age</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("Age")%></p>
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
            <div class="col-7 pr-0">
                <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 border">Departure Flight <i class="fa-solid fa-plane"></i></h2>
            </div>
            <div class="col-5 pl-0">
                <h2 class="h7 heading-bold text-colour6 bg-colour1 p-3 h-100 border border-left-0 text-sm-right">
                    <asp:Label ID="LabelClass" runat="server"></asp:Label></h2>
            </div>
        </div>
        <asp:Repeater ID="rptDeparture" runat="server">
            <HeaderTemplate>
                <div class="px-3 border">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-12 col-sm-6 col-md-3 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Flight</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7">
                                <img src='<%#Eval("Carrier.CarrierLogoPath")%>' class="img-fluid" />
                                <%#Eval("Carrier.CarrierName")%> <%#Eval("Carrier.CarrierCode")%><%#Eval("FlightNo")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-6 col-md-3 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="text-break h7">
                                <%#Eval("DepartureAirField.City")%> (<%#Eval("DepartureAirField.IATACode")%>)
                            </p>
                            <p class="text-break h7">
                                <%#Eval("DepartureAirField.AirportName")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="text-break h7">
                                <%#Eval("ArrivalAirField.City")%> (<%#Eval("ArrivalAirField.IATACode")%>)
                            </p>
                            <p class="text-break h7">
                                <%#Eval("ArrivalAirField.AirportName")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("DepartureDate").ToString()).ToString("dd/MM/yyyy")%>
                            </p>
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("DepartureDate").ToString()).ToString("HH:mm")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("ArrivalDate").ToString()).ToString("dd/MM/yyyy")%>
                            </p>
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("ArrivalDate").ToString()).ToString("HH:mm")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2" style="display:none">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Aircraft Type</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("Carrier.EquipmentType")%></p>
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

        <asp:Repeater ID="rptArrival" OnItemDataBound="rptArrival_ItemDataBound" runat="server">

            <HeaderTemplate>
                <div class="row">
                    <div class="col-8 pr-0">
                        <h2 class="h7 heading-bold text-colour6 bg-colour1 py-3 px-2 border" runat="server" visible="true" id="dvReturnFlight">Return Flights <i class="fa fa-plane" aria-hidden="true" style="-webkit-transform: scaleX(-1); transform: scaleX(-1);"></i></h2>
                    </div>
                    <div class="col-4 pl-0">
                        <h2 class="h7 heading-bold text-colour6 bg-colour1 py-3 px-2 border border-left-0 text-right">
                            <asp:Label ID="lblReturnClass" runat="server"></asp:Label></h2>
                    </div>
                </div>
                <div class="px-3 border">
                    <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Flight</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7">
                                <img src='<%#Eval("Carrier.CarrierLogoPath")%>' />
                                <%#Eval("Carrier.CarrierName")%> <%#Eval("Carrier.CarrierCode")%><%#Eval("FlightNo")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="text-break h7">
                                <%#Eval("DepartureAirField.City")%> (<%#Eval("DepartureAirField.IATACode")%>)
                            </p>
                            <p class="text-break h7">
                                <%#Eval("DepartureAirField.AirportName")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="text-break h7">
                                <%#Eval("ArrivalAirField.City")%> (<%#Eval("ArrivalAirField.IATACode")%>)
                            </p>
                            <p class="text-break h7">
                                <%#Eval("ArrivalAirField.AirportName")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Depart Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("DepartureDate").ToString()).ToString("dd/MM/yyyy")%>
                            </p>
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("DepartureDate").ToString()).ToString("HH:mm")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2 border-right">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Arrive Time</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("ArrivalDate").ToString()).ToString("dd/MM/yyyy")%>
                            </p>
                            <p class="h7">
                                <%#Convert.ToDateTime(Eval("ArrivalDate").ToString()).ToString("HH:mm")%>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-2">
                    <div class="row">
                        <div class="col-12 bg-colour2 p-2">
                            <h2 class="h7 heading-semibold text-colour7">Aircraft Type</h2>
                        </div>
                        <div class="col-12 p-2">
                            <p class="h7"><%#Eval("Carrier.EquipmentType")%></p>
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
