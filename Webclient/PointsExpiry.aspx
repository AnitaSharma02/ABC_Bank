<%@ Page Title="Points Expiry" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PointsExpiry.aspx.cs" Inherits="PointsExpiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">

    <link href="Css/account.css" rel="stylesheet" type="text/css" />
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
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item" ><a href="StatementSummary.aspx">My Account</a></li>
                    <li class="breadcrumb-item active">Points Expiry</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvExpiry pb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12 mb-3">
                    <div class="bg-colour2 p-3">
                        <div class="row align-items-center">
                            <div class="col-7 col-sm-9 col-lg-10">
                                <p class="">Please select the year to view your Points expiry schedule:</p>
                            </div>
                            <div class="col-5 col-sm-3 col-lg-2">
                                <asp:DropDownList ID="dtYear" runat="server" OnSelectedIndexChanged="dtYear_SelectedIndexChanged"
                                    AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <!-- <div class="col-sm"></div> -->
                        </div>
                    </div>

                     <div class="bg-colour2 px-3">
                        <div class="row mx-0" id="rptExpirySchedule" runat="server"></div>
                    </div>

                    <div class="bg-colour2 pt-0 pb-3">
                        <div class="row">
                            <div class="col-12">
                                <p class="heading-bold text-colour7" id="divExpiredon" runat="server">
                                    <span>Your </span>
                                    <asp:Label ID="lblMiles" runat="server" Text=""></asp:Label>
                                    <span>Points are going to expire on </span>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
