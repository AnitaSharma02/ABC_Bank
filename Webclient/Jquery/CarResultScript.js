$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    })
}

function SetFuelPolicy(fuelType) {
    if (fuelType == "0") return "Undefine";
    if (fuelType == "1") return "Full to Full";
    if (fuelType == "2") return "Prepay, no Refunds";
    if (fuelType == "3") return "Prepay, with Refunds";
    if (fuelType == "4") return "Free Tank";
}

function SetCarClass(CarType) {
    var regExpData = '[{"RegExCollection":[{"RegExpression":"((X...)|(.V..)).*","RegExVal":"Special"},{"RegExpression":"[EM][^V].*","RegExVal":"Mini"},{"RegExpression":"[E][^V].*","RegExVal":"Economy"},{"RegExpression":"[C][^V].*","RegExVal":"Compact"},{"RegExpression":"[I][^V].*","RegExVal":"Midsize"},{"RegExpression":"[S][^V].*","RegExVal":"Standard"},{"RegExpression":"[F][^V].*","RegExVal":"Full"},{"RegExpression":"[P][^V].*","RegExVal":"Premium"},{"RegExpression":"[L][^V].*","RegExVal":"Luxury"},{"RegExpression":".*","RegExVal":"All Cars"}]}]';
    regExpData = $.parseJSON(regExpData);

    for (i = 0; i < regExpData[0].RegExCollection.length; i++) {
        var NewCarType = new RegExp(regExpData[0].RegExCollection[i].RegExpression);
        if (NewCarType.test(CarType.toString())) {
            return regExpData[0].RegExCollection[i].RegExVal.toString();
        }
    }
}
function ViewCarInfo(refId) {
    $.ajax({
        type: 'POST',
        url: 'CarList.aspx/GetCarTermsCondtion',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrrefId':'" + refId.toString() + "'}",
        success: function (msg) {
            var carvehical = $.parseJSON(msg.d);
            var html = "";
            for (var ItermGroup = 0; ItermGroup < carvehical.RentalTermsRS.Items[0].termGroupField.length; ItermGroup++) {
                if (carvehical.RentalTermsRS.Items[0].termGroupField[ItermGroup].Term != null) {
                    for (var Iterm = 0; Iterm < carvehical.RentalTermsRS.Items[0].termGroupField[ItermGroup].Term.length; Iterm++) {
                        html += "<div class='w95'><p><b>" + carvehical.RentalTermsRS.Items[0].termGroupField[ItermGroup].Term[Iterm].Caption + "<br /></b>";
                        html += carvehical.RentalTermsRS.Items[0].termGroupField[ItermGroup].Term[Iterm].Body + "</p></div>";
                    }
                }
            }
            $("#divFurtherInfo").append(html);
            $("html").scrollTop(0);
            $("#divCarDetails").addClass("Car_Details_Cotent");
            $("#divCarDetails").removeClass("hidecontent");
            $("#CarFade").show();
        },
        error: function (errmsg) {
        }
    });
}
function CloseCarInfo() {
    $("#CarFade").hide();
    $("#divCarDetails").removeClass("Car_Details_Cotent");
    $("#divCarDetails").addClass("hidecontent");
    $("#divFurtherInfo").empty();
}

function placeholderOnFocus(obj, defaultVal) {
    if (obj.value == "") {
        obj.value = defaultVal;
    } else if (obj.value == defaultVal) {
        obj.value = "";
    } else { }
    $("#" + obj.id).removeClass('error');
};

