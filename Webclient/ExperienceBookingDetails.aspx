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
                        <div class="dvOrderId my-3 row">
                            <div class="col-12">
                                <p class="h6 heading-regular">Booking Code: <span class="heading6" id="bookingCode" runat="server"></span></p>
                                <p class="">Product Name: <span class="heading6" id="productName" runat="server"></span></p>
                                <p class="">Option: <span class="heading6" id="productTypeTitle" runat="server"></span></p>
                                <p class="">Address: <span class="heading6" id="Address" runat="server"></span></p>
                            </div>
                        </div>

                        <div class="dvPaymentBox">
                            <div class="row">
                                <div class="col-12 col-lg-5">
                                    <div class="border">
                                        <div class="bg-colour1 p-3">
                                            <p class="heading6 text-colour6">Booking Details</p>
                                        </div>
                                        <div class="bg-colour6 px-3">
                                            <div class="row">
                                                <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                    <span class="heading6">Booked Date</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
                                                    <p class="" id="bookingdate" runat="server">
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12 border-top mt-1 mb-1"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                    <span class="heading6">Arrival Date</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
                                                    <p class="" id="arrivaldate" runat="server">
                                                    </p>
                                                </div>
                                            </div>

                                            <div id="timeslotdiv" runat="server">
                                                <div class="row">
                                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                        <span class="heading6">Timeslot</span>
                                                        <span>:</span>
                                                    </div>
                                                    <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
                                                        <p class="" id="timeslot" runat="server">
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-12 border-top mt-1 mb-1"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                    <span class="heading6">Adults</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
                                                    <p class="" id="adultCount" runat="server">
                                                    </p>
                                                </div>
                                            </div>

                                            <div id="ChildCountDiv" runat="server">
                                                <div class="row">
                                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                        <span class="heading6">Children</span>
                                                        <span>:</span>
                                                    </div>
                                                    <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
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
                                                    <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                        <span class="heading6">Seniors</span>
                                                        <span>:</span>
                                                    </div>
                                                    <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
                                                        <p class="" id="seniorCount" runat="server">
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-12 border-top mt-1 mb-1"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                    <span class="heading6">Total Price</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-6 col-lg-7 d-flex justify-content-between my-2">
                                                    <p class="" id="totalPrice" runat="server">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="border mt-3">
                                        <div class="bg-colour1 p-3">
                                            <p class="heading6 text-colour6">Guest Info</p>
                                        </div>
                                        <div class="bg-colour6 px-3">
                                            <div class="row">
                                                <div class="col-lg-5 col-6 d-flex justify-content-between my-2">
                                                    <span class="heading6">Name</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-lg-7 col-6 d-flex justify-content-between my-2">
                                                    <p class="" id="Name" runat="server"></p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12 border-top mt-1 mb-1"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-5 col-6 d-flex justify-content-between my-2">
                                                    <span class="heading6">E-mail</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-lg-7 col-6 d-flex justify-content-between my-2">
                                                    <p class="text-break" id="EmailId" runat="server">
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12 border-top mt-1 mb-1"></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-5 col-6 d-flex justify-content-between my-2">
                                                    <span class="heading6">Phone</span>
                                                    <span>:</span>
                                                </div>
                                                <div class="col-lg-7 col-6 d-flex justify-content-between my-2">
                                                    <p class="" id="Phone" runat="server">
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-lg-7 mt-3 mt-md-0">
                                    <div class="border bg-colour6 mb-3">
                                        <div class="bg-colour1 p-3">
                                            <p class="heading6 text-colour6">Cancellation Policy</p>
                                        </div>
                                        <div class="my-2">
                                            <div class="col-12">
                                                <p class="">Cancellations are non refundable.</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="border bg-colour6 mb-3">
                                        <div class="bg-colour1 p-3">
                                            <p class="heading6 text-colour6">Additional Info</p>
                                        </div>
                                        <div class="my-2">
                                            <div class="col-12" id="AdditionalInfo" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="border bg-colour6 mb-3">
                                        <div class="bg-colour1 p-3">
                                            <p class="heading6 text-colour6">Pickup/Meeting Point Information</p>
                                        </div>
                                        <div class="my-2">
                                            <div class="col-12">
                                                <div class="row" id="MeetingTimeDiv" runat="server">
                                                    <div class="col-6 col-lg-5 d-flex justify-content-between my-2">
                                                        <p class="heading6">Time :</p>
                                                    </div>
                                                    <div class="col-6 col-md-6 col-lg-7 my-2 text-left">
                                                        <p class="" id="MeetingTime" runat="server">
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="row" id="MeetingAddressDiv" runat="server">
                                                    <div class="col-6 col-md-5 col-lg-4 d-flex justify-content-between my-2">
                                                        <span class="heading6">Address</span>
                                                        <span>:</span>
                                                    </div>
                                                    <div class="col-6 col-md-6 col-lg-8 my-2 text-left">
                                                        <p class="" id="MeetingAddress" runat="server">
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                                </div>
                                                <div class="row" id="MeetingLocationDiv" runat="server">
                                                    <div class="col-6 col-md-5 col-lg-4 d-flex justify-content-between my-2">
                                                        <p class="heading6">Location</p>
                                                        <span>:</span>
                                                    </div>
                                                    <div class="col-6 col-md-6 col-lg-8 my-2 text-left">
                                                        <p class="" id="MeetingLocation" runat="server">
                                                        </p>
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

