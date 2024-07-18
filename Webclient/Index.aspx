<%@ Page Title="Index" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/home.css" />
    <style>
        .dvInnerBanner{
            display:none;
        }        
    </style>
    <div class="dvTurnTransaction mb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1">
                    <h2 class="heading1 pt-0 text-center" data-i18n="home-turn">Turn Transactions into Rewards</h2>
                    <span class="h3 heading-regular text-center d-block py-3" data-i18n="home-our-diverse">Infinity Rewards - Our Diverse Loyalty Program</span>
                    <p class="text-center " data-i18n="home-discover">
                        Designed for the discerning customer, it offers diverse redemption options like flight bookings, hotel stays, car rentals, vouchers, online shopping, and point exchanges. Experience the joy of rewards that fit your lifestyle, turning every transaction into a chance for delightful experiences.
                    </p>
                </div>
            </div>
        </div>
    </div>

    <div class="dvRedemptionLinks bg-colour4 py-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 text-center mb-4">
                    <h2 class="h4 heading-semibold">Rewards that cater to every desire.</h2>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pr-md-0 d-md-flex">
                    <img class="img-fluid" src="/Images/homepage/rewards-that-cater-to-every-desire/flight-booking.jpg" alt="image not found" />
                </div>
                <div class="col-md-6 d-md-flex flex-md-column justify-content-md-center pl-md-0">
                    <div class="bg-colour3 p-3 p-md-4 h-100">
                        <div class="d-md-flex flex-md-column justify-content-md-center h-100">
                            <div>
                                <img class="my-3 my-md-2 my-lg-3 icon" src="/Images/icons/redemption-icons/flight.svg" alt="image not found" />
                            </div>
                            <h2 class="heading5 text-colour1 mb-3 mb-md-2 mb-lg-3">Flight Booking</h2>
                            <p>Redeem points for flights across a global network of airlines. Effortless booking process, broad selection of destinations, and exclusive deals for an unmatched air travel experience.</p>
                            <div>
                                <a href="/FlightSearch.aspx" class="btn btn-one text-uppercase my-3 my-md-2 my-lg-3">learn more</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pl-md-0 order-md-1 d-md-flex">
                    <img class="img-fluid" src="/Images/homepage/rewards-that-cater-to-every-desire/hotel-booking.jpg" alt="image not found" />
                </div>
                <div class="col-md-6 d-md-flex flex-md-column justify-content-md-center pr-md-0">
                    <div class="bg-colour3 p-3 p-md-4 h-100">
                        <div class="d-md-flex flex-md-column justify-content-md-center h-100">
                            <div>
                                <img class="my-3 my-md-2 my-lg-3 icon" src="/Images/icons/redemption-icons/hotels.svg" alt="image not found" />
                            </div>
                            <h2 class="heading5 text-colour1 mb-3 mb-md-2 mb-lg-3">Hotels</h2>
                            <p>Use your loyalty points to book stays at luxurious hotels worldwide. Experience unparalleled comfort, exceptional service, and convenience, making every trip memorable.</p>
                            <div>
                                <a href="/HotelSearch.aspx" class="btn btn-one text-uppercase my-3 my-md-2 my-lg-3">learn more</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pr-md-0 d-md-flex">
                    <img class="img-fluid" src="/Images/homepage/rewards-that-cater-to-every-desire/airport-lounge-booking.jpg" alt="image not found" />
                </div>
                <div class="col-md-6 d-md-flex flex-md-column justify-content-md-center pl-md-0">
                    <div class="bg-colour3 p-3 p-md-4 h-100">
                        <div class="d-md-flex flex-md-column justify-content-md-center h-100">
                            <div>
                                <img class="my-3 my-md-2 my-lg-3 icon" src="/Images/icons/redemption-icons/airport-lounge.svg" alt="image not found" />
                            </div>
                            <h2 class="heading5 text-colour1 mb-3 mb-md-2 mb-lg-3">Airport Lounge</h2>
                            <p>Exchange points for access to exclusive airport lounges. Enjoy peace, comfort, and luxury amenities, making your wait time a pleasant part of the journey.</p>
                            <div>
                                <a href="#" class="btn btn-one text-uppercase my-3 my-md-2 my-lg-3">learn more</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pl-md-0 order-md-1 d-md-flex">
                    <img class="img-fluid" src="/Images/homepage/rewards-that-cater-to-every-desire/miles-exchange-booking.jpg" alt="image not found" />
                </div>
                <div class="col-md-6 d-md-flex flex-md-column justify-content-md-center pr-md-0">
                    <div class="bg-colour3 p-3 p-md-4 h-100">
                        <div class="d-md-flex flex-md-column justify-content-md-center h-100">
                            <div>
                                <img class="my-3 my-md-2 my-lg-3 icon" src="/Images/icons/redemption-icons/miles-exchange.svg" alt="image not found" />
                            </div>
                            <h2 class="heading5 text-colour1 mb-3 mb-md-2 mb-lg-3">Miles Exchange</h2>
                            <p>Exchange your points for airline miles. Elevate your travel experience with upgrades, flights, and more.</p>
                            <div>
                                <a href="/ShopList.aspx?CategoryId=fcbfa209-3820-44d6-9c14-b7e232df584e&type=Milesexchange" class="btn btn-one text-uppercase my-3 my-md-2 my-lg-3">learn more</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pr-md-0 d-md-flex">
                    <img class="img-fluid" src="/Images/homepage/rewards-that-cater-to-every-desire/experiences-booking.jpg" alt="image not found" />
                </div>
                <div class="col-md-6 d-md-flex flex-md-column justify-content-md-center pl-md-0">
                    <div class="bg-colour3 p-3 p-md-4 h-100">
                        <div class="d-md-flex flex-md-column justify-content-md-center h-100">
                            <div>
                                <img class="my-3 my-md-2 my-lg-3 icon" src="/Images/icons/redemption-icons/experiences.svg" alt="image not found" />
                            </div>
                            <h2 class="heading5 text-colour1 mb-3 mb-md-2 mb-lg-3">Experiences</h2>
                            <p>Redeem for unique experiences, from thrilling adventure sports to serene cultural tours. Dive into new activities that enrich your travels and create lasting memories.</p>
                            <div>
                                <a href="#" class="btn btn-one text-uppercase my-3 my-md-2 my-lg-3">learn more</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 pl-md-0 order-md-1 d-md-flex">
                    <img class="img-fluid" src="/Images/homepage/rewards-that-cater-to-every-desire/shop-booking.jpg" alt="image not found" />
                </div>
                <div class="col-md-6 d-md-flex flex-md-column justify-content-md-center pr-md-0">
                    <div class="bg-colour3 p-3 p-md-4 h-100">
                        <div class="d-md-flex flex-md-column justify-content-md-center h-100">
                            <div>
                                <img class="my-3 my-md-2 my-lg-3 icon" src="/Images/icons/redemption-icons/shop.svg" alt="image not found" />
                            </div>
                            <h2 class="heading5 text-colour1 mb-3 mb-md-2 mb-lg-3">Shop</h2>
                            <p>Spend your points on a wide selection of products. From the latest electronics to trendy fashion, turn your loyalty into retail therapy.</p>
                            <div>
                                <a href="/Shop.aspx?CategoryId=9149a75f-1f53-4ed7-b9b3-260b0fd6d606&ProductType=Physical&type=Shop" class="btn btn-one text-uppercase my-3 my-md-2 my-lg-3">learn more</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="dvShopDeals py-5 d-none">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1 text-center mb-4">
                    <h2 class="heading1 text-colour1 mb-3" data-i18n="home-shopthebest">Shop the Best Deals</h2>
                    <p data-i18n="home-maximize">
                        Maximize your Points with our handpicked selection of top deals in electronics, fashion, home essentials, and more.
                    </p>
                </div>
            </div>
            <div class="dvShopDealsSlider swiper row my-2" id="dvShopDealsSlider">
            </div>
        </div>
    </div>

    <div class="dvVouchers py-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-lg-10 offset-lg-1 text-center mb-4">
                    <h2 class="heading1 text-colour6 mb-3" data-i18n="home-most-featured">Most Featured Vouchers</h2>
                    <p class="text-colour6" data-i18n="home-choose-from">Choose from over 5,000+ gift vouchers in our expansive Loyalty Program selection.</p>
                </div>
            </div>
            <div class="dvVoucherSlider " id="dvVoucherSlider">
            </div>
        </div>
    </div>

    <div class="dvRewards py-5 pb-lg-0">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 text-center mb-4">
                    <h2 class="h5 heading-regular text-uppercase mb-3" data-i18n="home-unlocking-rewards">Unlocking Rewards: Your Guide</h2>
                    <h2 class="heading2 mb-3" data-i18n="home-from-earning">From Earning to Redeeming: We've Got Answers</h2>
                    <p class="" data-i18n="home-navigating-our">
                        Navigating our rewards program is as fun as a rollercoaster ride.