function bindCarDatepicker() {

    $("#txtpickupDate").datepicker({
        minDate: minimumtime(),
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {

            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);

            $("#txtDropoffDate").datepicker('destroy');
            $("#txtpickupDate").val(dateText.toString());

            $("#txtDropoffDate").datepicker({
                minDate: dateText,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateReturnText, inst) {
                    return false;
                }
            });

            $("#txtDropoffDate").val(oneDay);
            $("#txtpickupDate").text("").text(dateText.toString());

        }
    });

}
function bindCarMobDatepicker() {
    $("#txtpickupDate").datepicker({
        minDate: minimumtime(),
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {

            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#txtDropoffDate").datepicker('destroy');
            $("#txtpickupDate").val(dateText.toString());
            $("#txtDropoffDate").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateReturnText, inst) {


                    return false;
                }
            });
            $("#txtDropoffDate").val(oneDay);
            $("#txtpickupDate").text("").text(dateText.toString());

        }
    });
}
$(document).ready(function () {
    //$("#txtDriverAge").hide();
    $("#chkDriverAge").click(function () {
        if ($(this).is(":checked")) {
            $(".dvInput2").hide();
        } else {
            $(".dvInput2").show();
        }
    });
    $("#chkDropoffLocation").click(function () {
        if ($(this).is(":checked")) {
            $(".dvInput1").hide();
            // $("#txtDriverAge").hide();
        } else {
            $(".dvInput1").show();
            //$("#txtDriverAge").show();
        }
    });
    var $window = $(window);
    function checkCarDatepickerWidth() {
        var windowsize = $window.width();
        if (windowsize > 550) {
            bindCarDatepicker();
        }
        else {
            bindCarMobDatepicker();
        }
    }
    // Execute on load
    checkCarDatepickerWidth();
    // Bind event listener
    $(window).resize(checkCarDatepickerWidth);


    $("#txtpickupLocation").autocomplete({
        source: function (request, response) {
            var list = [];
            if (request.term.toString() != "" && request.term.toString().length > 3) {
                $.ajax({
                    type: 'POST',
                    url: 'CarSearch.aspx/GetPickupLocation',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'prefixText':'" + request.term.toString() + "'}",
                    cache: false,
                    success: function (msg) {

                        response(msg.d);
                    },
                    error: function (errmsg) {
                    }
                });
                response(list);
            }
        },
        select: function (event, ui) {

            $("#txtpickupLocation").val(ui.item.label.split('|')[0]);
            $("#txtpickupLocation").val(ui.item.value.split('|')[0]);

            $("#hndpickupLocationId").val(ui.item.label.split('|')[1]);

            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300
    });

    $("#txtDopoffLocation").autocomplete({
        source: function (request, response) {
            var list = [];
            if (request.term.toString() != "" && request.term.toString().length > 3) {
                $.ajax({
                    type: 'POST',
                    url: 'CarSearch.aspx/GetPickupLocation',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{'prefixText':'" + request.term.toString() + "'}",
                    cache: false,
                    success: function (msg) {

                        response(msg.d);
                    },
                    error: function (errmsg) {
                    }
                });
                response(list);
            }
        },
        select: function (event, ui) {

            $("#txtDopoffLocation").val(ui.item.label.split('|')[0]);
            $("#txtDopoffLocation").val(ui.item.value.split('|')[0]);

            $("#hndDopoffLocationId").val(ui.item.label.split('|')[1]);

            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300
    });

    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
        var strResult = item.label.split('|')[0].toString();

        return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<span style='line-height: 14px;height: auto;width:auto'>" + strResult + "</span>").appendTo(ul);
    };

    $(function () {
        var ddldriverss = $("#txtDriverResidence");
        $.ajax({
            type: "POST",
            url: "CarList.aspx/GetDriverResidence",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var s = '';
                /*    '<option value="-1">Please Select </option>';*/

                for (var i = 0; i < response.d.length; i++) {

                    s += '<option value="' + response.d[i].split("|")[1] + '">' + response.d[i].split("|")[0] + '</option>';

                }

                $("#txtDriverResidence").html(s);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });

});

function handleSelectChange(event) {

    var selectElement = event.target;
    var value = selectElement.value;
    var TxtVal = selectElement.options[selectElement.selectedIndex].text;

    $("#hnddriverLocationId").val(value);
    $("#hnddriverLocationname").val(TxtVal);
}


//Function to clone dropdown values for same drop location.
function CloneDropDown(pCtrlParent, pCtrlChild, defaultValue, defaultText) {

    if (defaultValue != '' && defaultText != '') {
        pCtrlChild.append('<option value="' + defaultValue + '">' + defaultText + '</option>')
    }
    pCtrlChild.empty();
    var options = pCtrlParent[0].innerHTML;
    pCtrlChild.append(options);
    pCtrlChild.val(pCtrlParent);
    pCtrlChild.val(pCtrlParent.find('option:selected').val());
    pCtrlChild.selectric('refresh');
}

function getDrophourDetails(dateReturnText, locationId) {
    $.ajax({
        type: 'POST',
        url: 'Index.aspx/GetDropOffTime',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        //data: "{'DropOffDate':'" + dateReturnText + "','LocationId':'" + $("#CP_DDLLocation").find('option:selected').prop("value") + "'}",
        data: "{'DropOffDate':'" + dateReturnText + "','LocationId':'" + locationId + "'}",
        cache: false,
        success: function (data) {

            var dateList = data.d;
            var Time = -1
            var hdnddlToTime
            var count = 0;
            $("#CP_CarValidationError").hide();
            if (data.d != '000000000000000000000000') {
                var initialized = 0;
                $("#CP_ddlDropFromTime").html('');
                for (var count = 0; count <= dateList.length; count++) {
                    var times;
                    Time++;
                    if (data.d.charAt(count) == '1') {
                        if (Time < 10) {
                            $("#CP_ddlDropFromTime").append($("<option>" + "0" + Time + "</option>"));
                            times = "0" + Time;
                        }
                        else {
                            $("#CP_ddlDropFromTime").append($("<option>" + Time + "</option>"));
                            times = Time;
                        }
                        if (initialized == 0) {
                            hdnddlToTime = times;
                            initialized = 1;
                        }
                        else {
                            hdnddlToTime = hdnddlToTime + "," + times;
                        }
                    }
                }
                document.getElementById('CP_ddlDropFromTime').selectedIndex = 0;
            }
            else {
                $("#CP_CarValidationError").show();
                $("#CP_CarValidationError")[0].innerHTML = 'Car is not Available for this Date';
            }
            $("#CP_hdnddlToTime").val(hdnddlToTime);
            $("#CP_ddlDropFromTime").selectric('refresh');
        },
        error: function (errmsg) {
            alert(errmsg.d);
        }
    });
}

$(function () {
    $("#CP_CarTextBoxCheckout").datepicker({
        minDate: minimumtime(),
        numberOfMonths: 2,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateReturnText, inst) {
            alert('Select check in date first.');
            $("#CP_CarTextBoxCheckout").val('');
            return false;
        }
    });
});

function CarValidation() {
    var msg = "";
    $('input').removeClass("error");
    $('div').removeClass("error");
    $("#CP_CarValidationError").empty();

    if (($("#txtpickupLocation").val() == '' || $("#txtpickupLocation").val() == null) || $("#txtpickupLocation").val() == "Please enter a pick-up location") {
        msg += "*Please enter a pick-up location.<br>";
        $("#txtpickupLocation").addClass("error");
    }
    if (($("#txtpickupDate").val() == '' || $("#txtpickupDate").val() == null) || $("#txtpickupDate").val() == "Check-In") {
        msg += "*Enter Pikup Date.<br>";
        $("#txtpickupDate").addClass("error");
    }
    if (($("#txtDropoffDate").val() == '' || $("#txtDropoffDate").val() == null || $("#txtDropoffDate").val() == 'Check-Out')) {
        msg += "*Enter Drop off date.<br>";
        $("#txtDropoffDate").addClass("error");
    }
    if ($("#txtDropoffDate").val() != '' && $("#txtpickupDate").val() != '') {
        var startdate = $("#txtpickupDate").val().split('/');
        var newstartdate = startdate[2] + '-' + startdate[1] + '-' + startdate[0];
        var enddate = $("#txtDropoffDate").val().split('/');
        var newenddate = enddate[2] + '-' + enddate[1] + '-' + enddate[0];
        var d1 = Date.parse(newstartdate);
        var d2 = Date.parse(newenddate);
      
        if (d1 > d2) {
            msg += "* Drop off date greator than pickup date.<br>";
            $("#txtDropoffDate").addClass("error");
        }
    }
    if ($("#txtDropoffDate").val() == $("#txtpickupDate").val()) {
        if ($("#ddlPickupTime").val() >= $("#ddlDropoffTime").val()) {
            msg += "*Drop off time greator than pick up time.<br>";
            $("#ddlPickupTime").addClass("error");
            $("#ddlDropoffTime").addClass("error");
        }
    }

    var chkDropoffLocation = document.getElementById("chkDropoffLocation");
    var chkDriverAge = document.getElementById("chkDriverAge");

    if (!chkDropoffLocation.checked) {

        if (($("#txtDopoffLocation").val() == '' || $("#txtDopoffLocation").val() == null) || $("#txtDopoffLocation").val() == "Please enter Drop off location?") {
            msg += "*Please enter Drop off location.<br>";
            $("#txtDopoffLocation").addClass("error");
        }
    }

    if (!chkDriverAge.checked) {

        if (($("#txtDriverAge").val() == '' || $("#txtDriverAge").val() == null) || $("#txtDriverAge").val() == "Please enter Driver age") {
            msg += "*Please enter Driver age<br>";
            $("#txtDriverAge").addClass("error");
        }
        if (!AcceptNumbersonly($("#txtDriverAge").val().trim())) {
            msg += "*Please enter valid Driver age<br>";
            $("#txtDriverAge").addClass("error");
        }
    }

    if (msg.length > 0) {

        $("#CP_CarValidationError").show();
        $("#CP_CarValidationError")[0].innerHTML = "<span class='heading-semibold text-danger d-block'>Below fields are mandatory.</span>";
        return false;
    }
    else {

        $("#CP_CarValidationError")[0].innerHTML = "";
        $("#CP_CarValidationError").hide();

        var pickupday = $.trim($("#txtpickupDate").val()).split('/')[0];
        var pickupmonth = $.trim($("#txtpickupDate").val()).split('/')[1];
        var pickupyear = $.trim($("#txtpickupDate").val()).split('/')[2];
        var PickupformatedDate = pickupyear + '-' + pickupmonth + '-' + pickupday;

        var Dropoffday = $.trim($("#txtDropoffDate").val()).split('/')[0];
        var Dropoffmonth = $.trim($("#txtDropoffDate").val()).split('/')[1];
        var Dropoffyear = $.trim($("#txtDropoffDate").val()).split('/')[2];
        var DropformatedDate = Dropoffyear + '-' + Dropoffmonth + '-' + Dropoffday;

        var countryvalue = $("#spndriverresidenceCountry").html();
        var countrycode = $("#spndriverCountry").html();
        var hdncountryvalue = $.trim($("#hnddriverLocationname").val());
        var hdncountrycode = $.trim($("#hnddriverLocationId").val());

        var DriverResidence = '';
        var DriverResidenceName = '';
        var PickupLocation = "PickupLocation=" + $.trim($("#txtpickupLocation").val()) + "&";
        var PickupLocationId = "PickupLocationId=" + $.trim($("#hndpickupLocationId").val()) + "&";
        var PickupDate = "PickupDate=" + PickupformatedDate + "&";
        var PickupTime = "PickupTime=" + $.trim($("#ddlPickupTime").find(":selected").text()) + "&";
        var DropoffDate = "DropoffDate=" + DropformatedDate + "&";
        var DropoffTime = "DropoffTime=" + $.trim($("#ddlDropoffTime").find(":selected").text()) + "&";
        if (!$.isEmptyObject(hdncountrycode)) {
            DriverResidence = "DriverResidenceCode=" + $.trim($("#hnddriverLocationId").val()) + "&";
        } else { DriverResidence = "DriverResidenceCode=" + $.trim($("#spndriverCountry").html()) + "&"; }

        if (!$.isEmptyObject(hdncountryvalue)) {
            DriverResidenceName = "DriverResidenceName=" + $.trim($("#hnddriverLocationname").val()) + "&";
        } else { DriverResidenceName = "DriverResidenceName=" + $.trim($("#spndriverresidenceCountry").html()) + "&"; }

        var DropoffLocation = "";
        var DropoffLocationId = "";
        var DriverAge = "";

        if (chkDropoffLocation.checked) {

            DropoffLocation = "DropoffLocation=" + $.trim($("#txtpickupLocation").val()) + "&";
            DropoffLocationId = "DropoffLocationId=" + $.trim($("#hndpickupLocationId").val()) + "&";
        }
        else {
            DropoffLocation = "DropoffLocation=" + $.trim($("#txtDopoffLocation").val()) + "&";
            DropoffLocationId = "DropoffLocationId=" + $.trim($("#hndDopoffLocationId").val()) + "&";
        }

        if (chkDriverAge.checked) {

            DriverAge = "DriverAge=";
        }
        else {
            DriverAge = "DriverAge=" + $.trim($("#txtDriverAge").val());
        }

        var queryString = PickupLocation + PickupLocationId + PickupDate + PickupTime + DropoffDate + DropoffTime + DropoffLocation + DropoffLocationId + DriverResidence + DriverResidenceName + DriverAge;

        window.location = "CarSearchWait.aspx?" + queryString;

    }
};
function validateNumber(e) {

    const pattern = /^[0-9]$/;

    return pattern.test(e.key)

}
function minimumtime() {
    var currentDate = new Date();
    currentDate.setHours(currentDate.getHours() + 72, currentDate.getMinutes(), currentDate.getSeconds(), currentDate.getMilliseconds());
    return currentDate;
}
//Proceed to Booking.
function gotoBooking(obj) {

    var pstrRefId = $(obj).parent().find("input[type='hidden']").val();
    var LocationId = $(".hdnLocationId").find("input[type='hidden']").val()
    $.ajax({
        url: 'CarList.aspx/IsMemberLoggedIn',
        type: 'POST',  // or get
        contentType: 'application/json; charset =utf-8',
        data: "{'pstrRefId':'" + pstrRefId.toString() + "'}",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;

                if (newData == true) {
                    window.location.href = 'CarDetails.aspx';
                }
                else {
                    window.location.href = 'Index.aspx';
                }
            }
            catch (e) {
                //      alert(e);
                return false;
            }
            return true;
            // do procedure if success 
        },
        error: function (errmsg) {
            // do procedure if fail
            // may be send a message to the server side to display a message that     shows session timeout
        }
    });
}

