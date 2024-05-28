<%@ Page Title="Manage Booking" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="ManageBooking.aspx.cs" Inherits="ManageBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="\Css/MyAccount.css" rel="stylesheet" type="text/css" />
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
                        window.location = "Login.aspx";
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
                        window.location = "Login.aspx";
                },
                error: function (errmsg) {
                }
            });
            return false;
        }
        function ShowCarVoucher(BookingReferenceId) {
            $.ajax({
                type: 'POST',
                url: 'ManageBooking.aspx/ShowCarVoucher',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'BookingReferenceId':'" + BookingReferenceId + "'}",
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        window.open('PrintCarVoucher.aspx', '_blank');
                    }
                    else
                        window.location = "Login.aspx";
                },
                error: function (errmsg) {
                }
            });
            return false;
        }
        function ViewDetails(ReferenceId) {
            //debugger
            $.ajax({
                type: 'POST',
                url: 'ManageBooking.aspx/ShowInsuranceBookingDetails',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                //data: "{'ReferenceId':" + ReferenceId + "}",
                data: "{ReferenceId:'" + ReferenceId + "'}",
                cache: false,
                success: function (msg) {
                    //debugger;
                    if (msg.d != null) {
                        var pop = document.getElementById("bookingDetails");
                        pop.innerHTML = msg.d;
                        $('#exampleModal').modal('show');
                    }
                },
                error: function (errmsg) {
                }
            });
            return false;
        }
    </script>
    <style>
        #dvHeroSlider {
            display: none;
        }

        .dvRedemptionMenu {
            display: none;
        }

        #sitemap {
            display: none;
        }

        .dvInnerBanner {
            display: none
        }
    </style>

    <%--<div class="dvMember">
        <div class="d-md-block d-none">
            <div class="align-items-center bg-acc d-flex justify-content-center">
                <div class="d-flex justify-content-center align-items-center flex-column">
                    <ul>
                        <li class="d-block">
                            <div class="text-center">
                                <h2 class="h3 heading-semibold text-white" id="lblMemberName"><span data-i18n="account-welcome" class="acc-text">Welcome,</span><span class="ml-2 acc-text" id="spnMemberName"></span></h2>
                            </div>
                        </li>
                        <li class="d-block">
                            <div class="text-center">
                                <h3 class="h3 heading-semibold text-white" >
                                    <span id="totAvbPointDiv" data-i18n="account-total-points">Total NPoints</span>
                                        <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-white"></span></h3>
                            </div>
                        </li>
                    </ul>
                    <div class="mt-3">
                        <a
                            href="Index.aspx"
                            class="btn btn-two"
                            id="my_account_point_redeem_now"
                            data-i18n="btn-redeem-now">Redeem Now</a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="dvMember d-md-block d-none py-5">
    <div class="container-lg">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="h1 heading-light text-colour1" id="lblMemberName">
                    <span data-i18n="account-welcome" class="">Welcome,</span>
                    <span class="ml-2" id="spnMemberName"></span>
                </h2>
                <h2 class="h5 heading-bold text-colour1 mt-2 mb-3">
                    <span id="totAvbPointDiv" >Total NPoints</span>
                    <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-colour1">0</span>
                </h2>
                <a
                    href="Index.aspx"
                    class="btn btn-one"
                    id="my_account_point_redeem_now"
                    data-i18n="btn-redeem-now">Redeem Now
                </a>
            </div>
        </div>
    </div>
