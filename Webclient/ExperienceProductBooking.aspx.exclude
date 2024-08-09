<%@ Page Title="Experience Product Booking" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductBooking.aspx.cs" Inherits="ExperienceProductBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="css/experiences.css">

    <link rel="stylesheet" href="Css/jquery.ui.timepicker.css" />
    <script type="text/javascript" src="Jquery/jquery.timepicker.js"></script>

    <style type="text/css">
        .ui-widget.ui-widget-content {
            height: auto !important;
        }
        /*   #updProgress {
            display: none !important;
        }*/

        .col-md-5.col-sm-12 input {
            margin: 5px 0 15px 0 !important;
        }

        .col-md-5.col-sm-12 select {
            margin: 5px 0 15px 0 !important;
        }

        .main {
            padding: 0;
            margin: 25px 0 0 0;
            min-height: 200px;
            margin-top: 105px !important;
        }

        #divSearchCart {
            display: none !important;
        }

        ::-webkit-input-placeholder { /* Chrome/Opera/Safari */
            font-size: 13px !important;
        }

        ::-moz-placeholder { /* Firefox 19+ */
            font-size: 13px !important;
        }

        :-ms-input-placeholder { /* IE 10+ */
            font-size: 13px !important;
        }

        :-moz-placeholder { /* Firefox 18- */
            font-size: 13px !important;
        }

        input[type='checkbox'] {
            -webkit-font-smoothing: antialiased;
            text-rendering: optimizeSpeed;
            width: 16px;
            height: 16px;
            margin: 0;
            margin-right: 5px;
            display: block;
            float: left;
            cursor: pointer;
            appearance: none;
            border: 1px solid #CCCCCC !important;
            padding: 0 !important;
        }

        /* Style the label text */
        .checkArea label {
            padding: 0 0 0 10px !important;
        }

        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <input type="hidden" id="hdnPIntAvailablePoints" value="<%= pintAvailablePoints %>" />
    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item active" data-i18n="navigation-exp-prod-book">Experience Product Booking</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="MyExperiences">
        <div class="container pt-4">
            <div id="divBookingInput" class="row">
                <div class="spin-loader">
                    <img class="spin" width="50" src="Images/loader/spinner-2.gif" alt="" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        /* document.getElementById("pageName").innerHTML = "Experience";*/
        var bookId = '';
        $(document).ready(function () {
            bookId = getQuerystring('Id');
            fnGetOrderStatus();
        });
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
        function fnGetOrderStatus() {
            try {
                var arrData = {};
                arrData.pstrBookId = bookId;
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductBooking.aspx/GetOrderStatus',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    cache: false,
                    success: function (rtnData) {
                        fnBindQuestionList(rtnData.d);
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnBindQuestionList(data) {
            try {
                if (data != '') {
                    var parseData = JSON.parse(data);
                    if (parseData.questionList.length > 0) {
                        var html = '';
                        html += '<div class="col-md-5 col-sm-12">';
                        html += '<div style="display:none;" class="bookingBox"><h4>Booking</h4>';
                        html += '<label>Lead Person Name</label>';
                        html += '<input type=\"text\" readonly value=\"' + parseData.leadPassengerName + '\" /></div>';
                        var availabilityListNodeDetails = parseData.rawData.data.booking.availabilityList.nodes[0];
                        var productDetails = availabilityListNodeDetails.product;
                        var groupByPersonListNodes = [];
                        availabilityListNodeDetails.personList.nodes.forEach(function (a) {
                            if (!this[a.pricingCategoryLabel]) {
                                var labelName = '';
                                for (var o = 0; o < a.questionList.nodes.length; o++) {
                                    labelName += a.questionList.nodes[o].label + ",";
                                }
                                labelName = labelName.substring(0, labelName.lastIndexOf(","));
                                this[a.pricingCategoryLabel] = { pricingCategoryLabel: a.pricingCategoryLabel, pricingCategoryCount: 1, labelName: labelName };
                                groupByPersonListNodes.push(this[a.pricingCategoryLabel]);
                            } else {
                                this[a.pricingCategoryLabel].pricingCategoryCount = this[a.pricingCategoryLabel].pricingCategoryCount + 1;
                            }
                        }, Object.create(null));
                        var n = 0;
                        for (var i = 0; i < parseData.questionList.length; i++) {
                            var isParticipants = false;
                            if (i > 0 && parseData.questionList[i - 1].properties.toLowerCase() != 'bookingquestions' && parseData.questionList[i].properties.toLowerCase() == 'bookingquestions') {
                                html += '<div class="bookingBox"><h4>Lead Person Details</h4> ';
                            } else if (i == 0) {
                                html += '<div class="bookingBox"><h4>Lead Person Details</h4>';
                            }
                            else if (i > 0 && ((parseData.questionList[i - 1].properties.toLowerCase() != 'bookingpersonquestions' && parseData.questionList[i].properties.toLowerCase() == 'bookingpersonquestions') || (parseData.questionList[i - 1].properties.toLowerCase() != 'bookingavailabilityquestions' && parseData.questionList[i].properties.toLowerCase() == 'bookingavailabilityquestions'))) {
                                html += '</div>';
                                html += '<div class="bookingBox"><h4>Participants</h4>';
                            }
                            if (parseData.questionList[i].properties.toLowerCase() == 'bookingpersonquestions') {
                                isParticipants = true;
                            }
                            if (parseData.questionList[i].dataType != null) {
                                if (parseData.questionList[i].dataType.toLowerCase() == 'text') {
                                    html += '<div >';
                                    if (isParticipants && parseData.questionList[i].pricingCategoryLabel != parseData.questionList[i - 1].pricingCategoryLabel) {
                                        n++;
                                        html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                    }
                                    else if (isParticipants && parseData.questionList[i].pricingCategoryLabel == parseData.questionList[i - 1].pricingCategoryLabel) {
                                        for (var p = 0; p < groupByPersonListNodes.length; p++) {
                                            if (groupByPersonListNodes[p].pricingCategoryLabel == parseData.questionList[i].pricingCategoryLabel) {
                                                if (groupByPersonListNodes[p].labelName.split(',')[0] == parseData.questionList[i].label) {
                                                    n++;
                                                    html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                                }
                                            }
                                        }
                                    }
                                    else if (!isParticipants && parseData.questionList[i].label != null) {
                                        html += '<label>' + parseData.questionList[i].label + '</label>';
                                    }
                                    html += '<input onblur=\"fnValidateBookingInput();\" placeholder=\"' + (parseData.questionList[i].isRequired || (parseData.questionList[i].dataFormat != null && parseData.questionList[i].dataFormat.toLowerCase() == 'phone_number') ? parseData.questionList[i].label + ' *' : parseData.questionList[i].label) + '\" type=\"text\" onpaste=\"return false\" oncut=\"return false\" isRequired=\"' + (parseData.questionList[i].dataFormat != null && parseData.questionList[i].dataFormat.toLowerCase() == 'phone_number' ? true : parseData.questionList[i].isRequired) + '\" dataFormat=\"' + (parseData.questionList[i].dataFormat == null || parseData.questionList[i].dataFormat == '' ? 'alpha_numeric' : parseData.questionList[i].dataFormat.toLowerCase()) + '\" class=\"bookinginput txtinput\"  id=\"' + parseData.questionList[i].id + '\" />';
                                    if (parseData.questionList[i].dataFormat != null && parseData.questionList[i].dataFormat.toLowerCase() == 'phone_number') {
                                        html += '<span class=\"text-red\">Note: Please include country code e.g + 977 and do not include a leading zero</span>';
                                    }
                                    html += '</div>';
                                } else if (parseData.questionList[i].dataType.toLowerCase() == 'options') {
                                    html += '<div>';
                                    if (isParticipants && parseData.questionList[i].pricingCategoryLabel != parseData.questionList[i - 1].pricingCategoryLabel) {
                                        n++;
                                        html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                    }
                                    else if (isParticipants && parseData.questionList[i].pricingCategoryLabel == parseData.questionList[i - 1].pricingCategoryLabel) {
                                        for (var p = 0; p < groupByPersonListNodes.length; p++) {
                                            if (groupByPersonListNodes[p].pricingCategoryLabel == parseData.questionList[i].pricingCategoryLabel) {
                                                if (groupByPersonListNodes[p].labelName.split(',')[0] == parseData.questionList[i].label) {
                                                    n++;
                                                    html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                                }
                                            }
                                        }
                                    }
                                    else if (!isParticipants && parseData.questionList[i].label != null) {
                                        html += '<label>' + parseData.questionList[i].label + '</label>';
                                    }
                                    html += '<select onchange=\"fnValidateBookingInput();\" placeholder=\"' + (parseData.questionList[i].isRequired ? parseData.questionList[i].label + '*' : '') + '\" isRequired=\"' + parseData.questionList[i].isRequired + '\" class=\"bookinginput slcinput\" id=\"' + parseData.questionList[i].id + '\">';
                                    for (var j = 0; j < parseData.questionList[i].availableOptions.length; j++) {
                                        html += '<option value=\"' + parseData.questionList[i].availableOptions[j].value + '\">' + parseData.questionList[i].availableOptions[j].label + ' </option>';
                                    }
                                    html += '</select></div> ';
                                } else if (parseData.questionList[i].dataType.toLowerCase() == 'date') {
                                    html += '<div>';
                                    if (isParticipants && parseData.questionList[i].pricingCategoryLabel != parseData.questionList[i - 1].pricingCategoryLabel) {
                                        n++;
                                        html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                    }
                                    else if (isParticipants && parseData.questionList[i].pricingCategoryLabel == parseData.questionList[i - 1].pricingCategoryLabel) {
                                        for (var p = 0; p < groupByPersonListNodes.length; p++) {
                                            if (groupByPersonListNodes[p].pricingCategoryLabel == parseData.questionList[i].pricingCategoryLabel) {
                                                if (groupByPersonListNodes[p].labelName.split(',')[0] == parseData.questionList[i].label) {
                                                    n++;
                                                    html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                                }
                                            }
                                        }
                                    }
                                    else if (!isParticipants && parseData.questionList[i].label != null) {
                                        html += '<label>' + parseData.questionList[i].label + '</label>';
                                    }
                                    html += '<input onblur=\"fnValidateBookingInput();\" placeholder=\"' + (parseData.questionList[i].isRequired ? parseData.questionList[i].label + '*' : '') + '\" isRequired=\"' + parseData.questionList[i].isRequired + '\"  type=\"text\" onpaste=\"return false\" oncut=\"return false\" readonly class=\"bookinginput dtinput datepicker\"  id=\"' + parseData.questionList[i].id + '\" /></div>';
                                }
                                else if (parseData.questionList[i].dataType.toLowerCase() == 'time') {
                                    html += '<div>';
                                    if (isParticipants && parseData.questionList[i].pricingCategoryLabel != parseData.questionList[i - 1].pricingCategoryLabel) {
                                        n++;
                                        html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                    }
                                    else if (isParticipants && parseData.questionList[i].pricingCategoryLabel == parseData.questionList[i - 1].pricingCategoryLabel) {
                                        for (var p = 0; p < groupByPersonListNodes.length; p++) {
                                            if (groupByPersonListNodes[p].pricingCategoryLabel == parseData.questionList[i].pricingCategoryLabel) {
                                                if (groupByPersonListNodes[p].labelName.split(',')[0] == parseData.questionList[i].label) {
                                                    n++;
                                                    html += '<label>Participants ' + n + ' - ' + parseData.questionList[i].pricingCategoryLabel + '</label>';
                                                }
                                            }
                                        }
                                    }
                                    else if (!isParticipants && parseData.questionList[i].label != null) {
                                        html += '<label>' + parseData.questionList[i].label + '</label>';
                                    }
                                    html += '<input onblur=\"fnValidateBookingInput();\" placeholder=\"' + (parseData.questionList[i].isRequired ? parseData.questionList[i].label + '*' : '') + '\" isRequired=\"' + parseData.questionList[i].isRequired + '\"  type=\"text\" onpaste=\"return false\" readonly oncut=\"return false\" class=\"bookinginput tminput timepicker \"  id=\"' + parseData.questionList[i].id + '\" /></div>';
                                }
                            }
                        }
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                        html += '<div class="col-md-7 col-sm-12">';
                        html += '<div class="rightBox">';
                        html += '<div class="imgBox">';
                        html += '<img src="' + productDetails.imageList[0].urlLarge + '">';
                        html += '</div>';
                        html += '<div class="contBox   text-left">';
                        html += '<div class="textBox">';
                        html += '<h5>Summary</h5>';
                        html += '<span>' + productDetails.name + '</span>';
                        html += '<ul>';
                        var bookingDate = new Date(availabilityListNodeDetails.date);
                        html += '<li><p>Date: <span>' + bookingDate.toDateString() + '</span></p></li>';
                        for (var l = 0; l < availabilityListNodeDetails.optionList.nodes.length; l++) {
                            html += '<li><p>' + availabilityListNodeDetails.optionList.nodes[l].label + ': <span>' + availabilityListNodeDetails.optionList.nodes[l].answerFormattedText + '</span></p></li>';
                        }
                        for (var m = 0; m < groupByPersonListNodes.length; m++) {
                            html += '<li><p>' + groupByPersonListNodes[m].pricingCategoryLabel + ': <span>' + groupByPersonListNodes[m].pricingCategoryCount + '</span></p></li>';
                        }
                        html += '<li><p>Subtotal: <span>' + availabilityListNodeDetails.totalPrice.grossFormattedText + '</span></p></li>';
                        html += '</ul>';
                        html += '<div class="cancellation">';
                        html += '<h6>Cancellation Policy</h6>';
                        html += '<ul class="cancelDate">';
                        if (productDetails.cancellationPolicy.penaltyList.nodes.length > 0) {
                            for (var k = 0; k < productDetails.cancellationPolicy.penaltyList.nodes.length; k++) {
                                html += '<li><div class="iconBox"><i class="bi bi-slash-circle"></i></div> <div class="contentBox"> <p>' + productDetails.cancellationPolicy.penaltyList.nodes[k].formattedText + '</p><div></div></div></li>';
                            }
                        }
                        else {
                            html += '<li><div class="iconBox"><i class="bi bi-slash-circle"></i></div> <div class="contentBox"> <p><b>This product can not be cancelled, amended or refunded.</b></p><div></div></div></li>';
                        }

                        html += '</ul>'
                        html += '</div>';
                        html += '</div>';
                        html += '<br/>';
                        html += '<div class="textBox">';
                        html += '<h5>Completion</h5>';
                        html += '</div>';
                        html += '<span id=\"spnTAndCMsg\" class=\"text-red\">You just need to accept Holibob Terms and Conditions to continue.</span><br/>';
                        html += '<div class="checkArea">';
                        html += '<input type=\"checkbox\" id=\"chkTAndC\" onclick=\"fnValidateBookingInput();\" name=\"chkTAndC\" value=\"\"/><label>I accept the Terms and Conditions.</label><br/>';
                        html += '</div>';
                        html += '<button class=\"btn btn-yellow w-100\" id=\"btnCompleteBooking\" type=\"button\" disabled=\"disabled\" value=\"Complete Booking\">Complete Booking</button><br/>';
                        //html += '<span class=\"experienceerrormsg\">Please fill in all the required fields</span><br/>';
                        html += '<span class=\"experienceerrormsg\" id=\"spnCompleteBookingErrorMsg\" style="display:none;"></span>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                        $('#divBookingInput').empty().html(html);
                        //if (parseInt($('#hdnPIntAvailablePoints').val()) == 0 || (parseInt($('#hdnPIntAvailablePoints').val()) < parseInt(availabilityListNodeDetails.totalPrice.gross))) {
                        //    $('#spnCompleteBookingErrorMsg').empty().html('Insufficient Points.');
                        //    $('#spnCompleteBookingErrorMsg').show();
                        //}

                        fnBindDatePicker();
                    }
                }
            } catch (e) {
            }
        }
        var isValidPhoneNumber = phonenumber => {
            try {
                // Regex to check valid
                // International Phone Numbers
                let regex = new RegExp(/^[+]{1}(?:[0-9\-\(\)\/\.]\s?){6,15}[0-9]{1}$/);
                // if phonenumber
                // is empty return false
                if (phonenumber == null) {
                    return false;
                }
                // Return true if the phonenumber
                // matched the ReGex
                if (regex.test(phonenumber) == true) {
                    return true;
                }
                else {
                    return false;
                }
            } catch (e) { }
            return false;
        }
        function fnValidateBookingInput() {
            var rtnResponse = true;
            try {
                $(".bookinginput").each(function () {
                    $("#" + this.id).removeClass('experienceerror');
                    if ($("#" + this.id).hasClass("txtinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        var alphanumericRegex = /^[0-9a-zA-Z_]+$/;
                        if ($("#" + this.id).attr('dataformat') == 'email_address') {
                            if (this.value.replace(/\s+/g, '') == "" || (this.value.replace(/\s+/g, '') != "" && !fnIsValidEmailAddress(this.value))) {
                                $("#" + this.id).addClass('experienceerror');
                                rtnResponse = false;
                            }
                        }
                        if ($("#" + this.id).attr('dataformat') == 'phone_number' && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                            if (!$("#" + this.id).val().replace(/\s+/g, '').includes("+")) {
                                $("#" + this.id).val("+" + $("#" + this.id).val());
                            }
                            if (this.value.replace(/\s+/g, '') == '' || (this.value.replace(/\s+/g, '') != '' && this.value.length > 15) || (this.value.replace(/\s+/g, '') != '' && this.value.length < 15 && !isValidPhoneNumber(this.value))) {
                                $("#" + this.id).addClass('experienceerror');
                                rtnResponse = false;
                            }
                        }
                        if (($("#" + this.id).attr('dataformat') == 'alpha_numeric' || $("#" + this.id).attr('dataformat') == 'text') && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                            if (this.value.replace(/\s+/g, '') == '' || (this.value.replace(/\s+/g, '') != '' && !this.value.replace(/\s+/g, '').match(alphanumericRegex))) {
                                $("#" + this.id).addClass('experienceerror');
                                rtnResponse = false;
                            }
                        }
                    }
                    if ($("#" + this.id).hasClass("slcinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        if (this.value.replace(/\s+/g, '') == '') {
                            $("#" + this.id).addClass('experienceerror');
                            rtnResponse = false;
                        }
                    }
                    if ($("#" + this.id).hasClass("dtinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        if (this.value.replace(/\s+/g, '') == '') {
                            $("#" + this.id).addClass('experienceerror');
                            rtnResponse = false;
                        }
                    }
                    if ($("#" + this.id).hasClass("tminput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        if (this.value.replace(/\s+/g, '') == '') {
                            $("#" + this.id).addClass('experienceerror');
                            rtnResponse = false;
                        }
                    }
                });
                if (rtnResponse && $('#chkTAndC').prop("checked") == true) {
                    $('#spnTAndCMsg').empty().html("You're all set.");
                    $('#btnCompleteBooking').removeAttr('disabled');
                    $('#btnCompleteBooking').attr('onclick', 'fnSubmitBookingAnswer();');
                } else if ($('#chkTAndC').prop("checked") == true) {
                    $('#spnTAndCMsg').empty().html("Some of the required information is incomplete. Please ensure you have answered all questions. Once all the information is complete you will be able to complete the booking.");
                } else if ($('#chkTAndC').prop("checked") == false) {
                    $('#spnTAndCMsg').empty().html("You just need to accept Holibob Terms and Conditions to continue.");
                }
            } catch (e) {
            }
            return rtnResponse;
        }
        function fnIsValidEmailAddress(emailAddress) {
            var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
            return pattern.test(emailAddress);
        };
        function fnSubmitBookingAnswer() {
            try {
                $('#btnCompleteBooking').attr('disabled', 'disabled');

                if ($('#chkTAndC').prop("checked") == true) {

                    $("#updProgress").show();

                    $('#spnCompleteBookingErrorMsg').hide();
                    var arrBookingInput = [];
                    $(".bookinginput").each(function () {
                        arrBookingInput.push({
                            questionId: this.id,
                            value: this.value
                        });
                    });
                    var arrData = {};
                    arrData.pstrBookId = bookId;
                    arrData.plstobjAnswerList = arrBookingInput;
                    $.ajax({
                        type: 'POST',
                        url: 'ExperienceProductBooking.aspx/SubmitBookingAnswer',
                        contentType: 'application/json;',
                        dataType: 'json',
                        data: JSON.stringify(arrData),
                        cache: false,
                        success: function (rtnData) {
                            if (rtnData.d.includes(".aspx")) {
                                window.location.href = rtnData.d;
                            } else {
                                $("#updProgress").hide();

                                $('#btnCompleteBooking').removeAttr('disabled');
                                $('#spnCompleteBookingErrorMsg').empty().html(rtnData.d);
                                $('#spnCompleteBookingErrorMsg').show();
                            }
                        },
                        error: function (errmsg) {
                        }
                    });
                }
                else {
                    $('#spnTAndCMsg').empty().html("You just need to accept Holibob Terms and Conditions to continue.");
                }
            } catch (e) {
            }
        }
        function fnBindDatePicker() {
            $(".dtinput").datepicker({
                //maxDate: 0,
                numberOfMonths: 1,
                changeMonth: true,
                changeYear: true,
                //yearRange: "-90:-0",
                //maxDate: '-12Y',
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateText, inst) {
                }
            });
            var dt = new Date();
            $('.timepicker').timepicker({
                timeFormat: 'h:mm p',
                interval: 30,
                //minTime: '10',
                //maxTime: '6:00pm',
                defaultTime: dt.getHours().toString() + ":" + dt.getMinutes().toString(),
                //startTime: '10:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true
            });
        }
    </script>
</asp:Content>

