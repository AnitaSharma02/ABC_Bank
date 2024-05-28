<%@ Page Title="Order Status" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="OrderStatus.aspx.cs" Inherits="OrderStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    
    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item active" data-i18n="navigation-order-status">Order Status</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvOrderStatus d-flex flex-wrap justify-content-center align-items-center vh-center- pb-5 pt-3 pt-lg-5">
        <div id="divSuccess" class="col-sm-8 text-center" runat="server">
            <div class="bg-colour2 p-5">
                <h2 class="h2 heading-semibold text-center text-colour1 mb-3">Congratulations!</h2>
                <h2 class="h6 heading-regular text-center mb-3">Your Order is placed successfully, an email confirmation will be sent on your registered email id.</h2>
            </div>
        </div>
        <div id="divFailed" class="col-sm-8 text-center" runat="server">
            <div class="bg-colour2 p-5">
                <h2 class="h2 heading-semibold text-center text-colour1 mb-3">Sorry</h2>
                <h2 class="h6 heading-regular text-center mb-3">Your Order didn't go through, please try again after some time.</h2>
            </div>
        </div>
    </div>

</asp:Content>

