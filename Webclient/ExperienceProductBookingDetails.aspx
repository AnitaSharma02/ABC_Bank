<%@ Page Title="Experience Product Booking Details" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductBookingDetails.aspx.cs" Inherits="ExperienceProductBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="css/experiences.css">
    <style>
        /*body{
            padding-bottom:100px;
        }*/
        #divSearchCart {
            display: none !important;
        }

        #updProgress {
            display: none !important;
        }
    </style>
    <script type="text/javascript">
      /*  document.getElementById("pageName").innerHTML = "Experience";*/
        $(document).ready(function () {
            fnGetExperienceProductBookingDetails();
        });
        function fnGetExperienceProductBookingDetails() {
            try {
                var arrData = {};
                arrData.pstrBookId = getQuerystring('Id');
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductBookingDetails.aspx/GetProductBookingDetails',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    success: function (rtnData) {
                        fnBindExperienceProductDetails(rtnData.d);
                    },
                    beforeSend: function () {
                        $('.myexperiences').hide();
                        $('.spin-loader').show();
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnCamelCase(str) {
            str = str.toLowerCase().split(' ');
            for (var i = 0; i < str.length; i++) {
                str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
            }
            return str.join(' ');
        }
        function fnBindExperienceProductDetails(data) {
            try {
                if (data != '') {
                    var parseData = JSON.parse(data);
                    if (parseData.status.toLowerCase() == 'success' && parseData.data != null) {
                        $('#spnLeadPersonName').empty().html(fnCamelCase(parseData.data.leadPassengerName));
                        $('#spnBookingReference').empty().html(parseData.data.code);
                        $('#spnBookingStatus').empty().html(parseData.data.state);
                        if (parseData.data.state.toLowerCase() == 'confirmed') {
                            $('#spnBookingStatus').addClass('bookingconfirmed');
                        } else {
                            $('#spnBookingStatus').addClass('bookingfailed');
                        }
                        var availabilityListNodeDetails = parseData.data.rawData.data.booking.availabilityList.nodes[0];
                        var productDetails = availabilityListNodeDetails.product;
                        $('#imgProductImage').attr('src', productDetails.imageList[0].urlLarge);
                        $('#spnProductName').empty().html(productDetails.name);
                        $('#pTotalPoints').empty().html(availabilityListNodeDetails.totalPrice.grossFormattedText);
                        var bookingDate = new Date(availabilityListNodeDetails.date);
                        var ulProductBookingDetailsHtml = '';
                        ulProductBookingDetailsHtml += '<li><p>Date: <span>' + bookingDate.toDateString() + '</span></p></li>';
                        for (var l = 0; l < availabilityListNodeDetails.optionList.nodes.length; l++) {
                            ulProductBookingDetailsHtml += '<li><p>' + availabilityListNodeDetails.optionList.nodes[l].label + ': <span>' + availabilityListNodeDetails.optionList.nodes[l].answerFormattedText + '</span></p></li>';
                        }
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
                        for (var m = 0; m < groupByPersonListNodes.length; m++) {
                            ulProductBookingDetailsHtml += '<li><p>' + groupByPersonListNodes[m].pricingCategoryLabel + ': <span>' + groupByPersonListNodes[m].pricingCategoryCount + '</span></p></li>';
                        }
                        ulProductBookingDetailsHtml += '<li><p>Subtotal: <span>' + availabilityListNodeDetails.totalPrice.grossFormattedText + '</span></p></li>';
                        $('#ulProductBookingDetails').empty().html(ulProductBookingDetailsHtml);
                        var ulCancellationPolicyHtml = '';
                        for (var k = 0; k < productDetails.cancellationPolicy.penaltyList.nodes.length; k++) {
                            ulCancellationPolicyHtml += '<li><div class="iconBox"><i class="bi bi-slash-circle"></i></div> <div class="contentBox"> <p>' + productDetails.cancellationPolicy.penaltyList.nodes[k].formattedText + '</p><div></div></div></li>';
                        }
                        $('#ulCancellationPolicy').empty().html(ulCancellationPolicyHtml);
                        var divLeadPersonDetailsHtml = '<ul>';
                        var divParticipantDetailsHtml = '<ul>';
                        var questionListDetails = parseData.data.questionList;
                        var n = 0;
                        for (var i = 0; i < questionListDetails.length; i++) {
                            if (questionListDetails[i].properties.toLowerCase() == 'bookingpersonquestions') {
                                isParticipants = true;
                            }
                            if (questionListDetails[i].properties.toLowerCase() == 'bookingquestions') {
                                divLeadPersonDetailsHtml += '<li>';
                                if (questionListDetails[i].dataType.toLowerCase() == 'options') {
                                    $.each(questionListDetails[i].availableOptions, function (j, val) {
                                        if (val.value.toLowerCase() == questionListDetails[i].answerValue.toLowerCase()) {
                                            divLeadPersonDetailsHtml += '<p>' + questionListDetails[i].label + ': <span>' + val.label + '</span></p>';
                                        }
                                    });
                                } else {
                                    divLeadPersonDetailsHtml += '<p>' + questionListDetails[i].label + ': <span>' + questionListDetails[i].answerValue + '</span></p>';
                                }
                                divLeadPersonDetailsHtml += '</li>';
                            } else if (questionListDetails[i].properties.toLowerCase() == 'bookingavailabilityquestions') {
                                $('#divParticipants').show();
                                divParticipantDetailsHtml += '<li>';
                                divParticipantDetailsHtml += '<p> ' + (questionListDetails[i].properties.toLowerCase() == 'bookingpersonquestions' ? questionListDetails[i].pricingCategoryLabel + ' - ' + questionListDetails[i].label : questionListDetails[i].label) + ': <span>' + questionListDetails[i].answerValue + '</span></p>';
                                divParticipantDetailsHtml += '</li>';
                            }
                            else if (questionListDetails[i].properties.toLowerCase() == 'bookingpersonquestions') {
                                divParticipantDetailsHtml += '<li>';
                                if (isParticipants && questionListDetails[i].pricingCategoryLabel != null && questionListDetails[i].pricingCategoryLabel != questionListDetails[i - 1].pricingCategoryLabel) {
                                    n++;
                                    divParticipantDetailsHtml += '<label>Participants ' + n + ' - ' + questionListDetails[i].pricingCategoryLabel + '</label>';
                                }
                                else if (isParticipants && questionListDetails[i].pricingCategoryLabel != null && questionListDetails[i].pricingCategoryLabel == questionListDetails[i - 1].pricingCategoryLabel) {
                                    for (var p = 0; p < groupByPersonListNodes.length; p++) {
                                        if (groupByPersonListNodes[p].pricingCategoryLabel == questionListDetails[i].pricingCategoryLabel) {
                                            if (groupByPersonListNodes[p].labelName.split(',')[0] == questionListDetails[i].label) {
                                                n++;
                                                divParticipantDetailsHtml += '<label>Participants ' + n + ' - ' + questionListDetails[i].pricingCategoryLabel + '</label>';
                                            }
                                        }
                                    }
                                }
                                divParticipantDetailsHtml += '<p> ' + questionListDetails[i].label + ' : <span>' + questionListDetails[i].answerValue + '</span></p>';
                                divParticipantDetailsHtml += '</li>';
                            }
                        }
                        divLeadPersonDetailsHtml += '</ul>';
                        $('#divLeadPersonDetails').empty().html(divLeadPersonDetailsHtml);
                        divParticipantDetailsHtml += '</ul>';
                        $('#divParticipantDetails').empty().html(divParticipantDetailsHtml);
                        $('.myexperiences').show();
                        $('.spin-loader').hide();
                    }
                }
            } catch (e) {
            }
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
    </script>
    <div class="myexperiences" style="display: none;">
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="confirmMsg mt-5">
                        <p><i class="bi bi-check-circle-fill"></i>&nbsp;Congratulations your booking has been confirmed!</p>
                    </div>
                </div>
            </div>
            <div id="divBookingInput" class="row">
                <div class="col-md-5 col-sm-12">
                    <div class="bookingBox">
                        <h4>Booking</h4>
                        <div class="textBox">
                            <ul>
                                <li>
                                    <p>Lead Person Name: <span id="spnLeadPersonName"></span></p>
                                </li>
                                <li>
                                    <p>Holibob Booking Reference: <span id="spnBookingReference"></span></p>
                                </li>
                                <li>
                                    <p>Booking Status: <cite id="spnBookingStatus"></cite></p>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="bookingBox">
                        <h4>Lead Person Details</h4>
                        <div class="textBox" id="divLeadPersonDetails">
                        </div>
                    </div>
                    <div class="bookingBox" id="divParticipants" style="display: none;">
                        <h4>Participants</h4>
                        <div class="textBox" id="divParticipantDetails">
                        </div>
                    </div>
                </div>
                <div class="col-md-7 col-sm-12">
                    <div class="rightBox">
                        <div class="imgBox">
                            <img id="imgProductImage">
                        </div>
                        <div class="contBox">
                            <div class="textBox">
                                <h5>Summary</h5>
                                <span id="spnProductName"></span>
                                <ul id="ulProductBookingDetails">
                                </ul>
                                <div class="cancellation">
                                    <h6>Cancellation Policy</h6>
                                    <ul class="cancelDate" id="ulCancellationPolicy">
                                    </ul>
                                </div>
                            </div>
                            <br />
                            <div class="textBox">
                                <h5>Contact</h5>
                            </div>
                            <div class="completion">
                                <a href="javascript:void(0)"><i class="bi bi-envelope-fill"></i>&nbsp;giiftclubsupport@giift.com</a><br />
                                <span style="display: none;"><i class="bi bi-telephone-fill"></i>&nbsp;###</span>
                            </div>

                            <br />
                            <div class="textBox">
                                <h5>Completion</h5>
                            </div>

                            <span>Your booking is complete - enjoy the experience!</span>
                            <br />
                            <div class="completion">
                                <p>Total Amount:</p>
                                <p id="pTotalPoints"></p>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="spin-loader">
        <img class="spin" width="50" src="Images/loader/spinner-2.gif" alt="" />
    </div>
</asp:Content>

