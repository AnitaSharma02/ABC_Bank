<%@ Page Title="Insurance List Details" Language="C#" MasterPageFile="SiteShopMaster.master" AutoEventWireup="true" CodeFile="InsuranceListDetails.aspx.cs" Inherits="InsuranceListDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPSHOP" runat="Server">
    <link rel="stylesheet" href="css/isp.css">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }

        .dvModal .modal-header .close {
            opacity: 1;
            color: var(--text-colour6);
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
                    <li class="breadcrumb-item"><a href="/InsuranceList.aspx">Insurance</a></li>
                    <li class="breadcrumb-item active">Insurance Details</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvInsuranceDetails">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-md-4">
                    <div class="card mb-3">
                        <a data-toggle="modal" data-target="#productModal">
                            <div class="img-container">
                                <img src="#" id="imgProductImageMain" runat="server" />
                            </div>
                            <%--<div class="d-flex flex-wrap bg-white p-3">
                                <p class="h6 heading-bold text-truncate" ></p>
                            </div>--%>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-md-8">
                    <div class="row">
                        <div class="col-12 mb-3">
                            <h2 class="h2 heading-light text-colour1 text-truncate mb-1" id="divThumbnailServiceName" runat="server"></h2>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div id="divDynamicInputFields">
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="mb-3" id="divBuy" runat="server">
                                    <a href="javascript:void(0);" class="btn btn-two mr-3" id="btnBack" runat="server">
                                        
                                            Back
                                    </a>
                                    <a href="javascript:void(0);" class="btn btn-one" id="fetchUserdetails" runat="server" onclick="fnFetchUserDetails()">
                                       
                                            Fetch User Details
                                        
                                    </a>
                            </div>
                            <div id="divEmailErrorMsg" runat="server">
                                <h2 class="h6 heading-semibold text-danger">You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.</h2>
                            </div>
                        </div>

                        <div class="col-12">
                            <div id="divUserDetails" class="d-none">
                                <h3 class="h5 heading-semibold text-colour7 my-3">User Details</h3>
                                <div class="bg-colour2 p-3 mb-3">
                                    <div class="row">

                                        <!-- Pay Mode -->
                                        <div class="col-12 mb-3" id="trPayMode">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Pay Mode:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="payMode"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Pay Mode -->

                                        <!-- Proforma No -->
                                        <div class="col-12 mb-3" id="trproformaNo">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Proforma No.:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="proformaNo"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Proforma No -->

                                        <!-- Customer Name -->
                                        <div class="col-12 mb-3" id="trcustomerName">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Customer Name:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="customerName"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Customer Name -->

                                        <!-- Policy No. -->
                                        <div class="col-12 mb-3" id="trpolicyno">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Policy No.:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="policyno"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Policy No. -->

                                        <!-- Installment No: -->
                                        <div class="col-12 mb-3" id="trinstallmentNo">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Installment No.:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="installmentNo"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Installment No. -->

                                        <!-- Address. -->
                                        <div class="col-12 mb-3" id="traddress">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Address:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="address"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Address. -->

                                        <!-- Product Name -->
                                        <div class="col-12 mb-3" id="trProductName">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Product Name:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="ProductName"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Product Name -->

                                        <!-- Invoice No -->
                                        <div class="col-12 mb-3" id="trInvoiceNo">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Invoice No:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="InvoiceNo"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Invoice No -->

                                        <!-- Policy Status -->
                                        <div class="col-12 mb-3" id="trpolicyStatus">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Policy Status:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="policyStatus"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Policy Status -->

                                        <!-- Plan Code -->
                                        <div class="col-12 mb-3" id="trplanCode">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Plan Code:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="planCode"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Plan Code -->

                                        <!-- Due Date -->
                                        <div class="col-12 mb-3" id="trduedate">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Due Date:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="duedate"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Due Date -->

                                        <!-- Next Due Date -->
                                        <div class="col-12 mb-3" id="trNextDueDate">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Next Due Date:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="NextDueDate"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Next Due Date -->

                                        <!-- Current Due Date -->
                                        <div class="col-12 mb-3" id="trCurrentDueDate">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Current Due Date:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="CurrentDueDate"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Current Due Date -->

                                        <!-- Payment Date -->
                                        <div class="col-12 mb-3" id="trPaymentDate">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Payment Date:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="PaymentDate"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Payment Date -->

                                        <!-- Maturity Date -->
                                        <div class="col-12 mb-3" id="trMaturityDate">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Maturity Date:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="MaturityDate"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Maturity Date -->

                                        <!-- Term -->
                                        <div class="col-12 mb-3" id="trterm">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Term:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="term"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Term -->

                                        <!-- Amount/Points -->
                                        <div class="col-12 mb-3" id="tramount">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Amount/Points:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="amount"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Amount/Points -->

                                        <!-- Premium Amount/Points -->
                                        <div class="col-12 mb-3" id="trpremiumamount">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Premium Amount/Points:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="premiumamount"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Premium Amount/Points -->

                                        <!-- Rebate Amount/Points -->
                                        <div class="col-12 mb-3" id="trrebateamount">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Rebate Amount/Points:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="rebateamount"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Rebate Amount/Points: -->

                                        <!-- Fine Amount/Points -->
                                        <div class="col-12 mb-3" id="trfineamount">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Fine Amount/Points:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="fineamount"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Fine Amount/Points -->

                                        <!-- Adjustment Amount/Points -->
                                        <div class="col-12 mb-3" id="trAdjustmentAmount">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Adjustment Amount/Points:</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="AdjustmentAmount"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Adjustment Amount/Points -->

                                        <!-- TP Premium (Amount/Points) -->
                                        <div class="col-12 mb-3" id="trTPPremium">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">TP Premium (Amount/Points):</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="TPPremium"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- TP Premium (Amount/Points) -->

                                        <!-- Sum Insured (Amount/Points) -->
                                        <div class="col-12" id="trSumInsured">
                                            <div class="bg-white p-3">
                                                <div class="row">
                                                    <div class="col-md-4 col-6">
                                                        <p class="h6 text-regular">Sum Insured (Amount/Points):</p>
                                                    </div>
                                                    <div class="col-md-8 col-6">
                                                        <p class="h6 heading-semibold text-right" id="SumInsured"></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Sum Insured (Amount/Points) -->

                                    </div>
                                    <div class="">
                                        <a id="btnPayment" href="javascript:void(0);" class="btn btn-one d-none" onclick="fnPaymentRequest();">Payment</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="form-group pb-4 mt-3" id="divInsufficient" runat="server" style="display: none;">
                                <h2 class="h6 heading-semibold text-danger">Insufficient Points</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--Alert Modal -->
    <div class="dvModal modal fade" id="alertModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content py-4">
                <div class="modal-header justify-content-center pt-2 pb-0 border-0">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body text-center py-2" id="alertmessage">
                    <p id="errormessage"></p>
                </div>
                <div class="modal-footer justify-content-center border-0 pt-2 pb-0 px-0">
                    <button type="button" class="btn btn-yellow" data-dismiss="modal">Ok</button>
                    <%--<button type="button" class="btn btn-one">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var servicecode = getQuerystring('code');
            BindInsuranceProductDetails(servicecode);

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
        function BindInsuranceProductDetails(pstrServiceCode) {
            $("#updProgress").show();
            try {
                $.ajax({
                    type: 'POST',
                    url: 'InsuranceListDetails.aspx/BindInsuranceProductDetails',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{pstrServiceCode:" + pstrServiceCode + "}",
                    cache: false,
                    success: function (rtnData) {
                        fnBindDynamicFields(rtnData.d);
                        $("#updProgress").hide();
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnBindDynamicFields(data) {
            //debugger
            try {
                if (data != '') {
                    var parseData = JSON.parse(data);
                    if (parseData.fields != null) {
                        var html = '';
                        html += '<div class="dvDetails">';
                        html += '<h3 class="h5 heading-semibold text-colour7 mb-3">Details</h3>';
                        html += '<div class="row">';
                        $.each(parseData.fields, function (i, currfield) {
                            if (currfield.Type.toLowerCase() == 'string') {
                                html += '<div class="col-lg-6 form-group">';
                                html += '<label class="h6 heading-regular text-colour7 mb-2">' + currfield.Title + '</label>';
                                html += '<input onblur=\"fnValidateInput();\" placeholder=\"' + (currfield.Required ? currfield.Title + ' *' : currfield.Title) + '\" type=\"text\"  isRequired=\"' + currfield.Required + '\" dataFormat=\"text\" class=\"bookinginput txtinput input form-control\" id=\"txtpolicyno\" name=\"' + currfield.Title + '\"/>';
                                html += '</div>';
                            }
                            else if (currfield.Type.toLowerCase() == 'datetime') {
                                html += '<div class="col-lg-6 form-group">';
                                html += '<label class="h6 heading-regular text-colour7 mb-2">' + currfield.Title + '</label>';
                                html += '<div class=\"input-group\">';
                                html += '<input onblur=\"fnValidateInput();\" placeholder=\"' + (currfield.Required ? currfield.Title + '*' : '') + '\" isRequired=\"' + currfield.Required + '\"  type=\"text\" onpaste=\"return false\" oncut=\"return false\" readonly class=\"bookinginput dtinput datepicker form-control cal-icon \" name=\"' + currfield.Title + '\"  id=\"dobdatepicker\"/>';
                                html += '<div class=\"input-group-append\"><span class=\"input-group-text bg-white\"><i class=\"fa-regular fa-calendar\"></i></span></div></div>';
                                html += '</div>';
                            }
                        });
                        html += '</div>';
                        html += '<div class="row"><div class="col-12 mb-2">';
                        html += '<p class=\"experienceerrormsg\" id=\"spnCompleteBookingErrorMsg\" style="display:none;"></p>';
                        html += '</div></div>';
                        html += '</div>';
                        $('#divDynamicInputFields').empty().html(html);
                        fnBindDatePicker();

                    }
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
                yearRange: "-90:-0",
                //maxDate: '-12Y',
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateText, inst) {
                    $("#" + this.id).removeClass('experienceerror');
                    $('#spnCompleteBookingErrorMsg').empty();
                    fnValidateInput();
                }
            });
            var dt = new Date();
        }
        function fnValidateInput() {
            var rtnResponse = true;
            try {
                var html = '';
                $(".bookinginput").each(function () {
                    $("#" + this.id).removeClass('experienceerror');
                    if ($("#" + this.id).hasClass("txtinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        var numericRegex = /^[0-9]{0,}$/;
                        if (($("#" + this.id).attr('dataformat') == 'text') && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                            if (this.value.replace(/\s+/g, '') == '' || (this.value.replace(/\s+/g, '') != '' && !this.value.replace(/\s+/g, '').match(numericRegex))) {
                                $("#" + this.id).addClass('experienceerror');
                                var textmsg = $("#" + this.id).attr('name')
                                html += "<p class=\"h6 heading-semibold text-danger\">Please enter valid " + textmsg + "</p>";
                                rtnResponse = false;
                            }
                        }
                    }
                    if ($("#" + this.id).hasClass("dtinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        if (this.value.replace(/\s+/g, '') == '') {
                            $("#" + this.id).addClass('experienceerror');
                            var textmsg = $("#" + this.id).attr('name')
                            html += "<p class=\"h6 heading-semibold text-danger\">Please Select " + textmsg + ".</p>";
                            rtnResponse = false;
                        }
                    }
                });
                if (html != "") {
                    $('#spnCompleteBookingErrorMsg').empty().html(html);
                    $('#spnCompleteBookingErrorMsg').show();
                }
                else {
                    $('#spnCompleteBookingErrorMsg').empty().html(html);
                    $('#spnCompleteBookingErrorMsg').hide();
                }

            } catch (e) {
            }
            return rtnResponse;
        }
        function formatDate(input) {
            //debugger
            var datePart = input.match(/\d+/g),
                year = datePart[2], // get 4 digits
                month = datePart[1],
                day = datePart[0];

            return year + '-' + month + '-' + day;
        }
        function fnFetchUserDetails() {
            var isValid = fnValidateInput();
            if (isValid) {
                try {
                    var servicecode = getQuerystring('code');
                    $("#updProgress").show();
                    $.ajax({
                        type: 'POST',
                        url: 'InsuranceListDetails.aspx/FetchUserDetails',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: "{pstrServiceCode:" + servicecode + ",PolicyNo:'" + $("#txtpolicyno").val() + "',DOB:'" + formatDate($("#dobdatepicker").val()) + "'}",
                        cache: false,
                        success: function (data) {
                            if (data.d != "") {
                                fnBindUserDetailsFields(data.d);
                            }
                            else {
                                $('#spnCompleteBookingErrorMsg').empty().html("Invalid details entered.");
                                $('#spnCompleteBookingErrorMsg').show();
                            }
                        },
                        error: function (errmsg) {
                        }
                    });
                } catch (e) {
                }
            }

        }
        function fnBindUserDetailsFields(data) {
            //debugger
            try {
                if (data != '') {
                    if (isJson(data)) {
                        var parseData = JSON.parse(data);
                        if (parseData != null) {
                            $('#divUserDetails').removeClass("d-none");
                            if (parseData.PayMode != "") {
                                $("#payMode").html(parseData.PayMode);
                                $("#trPayMode").removeClass("d-none");
                            }
                            else {
                                $("#trPayMode").addClass("d-none");
                            }
                            if (parseData.ProformaNo != "") {
                                $("#proformaNo").html(parseData.ProformaNo);
                                $("#trproformaNo").removeClass("d-none");
                            }
                            else {
                                $("#trproformaNo").addClass("d-none");
                            }
                            if (parseData.CustomerName != "") {
                                $("#customerName").html(parseData.CustomerName);
                                $("#trcustomerName").removeClass("d-none");
                            }
                            else {
                                $("#trcustomerName").addClass("d-none");
                            }
                            if (parseData.PolicyNo != "") {
                                $("#policyno").html(parseData.PolicyNo);
                                $("#trpolicyno").removeClass("d-none");
                            }
                            else {
                                $("#trpolicyno").addClass("d-none");
                            }
                            if (parseData.InstallmentNo != "") {
                                $("#installmentNo").html(parseData.InstallmentNo);
                                $("#trinstallmentNo").removeClass("d-none");
                            }
                            else {
                                $("#trinstallmentNo").addClass("d-none");
                            }
                            if (parseData.Address != "") {
                                $("#address").html(parseData.Address);
                                $("#traddress").removeClass("d-none");
                            }
                            else {
                                $("#traddress").addClass("d-none");
                            }
                            if (parseData.ProductName != "") {
                                $("#ProductName").html(parseData.ProductName);
                                $("#trProductName").removeClass("d-none");
                            }
                            else {
                                $("#trProductName").addClass("d-none");
                            }
                            if (parseData.InvoiceNo != "") {
                                $("#InvoiceNo").html(parseData.InvoiceNo);
                                $("#trInvoiceNo").removeClass("d-none");
                            }
                            else {
                                $("#trInvoiceNo").addClass("d-none");
                            }
                            if (parseData.PolicyStatus != "") {
                                $("#policyStatus").html(parseData.PolicyStatus);
                                $("#trpolicyStatus").removeClass("d-none");
                            }
                            else {
                                $("#trpolicyStatus").addClass("d-none");
                            }
                            if (parseData.PlanCode != "") {
                                $("#planCode").html(parseData.PlanCode);
                                $("#trplanCode").removeClass("d-none");
                            }
                            else {
                                $("#trplanCode").addClass("d-none");
                            }
                            if (parseData.DueDate != "") {
                                $("#duedate").html(parseData.DueDate);
                                $("#trduedate").removeClass("d-none");
                            }
                            else {
                                $("#trduedate").addClass("d-none");
                            }
                            if (parseData.NextDueDate != "") {
                                $("#NextDueDate").html(parseData.NextDueDate);
                                $("#trNextDueDate").removeClass("d-none");
                            }
                            else {
                                $("#trNextDueDate").addClass("d-none");
                            }
                            if (parseData.CurrentDueDate != "") {
                                $("#CurrentDueDate").html(parseData.CurrentDueDate);
                                $("#trCurrentDueDate").removeClass("d-none");
                            }
                            else {
                                $("#trCurrentDueDate").addClass("d-none");
                            }
                            if (parseData.PaymentDate != "") {
                                $("#PaymentDate").html(parseData.PaymentDate);
                                $("#trPaymentDate").removeClass("d-none");
                            }
                            else {
                                $("#trPaymentDate").addClass("d-none");
                            }
                            if (parseData.MaturityDate != "") {
                                $("#MaturityDate").html(parseData.MaturityDate);
                                $("#trMaturityDate").removeClass("d-none");
                            }
                            else {
                                $("#trMaturityDate").addClass("d-none");
                            }
                            if (parseData.Term != "") {
                                $("#term").html(parseData.Term);
                                $("#trterm").removeClass("d-none");
                            }
                            else {
                                $("#trterm").addClass("d-none");
                            }
                            if (parseData.Amount != "") {
                                $("#amount").html(parseData.Amount + " NPR / " + ConvertAmountToPoints(parseData.Amount, parseData.PointRate) + " Points");
                                $("#tramount").removeClass("d-none");
                            }
                            else {
                                $("#tramount").addClass("d-none");
                            }
                            if (parseData.PremiumAmount != "") {
                                $("#premiumamount").html(parseData.PremiumAmount + " NPR / " + ConvertAmountToPoints(parseData.PremiumAmount, parseData.PointRate) + " Points");
                                $("#trpremiumamount").removeClass("d-none");
                            }
                            else {
                                $("#trpremiumamount").addClass("d-none");
                            }
                            if (parseData.RebateAmount != "") {
                                $("#rebateamount").html(parseData.RebateAmount + " NPR / " + ConvertAmountToPoints(parseData.RebateAmount, parseData.PointRate) + " Points");
                                $("#trrebateamount").removeClass("d-none");
                            }
                            else {
                                $("#trrebateamount").addClass("d-none");
                            }
                            if (parseData.FineAmount != "") {
                                $("#fineamount").html(parseData.FineAmount + " NPR / " + ConvertAmountToPoints(parseData.FineAmount, parseData.PointRate) + " Points");
                                $("#trfineamount").removeClass("d-none");
                            }
                            else {
                                $("#trfineamount").addClass("d-none");
                            }
                            if (parseData.AdjustmentAmount != "") {
                                $("#AdjustmentAmount").html(parseData.AdjustmentAmount + " NPR / " + ConvertAmountToPoints(parseData.AdjustmentAmount, parseData.PointRate) + " Points");
                                $("#trAdjustmentAmount").removeClass("d-none");
                            }
                            else {
                                $("#trAdjustmentAmount").addClass("d-none");
                            }
                            if (parseData.TP_Premium != "") {
                                $("#TPPremium").html(parseData.TP_Premium + " NPR / " + ConvertAmountToPoints(parseData.TP_Premium, parseData.PointRate) + " Points");
                                $("#trTPPremium").removeClass("d-none");
                            }
                            else {
                                $("#trTPPremium").addClass("d-none");
                            }
                            if (parseData.SumInsured != "") {
                                $("#SumInsured").html(parseData.SumInsured + " NPR / " + ConvertAmountToPoints(parseData.SumInsured, parseData.PointRate) + " Points");
                                $("#trSumInsured").removeClass("d-none");
                            }
                            else {
                                $("#trSumInsured").addClass("d-none");
                            }
                            CheckAvailability(ConvertAmountToPoints(parseData.Amount, parseData.PointRate));
                        }
                    }
                    else {
                        $("#errormessage").html(data);
                        $('#alertModal').modal('show');
                    }
                }
            }
            catch (e) {

            }
        }
        function isJson(str) {
            try {
                JSON.parse(str);
                return true;
            } catch (e) {
                return false;
            }
            return true;
        }
        function CheckAvailability(amount) {
            $.ajax({
                type: "POST",
                url: "InsuranceListDetails.aspx/CheckAvailability",
                data: "{pntamount:" + amount + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        $("#CP_CPSHOP_divInsufficient").hide();
                        $("#btnPayment").removeClass('d-none');
                        return true;
                    }
                    else {
                        $("#CP_CPSHOP_divInsufficient").show();
                        $("#btnPayment").addClass('d-none');
                        return false;
                    }
                    $("#updProgress").hide();
                }
            });
            return false;
        }
        function fnPaymentRequest() {
            try {
                $.ajax({
                    type: "POST",
                    url: "InsuranceListDetails.aspx/CheckoutGenerateOTP",
                    data: "",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        var data = msg.d;
                        if (data.includes(".aspx")) {
                            window.location.href = data;
                        }
                        else if (data == "SESSION_TIME_OUT") {
                            $("#errormessage").html("Your session time out. Please login again.");
                            $('#alertModal').modal('show');
                        }
                        else {
                            $("#errormessage").html("Purchase failed!!! Please try again later.");
                            $('#alertModal').modal('show');
                        }
                        return false;
                    },
                    error: function (err) {
                    }
                });
            }
            catch (e) {
            }
        }
        function ConvertAmountToPoints(pstrAmount, pfltPointrate) {
            var points = 0;
            points = Math.ceil(pstrAmount / pfltPointrate);
            return points;
        }
    </script>
</asp:Content>