//Function for Searching cars as per search options selected.
function LocationSearchCarSearch() {
    var msg = "";
    if ($.trim($("#CP_DDLCountry option:selected").text()) == "Select Country" || $.trim($("#CP_DDLCountry option:selected").text()) == "") {
        msg += "Please Select PickUp Country." + "<br/>";
        $("#CP_DDLCountry").parent().parent().addClass('error');
    }
    if ($.trim($("#CP_DDlCity option:selected").text()) == "Select City" || $.trim($("#CP_DDlCity option:selected").text()) == "") {
        msg += "Please Select PickUp City." + "<br/>";
        $("#CP_DDlCity").parent().parent().addClass('error');
    }
    if ($.trim($("#CP_DDLLocation option:selected").text()) == "Select Location" || $.trim($("#CP_DDLLocation option:selected").text()) == "") {
        msg += "Please Select Pickup Location." + "<br/>";
        $("#CP_DDLLocation").parent().parent().addClass('error');
    }
    if ($.trim($("#CP_CarTextBoxCheckin").val()) == "" || $.trim($("#CP_CarTextBoxCheckin").val()) == "Enter Date") {
        msg += "Please Select Pickup Date." + "<br/>";
        $("#CP_CarTextBoxCheckin").addClass('error');
    }
    if ($.trim($("#CP_CarTextBoxCheckout").val()) == "" || $.trim($("#CP_CarTextBoxCheckout").val()) == "Enter Date") {
        msg += "Please Select Drop-Off Date." + "<br/>";
        $("#CP_CarTextBoxCheckout").addClass('error');
    }
    if (!$("#CP_chkDriver").is(":checked")) {
        if ($.trim($("#CP_txtDiverAge").val()) < 21 || $.trim($("#CP_txtDiverAge").val()) > 70) {
            msg += "Driver age is not valid." + "<br/>";
            $("#CP_txtDiverAge").addClass('error');
        }
    }
    if (!$("#CP_droploction").is(":checked")) {
        if ($.trim($("#CP_ddlDropCountry").val()) == "Select Country" || $.trim($("#CP_ddlDropCountry").val()) == "") {
            msg += "Please Select Drop Country." + "<br/>";
            $("#CP_ddlDropCountry").parent().parent().addClass('error');
        }
        if ($.trim($("#CP_ddlDropCity").text()) == "Select City") {
            msg += "Please Select Drop City." + "<br/>";
            $("#CP_ddlDropCity").parent().parent().addClass('error');
        }
        if ($.trim($("#CP_ddlDropLocation").text()) == "Select Location" || $.trim($("#CP_ddlDropLocation").text()) == "") {
            msg += "Please Select Drop Location." + "<br/>";
            $("#CP_ddlDropLocation").parent().parent().addClass('error');
        }
    }
    if ($.trim($("#CP_CarTextBoxCheckin").val()) != "" || $.trim($("#CP_CarTextBoxCheckout").val()) != "") {
        var PickUptDate = $("#CP_CarTextBoxCheckin").val();
        var PickUpHr = $.trim($("#CP_ddlFromTime option:selected").text());
        var PickUpMin = $.trim($("#CP_ddlMinFrom option:selected").text());

        var DropDate = $("#CP_CarTextBoxCheckout").val();
        var DropHr = $.trim($("#CP_ddlDropFromTime option:selected").text());
        var DropMin = $.trim($("#CP_ddlDropMinFrom option:selected").text());

        var PickUpDateArr = PickUptDate.split('/');
        var PickupformatedDate = PickUpDateArr[1] + '/' + PickUpDateArr[0] + '/' + PickUpDateArr[2];
        var DropDateArr = DropDate.split('/');
        var DropformatedDate = DropDateArr[1] + '/' + DropDateArr[0] + '/' + DropDateArr[2];

        var PickupDateTime = new Date(PickupformatedDate + ' ' + PickUpHr + ':' + PickUpMin);
        var DropOffDateTime = new Date(DropformatedDate + ' ' + DropHr + ':' + DropMin);

        var diff = DropOffDateTime - PickupDateTime;

        var diffSeconds = diff / 1000;
        var HH = Math.floor(diffSeconds / 3600);
        var MM = Math.floor(diffSeconds % 3600) / 60;

        if (HH < 1) {
            msg += "your rental must be for at least 1 hours or more";
        }
    }
    else {
    }

    if (msg.length > 0) {
        $("#CP_CarValidationError").show();
        $("#CP_CarValidationError")[0].innerHTML = "<span class='heading-semibold text-danger d-block'>Below fields are mandatory.</span>";
        return !(msg.length > 0);
    }
    else {
        strPickupCountry = "PickupCountry=" + $.trim($('#CP_DDLCountry option:selected').text()) + "&";
        strPickupCity = "pickupCity=" + $.trim($('#CP_DDlCity option:selected').text()) + "&";
        var strPickupLocation = "pickupLocation=" + $.trim($("#CP_DDLLocation").find('option:selected').prop("value")) + "&";
        var strPickupLocationName = "pickupLocationName=" + $.trim($("#CP_DDLLocation").find('option:selected').text()) + "&";
        var strDropofCountry = "";
        var strDropofCity = "";
        var strDropoffLocation = "";
        if ($('#CP_droploction').attr('checked')) {
            strDropofCountry = "DropofCountry=" + $.trim($('#CP_DDLCountry option:selected').text()) + "&";
            strDropofCity = "dropofCity=" + $.trim($('#CP_DDlCity option:selected').text()) + "&";
            strDropoffLocation = "dropoffLocation=" + $("#CP_DDLLocation").val() + "&";
            strDropoffLocationName = "dropoffLocationName=" + $.trim($("#CP_DDLLocation").find('option:selected').text()) + "&";
        }
        else {
            strDropofCountry = "DropofCountry=" + $('#CP_ddlDropCountry option:selected').text() + "&";
            strDropofCity = "dropofCity=" + $.trim($('#CP_ddlDropCity option:selected').text()) + "&";
            strDropoffLocation = "dropoffLocation=" + $.trim($("#CP_ddlDropLocation").val()) + "&";
            strDropoffLocationName = "dropoffLocationName=" + $.trim($("#CP_ddlDropLocation").find('option:selected').text()) + "&";
        }

        var isdropoff = "isdropoff=" + $("#CP_droploction").is(":checked").toString() + "&";
        var strDepartDate = "departDate=" + $.trim($("#CP_CarTextBoxCheckin").val()) + "&";
        var strDepartTime = "departTime=" + $.trim($("#CP_ddlFromTime option:selected").text()) + ":" + $.trim($("#CP_ddlMinFrom option:selected").text()) + "&";
        var strReturnDate = "arrDate=" + $.trim($("#CP_CarTextBoxCheckout").val()) + "&";
        var strReturnTime = "arrTime=" + $.trim($("#CP_ddlDropFromTime option:selected").text()) + ":" + $.trim($("#CP_ddlDropMinFrom option:selected").text()) + "&";
        var isdriverage = "isdriverage=" + $("#CP_chkDriver").is(":checked").toString() + "&";
        var strdriverage = "";
        if ($("#CP_chkDriver").is(":checked")) {
            strdriverage = "driverage=" + 25 + "&";
        }
        else {
            strdriverage = "driverage=" + $("#CP_txtDiverAge").val() + "&";
        }
        var strRedeemPoints;
        if ($("#CP_CheckBoxRedeem_Car").is(":checked")) {
            strRedeemPoints = "isRedeemMiles=" + "true" + "&";
        }
        else {
            strRedeemPoints = "isRedeemMiles=" + "false" + "&";
        }

        var hdnddlFromTime = "hdnddlFromTime=" + $("#CP_hdnddlFromTime").val() + "&";
        var hdnddlToTime = "hdnddlToTime=" + $("#CP_hdnddlToTime").val();
        var queryString = strPickupCountry + strDropofCountry + strPickupCity + strDropofCity + strPickupLocation + strPickupLocationName + strDropoffLocation + strDropoffLocationName + isdropoff + strDepartDate + strDepartTime + strReturnDate + strReturnTime + isdriverage + strdriverage + hdnddlFromTime + strRedeemPoints + hdnddlToTime;
        window.location = "CarSearchWait.aspx?" + queryString;
        return false;
    }
}

