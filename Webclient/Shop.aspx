<%@ Page Title="Shop" Language="C#" MasterPageFile="SiteShopMaster.master" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPSHOP" runat="Server">

    <style>
        .dvInnerBanner{
            display:none;
        }
    </style>
 
    <div id="shopoffers" class="dvShopPage"></div> 
    <script type="text/javascript">
        $(document).ready(function () {
            BindShoppageBanner()
            BindOffers();
            GetRedemptionOptions();
           // $("#dvInnerBanner").attr("src", "images/shoppage/shop-banner.jpg");
        });
        function BindOffers() {
            $.ajax({
                type: 'POST',
                url: 'Shop.aspx/GetOffers',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d != '') {
                        $("#shopoffers").append(msg.d);
                        new Swiper("#divshopswiper", {
                            direction: "horizontal",
                            loop: false,
                            speed: 500,
                            slidesPerView: 1,
                            navigation: {
                                nextEl: "#shop_swiper-button-next",
                                prevEl: "#shop_swiper-button-prev",
                            },
                            spaceBetween: 30,
                            breakpoints: {
                                0: {
                                    slidesPerView: 1,
                                },
                                576: {
                                    slidesPerView: 2,
                                },
                                768: {
                                    slidesPerView: 3,
                                },
                                992: {
                                    slidesPerView: 4,
                                },
                                1200: {
                                    slidesPerView: 4,
                                },
                            },
                            mousewheel: {
                                enabled: false,
                            },
                            keyboard: {
                                enabled: true,
                            },
                        });
                        
                    }
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            })
            return false;
        }
    </script>
</asp:Content>
