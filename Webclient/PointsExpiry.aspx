<%@ Page Title="Points Expiry" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PointsExpiry.aspx.cs" Inherits="PointsExpiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">

    <link href="Css/MyAccount.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script>
        $.fn.digits = function () {
            return this.each(function () {
                $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
        $(document).ready(function () {
            $('#spnMemberName').empty().html($('.uName').html());
            $("#CP_lblMiles").digits();
        });
    </script>
    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        .dvShopMenu{
            display: none;
        }
    </style>


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
                    <li class="breadcrumb-item active" data-i18n="bread-expiry">Points Expiry</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvExpiry pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 mb-3">
                    <div class="bg-lightgrey p-3">
                        <div class="row align-items-center">
                            <div class="col-7 col-sm-9 col-lg-10">
                                <p class="h6 heading-semibold" data-i18n="pe-please-select-the-year">Please select the year to view your NPoints expiry schedule:</p>
                            </div>
                            <div class="col-5 col-sm-3 col-lg-2">
                                <asp:DropDownList ID="dtYear" runat="server" OnSelectedIndexChanged="dtYear_SelectedIndexChanged"
                                    AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <!-- <div class="col-sm"></div> -->
                        </div>
                    </div>

                     <div class="bg-lightgrey px-3">
                        <div class="row mx-0" id="rptExpirySchedule" runat="server"></div>
                    </div>

                    <div class="bg-lightgrey pt-0 pb-3">
                        <div class="row">
                            <div class="col-12">
                                <div class="h6 heading-semibold text-colour7" id="divExpiredon" runat="server">
                                    <span data-i18n="pe-your">Your </span>
                                    <asp:Label ID="lblMiles" runat="server" Text=""></asp:Label>
                                    <span data-i18n="pe-points-going-expire">NPoints are going to expire on </span>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