function GetCarList() {

    $.ajax({
        type: 'POST',
        url: 'CarList.aspx/SetCarTemplate',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        data: '',
        success: function (msg) {

            try {

                var CarSearchRequest = $.parseJSON(msg.d[1]);
                $("#spnpickuplocation").text(CarSearchRequest.pickUp.location.name);
                $("#spndroppoffLocation").text(CarSearchRequest.dropOff.location.name);
                $("#spndriverresidenceCountry").text(msg.d[7].toString());

                $("#spndriverCountry").text(msg.d[6].toString());//Driver country code
                var PickUpDate = CarSearchRequest.pickUp.dateTime;
                var DropoffDate = CarSearchRequest.dropOff.dateTime;

                $("#spnpickupdate").text(PickUpDate);
                $("#spndropoffdate").text(DropoffDate);
                var date = PickUpDate.split('T');

                if (date[0] != '' || date[0] != null) {

                    var dateAr = date[0].split('-');
                    var newDate = dateAr[2] + '/' + dateAr[1] + '/' + dateAr[0];
                    $("#txtDropoffDate").datepicker('destroy');
                    $("#txtDropoffDate").datepicker({
                        minDate: newDate,
                        numberOfMonths: 1,
                        dateFormat: 'dd/mm/yy',
                        onSelect: function (dateReturnText, inst) {
                            return false;
                        }
                    });

                }
                //set modify search form
                $("#txtpickupLocation").val(CarSearchRequest.pickUp.location.name);
                $("#hndpickupLocationId").val(CarSearchRequest.pickUp.location.id);

                $("#txtDopoffLocation").val(CarSearchRequest.dropOff.location.name);
                $("#hndDopoffLocationId").val(CarSearchRequest.dropOff.location.id);

                var pickupday = $.trim(CarSearchRequest.pickUp.date).split('-')[2];
                var pickupmonth = $.trim(CarSearchRequest.pickUp.date).split('-')[1];
                var pickupyear = $.trim(CarSearchRequest.pickUp.date).split('-')[0];
                var PickupformatedDate = pickupday + '/' + pickupmonth + '/' + pickupyear;

                var Dropoffday = $.trim(CarSearchRequest.dropOff.date).split('-')[2];
                var Dropoffmonth = $.trim(CarSearchRequest.dropOff.date).split('-')[1];
                var Dropoffyear = $.trim(CarSearchRequest.dropOff.date).split('-')[0];
                var DropformatedDate = Dropoffday + '/' + Dropoffmonth + '/' + Dropoffyear;

                $("#txtpickupDate").val(PickupformatedDate);
                $('#ddlPickupTime').val(CarSearchRequest.pickUp.Time);

                $('#txtDropoffDate').val(DropformatedDate);
                $('#ddlDropoffTime').val(CarSearchRequest.dropOff.Time);

                //set modify search form

            } catch { }

            var Template = msg.d[0];
            $("#divCarListContainer")[0].innerHTML = Template.toString(); //Car List
            $("#spancarcount").text(msg.d[2]); //Car Count 
            //$("#filterPickup")[0].innerHTML = msg.d[3].toString(); //Pickupfilter HTML
            //$("#filterSupplier")[0].innerHTML = msg.d[4].toString(); //Supplierfilter HTML
            var driverage = msg.d[5].toString(); //Driver's age
            if (driverage == null || driverage == "0") {
                $('#chkDriverAge').prop('checked', true);
            }
            else {
                $("#txtDriverAge").val(driverage);
                $("#txtDriverAge").show();
            }
        },
        error: function (e) {
        }
    });
}
function bindcountry() {
    var countryvalue = $("#spndriverresidenceCountry").html();
    var countrycode = $("#spndriverCountry").html();
    $('#txtDriverResidence option[value="' + countrycode + '"]').attr("selected", "selected");
    if ($("#txtDriverAge").val() != '') {
        $('#chkDriverAge').prop('checked', false);
        $(".dvInput2").show();
    }
    else {
        $('#chkDriverAge').prop('checked', true);
        $(".dvInput2").hide();
    }
    if ($("#txtDopoffLocation").val() != '') {
        $('#chkDropoffLocation').prop('checked', false);
        $(".dvInput1").show();
    }
    else {
        $('#chkDropoffLocation').prop('checked', true);
        $(".dvInput1").hide();
    }
    if ($("#txtDopoffLocation").val() == $("#txtpickupLocation").val()) {
        $('#chkDropoffLocation').prop('checked', true);
        $(".dvInput1").hide();
    }
}

