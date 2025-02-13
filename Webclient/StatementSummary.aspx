<%@ Page Title="Statement Summary" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="StatementSummary.aspx.cs" Inherits="StatementSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/account.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#spnMemberName').empty().html($('.uName').html());
            GetStatementSummary();
        });
        function GetStatementSummary() {
            $.ajax({
                type: 'POST',
                url: 'StatementSummary.aspx/GetStatementSummary',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                global: false,
                cache: false,
                success: function (msg) {
                    if (msg.d.length > 0) {
                        $('#CP_lblExpiredPoints').html(msg.d[0]);
                        $('#CP_lblRedeemedmile').html(msg.d[1]);
                        $('#CP_lblBonusmile').html(msg.d[2]);
                        $('#CP_lblSpendmile').html(msg.d[3]);
                        $('#CP_lblPurchasemile').html(msg.d[4]);
                        $('#CP_lblPoints').html(msg.d[5]);
                    }
                },
                error: function (errmsg) {
                }
            });
        }
        function BindPointsExpiryDetails() {
            $.ajax({
                type: 'POST',
                url: 'StatementSummary.aspx/BindPointsExpiryDetails',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                global: false,
                cache: false,
                success: function (msg) {
                    if (msg.d.length > 0) {
                        if (msg.d[0] == 'quarterly') {
                            $('#CP_ulPointExpQuarterly').show();
                            $('#CP_lblQ1Points').html(msg.d[1]);
                            $('#CP_lblQ2Points').html(msg.d[2]);
                            $('#CP_lblQ3Points').html(msg.d[3]);
                            $('#CP_lblQ4Points').html(msg.d[4]);

                            $('#CP_lblQ1Year').html(msg.d[5]);
                            $('#CP_lblQ2Year').html(msg.d[6]);
                            $('#CP_lblQ3Year').html(msg.d[7]);
                            $('#CP_lblQ4Year').html(msg.d[8]);
                        }
                        else if (msg.d[0] == 'monthly') {
                            $('#CP_ulPointExpMonthly').show();
                            $('#CP_ulPointExpMonthly').empty().html(msg.d[9]);
                            $('.bxslider').bxSlider({
                                minSlides: 1,
                                maxSlides: 5,
                                slideWidth: 170,
                                slideMargin: 10,
                                responsive: true
                            });
                        }
                    }
                },
                error: function (errmsg) {
                }
            });
        }
    </script>

    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        .dvShopMenu{
            display: none;
        }
    </style>

    <%--<div class="dvMember">
        <div class="d-md-block d-none">
            <div class="align-items-center bg-acc d-flex justify-content-center">
                <div class="d-flex justify-content-center align-items-center flex-column">
                    <ul>
                        <li class="d-block">
                            <div class="text-center">
                                <h2 class="h3 heading-semibold text-white" id="lblMemberName"><span class="acc-text">Welcome,</span><span class="ms-2 acc-text" id="spnMemberName"></span></h2>
                            </div>
                        </li>
                        <li class="d-block">
                            <div class="text-center">
                                <h3 class="h3 heading-semibold text-white">
                                    <span id="totAvbPointDiv"></span>
                                    <span id="spnMemberCurrentBal" class="ms-2 heading-bold text-white"></span>
                                </h3>
                            </div>
                        </li>
                    </ul>
                    <div class="mt-3">
                        <a
                            href="Index.aspx"
                            class="btn btn-two"
                            id="my_account_point_redeem_now">Redeem Now</a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="dvMember d-md-block d-none py-5">
    <div class="container-xl">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="h1 heading-semibold text-colour1" id="lblMemberName">
                    <span class="">Welcome,</span>
                    <span id="spnMemberName"></span>
                </h2>
                <h2 class="h5 heading-bold text-colour1 mt-2 mb-3">
                    <span id="totAvbPointDiv" >Total Points</span>
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
                    <li class="breadcrumb-item">Statement Summary</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvStatementSummary pb-5">
        <div class="container-xl">
            <div class="row">
                <div class="dvEarned col-12 mb-4">
                    <div class="bg-colour2 py-2 b-radius">
                        <img class="d-inline-block ps-2 pe-1 ps-sm-3 pe-sm-2" src="/images/icons/other/plus.svg" />
                        <h5 id="my_account_poin_summary_earned" class="d-inline-block h6 heading-bold">Earned Points</h5>
                    </div>
                    <div class="d-flex flex-wrap">
                        <div class="col-6 border-bottom border-right border-left py-3">
                            <p class="">Bonus Points</p>
                        </div>
                        <div class="col-6 border-bottom border-right py-3">
                            <asp:Label ID="lblBonusmile" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                    <div class="d-flex flex-wrap">
                        <div class="col-6 border-bottom border-right border-left py-3">
                            <p id="my_account_poin_summary_spend" class="">On Spend Points</p>
                        </div>
                        <div class="col-6 border-bottom border-right py-3">
                            <asp:Label ID="lblSpendmile" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                    <div class="d-none flex-wrap">
                        <div class="col-6 border-bottom border-right border-left py-3">
                            <p id="my_account_poin_summary_purchase" class="">Purchase Points</p>
                        </div>
                        <div class="col-6 border-bottom border-right py-3">
                            <asp:Label ID="lblPurchasemile" runat="server" Text="0"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="dvRedeemed col-12 mb-4">
                    <div class="bg-colour2 py-2 b-radius">
                        <img class="d-inline-block ps-2 pe-1 ps-sm-3 pe-sm-2" src="/images/icons/other/minus.svg" />
                        <h5 id="my_account_poin_summary_redeemed" class="d-inline-block h6 heading-bold">Redeemed Points</h5>
                    </div>
                    <div class="d-flex flex-wrap">
                        <div class="col-6 border-bottom border-right border-left py-3">
                            <p>Points</p>
                        </div>
                        <div class="col-6 border-bottom border-right py-3">
                            <p>
                                <asp:Label ID="lblRedeemedmile" runat="server" Text="0"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="dvExpired col-12 mb-4 d-none">
                    <div class="bg-colour2 py-2 b-radius">
                        <img class="d-inline-block ps-2 pe-1 ps-sm-3 pe-sm-2" src="/images/icons/other/minus.svg" />
                        <h5 class="d-inline-block heading-bold">Expired Points</h5>
                    </div>
                    <div class="d-flex flex-wrap">
                        <div class="col-6 border-bottom border-right border-left py-3">
                            <p>Points</p>
                        </div>
                        <div class="col-6 border-bottom border-right py-3">
                            <p>
                                <asp:Label ID="lblExpiredPoints" runat="server" Text="0"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>

                <div class="dvBalance col-12">
                    <div class="bg-colour1 py-2 b-radius d-flex flex-wrap justify-content-between align-items-center">
                        <div>
                            <img class="d-inline-block ps-2 pe-1 ps-sm-3 pe-sm-2" src="/images/icons/other/equal.svg" />
                            <h5 class="d-inline-block h6 heading-bold text-white">Points Balance</h5>
                        </div>
                        <span id="my_account_poin_summary_balance" class="d-inline-block text-white heading-bold px-3">
                            <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>
                        </span>
                    </div>
                    <div class="d-flex">
                        <div class="col-12 border py-3">
                            <ul class="ps-3">
                                <li>Bonus Points: are the extra points given by the Bank as Bonus.</li>
                                <li>Spend Points: are the points earned/accumulated by using various products & services of Bank.
                                </li>
                                <li>Redeemed Points: are the points utilized by the user.</li>
                                <li>Closing Points: are the points available in loyalty account.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
