<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceBookingDetails.aspx.cs" Inherits="ExperienceBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <%--<link href="Css/account.css" rel="stylesheet" />--%>
    <link href="Css/experience.css" rel="stylesheet" />

    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        .dvBreadcrumbs {
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

    <div class="container-xl my-3">
        <div class="row">
            <div class="col-12">
                <div class="border p-3">
                    <div class="row">
                        <div class="col-12">
                            <div class="bg-colour2 p-3">
                                <img src="Images/logos/infinity-logo.svg" alt="" />
                            </div>
                        </div>
                    </div>
                    <div class="dvExperienceBookingDetails" id="OrderDetailsDiv" runat="server">
                        <div class="row my-3">
                            <div class="col-12">
                                <p><span class="h6 heading-semibold text-colour7">Booking Code : </span><span id="bookingCode" runat="server"></span></p>
                                <p><span class="h6 heading-semibold text-colour7">Product Name : </span><span id="productName" runat="server"></span></p>
                                <p><span class="h6 heading-semibold text-colour7">Option : </span><span id="productTypeTitle" runat="server"></span></p>
                                <p><span class="h6 heading-semibold text-colour7">Address : </span><span id="Address" runat="server"></span></p>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3 border">Booking Details</h2>
                                <div class="px-3 border">
                                    <div class="row">
                                        <div class="col-6 col-md border-right">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Booked Date</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="bookingdate" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Arrival Date</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="arrivaldate" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right" id="timeslotdiv" runat="server">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Timeslot</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="timeslot" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Adults</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="adultCount" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right" id="ChildCountDiv" runat="server">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Children</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="childCount" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right" id="SeniorCountDiv" runat="server">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Seniors</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="seniorCount" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Total Price</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="totalPrice" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3 border">Guest Info</h2>
                                <div class="px-3 border">
                                    <div class="row">
                                        <div class="col-6 col-md border-right">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Name</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="Name" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">E-mail</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7 text-break" id="EmailId" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Phone</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="Phone" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3 border">Pickup/Meeting Point Information</h2>
                                <div class="px-3 border">
                                    <div class="row">
                                        <div class="col-6 col-md border-right" id="MeetingTimeDiv" runat="server">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Time</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="MeetingTime" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md border-right" id="MeetingAddressDiv" runat="server">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Address</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="MeetingAddress" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-6 col-md" id="MeetingLocationDiv" runat="server">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <h2 class="h7 heading-semibold text-colour7">Location</h2>
                                                </div>
                                                <div class="col-12 p-2">
                                                    <p class="h7" id="MeetingLocation" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="PickUpMeetingErrorDiv" runat="server">
                                        <div class="col-12 my-2">
                                            <p class="">No pickup/meeting point information available.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3 border">Cancellation Policy</h2>
                                <div class="px-3 border">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12 p-2">
                                                    <p class="h7">Cancellations are non refundable.</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <h2 class="h7 heading-semibold text-colour6 bg-colour1 p-3 border">Additional Info</h2>
                                <div class="px-3 border">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12" id="AdditionalInfo" runat="server"></div>
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