function PickupFilter(vehicleAtId) {

    var checkBox = $('#gridChkPikup' + vehicleAtId);

    if (checkBox.is(':checked')) {

        $.ajax({
            type: 'POST',
            url: 'CarList.aspx/PickupFilter',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'pstrvehicleAtId':'" + vehicleAtId + "'}",
            cache: false,
            success: function (msg) {
                var Template = msg.d[0];
                $("#divCarListContainer")[0].innerHTML = Template.toString(); //Car List
                $("#spancarcount").text(msg.d[1]); //Car Count 
            },
            error: function (errmsg) {

            }
        });

    }
    else {
        FilterCarList("");
    }


}

function SupplierFilter(supplierId) {

    var checkBox = $('#gridChkPikup' + supplierId);

    if (checkBox.is(':checked')) {

        $.ajax({
            type: 'POST',
            url: 'CarList.aspx/SupplierFilter',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'pstrsupplierId':'" + supplierId + "'}",
            cache: false,
            success: function (msg) {
                var Template = msg.d[0];
                $("#divCarListContainer")[0].innerHTML = Template.toString(); //Car List
                $("#spancarcount").text(msg.d[1]); //Car Count 
            },
            error: function (errmsg) {

            }
        });
    }
    else {
        FilterCarList("");
    }


}

function ViewMoreInfo(uniqueRef) {

    $.ajax({
        type: 'POST',
        url: 'CarList.aspx/GetMoreInfoDetails',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstruniqueRefNo':'" + uniqueRef + "'}",
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                $("#divmoreInfoDetails")[0].innerHTML = msg.d[0].toString(); //Car more Info popup

                $("#dvMoreInfoModal").modal("show");

                updateVcDataSections();
            }
            else {
                window.location.href = "ErrorPage.aspx";
            }
        },
        error: function (errmsg) {

        }
    });



}

function ViewDeal(uniqueRef) {

    try {
        window.location = "CarDetails.aspx?uniqueRefId=" + uniqueRef;

    } catch (e) {
    }
}

