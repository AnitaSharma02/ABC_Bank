<%@ Page Title="Experience Product Details" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductDetails.aspx.cs" Inherits="ExperienceProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <input id="hdnAvailabilityType" type="hidden" runat="server" value="" />
    <link rel="stylesheet" href="css/experiences.css" />
    <link href="Css/ExperienceCalendar.css" rel="stylesheet" />
    <script src="Jquery/ExperienceCalendar.js" type="text/javascript"></script>
    <style type="text/css">
       /* body{
            padding-bottom:80px !important;
        }*/
        #divClndAvailableBookingDateLoader .cssload-wrapper{
            margin-top: 0px !important;
        }
        .disClass a {
            cursor: default;
        }

        .new-bg {
            background: #fff;
        }

        td.SelectedDayStyle {
            background-color: #000000 !important;
        }

        #divSearchCart {
            display: none !important;
        }

        ul,
        ol,
        li {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item active" data-i18n="navigation-exp-prod-det">Experience Product Details</li>
                </ul>
            </nav>
        </div>
    </div>
    <section class="MyExperiences pb-5 new-bg">
        <div class="container">
            <div class="row">
                <div class="col-md-8">
                    <div id="divExperienceProductDetails"></div>
                </div>
                <div class="cldBookingDate col-md-4">
                    <div id="divClndBookingDate" class="cldBookingDate text-left">
                        <label class="">Select Date</label>
                        <div id="divClndAvailableBookingDate" style="width: 100%;"></div>
                        <div id="divClndAvailableBookingDateLoader"></div>
                    </div>
                    <div id="divNoAvailabilityMsg" style="display: none;"></div>
                    <div id="divBookingAvailabilityInput" style="display: none;"></div>
                    <div id="divBookingErrorMsg" style="display: none;"></div>
                    <div id="divEmailErrorMsg" style="display: none;" runat="server"></div>
                </div>
            </div>
        </div>
    </section>
    <script type="text/javascript">
        /*document.getElementById("pageName").innerHTML = "Experience";*/
        var availabilityId = '';
        $(document).ready(function () {
            fnGetExperienceProductDetails();
            fnGetExperienceProductStatus();
            var errordiv = document.getElementById('CP_divEmailErrorMsg').style.display;
            if (errordiv == "block") {
                $('#btnAddToBooking').hide();
                $('#btnAddToBooking').removeAttr('onclick');
            }
            else {
                $('#btnAddToBooking').show();
                $('#btnAddToBooking').attr('onclick', 'fnCreateBooking();');
            }
            setTimeout(function () { fnAccordianSetup(); }, 4000);
        });
        function fnShowLoader(id) {
            var html = '';
            html += '<div id="divExperienceLoader" class="spin-loader">';
            html += '<img class="spin" width="50" src="Images/loader/spinner-2.gif" alt="" />';
            html += '</div>';
            $("#" + id + "").empty().html(html);
            $("#" + id + "").show();
        }
        $(document).ajaxStart(function () {
            $("#updProgress").hide();
        }).ajaxStop(function () {
            $("#updProgress").hide();
        });
        function fnGetExperienceProductDetails() {
            try {
                fnShowLoader('divExperienceProductDetails');
                var arrData = {};
                arrData.pstrProductId = getQuerystring('Id');
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductDetails.aspx/GetExperienceProductDetails',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    success: function (rtnData) {
                        fnBindExperienceProductDetails(rtnData.d);
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnGetExperienceProductStatus(startDate, checkNextMonthAvailabilityIfNotFound, availabilityType) {
            try {
                fnShowLoader('divClndAvailableBookingDateLoader');
                $('#divClndAvailableBookingDate').hide();
                var firstCall = false;
                if (typeof (startDate) == 'undefined' || startDate == '') {
                    startDate = fnFormatDate(new Date());
                    firstCall = true;
                }
                var arrData = {};
                arrData.pstrProductId = getQuerystring('Id');
                arrData.pstrStartDate = startDate;
                arrData.pblnCheckNextMonthAvailabilityIfNotFound = typeof (checkNextMonthAvailabilityIfNotFound) == 'undefined' ? true : checkNextMonthAvailabilityIfNotFound;
                arrData.pstrAvailabilityType = typeof(availabilityType) == 'undefined' ? 'date' : availabilityType;
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductDetails.aspx/GetExperienceProductStatus',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    success: function (rtnData) {
                        $('#divClndAvailableBookingDateLoader').hide();
                        if (rtnData.d[3] != '' && rtnData.d[3] != 'true') {
                            $('#divClndBookingDate').hide();
                        }
                        if (rtnData.d[0] != '') {
                            $('#divNoAvailabilityMsg').empty().html(rtnData.d[0]);
                            $('#divNoAvailabilityMsg').show();
                            if (rtnData.d[1] != '' && rtnData.d[4] == 'pass') {
                                fnGetExperienceProductAvailability(rtnData.d[1]);
                            }
                            if (rtnData.d[1] == '' && rtnData.d[4] == 'date') {
                                $('#divClndBookingDate').hide();
                            }
                        } else if (rtnData.d[0] == '') {
                            $('#divClndAvailableBookingDate').show();
                            if (rtnData.d[4] == 'date') {
                                var productBookingDate = [];
                                JSON.parse(rtnData.d[1]).forEach(function (a) {
                                    productBookingDate.push({ date: a.date, value: a.id });
                                }, Object.create(null));
                                productBookingDate.sort(function (a, b) {
                                    return new Date(a.date) - new Date(b.date);
                                });
                                if (firstCall) {
                                    $('#divClndAvailableBookingDate').calendar({
                                        width: 320,
                                        height: 320,
                                        data: productBookingDate,
                                        date: new Date(rtnData.d[2]),
                                        selectedRang: [productBookingDate[0]?.date, productBookingDate[productBookingDate.length > 0 ? productBookingDate.length - 1 : 0]?.date],
                                        onSelected: function (view, date, data) {
                                            $('#divBookingAvailabilityInput').empty();
                                            if (typeof (data) != 'undefined' && data != '' && view == 'date') {
                                                fnGetExperienceProductAvailability(data);
                                            }
                                        },
                                        viewChange: function (view, y, m) {
                                            $('#divBookingAvailabilityInput').empty();
                                            if (view == 'date' && typeof (m) != 'undefined') {
                                                fnGetExperienceProductStatus(fnFormatDate(new Date(y, m - 1, 1)), false, rtnData.d[4]);
                                            }
                                        }
                                    });
                                } else {
                                    $('#divClndAvailableBookingDate').calendar("setData", productBookingDate);
                                    $('#divClndAvailableBookingDate').calendar("setMonth", new Date(rtnData.d[2]).getMonth);
                                    $('#divClndAvailableBookingDate').calendar("setDate", new Date(rtnData.d[2])); 
                                }
                            }
                        }
                    },
                    error: function (errmsg) {
                    },
                    beforeSend: function () {
                        $("#updProgress").hide();
                    }
                });
            } catch (e) {
            }
        }
        function fnBindExperienceProductDetails(data) {
            try {
                var html = '';
                if (data != '') {
                    var parseData = JSON.parse(data);
                    $('#CP_hdnAvailabilityType').val(parseData.origSupplierResponse.product.availabilityType);
                    html += '<div class="MyExpeLeft">';
                    html += '<h2> ' + parseData.origSupplierResponse.product.name + '</h2></div>';
                    html += '<div class="sliderBox">';
                    html += '<div class="carousel-container position-relative row">';
                    html += '<div id="myCarousel" class="carousel slide" data-ride="carousel">';
                    html += '<div class="carousel-inner">';
                    for (var i = 0; i < parseData.origSupplierResponse.product.imageList.length; i++) {
                        html += '<div class="carousel-item ' + (i == 0 ? 'active' : '') + '" data-slide-number="' + i + '">';
                        html += '<img src="' + parseData.origSupplierResponse.product.imageList[i].urlLarge + '" class="d-block w-100" data-remote="' + parseData.origSupplierResponse.product.imageList[i].urlLarge + '" data-type="image" data-toggle="lightbox" data-gallery="example-gallery">';
                        html += '</div>';
                    }
                    html += '</div>';
                    html += '</div>';
                    html += '<div id="carousel-thumbs" class="carousel slide w-100 py-2" data-ride="carousel">';
                    html += '<div class="carousel-inner w-100 mx-auto">';
                    html += '<div class="carousel-item active">';

                    html += '<div class="d-flex justify-content-center">';
                    for (var j = 0; j < parseData.origSupplierResponse.product.imageList.length; j++) {
                        html += '<div id="carousel-selector-' + j + '" class="thumb px-1 py-2 selected" data-target="#myCarousel" data-slide-to="' + j + '">';
                        html += '<img src="' + parseData.origSupplierResponse.product.imageList[j].urlTiny + '" class="img-fluid">';
                        html += '</div>';
                        if (j == 5) {
                            break;
                        }
                    }
                    html += '</div>';
                    html += '</div>';
                    for (var k = 6; k < parseData.origSupplierResponse.product.imageList.length; k++) {
                        if (k == 6) {
                            html += '<div class="carousel-item">';
                            html += '<div class="d-flex justify-content-center">';
                        }
                        html += '<div id="carousel-selector-' + k + '" class="thumb px-1 py-2 selected" data-target="#myCarousel" data-slide-to="' + k + '">';
                        html += '<img src="' + parseData.origSupplierResponse.product.imageList[k].urlTiny + '" class="img-fluid">';
                        html += '</div>';
                        if (k == 10) {
                            html += '</div>';
                            html += '</div>';
                            break;
                        }
                    }
                    html += '</div>';
                    html += '<a class="carousel-control-prev" href="#carousel-thumbs" role="button" data-slide="prev">';
                    html += '<span class="carousel-control-prev-icon" aria-hidden="true" />';
                    html += '<span class="sr-only">Previous</span>';
                    html += '</a>';
                    html += '<a class="carousel-control-next" href="#carousel-thumbs" role="button" data-slide="next">';
                    html += '<span class="carousel-control-next-icon" aria-hidden="true" />';
                    html += '<span class="sr-only">Next</span>';
                    html += '</a>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '<div class="prdIcon expHead">';
                    html += '<ul class="moreExp row">';
                    var guideLanguages = '';
                    if (parseData.origSupplierResponse.product.guideLanguageList.recordCount > 1) {
                        html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-globe"></i></div> <div class="contentBox"><span>Guide Language</span><p>Multiple</p><div></li>';
                        for (var n = 0; n < parseData.origSupplierResponse.product.guideLanguageList.nodes.length; n++) {
                            if (parseData.origSupplierResponse.product.guideLanguageList.nodes[n].name != null && parseData.origSupplierResponse.product.guideLanguageList.nodes[n].name != '') {
                                guideLanguages += '<li>' + parseData.origSupplierResponse.product.guideLanguageList.nodes[n].name + ' </li> ';
                            }
                        }
                    } else if (parseData.origSupplierResponse.product.guideLanguageList.recordCount == 1) {
                        if (parseData.origSupplierResponse.product.guideLanguageList.nodes[0].name != null && parseData.origSupplierResponse.product.guideLanguageList.nodes[0].name != '') {
                            html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-globe"></i></div> <div class="contentBox"><span>Guide Language</span><p>' + parseData.origSupplierResponse.product.guideLanguageList.nodes[0].name + '</p><div></li>';
                        }
                    }
                    if (parseData.origSupplierResponse.product.attributeList.nodes.length > 0) {
                        var groupByProductAttributeListNodes = [];
                        parseData.origSupplierResponse.product.attributeList.nodes.forEach(function (a) {
                            if (!this[a.level1]) {
                                this[a.level1] = { level1: a.level1, name: [] };
                                this[a.level1].name.push(a.name);
                                groupByProductAttributeListNodes.push(this[a.level1]);
                            } else {
                                this[a.level1].name.push(fnCamelCase(a.name))
                            }
                        }, Object.create(null));
                        for (var o = 0; o < groupByProductAttributeListNodes.length; o++) {
                            switch (groupByProductAttributeListNodes[o].level1.replace(/\s+/g, '').toLowerCase()) {
                                case "addedextras":
                                    html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-clock"></i></div> <div class="contentBox"><span>' + groupByProductAttributeListNodes[o].level1 + '</span><p>' + groupByProductAttributeListNodes[o].name.join(", ") + '</p><div></li>';
                                    break;
                                case "accessibility":
                                    html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-person"></i></div> <div class="contentBox"><span>' + groupByProductAttributeListNodes[o].level1 + '</span><p>' + groupByProductAttributeListNodes[o].name.join(", ") + '</p><div></li>';
                                    break;
                                case "meetingtype":
                                    html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-person-square"></i></div> <div class="contentBox"><span>' + groupByProductAttributeListNodes[o].level1 + '</span><p>' + groupByProductAttributeListNodes[o].name.join(", ") + '</p><div></li>';
                                    break;
                                case "goodtoknow":
                                    html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-exclamation-circle"></i></div> <div class="contentBox"><span>' + groupByProductAttributeListNodes[o].level1 + '</span><p>' + groupByProductAttributeListNodes[o].name.join(", ") + '</p><div></li>';
                                    break;
                                case "tourtype":
                                    html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-briefcase"></i></div> <div class="contentBox"><span>' + groupByProductAttributeListNodes[o].level1 + '</span><p>' + groupByProductAttributeListNodes[o].name.join(", ") + '</p><div></li>';
                                    break;
                                case "travelertype":
                                    html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-people"></i></div> <div class="contentBox"><span>' + groupByProductAttributeListNodes[o].level1 + '</span><p>' + groupByProductAttributeListNodes[o].name.join(", ") + '</p><div></li>';
                                    break;
                            }
                        }
                    }
                    if (parseData.origSupplierResponse.product.difficultyLevel != '' && parseData.origSupplierResponse.product.difficultyLevel != null) {
                        html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-star-fill"></i></div> <div class="contentBox"><span>Difficulty Level</span><p>' + parseData.origSupplierResponse.product.difficultyLevel + '</p><div></li>';
                    }
                    var durationHtml = fnSetProductMinAndMaxDuration(parseData.origSupplierResponse.product.minDuration, parseData.origSupplierResponse.product.maxDuration);
                    if (durationHtml != '') {
                        html += '<li class="col-md-4 col-6"><div class="iconBox"><i class="bi bi-stopwatch"></i></div> <div class="contentBox"><span>Duration</span><p>' + durationHtml + '</p><div></li>';
                    }
                    html += '</ul>';
                    html += '</div>';
                    html += '<div class="accordion">';
                    html += '<div class="accordion-head"><h4>About</h4><div class="arrow down"></div></div><div class="accordion-body">' + parseData.origSupplierResponse.product.description + '</div>';
                    var groupByContentListNodes = [];
                    parseData.origSupplierResponse.product.contentList.nodes.forEach(function (a) {
                        if (!this[a.type]) {
                            this[a.type] = { type: a.type, name: ((a.name == null || a.name == '') ? '<ul><li>' + a.description + '</li>' : (a.name != '' && a.name != null && a.description != '' && a.description != null) ? '<li> <strong>' + a.name + ' </strong>' + a.description + '</li>' : '<li>' + a.name + '</li>') };
                            groupByContentListNodes.push(this[a.type]);
                        } else {
                            this[a.type].name += ((a.name == null || a.name == '') ? '<li>' + a.description + '</li>' : (a.name != '' && a.name != null && a.description != '' && a.description != null) ? '<li> <strong>' + a.name + ' </strong>' + a.description + '</li>' : '<li>' + a.name + '</li></ul>');
                        }
                    }, Object.create(null));
                    for (var l = 0; l < groupByContentListNodes.length; l++) {
                        html += '<div class="accordion-head"><h4>' + fnCamelCase(groupByContentListNodes[l].type) + '</h4><div class="arrow down"></div></div>';
                        html += '<div class="accordion-body">' + groupByContentListNodes[l].name + '</div>';
                    }
                    if (guideLanguages != '') {
                        html += '<div class="accordion-head"><h4>Guide Language</h4><div class="arrow down"></div></div>';
                        html += '<div class="accordion-body">' + guideLanguages + '</div>';
                    }
                    if (parseData.origSupplierResponse.product.hasStartTime) {
                        html += '<div class="accordion-head"><h4>Start Time</h4><div class="arrow down"></div></div>';
                        html += '<div class="accordion-body">';
                        html += '<ul class="moreExp1">';
                        if (parseData.origSupplierResponse.product.startTimeList != null && parseData.origSupplierResponse.product.startTimeList.nodes != null) {
                            for (var p = 0; p < parseData.origSupplierResponse.product.startTimeList.nodes.length; p++) {
                                html += '<li class="timerBlk"><span class="timeIcn"><i class="bi bi-stopwatch"></i></span> <div class="contentBox"><div class="fLeft"><span>Duration</span> <p>' + fnSetProductMinAndMaxDuration(parseData.origSupplierResponse.product.startTimeList.nodes[p].duration, parseData.origSupplierResponse.product.startTimeList.nodes[p].duration) + '</p></div><div class="fRight"><span>Start Time</span><p> ' + parseData.origSupplierResponse.product.startTimeList.nodes[p].startTime + '</p></div></div></li>';
                            }
                        }
                        html += '</ul>';
                        html += '</div>';
                    }
                    var cancellationPolicyHtml = '';
                    if (parseData.origSupplierResponse.product.cancellationPolicy.hasFreeCancellation || parseData.origSupplierResponse.product.cancellationPolicy.isCancellable) {
                        html += '<div class="accordion-head"><h4>Cancellation Policy</h4><div class="arrow down"></div></div>';
                        for (var m = 0; m < parseData.origSupplierResponse.product.cancellationPolicy.penaltyList.nodes.length; m++) {
                            cancellationPolicyHtml += '<li>' + parseData.origSupplierResponse.product.cancellationPolicy.penaltyList.nodes[m].formattedText + '</li>';
                        }
                        html += '<div class="accordion-body">' + cancellationPolicyHtml + '</div>';
                    }
                    else {
                        html += '<div class="accordion-head"><h4>Cancellation Policy</h4><div class="arrow down"></div></div>';
                        cancellationPolicyHtml += '<li><b>This product can not be cancelled, amended or refunded.</b></li>';
                        html += '<div class="accordion-body">' + cancellationPolicyHtml + '</div>';
                    }
                    html += '</div>';
                    html += '</div></div>';
                    $('#divExperienceProductDetails').empty().html(html);

                    fnCallCarousel();
                } else {
                    html += "<span>No product details found.</span>";
                    $('#divExperienceProductDetails').empty().html(html);
                }
            } catch (e) {
            }
        }
        function fnCamelCase(str) {
            str = str.toLowerCase().split('_');
            for (var i = 0; i < str.length; i++) {
                str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
            }
            return str.join(' ');
        }
        function fnAccordianSetup() {
            //Accordian
            $('.accordion').each(function () {
                var $accordian = $(this);
                $accordian.find('.accordion-head').on('click', function () {
                    $(this).parent().find(".accordion-head").removeClass('open close');
                    $(this).removeClass('open').addClass('close');
                    $accordian.find('.accordion-body').slideUp();
                    if (!$(this).next().is(':visible')) {
                        $(this).removeClass('close').addClass('open');
                        $(this).next().slideDown();
                    }
                });
            });
        }
        function fnSetProductMinAndMaxDuration(minDuration, maxDuration) {
            var html = '';
            try {
                if (minDuration != null && maxDuration != null && minDuration != 'P0D' && maxDuration != 'P0D' && minDuration != maxDuration) {
                    html = minDuration + ' - ' + maxDuration;
                } else if (minDuration != null && maxDuration != null && minDuration != 'P0D' && maxDuration != 'P0D' && minDuration == maxDuration) {
                    html = minDuration;
                }
                if (html != '') {
                    html = html.replace(/PT/g, '');
                    html = html.replace(/P/g, '');
                    html = html.replace(/T/g, '');
                    html = html.replace(/H/g, ' hours ');
                    html = html.replace(/M/g, ' minutes');
                    html = html.replace(/D/g, ' day');
                }
            } catch (e) {
            }
            return html;
        }
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
        function fnClrFields() {
            $('#divBookingAvailabilityInput').empty();
            $('#divBookingErrorMsg').hide();
            $('#divBookingErrorMsg').empty();
        }
        function fnGetExperienceProductAvailability(availabilityId) {
            try {
                fnClrFields();
                //fnShowLoader('divBookingAvailabilityInput');
                var arrData = {};
                arrData.pstrAvailabilityId = availabilityId;
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductDetails.aspx/FetchAvailability',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    cache: false,
                    success: function (rtnData) {
                        fnBindOptionAndPricePricingCategoryInputList(rtnData.d);
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnBindOptionAndPricePricingCategoryInputList(data) {
            try {
                if (data != '') {
                    var parseData = JSON.parse(data);
                    availabilityId = parseData.id;
                    var html = '';
                    if (parseData.optionList.nodes != null && parseData.optionList.nodes.length > 0) {
                        for (var i = 0; i < parseData.optionList.nodes.length; i++) {
                            if (parseData.optionList.nodes[i].answerValue == null) {
                                if (parseData.optionList.nodes[i].dataType.toLowerCase() == 'options') {
                                    if (parseData.optionList.nodes[i].availableOptions.length > 0) {
                                        html += '<div><label>' + parseData.optionList.nodes[i].label + '</label>: ';
                                        html += '<select onchange=\"fnFetchAvailabilityWithOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" id="slc' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '">';
                                        html += '<option value=\"\">Select</option>';
                                        for (j = 0; j < parseData.optionList.nodes[i].availableOptions.length; j++) {
                                            html += '<option value=\"' + parseData.optionList.nodes[i].availableOptions[j].value + '\">' + parseData.optionList.nodes[i].availableOptions[j].label + ' </option>';
                                        }
                                        html += '</select><div>';
                                    }
                                } else if (parseData.optionList.nodes[i].dataType.toLowerCase() == 'boolean') {
                                    html += '<div><label>' + parseData.optionList.nodes[i].label + '</label>: ';
                                    html += '<input Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" onclick=\"fnFetchAvailabilityWithOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" type="radio" id="rdl_Yes_' + parseData.optionList.nodes[i].id + '" name="rdo_' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '" value="yes">';
                                    html += '<label for="rdl_Yes_' + parseData.optionList.nodes[i].id + '">Yes</label>';
                                    html += '<input Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" onclick=\"fnFetchAvailabilityWithOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" type="radio" id="rdl_No_' + parseData.optionList.nodes[i].id + '" name="rdo_' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '" value="no">';
                                    html += '<label for="rdl_No_' + parseData.optionList.nodes[i].id + '">No</label>';
                                    html += '<div>';
                                } else if (parseData.optionList.nodes[i].dataType.toLowerCase() == 'text') {
                                    html += '<div><label>' + parseData.optionList.nodes[i].label + '</label>: ';
                                    html += '<input type="text" onkeypress="return fnTextBoxInputeKeyPress(event,\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');" Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" id="txt' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '">';
                                    html += '<div>';
                                }
                            }
                            else if (parseData.optionList.nodes[i].answerValue != null) {
                                if (parseData.optionList.nodes[i].dataType.toLowerCase() == 'options') {
                                    html += '<div><label>' + parseData.optionList.nodes[i].label + '</label>: ';
                                    html += '<select style=\"display: none;\" onchange=\"fnFetchAvailabilityWithOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" id="slc' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '">';
                                    for (j = 0; j < parseData.optionList.nodes[i].availableOptions.length; j++) {
                                        html += '<option ' + (parseData.optionList.nodes[i].availableOptions[j].value == parseData.optionList.nodes[i].answerValue ? "Selected=\"Selected\"" : "") + ' value=\"' + parseData.optionList.nodes[i].availableOptions[j].value + '\">' + parseData.optionList.nodes[i].availableOptions[j].label + ' </option>';
                                    }
                                    html += '</select>';
                                    html += '<label id="lbl' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '">' + parseData.optionList.nodes[i].answerFormattedText + '</label>';
                                    if (parseData.optionList.nodes[i].availableOptions.length > 1) {
                                        html += '<div class="editBtn"><i class="bi bi-pencil-fill" id=\"btnEdit' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\" onclick=\"fnEditOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" ></i></div>';
                                    }
                                    html += '</div >';
                                } else if (parseData.optionList.nodes[i].dataType.toLowerCase() == 'boolean') {
                                    html += '<div><label>' + parseData.optionList.nodes[i].label + '</label>: ';
                                    html += '<div style=\"display: none;\" id=\"divRdo' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\"><input Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" onclick=\"fnFetchAvailabilityWithOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" type="radio" id="rdl_Yes_' + parseData.optionList.nodes[i].id + '" name="rdo_' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '" value="yes">';
                                    html += '<label for="rdl_Yes_' + parseData.optionList.nodes[i].id + '">Yes</label>';
                                    html += '<input Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" onclick=\"fnFetchAvailabilityWithOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" type="radio" id="rdl_No_' + parseData.optionList.nodes[i].id + '" name="rdo_' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '" value="no">';
                                    html += '<label for="rdl_No_' + parseData.optionList.nodes[i].id + '">No</label></div>';
                                    html += '<label id="lbl' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '">' + (parseInt(parseData.optionList.nodes[i].answerValue) == 1 ? "Yes" : "No") + '</label>';
                                    if (parseData.optionList.nodes[i].answerFormattedText.length > 1) {
                                        html += '<div class="editBtn"><i class="bi bi-pencil-fill" id=\"btnEdit' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\" onclick=\"fnEditOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" ></i></div>';
                                    }
                                    html += '<div>';
                                } else if (parseData.optionList.nodes[i].dataType.toLowerCase() == 'text') {
                                    html += '<div><label>' + parseData.optionList.nodes[i].label + '</label>: ';
                                    html += '<div style=\"display: none;\" id=\"divTxt' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\"><input type="text" value="' + parseData.optionList.nodes[i].answerFormattedText + '" onkeypress="return fnTextBoxInputeKeyPress(event,\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');" Experience' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + 'InputId=\"' + parseData.optionList.nodes[i].id + '\" id="txt' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '"></div>';
                                    html += '<label id="lbl' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '">' + parseData.optionList.nodes[i].answerFormattedText + '</label>';
                                    if (parseData.optionList.nodes[i].answerFormattedText.length > 1) {
                                        html += '<div class="editBtn"><i class="bi bi-pencil-fill" id=\"btnEdit' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\" onclick=\"fnEditOptionList(\'' + parseData.optionList.nodes[i].label.replace(/\s+/g, '') + '\',\'' + parseData.optionList.nodes[i].dataType.toLowerCase() + '\');\" ></i></div>';
                                    }
                                    html += '<div>';
                                }
                            }
                        }
                    }
                    if (parseData.pricingCategoryList.nodes != null && parseData.pricingCategoryList.nodes.length > 0) {
                        var nodesUnitsCount = 0;
                        for (var k = 0; k < parseData.pricingCategoryList.nodes.length; k++) {
                            html += '<div class="borderBox switch"><label id="lbl' + parseData.pricingCategoryList.nodes[k].id + '">' + parseData.pricingCategoryList.nodes[k].label.replace('(price per person)', '') + '</label>';
                            html += '<br/>';
                            //html += '<span>' + parseData.pricingCategoryList.nodes[k].unitPrice.grossFormattedText + '</span>';
                            html += '<div class="switcher"><span class="minusIcon" ' + (parseData.pricingCategoryList.nodes[k].units > 0 ? '' : 'disabled="disabled"') + '  id=\"spnSub' + parseData.pricingCategoryList.nodes[k].id + '\" onclick=\"fnSubtractValue(\'' + parseData.pricingCategoryList.nodes[k].id + '\');\">-</span>';
                            if (parseData.pricingCategoryList.nodes[k].minParticipants == null || parseData.pricingCategoryList.nodes[k].minParticipants == 0) {
                                parseData.pricingCategoryList.nodes[k].minParticipants = 1;
                            }
                            if (parseData.pricingCategoryList.nodes[k].maxParticipants == null) {
                                parseData.pricingCategoryList.nodes[k].maxParticipants = 999;
                            }
                            html += '<input type=\"text\" value=\"' + parseData.pricingCategoryList.nodes[k].units + '\" onpaste=\"return false\" oncut=\"return false\" class=\"pricingcategorylistinput inputBox\"  id=\"' + parseData.pricingCategoryList.nodes[k].id + '\" readonly=\"readonly\" minlength=\"' + parseData.pricingCategoryList.nodes[k].minParticipants + '\" maxlength=\"' + parseData.pricingCategoryList.nodes[k].maxParticipants + '\"/>';
                            html += '<span class="addIcon" ' + (parseData.pricingCategoryList.nodes[k].minParticipants == 0 || parseData.pricingCategoryList.nodes[k].maxParticipants == parseData.pricingCategoryList.nodes[k].units ? 'disabled="disabled"' : '') + ' id=\"spnAdd' + parseData.pricingCategoryList.nodes[k].id + '\" onclick=\"fnAddValue(\'' + parseData.pricingCategoryList.nodes[k].id + '\');\">+</span></div>';
                            var spnMinMaxPersonMsg = ''
                            if (parseData.pricingCategoryList.nodes[k].units > 0 && parseData.pricingCategoryList.nodes[k].minParticipants == parseData.pricingCategoryList.nodes[k].units) {
                                spnMinMaxPersonMsg += 'Minimum: ' + parseData.pricingCategoryList.nodes[k].units;
                            }
                            if (parseData.pricingCategoryList.nodes[k].units > 0 && parseData.pricingCategoryList.nodes[k].maxParticipants == parseData.pricingCategoryList.nodes[k].units) {
                                spnMinMaxPersonMsg += 'Maximum: ' + parseData.pricingCategoryList.nodes[k].units;
                            }
                            html += '<span id="spnMinMaxPersonMsg' + parseData.pricingCategoryList.nodes[k].id + '">' + spnMinMaxPersonMsg + '</span>';
                            if (parseData.pricingCategoryList.nodes[k].discountsAvailable) {
                                html += '<span class="txtNew">This category has group discounts.</span>';
                            }
                            if (parseData.pricingCategoryList.nodes[k].isDiscounted) {
                                html += '<span class="txtNew">Group discount applied.</span>';//Total savings: <b>' + parseData.pricingCategoryList.nodes[k].totalDiscountApplied.grossFormattedText + '</b>
                            }
                            html += '</div>';
                            nodesUnitsCount += parseInt(parseData.pricingCategoryList.nodes[k].units);
                        }
                        html += '<div>';
                        html += '<br/>';
                        html += '<div class="textTotal">Total: <span id=\"spnTotalPrice\"> ' + parseData.pricingCategoryList.totalPrice.grossFormattedText + ' </span></div>';
                        html += '<div class="textErrorMsg" style="display: none;"><span id=\"spnErrorMsg\"> </span></div>';
                        html += '</div>';
                        if (nodesUnitsCount > 0 && parseInt(parseData.pricingCategoryList.totalPrice.gross) > 0) {
                            html += '<button class="btn btn-yellow w-100 mt-2" id=\"btnAddToBooking\" style=\"display: block;\" type=\"button\" value=\"Add To Booking\" onclick=\"fnCreateBooking();\">Add to Booking</button>';
                        }
                    }
                    $('#divBookingAvailabilityInput').empty().html(html);
                    $('#divBookingAvailabilityInput').show();
                } else {
                    $('#divBookingAvailabilityInput').empty();
                    $('#divBookingErrorMsg').empty().html("We're sorry, there is no availability at this time.");
                    $('#divBookingErrorMsg').show();
                }
            } catch (e) {
            }
        }
        function fnTextBoxInputeKeyPress(e, nodeLabel, dataType) {
            // look for window.event in case event isn't passed in
            e = e || e.event;
            if (e.keyCode == 13) {
                if ($('#txt' + nodeLabel).val().length > 3) {
                    fnFetchAvailabilityWithOptionList(nodeLabel, dataType);
                }
                return false;
            }
            return true;
        }
        function fnEditOptionList(lableName, dataType) {
            try {
                $('#lbl' + lableName).hide();
                if (dataType == 'options') {
                    $('#slc' + lableName).show();
                } else if (dataType == 'boolean') {
                    $('#divRdo' + lableName).show();
                }
                else if (dataType == 'text') {
                    $('#divTxt' + lableName).show();
                }
                $('#btnEdit' + lableName).hide();
            } catch (e) {
            }
        }
        function fnFetchAvailabilityWithOptionList(nodeLabel, dataType) {
            try {
                var arrData = {};
                arrData.pstrAvailabilityId = availabilityId;
                if (dataType == 'options') {
                    arrData.pstrInputId = $('#slc' + nodeLabel + '').attr('Experience' + nodeLabel + 'InputId');
                    arrData.pstrInputValue = $('#slc' + nodeLabel + ' option:selected').val();
                } else if (dataType == 'boolean') {
                    arrData.pstrInputId = document.querySelector('input[name="rdo_' + nodeLabel + '"]:checked').attributes['experience' + nodeLabel.toLowerCase() + 'inputid'].nodeValue;
                    arrData.pstrInputValue = document.querySelector('input[name="rdo_' + nodeLabel + '"]:checked').value;
                } else if (dataType == 'text') {
                    arrData.pstrInputId = $('#txt' + nodeLabel).attr('Experience' + nodeLabel + 'InputId');
                    arrData.pstrInputValue = $('#txt' + nodeLabel).val();
                }
                arrData.pstrRequestType = "optionlist";
               // fnShowLoader('divBookingAvailabilityInput');
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductDetails.aspx/FetchAvailabilityWithOptionList',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    cache: false,
                    success: function (rtnData) {
                        fnBindOptionAndPricePricingCategoryInputList(rtnData.d);
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnAddValue(inputId) {
            try {
                $('#spnMinMaxPersonMsg' + inputId).empty();
                var inputCurrentVal = parseInt($('#' + inputId).val());
                var inputMinVal = parseInt($('#' + inputId).attr('minlength'));
                var inputMaxVal = parseInt($('#' + inputId).attr('maxlength'));
                if (inputCurrentVal < inputMaxVal) {
                    if (inputCurrentVal >= inputMinVal) {
                        $('#' + inputId).val(inputCurrentVal + 1);
                    } else {
                        $('#' + inputId).val(inputCurrentVal + inputMinVal);
                    }
                    fnFetchTotalPrice();                   
                } else {
                    $('#spnAdd' + inputId).attr("disabled", "disabled")
                }
            } catch (e) {
            }
        }
        function fnSubtractValue(inputId) {
            try {
                $('#spnMinMaxPersonMsg' + inputId).empty();
                var inputCurrentVal = parseInt($('#' + inputId).val());
                var inputMinVal = parseInt($('#' + inputId).attr('minlength'));
                var inputMaxVal = parseInt($('#' + inputId).attr('maxlength'));
                if (inputCurrentVal > 0 && inputCurrentVal <= inputMaxVal) {
                    if (inputCurrentVal == inputMinVal) {
                        $('#' + inputId).val(inputCurrentVal - inputMinVal);
                    } else {
                        $('#' + inputId).val(inputCurrentVal - 1);
                    }
                    fnFetchTotalPrice();
                } else {
                    $('#spnSub' + inputId).attr("disabled", "disabled");
                }
            } catch (e) {
            }
        }
        function fnFetchTotalPrice() {
            try {
                $('#divBookingErrorMsg').empty();
                $('#divBookingErrorMsg').hide();
                $('#btnAddToBooking').hide();
                $('#btnAddToBooking').removeAttr('onclick');
                var arrData = {};
                arrData.pstrAvailabilityId = availabilityId;
                var arrPricingCategoryInputListWithValue = [];
                $(".pricingcategorylistinput").each(function () {
                    arrPricingCategoryInputListWithValue.push({
                        id: this.id,
                        value: this.value
                    });
                });
                arrData.plstobjFetchAvailabilityWithPricingCategoryOptionList = arrPricingCategoryInputListWithValue;
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductDetails.aspx/FetchAvailabilityWithPricingCategory',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    async: true,
                    cache: false,
                    success: function (rtnData) {
                        if (rtnData.d != '') {
                            fnBindOptionAndPricePricingCategoryInputList(rtnData.d);
                            var parseData = JSON.parse(rtnData.d);
                            $('#spnTotalPrice').empty().html(parseData.pricingCategoryList.totalPrice.grossFormattedText);
                            var countOfNodesUnit = 0;
                            for (var i = 0; i < parseData.pricingCategoryList.nodes.length; i++) {
                                countOfNodesUnit += parseInt(parseData.pricingCategoryList.nodes[i].units);
                            }
                            if (countOfNodesUnit > 0 && parseInt(parseData.pricingCategoryList.totalPrice.gross) > 0) {
                                CheckAvailability(parseInt(parseData.pricingCategoryList.totalPrice.gross)); 
                            }
                            var errordiv = document.getElementById('CP_divEmailErrorMsg').style.display;
                            if (errordiv == "block") {
                                $('#btnAddToBooking').hide();
                                $('#btnAddToBooking').removeAttr('onclick');
                            }
                            else {
                                $('#btnAddToBooking').show();
                                $('#btnAddToBooking').attr('onclick', 'fnCreateBooking();');
                            }
                        }
                    },
                    error: function (errmsg) {
                    },
                    beforeSend: function () {
                        $("#updProgress").show();
                    }
                });
            } catch (e) {
            }
        }
        function fnCreateBooking() {
            try {
                var arrData = {};
                $('#btnAddToBooking').attr('disabled', 'disabled');
                arrData.pstrAvailabilityId = availabilityId;
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductDetails.aspx/CreateBooking',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    cache: false,
                    success: function (rtnData) {
                        if (rtnData.d != '') {
                            window.location.href = "ExperienceProductBooking.aspx?Id=" + rtnData.d;
                        } else {
                            $("#btnAddToBooking").removeAttr('disabled');

                            $('#divBookingErrorMsg').empty().html("Something went wrong.");
                            $('#divBookingErrorMsg').show();
                        }
                    },
                    error: function (errmsg) {
                    },
                    beforeSend: function () {
                        $("#updProgress").show();
                    }
                });

            } catch (e) {
            }
        }
        function fnCallCarousel() {
            $('#myCarousel').carousel({
                interval: false
            });
            $('#carousel-thumbs').carousel({
                interval: false
            });

            // handles the carousel thumbnails
            // https://stackoverflow.com/questions/25752187/bootstrap-carousel-with-thumbnails-multiple-carousel
            $('[id^=carousel-selector-]').click(function () {
                var id_selector = $(this).attr('id');
                var id = parseInt(id_selector.substr(id_selector.lastIndexOf('-') + 1));
                $('#myCarousel').carousel(id);
            });
            // Only display 3 items in nav on mobile.
            if ($(window).width() < 575) {
                $('#carousel-thumbs .row div:nth-child(4)').each(function () {
                    var rowBoundary = $(this);
                    $('<div class="row mx-0">').insertAfter(rowBoundary.parent()).append(rowBoundary.nextAll().addBack());
                });
                $('#carousel-thumbs .carousel-item .row:nth-child(even)').each(function () {
                    var boundary = $(this);
                    $('<div class="carousel-item">').insertAfter(boundary.parent()).append(boundary.nextAll().addBack());
                });
            }
            // Hide slide arrows if too few items.
            if ($('#carousel-thumbs .carousel-item').length < 2) {
                $('#carousel-thumbs [class^=carousel-control-]').remove();
                $('.machine-carousel-container #carousel-thumbs').css('padding', '0 5px');
            }
            // when the carousel slides, auto update
            $('#myCarousel').on('slide.bs.carousel', function (e) {
                var id = parseInt($(e.relatedTarget).attr('data-slide-number'));
                $('[id^=carousel-selector-]').removeClass('selected');
                $('[id=carousel-selector-' + id + ']').addClass('selected');
            });
            // when user swipes, go next or previous
            $('#myCarousel').swipe({
                fallbackToMouseEvents: true,
                swipeLeft: function (e) {
                    $('#myCarousel').carousel('next');
                },
                swipeRight: function (e) {
                    $('#myCarousel').carousel('prev');
                },
                allowPageScroll: 'vertical',
                preventDefaultEvents: false,
                threshold: 75
            });
            $('#myCarousel .carousel-item img').on('click', function (e) {
                var src = $(e.target).attr('data-remote');
                if (src) $(this).ekkoLightbox();
            });
        }
        function fnFormatDate(date) {
            // Create a date object from a date string
            //var date = new Date("Wed, 04 May 2022");
            // Get year, month, and day part from the date
            var year = date.toLocaleString("default", { year: "numeric" });
            var month = date.toLocaleString("default", { month: "2-digit" });
            var day = date.toLocaleString("default", { day: "2-digit" });
            // Generate yyyy-mm-dd date string
            return year + "-" + month + "-" + day;
        }
        function CheckAvailability(amount) {
            $.ajax({
                type: "POST",
                url: "ExperienceProductDetails.aspx/CheckAvailability",
                data: "{pntamount:" + amount + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        $('#btnAddToBooking').show();
                        $('#btnAddToBooking').attr('onclick', 'fnCreateBooking();');
                        $('.textErrorMsg').hide();
                        return true;
                    }
                    else {
                        $('#btnAddToBooking').hide();
                        $('#btnAddToBooking').removeAttr('onclick');
                        $('.textErrorMsg').show();
                        $('#spnErrorMsg').html("<p style='color: red;'>Insufficient Points.</p>");
                        return false;
                    }
                }
            });
            return false;
        }
    </script>
</asp:Content>

