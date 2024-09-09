<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductStatus.aspx.cs" Inherits="ExperienceProductStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />
    <div class="dvExperienceProductStatus mb-0 mb-md-2">
        <div class="container-xl">
            <div class="row dvDeliveryTrack">
                <div class="col-12">
                    <div class="row">
                        <div class="col-4 mb-lg-3">
                            <div class="dvLine border d-none d-md-block px-3"></div>
                            <div class="row justify-content-md-center">
                                <div class="col-md-auto my-3">
                                    <div class="d-flex flex-column flex-sm-row align-items-center active">
                                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">1</span>
                                        <a class="h7 heading-regular bg-colour6 px-3 text-center text-colour7">Booking Details</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4 mb-lg-3">
                            <div class="dvLine border d-none d-md-block px-3"></div>
                            <div class="row justify-content-md-center">
                                <div class="col-md-auto my-3">
                                    <div class="d-flex flex-column flex-sm-row align-items-center">
                                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">2</span>
                                        <a class="h7 heading-regular bg-colour6 px-3 text-center text-colour7" id="hrefBookingDetailsId" runat="server">Payment Details</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4 mb-lg-3">
                            <%--<div class="dvLine border d-none d-md-block px-3"></div>--%>
                            <div class="row justify-content-md-center">
                                <div class="col-md-auto my-3">
                                    <div class="d-flex flex-column flex-sm-row align-items-center">
                                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">3</span>
                                        <a class="h7 heading-regular bg-colour6 px-3 text-center text-colour7">Thank You</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 text-center" id="divMessage" runat="server"></div>
            </div>
        </div>
    </div>
</asp:Content>