function GetCarDetails(SelectedCarId) {

    $.ajax({
        type: 'POST',
        url: 'CarDetails.aspx/GetCarDetails',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstruniqueRefNo':'" + SelectedCarId + "'}",
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                var CarSearchRequest = $.parseJSON(msg.d[2]);
                var Vehicle = $.parseJSON(msg.d[6]);

                $("#hndRatereference").val(Vehicle.packages[0].rateReference);

                try {

                    $("#spnpickupDetails").text(CarSearchRequest.pickUp.location.name);
                    $("#spnpickupDate").text(CarSearchRequest.pickUp.dateTime);

                    $("#spndropoffDetails").text(CarSearchRequest.dropOff.location.name);
                    $("#spndropoffDate").text(CarSearchRequest.dropOff.dateTime);

                } catch { }

                $("#divCarDetailsLeftpannel")[0].innerHTML = msg.d[3].toString(); //car details Left pannel
                $("#divpaymentOptionContainer")[0].innerHTML = msg.d[0].toString(); //Payment Options
                $("#divExtrascontainer")[0].innerHTML = msg.d[1].toString(); // extras details i.e. Excess Protection
                $("#divAdditionalEquipment")[0].innerHTML = msg.d[4].toString(); // Additional Equipment
                $("#spncarhireAmount").text(msg.d[5].toString());//Total car hire Amount
                $("#divmoreInfoDetails")[0].innerHTML = msg.d[7].toString(); //car More Info Popup html
                $("#spncarTotalAmount").text(msg.d[5].toString());
                $("#spnPayableAmount").text(msg.d[5].toString());

                updateVcDataSections();
            }
            else {
                window.location.href = "ErrorPage.aspx";
            }
        }, beforeSend: function () {
            $("#updProgress").show();
        },
        error: function (errmsg) {

        }
    });

}

function GetRateDetails(RateReference) {

    $("#hndRatereference").val(RateReference);

    $.ajax({
        type: 'POST',
        url: 'CarDetails.aspx/GetRateDetails',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrRateReference':'" + RateReference + "'}",
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                $("#divExtrascontainer")[0].innerHTML = msg.d[0].toString(); //extras details i.e. Excess Protection
                $("#divAdditionalEquipment")[0].innerHTML = msg.d[1].toString(); // Additional Equipment
                $("#spnPayableAmount").text(msg.d[2].toString());
                updateVcDataSections();
            }
            else {
                window.location.href = "ErrorPage.aspx";
            }
        },
        error: function (errmsg) {

        }
    });
}

function AddRemoveAditionalCharges(Action, name, amount, rateRef, ProductId) {

    $.ajax({
        type: 'POST',
        url: 'CarDetails.aspx/AddRemoveAditionalCharges',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrAction':'" + Action + "','pstrname':'" + name + "','pstramount':'" + amount + "','pstrProductId':'" + ProductId + "'}",
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                var CarBookingDetails = $.parseJSON(msg.d[0]);
             
                var FinalAmount = parseFloat(CarBookingDetails.CarHireAmount);
                var TotalExcessChargeAmount = parseFloat(CarBookingDetails.TotalAdditionalchargesAmount == null ? 0 : CarBookingDetails.TotalAdditionalchargesAmount) + parseFloat(CarBookingDetails.TotalAdditionalequipmentAmount == null ? 0 : CarBookingDetails.TotalAdditionalequipmentAmount);

                var completeHTML = "";
                if (Action == "ADD") {

                    if (name == "Excess Protection") {

                        $("#hndIsExcessprotectionAdded").val("true");
                    }

                    try {

                        $("#ADD_" + ProductId).addClass('d-none');
                        $("#REMOVE_" + ProductId).removeClass('d-none');


                    } catch { }


                    for (count = 0; count < CarBookingDetails.AdditonalCharges.length; count++) {

                        if (!CarBookingDetails.AdditonalCharges[count].IsAdditionalEquipments) {

                            completeHTML += " <div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                            completeHTML += " </div>";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].amount).toFixed(2) + " Points</p>";
                            completeHTML += " </div>";
                            completeHTML += " </div>";
                        }
                        else {

                            completeHTML += "<div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Quantity + "x " + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                            completeHTML += "</div>";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].TotalChargeamount).toFixed(2) + " Points</p>";
                            completeHTML += "</div>";
                            completeHTML += "</div>";
                        }
                    }

                    $("#divAdditionaCharges")[0].innerHTML = completeHTML;

                    $("#spncarTotalAmount").text(Math.ceil(parseFloat(FinalAmount + TotalExcessChargeAmount).toFixed(2)));
                    $("#spnPayableAmount").text(Math.ceil(parseFloat(FinalAmount + TotalExcessChargeAmount).toFixed(2)));
                }
                else {

                    if (name == "Excess Protection") {
                        $("#hndIsExcessprotectionAdded").val("False");
                    }

                    for (count = 0; count < CarBookingDetails.AdditonalCharges.length; count++) {

                        if (!CarBookingDetails.AdditonalCharges[count].IsAdditionalEquipments) {

                            completeHTML += " <div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                            completeHTML += " </div>";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].amount).toFixed(2) + " Points</p>";
                            completeHTML += " </div>";
                            completeHTML += " </div>";
                        }
                        else {

                            completeHTML += "<div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Quantity + "x " + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                            completeHTML += "</div>";
                            completeHTML += "<div class=\"col-6\">";
                            completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].TotalChargeamount).toFixed(2) + " Points</p>";
                            completeHTML += "</div>";
                            completeHTML += "</div>";
                        }
                    }

                    $("#divAdditionaCharges")[0].innerHTML = completeHTML;

                    $("#spncarTotalAmount").text(Math.ceil(parseFloat(FinalAmount + TotalExcessChargeAmount).toFixed(2)));
                    $("#spnPayableAmount").text(Math.ceil(parseFloat(FinalAmount + TotalExcessChargeAmount).toFixed(2)));

                    $("#ADD_" + ProductId).removeClass('d-none');
                    $("#REMOVE_" + ProductId).addClass('d-none');

                    $("#AdditionalChrg_" + ProductId).remove();

                }
            }
            else {
                window.location.href = "ErrorPage.aspx";
            }
        },
        error: function (errmsg) {

        }
    });
}

function CreateCarPayment() {

    var IsExcessprotectionAdded = $("#hndIsExcessprotectionAdded").val();
    var Amount = $("#hndExessProtection").val();
    $("#spnExcessAmount").text(Amount + " USD");
    $("#spnExcessAmount1").text(Amount);

    //if (IsExcessprotectionAdded != "true") {

    //    $("#dvExcessProtectionModal").modal("show");
    //}
    //else {
    //    window.location.href = "CarPayment.aspx";
    //}
    window.location.href = "CarPayment.aspx";
}

