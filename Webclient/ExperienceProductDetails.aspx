<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductDetails.aspx.cs" Inherits="ExperienceProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />

    <div class="dvProductDetails pb-2" id="dvProductDetails">
        <div class="container-fluid">
            <div class="row">
                <div class="dvHighlightsLeft col-lg-4" id="divExpProductName">
                </div>

                <div class="dvProdcutTypeRight col-lg-8 dvCardBox">
                    <div class="scroll-ver- pl-xl-5- pr-xl-5- pl-lg-1- pr-lg-2-">
                        <div class="dvBreadcrumbs mt-3 mb-3 d-none d-md-block">
                            <div class="container-lg">
                                <nav>
                                    <ul class="breadcrumb px-0 py-3">
                                        <li class="mr-3">
                                            <a href="hoteldetails.html">
                                                <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                                        </li>
                                        <li class="breadcrumb-item"><a href="Index.aspx">Home</a></li>
                                        <li class="breadcrumb-item"><a href="ExperienceProductList.aspx">Experiences</a></li>
                                        <li class="breadcrumb-item active">Product Detail</li>
                                    </ul>
                                </nav>
                            </div>
                        </div>

                        <div class="dvHeadBox pt-3">
                            <h2 class="h4 heading-medium mb-3">Product types</h2>
                        </div>
                        <div class="dvFormBox">
                            <div class="border leftCont bg-white p-3">
                                <div class="row" id="expinputdiv">
                                    <div class="col-12 col-md-4 mb-2 mb-md-0">
                                        <label class="label">Date</label>
                                        <div class="dvtxtBookingDate dvInputGroup input-group">
                                            <input
                                                id="txtBookingDate"
                                                value="Enter Date"
                                                autocomplete="off"
                                                readonly="readonly"
                                                type="text"
                                                class="form-control" />
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6 col-md-4" id="divAdult"></div>
                                    <div class="col-6 col-md-4" id="divSenior"></div>
                                    <div class="col-6 col-md-4" id="divChild"></div>
                                </div>
                            </div>
                        </div>
                        <div class="dvTourBox mt-3">
                            <div class="border shadow-on-hover leftCont bg-white" id="dvProductTypeDetails">
                            </div>
                        </div>
                    </div>
                </div>

                <div id="Errordiv"></div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {           
            BindBanner();
            GetRedemptionOptions();
        });
        // $(document).ready(function () {
        $(".dvtxtBookingDate .input-group-append .input-group-text").on("click", function () {
            $("#txtBookingDate").datepicker("show");
        });
        var uuid = getQuerystring("uuid");
        if (uuid != null && uuid != "") {
            GetProductInfo(uuid);
        }
        else {
            window.location = "ExperienceProductList.aspx";
        }
        //});

        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

        function GetProductInfo(uuid) {
            $.ajax({
                type: 'POST',
                url: 'ExperienceProductDetails.aspx/GetProductInfo',
                contentType: 'application/json;',
                dataType: 'json',
                data: "{uuid:'" + uuid + "'}",
                cache: false,
                async: false,
                success: function (rtnData) {

                    if (rtnData.d != "" && rtnData.d != null) {
                        if (rtnData.d == "ErrorPage.aspx") {
                            window.location.href = "ErrorPage.aspx";
                        }
                        else {
                            fnBindExperienceProductInfo(rtnData.d);
                        }

                    }
                    $("#divExperienceLoader").hide();
                    $('#updProgress').hide();
                },
                error: function (errmsg) {

                },
                beforeSend: function () {
                    fnShowLoader('dvProductDetails');

                }
            });
        }

        function fnBindExperienceProductInfo(data) {
            var html = '';
            if (data != '') {
                var parseData = JSON.parse(data);
                if (parseData.data != null) {
                    $("#Errordiv").show();
                    html += '<div class="row pt-4">';
                    html += '<div class="scroll-ver col-12">';
                    html += '<div class="row">';
                    html += '<div class="col-12">';
                    html += '<p class="heading-regular">' + parseData.data.locations[0].city + ',' + parseData.data.locations[0].country + '</p>';
                    html += '<p class="heading-semibold">' + parseData.data.title + '</p>';
                    html += '</div>';
                    html += '</div>';
                    html += '<div class="row mt-3">';
                    html += '<div class="dvThumbSwiperSlider col-lg-12 col-xl-12">';
                    html += '<div class="bg-colour3 p-3">';
                    html += '<div style="--swiper-navigation-color: #fff; --swiper-pagination-color: #fff" class="swiper dvThumbBannerSlide">';
                    html += '<div class="swiper-wrapper">';
                    for (var i = 0; i < parseData.data.photos.length; i++) {
                        html += '<div class="swiper-slide img-container">';
                        html += '<img src="' + parseData.data.photos[i].paths.original + '"/>';
                        html += '</div>';
                    }
                    html += '</div>';
                    html += '<div class="swiper-button-next">';
                    html += '<img src="images/carpage/icons/right-arrow-violet.svg" />';
                    html += '</div>';
                    html += '<div class="swiper-button-prev">';
                    html += '<img src="images/carpage/icons/left-arrow-violet.svg" />';
                    html += '</div>';
                    html += '</div>';
                    html += '<div class="my-2"></div>';
                    html += '<div class="swiper dvThumbSlide">';
                    html += '<div class="swiper-wrapper">';

                    for (var i = 0; i < parseData.data.photos.length; i++) {
                        html += '<div class="swiper-slide img-container">';
                        html += '<img src="' + parseData.data.photos[i].paths.original + '"/>';
                        html += '</div>';
                    }
                    html += '</div>';
                    html += '<div class="swiper-button-next">';
                    html += '<img src="images/carpage/icons/right-arrow-violet.svg" />';
                    html += '</div>';
                    html += '<div class="swiper-button-prev">';
                    html += '<img src="images/carpage/icons/left-arrow-violet.svg" />';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '<div class="row mt-3">';
                    html += '<div class="dvTextIcon col-12">';
                    if (parseData.data.producttypedetails != null && parseData.data.producttypedetails.item_uuid != null && parseData.data.producttypedetails.item_uuid[0].typeinfo != null) {
                        if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes == 0) {
                            html += '<p class="heading-light"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays + ' day</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes == 0) {
                            html += '<p class="heading-light"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours + ' hr</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes > 0) {
                            html += '<p class="heading-light"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes + ' min</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes > 0) {
                            html += '<p class="heading-light"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours + ' hr ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes + ' min</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes > 0) {
                            html += '<p class="heading-light"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays + ' day ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours + ' hr ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes + ' min</p>';
                        }
                    }
                    if (parseData.data != null && parseData.data.tourType != null && parseData.data.tourType != "") {
                        switch (parseData.data.tourType.toLowerCase()) {
                            case "join":
                                html += '<p class="heading-light pt-1"><i class="fa-solid fa-share-nodes"></i> Shared</p>';
                                break;
                            case "private":
                                html += '<p class="heading-light pt-1"><i class="fa-solid fa-user"></i> Private Tour</p>';
                                break;
                        }
                    }
                    html += '<p class="heading-light pt-1">';
                    html += '<i class="fa-solid fa-people-group"></i> Group Size: min: ' + parseData.data.minPax + ' pax | Max: ' + parseData.data.maxPax + ' pax';
                    html += '</p>';
                    if (parseData.data != null && parseData.data.businessHoursFrom != null && parseData.data.businessHoursFrom != '' && parseData.data.businessHoursTo != null && parseData.data.businessHoursTo != '') {
                        html += '<p class="heading-light pt-1">Opening hours: ' + parseData.data.businessHoursFrom + 'am —' + parseData.data.businessHoursTo + 'pm</p>';
                    }
                    html += '</div>';
                    html += '</div>';

                    if (parseData.data != null && parseData.data.highlights != null && parseData.data.highlights != '') {
                        html += '<div class="border-bottom my-4"></div>';
                        html += '<div class="row">';
                        html += '<div class="dvHighligts col-12">';
                        html += '<p class="heading-semibold">Highlights</p>';
                        html += '<ul class="mt-2">';
                        $.each(parseData.data.highlights.split("\n"), function (i) {
                            html += '<li class="heading-light">' + parseData.data.highlights.split("\n")[i] + '</li>';
                        });
                        html += '</ul>';
                        html += '</div>';
                        html += '</div>';
                    }

                    html += '<div class="border-bottom my-4"></div>';
                    html += '<div class="row">';
                    html += '<div class="dvHighligts col-12">';
                    html += '<p class="heading-semibold">Additional Info</p>';
                    if (parseData.data != null && parseData.data.priceIncludes != null && parseData.data.priceIncludes != '') {
                        html += '<p class="heading-regular pt-2">Price Includes</p>';
                        html += '<ul>';
                        $.each(parseData.data.priceIncludes.split("\n"), function (i) {
                            html += '<li class="heading-light">' + parseData.data.priceIncludes.split("\n")[i] + '</li>';
                        });
                        html += '</ul>';
                    }
                    if (parseData.data != null && parseData.data.priceExcludes != null && parseData.data.priceExcludes != '') {
                        html += '<p class="heading-regular pt-2">Price Excludes</p>';
                        html += '<ul>';
                        $.each(parseData.data.priceExcludes.split("\n"), function (i) {
                            html += '<li class="heading-light">' + parseData.data.priceExcludes.split("\n")[i] + '</li>';
                        });
                        html += '</ul>';
                    }

                    html += '<div class="row">';
                    html += '<div class="dvReadMore col-12">';
                    html += '<ul>';
                    if (parseData.data != null && parseData.data.additionalInfo != null && parseData.data.additionalInfo != '') {
                        $.each(parseData.data.additionalInfo.split("\n"), function (i) {
                            if (parseData.data.additionalInfo.split("\n")[i] != null && parseData.data.additionalInfo.split("\n")[i] != '') {
                                html += '<p class="heading-light pt-2">' + parseData.data.additionalInfo.split("\n")[i] + '</p>';
                            }
                        });
                    }
                    if (parseData.data != null && parseData.data.warnings != null && parseData.data.warnings != '') {
                        html += '<p class="font-weight-bold pt-2">Warnings of the activity</p>';
                        $.each(parseData.data.warnings.split("\r\n"), function (i) {
                            if (parseData.data.warnings.split("\r\n")[i]) {
                                html += '<p class="heading-regular pt-2">' + parseData.data.warnings.split("\r\n")[i] + '</p>';
                            }
                        });
                    }
                    if (parseData.data != null && parseData.data.guideLanguages.length > 0) {
                        html += '<li class="heading-light pt-2">Additional audio guide language:' + Array.prototype.map.call(parseData.data.guideLanguages, function (item) { return toTitleCase(item.name); }).join(",") + '</li>';
                    }
                    if (parseData.data != null && parseData.data.audioHeadsetLanguages.length > 0) {
                        html += '<li class="heading-light pt-2">Languages for Audio Headset material:' + Array.prototype.map.call(parseData.data.audioHeadsetLanguages, function (item) { return toTitleCase(item.name); }).join(",") + '</li>';
                    }
                    html += '</ul>';
                    html += '<div class="toggle_btn">';
                    html += '<span class="toggle_text">Show More</span>';
                    html += '<span class="arrow">';
                    html += '<i class="fa fa-angle-down" aria-hidden="true"></i>';
                    html += '</span>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    if (parseData.data != null && parseData.data.itinerary != null && parseData.data.itinerary != '') {
                        html += '<div class="border-bottom my-4"></div>';
                        html += '<div class="row">';
                        html += '<div class="dvHighligts col-12">';
                        html += '<p class="heading-semibold">Itinerary</p>';
                        html += '<ul class="mt-2">';
                        $.each(parseData.data.itinerary.split("\n"), function (i) {
                            html += '<li class="heading-light">' + parseData.data.itinerary.split("\n")[i] + '</li>';
                        });
                        html += '</ul>';
                        html += '</div>';
                        html += '</div>';
                    }
                    html += '<div class="border-bottom my-4"></div>';
                    html += '<div class="row">';
                    html += '<div class="dvHighligts dvDescription col-12">';

                    if (parseData.data != null && parseData.data.description != null && parseData.data.description != '') {
                        html += '<p class="heading-semibold">Description</p>';
                        html += '<div class="dvReadMore2">';
                        html += '<ul>';
                        $.each(parseData.data.description.split("\r\n"), function (i) {
                            html += '<li class="heading-light pt-2">' + parseData.data.description.split("\r\n")[i] + '</li>';
                        });
                        html += '</ul>';
                        html += '<div class="toggle_btn2">';
                        html += '<span class="toggle_text2">Show More</span>';
                        html += '<span class="arrow">';
                        html += '<i class="fa fa-angle-down" aria-hidden="true"></i>';
                        html += '</span>';
                        html += '</div>';
                        html += '</div>';
                    }
                    //html += '<div class="img-container mt-3">';
                    //html += '<img alt="Product Image" src="images/carpage/map.png" />';
                    //html += '</div>';
                    if (parseData.data != null && parseData.data.title != null && parseData.data.title != '' && parseData.data.address != null && parseData.data.address != '') {
                        html += '<p class="heading-regular pt-2 pb-5">Address: <a target="_blank" href="http://maps.google.com/maps?q=' + parseData.data.title + ',' + parseData.data.address + '">' + parseData.data.title + ',' + parseData.data.address + '</a></p>';
                    }
                    html += '</div>';
                    html += '</div>';
                    $("#divExpProductName").append(html);

                    var swiper = new Swiper(".dvThumbSlide", {
                        spaceBetween: 10,
                        slidesPerView: 4,
                        freeMode: true,
                        watchSlidesProgress: true,
                        navigation: {
                            nextEl: ".swiper-button-next",
                            prevEl: ".swiper-button-prev",
                        },
                    });
                    var swiper2 = new Swiper(".dvThumbBannerSlide", {
                        spaceBetween: 10,
                        navigation: {
                            nextEl: ".swiper-button-next",
                            prevEl: ".swiper-button-prev",
                        },
                        thumbs: {
                            swiper: swiper,
                        },
                    });

                    $(".toggle_btn").click(function () {
                        $(this).toggleClass("active");
                        $(".dvReadMore ul").toggleClass("active");

                        if ($(".toggle_btn").hasClass("active")) {
                            $(".toggle_text").text("Show Less");
                        } else {
                            $(".toggle_text").text("Show More");
                        }
                    });
                    $(".toggle_btn2").click(function () {
                        $(this).toggleClass("active");
                        $(".dvReadMore2 ul").toggleClass("active");

                        if ($(".toggle_btn2").hasClass("active")) {
                            $(".toggle_text2").text("Show Less");
                        } else {
                            $(".toggle_text2").text("Show More");
                        }
                    });
                    var firstAvailabilityDate = new Date(parseData.producttypedetails.item_uuid[0].typeinfo.firstAvailabilityDate);
                    var lastAvailabilityDate = new Date();
                    lastAvailabilityDate.setFullYear(firstAvailabilityDate.getFullYear() + 2)
                    BindDatepicker(firstAvailabilityDate, lastAvailabilityDate);
                    html = "";
                    if (parseData.data != null && parseData.producttypedetails != null && parseData.producttypedetails.item_uuid != null && parseData.producttypedetails.item_uuid[0].typeinfo != null) {
                        let minAdult = parseData.producttypedetails.item_uuid[0].typeinfo.minPax;
                        let maxAdult = parseData.producttypedetails.item_uuid[0].typeinfo.maxPax;
                        /* $("#lblAdult").text("Adult(" + parseData.producttypedetails.item_uuid[0].typeinfo.minAdultAge + "-" + parseData.producttypedetails.item_uuid[0].typeinfo.maxAdultAge + " Yrs)");*/
                        html += '<label class="label" id="lblSenior">Adult( ' + parseData.producttypedetails.item_uuid[0].typeinfo.minAdultAge + '-' + parseData.producttypedetails.item_uuid[0].typeinfo.maxAdultAge + ' Yrs)</label>';
                        html += '<div class=" dvInputGroup input-group" id="sltAdult">';
                        html += '<select class="select selectBtn selectDropdown form-control" name="DrpDownAdult" id="selectDrpDownAdult" onchange="SelectDrpDownChangeFunction()">';
                        /*html += '<option  selected="selected" value="firstOption">Select</option>';*/
                        for (let i = (minAdult == null ? 0 : minAdult); i <= maxAdult; i++) {
                            html += '<option  value="' + i + '">' + i + '</option>';
                        }
                        html += '</select>'
                        html += '</div>';
                        $("#divAdult").append(html);
                    }
                    html = "";
                    if (parseData.producttypedetails.item_uuid[0].typeinfo.allowSeniors == true) {
                        let minSenior = parseData.producttypedetails.item_uuid[0].typeinfo.minSeniors;
                        let maxSenior = parseData.producttypedetails.item_uuid[0].typeinfo.maxSeniors;
                        html += '<label class="label" id="lblSenior">Senior( ' + parseData.producttypedetails.item_uuid[0].typeinfo.minSeniorAge + '-' + parseData.producttypedetails.item_uuid[0].typeinfo.maxSeniorAge + ' Yrs)</label>';
                        html += '<div class="dvInputGroup input-group" id="sltSenior">';
                        html += '<select class="select selectBtn selectDropdown form-control" name="DrpDownSenior" id="selectDrpDownSenior" onchange="SelectDrpDownChangeFunction()">';
                        /*html += '<option  selected="selected" value="firstOption">Select</option>';*/
                        for (let i = (minSenior == null ? 0 : minSenior); i <= maxSenior; i++) {
                            html += '<option  value="' + i + '">' + i + '</option>';
                        }
                        html += '</select>'
                        html += '</div>';
                        $("#divSenior").show();
                        $("#divSenior").append(html);
                    }
                    else {
                        $("#divSenior").hide();
                    }
                    html = "";
                    if (parseData.producttypedetails.item_uuid[0].typeinfo.allowChildren == true) {
                        let minChildren = parseData.producttypedetails.item_uuid[0].typeinfo.minChildren;
                        let maxChildren = parseData.producttypedetails.item_uuid[0].typeinfo.maxChildren;
                        html += '<label class="label" id="lblChildren">Child( ' + parseData.producttypedetails.item_uuid[0].typeinfo.minChildAge + '-' + parseData.producttypedetails.item_uuid[0].typeinfo.maxChildAge + ' Yrs)</label>';
                        html += '<div class="dvInputGroup input-group" id="sltChild">';
                        html += '<select class="select selectBtn selectDropdown form-control" name="DrpDownChildren" id="selectDrpDownChildren" onchange="SelectDrpDownChangeFunction()">';
                        /*html += '<option  selected="selected" value="firstOption">Select</option>';*/
                        for (let i = (minChildren == null ? 0 : minChildren); i <= maxChildren; i++) {
                            html += '<option  value="' + i + '">' + i + '</option>';
                        }
                        html += '</select>'
                        html += '</div>';
                        $("#divChild").show();
                        $("#divChild").append(html);
                    }
                    else {
                        $("#divChild").hide();
                    }
                    html = '';
                    $.each(parseData.producttypedetails.item_uuid, function (i) {
                        html += '<div class="border-bottom p-3">';
                        html += '<div class="d-flex justify-content-between">';
                        html += '<p class="heading-medium">' + parseData.producttypedetails.item_uuid[i].typeinfo.title + '</p>';
                        html += '</div>';
                        html += '<div class="voucherName pt-1">';
                        html += '<p class="C">' + parseData.producttypedetails.item_uuid[i].typeinfo.description + '</p>';
                        html += '</div>';
                        html += '</div>';

                        html += '<div class="pointsBox p-3">';
                        html += '<div class="d-inline-flex d-sm-flex justify-content-between flex-wrap">';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.available == true) {
                            html += '<p "class="ptypepricebydate heading-regular"><i class="fa-solid"></i> Valid only on <span>' + formatDate(parseData.producttypedetails.item_uuid[i].typePriceByDate.date) + '</span></p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isBmgVoucher == true) {
                            html += '<p class="heading-regular"><i class="fa-solid fa-mobile-screen-button"></i><span class="ml-2">Show on mobile</span></p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isNonRefundable == true) {
                            html += '<p class="heading-regular">';
                            html += '<i class="fa-solid fa-triangle-exclamation"></i><span class="ml-2">Non Refundable</span>';
                            html += '</p>';
                        }
                        else {
                            html += '<p class="heading-regular">';
                            html += '<i class="fa-solid fa-triangle-exclamation"></i><span class="ml-2">Refundable</span>';
                            html += '</p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.voucherRequiresPrinting == true) {
                            html += '<p class="heading-regular"><i class="fa-solid fa-bolt"></i><span class="ml-2">Print ticket</p></span>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.instantConfirmation == true) {
                            html += '<p class="heading-regular"><i class="fa-solid fa-bolt"></i><span class="ml-2">Instant</p></span>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.directAdmission == true) {
                            html += '<p class="heading-regular">';
                            html += '<i class="fa fa-address-book" aria-hidden="true"></i><span class="ml-2">Direct admission</span>';
                            html += '</p>';
                        }
                        html += '</div>';

                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate == null) {
                            html += '<div class="dvBtn mt-4 mb-2 text-right clsentertraveldate">';
                            html += '<button class="btn btn-one" onclick="var retvalue = ShowDatePicker(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button"';
                            html += 'value="Search"> Enter travel date </button>';
                            html += '</div>';
                        }
                        html += '</div>';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.rates != null) {
                            html += '<div class="border-top"></div>';
                            html += '<div class="d-flex flex-wrap dvtypepricingbox">';
                            html += '<div class="col-12 col-md-9 p-0 br-1 dvPricingBox">';
                            html += '</div>';
                            html += '<div class="dvRateBox col-12 col-md-3 p-0 mt-3 mt-md-0">';
                            html += '<div class="w-100 border">';
                            html += '<div class="dvBgcolor2">';
                            html += '<h2 class="h6 heading-bold p-3 bg-colour1 text-colour3">Price <span class="heading-xs">includes GST</span></h2>';
                            html += '</div>';
                            html += '<div class="dvBtnBg">';
                            let isTimeslotsAvailable = 0;
                            var showBookNow = false;
                            if (parseData.producttypedetails.item_uuid[i] != null) {
                                var ratesAarray = parseData.producttypedetails.item_uuid[i].typePriceByDate.rates.filter(obj => obj.type.toLowerCase() == "recommendedprice");
                                $.each(ratesAarray, function (n) {
                                    categoryName = toTitleCase(ratesAarray[n].category);
                                    let recommendedPrice = Math.ceil(ratesAarray[n].amount);
                                    var recommendedPriceFormat = ratesAarray[n].convertedCurrency + FormatCurrency(recommendedPrice);
                                    if (parseInt($('#selectDrpDownAdult').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "adult") {
                                        showBookNow = true;
                                        html += '<p class="heading-bold">' + $('#selectDrpDownAdult').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownSenior').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "senior" && showBookNow) {
                                        html += '<p class="heading-bold">' + $('#selectDrpDownSenior').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownChildren').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "child" && showBookNow) {
                                        html += '<p class="heading-bold">' + $('#selectDrpDownChildren').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                });
                                if (!showBookNow) {
                                    html += '<p class="heading-bold">Unavailable</p>';
                                }
                                if (parseData.producttypedetails.item_uuid[i] != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots.length > 0 && showBookNow) {
                                    isTimeslotsAvailable = 1;
                                    html += '<div>';
                                    html += '<select class="select selectBtn selectDropdown form-control" id="sltTimeSlot">';
                                    html += '<option value="">Select timeslot</option>';
                                    $.each(parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots, function (k) {
                                        html += '<option value="' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].uuid + '">' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].startTime.slice(0, -3) + ' - ' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].endTime.slice(0, -3) + '</option>';
                                    });
                                    html += '</select>';
                                    html += '</div>';
                                }
                            }
                            if (showBookNow) {
                                html += '<div id="dvBookNow" class="dvBtn mt-4 mb-2">';
                                html += '<button id="btnBookNow" class="btn btn-one w-100" onclick="var retvalue = BookNow(\'' + parseData.producttypedetails.item_uuid[i].uuid.toString() + '\',' + isTimeslotsAvailable + '); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button">Book Now</button>';
                                html += '</div>';
                            }
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                        }
                    });
                    $("#dvProductTypeDetails").append(html);
                }
                else {
                    $("#Errordiv").show();
                    html += '<div class="dvError d-flex justify-content-center align-items-center vh-center">';
                    html += '<div class="col text-center">';
                    html += '<div class="bg-colour4 p-5">';
                    html += '<i class="fa-solid fa-circle-exclamation"></i>';
                    html += '<h2 class="h2 heading-semibold text-colour1 mb-3">OOPS...!</h2>';
                    html += '<p class="alert alert-danger">We are facing some issue while fetching product details. Please try again after sometime.</p>';
                    html += '</div>';
                    html += '</div>';
                    html += ' </div>';
                    $("#Errordiv").append(html);
                }
            }
        }

        function toTitleCase(str) {
            return str.replace(
                /\w\S*/g,
                text => text.charAt(0).toUpperCase() + text.substring(1).toLowerCase()
            );
        }

        function BindDatepicker(firstAvailabilityDate, lastAvailabilityDate) {
            //datepicker bind
            $("#txtBookingDate").datepicker({
                changeMonth: true,
                changeYear: true,
                showAnim: "clip",
                dateFormat: "dd/mm/yy",
                // showButtonPanel: true,
                dateFormat: "dd-mm-yy",
                minDate: firstAvailabilityDate,
                maxDate: lastAvailabilityDate,
                onSelect: function (dateText) {
                    GetProductTypesPriceByDate();
                }
            });
        }
        function SelectDrpDownChangeFunction() {
            GetProductTypesPriceByDate();

        }
        function ShowDatePicker() {
            $('#txtBookingDate').datepicker('show');
        }
        function GetProductTypesPriceByDate() {
            var isSubmit = false;
            var adultCount = $('#selectDrpDownAdult').children("option:selected").val();
            var seniorsCount = $('#selectDrpDownSenior').children("option:selected").val();
            var childrenCount = $('#selectDrpDownChildren').children("option:selected").val();
            var bookingDate = $('#txtBookingDate').val();
            if (bookingDate == '' || bookingDate == 'Enter Date') {
                $('#txtBookingDate').datepicker('show');
            }
            if ((bookingDate != '' && bookingDate != 'Enter Date') && (parseInt(adultCount) > 0 || parseInt(seniorsCount) > 0 || parseInt(childrenCount) > 0)) {
                isSubmit = true;
            }
            if (isSubmit) {
                $('.ptypepricebydate').remove();
                $('.dvtypepricingbox').remove();
                $('.clsentertraveldate').remove();
                var uuid = getQuerystring("uuid");
                console.log(uuid);
                if (uuid != null && uuid != "" && uuid != undefined) {
                    GetProductPriceByDate(uuid);
                }

            }
        }
        function FormatCurrency(price) {
            price += '';
            x = price.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }
        function GetProductPriceByDate(uuid) {
            $.ajax({
                type: 'POST',
                url: 'ExperienceProductDetails.aspx/GetProductTypesPriceByDate',
                contentType: 'application/json;',
                dataType: 'json',
                data: "{date:'" + $('#txtBookingDate').val() + "',uuid:'" + uuid + "'}",
                cache: false,
                async: false,
                success: function (rtnData) {
                    if (rtnData.d != "" && rtnData.d != null) {
                        if (rtnData.d == "ErrorPage.aspx") {
                            window.location.href = "ErrorPage.aspx";
                        }
                        else {
                            fnBindProductTypePriceByDate(rtnData.d);
                        }
                    }
                },
                error: function (errmsg) {
                }
            });
        }
        function fnBindProductTypePriceByDate(data) {
            debugger
            var html = '';
            if (data != '') {
                var parseData = JSON.parse(data);
                if (parseData.data != null) {
                    html = '';
                    $.each(parseData.producttypedetails.item_uuid, function (i) {
                        html += '<div class="border-bottom p-3">';
                        html += '<div class="d-flex justify-content-between">';
                        html += '<p class="heading-medium">' + parseData.producttypedetails.item_uuid[i].typeinfo.title + '</p>';
                        html += '</div>';
                        html += '<div class="voucherName pt-1">';
                        html += '<p class="C">' + parseData.producttypedetails.item_uuid[i].typeinfo.description + '</p>';
                        html += '</div>';
                        html += '</div>';

                        html += '<div class="pointsBox p-3">';
                        html += '<div class="d-inline-flex d-sm-flex justify-content-between flex-wrap">';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.available == true) {
                            html += '<p "class="ptypepricebydate heading-regular"><i class="fa-solid"></i> Valid only on <span>' + formatDate(parseData.producttypedetails.item_uuid[i].typePriceByDate.date) + '</span></p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isBmgVoucher == true) {
                            html += '<p class="heading-regular"><i class="fa-solid fa-mobile-screen-button"></i>Show on mobile</p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isNonRefundable == true) {
                            html += '<p class="heading-regular">';
                            html += '<i class="fa-solid fa-triangle-exclamation"></i>Non Refundable';
                            html += '</p>';
                        }
                        else {
                            html += '<p class="heading-regular">';
                            html += '<i class="fa-solid fa-triangle-exclamation"></i>Refundable';
                            html += '</p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.voucherRequiresPrinting == true) {
                            html += '<p class="heading-regular"><i class="fa-solid fa-bolt"></i>Print ticket</p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.instantConfirmation == true) {
                            html += '<p class="heading-regular"><i class="fa-solid fa-bolt"></i>Instant</p>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.directAdmission == true) {
                            html += '<p class="heading-regular">';
                            html += '<i class="fa fa-address-book" aria-hidden="true"></i>Direct admission';
                            html += '</p>';
                        }
                        html += '</div>';

                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate == null) {
                            html += '<div class="dvBtn mt-4 mb-2 text-right clsentertraveldate">';
                            html += '<button class="btn btn-one" onclick="var retvalue = ShowDatePicker(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button"';
                            html += 'value="Search"> Enter travel date </button>';
                            html += '</div>';
                        }
                        html += '</div>';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.rates != null) {
                            html += '<div class="border-top"></div>';
                            html += '<div class="d-flex flex-wrap dvtypepricingbox">';
                            html += '<div class="col-12 col-md-9 p-0 br-1 dvPricingBox">';
                            html += '</div>';
                            html += '<div class="dvRateBox col-12 col-md-3 p-0 mt-3 mt-md-0">';
                            html += '<div class="w-100 border">';
                            html += '<div class="dvBgcolor2">';
                            html += '<h2 class="h6 heading-bold p-3 bg-colour1 text-colour3">Price <span class="heading-xs">includes GST</span></h2>';
                            html += '</div>';
                            html += '<div class="dvBtnBg">';
                            debugger
                            let isTimeslotsAvailable = 0;
                            var showBookNow = false;
                            if (parseData.producttypedetails.item_uuid[i] != null) {
                                var ratesAarray = parseData.producttypedetails.item_uuid[i].typePriceByDate.rates.filter(obj => obj.type.toLowerCase() == "recommendedprice");
                                $.each(ratesAarray, function (n) {
                                    categoryName = toTitleCase(ratesAarray[n].category);
                                    let recommendedPrice = Math.ceil(ratesAarray[n].amount);
                                    var recommendedPriceFormat = ratesAarray[n].convertedCurrency + " " + FormatCurrency(recommendedPrice);
                                    if (parseInt($('#selectDrpDownAdult').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "adult") {
                                        showBookNow = true;
                                        html += '<p class="heading-bold">' + $('#selectDrpDownAdult').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownSenior').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "senior" && showBookNow) {
                                        html += '<p class="heading-bold">' + $('#selectDrpDownSenior').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownChildren').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "child" && showBookNow) {
                                        html += '<p class="heading-bold">' + $('#selectDrpDownChildren').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                });
                                if (!showBookNow) {
                                    html += '<p class="heading-bold">Unavailable</p>';
                                }
                                if (parseData.producttypedetails.item_uuid[i] != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots.length > 0 && showBookNow) {
                                    isTimeslotsAvailable = 1;
                                    html += '<div>';
                                    html += '<select class="select selectBtn selectDropdown form-control" id="sltTimeSlot">';
                                    html += '<option value="">Select timeslot</option>';
                                    $.each(parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots, function (k) {
                                        html += '<option value="' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].uuid + '">' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].startTime.slice(0, -3) + ' - ' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].endTime.slice(0, -3) + '</option>';
                                    });
                                    html += '</select>';
                                    html += '</div>';
                                }
                            }
                            if (showBookNow) {
                                html += '<div id="dvBookNow" class="dvBtn mt-4 mb-2">';
                                /*                                html += '<button id="btnBookNow" class="btn btn-one w-100" onclick="var retvalue = BookNow(' + parseData.producttypedetails.item_uuid[i].uuid.toString() + '); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button">Book Now</button>';*/
                                html += '<button id="btnBookNow" class="btn btn-one w-100" onclick="var retvalue = BookNow(\'' + parseData.producttypedetails.item_uuid[i].uuid.toString() + '\',' + isTimeslotsAvailable + '); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button">Book Now</button>';
                                html += '</div>';
                            }
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                        }
                    });
                    $("#dvProductTypeDetails").empty().html(html);
                }
            }
        }
        function formatDate(input) {
            var datePart = input.match(/\d+/g),
                year = datePart[0], // get four digits
                month = datePart[1], day = datePart[2];
            return day + '-' + month + '-' + year;
        }
        function fnShowLoader(id) {
            var html = '';
            html += '<div id="divExperienceLoader" class="spin-loader" style="margin: auto;">';
            html += '<img class="spin" width="50" src="Images/loading.gif" alt="" />';
            html += '</div>';
            $("#" + id + "").append(html);
            $("#" + id + "").show();
        }
        function BookNow(ptuuid, IsTimeslotsAvailable) {
            debugger
            $('#btnBookNow').prop('disabled', true);
            fnShowLoader('dvProductDetails');
            // $('#updProgress').show();
            var uuid = getQuerystring("uuid");
            if (parseInt(IsTimeslotsAvailable) == 1) {
                $('#sltTimeSlot').removeClass('error');
                var timeSlot = $.trim($('#sltTimeSlot option:selected').val());
                if (timeSlot == '') {
                    $('#sltTimeSlot').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                    // $('#updProgress').hide();
                    $('#btnBookNow').prop('disabled', false);
                    $('#sltTimeSlot').addClass('error');
                }
                else {
                    window.location.href = "ExperienceProductBookingDetails.aspx?uuid=" + ptuuid + "&adultCount=" + parseInt($('#selectDrpDownAdult').children("option:selected").val()) + "&childrenCount=" + parseInt($('#selectDrpDownChildren').children("option:selected").val() == undefined ? 0 : $('#selectDrpDownChildren').children("option:selected").val()) + "&seniorsCount=" + parseInt($('#selectDrpDownSenior').children("option:selected").val() == undefined ? 0 : $('#selectDrpDownSenior').children("option:selected").val()) + "&ptuuid=" + ptuuid + "&puuid=" + uuid + '&selectedDate=' + encodeURIComponent($('#txtBookingDate').val()) + '&timeslotuuid=' + ($('#sltTimeSlot').children("option:selected").val() == undefined ? "" : $('#sltTimeSlot').children("option:selected").val());
                }
            }
            else {
                window.location.href = "ExperienceProductBookingDetails.aspx?uuid=" + ptuuid + "&adultCount=" + parseInt($('#selectDrpDownAdult').children("option:selected").val()) + "&childrenCount=" + parseInt($('#selectDrpDownChildren').children("option:selected").val() == undefined ? 0 : $('#selectDrpDownChildren').children("option:selected").val()) + "&seniorsCount=" + parseInt($('#selectDrpDownSenior').children("option:selected").val() == undefined ? 0 : $('#selectDrpDownSenior').children("option:selected").val()) + "&ptuuid=" + ptuuid + "&puuid=" + uuid + '&selectedDate=' + encodeURIComponent($('#txtBookingDate').val()) + '&timeslotuuid=' + ($('#sltTimeSlot').children("option:selected").val() == undefined ? "" : $('#sltTimeSlot').children("option:selected").val());
            }
        }
    </script>
</asp:Content>

