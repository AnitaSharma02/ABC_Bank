<%@ Page Title="Flight Passenger" Language="C#" AutoEventWireup="true" CodeFile="FlightPassenger.aspx.cs"
    Inherits="FlightPassenger" MasterPageFile="~/SiteMaster.master" %>

<%@ Register Src="~/UserControl/AdultPassangerDetails.ascx" TagName="Adult" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/ChildPassangerDetails.ascx" TagName="Children" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/InfantPassangerDetails.ascx" TagName="Infant" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <%--<link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="\Css/flight.css" />
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;} 
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
            $("#updProgress").css("display", "block");
            setTimeout(function () {
                $("#updProgress").css("display", "none");
            }, 2000);
        }
    </script>
     <div class="dvBreadcrumbs">
     <div class="container-lg">
         <nav>
             <ul class="breadcrumb px-0 py-3">
                 <li class="mr-3">
                     <a href="\">
                         <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                 </li>
                 <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                 <li class="breadcrumb-item "><a href="flightlist.aspx"> Flight List</a></li>
                 <li class="breadcrumb-item">Flight Passenger</li>
             </ul>
         </nav>
     </div>
 </div>
     
<div class="dvFlightPassenger pb-5 mt-lg-4">
    <div class="container-lg">
        <div class="row">
             <div class="col-12">
                <div class="h6 heading-semibold text-danger mb-3" id="errorDiv" runat="server"></div>
             </div>
                <div class="col-lg-7">
                   <div class="row">
                        <div class="col-12">
                           <h2 class="h6 heading-bold text-colour6 bg-colour1 p-3" data-i18n="flightpassenger-passenger">Passenger Details</h2>
                         </div>
                         <div class="col-12">
                            <div class="bg-colour2 p-3">
                                <div class="row">
                                    <div class="col-12 mb-2">
                                        <div id="AdultInfo" runat="server">
                                            <asp:Repeater ID="rptAdultControl" runat="server">
                                                <ItemTemplate>
                                                    <h2 class="h6 heading-semibold text-colour7 mb-2"><span data-i18n="flightpassenger-adult">Adult: <%# (Container.ItemIndex + 1) %></span></h2>
                                                    <asp:Panel ID="panelAdultControlHolder" CssClass="Flight_Ticket_holder" runat="server">
                                                        <uc:Adult ID="adultdetails" runat="server" />
                                                    </asp:Panel>
                                                </ItemTemplate>
                                              </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                <div class="row">
                                    <div class="col-12 mb-2">
                                        <div id="ChildInfo" runat="server" visible="false">
                                        <asp:Repeater ID="rptChildControl" runat="server">
                                            <ItemTemplate>
                                                <h2 class="h6 heading-semibold text-colour7 mb-2"><span data-i18n="flightpassenger-child">Child: <%# (Container.ItemIndex + 1) %></span></h2>
                                                <asp:Panel ID="panelChildControlHolder" CssClass="Flight_Ticket_holder" runat="server">
                                                    <uc:Children ID="childdetails" runat="server" />
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 mb-2">
                                        <div id="InfantInfo" runat="server" visible="false">
                                        <asp:Repeater ID="rptInfantControl" runat="server">
                                            <ItemTemplate>
                                                <h2 class="h6 heading-semibold text-colour7 mb-2"><span data-i18n="flightpassenger-infant">Infant: <%# (Container.ItemIndex + 1) %></span></h2>
                                                <asp:Panel ID="panelInfantControlHolder" CssClass="Flight_Ticket_holder" runat="server">
                                                    <uc:Infant ID="infantdetails" runat="server" />
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="searchBtn col-12 mb-2">
                                        <asp:Button ValidationGroup="WebValidation" ID="btnSubmit" runat="server" OnClientClick="fnShowLoaderOnSubmitClick();" OnClick="btnBookFlight_Click" Text="Proceed to Pay" class="btn btn-one" data-i18n="flightpassenger-proceed-topay"></asp:Button>
                                    </div>
                                </div>
                            </div>
                         </div>
                   </div>
                </div>

             <div class="col-lg-5 mt-3 mt-lg-0">
                <div class="row">
                     <div class="col-12">
                        <div class="bg-colour1 d-flex justify-content-between align-items-center">
                            <h2 class="h6 heading-bold text-colour6 p-3" data-i18n="flightpassenger-itinerary">Itinerary</h2>
                            <a href="FlightList.aspx?edit=1" data-i18n="flightpassenger-edit" class="btn btn-two mr-2">Edit</a>
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
                                <div class="row">
                                    <asp:Repeater ID="rptDeparture" runat="server">
                                        <ItemTemplate>
                                            <div class="col-12">
                                                <div class="row align-items-center mt-md-1">
                                                    <div class="col-3 col-sm-2 mb-3 mb-sm-0 pr-lg-1">
                                                      <div class="img-container">
                                                        <img class="img-fluid" src='<%#Eval("Carrier.CarrierLogoPath")%>' />
                                                      </div>
                                                    </div>
                                                    <div class="col-6 offset-3 offset-sm-0 col-sm-3 col-lg-3 d-flex align-items-center flex-lg-column text-lg-center mb-3 mb-sm-0 px-lg-1">
                                                      <i class="fa-regular fa-clock"></i>
                                                      <div class="ml-2 ml-lg-0 mt-lg-1">
                                                        <p class="h7 text-colour7"><%#Eval("DepartureAirField.City")%> (<%#Eval("DepartureAirField.IATACode")%>)</p>
                                                        <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("DepartureDate").ToString()).ToString("dd/MM/yyyy")%></p>
                                                        <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("DisplayDepartureTime").ToString()).ToString("hh:mm tt")%></p>
                                                      </div>
                                                    </div>
                                                    <div class="col-6 col-sm-3 col-lg-3 col-xl-4 d-flex align-items-center flex-lg-column text-lg-center px-lg-1"  >
                                                        <i class="fa-regular fa-clock"></i>
                                                        <div class="ml-2 ml-lg-0 mt-lg-1">
                                                          <p class="h7 text-colour7"><%#Eval("TotalDurationHrs")%> hr <%#Eval("TotalDurationMins")%> m</p>
                                                        </div>
                                                      </div>
                                                    <div class="col-6 col-sm-3 col-lg-3 d-flex align-items-center flex-lg-column text-lg-center pl-lg-1">
                                                      <i class="fa-regular fa-clock"></i>
                                                      <div class="ml-2 ml-lg-0 mt-lg-1">
                                                        <p class="h7 text-colour7"><%#Eval("ArrivalAirField.City")%> (<%#Eval("ArrivalAirField.IATACode")%>)</p>
                                                        <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("ArrivalDate").ToString()).ToString("dd/MM/yyyy")%></p>
                                                        <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("DisplayArrivalTime").ToString()).ToString("hh:mm tt")%></p>
                                                      </div>
                                                    </div>
                                                </div>
                                                <div class="border my-3 d-block w-100 rajesh"></div>
                                            </div> 
                                        </ItemTemplate>
                                    </asp:Repeater>
                                 </div>
                             
                                <div class="row">
                                    <div class="col-12"> 
                                        <asp:Label ID="lblarrival" class="h6 heading-semibold text-colour7" runat="server" Visible="false" data-i18n="flightpassenger-arrival">Arrival Flight</asp:Label>
                                    </div>
                                </div>
                                
                                <div class="row">
                                    <asp:Repeater ID="rptArrival" runat="server">
                                            <ItemTemplate>
                                                
                                                <div class="col-12"> 
                                                    <%--<div class="border my-3 d-block w-100"></div>--%>
                                                    <div class="row align-items-center mt-md-1">
                                                        <div class="col-3 col-sm-2 mb-3 mb-sm-0 pr-lg-1">
                                                          <div class="img-container">
                                                            <img class="img-fluid" src='<%#Eval("Carrier.CarrierLogoPath")%>' />
                                                          </div>
                                                        </div>
                                                          <div class="col-6 offset-3 offset-sm-0 col-sm-3 col-lg-3 d-flex align-items-center flex-lg-column text-lg-center mb-3 mb-sm-0 px-lg-1">
                                                            <i class="fa-regular fa-clock"></i>
                                                            <div class="ml-2 ml-lg-0 mt-lg-1">
                                                              <p class="h7 text-colour7"><%#Eval("DepartureAirField.City")%> (<%#Eval("DepartureAirField.IATACode")%>)</p>
                                                              <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("DepartureDate").ToString()).ToString("dd/MM/yyyy")%></p>
                                                              <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("DisplayDepartureTime").ToString()).ToString("hh:mm tt")%></p>
                                                            </div>
                                                          </div> 
                                                          <div class="col-6 col-sm-3 col-lg-3 col-xl-4 d-flex align-items-center flex-lg-column text-lg-center px-lg-1"  >
                                                              <i class="fa-regular fa-clock"></i>
                                                              <div class="ml-2 ml-lg-0 mt-lg-1">
                                                                <p class="h7 text-colour7"><%#Eval("TotalDurationHrs")%> hr <%#Eval("TotalDurationMins")%> m</p>
                                                              </div>
                                                            </div>
                                                        <div class="col-6 col-sm-3 col-lg-3 d-flex align-items-center flex-lg-column text-lg-center pl-lg-1">
                                                          <i class="fa-regular fa-clock"></i>
                                                          <div class="ml-2 ml-lg-0 mt-lg-1">
                                                            <p class="h7 text-colour7"><%#Eval("ArrivalAirField.City")%> (<%#Eval("ArrivalAirField.IATACode")%>)</p>
                                                            <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("ArrivalDate").ToString()).ToString("dd/MM/yyyy")%></p>
                                                            <p class="h7 text-colour7"><%#Convert.ToDateTime(Eval("DisplayArrivalTime").ToString()).ToString("hh:mm tt")%></p>
                                                          </div>
                                                        </div>
                                                       
                                                    </div>
                                                    <div class="border my-3 d-block w-100"></div>
                                                </div> 
                                                     
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </div>
                                
                         
                                <div class="row">
                                  <div class="col-12 d-flex justify-content-between">
                                    <span data-i18n="flightpassenger-total-points" class="h6 heading-bold text-colour7" >Total Points: </span>
                                    <asp:Label ID="lblTotalPoints" runat="server" CssClass="h6 heading-bold text-colour7" />
                                  </div>
                                </div>
                            
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