function AddExcessprotection() {

    var ExessProtectiondata = $("#hndExessProtectiondata").val();

    //alert(ExessProtectiondata);

    var code = ExessProtectiondata.split('|')[0];
    var Amount = ExessProtectiondata.split('|')[1];

    AddRemoveAditionalCharges("ADD", "Excess Protection", Amount, "", code);

    window.location.href = "CarPayment.aspx";
}

function MapCarDetails() {

    $.ajax({
        type: 'POST',
        url: 'CarPayment.aspx/MapCarDetails',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: '',
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                var CarSearchRequest = $.parseJSON(msg.d[1]);
                var CarBookingDetails = $.parseJSON(msg.d[2]);
                var completeHTML = "";
                try {

                    $("#spnpickupDetails").text(CarSearchRequest.pickUp.location.name);
                    $("#spnpickupDate").text(CarSearchRequest.pickUp.dateTime);

                    $("#spndropoffDetails").text(CarSearchRequest.dropOff.location.name);
                    $("#spndropoffDate").text(CarSearchRequest.dropOff.dateTime);

                } catch { }

                $("#divCarDetailsLeftpannel")[0].innerHTML = msg.d[0].toString(); //car details Left pannel
                $("#divmoreInfoDetails")[0].innerHTML = msg.d[3].toString(); //car More Info Popup html

                for (count = 0; count < CarBookingDetails.AdditonalCharges.length; count++) {

                    if (!CarBookingDetails.AdditonalCharges[count].IsAdditionalEquipments) {

                        completeHTML += " <div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                        completeHTML += " </div>";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].amount).toFixed(2) + " Points</p>";
                        completeHTML += " </div>";
                        completeHTML += " </div>";
                    }
                    else {

                        completeHTML += "<div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Quantity + "x " + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                        completeHTML += "</div>";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].TotalChargeamount).toFixed(2) + " Points</p>";
                        completeHTML += "</div>";
                        completeHTML += "</div>";
                    }
                }
                $("#divAdditionaCharges")[0].innerHTML = completeHTML;

                $("#spncarTotalAmount").text(Math.ceil(parseFloat(CarBookingDetails.PayableAmount)));
                $("#spncarhireAmount").text(Math.ceil(parseFloat(CarBookingDetails.CarHireAmount)));
                $("#spnPayableAmount").text(Math.ceil(parseFloat(CarBookingDetails.PayableAmount)));
          //$("#spnAdditionalChargetotal").text(parseFloat(CarBookingDetails.TotalAdditionalequipmentAmount == null ? 0 : CarBookingDetails.TotalAdditionalequipmentAmount).toFixed(2));

                updateVcDataSections();
            }
            else {
                window.location.href = "ErrorPage.aspx";
            }
        },
        error: function (errmsg) {

        }
    });
}

function AddAdditionalCharges(a) {
    var select_id = document.getElementById(a);
    var test = select_id.options[select_id.selectedIndex].value;
    var id = a;
    var si = test;
    var Aditionalchargename = $("#spnaditionalchargename_" + id).text();
    var Aditionalchargeamount = $("#spnaditionalchargeamount_" + id)[0].innerHTML;
    var totalperticularAdditionalcharge = $("#hndTotaladitionalchargeamount_" + id).val();
    var totalAdditionalcharge = $("#hndTotaladitionalchargeamount").val();
    var totalamount = $("#spncarTotalAmount").text();
    var CarhireAmount = $("#spncarhireAmount").text();
    $.ajax({
        type: 'POST',
        url: 'CarDetails.aspx/AddRemoveAditionalEquipment',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrname':'" + Aditionalchargename + "','pstramount':'" + Aditionalchargeamount + "','pstrProductId':'" + id + "','pstrQnty':'" + si + "'}",
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                var CarBookingDetails = $.parseJSON(msg.d[0]);

                var completeHTML = "";
                var TotalExcessChargeAmount = CarBookingDetails.TotalAdditionalchargesAmount + CarBookingDetails.TotalAdditionalequipmentAmount;

                for (count = 0; count < CarBookingDetails.AdditonalCharges.length; count++) {

                    if (CarBookingDetails.AdditonalCharges[count].IsAdditionalEquipments) {

                        completeHTML += "<div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Quantity + "x " + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                        completeHTML += "</div>";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].TotalChargeamount) + " Points</p>";
                        completeHTML += "</div>";
                        completeHTML += "</div>";
                    }
                    else {
                        completeHTML += " <div id=\"AdditionalChrg_" + CarBookingDetails.AdditonalCharges[count].Code + "\" class=\"d-flex justify-content-between align-items-center pt-2 pb-2\">";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p>" + CarBookingDetails.AdditonalCharges[count].Name + "</p>";
                        completeHTML += " </div>";
                        completeHTML += "<div class=\"col-6\">";
                        completeHTML += "<p class=\"text-right\">" + parseFloat(CarBookingDetails.AdditonalCharges[count].amount) + " Points</p>";
                        completeHTML += " </div>";
                        completeHTML += " </div>";
                    }
                }

                $("#divAdditionaCharges")[0].innerHTML = completeHTML;

                var FinalAmount = parseFloat(CarBookingDetails.TotalAdditionalequipmentAmount == null ? 0 : CarBookingDetails.TotalAdditionalequipmentAmount) + parseFloat(CarBookingDetails.TotalAdditionalchargesAmount == null ? 0 : CarBookingDetails.TotalAdditionalchargesAmount) + parseFloat(CarBookingDetails.CarHireAmount);

                $("#spncarTotalAmount").text(Math.ceil(parseFloat(FinalAmount.toFixed(2))));
                $("#spnPayableAmount").text(Math.ceil(parseFloat(FinalAmount.toFixed(2))));


            }
            else {
                window.location.href = "ErrorPage.aspx";
            }
        },
        error: function (errmsg) {

        }
    });
}

