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

function bindCarDatepicker() {
    $("#CP_CarTextBoxCheckin").datepicker({
        minDate: minimumtime(),
        numberOfMonths: 2,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
            $.ajax({
                type: 'POST',
                url: 'Index.aspx/GetPickupTime',
                //url: 'CarSearch.aspx/GetPickupTime',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'Pickupdate':'" + dateText + "','LocationId':'" + $("#CP_DDLLocation").find('option:selected').prop("value") + "'}",
                cache: false,
                success: function (data) {
                    //show Timepicker
                    //debugger;
                    $("#CP_ddlFromTime").empty();
                    var dateList = data.d;
                    var Time = -1
                    var hdnddlFromTime
                    var count = 0;

                    $("#CP_CarValidationError").hide();
                    if (data.d != '000000000000000000000000') {
                        var initialized = 0;
                        for (var count = 0; count <= dateList.length; count++) {
                            var times;
                            Time++;
                            if (data.d.charAt(count) == '1') {
                                if (Time < 10) {
                                    $("#CP_ddlFromTime").append($("<option>" + "0" + Time + "</option>"));
                                    times = "0" + Time;
                                }
                                else {
                                    $("#CP_ddlFromTime").append($("<option>" + Time + "</option>"));
                                    times = Time;
                                }
                               if (initialized == 0) {
                                    hdnddlFromTime = times;
                                    initialized = 1;
                                }
                                else {
                                    hdnddlFromTime = hdnddlFromTime + "," + times;
                                }
                            }
                        }
                        document.getElementById('CP_ddlFromTime').selectedIndex = 0;
                        //$("#CP_DivFromHour").html($("#CP_ddlFromTime option:first-child").val());
                    }
                    else {
                        $("#CP_CarValidationError").show();
                        $("#CP_CarValidationError")[0].innerHTML = 'Car is not Available for this Date';
                    }
                    $("#CP_hdnddlFromTime").val(hdnddlFromTime);
                    $("#CP_ddlFromTime").selectric('refresh');
                },
                error: function (errmsg) {

                    alert(errmsg.d);

                }
            });

            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#CP_CarTextBoxCheckout").datepicker('destroy');


            $("#CP_CarTextBoxCheckin").val(dateText.toString());
            $("#CP_CarSearchDivCheckin").html(dateText.toString());
            $("#CP_CarSearchDivCheckout").html("");
            $("#CP_CarTextBoxCheckout").val('');
            $("#CP_CarSearchLabelCheckout").text("");
            $("#CP_CarTextBoxCheckout").datepicker("destroy");
            var locationId;
            if ($('#CP_droploction').attr('checked')) {
                locationId = $("#CP_DDLLocation").find('option:selected').prop("value");
            }
            else {
                locationId = $("#CP_ddlDropLocation").find('option:selected').prop("value");
            }
            $("#CP_CarTextBoxCheckout").datepicker({
                minDate: dateText,
                numberOfMonths: 2,
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateReturnText, inst) {

                    if ($('#CP_droploction').attr('checked')) {
                        locationId = $("#CP_DDLLocation").find('option:selected').prop("value");
                    }
                    else {
                        locationId = $("#CP_ddlDropLocation").find('option:selected').prop("value");
                    }
                    getDrophourDetails(dateReturnText, locationId);
                    $("#CP_CarSearchDivCheckout").html("");
                    $("#CP_CarSearchDivCheckout").html(dateReturnText.toString());
                    $("#CP_CarTextBoxCheckout").val(dateReturnText.toString());
                    $("#CP_CarSearchLabelCheckout").text("");
                    $("#CP_CarSearchLabelCheckout").text(dateReturnText);
                    return false;
                }
            });
            $("#CP_CarTextBoxCheckout").val(oneDay);
            $("#CP_CarSearchLabelCheckin").text("").text(dateText.toString());

            getDrophourDetails(dateText.toString(), $("#CP_DDLLocation").find('option:selected').prop("value"));
        }
    });
}
function bindCarMobDatepicker() {
    $("#CP_CarTextBoxCheckin").datepicker({
        minDate: minimumtime(),
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
            $.ajax({
                type: 'POST',
                url: 'Index.aspx/GetPickupTime',
                //url: 'CarSearch.aspx/GetPickupTime',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'Pickupdate':'" + dateText + "','LocationId':'" + $("#CP_DDLLocation").find('option:selected').prop("value") + "'}",
                cache: false,
                success: function (data) {
                    //show Timepicker
                    //debugger;
                    $("#CP_ddlFromTime").empty();
                    var dateList = data.d;
                    var Time = -1
                    var hdnddlFromTime
                    var count = 0;

                    $("#CP_CarValidationError").hide();
                    if (data.d != '000000000000000000000000') {
                        var initialized = 0;
                        for (var count = 0; count <= dateList.length; count++) {
                            var times;
                            Time++;
                            if (data.d.charAt(count) == '1') {
                                if (Time < 10) {
                                    $("#CP_ddlFromTime").append($("<option>" + "0" + Time + "</option>"));
                                    times = "0" + Time;
                                }
                                else {
                                    $("#CP_ddlFromTime").append($("<option>" + Time + "</option>"));
                                    times = Time;
                                }
                                if (initialized == 0) {
                                    hdnddlFromTime = times;
                                    initialized = 1;
                                }
                                else {
                                    hdnddlFromTime = hdnddlFromTime + "," + times;
                                }
                            }
                        }
                        document.getElementById('CP_ddlFromTime').selectedIndex = 0;
                        //$("#CP_DivFromHour").html($("#CP_ddlFromTime option:first-child").val());
                    }
                    else {
                        $("#CP_CarValidationError").show();
                        $("#CP_CarValidationError")[0].innerHTML = 'Car is not Available for this Date';
                    }
                    $("#CP_hdnddlFromTime").val(hdnddlFromTime);
                    $("#CP_ddlFromTime").selectric('refresh');
                },
                error: function (errmsg) {

                    alert(errmsg.d);

                }
            });

            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#CP_CarTextBoxCheckout").datepicker('destroy');


            $("#CP_CarTextBoxCheckin").val(dateText.toString());
            $("#CP_CarSearchDivCheckin").html(dateText.toString());
            $("#CP_CarSearchDivCheckout").html("");
            $("#CP_CarTextBoxCheckout").val('');
            $("#CP_CarSearchLabelCheckout").text("");
            $("#CP_CarTextBoxCheckout").datepicker("destroy");
            var locationId;
            if ($('#CP_droploction').attr('checked')) {
                locationId = $("#CP_DDLLocation").find('option:selected').prop("value");
            }
            else {
                locationId = $("#CP_ddlDropLocation").find('option:selected').prop("value");
            }
            $("#CP_CarTextBoxCheckout").datepicker({
                minDate: dateText,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                onSelect: function (dateReturnText, inst) {

                    if ($('#CP_droploction').attr('checked')) {
                        locationId = $("#CP_DDLLocation").find('option:selected').prop("value");
                    }
                    else {
                        locationId = $("#CP_ddlDropLocation").find('option:selected').prop("value");
                    }
                    getDrophourDetails(dateReturnText, locationId);
                    $("#CP_CarSearchDivCheckout").html("");
                    $("#CP_CarSearchDivCheckout").html(dateReturnText.toString());
                    $("#CP_CarTextBoxCheckout").val(dateReturnText.toString());
                    $("#CP_CarSearchLabelCheckout").text("");
                    $("#CP_CarSearchLabelCheckout").text(dateReturnText);
                    return false;
                }
            });
            $("#CP_CarTextBoxCheckout").val(oneDay);
            $("#CP_CarSearchLabelCheckin").text("").text(dateText.toString());

            getDrophourDetails(dateText.toString(), $("#CP_DDLLocation").find('option:selected').prop("value"));
        }
    });
}
$(document).ready(function () {
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
});

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
                    window.location.href = 'Login.aspx';
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
        $("#CP_CarValidationError")[0].innerHTML = "Below fields are mandatory.";
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
            var Template = msg.d[0];
            CarList = $.parseJSON(msg.d[1]);
            $("#TotalCarCount").text(CarList.length);
            $("#CP_LoadTemplate")[0].innerHTML = Template.toString();
            $("#CarList").setTemplateElement("CarTemplate");
            $("#CarList").processTemplate(CarList);
            $(".miles").digits();
            $('#CP_hdnCarFilterRange').val(msg.d[2]);
            $('#spnSearchdetail')[0].innerHTML = msg.d[3];
        },
        error: function (e) {
        }
    });
}


