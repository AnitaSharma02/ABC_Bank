<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductBookingDetails.aspx.cs" Inherits="ExperienceProductBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />

    <style>
        .dvRedemptionMenu,
        .dvInnerBanner {
            display: none;
        }
    </style>

    <div class="dvExperienceProductBookingDetails py-3">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <div class="row dvDeliveryTrack">
                        <div class="col-4 mb-lg-3">
                            <div class="dvLine border d-none d-md-block px-3"></div>
                            <div class="row justify-content-md-center">
                                <div class="col-md-auto my-3">
                                    <div class="d-flex flex-column flex-sm-row align-items-center active">
                                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">1</span>
                                        <a class="h7 heading-regular bg-colour6 px-3 text-center text-colour7">Booking Details</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4 mb-lg-3">
                            <div class="dvLine border d-none d-md-block px-3"></div>
                            <div class="row justify-content-md-center">
                                <div class="col-md-auto my-3">
                                    <div class="d-flex flex-column flex-sm-row align-items-center">
                                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">2</span>
                                        <a class="h7 heading-regular bg-colour6 px-3 text-center text-colour7" id="hrefBookingDetailsId" runat="server">Payment Details</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-4 mb-lg-3">
                            <%--<div class="dvLine border d-none d-md-block px-3"></div>--%>
                            <div class="row justify-content-md-center">
                                <div class="col-md-auto my-3">
                                    <div class="d-flex flex-column flex-sm-row align-items-center">
                                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">3</span>
                                        <a class="h7 heading-regular bg-colour6 px-3 text-center text-colour7">Thank You</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12">
                    <div class="row">
                        <div class="col-12 col-md-5 col-lg-4">
                            <div class="dvBookingDetails dvVcData border b-radius bg-colour2 px-3 pb-3" id="divPaymentdetails">
                            </div>
                        </div>
                        <div class="col-12 col-md-7 col-lg-8 mt-3 mt-md-0" id="divContactdetails">
                            <div class="border b-radius bg-colour2 p-3">
                                <p class="heading6">Guest Contact Details</p>
                                <div class="row mt-3">
                                    <div class="col-12 col-md-12 col-lg-4 mb-3">
                                        <label class="label"><span>Title</span><span class="text-danger">*</span></label>
                                        <div class="dvInput input-group">
                                            <select class="select selectBtn selectDropdown form-control" id="sltitle">
                                                <option selected="selected" value="">Select Title</option>
                                                <option value="Mr">Mr.</option>
                                                <option value="Ms">Ms.</option>
                                                <option value="Mrs">Mrs.</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-4 mb-3">
                                        <label class="label"><span>First Name</span><span class="text-danger">*</span></label>
                                        <div class="dvInputGroup input-group">
                                            <input
                                                type="text"
                                                class="form-control border-right-0"
                                                id="txtFirstName"
                                                onkeyup="ValidateBookingDetailsFields();"
                                                maxlength="15"
                                                autocomplete="off"
                                                placeholder="Enter First Name"
                                                aria-describedby="inputGroupPrepend2"
                                                required="" />
                                            <div class="input-group-prepend">
                                                <span class="input-group-text bg-colour6"><i class="fa fa-user" aria-hidden="true"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-4 mb-3">
                                        <label class="label"><span>Last Name</span><span class="text-danger">*</span></label>
                                        <div class="dvInputGroup input-group">
                                            <input
                                                type="text"
                                                class="form-control border-right-0"
                                                id="txtLastName"
                                                placeholder="Enter Last Name"
                                                onkeyup="ValidateBookingDetailsFields();"
                                                maxlength="15"
                                                autocomplete="off"
                                                aria-describedby="inputGroupPrepend2"
                                                required="" />
                                            <div class="input-group-prepend">
                                                <span class="input-group-text bg-colour6"><i class="fa fa-user" aria-hidden="true"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-12 col-md-12 col-lg-4 mb-3">
                                        <label class="label"><span>Email</span><span class="text-danger">*</span></label>
                                        <div class="dvInputGroup input-group">
                                            <input
                                                type="text"
                                                class="form-control border-right-0"
                                                id="txtEmailId"
                                                onkeyup="ValidateBookingDetailsFields();"
                                                maxlength="50"
                                                autocomplete="off"
                                                placeholder="Enter Email Id"
                                                aria-describedby="inputGroupPrepend2"
                                                required="required" />
                                            <div class="input-group-prepend">
                                                <span class="input-group-text bg-colour6">
                                                    <i class="fa fa-envelope" aria-hidden="true"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-4 mb-3">
                                        <label class="label"><span>Contact Number</span><span class="text-danger">*</span></label>
                                        <div class="dvInputGroup input-group">
                                            <input
                                                type="text"
                                                class="form-control border-right-0"
                                                id="txtContactNumber"
                                                onkeyup="ValidateBookingDetailsFields();"
                                                maxlength="13"
                                                autocomplete="off"
                                                placeholder="Enter Contact Number"
                                                aria-describedby="inputGroupPrepend2"
                                                required="required" />
                                            <div class="input-group-prepend">
                                                <span class="input-group-text bg-colour6">
                                                    <i class="fa fa-phone-square" aria-hidden="true"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-12 col-md-12 col-lg-4 mb-3">
                                        <label class="label"><span>Date of Birth</span><span class="text-danger">*</span></label>
                                        <div class="dvTxtDOBAdult dvInputGroup input-group">
                                            <%--<asp:TextBox ID="" class="form-control icnDate" runat="server" AutoComplete="off" placeholder="Enter Date" ReadOnly="true"></asp:TextBox>--%>
                                            <input
                                                type="text"
                                                class="form-control border-right-0"
                                                id="txtDOB"
                                                onkeyup="ValidateBookingDetailsFields();"
                                                autocomplete="off"
                                                placeholder="Enter Date of Birth"
                                                aria-describedby="inputGroupPrepend2"
                                                required="required" />

                                            <div class="input-group-append">
                                                <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        <%-- <asp:RequiredFieldValidator ID="rfvAdultDOB" runat="server" ControlToValidate="txtDOB"
                                            Display="Dynamic" ErrorMessage="Enter Date of Birth" ValidationGroup="WebValidation"
                                            CssClass="rptErrorMassage h7 heading-regular text-danger"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidatorAdultDOB" runat="server" ErrorMessage="Adults (12+ yrs)"
                                            Display="Dynamic" ValidationGroup="WebValidation" OnServerValidate="IssueAdultDateValidator"
                                            ControlToValidate="txtDOB" CssClass="rptErrorMassage h7 heading-regular text-danger">
                                        </asp:CustomValidator>--%>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-4" id="divNationality" runat="server">
                                        <label class="label"><span>Nationality</span><span class="text-danger">*</span></label>
                                        <div class="dvInput input-group" id="divNationalityData" runat="server">
                                            <asp:DropDownList ID="drpNationality" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="drpNationality"
                                            ErrorMessage="Enter Nationality" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger"
                                            Enabled="false"></asp:RequiredFieldValidator>

                                    </div>

                                </div>
                            </div>

                            <div class="mt-3">
                                <div id="divAdditionalInfo" class="dvVcData h7 col-12 border b-radius bg-colour2 text-colour7 p-3">
                                </div>
                            </div>

                            <div class="mt-3">
                                <div class="col-12 dvVcData border b-radius bg-colour2 p-3" id="divPickupInformation">
                                </div>
                            </div>
                            <div class="border-bottom"></div>
                            <div class="col-12 border b-radius bg-colour2 p-3 mt-3">
                                <div class="row">
                                    <div class="cancelBox col-12 col-lg-6">
                                        <p class="heading6">Cancellation Policy:</p>
                                        <p class="h7 text-colour7" id="cancellationPolicy"></p>
                                    </div>
                                    <div class="col-12 col-lg-6 mt-lg-0 mt-3">
                                        <div class="dvLabel mb-3">
                                            <label class="checkbox-container d-flex">
                                                <span class="d-inline-block ml-1">
                                                    <input type="checkbox" onchange="ValidateBookingDetailsFields();" id="chkTnCPolicy" />
                                                    <span class="checkmark"></span>
                                                </span>
                                                <span id="spnTnCPolicy" class="d-inline-block ml-2 heading-regular h7">
                                                    <span>I have read and agree to ABC Bank</span>
                                                    <a href="TermsAndConditions.aspx" class="link1" target="_blank">Terms & Conditions</a> <span>and</span>
                                                    <a href="BookingPolicy.aspx" class="link1" target="_blank">Booking & Cancellation Policy</a>
                                                    <span>of the respective service provider.</span>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="dvNote">
                                            <p class="pt-2" id="txtImpNote" runat="server"></p>
                                            <%--  <p class="pt-2 h7">Once the transaction is successful you will get CV points* within 40-45 days. Please refer to <b><a href="TermsAndConditions.aspx" target="_blank">Terms & Conditions</a></b> for more details.</p>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="mt-3 text-center text-lg-right" id="divbtnProceedPayment">
                                <%--<button
                                        class="btn btn-one"
                                        type="button" id="btnProceedToPayment"
                                        value="Search">
                                        Proceed to payment
                                    </button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--Alert Modal -->
    <div class="dvCommonModal dvAlertModal modal fade" id="dvAlertModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title">
                        <span>Alert</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body text-center" id="alertmessage">
                    <p class="h6 text-colour7 heading-semibold" id="errormessage"></p>
                </div>
                <div class="modal-footer justify-content-center border-0 px-0">
                    <button type="button" class="btn btn-one" data-dismiss="modal">Ok</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $("#txtDOB").click(function () {
                $("#txtDOB").datepicker('show');
            });
            $(".dvTxtDOBAdult .input-group-append .input-group-text").on("click", function () {
                $("#txtDOB").datepicker("show");
            });
            $("#txtDOB").datepicker({
                numberOfMonths: 1,
                changeMonth: true,
                changeYear: true,
                //showButtonPanel: true,
                yearRange: "-90:-0",
                dateFormat: 'dd/mm/yy',
                maxDate: new Date,
                onSelect: function (dateText, inst) {
                    $("#txtDOB").text("");
                    $("#txtDOB").text(dateText);
                    $("#txtDOB").val(dateText.toString());
                    return false;
                }
            });
            var uuid = getQuerystring("uuid");
            if (uuid != null && uuid != "") {
                var adultCount = getQuerystring("adultCount");
                var childrenCount = getQuerystring("childrenCount");
                var seniorsCount = getQuerystring("seniorsCount");
                var ptuuid = getQuerystring("ptuuid");
                var puuid = getQuerystring("puuid");
                var date = getQuerystring("selectedDate");
                var selectedDate = decodeURIComponent(date);
                var timeslotuuid = getQuerystring("timeslotuuid");
                GetPaymentDetails(adultCount, childrenCount, seniorsCount, ptuuid, puuid, selectedDate, timeslotuuid);
            }
            else {
                window.location = "ExperienceProductList.aspx";
            }
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

        function GetPaymentDetails(adultCount, childrenCount, seniorsCount, ptuuid, puuid, selectedDate, timeslotuuid) {
            var arrData = {};
            arrData.adultCount = adultCount;
            arrData.childrenCount = childrenCount;
            arrData.seniorsCount = seniorsCount;
            arrData.ptuuid = ptuuid;
            arrData.puuid = puuid;
            arrData.selectedDate = selectedDate;
            arrData.timeslotuuid = timeslotuuid;
            $.ajax({
                type: 'POST',
                url: 'ExperienceProductBookingDetails.aspx/GetPaymentDetails',
                contentType: 'application/json;',
                dataType: 'json',
                data: JSON.stringify(arrData),
                cache: false,
                success: function (rtnData) {
                    if (rtnData.d != "" && rtnData.d != null) {
                        if (rtnData.d == "ErrorPage.aspx") {
                            window.location.href = "ErrorPage.aspx";
                        }
                        else {
                            fnBindPaymentDetails(rtnData.d, adultCount, childrenCount, seniorsCount, ptuuid, puuid, selectedDate, timeslotuuid);
                        }
                    }
                },
                error: function (errmsg) {

                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            });
        }

        function fnBindPaymentDetails(result, adultCount, childrenCount, seniorsCount, ptuuid, puuid, selectedDate, timeslotuuid) {
            var html = '';
            if (result != '') {
                var data = JSON.parse(result);
                if (data.ProductInfoResponse != null) {
                    //html += '<div class="">';
                    html += '<div class="d-flex flex-wrap justify-content-between align-items-center b-radius-top-right bg-colour1 px-3 py-2 mx-n3">';
                    html += '<p class="heading6 text-colour6">Booking Summary</p>';
                    html += '<p><a id="hrefEditbuttonId" class="btn btn-two" runat="server">Edit</a></p>';
                    html += '</div>';
                    //html += '</div>';
                    //html += '<div class="dvCityName pl-3 pr-3">';
                    html += '<h2 class="heading6 mt-3">' + data.ProductInfoResponse.data.title + '</h2>';
                    html += '<p class="h7 mb-3">Option: ' + data.ProductInfoResponse.producttypedetails.item_uuid.filter(obj => obj.uuid == ptuuid)[0].title + '</p>';
                    //html += '</div>';
                    //html += '<div class="dvSelectDate pl-3 pr-3 mt-3">';
                    html += '<div class="d-flex flex-wrap justify-content-between">';
                    html += '<p class="h7">Selected Date:</p>';
                    html += '<p class="h7">' + formatDate(selectedDate) + '</p>';
                    html += '</div>';
                    //html += '</div>';

                    if (timeslotuuid != null && timeslotuuid != '') {
                        //html += '<div class="dvSelectDate pl-3 pr-3 pt-2">';
                        html += '<div class="d-flex flex-wrap justify-content-between">';
                        html += '<p class="h7">Time Slot:</p>';
                        $.each(data.ProductInfoResponse.producttypedetails.item_uuid, function (i) {
                            if (ptuuid == data.ProductInfoResponse.producttypedetails.item_uuid[i].uuid) {
                                $.each(data.ProductInfoResponse.producttypedetails.item_uuid[i].typePriceByDate.timeslots, function (j) {
                                    if (timeslotuuid == data.ProductInfoResponse.producttypedetails.item_uuid[i].typePriceByDate.timeslots[j].uuid) {
                                        html += '<p class="h7">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typePriceByDate.timeslots[j].startTime.slice(0, -3) + ' - ' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typePriceByDate.timeslots[j].endTime.slice(0, -3) + ' hrs </p>';
                                    }
                                });
                            }
                        });
                        html += '</div>';
                        //html += '</div>';
                    }
                    //html += '<div class="dvSelectDate pl-3 pr-3 pt-2">';
                    var totalAmount = 0.00;
                    var totalPax = 0;
                    $.each(data.ProductInfoResponse.producttypedetails.item_uuid, function (i) {
                        if (ptuuid == data.ProductInfoResponse.producttypedetails.item_uuid[i].uuid) {
                            var ratesAarray = data.ProductInfoResponse.producttypedetails.item_uuid[i].typePriceByDate.rates.filter(obj => obj.type.toLowerCase() == "recommendedprice");
                            $.each(ratesAarray, function (n) {
                                var categoryName = toTitleCase(ratesAarray[n].category);
                                let recommendedPrice = Math.ceil(ratesAarray[n].amount);
                                var recommendedPriceFormat = ratesAarray[n].convertedCurrency + " " + FormatCurrency(recommendedPrice);
                                if (parseInt(adultCount) > 0 && ratesAarray[n].category.toLowerCase() == "adult") {
                                    totalPax += parseInt(adultCount);
                                    totalAmount += (parseInt(adultCount) * recommendedPrice);
                                    html += '<div class="d-flex justify-content-between">';
                                    html += '<p class="h7">' + adultCount + ' x ' + categoryName + ':</p>';
                                    html += '<p class="h7">' + recommendedPriceFormat + '</p>';
                                    html += '</div>';
                                }
                                else if (parseInt(seniorsCount) > 0 && ratesAarray[n].category.toLowerCase() == "senior") {
                                    totalPax += parseInt(seniorsCount);
                                    totalAmount += (parseInt(seniorsCount) * recommendedPrice);
                                    html += '<div class="d-flex justify-content-between">';
                                    html += '<p class="h7">' + seniorsCount + ' x ' + categoryName + ':</p>';
                                    html += '<p class="h7">' + recommendedPriceFormat + '</p>';
                                    html += '</div>';
                                }
                                else if (parseInt(childrenCount) > 0 && ratesAarray[n].category.toLowerCase() == "child") {
                                    totalPax += parseInt(childrenCount);
                                    totalAmount += (parseInt(childrenCount) * recommendedPrice);
                                    html += '<div class="d-flex justify-content-between">';
                                    html += '<p class="h7">' + childrenCount + ' x ' + categoryName + ':</p>';
                                    html += '<p class="h7">' + recommendedPriceFormat + '</p>';
                                    html += '</div>';
                                }
                            });
                        }
                    });
                    //html += '</div>';

                    html += '<div class="border-top my-2"></div>';
                    //html += '<div class="dvSelectDate pl-3 pr-3 pt-2 pb-2">';
                    html += '<div class="d-flex justify-content-between">';
                    html += '<p class="h7">Service fee</p>';
                    html += '<p class="h7">' + data.ProductInfoResponse.data.convertedCurrency.code + " " + FormatCurrency(0) + '</p>';
                    html += '</div>';
                    //html += '</div>';
                    html += '<div class="border-bottom my-2"></div>';
                    //html += '<div class="dvSelectDate pl-3 pr-3 pt-2">';
                    html += '<div class="d-flex justify-content-between">';
                    html += '<p class="heading6">TOTAL:</p>';
                    html += '<div>';
                    html += '<span class="heading6 pr-2" id="currencycode">' + data.ProductInfoResponse.data.convertedCurrency.code + '</span><span class="heading6" id="totalAmount">' + FormatCurrency(totalAmount) + '</span>';
                    html += '</div>';
                    /*                 html += '<p class="heading-semibold" id="totalAmount">' + data.ProductInfoResponse.data.convertedCurrency.code + " " + FormatCurrency(totalAmount) + '</p>';*/
                    html += '</div>';
                    //html += '</div>';
                    //html += '<div class="dvSelectDate pl-3 pr-3 pb-3">';
                    html += '<div class="d-flex justify-content-between">';
                    html += '<p></p>';
                    html += '<p class="h8">Price incl. GST</p>';
                    html += '</div>';
                    //html += '</div>';
                    $("#divPaymentdetails").empty().append(html);
                    updateVcDataSections();

                    html = '';
                    $.each(data.ProductInfoResponse.producttypedetails.item_uuid, function (i) {
                        if (ptuuid == data.ProductInfoResponse.producttypedetails.item_uuid[i].uuid) {
                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.hasOptions == true) {
                                html += '<p class="heading6">Additional Info</p>';
                                html += '<div class="row">';
                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking.length > 0) {
                                    let optionsPerBooking = 0;
                                    $.each(data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking, function (j) {
                                        data.BookingRequest.options.perBooking[optionsPerBooking].uuid = data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].uuid;
                                        data.BookingRequest.options.perBooking[optionsPerBooking].inputType = data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType;
                                        html += '<div class="col-12 col-md-12 col-lg-4 mt-3">';
                                        if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType != 7 && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType != 8) {
                                            html += '<label class="h8 heading-semibold label">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name;
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired) {
                                                html += '<span class="text-danger">*</span>';
                                            }
                                            html += '</label>';
                                        }
                                        if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 1 || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 2)//dropdown
                                        {
                                            html += '<div class="input-group-">';
                                            var selectId = "slt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var selectclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                selectclassname = "sltadditionalinfo";
                                            }
                                            html += '<select id="' + selectId + '" class="select selectBtn selectDropdown form-control ' + selectclassname + '">';
                                            html += '<option value="">Select ' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '</option>';
                                            $.each(data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].items, function (k) {
                                                html += '<option value="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].items[k].value + '">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].items[k].label + '</option>';
                                            });
                                            html += '</select>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + selectId + ' option:selected').val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 4 || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 12)//string-country
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.toLowerCase() == "email") {
                                                    selectclassname = "txtemailadditionalinfo";
                                                } else {
                                                    selectclassname = "txtalphanumericadditionalinfo";
                                                }
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input autocomplete="off" maxlength="50" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 3)//number
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtminnmaxnumericadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input autocomplete="off" minNumber="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].minNumber + '" maxNumber="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].maxNumber + '" maxlength="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].maxNumber + '" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 13)//phone
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtphonenoadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input autocomplete="off" maxlength="13" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 14)//flight no
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtflightnoadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 9)//address
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtaddressadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<textarea autocomplete="off" maxlength="250" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"></textarea>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 5)//boolean
                                        {
                                            var inputId = "chk_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "chkadditionalinfo";
                                            } else {
                                                inputclassname = "chknonmandatoryadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input type="checkbox" onchange="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '"/>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 6)//date
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtdatetimeadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input readonly="readonly"  autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();"  id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            html += '<div class="input-group-append">';
                                            html += '<span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>';
                                            html += '</div>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            BindInputDatepicker(inputId, data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", ""));
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 10)//time
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtdatetimeadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input readonly="readonly"  autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();"  id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            html += '<div class="input-group-append">';
                                            html += '<span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>';
                                            html += '</div>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            BindInputTime(inputId, selectedDate);
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 11)//datetime
                                        {
                                            var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                            var inputclassname = "";
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                inputclassname = "txtdatetimeadditionalinfo";
                                            }
                                            html += '<div class="input-group-">';
                                            html += '<input readonly="readonly"  autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();"  id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                            html += '<div class="input-group-append">';
                                            html += '<span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>';
                                            html += '</div>';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                            }
                                            html += '</div>';
                                            BindInputDateAndTime(inputId, selectedDate);
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                        }
                                        else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 7 || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 8) //file || image
                                        {
                                            html += '<input type="hidden" value=" "  id="txtfileImg"/>';
                                            data.BookingRequest.options.perBooking[optionsPerBooking].value = $('#txtfileImg' + inputId).val();
                                        }
                                        html += '</div>';
                                        optionsPerBooking++;
                                    });
                                }
                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perPax.length > 0) {
                                    html += '<div class="border-top mt-1 mb-1 w-100"></div>';
                                    for (var f = 0; f < totalPax; f++) {
                                        html += '<div class="col-12 p-3">Pax ' + (f + 1) + '</div>';
                                        let optionsPerPax = 0;
                                        $.each(data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perPax, function (j) {
                                            data.BookingRequest.options.perPax[f][optionsPerPax].uuid = data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perPax[j].uuid;
                                            data.BookingRequest.options.perPax[f][optionsPerPax].inputType = data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perPax[j].inputType;
                                            html += '<div class="col-12 col-md-12 col-lg-4 mb-3 mb-lg-0">';
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType != 7 && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType != 8) {
                                                html += '<label class="h8 heading-semibold label">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name;
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired) {
                                                    html += '<span class="text-danger">*</span>';
                                                }
                                                html += '</label>';
                                            }
                                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 1 || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 2)//dropdown
                                            {
                                                html += '<div class="input-group-">';
                                                var selectId = "slt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var selectclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    selectclassname = "sltadditionalinfo";
                                                }
                                                html += '<select id="' + selectId + '" class="select selectBtn selectDropdown form-control ' + selectclassname + '">';
                                                html += '<option value="">Select ' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '</option>';
                                                $.each(data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].items, function (k) {
                                                    html += '<option value="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].items[k].value + '">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].items[k].label + '</option>';
                                                });
                                                html += '</select>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + selectId + ' option:selected').val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 4 || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 12)//string-country
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.toLowerCase() == "email") {
                                                        selectclassname = "txtemailadditionalinfo";
                                                    } else {
                                                        selectclassname = "txtalphanumericadditionalinfo";
                                                    }
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input autocomplete="off" maxlength="50" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 3)//number
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtminnmaxnumericadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input autocomplete="off" minNumber="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].minNumber + '" maxNumber="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].maxNumber + '" maxlength="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].maxNumber + '" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 13)//phone
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtphonenoadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input autocomplete="off" maxlength="13" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 14)//flight no
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtflightnoadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 9)//address
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtaddressadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<textarea autocomplete="off" maxlength="250" onkeyup="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"></textarea>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 5)//boolean
                                            {
                                                var inputId = "chk_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "chkadditionalinfo";
                                                } else {
                                                    inputclassname = "chknonmandatoryadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input type="checkbox" onchange="ValidateBookingDetailsFields();" id="' + inputId + '" class="form-control ' + inputclassname + '"/>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 6)//date
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtdatetimeadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input readonly="readonly"  autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();"  id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                html += '<div class="input-group-append">';
                                                html += '<span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>';
                                                html += '</div>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                BindInputDatepicker(inputId, data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", ""));
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 10)//time
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtdatetimeadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input readonly="readonly"  autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();"  id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                html += '<div class="input-group-append">';
                                                html += '<span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>';
                                                html += '</div>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                BindInputTime(inputId, selectedDate);
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 11)//datetime
                                            {
                                                var inputId = "txt_" + optionsPerBooking + "_" + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                                var inputclassname = "";
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].IsRequired == true) {
                                                    inputclassname = "txtdatetimeadditionalinfo";
                                                }
                                                html += '<div class="input-group-">';
                                                html += '<input readonly="readonly"  autocomplete="off" maxlength="20" type="text" onkeyup="ValidateBookingDetailsFields();"  id="' + inputId + '" class="form-control ' + inputclassname + '" placeholder="' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name + '"/>';
                                                html += '<div class="input-group-append">';
                                                html += '<span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>';
                                                html += '</div>';
                                                if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description != '') {
                                                    html += '<span class="h8 heading-semibold label text-capitalize mt-2 w-100">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].description + '</span>';
                                                }
                                                html += '</div>';
                                                BindInputDateAndTime(inputId, selectedDate);
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                            }
                                            else if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 7 || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 8) //file || image
                                            {
                                                html += '<input type="hidden" value=" "  id="txtfileImg"/>';
                                                data.BookingRequest.options.perPax[f][optionsPerPax].value = $('#txtfileImg' + inputId).val();
                                            }
                                            html += '</div>';
                                            optionsPerPax++;
                                        });
                                    }

                                }
                                html += '</div>';
                            }
                        }
                        if (html == "") {
                            $("#divAdditionalInfo").hide();
                        } else {
                            $("#divAdditionalInfo").show();
                            $("#divAdditionalInfo").empty().append(html);
                            updateVcDataSections();
                        }
                        html = "";
                        if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingTime != null
                            || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingAddress != null
                            || data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingLocation != null) {
                            html += '<p class="heading6 pb-2">Pickup/Meeting Point Information</p>';
                            html += '<p class="heading-semibold text-colour7 h7">Extra Information:</p>';
                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingTime != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingTime != '') {
                                html += '<p class="heading-semibold text-colour7 h7"><span>Time: </span><span class="heading-regular">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingTime + '</span></p>';
                            }
                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingAddress != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingAddress != '') {
                                html += '<p class="heading-semibold text-colour7 h7"><span>Address: </span><span class="heading-regular">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingAddress + '</span></p>';
                            }
                            if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingLocation != null && data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingLocation != '') {
                                html += '<p class="heading-semibold text-colour7 h7"><span>Location: </span><span class="heading-regular">' + data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.meetingLocation + '</span></p>';
                            }
                            $("#divPickupInformation").empty().append(html);
                            updateVcDataSections();
                        }
                        html = "";
                        if (data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.cancellationPolicySummary != null &&
                            data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.cancellationPolicySummary != "") {
                            $("#cancelBox").show();
                            html += data.ProductInfoResponse.producttypedetails.item_uuid[i].typeinfo.cancellationPolicySummary;
                            $("#cancellationPolicy").empty().append(html);
                        } else {
                            $("#cancelBox").hide();
                        }

                    });
                    html = "";
                    html += '<button class="btn btn-one" type="button" id="btnProceedToPayment" value="ProceedToPayment" onclick="var retvalue = ProceedToPaymentOnclickEvent(\'' + encodeURIComponent(data.ProductInfoResponse.data.title) + '\'' + ',\'' + ptuuid + '\'' + ',\'' + encodeURIComponent(JSON.stringify(data.BookingRequest)) + '\'' + ',\'' + totalPax + '\',\'' + encodeURIComponent(JSON.stringify(data.ProductInfoResponse.producttypedetails)) + '\'); event.returnValue= retvalue;event.preventDefault(); return retvalue;" >Proceed to payment';
                    html += '</button>';
                    $("#divbtnProceedPayment").empty().append(html);

                }
            }
        }

        function formatDate(input) {
            var datePart = input.match(/\d+/g),
                day = datePart[0], // get four digits
                month = datePart[1], year = datePart[2];
            const monthNames = ["Jan", "Feb", "Mar", "Apr",
                "May", "Jun", "Jul", "Aug",
                "Sep", "Oct", "Nov", "Dec"];
            const monthName = monthNames[parseInt(month) - 1];
            return day + ' ' + monthName + ' ' + year;
        }
        function toTitleCase(str) {
            return str.replace(
                /\w\S*/g,
                text => text.charAt(0).toUpperCase() + text.substring(1).toLowerCase()
            );
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
        function BindInputDatepicker(datepickerId, name) {
            var todayDate = new Date();
            var currentYear = todayDate.getFullYear();
            if (name.toLowerCase() == "dateofbirth") {
                var startYear = currentYear - 100;
                var endYear = currentYear;
                $("#" + datepickerId + "").datepicker({
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    showAnim: "clip",
                    dateFormat: "dd/mm/yy",
                    minDate: new Date(startYear),
                    maxDate: todayDate,
                    yearRange: "" + startYear + ":" + endYear + "",
                    changeMonth: true,
                    changeYear: true,
                    onSelect: function (dateText) {
                        ValidateBookingDetailsFields();
                    }
                });
            }
            else if (name.toLowerCase() == "passportexpirydate") {
                var startYear = currentYear;
                var endYear = currentYear + 50;
                $("#" + datepickerId + "").datepicker({
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    showAnim: "clip",
                    dateFormat: "dd/mm/yy",
                    minDate: todayDate,
                    yearRange: "" + startYear + ":" + endYear + "",
                    changeMonth: true,
                    changeYear: true,
                    onSelect: function (dateText) {
                        ValidateBookingDetailsFields();
                    }
                });
            }
            else {
                var startYear = currentYear - 50;
                var endYear = currentYear + 50;
                $("#" + datepickerId + "").datepicker({
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    showAnim: "clip",
                    dateFormat: "dd/mm/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "" + startYear + ":" + endYear + "",
                    onSelect: function (dateText) {
                        ValidateBookingDetailsFields();
                    }
                });
            }
        }

        function BindInputTime(datepickerId, date) {
            var minDate = new Date(date);
            var todaysDate = new Date();
            if (minDate.setHours(0, 0, 0, 0) == todaysDate.setHours(0, 0, 0, 0)) {
                minDate = new Date();
            }
            $("#" + datepickerId + "").timepicker({
                showAnim: "clip",
                timeFormat: 'hh:mm tt',
                minDateTime: minDate,
                onSelect: function (dateText) {
                    ValidateBookingDetailsFields();
                }
            });
        }
        function BindInputDateAndTime(datepickerId, date) {
            var minDate = new Date(date);
            var todaysDate = new Date();
            if (minDate.setHours(0, 0, 0, 0) == todaysDate.setHours(0, 0, 0, 0)) {
                minDate = new Date();
            }
            $("#" + datepickerId + "").datetimepicker({
                controlType: 'select',
                oneLine: true,
                timeFormat: 'hh:mm tt',
                showAnim: "clip",
                dateFormat: "dd/mm/yy",
                minDateTime: minDate,
                changeMonth: true,
                changeYear: true,
                onSelect: function (dateText) {
                    ValidateBookingDetailsFields();
                }
            });
        }
        function ValidateBookingDetailsFields() {
            var isValidated = true;
            $(".error").remove();
            $("#spnTnCPolicy").removeClass('text-danger');
            var salutation = $.trim($('#sltitle option:selected').val());
            var firstName = $.trim($('#txtFirstName').val());
            var lastName = $.trim($('#txtLastName').val());
            var emailId = $.trim($('#txtEmailId').val());
            var contactNumber = $.trim($('#txtContactNumber').val());
            var dob = $.trim($("#txtDOB").val());
            // var cvMembershipNo = $.trim($('#txtCVMembershipNo').val());
            if (salutation == '') {
                isValidated = false;
                $('#sltitle').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
            }
            if (firstName.length == 0) {
                isValidated = false;
                $('#txtFirstName').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
            } else if (firstName.length > 0) {
                var filter = /^[a-zA-Z\s]*$/;
                if (!filter.test(firstName)) {
                    isValidated = false;
                    $('#txtFirstName').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid first name</span>');
                }
            }
            if (lastName.length == 0) {
                isValidated = false;
                $('#txtLastName').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
            } else if (lastName.length > 0) {
                var filter = /^[a-zA-Z\s]*$/;
                if (!filter.test(lastName)) {
                    isValidated = false;
                    $('#txtLastName').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid last name</span>');
                }
            }
            if (emailId.length == 0) {
                isValidated = false;
                $('#txtEmailId').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
            } else if (emailId.length > 0) {
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!filter.test(emailId)) {
                    isValidated = false;
                    $('#txtEmailId').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid email</span>');
                }
            }
            if (contactNumber.length == 0) {
                isValidated = false;
                $('#txtContactNumber').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
            } else if (contactNumber.length > 0) {
                var filter = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
                if (!filter.test(contactNumber)) {
                    isValidated = false;
                    $('#txtContactNumber').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid contact number</span>');
                }
            }
            if (!$("#chkTnCPolicy").is(':checked')) {
                isValidated = false;
                $("#spnTnCPolicy").addClass('text-danger');
            }
            if (dob == "") {
                isValidated = false;
                $('#txtDOB').closest("div").after('<span class="error h8 heading-regular text-danger">Enter Date of Birth</span>');
            }
            //if (cvMembershipNo.length == 0) {
            //    isValidated = false;
            //    $('#txtCVMembershipNo').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
            //} else {
            //    var filter = /^[0-9]{9}$/;
            //    if (!filter.test(cvMembershipNo)) {
            //        isValidated = false;
            //        $('#txtCVMembershipNo').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid CV Membership No.</span>');
            //    }
            //}
            $(".txtalphaadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[a-zA-Z\s]*$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtemailadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^([a-zA-Z0-9_\.\-])+\@@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtalphanumericadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[a-zA-Z0-9\s]*$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtaddressadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[a-zA-Z0-9,\s]*$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtphonenoadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtflightnoadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[A-Z0-9][A-Z0-9][0-9]{0,4}$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtdatetimeadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                }
            });
            $(".chkadditionalinfo").each(function () {
                if (!$('#' + this.id).is(":checked")) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                }
            });
            $(".chknonmandatoryadditionalinfo").each(function () {
                $('#' + this.id).attr("checked", $('#' + this.id).is(":checked"));
                $('#' + this.id).attr("value", $('#' + this.id).is(":checked"));
            });
            $(".txtminnmaxnumericadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[0-9\s]*$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    } else if ($(this).attr("minNumber") != '' && this.value < parseInt($(this).attr("minNumber"))) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                    else if ($(this).attr("maxNumber") != '' && this.value > parseInt($(this).attr("maxNumber"))) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".txtnumericadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (this.value.length > 0) {
                    var filter = /^[0-9\s]*$/;
                    if (!filter.test(this.value)) {
                        isValidated = false;
                        $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid ' + $(this).attr("apifieldname").toLowerCase() + '</span>');
                    }
                }
            });
            $(".sltadditionalinfo").each(function () {
                if (this.value.length == 0) {
                    isValidated = false;
                    $('#' + this.id).closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                }
            });
            return isValidated;
        }

        function ProceedToPaymentOnclickEvent(titleName, ptuuid, objbookingrequest, totalPax, objproducttypedetails) {
            $('#btnProceedToPayment').prop('disabled', true);
            $('#updProgress').show();
            if (ValidateBookingDetailsFields()) {
                fnProceedToPayment(titleName, $("#currencycode").text(), $("#totalAmount").text(), objproducttypedetails, ptuuid, objbookingrequest, totalPax);
            }
            else {
                $('#btnProceedToPayment').prop('disabled', false);
                $('#updProgress').hide();
            }
        }
        function fnProceedToPayment(titleName, currencycode, totalAmount, objproducttypedetails, ptuuid, objbookingrequest, totalPax) {
            var arrData = {};
            var mobileNo = $('#txtContactNumber').val();
            arrData.currencycode = currencycode;
            arrData.totalAmount = totalAmount.replace(",", "");
            arrData.title = $.trim($('#sltitle option:selected').val());
            arrData.firstName = $.trim($('#txtFirstName').val());
            arrData.lastName = $.trim($('#txtLastName').val());
            arrData.emailId = $.trim($('#txtEmailId').val());
            arrData.contactNo = mobileNo.substr(mobileNo.length - 10);
            //arrData.cvMembershipNo = $.trim($('#txtCVMembershipNo').val());

            var decodedbookingrequest = decodeURIComponent(objbookingrequest);
            var decodedproducttypedetails = decodeURIComponent(objproducttypedetails);
            var lobjbookingrequest = JSON.parse(decodedbookingrequest);
            var lobjproducttypedetails = JSON.parse(decodedproducttypedetails);
            $.each(lobjproducttypedetails.item_uuid, function (i) {
                if (ptuuid == lobjproducttypedetails.item_uuid[i].uuid) {
                    if (lobjproducttypedetails.item_uuid[i].typeinfo.hasOptions == true) {
                        if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking.length > 0) {
                            let optionsPerBooking = 0;
                            $.each(lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking, function (j) {
                                lobjbookingrequest.options.perBooking[optionsPerBooking].uuid = lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].uuid;
                                lobjbookingrequest.options.perBooking[optionsPerBooking].inputType = lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType;
                                if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 1 || lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 2)//dropdown
                                {
                                    var selectId = "slt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + selectId + ' option:selected').val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 4 || lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 12)//string-country
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 3)//number
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 13)//phone
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 14)//flight no
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 9)//address
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 5)//boolean
                                {
                                    var inputId = "chk_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 6)//date
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 10)//time
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 11)//datetime
                                {
                                    var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#' + inputId).val();
                                }
                                else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 7 || lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 8) //file || image
                                {
                                    html += '<input type="hidden" value=" "  id="txtfileImg"/>';
                                    lobjbookingrequest.options.perBooking[optionsPerBooking].value = $('#txtfileImg' + inputId).val();
                                }
                                optionsPerBooking++;
                            });
                        }
                        if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perPax.length > 0) {
                            let optionsPerPax = 0;
                            for (var f = 0; f < totalPax; f++) {
                                $.each(lobjproducttypedetails.item_uuid[i].typeinfo.options.perPax, function (j) {
                                    lobjbookingrequest.options.perPax[f][optionsPerPax].uuid = lobjproducttypedetails.item_uuid[i].typeinfo.options.perPax[j].uuid;
                                    lobjbookingrequest.options.perPax[f][optionsPerPax].inputType = lobjproducttypedetails.item_uuid[i].typeinfo.options.perPax[j].inputType;
                                    if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 1 || lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 2)//dropdown
                                    {
                                        var selectId = "slt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + selectId + ' option:selected').val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 4 || lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 12)//string-country
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 3)//number
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 13)//phone
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 14)//flight no
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 9)//address
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 5)//boolean
                                    {
                                        var inputId = "chk_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 6)//date
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 10)//time
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 11)//datetime
                                    {
                                        var inputId = "txt_" + optionsPerBooking + "_" + lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].name.replace(" ", "").replace("/", "");
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#' + inputId).val();
                                    }
                                    else if (lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 7 || lobjproducttypedetails.item_uuid[i].typeinfo.options.perBooking[j].inputType == 8) //file || image
                                    {
                                        html += '<input type="hidden" value=" "  id="txtfileImg"/>';
                                        lobjbookingrequest.options.perPax[f][optionsPerPax].value = $('#txtfileImg' + inputId).val();
                                    }
                                    optionsPerPax++;
                                });
                            }
                        }
                    }
                }
            });

            arrData.pobjbookingRequest = lobjbookingrequest;
            arrData.titleName = decodeURIComponent(titleName);
            arrData.Address = $("#CP_drpNationality").val();
            arrData.DOB = $("#txtDOB").val();
            $.ajax({
                type: 'POST',
                url: 'ExperienceProductBookingDetails.aspx/ProcessPayment',
                contentType: 'application/json;',
                dataType: 'json',
                data: JSON.stringify(arrData),
                cache: false,
                success: function (rtnData) {
                    if (rtnData.d != "" && rtnData.d != null) {
                        if (rtnData.d == "ErrorPage.aspx") {
                            window.location.href = "ErrorPage.aspx";
                        }
                        else if (rtnData.d == "SESSION_TIME_OUT") {
                            var pop = document.getElementById("alertmessage");
                            pop.innerHTML = "Your session time out. Please login again.";
                            $('#alertModal').modal('show');
                        }
                        else {
                            window.location.href = rtnData.d;
                        }
                    }
                },
                error: function (errmsg) {
                    console.log(errmsg.text);
                    window.location.href = "ErrorPage.aspx";
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            });
        }
    </script>
</asp:Content>

