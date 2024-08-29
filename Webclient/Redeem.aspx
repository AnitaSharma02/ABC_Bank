<%@ Page Title="Redeem" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="redeem.aspx.cs" Inherits="redeem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/shop.css" type="text/css" rel="stylesheet">
    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu {
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
                    <li class="breadcrumb-item active" data-i18n="navigation-redeem">Redeem</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvRedeem mb-5">
        <%--<h2 class="py-5 text-center">Coming Soon</h2>--%>
        <div class="container-xl">
            <div class="row">
                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aTravelLink" class="image">
                                <img id="imgTravel" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Flight</h2>
                            <p>If you’re making travel plans, this is a great redemption option for you. Choose from over 900 airlines and book your flights with your NPoints!</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aHotelLink" class="image">
                                <img id="imgHotel" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Hotels</h2>
                            <p>Make your travel dreams come true. Take your pick from more than 4,50,000 hotels across the world and enjoy your holiday!</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aVouchersLink" class="image">
                                <img id="imgVouchers" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Gift Cards</h2>
                            <p>It’s time to give your gifts a special touch. Redeem your NPoints for gift vouchers of your favourite brands and give them to your loved ones.</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aShopLink" class="image">
                                <img id="imgShop" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Shop</h2>
                            <p>If you like shopping, you’re going to love this redemption option. Redeem your NPoints for shopping your favourite products like apparel and electronics.</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aLoungesLink" class="image">
                                <img id="imgLounges" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Lounges</h2>
                            <p>Make your travel experience better with this loyalty program! Get access to (add number) international airport lunges with your loyalty membership!</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aMilesLink" class="image">
                                <img id="imgMiles" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Miles Exchange</h2>
                            <p>Coming Soon</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aOfferLink" class="image">
                                <img id="imgOffer" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Offers</h2>
                            <p>Coming Soon</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aISPLink" class="image">
                                <img id="imgISP" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Internet Service Provider</h2>
                            <p>Stay connected with ease by redeeming your NPoints for your internet service provider bills. Enjoy uninterrupted access to the online world while maximizing your rewards</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aUtilityLink" class="image">
                                <img id="imgUtility" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Utility</h2>
                            <p>There’s so much more you can do with your NPoints. Pay your utility bills from the NPoints you’ve accumulated in this program.</p>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 mb-4">
                    <div class="row justify-content-center justify-content-sm-start align-items-center">
                        <div class="col-auto d-flex justify-content-center">
                            <a href="javascript:void(0)" id="aInsuranceLink" class="image">
                                <img id="imgInsurance" alt="Alternate Text" />
                            </a>
                        </div>
                        <div class="col-12 col-sm-8 text-center text-sm-left">
                            <h2 class="h6 heading-semibold my-2">Insurance</h2>
                            <p>Protect what matters most with NPoints. Redeem them for insurance premiums, ensuring your peace of mind and financial security.</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            GetHomeRedemptionOptions();
        });
        function GetHomeRedemptionOptions() {
            try {
                let redemptionOptions = '<%=HttpContext.Current.Application["GetHomeRedemptionOptions"]%>';
                if (redemptionOptions != '') {
                    var json = $.parseJSON(redemptionOptions);
                    $.each(json, function (key, value) {
                        var PageURL = value.Properties.find(x => x.Name == "PageUrl").Value.replace("dotaspx", ".aspx");
                        if (value.Name.toLowerCase() == "flight") {
                            $("#aTravelLink").attr("href", PageURL);
                            $("#imgTravel").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "hotel") {
                            $("#aHotelLink").attr("href", PageURL);
                            $("#imgHotel").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "lounges") {
                            $("#aLoungesLink").attr("href", PageURL);
                            $("#imgLounges").attr("src", value.PrimaryImage.Url);
                        } else if (value.Name.toLowerCase() == "shop") {
                            $("#aShopLink").attr("href", PageURL);
                            $("#imgShop").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "gift cards") {
                            $("#aVouchersLink").attr("href", PageURL);
                            $("#imgVouchers").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "utility") {
                            $("#aUtilityLink").attr("href", PageURL);
                            $("#imgUtility").attr("src", value.PrimaryImage.Url);
                        } else if (value.Name.toLowerCase() == "experiences") {
                            $("#aExperiencesLink").attr("href", PageURL);
                            $("#imgExperiences").attr("src", value.PrimaryImage.Url);
                        } else if (value.Name.toLowerCase() == "insurance") {
                            $("#aInsuranceLink").attr("href", PageURL);
                            $("#imgInsurance").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "internet service providers") {
                            $("#aISPLink").attr("href", PageURL);
                            $("#imgISP").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "miles exchange") {
                            $("#aMilesLink").attr("href", PageURL);
                            $("#imgMiles").attr("src", value.PrimaryImage.Url);
                        }
                        else if (value.Name.toLowerCase() == "offers") {
                            $("#aOfferLink").attr("href", PageURL);
                            $("#imgOffer").attr("src", value.PrimaryImage.Url);
                        }
                    });
                }
                else {
                    $.ajax({
                        type: 'POST',
                        url: 'Index.aspx/GetHomeRedemptionOptions',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: "",
                        success: function (msg) {
                            if (msg.d != '') {
                                var json = $.parseJSON(msg.d);
                                $.each(json, function (key, value) {
                                    var PageURL = value.Properties.find(x => x.Name == "PageUrl").Value.replace("dotaspx", ".aspx");
                                    if (value.Name.toLowerCase() == "flight") {
                                        $("#aTravelLink").attr("href", PageURL);
                                        $("#imgTravel").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "hotel") {
                                        $("#aHotelLink").attr("href", PageURL);
                                        $("#imgHotel").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "lounges") {
                                        $("#aLoungesLink").attr("href", PageURL);
                                        $("#imgLounges").attr("src", value.PrimaryImage.Url);
                                    } else if (value.Name.toLowerCase() == "shop") {
                                        $("#aShopLink").attr("href", PageURL);
                                        $("#imgShop").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "gift cards") {
                                        $("#aVouchersLink").attr("href", PageURL);
                                        $("#imgVouchers").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "utility") {
                                        $("#aUtilityLink").attr("href", PageURL);
                                        $("#imgUtility").attr("src", value.PrimaryImage.Url);
                                    } else if (value.Name.toLowerCase() == "experiences") {
                                        $("#aExperiencesLink").attr("href", PageURL);
                                        $("#imgExperiences").attr("src", value.PrimaryImage.Url);
                                    } else if (value.Name.toLowerCase() == "insurance") {
                                        $("#aInsuranceLink").attr("href", PageURL);
                                        $("#imgInsurance").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "internet service providers") {
                                        $("#aISPLink").attr("href", PageURL);
                                        $("#imgISP").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "miles exchange") {
                                        $("#aMilesLink").attr("href", PageURL);
                                        $("#imgMiles").attr("src", value.PrimaryImage.Url);
                                    }
                                    else if (value.Name.toLowerCase() == "offers") {
                                        $("#aOfferLink").attr("href", PageURL);
                                        $("#imgOffer").attr("src", value.PrimaryImage.Url);
                                    }
                                });
                            }
                        }
                    })
                    return false;
                }
            } catch (e) {
            }
        }
    </script>

</asp:Content>