function MakePayment() {

    var msg = "";
    $("#errorFirtsname").html("");
    $("#errorSurName").html("");
    $("#errorEmailId").html("");
    $("#errorMobileNo").html("");
    $("#errorTnC").html("");


    if ($("#CP_txtFirstName").val().length == 0) {
        msg += "Please enter First Name. <br/>";
        $("#errorFirtsname").html("<span>Please enter First name.</span>");
    }
    else {
        if (AcceptAlphasonly($("#CP_txtFirstName").val().trim())) {
            msg += "FirstName Accept Alphabets Only.<br/>";
            $("#errorFirtsname").html("<span>Firstname Accept Alphabets Only.</span>");
        }
    }
    if ($("#CP_txtSurName").val().length == 0) {
        $("#errorSurName").html("<span>Please enter Surname.</span>");
        msg += "Please enter SurName. <br/>";
    }
    else {
        if (AcceptAlphasonly($("#CP_txtSurName").val().trim())) {
            $("#errorSurName").html("<span>Surname Accept Alphabets Only.</span>");
            msg += "SurName Accept Alphabets Only.<br/>";
        }
    }
    if ($('#CP_txtEmailId').val() == '') {
        $("#errorEmailId").html("<span>Please enter Email.</span>");
        msg += "Please enter Email. <br/>";
    }
    else {

        if (!isValidEmailAddress($('#CP_txtEmailId').val().trim())) {
            $("#errorEmailId").html("<span>Please enter Valid Email-ID.</span>");
            msg += "Please enter Valid Email-ID.<br/>";
        }
    }
    if ($("#CP_txtMobileNo").val().length == 0) {
        $("#errorMobileNo").html("<span>Please enter Mobile no.</span>");
        msg += "Please enter Mobile no. <br/>";
    }
    else {
        if (!AcceptNumbersonly($("#CP_txtMobileNo").val().trim())) {
            {
                $("#errorMobileNo").html("<span>Mobile no Accept numbers Only.</span>");
                msg += "Mobile no Accept numbers Only.<br/>";
            }
        }
    }


    var $Checkbox = $('#chkTnC');
    if ($Checkbox.is(':checked') === false) {

        $("#errorTnC").html("<span>Please Accept Terms & Condition.</span>");
        msg += "Please Accept Terms & Condition.<br/>";
    }

    if (msg.length > 0) {
        return false;
    }
}

function getMonthName(monthNumber) {

    const date = new Date();
    date.setMonth(monthNumber - 1);

    return date.toLocaleString('en-US', { month: 'long' });
}

function AcceptNumbersonly(number) {
    var pattern = new RegExp(/^[0-9]+$/);
    var Isvalid = pattern.test(number);
    return Isvalid;
};

function AcceptAlphasonly(char) {
    var pattern = new RegExp(/[^a-zA-Z]+/);
    var Isvalid = pattern.test(char);
    return Isvalid;
};

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}

function FilterCarList(GetResult) {

    var TransmissioncheckBoxList = $('input:checkbox[name=Transmission]');
    var Transmissionvalues = '';
    var PASSENGERScheckBoxList = $('input:checkbox[name=PASSENGERS]');
    var PASSENGERSvalues = '';
    var VehicleTypecheckBoxList = $('input:checkbox[name=VehicleType]');
    var VehicleTypevalues = '';


    if (GetResult != "All") {

        for (var i = 0; i < TransmissioncheckBoxList.length; i++) {
            if (TransmissioncheckBoxList[i].checked) {
                if (Transmissionvalues != '') {
                    Transmissionvalues += ',' + TransmissioncheckBoxList[i].value;
                } else {
                    Transmissionvalues += TransmissioncheckBoxList[i].value;
                }
            }
        }

        for (var i = 0; i < PASSENGERScheckBoxList.length; i++) {
            if (PASSENGERScheckBoxList[i].checked) {
                if (PASSENGERSvalues != '') {
                    PASSENGERSvalues += ',' + PASSENGERScheckBoxList[i].value;
                } else {
                    PASSENGERSvalues += PASSENGERScheckBoxList[i].value;
                }
            }
        }

        for (var i = 0; i < VehicleTypecheckBoxList.length; i++) {
            if (VehicleTypecheckBoxList[i].checked) {
                if (VehicleTypevalues != '') {
                    VehicleTypevalues += ',' + VehicleTypecheckBoxList[i].value;
                } else {
                    VehicleTypevalues += VehicleTypecheckBoxList[i].value;
                }
            }
        }
    }
    else {
        for (var i = 0; i < TransmissioncheckBoxList.length; i++) {

            TransmissioncheckBoxList[i].checked = false;
        }

        for (var i = 0; i < PASSENGERScheckBoxList.length; i++) {

            PASSENGERScheckBoxList[i].checked = false;
        }

        for (var i = 0; i < VehicleTypecheckBoxList.length; i++) {

            VehicleTypecheckBoxList[i].checked = false;
        }

    }

    $.ajax({
        type: 'POST',
        url: 'CarList.aspx/FilterCarList',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrTransmissionvalues':'" + Transmissionvalues + "','pstrPassengersvalues':'" + PASSENGERSvalues + "','pstrVehicleTypevalues':'" + VehicleTypevalues + "'}",
        async: false,

        success: function (msg) {

            var Template = msg.d[0];
            $("#divCarListContainer")[0].innerHTML = Template.toString(); //Car List
            $("#spancarcount").text(msg.d[1]); //Car Count 

        },
        error: function (e) {
        }
    });

}

function ViewMoreInfoAdditionalCharges(CodeId) {

    var completeHTML = "";

    $.ajax({
        type: 'POST',
        url: 'CarDetails.aspx/GetMoreInfoAdditionalCharges',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrCodeId':'" + CodeId + "'}",
        cache: false,
        success: function (msg) {

            if (msg.d[0] != "Failed") {

                var extras = $.parseJSON(msg.d[0]);

                completeHTML += "<div class=\"col-12 productDetails\">";
                completeHTML += "<div class=\"row justify-content-between align-items-center mb-3\">";
                completeHTML += "<div class=\"col-12 col-sm-8 col-md-8 col-lg-10 order-md-0\">";
                completeHTML += "<p><i class=\"fa fa-clipboard mr-2\" aria-hidden=\"true\"></i> <span class=\"text-colour7 heading-semibold\">" + extras.name + "</span></p>";
                completeHTML += "</div>";
                completeHTML += "<div class=\"col-12 col-sm-4 col-md-4 col-lg-2 mt-2 mt-md-0 order-md-1 text-left\">";
                completeHTML += "<span class=\"text-colour7 heading-semibold\"> " + extras.rentalPrice.display.amount + " Points</span>"; // mapp dynamic amount pending.
                completeHTML += "</div>";
                completeHTML += "</div>";
                completeHTML += "<div class=\"row dvSeat\">";
                completeHTML += "<div class=\"col-12\">";
                completeHTML += "<p class=\"\"><i class=\"fa fa-clipboard mr-2\" aria-hidden=\"true\"></i><span class=\"text-colour7 heading-semibold\">" + extras.name + "</span></p>";
                completeHTML += "</div>";
                completeHTML += "</div>";
                completeHTML += "</div>";

                $("#divMoreinfoAdditionalEquipment")[0].innerHTML = completeHTML;

                $("#dvAdditionalEquipmentModal").modal("show");

                updateVcDataSections();
            }
            else {

            }
        },
        error: function (errmsg) {

        }
    });



}





