<%@ Page Title="Hotel Details" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="HotelDetails.aspx.cs" Inherits="HotelDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/hotel.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/HotelDetails.js" type="text/javascript"></script>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;}
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\"><img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="HotelResults.aspx"> Hotel Results</a></li>
                    <li class="breadcrumb-item">Hotel Details</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="dvHotelDetails pb-5 mt-lg-4">
        <div class="container-xl">
            <div class="row">
                <div class="dvThumbSwiperSlider col-md-6 mb-3">
                    <div class="border b-radius p-3">
                        <div class="swiper mySwiper2 mb-2">
                            <div id="BannerImage" class="swiper-wrapper">
                            </div>
                            <div class="swiper-button-next">
                                <img src="images/icons/arrows/right-yellow-arrow-2.svg" />
                            </div>
                            <div class="swiper-button-prev">
                                <img src="images/icons/arrows/left-yellow-arrow-2.svg" />
                            </div>
                        </div>
                        <div class="swiper mySwiper">
                            <div id="ThumbBannerImage" class="swiper-wrapper">
                            </div>
                            <div class="swiper-button-next">
                                <img src="images/icons/arrows/right-yellow-arrow-2.svg" />
                            </div>
                            <div class="swiper-button-prev">
                                <img src="images/icons/arrows/left-yellow-arrow-2.svg" />
                            </div>
                        </div>
                    </div>
                </div>


                <%--<div class="dvThumbSwiperSlider col-md-6 mb-3">
                    <div class="Hotelbanner" id="BannerImage">
                        <div id="divpre" class="btn btn-one">
                            Prev
                        </div>
                        <div id="divNext" class="btn btn-one">
                            Next
                        </div>
                    </div>
                </div>--%>
                <div class="dvHotelInfo dvVcData col-md-6 mb-3">
                    <div class="row mb-3">
                        <div class="col-12">
                            <h2 id="HotelName" class="heading2 text-colour1"></h2>
                            <p>
                                <span id="HotelAddress"></span>
                                <span id="HotelCity"></span>
                            </p>
                            <p id="divrating"></p>
                        </div>
                    </div>
                    <div class="row mb-3" id="contact1">
                        <%--<div class="col-6 d-flex align-items-center">
                          <img src="images/icons/other/ico-phone.png" alt="" />
                          <span class="ml-2">91-11-27052700</span>
                        </div>
                        <div class="col-6 d-flex align-items-center">
                          <img src="images/icons/other/ico-msg.png" alt="" />
                          <span class="ml-2">Not Available</span>
                        </div>--%>
                    </div>
                    <div class="row mb-3" id="contact2">
                        <%--<div class="col-6 d-flex align-items-center">
                            <img src="images/icons/other/ico-phone.png" alt="" />
                            <span class="ml-2">91-11-27052700</span>
                        </div>
                        <div class="col-6 d-flex align-items-center">
                            <img src="images/icons/other/ico-weblink.png" alt="" />
                            <span class="ml-2">Not Available</span>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-12" id="BasicAmenities">
                            <%--<button
                            data-toggle="modal"
                            data-target="#dvAmenitiesModal"
                            class="btn btn-one mb-3"
                          >
                            View Amenities
                          </button>--%>
                            <%--<button class="btn btn-one">View More</button>--%>
                            <div class="dvMinRate row">
                                <div class="col-12">
                                    <p id="minrate">
                                        <%--<span class="heading-md-medium">65,520 Points</span>
                                        <span class="heading-regular">(for 1 night(s))</span>--%>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <button type="button"
                                data-toggle="modal"
                                data-target="#dvAmenitiesModal"
                                class="btn btn-two mb-3">
                                View Amenities
                            </button>
                        </div>
                    </div>


                </div>

                <div class="dvSelectRoom col-12">
                    <h2 class="h4 heading-semibold text-colour7 bg-colour2 p-3">Select Your Room</h2>
                    <div id="rptRoomDetails" class="dvVcData row mt-3"></div>
                </div>

                <div id="OverView" class="dvAboutHotel dvVcData col-12 mt-3"></div>

            </div>

            <div class="row">
                <div class="col-12 d-none">
                    <div class="map" id="divGoogleMaps">
                        <img id="imgMap" alt="Map" class="w-100" />
                    </div>
                </div>
            </div>


            <div class="row pt-5 dvSimilarListing">
                <div class="col-12 mb-3 text-center">
                    <h2 class="heading1">Similar Listing</h2>
                </div>
                <div id="NextHotelList" class="dvVcData col-12">
                </div>
            </div>
        </div>
    </div>

    <div class="dvCommonModal modal fade" id="dvAmenitiesModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="dvVcData modal-header border-0">
                    <h5 class="modal-title" id="DivAmenitiesCategoryName"></h5>
                     <button type="button" class="close" data-dismiss="modal">
                         <i class="fa-solid fa-xmark"></i>
                     </button>
                </div>
                <div class="modal-body pt-0">
                    <div id="DivAmenities" class="dvVcData row"></div>
                </div>
                <!-- <div class="modal-footer">
              <button type="button" class="btn btn-two" data-dismiss="modal">Close</button>
              <button type="button" class="btn btn-one">Save changes</button>
            </div> -->
            </div>
        </div>
    </div>

    <script>
        var swiper = new Swiper(".mySwiper", {
            spaceBetween: 10,
            slidesPerView: 4,
            freeMode: true,
            watchSlidesProgress: true,
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
        });
        var swiper2 = new Swiper(".mySwiper2", {
            spaceBetween: 10,
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
            thumbs: {
                swiper: swiper,
            },
        });
    </script>
</asp:Content>
