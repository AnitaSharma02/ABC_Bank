<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductStatus.aspx.cs" Inherits="ExperienceProductStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />
    <div class="dvExperience mb-0 mb-md-2">
        <div class="container">
            <div class="dvPaymentList-steps d-none d-md-block">
                <div class="step-line"></div>
                <div class="steps">
                    <div class="payment-step">
                        <span class="step">
                            <span class="step-circle future rounded-circle">1</span>
                            <span class="d-block d-md-inline heading-reqular">Booking Details</span>
                        </span>
                    </div>

                    <div class="payment-step">
                        <span class="step">
                            <span class="step-circle future rounded-circle">2</span>
                            <span class="d-block d-md-inline heading-reqular">Payment Details</span>
                        </span>
                    </div>
                    <div class="payment-step">
                        <span class="step">
                            <span class="step-circle active future rounded-circle">3</span>
                            <step class="d-block d-md-inline stepText active heading-reqular ">Thank You!</step>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="dvPaymentBox mt-0 mt-md-2 pt-md-5 pt-3 pb-5" style="min-height: 350px;">
        <div class="container">
            <div class="row justify-content-md-center">
                <div class="col-12 col-md-8 col-lg-8 mt-3 mt-md-0" id="divMessage" runat="server">
                    <%--<div class="border dvCongrat bg-white p-5 text-center">
                        <p class="h5 heading-bold">Congratulations!</p>
                        <p class="heading-light pt-2">Your Order is placed successfully, an email confirmation will be sent on your registered email id.</p>

                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

