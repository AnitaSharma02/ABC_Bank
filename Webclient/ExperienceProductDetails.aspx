<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductDetails.aspx.cs" Inherits="ExperienceProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />
    <style>
        .dvInnerBanner{
            display:none;
        }
    </style>

    <div class="dvBreadcrumbs my-3 bg-colour2">
        <div class="container-xl">
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

    <div class="dvExperienceProductDetails pb-5" id="dvProductDetails">
        <div class="container-xl">
            <div class="row">
                <div class="dvInformation col-lg-4">
                    <div id="swiperHtml"></div>
                </div>

                <div class="dvForm col-lg-8">
                    <div id="swiperHeadingHtml"></div>
                    <div class="row">
                        <div class="col-12">
                            <h2 class="heading6 text-colour7 my-3">Product types</h2>
                        </div>
                        <div class="dvForm col-12">
                            <div class="border b-radius p-3 mb-3">
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
                                                <span class="input-group-text"><i class="fa-regular fa-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-6 col-md-4" id="divAdult"></div>
                                    <div class="col-6 col-md-4" id="divSenior"></div>
                                    <div class="col-6 col-md-4" id="divChild"></div>
                                </div>
                            </div>
                            <div class="border b-radius p-3 mb-3">
                                <div class="dvVcData" id="dvProductTypeDetails"></div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="dvTabs col-12">
                            <nav>
                                <div class="nav nav-tabs flex-nowrap scroll-hoz border-bottom-0" id="nav-tab" role="tablist">
                                    <button class="nav-link active" data-toggle="tab" data-target="#highlights-tab" type="button">Information</button>
                                    <button class="nav-link" data-toggle="tab" data-target="#description-tab" type="button">Description</button>
                                    <button class="nav-link" data-toggle="tab" data-target="#address-tab" type="button">Location</button>
                                </div>
                            </nav>
                            <div class="tab-content" id="nav-tabContent">
                                <div class="tab-pane fade border show active" id="highlights-tab">
                                    <div id="highlightsHtml" class="p-3"></div>
                                </div>
                                <div class="tab-pane fade border" id="description-tab">
                                    <div id="descriptionHtml" class="p-3"></div>
                                </div>
                                <div class="tab-pane fade border" id="address-tab">
                                    <div id="addressHtml" class="p-3"></div>
                                </div>
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
            var swiperHtml = '';
            var swiperHeadingHtml = '';
            var highlightsHtml = '';
            var descriptionHtml = '';
            var addressHtml = '';
            if (data != '') {
                var parseData = JSON.parse(data);
                if (parseData.data != null) {
                    $("#Errordiv").show();
                    swiperHtml += '<div class="row">';
                    swiperHtml += '<div class="col-12">';
                    swiperHeadingHtml += '<div class="row mt-3 mt-lg-0">';
                    swiperHeadingHtml += '<div class="col-12">';
                    swiperHeadingHtml += '<p class="heading6">' + parseData.data.locations[0].city + ',' + parseData.data.locations[0].country + '</p>';
                    swiperHeadingHtml += '<p class="heading6">' + parseData.data.title + '</p>';
                    swiperHeadingHtml += '</div>';
                    swiperHeadingHtml += '</div>';
                    $("#swiperHeadingHtml").append(swiperHeadingHtml);
                    swiperHtml += '<div class="row">';
                    swiperHtml += '<div class="dvThumbSwiperSlider col-lg-12 col-xl-12">';
                    swiperHtml += '<div class="border b-radius p-3">';
                    swiperHtml += '<div style="--swiper-navigation-color: #fff; --swiper-pagination-color: #fff" class="swiper dvThumbBannerSlide">';
                    swiperHtml += '<div class="swiper-wrapper">';
                    for (var i = 0; i < parseData.data.photos.length; i++) {
                        swiperHtml += '<div class="swiper-slide img-container">';
                        swiperHtml += '<img src="' + parseData.data.photos[i].paths.original + '"/>';
                        swiperHtml += '</div>';
                    }
                    swiperHtml += '</div>';
                    swiperHtml += '<div class="swiper-button-next">';
                    swiperHtml += '<img src="images/carpage/icons/right-arrow-violet.svg" />';
                    swiperHtml += '</div>';
                    swiperHtml += '<div class="swiper-button-prev">';
                    swiperHtml += '<img src="images/carpage/icons/left-arrow-violet.svg" />';
                    swiperHtml += '</div>';
                    swiperHtml += '</div>';
                    swiperHtml += '<div class="my-2"></div>';
                    swiperHtml += '<div class="swiper dvThumbSlide">';
                    swiperHtml += '<div class="swiper-wrapper">';

                    for (var i = 0; i < parseData.data.photos.length; i++) {
                        swiperHtml += '<div class="swiper-slide img-container">';
                        swiperHtml += '<img src="' + parseData.data.photos[i].paths.original + '"/>';
                        swiperHtml += '</div>';
                    }
                    swiperHtml += '</div>';
                    swiperHtml += '<div class="swiper-button-next">';
                    swiperHtml += '<img src="images/carpage/icons/right-arrow-violet.svg" />';
                    swiperHtml += '</div>';
                    swiperHtml += '<div class="swiper-button-prev">';
                    swiperHtml += '<img src="images/carpage/icons/left-arrow-violet.svg" />';
                    swiperHtml += '</div>';
                    swiperHtml += '</div>';
                    swiperHtml += '</div>';
                    swiperHtml += '</div>';
                    swiperHtml += '</div>';
                    swiperHtml += '<div class="row mt-3">';
                    swiperHtml += '<div class="dvTextIcon col-12">';
                    if (parseData.data.producttypedetails != null && parseData.data.producttypedetails.item_uuid != null && parseData.data.producttypedetails.item_uuid[0].typeinfo != null) {
                        if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes == 0) {
                            swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays + ' day</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes == 0) {
                            swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours + ' hr</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes > 0) {
                            swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes + ' min</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays == 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes > 0) {
                            swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours + ' hr ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes + ' min</p>';
                        }
                        else if (parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours > 0
                            && parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes > 0) {
                            swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-clock"></i> ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationDays + ' day ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationHours + ' hr ' + parseData.data.producttypedetails.item_uuid[0].typeinfo.durationMinutes + ' min</p>';
                        }
                    }
                    if (parseData.data != null && parseData.data.tourType != null && parseData.data.tourType != "") {
                        switch (parseData.data.tourType.toLowerCase()) {
                            case "join":
                                swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-share-nodes"></i> Shared</p>';
                                break;
                            case "private":
                                swiperHtml += '<p class="h7 text-colour7"><i class="fa-solid fa-user"></i> Private Tour</p>';
                                break;
                        }
                    }
                    swiperHtml += '<p class="h7 text-colour7">';
                    swiperHtml += '<i class="fa-solid fa-people-group"></i> Group Size: min: ' + parseData.data.minPax + ' pax | Max: ' + parseData.data.maxPax + ' pax';
                    swiperHtml += '</p>';
                    if (parseData.data != null && parseData.data.businessHoursFrom != null && parseData.data.businessHoursFrom != '' && parseData.data.businessHoursTo != null && parseData.data.businessHoursTo != '') {
                        swiperHtml += '<p class="h7 text-colour7">Opening hours: ' + parseData.data.businessHoursFrom + 'am —' + parseData.data.businessHoursTo + 'pm</p>';
                    }
                    swiperHtml += '</div>';
                    swiperHtml += '</div>';
                    $("#swiperHtml").append(swiperHtml);



                    /*dvHighlights*/
                    if (parseData.data != null && parseData.data.highlights != null && parseData.data.highlights != '') {
                        //highlightsHtml += '<div class="border-bottom my-4"></div>';
                        highlightsHtml += '<div class="row">';
                        highlightsHtml += '<div class="dvHighlights col-12 mb-2">';
                        highlightsHtml += '<p class="heading6">Highlights</p>';
                        highlightsHtml += '<ul class="mt-2">';
                        $.each(parseData.data.highlights.split("\n"), function (i) {
                            highlightsHtml += '<li>' + parseData.data.highlights.split("\n")[i] + '</li>';
                        });
                        highlightsHtml += '</ul>';
                        highlightsHtml += '</div>';
                        highlightsHtml += '</div>';
                    }
                    /*dvHighlights*/


                    /*dvAdditionalInfo*/
                    //html += '<div class="border-bottom my-4"></div>';
                    highlightsHtml += '<div class="row">';
                    highlightsHtml += '<div class="dvAdditionalInfo col-12">';
                    //html += '<p class="heading6">Additional Info</p>';
                    if (parseData.data != null && parseData.data.priceIncludes != null && parseData.data.priceIncludes != '') {
                        highlightsHtml += '<p class="heading6 pt-2">Price Includes</p>';
                        highlightsHtml += '<ul>';
                        $.each(parseData.data.priceIncludes.split("\n"), function (i) {
                            highlightsHtml += '<li>' + parseData.data.priceIncludes.split("\n")[i] + '</li>';
                        });
                        highlightsHtml += '</ul>';
                    }
                    if (parseData.data != null && parseData.data.priceExcludes != null && parseData.data.priceExcludes != '') {
                        highlightsHtml += '<p class="heading6 pt-2">Price Excludes</p>';
                        highlightsHtml += '<ul>';
                        $.each(parseData.data.priceExcludes.split("\n"), function (i) {
                            highlightsHtml += '<li>' + parseData.data.priceExcludes.split("\n")[i] + '</li>';
                        });
                        highlightsHtml += '</ul>';
                        highlightsHtml += '</div>';
                        highlightsHtml += '</div>';
                    }
                    /*dvAdditionalInfo*/


                    /*dvAdditionalDetails*/
                    highlightsHtml += '<div class="row">';
                    highlightsHtml += '<div class="dvAdditionalDetails col-12">';
                    //html += '<ul>';
                    if (parseData.data != null && parseData.data.additionalInfo != null && parseData.data.additionalInfo != '') {
                        $.each(parseData.data.additionalInfo.split("\n"), function (i) {
                            if (parseData.data.additionalInfo.split("\n")[i] != null && parseData.data.additionalInfo.split("\n")[i] != '') {
                                highlightsHtml += '<p class="pt-2">' + parseData.data.additionalInfo.split("\n")[i] + '</p>';
                            }
                        });
                    }
                    if (parseData.data != null && parseData.data.warnings != null && parseData.data.warnings != '') {
                        highlightsHtml += '<p class="heading6 pt-2">Warnings of the activity</p>';
                        $.each(parseData.data.warnings.split("\r\n"), function (i) {
                            if (parseData.data.warnings.split("\r\n")[i]) {
                                highlightsHtml += '<p class="pt-2">' + parseData.data.warnings.split("\r\n")[i] + '</p>';
                            }
                        });
                    }
                    if (parseData.data != null && parseData.data.guideLanguages.length > 0) {
                        highlightsHtml += '<ul><li class="pt-2">Additional audio guide language:' + Array.prototype.map.call(parseData.data.guideLanguages, function (item) { return toTitleCase(item.name); }).join(",") + '</li></ul>';
                    }
                    if (parseData.data != null && parseData.data.audioHeadsetLanguages.length > 0) {
                        highlightsHtml += '<li><li class="pt-2">Languages for Audio Headset material:' + Array.prototype.map.call(parseData.data.audioHeadsetLanguages, function (item) { return toTitleCase(item.name); }).join(",") + '</li></ul>';
                    }
                    //html += '</ul>';
                    /*dvAdditionalDetails*/
                    
                    highlightsHtml += '</div>';
                    highlightsHtml += '</div>';
                    
                    if (parseData.data != null && parseData.data.itinerary != null && parseData.data.itinerary != '') {
                        //highlightsHtml += '<div class="border-bottom my-4"></div>';
                        highlightsHtml += '<div class="row">';
                        highlightsHtml += '<div class="dvHighlights col-12">';
                        highlightsHtml += '<p>Itinerary</p>';
                        highlightsHtml += '<ul class="mt-2">';
                        $.each(parseData.data.itinerary.split("\n"), function (i) {
                            highlightsHtml += '<li>' + parseData.data.itinerary.split("\n")[i] + '</li>';
                        });
                        highlightsHtml += '</ul>';
                        highlightsHtml += '</div>';
                        highlightsHtml += '</div>';
                    }
                    $("#highlightsHtml").append(highlightsHtml);


                    //descriptionHtml += '<div class="border-bottom my-4"></div>';
                    descriptionHtml += '<div class="row">';
                    descriptionHtml += '<div class="dvDescription col-12">';

                    if (parseData.data != null && parseData.data.description != null && parseData.data.description != '') {
                        descriptionHtml += '<p class="heading6">Description</p>';
                        descriptionHtml += '<div>';
                        //descriptionHtml += '<ul>';
                        $.each(parseData.data.description.split("\r\n"), function (i) {
                            descriptionHtml += '<p class="pt-2">' + parseData.data.description.split("\r\n")[i] + '</p>';
                        });
                        //descriptionHtml += '</ul>';
                        descriptionHtml += '</div>';
                    }
                    descriptionHtml += '</div>';
                    descriptionHtml += '</div>';
                    $("#descriptionHtml").append(descriptionHtml);

                    //html += '<div class="img-container mt-3">';
                    //html += '<img alt="Product Image" src="images/carpage/map.png" />';
                    //html += '</div>';
                    if (parseData.data != null && parseData.data.title != null && parseData.data.title != '' && parseData.data.address != null && parseData.data.address != '') {
                        addressHtml += '<p><span class="heading6">Address:</span> <a class="link1" target="_blank" href="http://maps.google.com/maps?q=' + parseData.data.title + ',' + parseData.data.address + '">' + parseData.data.title + ',' + parseData.data.address + '</a></p>';
                    }                    
                    $("#addressHtml").append(addressHtml);

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

                    //$(".toggle_btn").click(function () {
                    //    $(this).toggleClass("active");
                    //    $(".dvReadMore ul").toggleClass("active");

                    //    if ($(".toggle_btn").hasClass("active")) {
                    //        $(".toggle_text").text("Show Less");
                    //    } else {
                    //        $(".toggle_text").text("Show More");
                    //    }
                    //});
                    //$(".toggle_btn2").click(function () {
                    //    $(this).toggleClass("active");
                    //    $(".dvReadMore2 ul").toggleClass("active");

                    //    if ($(".toggle_btn2").hasClass("active")) {
                    //        $(".toggle_text2").text("Show Less");
                    //    } else {
                    //        $(".toggle_text2").text("Show More");
                    //    }
                    //});
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
                        html += '<div class=" dvInput input-group" id="sltAdult">';
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
                        html += '<div class="dvInput input-group" id="sltSenior">';
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
                        html += '<div class="dvInput input-group" id="sltChild">';
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
                        html += '<div class="dvTitle bg-colour2 p-3 mb-1">';
                        html += '<div class="d-flex justify-content-between">';
                        html += '<p class="heading6">' + parseData.producttypedetails.item_uuid[i].typeinfo.title + '</p>';
                        html += '</div>';
                        html += '<div class="pt-1">';
                        html += '<p class="h7 text-colour7">' + parseData.producttypedetails.item_uuid[i].typeinfo.description + '</p>';
                        html += '</div>';
                        html += '</div>';

                        html += '<div class="dvEnterTravelDate">';
                        html += '<div class="d-flex flex-wrap bg-colour2 py-2">';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.available == true) {
                            html += '<div "class="col-12 d-flex align-items-center ptypepricebydate text-colour7 mb-2"> <span class="h7 heading-semibold">Valid only on</span> <span class="ml-2 h7 heading-semibold">' + formatDate(parseData.producttypedetails.item_uuid[i].typePriceByDate.date) + '</span></div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isBmgVoucher == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7"><i class="fa-solid fa-mobile-screen-button h7"></i><span class="ml-2 h7">Show on mobile</span></div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isNonRefundable == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7">';
                            html += '<i class="fa-solid fa-triangle-exclamation h7"></i><span class="ml-2 h7">Non Refundable</span>';
                            html += '</div>';
                        }
                        else {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7">';
                            html += '<i class="fa-solid fa-triangle-exclamation h7"></i><span class="ml-2 h7">Refundable</span>';
                            html += '</div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.instantConfirmation == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7"><i class="fa-solid fa-bolt h7"></i><span class="ml-2 h7">Instant</div></span>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.voucherRequiresPrinting == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7"><i class="fa-solid fa-bolt h7"></i><span class="ml-2 h7">Print ticket</div></span>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.directAdmission == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7">';
                            html += '<i class="fa fa-address-book h7" aria-hidden="true"></i><span class="ml-2 h7">Direct admission</span>';
                            html += '</div>';
                        }
                        html += '</div>';

                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate == null) {
                            html += '<div class="mt-2 mb-4 text-center text-lg-right clsentertraveldate">';
                            html += '<button class="btn btn-one" onclick="var retvalue = ShowDatePicker(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button"';
                            html += 'value="Search"> Enter travel date </button>';
                            html += '</div>';
                        }
                        html += '</div>';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.rates != null) {
                            html += '<div class="border-top"></div>';
                            html += '<div class="d-flex flex-wrap dvtypepricingbox">';
                            html += '<div class="col-12 col-md-9 p-0 br-1">';
                            html += '</div>';
                            html += '<div class="dvRateBox col-12 col-md-3 p-0 mt-3 mt-md-0">';
                            html += '<div class="w-100 border">';
                            html += '<div class="dvBgcolor2">';
                            html += '<h2 class="h6 heading-bold p-3 bg-colour1 text-colour3">Price <span class="">includes GST</span></h2>';
                            html += '</div>';
                            html += '<div class="">';
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
                                        html += '<p>' + $('#selectDrpDownAdult').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownSenior').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "senior" && showBookNow) {
                                        html += '<p>' + $('#selectDrpDownSenior').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownChildren').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "child" && showBookNow) {
                                        html += '<p>' + $('#selectDrpDownChildren').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                });
                                if (!showBookNow) {
                                    html += '<p>Unavailable</p>';
                                }
                                html += '<div class="d-flex flex-wrap justify-content-between">';
                                html += '<div class="col-12">';
                                html += '<div id="sltTimeSlot">';
                                html += '<div class="row">';
                                if (parseData.producttypedetails.item_uuid[i] != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots.length > 0 && showBookNow) {
                                    isTimeslotsAvailable = 1;
                                    html += '<div class="col-sm-6 mb-2">';                                    
                                    html += '<select class="select selectBtn selectDropdown form-control">';
                                    html += '<option value="">Select timeslot</option>';
                                    $.each(parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots, function (k) {
                                        html += '<option value="' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].uuid + '">' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].startTime.slice(0, -3) + ' - ' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].endTime.slice(0, -3) + '</option>';
                                    });
                                    html += '</select>';
                                    //html += '</div>';
                                    html += '</div>';
                                }
                            }
                            if (showBookNow) {
                                html += '<div id="dvBookNow" class="col-sm-6">';
                                html += '<button id="btnBookNow" class="btn btn-one w-100" onclick="var retvalue = BookNow(\'' + parseData.producttypedetails.item_uuid[i].uuid.toString() + '\',' + isTimeslotsAvailable + '); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button">Book Now</button>';
                                html += '</div>';
                            }
                            html += '</div>';//row
                            html += '</div>';//col-12
                            html += '</div>';//sltTimeSlot
                            html += '</div>';
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
                        html += '<div class="dvTitle bg-colour2 p-3 mb-1">';
                        html += '<div class="d-flex justify-content-between">';
                        html += '<p class="heading6">' + parseData.producttypedetails.item_uuid[i].typeinfo.title + '</p>';
                        html += '</div>';
                        html += '<div class="pt-1">';
                        html += '<p class="h7 text-colour7">' + parseData.producttypedetails.item_uuid[i].typeinfo.description + '</p>';
                        html += '</div>';
                        html += '</div>';

                        html += '<div class="dvEnterTravelDetails">';
                        html += '<div class="d-flex flex-wrap bg-colour2 py-2">';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.available == true) {
                            html += '<div class="col-12 d-flex align-items-center ptypepricebydate text-colour7 mb-2"> <span class="h7 heading-semibold">Valid only on</span> <span class="ml-2 h7 heading-semibold">' + formatDate(parseData.producttypedetails.item_uuid[i].typePriceByDate.date) + '</span></div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isBmgVoucher == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7"><i class="fa-solid fa-mobile-screen-button h7"></i><span class="ml-2 h7">Show on mobile</span></div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.isNonRefundable == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7">';
                            html += '<i class="fa-solid fa-triangle-exclamation h7"></i><span class="ml-2 h7">Non Refundable</span>';
                            html += '</div>';
                        }
                        else {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7">';
                            html += '<i class="fa-solid fa-triangle-exclamation h7"></i><span class="ml-2 h7">Refundable</span>';
                            html += '</div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.voucherRequiresPrinting == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7"><i class="fa-solid fa-bolt h7"></i><span class="ml-2 h7">Print ticket</span></div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.instantConfirmation == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7"><i class="fa-solid fa-bolt h7"></i><span class="ml-2 h7">Instant</span></div>';
                        }
                        if (parseData.producttypedetails.item_uuid[i].typeinfo.directAdmission == true) {
                            html += '<div class="col-6 col-md-4 d-flex align-items-center text-colour7">';
                            html += '<i class="fa fa-address-book h7" aria-hidden="true"></i><span class="ml-2 h7">Direct admission</span>';
                            html += '</div>';
                        }
                        html += '</div>';

                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate == null) {
                            html += '<div class="mt-2 mb-4 text-center text-lg-right clsentertraveldate">';
                            html += '<button class="btn btn-one" onclick="var retvalue = ShowDatePicker(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button"';
                            html += 'value="Search"> Enter travel date </button>';
                            html += '</div>';
                        }
                        html += '</div>';
                        if (parseData.producttypedetails.item_uuid[i].typePriceByDate != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.rates != null) {
                            //html += '<div class="border-top"></div>';
                            html += '<div class="d-flex flex-wrap dvtypepricingbox">';
                            //html += '<div class="col-12 col-md-9 p-0 br-1">';
                            //html += '</div>';
                            html += '<div class="dvTotalPrice col-12 px-0 mb-3">';
                            html += '<div class="bg-colour2 p-3 text-center">';
                            html += '<div class="d-flex flex-wrap align-items-center mb-1">';
                            html += '<p class="h7"><span>Price</span> <span class="">includes GST</span></p>';
                            
                            //html += '<div class="">';
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
                                        html += '<p class="dvItem text-colour7 heading-bold h7 ml-2">' + $('#selectDrpDownAdult').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownSenior').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "senior" && showBookNow) {
                                        html += '<p class="dvItem text-colour7 heading-bold h7 ml-2">' + $('#selectDrpDownSenior').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                    else if (parseInt($('#selectDrpDownChildren').children("option:selected").val()) > 0 && ratesAarray[n].category.toLowerCase() == "child" && showBookNow) {
                                        html += '<p class="dvItem text-colour7 heading-bold h7 ml-2">' + $('#selectDrpDownChildren').children("option:selected").val() + " " + categoryName + ' x ' + recommendedPriceFormat + '</p>';
                                    }
                                });
                                if (!showBookNow) {
                                    html += '<p>Unavailable</p>';
                                }
                                html += '</div>';
                                html += '<div class="d-flex flex-wrap justify-content-between align-items-center mx-n3">';
                                html += '<div class="col-12">';
                                html += '<div id="sltTimeSlot">';
                                html += '<div class="row">';
                                if (parseData.producttypedetails.item_uuid[i] != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots != null && parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots.length > 0 && showBookNow) {
                                    isTimeslotsAvailable = 1;
                                    html += '<div class="col-sm-6 mb-2">';                                    
                                    html += '<select class="select selectBtn selectDropdown form-control">';
                                    html += '<option value="">Select timeslot</option>';
                                    $.each(parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots, function (k) {
                                        html += '<option value="' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].uuid + '">' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].startTime.slice(0, -3) + ' - ' + parseData.producttypedetails.item_uuid[i].typePriceByDate.timeslots[k].endTime.slice(0, -3) + '</option>';
                                    });
                                    html += '</select>';
                                    html += '</div>';
                                    
                                }
                            }
                            if (showBookNow) {
                                html += '<div id="dvBookNow" class="col-sm-6">';
                                /* html += '<button id="btnBookNow" class="btn btn-one w-100" onclick="var retvalue = BookNow(' + parseData.producttypedetails.item_uuid[i].uuid.toString() + '); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button">Book Now</button>';*/
                                html += '<button id="btnBookNow" class="btn btn-one w-100" onclick="var retvalue = BookNow(\'' + parseData.producttypedetails.item_uuid[i].uuid.toString() + '\',' + isTimeslotsAvailable + '); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button">Book Now</button>';
                                html += '</div>';
                            }
                            html += '</div>';//row
                            html += '</div>';//sltTimeSlot
                            html += '</div>';//col-12
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
            //debugger
            $('#btnBookNow').prop('disabled', true);
            fnShowLoader('dvProductDetails');
            // $('#updProgress').show();
            var uuid = getQuerystring("uuid");
            if (parseInt(IsTimeslotsAvailable) == 1) {
                $('#sltTimeSlot').removeClass('text-danger');
                var timeSlot = $.trim($('#sltTimeSlot option:selected').val());
                if (timeSlot == '') {
                    $('#sltTimeSlot').closest("div").after('<p class="dvErrors text-danger text-left"><span>This field is required</span></p>');
                    // $('#updProgress').hide();
                    $('#btnBookNow').prop('disabled', false);
                    $('#sltTimeSlot').addClass('text-danger');
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

