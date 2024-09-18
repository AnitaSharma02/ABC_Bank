<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceBookingDetails.aspx.cs" Inherits="ExperienceBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <%--<link href="Css/account.css" rel="stylesheet" />--%>
    <link href="Css/experience.css" rel="stylesheet" />

    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu {
            display: none;
        }
    </style>

    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item"><a href="ManageBooking.aspx" data-i18n="bread-manage">Manage Booking</a></li>
                    <li class="breadcrumb-item active">Experience Booking Details</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvExperienceBookingDetails pb-5" id="OrderDetailsDiv" runat="server">
        <div class="container-xl">
            <div class="dvOrderId row">
                <div class="col-12">
                    <p class="h6 heading-regular">Booking Code: <span class="heading6" id="bookingCode" runat="server"></span></p>
                </div>
            </div>
        </div>
        <div class="container-xl">
            <div class="row">
                <div class="col-12 my-4">
                    <div class="bg-colour2 p-3 mt-sm-0">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour6 p-3">
                                    <div class="row align-items-sm-center justify-content-between">
                                        <div class="col-12 col-sm-auto col-lg-auto mb-2 mb-lg-0">
                                            <p class="">Product Name:</p>
                                            <p class="heading6" id="productName" runat="server"></p>
                                        </div>
                                        <div class="col-12 col-sm-auto col-lg-auto mb-2 mb-lg-0">
                                            <p class="">Option:</p>
                                            <p class="heading6" id="productTypeTitle" runat="server"></p>
                                        </div>
                                        <div class="col-12 col-sm-auto col-lg-auto mb-2 mb-lg-0">
                                            <p class="">Address:</p>
                                            <p class="heading6" id="Address" runat="server"></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="dvPaymentBox mt-0 mt-md-2 mb-4 pt-md-5 pt-3 pb-5">
            <div class="container-xl">
                <div class="row">
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="border">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="heading6">Booking Details</p>
                                </div>
                            </div>
                            <div class="bg-colour6 px-3">
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="heading6">Booked Date:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="" id="bookingdate" runat="server">
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="heading6">Arrival Date:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="" id="arrivaldate" runat="server">
                                        </p>
                                    </div>
                                </div>

                                <div id="timeslotdiv" runat="server">
                                    <div class="row">
                                        <div class="col-12 border-top mt-1 mb-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="heading6">Timeslot:</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                            <p class="" id="timeslot" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="heading6">Adults:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="" id="adultCount" runat="server">
                                        </p>
                                    </div>
                                </div>

                                <div id="ChildCountDiv" runat="server">
                                    <div class="row">
                                        <div class="col-12 border-top mt-1 mb-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="heading6">Children:</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                            <p class="" id="childCount" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div id="SeniorCountDiv" runat="server">
                                    <div class="row">
                                        <div class="col-12 border-top mt-1 mb-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="heading6">Seniors:</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                            <p class="" id="seniorCount" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="heading6">Total Price:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="" id="totalPrice" runat="server">
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="border mt-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="heading6">Guest Info</p>
                                </div>
                            </div>
                            <div class="bg-colour6 px-3">
                                <div class="row">
                                    <div class="dvSelectDate col-4 my-2">
                                        <p class="heading6">Name:</p>
                                    </div>
                                    <div class="dvSelectDate col-8 my-2 text-right">
                                        <p class="" id="Name" runat="server"></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-4 my-2">
                                        <p class="heading6">E-mail:</p>
                                    </div>
                                    <div class="dvSelectDate col-8 my-2 text-right">
                                        <p class="text-break" id="EmailId" runat="server">
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-4 my-2">
                                        <p class="heading6">Phone:</p>
                                    </div>
                                    <div class="dvSelectDate col-8 my-2 text-right">
                                        <p class="" id="Phone" runat="server">
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 col-lg-8 mt-3 mt-md-0">
                        <div class="border bg-colour6 mb-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="heading6">Cancellation Policy</p>
                                </div>
                            </div>
                            <div class="dvSelectDate my-2">
                                <div class="dvHighligts col-12">
                                    <p class="">Cancellations are non refundable.</p>
                                </div>
                            </div>
                        </div>
                        <div class="border bg-colour6 mb-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="heading6">Additional Info</p>
                                </div>
                            </div>
                            <div class="dvSelectDate my-2">
                                <div class="dvHighligts col-12" id="AdditionalInfo" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="border bg-colour6 mb-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="heading6">Pickup/Meeting Point Information</p>
                                </div>
                            </div>
                            <div class="dvSelectDate my-2">
                                <div class="dvHighligts col-12">
                                    <div class="row" id="MeetingTimeDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="heading6">Time :</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-left">
                                            <p class="" id="MeetingTime" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="MeetingAddressDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="heading6">Address :</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-left">
                                            <p class="" id="MeetingAddress" runat="server">
                                            </p>
                                        </div>
                                    </div>

                                    <div class="row" id="MeetingLocationDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="heading6">Location :</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-left">
                                            <p class="" id="MeetingLocation" runat="server">
                                            </p>
                                        </div>
                                    </div>


                                    <div class="row" id="PickUpMeetingErrorDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="">No pickup/meeting point information available.</p>
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

    <div class="dvError d-flex justify-content-center align-items-center vh-center" id="ErrorDiv" runat="server">
        <div class="col text-center">
            <div class="bg-colour4 p-5">
                <i class="fa-solid fa-circle-exclamation"></i>
                <h2 class="h2 heading-semibold text-colour1 mb-3">OOPS...!</h2>
                <p class="alert alert-danger">We are facing some issue while fetching booking details. Please try again after sometime.</p>
            </div>
        </div>
    </div>
</asp:Content>

