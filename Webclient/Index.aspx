<%@ Page Title="Index" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <div class="featured-services mb-5">
        <div class="container-lg">
            <div class="row">
                <div class="dvTurnText col-12 col-lg-10 offset-lg-1">
                    <h2 class="h1 heading-light pt-0 text-center" data-i18n="home-turn">Turn Transactions into Rewards</h2>
                    <span class="h3 heading-light text-center d-block py-3" data-i18n="home-our-diverse">Our Diverse Loyalty Program</span>
                    <p class="text-center " data-i18n="home-discover">
                        Discover the extraordinary benefits of our Bank Loyalty Program. Designed for the discerning customer, it offers diverse redemption options like flight bookings, hotel stays, car rentals, vouchers, online shopping, and point exchanges.
                    Experience the joy of rewards that fit your lifestyle, turning every transaction into a chance for delightful experiences.
                    Join us for a journey of endless possibilities and rewards that cater to every desire.
                    </p>
                </div>
            </div>
        </div>
    </div>
    <div class="dvShopDeals py-5 d-none">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1 text-center mb-4">
                    <h2 class="h1 heading-light text-colour1 mb-3" data-i18n="home-shopthebest">Shop the Best Deals</h2>
                    <p data-i18n="home-maximize">
                        Maximize your NPoints with our handpicked selection of top deals in electronics, fashion, home essentials, and more.
                    </p>
                </div>
            </div>
            <div class="dvShopDealsSlider swiper row my-2" id="dvShopDealsSlider">
            </div>         
        </div>
    </div>
    <div class="dvTravelTheWorld py-5 mb-3">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1 text-center mb-4">
                    <h2 class="h1 heading-light text-colour1 mb-3" data-i18n="home-travel">Travel the world</h2>
                    <p data-i18n="home-unlock">Unlock a world of travel with your NPoints, accessing over 10,000+ deals on flights, hotels, and more.</p>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-md-6 col-lg-3">
                    <div class="shadow-sm mb-3">
                        <a id="hrefFlight" runat="server">
                            <div class="img-container">
                                <img src="images/homepage/travel-section/flight.jpg" alt="" />
                            </div>
                            <div class="d-flex flex-column px-3 py-3 dvCardName">
                                <h2 class="h6 heading-bold text-truncate text-white" data-i18n="home-flights">Flights
                                    <img src="images/icons/arrows/right-arrow.svg" alt=""></h2>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-md-6 col-lg-3">
                    <div class="shadow-sm mb-3">
                        <a id="hrefHotel"  runat="server">
                            <div class="img-container">
                                <img src="images/homepage/travel-section/hotel.jpg" alt="" />
                            </div>
                            <div class="d-flex flex-column px-3 py-3 dvCardName">
                                <h2 class="h6 heading-bold text-truncate text-white" data-i18n="home-hotels">Hotels
                                    <img src="images/icons/arrows/right-arrow.svg" alt=""></h2>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-md-6 col-lg-3">
                    <div class="shadow-sm mb-3">
                        <a id="hrefLounges" runat="server">
                            <div class="img-container">
                                <img src="images/homepage/travel-section/airport-lounges.jpg" alt="" />
                            </div>
                            <div class="d-flex flex-column px-3 py-3 dvCardName">
                                <h2 class="h6 heading-bold text-truncate text-white" data-i18n="home-airport-lounges">Airport Lounges
                                    <img src="images/icons/arrows/right-arrow.svg" alt=""></h2>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-md-6 col-lg-3">
                    <div class="shadow-sm mb-3">
                        <a id="hrefMiles" runat="server">
                            <div class="img-container">
                                <img src="images/homepage/travel-section/miles-exchange.jpg" alt="" />
                            </div>
                            <div class="d-flex flex-column px-3 py-3 dvCardName">
                                <h2 class="h6 heading-bold text-truncate text-white" data-i18n="home-miles-exchange">Miles Exchange
                                    <img src="images/icons/arrows/right-arrow.svg" alt=""></h2>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="dvVouchers py-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1 text-center mb-4">
                    <h2 class="h1 heading-light text-colour1 mb-3" data-i18n="home-most-featured">Most Featured Vouchers</h2>
                    <p data-i18n="home-choose-from">Choose from over 5,000+ gift vouchers in our expansive Loyalty Program selection.</p>
                </div>
            </div>
            <div class="dvVoucherSlider swiper row" id="dvVoucherSlider">               
            </div>
        </div>
    </div>
    <div class="dvFaqSection pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1 text-center mb-4">
                    <h2 class="h1 heading-light text-colour7 mb-1" data-i18n="home-faqs-helpful">FAQs: A Helpful Insights</h2>
                </div>
                <div class="col-12 col-lg-10 offset-lg-1">
                    <div class="bg-lightgray">
                        <div class="accordion" id="manage-accordion">
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn btn-block text-left p-3" type="button" data-toggle="collapse" data-target="#collapse1">
                                            <span class="h6 heading-semibold" data-i18n="home-Hotel">Hotel</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse1" class="collapse show" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0">
                                        <div>
                                            <div class="row mb-1">
                                                <div class="col-12">
                                                    <div class="bg-white p-3">
                                                        <div class="row align-items-lg-center justify-content-between">
                                                            <div class="col-12">
                                                                <div class="d-flex justify-content-start">
                                                                    <div class="dvCheckImage">
                                                                        <img src="images/homepage/faq-section/check.png" alt="" />
                                                                    </div>
                                                                    <div class="dvcheckContent ml-3">
                                                                        <p class="h6 heading-bold">Can I make a hotel booking for today’s check-in?</p>
                                                                        <p>Sorry this is not possible. Hotel reservations have to be booked a minimum of 3 days in advance.</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row dvBorderBottom">
                                                            <div class="col-12">
                                                                <div class="border-bottom my-3"></div>
                                                            </div>
                                                        </div>
                                                        <div class="row align-items-lg-center justify-content-between">
                                                            <div class="col-12">
                                                                <div class="d-flex justify-content-start">
                                                                    <div class="dvCheckImage">
                                                                        <img src="images/homepage/faq-section/check.png" alt="" />
                                                                    </div>
                                                                    <div class="dvcheckContent ml-3">
                                                                        <p class="h6 heading-bold">How can I access my recent hotel booking details?</p>
                                                                        <p>Check your confirmation email or log in to the 'My Bookings' section on our website. Need help? Contact customer service.</p>
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
                            </div>
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse" data-target="#collapse2">
                                            <span class="h6 heading-semibold" data-i18n="home-experiences">Experiences</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse2" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0">
                                        <div>
                                            <div class="row mb-1">
                                                <div class="col-12">
                                                    <div class="bg-white p-3">
                                                        <div class="row align-items-lg-center justify-content-between">
                                                            <div class="col-12">
                                                                <div class="d-flex justify-content-start">
                                                                    <div class="dvCheckImage">
                                                                        <img src="images/homepage/faq-section/check.png" alt="" />
                                                                    </div>
                                                                    <div class="dvcheckContent ml-3">
                                                                        <p class="h6 heading-bold">Can I make a hotel booking for today’s check-in?</p>
                                                                        <p>Sorry this is not possible. Hotel reservations have to be booked a minimum of 3 days in advance.</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row dvBorderBottom">
                                                            <div class="col-12">
                                                                <div class="border-bottom my-3"></div>
                                                            </div>
                                                        </div>
                                                        <div class="row align-items-lg-center justify-content-between">
                                                            <div class="col-12">
                                                                <div class="d-flex justify-content-start">
                                                                    <div class="dvCheckImage">
                                                                        <img src="images/homepage/faq-section/check.png" alt="" />
                                                                    </div>
                                                                    <div class="dvcheckContent ml-3">
                                                                        <p class="h6 heading-bold">How can I access my recent hotel booking details?</p>
                                                                        <p>Check your confirmation email or log in to the 'My Bookings' section on our website. Need help? Contact customer service.</p>
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
                            </div>
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse" data-target="#collapse3">
                                            <span class="h6 heading-semibold" data-i18n="home-vouchers">Vouchers</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse3" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body scroll-ver p-0">
                                        <div>
                                            <div class="row mb-1">
                                                <div class="col-12">
                                                    <div class="bg-white p-3">
                                                        <div class="row align-items-lg-center justify-content-between">
                                                            <div class="col-12">
                                                                <div class="d-flex justify-content-start">
                                                                    <div class="dvCheckImage">
                                                                        <img src="images/homepage/faq-section/check.png" alt="" />
                                                                    </div>
                                                                    <div class="dvcheckContent ml-3">
                                                                        <p class="h6 heading-bold">Can I make a hotel booking for today’s check-in?</p>
                                                                        <p>Sorry this is not possible. Hotel reservations have to be booked a minimum of 3 days in advance.</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row dvBorderBottom">
                                                            <div class="col-12">
                                                                <div class="border-bottom my-3"></div>
                                                            </div>
                                                        </div>
                                                        <div class="row align-items-lg-center justify-content-between">
                                                            <div class="col-12">
                                                                <div class="d-flex justify-content-start">
                                                                    <div class="dvCheckImage">
                                                                        <img src="images/homepage/faq-section/check.png" alt="" />
                                                                    </div>
                                                                    <div class="dvcheckContent ml-3">
                                                                        <p class="h6 heading-bold">How can I access my recent hotel booking details?</p>
                                                                        <p>Check your confirmation email or log in to the 'My Bookings' section on our website. Need help? Contact customer service.</p>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#dvHeroSlider").show();
            BindBanner();
            GetRedemptionOptions();
            //BindShopBestDeals();
            BindVouchers();
            GetTravelOptions();
        });
        <%--function BindShopBestDeals() {
            try {
                var shopdealsHTML = '<%=HttpContext.Current.Application["ShopBestDeals"]%>';
                if (shopdealsHTML != '') {
                    $("#dvShopDealsSlider").html(shopdealsHTML);
                    const shopDealsSlider = new Swiper(".dvShopDealsSlider.swiper", {
                        direction: "horizontal",
                        loop: false,
                        speed: 500,
                        slidesPerView: 1,
                        navigation: {
                            nextEl: ".swiper-button-next",
                            prevEl: ".swiper-button-prev",
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
                else {
                    $.ajax({
                        type: 'POST',
                        url: 'Index.aspx/GetShopBestDeals',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: "",
                        async: true,
                        success: function (msg) {
                            if (msg.d != '') {
                                $("#dvShopDealsSlider").html(msg.d);
                                const shopDealsSlider = new Swiper(".dvShopDealsSlider.swiper", {
                                    direction: "horizontal",
                                    loop: false,
                                    speed: 500,
                                    slidesPerView: 1,
                                    navigation: {
                                        nextEl: ".swiper-button-next",
                                        prevEl: ".swiper-button-prev",
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
                            return false;
                        }
                    })

                }


            } catch (e) {
            }
        }--%>
        function BindVouchers() {
            try {
                var vouchersHTML = '<%=HttpContext.Current.Application["Vouchers"]%>';
                if (vouchersHTML != '') {
                    $("#dvVoucherSlider").html(vouchersHTML);
                    const voucherSlider = new Swiper(".dvVoucherSlider.swiper", {
                        direction: "horizontal",
                        loop: false,
                        speed: 500,
                        slidesPerView: 1,
                        navigation: {
                            nextEl: ".swiper-button-next",
                            prevEl: ".swiper-button-prev",
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
                else {
                    $.ajax({
                        type: 'POST',
                        url: 'Index.aspx/GetVouchers',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: "",
                        async: true,
                        success: function (msg) {
                            if (msg.d != '') {
                                $("#dvVoucherSlider").show();
                                $("#dvVoucherSlider").html(msg.d);
                                const voucherSlider = new Swiper(".dvVoucherSlider.swiper", {
                                    direction: "horizontal",
                                    loop: false,
                                    speed: 500,
                                    slidesPerView: 1,
                                    navigation: {
                                        nextEl: ".swiper-button-next",
                                        prevEl: ".swiper-button-prev",
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
                            else {
                                $("#dvVoucherSlider").hide();
                            }
                            return false;
                        }
                    })

                }
            } catch (e) {
            }

        }
        function GetTravelOptions() {
            try {

                let travelOptions = '<%=HttpContext.Current.Application["GetTravelOptions"]%>';
                if (travelOptions != '') {
                    var json = $.parseJSON(travelOptions);
                    $.each(json, function (key, value) {
                        var PageURL = value.Properties.find(x => x.Name == "PageUrl").Value.replace("dotaspx", ".aspx");
                        if (value.Name.toLowerCase() == "flight") {
                            $("#CP_hrefFlight").attr("href", PageURL);
                        }
                        else if (value.Name.toLowerCase() == "hotel") {
                            $("#CP_hrefHotel").attr("href", PageURL);
                        }
                        else if (value.Name.toLowerCase() == "lounges") {
                            $("#CP_hrefLounges").attr("href", PageURL);
                        }
                        else if (value.Name.toLowerCase() == "miles exchange") {
                            $("#CP_hrefMiles").attr("href", PageURL);
                        }
                    });
                }
                else {
                    $.ajax({
                        type: 'POST',
                        url: 'Index.aspx/GetTravelOptions',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: "",
                        success: function (msg) {
                            if (msg.d != '') {
                                var json = $.parseJSON(msg.d);
                                $.each(json, function (key, value) {
                                    var PageURL = value.Properties.find(x => x.Name == "PageUrl").Value.replace("dotaspx", ".aspx");
                                    if (value.Name.toLowerCase() == "flight") {
                                        $("#CP_hrefFlight").attr("href", PageURL);
                                    }
                                    else if (value.Name.toLowerCase() == "hotel") {
                                        $("#CP_hrefHotel").attr("href", PageURL);
                                    }
                                    else if (value.Name.toLowerCase() == "lounges") {
                                        $("#CP_hrefLounges").attr("href", PageURL);
                                    }
                                    else if (value.Name.toLowerCase() == "miles exchange") {
                                        $("#CP_hrefMiles").attr("href", PageURL);
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
