<%@ Page Title="Cart" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/shop.css">
    <input type="hidden" id="hdnstrCategoryID" value="<%= pstrCategoryID %>" />
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner, .breadcrumbBox {
            display: none;
        }
        .hide-checkout-button {
            display: none
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="hoteldetails.html">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active"><a href="Shop.aspx?CategoryId=9149a75f-1f53-4ed7-b9b3-260b0fd6d606&ProductType=Physical&type=Shop&Locale=en"> Shop</a></li>
                    <li class="breadcrumb-item active">Cart</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvCartDetails pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12">
                    <div class="bg-colour2 p-3 mt-4 mt-sm-0">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour6 p-3">
                                    <div id="divCartContents" runat="server">
                                    </div>
                                    <div class="row dvBorderBottom mt-3 mb-2">
                                        <div class="col-12 border-bottom">
                                        </div>
                                    </div>
                                    <div class="row align-items-lg-center justify-content-end">
                                        <div class="col-12 col-md-6 mt-2 col-lg-auto">
                                            <a class="btn btn-one w-100" href="Shop.aspx" id="btnContinueShopping" data-i18n="shopcart-continue">Continue Shopping</a>
                                        </div>
                                        <div class="col-12 col-md-6 mt-2 col-lg-auto">
                                            <a class="btn btn-two w-100" id="btnCheckout" runat="server" href="Checkout.aspx" data-i18n="shopcart-checkout">Checkout</a>
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
    <div class="container mt-3" dir="ltr" style="direction: ltr;">
        <div class="row">
            <div class="col-12 ">
                <div class="bg-light" style="margin-bottom: 50px;">

                    <div id="dvErrorMsg" runat="server" visible="false">
                        Insufficient Balance
                    </div>
                    <div id="divEmailErrorMsg" runat="server">
                        <h2 class="newRate">You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.</h2>
                    </div>
                    <div class="col mb-2">
                        <div class="cartBtnBlk">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById("btnContinueShopping").href = $('#hdnstrCategoryID').val();
            $('#aBreadcrumbUrl').attr('href', $('#hdnstrCategoryID').val());
        });
        function RemoveLineItem(lstrProductId) {
            $.ajax({
                type: "POST",
                url: "Cart.aspx/RemoveLineItem",
                data: "{lstrProductId:'" + lstrProductId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d != "") {
                        if (msg.d == "<div class='heading-regular h6 text-colour7' data-i18n=\'shopcart-cartempty\'>Cart Empty</div>") {
                            $("#CP_btnCheckout").addClass("not-active");
                            $("#CP_btnCheckout").addClass("hide-checkout-button");
                        } else {
                            $("#CP_btnCheckout").removeClass("not-active");
                        }
                        $('#CP_divCartContents').empty().html(msg.d);
                        CheckAvailability();
                    }
                    return false;
                }
            });
            return false;
        }
        function UpdateLineItemQty(lstrProductId, lintMinQty, lintMaxQty, lstrProductType, lintQty) {
            if (lstrProductType != "Digital" && parseInt(lintQty) < parseInt(lintMinQty)) {
                alert("You have to purchase minimum " + lintMinQty + " items of this product.");
                document.getElementById(lstrProductId).value = lintMaxQty;
            }
            else if (lstrProductType != "Digital" && parseInt(lintQty) > parseInt(lintMaxQty)) {
                alert("You can only add maximum " + lintMaxQty + " items of this product.");
                document.getElementById(lstrProductId).value = lintMaxQty;
            }
            else if (!/^[0-9]+$/.test(lintQty)) {
                alert("Accept only numeric value.");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "Cart.aspx/UpdateLineItemQty",
                    data: "{lstrProductId:'" + lstrProductId + "','lintQty':'" + lintQty + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg.d != "") {
                            if (msg.d == "<div class='emptyCart'>Cart Empty</div>") {
                                $("#CP_btnCheckout").addClass("not-active");
                            } else {
                                $("#CP_btnCheckout").removeClass("not-active");
                            }
                            $('#CP_divCartContents').html("");
                            $('#CP_divCartContents').html(msg.d);
                            CheckAvailability();
                        }
                        return false;
                    }
                });
            }
            return false;
        }
        function CheckAvailability() {
            $.ajax({
                type: "POST",
                url: "Cart.aspx/CheckAvailability",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        $("#CP_btnCheckout").removeAttr("style");
                        $("#CP_dvErrorMsg").hide();
                    }
                    else {
                        $("#CP_btnCheckout").attr("style", "pointer-events:none");
                        $("#CP_dvErrorMsg").show();
                    }
                    return false;
                }
            });
            return false;
        }
        $('form input').keydown(function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                return false;
            }
        });
    </script>
</asp:Content>
