<%@ Page Title="Order History" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/account.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#spnMemberName').empty().html($('.uName').html());
            BindCustomerOrders(0);
        });
        function BindCustomerOrders(PageNo) {
            $.ajax({
                type: 'POST',
                url: 'OrderHistory.aspx/BindCustomerOrders',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{skip:" + PageNo + "}",
                success: function (msg) {
                    if (msg.d != '') {
                        $("#divOrderList").html(msg.d);
                    }
                },
                beforeSend: function () {
                    $("#updProgress").show();
                },
                error: function (err) {
                }
            })
        }
    </script>
    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        .dvShopMenu {
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
                     <li class="breadcrumb-item" ><a href="StatementSummary.aspx">My Account</a></li>
                    <li class="breadcrumb-item active">Order History</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="dvOrderHistory pb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <div class="bg-colour2 p-3">
                        <div class="radioTransaction" style="display: none;">
                            <div class="acc-trnsactn-type">
                                <label for="tab1" class="active"><a href="../MyOrders.aspx">GiftCards</a></label>
                            </div>
                            <div class="acc-trnsactn-type">
                                <label for="tab4"><a href="Shop/OrderHistory.aspx">Shop Orders</a></label>
                            </div>
                            <div class="tab-panels" style="display: none">
                                <div class="myacc-conthdr">
                                    <div class="wrap chkpgs myaccpgs">
                                        <div class="pgcol1 card-body">

                                            <div class="myacc-cont">
                                                <div>
                                                    <div class="">
                                                        <div class="acco2">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clr"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOrderList"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