Here are answers to some common questions to ensure your journey is smooth and rewarding.
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-12 bg-image">
                    <div class="row">
                        <div class="col-lg-6 offset-xl-1 col-xl-6 py-lg-5">
                            <div class="row mb-3">
                                <div class="col-2 col-sm-1">
                                    <i class="fa-solid fa-check bg-colour1 p-2 text-colour6"></i>
                                </div>
                                <div class="col-10 col-sm-11">
                                    <h2 class="h5 heading-semibold mb-2">How do I accumulate points?</h2>
                                    <p>Earn points with every transaction made through the bank. Start accumulating rewards with every interaction!</p>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-2 col-sm-1">
                                    <i class="fa-solid fa-check bg-colour1 p-2 text-colour6"></i>
                                </div>
                                <div class="col-10 col-sm-11">
                                    <h2 class="h5 heading-semibold mb-2">What can I exchange my points for?</h2>
                                    <p>Your points are your gateway to discounts. Redeem them for exclusive Gift vouchers, shopping, flight & hotel booking and mobile top-up.</p>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-2 col-sm-1">
                                    <i class="fa-solid fa-check bg-colour1 p-2 text-colour6"></i>
                                </div>
                                <div class="col-10 col-sm-11">
                                    <h2 class="h5 heading-semibold mb-2">How long are my points valid for?</h2>
                                    <p>Your points will remain active for 2 years from the date you earn them. Make sure to redeem them before they expire!</p>
                                </div>
                            </div>
                            <div class="row mb-3 mb-lg-0">
                                <div class="col-2 col-sm-1">
                                    <i class="fa-solid fa-check bg-colour1 p-2 text-colour6"></i>
                                </div>
                                <div class="col-10 col-sm-11">
                                    <h2 class="h5 heading-semibold mb-2">How can I check my points balance?</h2>
                                    <p>To keep a tab on your rewards, simply log in and navigate to the 'My Account' dashboard. Your points balance will be available there.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="img-container">
                                <img class="img-fluid" src="/Images/homepage/reward-section/rewards.jpg" alt="Image not found" />
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
                    const voucherSlider = new Swiper(".dvVoucherSlider .swiper", {
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
                                const voucherSlider = new Swiper(".dvVoucherSlider .swiper", {
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
