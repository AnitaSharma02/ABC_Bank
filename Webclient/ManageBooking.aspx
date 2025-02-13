<%@ Page Title="Manage Booking" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="ManageBooking.aspx.cs" Inherits="ManageBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="\Css/account.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        $.fn.digits = function () {
            return this.each(function () {
                $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
        $(document).ready(function () {
            $('#spnMemberName').empty().html($('.uName').html());
            $(".milesPoint").digits();
        });
        function ShowAirReceipt(ItineraryTripId) {
            $.ajax({
                type: 'POST',
                url: 'ManageBooking.aspx/ShowAirReceipt',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'TripId':'" + ItineraryTripId + "'}",
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        window.open('AirPrintReceipt.aspx', '_blank');
                    }
                    else
                        window.location = "Index.aspx";
                },
                error: function (errmsg) {

                }
            });
            return false;
        }
        function ShowHotelVoucher(TransactionReferenceCode) {
            $.ajax({
                type: 'POST',
                url: 'ManageBooking.aspx/ShowHotelVoucher',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'TransactionReferenceCode':'" + TransactionReferenceCode + "'}",
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        window.open('HotelVoucherPrint.aspx', '_blank');
                    }
                    else
                        window.location = "Index.aspx";
                },
                error: function (errmsg) {
                }
            });
            return false;
        }
        function ShowCarVoucher(BookingReferenceId, accessToken, reservationNumber) {
            $.ajax({
                type: 'POST',
                url: 'ManageBooking.aspx/ShowCarVoucher',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'BookingReferenceId':'" + BookingReferenceId + "','accessToken':'" + accessToken + "','reservationNumber':'" + reservationNumber + "'}",
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        window.open('PrintCarVoucher.aspx', '_blank');
                    }
                    else
                        window.location = "Index.aspx";
                },
                error: function (errmsg) {
                }
            });
            return false;
        }
        function ShowExperienceVoucher(uuid) {
            window.open('ExperienceBookingDetails.aspx?uuid=' + uuid, '_blank');
            //window.location.href = "/ExperienceBookingDetails.aspx?uuid="uuid;
        }
    </script>
    <style>
        #dvHeroSlider,
        .dvRedemptionMenu,
        .dvInnerBanner
        #sitemap {
            display: none;
        }
    </style>

    <div class="dvMember d-md-block d-none py-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12 text-center">
                    <h2 class="h1 heading-semibold text-colour1" id="lblMemberName">
                        <span class="">Welcome,</span>
                        <span id="spnMemberName"></span>
                    </h2>
                    <h2 class="h5 heading-bold text-colour1 mt-2 mb-3">
                        <span id="totAvbPointDiv">Total Points</span>
                        <span id="spnMemberCurrentBal" class="heading-bold text-colour1">0</span>
                    </h2>
                    <a
                        href="Index.aspx"
                        class="btn btn-one"
                        id="my_account_point_redeem_now">Redeem Now
                    </a>
                </div>
            </div>
        </div>
    </div>

    <div class="dvAccountMenu">
        <div class="container-xl">
            <div class="row equal-col my-3" id="AccMenu">
            </div>
        </div>
    </div>

    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-4">
                    <li class="me-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="StatementSummary.aspx">My Account</a></li>
                    <li class="breadcrumb-item active">Manage Booking</li>
                </ul>
            </nav>
        </div>
    </div>

    <!-- Modal -->
    <div class="dvCommonModal dvExampleModal modal fade" id="dvExampleModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title">
                        <span>Booking Details</span>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="bookingDetails"></div>
                </div>
                <%-- <div class="modal-footer">
            <button type="button" class="btn btn-two" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-one">Save changes</button>
          </div>--%>
            </div>
        </div>
    </div>

    <div class="dvManage pb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <div class="border b-radius p-3">
                        <div class="dvCommonAccordion accordion" id="manage-accordion">
                            <div class="accordion-item mb-3">
                                    <h2 class="accordion-header mb-0 ">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse1">
                                            <span>Flight Booking Details</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse1" class="collapse show" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0">
                                        <asp:Repeater ID="rptBookingDetails" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="bg-colour6 p-3">
                                                            <div class="row justify-content-between">
                                                                <div class="col-6 col-md-3 col-xl-2 mb-3 mb-xl-0">
                                                                    <p>
                                                                        <span class="h7 d-block heading-bold text-colour7">Departure Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DepartureDate")).ToString("dd/MM/yyyy")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 col-xl-2 mb-3 mb-xl-0">
                                                                    <p>
                                                                        <span class="h7 d-block heading-bold text-colour7">Return Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ArrivalDate")).ToString("dd/MM/yyyy") == "01/01/0001" ? "NA" : Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ArrivalDate")).ToString("dd/MM/yyyy") %>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 col-xl-2 mb-3 mb-xl-0">
                                                                    <p>
                                                                        <span class="h7 d-block heading-bold text-colour7">Details</span>
                                                                        <span class="h6 d-block">
                                                                            <%# DataBinder.Eval(Container, "DataItem.OriginLocation")%>
                                                                                    -
                                                                                    <%# DataBinder.Eval(Container, "DataItem.DestinationLocation")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 col-xl-2 mb-3 mb-xl-0">
                                                                    <p>
                                                                        <span class="h7 d-block heading-bold text-colour7">Ref No.</span>
                                                                        <span class="h6 d-block">
                                                                            <%# DataBinder.Eval(Container, "DataItem.ItineraryReference")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 col-xl-1">
                                                                    <p>
                                                                        <span class="h7 d-block heading-bold text-colour7">Trip Id</span>
                                                                        <span class="h6 d-block">
                                                                            <%# DataBinder.Eval(Container, "DataItem.ItineraryTripId")%><asp:HiddenField ID="hdnTripId"
                                                                                runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItineraryReference")%>' />
                                                                            <asp:HiddenField ID="hdnReferrerId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.RefererDetails.Id")%>' />
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 col-xl-1">
                                                                    <p>
                                                                        <span class="h7 d-block heading-bold text-colour7">Points</span>
                                                                        <span class="h6 d-block">
                                                                            <%# DataBinder.Eval(Container, "DataItem.FareDetails.TotalPoints")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-md-6 col-xl-2 mt-2">
                                                                    <p>
                                                                        <asp:LinkButton ID="BtnViewAir" CssClass="btn btn-one w-100" Text="View Details" runat="server" OnClientClick='<%#String.Format("javascript:return ShowAirReceipt(\"{0}\")",Eval("ItineraryReference").ToString())%>'></asp:LinkButton>
                                                                    </p>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="dvBorderBottom row">
                                                    <div class="col-12">
                                                        <div class="border-bottom"></div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div id="divFlightrecord" class="p-3" runat="server" visible="false">
                                            <asp:Label runat="server" ID="lblFlightrecord" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="accordion-item mb-3">
                                    <h2 class="accordion-header mb-0">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase collapsed" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse2">
                                            Hotel Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse2" class="collapse" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0">
                                        <asp:Repeater ID="rptHotelCancelBookingDetails" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="bg-colour6 p-3">
                                                            <div class="row justify-content-between">
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Check-in Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Convert.ToDateTime(Eval("searchCriteria.CheckInDate")).ToShortDateString()%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Check-out Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Convert.ToDateTime(Eval("searchCriteria.CheckOutDate")).ToShortDateString()%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Convert.ToDateTime(Eval("BookinDate")).ToShortDateString()%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Details</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("Hotel.basicinfo.hotelname")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Ref No.</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("TransactionReferenceCode")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Points</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("TotalPoint")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Status</span>
                                                                        <span class="h6 d-block">
                                                                            <%# (Convert.ToInt32(Eval("Status")).Equals(1))? "BOOKED" : "CANCELLED" %>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-md-3">
                                                                    <p>
                                                                        <asp:HiddenField ID="hdnCancelHotelBooking" Value='<%#String.Concat(Eval("ExternalBookingId"), "&bookid=", Eval("BookingId").ToString(),"&transRef=",Eval("TransactionReferenceCode").ToString())%>'
                                                                            runat="server"></asp:HiddenField>
                                                                        <asp:HiddenField ID="hdnTransRef" Value='<%#Eval("TransactionReferenceCode").ToString()%>'
                                                                            runat="server"></asp:HiddenField>
                                                                        <asp:LinkButton ID="imgBtnHotelVoucher" class="btn btn-one w-100" runat="server" Text="View Details" Enabled='<%# Convert.ToInt32(Eval("Status")).Equals(1) %>'
                                                                            OnClientClick='<%#String.Format("javascript:return ShowHotelVoucher(\"{0}\")",Eval("TransactionReferenceCode").ToString())%>'></asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="dvBorderBottom row">
                                                    <div class="col-12">
                                                        <div class="border-bottom"></div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div id="divHotelrecord" class="p-3" runat="server" visible="false">
                                            <asp:Label runat="server" ID="lblHotelrecord" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="accordion-item mb-3 ">
                                    <h2 class="accordion-header mb-0">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase collapsed" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse3">
                                            Experience Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse3" class="collapse" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0" id="divExperienceBookingDetails" runat="server">
                                        <asp:Repeater ID="rptExperienceBookingDetails" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="bg-colour6 p-3">
                                                            <div class="row justify-content-between">
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Product Title</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("prodtitle")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Option</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("productTypeTitle")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Code</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("code")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Uuid</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("uuid")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("bookingDate")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Arrival Date</span>
                                                                        <span class="h6 d-block">
                                                                            <%#Eval("arrivalDate")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                              <%--  <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Price</span>
                                                                        <span class="h6 d-block">
                                                                            <%# Convert.ToInt32(Eval("grandTotalAmount")) %>
                                                                        </span>
                                                                    </p>
                                                                </div>--%>
                                                                <div class="col-6 col-md-3 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Status</span>
                                                                        <span class="h6 d-block">
                                                     
                                                                            <%# Eval("status") %>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-md-3">
                                                                    <p>
                                                               <%--       <a target="_blank" href ="/ExperienceBookingDetails.aspx?uuid="<%#Eval("uuid")%>" class="btn btn-one w-100">View Details</a>--%>
                                                                        <asp:LinkButton ID="imgBtnHotelVoucher" class="btn btn-one w-100" runat="server" Text="View Details"
                                                                            OnClientClick='<%#String.Format("javascript:return ShowExperienceVoucher(\"{0}\")",Eval("uuid"))%>'></asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="dvBorderBottom row">
                                                    <div class="col-12">
                                                        <div class="border-bottom"></div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div id="divExperiencerecord" class="p-3" runat="server" visible="false">
                                            <asp:Label runat="server" ID="lblExperiencerecord" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="accordion mb-3 d-none">
                                    <h2 class="accordion-header mb-0">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase collapsed" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse4">
                                            Domestic Flight Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse4" class="collapse" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0" id="divDomesticFlightBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="accordion mb-3" style="display: none;">
                                    <h2 class="accordion-header mb-0">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase collapsed" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse5">
                                            Insurance Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse5" class="collapse" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0" id="divInsuranceFlightBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="accordion" style="display: none;">
                                    <h2 class="accordion-header mb-0">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase collapsed" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse6">
                                            ISP Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse6" class="collapse" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0" id="divISPFlightBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>

                            <div class="accordion">
                                    <h2 class="accordion-header mb-0">
                                        <button class="accordion-button btn- btn-block text-start p-3 h6 text-uppercase collapsed" type="button"
                                            data-bs-toggle="collapse" data-bs-target="#collapse7">
                                            <span>Car Booking Details</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                <div id="collapse7" class="collapse" data-bs-parent="#manage-accordion">
                                    <div class="accordion-body scroll-ver p-0">
                                        <asp:Repeater ID="rptCarBookingDetails" runat="server">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="bg-colour6 p-3">
                                                            <div class="row justify-content-between">
                                                                <div class="col-12 col-sm-6 col-lg-4 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Car Name</span>
                                                                        <span class="d-block h6">
                                                                            <%#Eval("Vehicle_name")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-lg-4 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Ref Id</span>
                                                                        <span class="d-block h6">
                                                                            <%#Eval("Reference_Unique_Id") %>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-lg-4 mb-3">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Pick-Up Details</span>
                                                                        <span class="d-block h6">
                                                                            <%--  <%#Eval("pickUpDateTime")%>--%>
                                                                            <%#Eval("pickUpBranchLine")%> 
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-lg-4">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Drop-Off Details</span>
                                                                        <span class="d-block h6">
                                                                            <%--  <%#Eval("dropOffDateTime")%>--%>
                                                                            <%#Eval("dropOffBranchLine")%> 
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-lg-4">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Status</span>
                                                                        <span class="d-block h6">
                                                                            <%#Eval("Status")%>
                                                                        </span>
                                                                    </p>
                                                                </div>
                                                                <div class="col-12 col-sm-6 col-lg-4">
                                                                    <span>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Points</span>
                                                                        <span class="d-block- h6">
                                                                            <%#Eval("Payment_Amount")%>
                                                                        </span>
                                                                    </span>
                                                                    <asp:HiddenField ID="HiddenField1" Value='<%#Eval("Reference_Unique_Id").ToString()%>' runat="server"></asp:HiddenField>
                                                                    <asp:LinkButton CssClass="btn btn-one w-50 ms-2" ID="LinkButton1" runat="server" Text="Print" OnClientClick='<%#String.Format("javascript:return ShowCarVoucher(\"{0}\",\"{1}\",\"{2}\")",Eval("Reference_Unique_Id").ToString(),Eval("Access_Token").ToString(),Eval("Reserve_Number").ToString())%>'></asp:LinkButton>
                                                                </div>
                                                                <%-- <div class="col-12 col-md-3 mb-1">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Pickup Location</span>
                                                                        <span class="d-block h6"> 
                                                                        </span>
                                                                    </p>
                                                                </div>--%>
                                                                <%--  <div class="col-12 col-md-3 mb-1 mt-2 mt-lg-0">
                                                                    <p>
                                                                        <span class="h7 d-block heading-semibold text-colour7">Booking Drop-Off Location</span>
                                                                            </p>
                                                                </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="dvBorderBottom row">
                                                    <div class="col-12">
                                                        <div class="border-bottom"></div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div id="divCarrecord" runat="server" visible="false">
                                            <asp:Label runat="server" ID="lblCarrecord" Visible="false"></asp:Label>
                                        </div>
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
