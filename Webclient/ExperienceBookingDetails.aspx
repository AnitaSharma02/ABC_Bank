<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceBookingDetails.aspx.cs" Inherits="ExperienceBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/account.css" rel="stylesheet" />
    <link href="Css/experience.css" rel="stylesheet" />
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ol class="breadcrumb py-3 px-0 mb-0">
                    <li class="mr-3">
                        <a href="\home">
                            <img src="/images/icons/back-arrow.svg" alt="back">
                        </a>
                    </li>
                    <li class="breadcrumb-item" onclick="window.close();">Retrieve History</li>
                    <li class="breadcrumb-item active">Experience Booking Details</li>
                </ol>
            </nav>
        </div>
    </div>
    <div class="dvOrderDetails pb-5" id="OrderDetailsDiv" runat="server">
        <div class="container-lg">
            <div class="dvOrderId row">
                <div class="col-12">
                    <p class="h6 heading-regular">Booking Code: <span class="h6 heading-semibold" id="bookingCode" runat="server"></span></p>
                </div>
            </div>
        </div>
        <div class="container-lg">
            <div class="row">
                <div class="col-12 my-4">
                    <div class="bg-colour3 p-3 mt-sm-0">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-white p-3">
                                    <div class="row align-items-sm-center justify-content-between">
                                        <div class="col-12 col-sm-auto col-lg-auto mb-2 mb-lg-0">
                                            <p class="h6 heading-regular">Product Name:</p>
                                            <p class="h6 heading-semibold" id="productName" runat="server"></p>
                                        </div>
                                        <div class="col-12 col-sm-auto col-lg-auto mb-2 mb-lg-0">
                                            <p class="h6 heading-regular">Option:</p>
                                            <p class="h6 heading-semibold" id="productTypeTitle" runat="server"></p>
                                        </div>
                                        <div class="col-12 col-sm-auto col-lg-auto mb-2 mb-lg-0">
                                            <p class="h6 heading-regular">Address:</p>
                                            <p class="h6 heading-semibold" id="Address" runat="server"></p>
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
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="border">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="h6 heading-semibold">Booking Details</p>
                                </div>
                            </div>
                            <div class="bg-white px-3">
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="h6 heading-semibold">Booked Date:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="h6 heading-regular" id="bookingdate" runat="server">
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="h6 heading-semibold">Arrival Date:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="h6 heading-regular" id="arrivaldate" runat="server">
                                        </p>
                                    </div>
                                </div>

                                <div id="timeslotdiv" runat="server">
                                    <div class="row">
                                        <div class="col-12 border-top mt-1 mb-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="h6 heading-semibold">Timeslot:</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                            <p class="h6 heading-regular" id="timeslot" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="h6 heading-semibold">Adults:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="h6 heading-regular" id="adultCount" runat="server">
                                        </p>
                                    </div>
                                </div>

                                <div id="ChildCountDiv" runat="server">
                                    <div class="row">
                                        <div class="col-12 border-top mt-1 mb-1"></div>
                                    </div>
                                    <div class="row">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="h6 heading-semibold">Children:</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                            <p class="h6 heading-regular" id="childCount" runat="server">
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
                                            <p class="h6 heading-semibold">Seniors:</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                            <p class="h6 heading-regular" id="seniorCount" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                        <p class="h6 heading-semibold">Total Price:</p>
                                    </div>
                                    <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-right">
                                        <p class="h6 heading-regular" id="totalPrice" runat="server">
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="border mt-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="h6 heading-semibold">Guest Info</p>
                                </div>
                            </div>
                            <div class="bg-white px-3">
                                <div class="row">
                                    <div class="dvSelectDate col-4 my-2">
                                        <p class="h6 heading-semibold">Name:</p>
                                    </div>
                                    <div class="dvSelectDate col-8 my-2 text-right">
                                        <p class="h6 heading-regular" id="Name" runat="server"></p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-4 my-2">
                                        <p class="h6 heading-semibold">E-mail:</p>
                                    </div>
                                    <div class="dvSelectDate col-8 my-2 text-right">
                                        <p class="h6 heading-regular text-break" id="EmailId" runat="server">
                                        </p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 border-top mt-1 mb-1"></div>
                                </div>
                                <div class="row">
                                    <div class="dvSelectDate col-4 my-2">
                                        <p class="h6 heading-semibold">Phone:</p>
                                    </div>
                                    <div class="dvSelectDate col-8 my-2 text-right">
                                        <p class="h6 heading-regular" id="Phone" runat="server">
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-6 col-lg-8 mt-3 mt-md-0">
                        <div class="border bg-white mb-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="h6 heading-semibold">Cancellation Policy</p>
                                </div>
                            </div>
                            <div class="dvSelectDate my-2">
                                <div class="dvHighligts col-12">
                                    <p class="h6 heading-regular">Cancellations are non refundable.</p>
                                </div>
                            </div>
                        </div>
                        <div class="border bg-white mb-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="h6 heading-semibold">Additional Info</p>
                                </div>
                            </div>
                            <div class="dvSelectDate my-2">
                                <div class="dvHighligts col-12" id="AdditionalInfo" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="border bg-white mb-3">
                            <div class="dvBookingDetails">
                                <div class="d-flex justify-content-between">
                                    <p class="h6 heading-semibold">Pickup/Meeting Point Information</p>
                                </div>
                            </div>
                            <div class="dvSelectDate my-2">
                                <div class="dvHighligts col-12">
                                    <div class="row" id="MeetingTimeDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="h6 heading-semibold">Time :</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-left">
                                            <p class="h6 heading-regular" id="MeetingTime" runat="server">
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="MeetingAddressDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="h6 heading-semibold">Address :</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-left">
                                            <p class="h6 heading-regular" id="MeetingAddress" runat="server">
                                            </p>
                                        </div>
                                    </div>

                                    <div class="row" id="MeetingLocationDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="h6 heading-semibold">Location :</p>
                                        </div>
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-7 my-2 text-left">
                                            <p class="h6 heading-regular" id="MeetingLocation" runat="server">
                                            </p>
                                        </div>
                                    </div>


                                    <div class="row" id="PickUpMeetingErrorDiv" runat="server">
                                        <div class="dvSelectDate col-6 col-md-6 col-lg-5 my-2">
                                            <p class="h6 heading-regular">No pickup/meeting point information available.</p>
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