</div>

    <div class="dvAccountMenu">
        <div class="container-lg">
            <div class="row equal-col my-3" id="AccMenu">
            </div>
        </div>
    </div>

    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-4">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item" data-i18n="bread-my-account">My Account</li>
                    <li class="breadcrumb-item active" data-i18n="bread-manage">Manage Booking</li>
                </ul>
            </nav>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered"" role="document">
        <div class="modal-content">
          <div class="modal-header" style="background: #E5A812;">
            <h5 class="modal-title" id="exampleModalLabel" >Booking Details</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
          <div id="bookingDetails"></div>    
          </div>
         <%-- <div class="modal-footer">
            <button type="button" class="btn btn-two" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-one">Save changes</button>
          </div>--%>
        </div>
      </div>
    </div>

    <div class="dvManage pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12">
                    <div class="bg-lightgray p-3">
                        <div class="accordion" id="manage-accordion">

                            <!-- FLIGHT BOOKING DETAILS -->
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0 ">
                                        <%--<button class="btn btn-block text-left p-3 heading-semibold" type="button" data-toggle="collapse"
                                            data-target="#collapse1" data-i18n="mb-flight-booking-details">
                                            Flight Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>--%>
                                        <button class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase" type="button"
                                            data-toggle="collapse" data-target="#collapse1">
                                            <span data-i18n="mb-flight-booking-details">Flight Booking Details</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>

                                <div id="collapse1" class="collapse show" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0">
                                        <div>
                                            <asp:Repeater ID="rptBookingDetails" runat="server">
                                                <ItemTemplate>
                                                    <div class="row mb-1">
                                                        <div class="col-12">
                                                            <div class="bg-white p-3">
                                                                <div class="row justify-content-between">
                                                                    <div class="col-6 col-md-3 col-xl-2 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-flight-departure-label">Departure Date</span>
                                                                            <span class="h6 d-block">
                                                                                <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DepartureDate")).ToString("dd/MM/yyyy")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 col-xl-2 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-flight-booking-label">Return Date</span>
                                                                            <span class="h6 d-block">
                                                                                <%# Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ArrivalDate")).ToString("dd/MM/yyyy") == "01/01/0001" ? "NA" : Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.ArrivalDate")).ToString("dd/MM/yyyy") %>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 col-xl-2 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-flight-details-label">Details</span>
                                                                            <span class="h6 d-block">
                                                                                <%# DataBinder.Eval(Container, "DataItem.OriginLocation")%>
                                                                                    -
                                                                                    <%# DataBinder.Eval(Container, "DataItem.DestinationLocation")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 col-xl-2 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-flight-ref-label">Ref No.</span>
                                                                            <span class="h6 d-block">
                                                                                <%# DataBinder.Eval(Container, "DataItem.ItineraryReference")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 col-xl-1 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-flight-trip-label">Trip Id</span>
                                                                            <span class="h6 d-block">
                                                                                <%# DataBinder.Eval(Container, "DataItem.ItineraryTripId")%><asp:HiddenField ID="hdnTripId"
                                                                                    runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ItineraryReference")%>' />
                                                                                <asp:HiddenField ID="hdnReferrerId" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.RefererDetails.Id")%>' />
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 col-xl-1 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-flight-points-label">NPoints</span>
                                                                            <span class="h6 d-block">
                                                                                <%# DataBinder.Eval(Container, "DataItem.FareDetails.TotalPoints")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-12 col-md-6 col-xl-2 mt-3 mt-md-0">
                                                                        <p>
                                                                            <asp:LinkButton ID="BtnViewAir" CssClass="btn btn-one w-100" Text="View Details" runat="server" OnClientClick='<%#String.Format("javascript:return ShowAirReceipt(\"{0}\")",Eval("ItineraryTripId").ToString())%>'></asp:LinkButton>
                                                                        </p>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <div id="divFlightrecord" runat="server" visible="false">
                                                <asp:Label runat="server" ID="lblFlightrecord" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- FLIGHT BOOKING DETAILS -->

                            <!-- HOTEL BOOKING DETAILS -->
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase collapsed" type="button"
                                            data-toggle="collapse" data-target="#collapse2">
                                            Hotel Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse2" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0">
                                        <div>
                                            <asp:Repeater ID="rptHotelCancelBookingDetails" runat="server">
                                                <ItemTemplate>
                                                    <div class="row mb-1">
                                                        <div class="col-12">
                                                            <div class="bg-white p-3">
                                                                <div class="row justify-content-between">
                                                                    <div class="col-6 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-checkin-label">Check-in Date</span>
                                                                            <span class="h6 d-block">
                                                                                <%#Convert.ToDateTime(Eval("searchCriteria.CheckInDate")).ToShortDateString()%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-checkout-label">Check-out Date</span>
                                                                            <span class="h6 d-block">
                                                                                <%#Convert.ToDateTime(Eval("searchCriteria.CheckOutDate")).ToShortDateString()%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-booking-label">Booking Date</span>
                                                                            <span class="h6 d-block">
                                                                                <%#Convert.ToDateTime(Eval("BookinDate")).ToShortDateString()%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-details-label">Details</span>
                                                                            <span class="h6 d-block">
                                                                                <%#Eval("Hotel.basicinfo.hotelname")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-12 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-ref-label">Ref No.</span>
                                                                            <span class="h6 d-block">
                                                                                <%#Eval("TransactionReferenceCode")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-points-label">NPoints</span>
                                                                            <span class="h6 d-block">
                                                                                <%#Eval("TotalPoint")%>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-6 col-md-3 mb-1">
                                                                        <p>
                                                                            <span class="h7 d-block heading-semibold" data-i18n="managebooking-hotel-status-label">Status</span>
                                                                            <span class="h6 d-block">
                                                                                <%# (Convert.ToInt32(Eval("Status")).Equals(1))? "BOOKED" : "CANCELLED" %>
                                                                            </span>
                                                                        </p>
                                                                    </div>
                                                                    <div class="col-12 col-md-3 mb-1 mt-2 mt-lg-0">
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
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <div id="divHotelrecord" runat="server" visible="false">
                                                <asp:Label runat="server" ID="lblHotelrecord" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- HOTEL BOOKING DETAILS -->

                            <!-- EXPERIENCE BOOKING DETAILS -->
                            <div class="card mb-3 d-none">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase collapsed" type="button"
                                            data-toggle="collapse" data-target="#collapse3">
                                            Experience Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse3" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0" id="divExperienceBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>
                            <!-- EXPERIENCE BOOKING DETAILS -->

                            <!-- DOMESTIC FLIGHT BOOKING DETAILS -->
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase collapsed" type="button"
                                            data-toggle="collapse" data-target="#collapse4">
                                            Domestic Flight Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse4" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0" id="divDomesticFlightBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>
                            <!-- DOMESTIC FLIGHT BOOKING DETAILS -->

                            <!-- INSURANCE BOOKING DETAILS -->
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase collapsed" type="button"
                                            data-toggle="collapse" data-target="#collapse5">
                                            Insurance Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse5" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0" id="divInsuranceFlightBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>
                            <!-- INSURANCE BOOKING DETAILS -->

                            <!-- ISP BOOKING DETAILS -->
                            <div class="card">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase collapsed" type="button"
                                            data-toggle="collapse" data-target="#collapse6">
                                            ISP Booking Details
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse6" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0" id="divISPFlightBookingDetails" runat="server">
                                    </div>
                                </div>
                            </div>
                            <!-- ISP BOOKING DETAILS -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
