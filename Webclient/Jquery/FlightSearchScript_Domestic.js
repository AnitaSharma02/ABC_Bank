new function ($) {
    $.fn.setCursorPosition = function (pos) {
        if ($(this).get(0).setSelectionRange) {
            $(this).get(0).setSelectionRange(pos, pos);
        } else if ($(this).get(0).createTextRange) {
            var range = $(this).get(0).createTextRange();
            range.collapse(true);
            range.moveEnd('character', pos);
            range.moveStart('character', pos);
            range.select();
        }
    }
}(jQuery);
var flightDepAmt = 0;
var flightRetAmt = 0;
function showTripSummary(summaryType) {
    $.ajax({
        type: 'POST',
        url: 'FlightListForDomestic.aspx/ShowTripSummary',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrSummaryType':'" + summaryType.toString() + "'}",
        success: function (msg) {
            var strOnwardsSummary = "";
            var strOnwardFlightInfo = "";
            var strReturnFlightInfo = "";
            var strReturnSummary = "";
            var Onwardindex = 0;
            var Returndindex = 0;
            if (summaryType == "Onward") {
                var flights = msg.d[0];
                $("#hdnOnwardSelectedFlight").val("1");
            }
            else {
                var flights = msg.d[0];
                $("#hdnReturnSelectedFlight").val("1");
            }
            flight = $.parseJSON(flights);
            var strSummary = "";

            var strTotal = "";
            if (summaryType == "Onward") {
                //debugger
                for (count = 0; count < flight.length; count++) {
                    if (flight.length > 1) {
                        if (Onwardindex == 0) {
                            Onwardindex = 1;
                            lastcount = flight.length - 1;
                            strOnwardsSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight[count].Departure;
                            strOnwardsSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight[lastcount].Arrival + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight[lastcount].Fare_total + "</label></li><li><label class='trpsum-miles-type'>Points</label></li></ul></div>"
                        }
                    } else {
                        strOnwardsSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight[count].Departure;
                        strOnwardsSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight[count].Arrival + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight[count].Fare_total + "</label></li><li><label class='trpsum-miles-type'>Points+</label></li></ul></div>"
                    }

                    strOnwardFlightInfo += "<div class='trpsum-flt-logo'><img src=" + flight[count].Airline_logo + " /></div><div class='fltsumm-col-1'><ul class='fltsumm-dtrow'><li><span class='trpsum-fltname robotbold'>" + flight[count].Airline_name + "</span></li><li><span class='trpsum-fltcode'>(" + flight[count].Flight_no + ")</span></li></ul></div><div class='fltsumm-col-2'><ul class='fltsumm-dtrow'><li><span class='trpsum-c-code robotbold'>" + flight[count].Departure + "</span><img src='images/right-arrow.png' class='trpsum-c-arrow ' /><span class='trpsum-c-code robotbold'>" + flight[count].Arrival + "</span></li><li><label class='trpsum-dprtime'>" + flight[count].Departure_time + "</label><label class='trpsum-arrvtime'>" + flight[count].Arrival_time + "</label></li></ul></div><div class='fltsumm-col-3 alR'><ul class='fltsumm-dtrow'><li><label class='trpsum-dprtdate robotbold'>" + flight[count].Flight_date + "</label></li><li>1 stop</li></ul></div>";
                }
                $("#divOnwardFlightDetails").html(strOnwardsSummary);
                $("#divOnwardFlightInfo").html(strOnwardFlightInfo);
            }
            if (summaryType == "Return") {
                for (count = 0; count < flight.length; count++) {
                    if (flight.length > 1) {
                        if (Returndindex == 0) {
                            Returndindex = 1;
                            {
                                lastcount = flight.length - 1;
                                strReturnSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight[count].Departure;
                                strReturnSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight[lastcount].Arrival + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight[lastcount].Fare_total + "</label></li><li><label class='trpsum-miles-type'>Points</label></li></ul></div>"

                            }
                        }
                    } else {
                        strReturnSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight[count].Departure;
                        strReturnSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight[count].Arrival + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight[count].Fare_total + "</label></li><li><label class='trpsum-miles-type'>Points</label></li></ul></div>"
                    }
                    strReturnFlightInfo += "<div class='trpsum-flt-logo'><img src=" + flight[count].Airline_logo + " /></div><div class='fltsumm-col-1'><ul class='fltsumm-dtrow'><li><span class='trpsum-fltname'>" + flight[count].Airline_name + "</span></li><li><span class='trpsum-fltcode'>(" + flight[count].Flight_no + ")</span></li></ul></div><div class='fltsumm-col-2'><ul class='fltsumm-dtrow'><li><span class='trpsum-c-code'>" + flight[count].Departure + "</span><img src='images/right-arrow.png' class='trpsum-c-arrow ' /><span class='trpsum-c-code'>" + flight[count].Arrival + "</span></li><li><label class='trpsum-dprtime'>" + flight[count].Departure_time + "</label><label class='trpsum-arrvtime'>" + flight[count].Arrival_time + "</label></li></ul></div><div class='fltsumm-col-3 alR'><ul class='fltsumm-dtrow'><li><label class='trpsum-dprtdate'>" + flight[count].Flight_date + "</label></li><li>1 stop</li></ul></div>";
                }
                $("#divReturnFlightDetails").html(strReturnSummary);
                $("#divReturnFlightInfo").html(strReturnFlightInfo);
            }


            if (summaryType == "Onward") {
                flightDepAmt = flight[0].Fare_total;
            }
            else {
                flightRetAmt = flight[0].Fare_total;
            }
            $(".fixedBot-tripSumm").show();

            strTotal += "<ul class='fR'><li><label class='trpsumm-totalmiles'>" + parseInt(flightDepAmt + flightRetAmt) + "</label></li><li><label class='trpsum-miletype'> Total Points</label></li></ul>";
            $("#divTotal").html(strTotal);
            var strheight = $("#Trip_Summary_Main").height();
            $("#domesticTwoWay").css("padding-bottom", strheight + "px");
            //$("#divTotal").html(strSummary);
            return false;
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}
function onewayShowDomestic() {
    $("#divrtndomestic").hide();
    $("#onelidomestic").addClass("tab-act");
    $("#retlidomestic").removeClass("tab-act");
    $("#hdntripdomestic").val('false');
    return false;
}
function RoundTripShowDomestic() {
    $("#divrtndomestic").show();
    $("#onelidomestic").removeClass("tab-act");
    $("#retlidomestic").addClass("tab-act");
    $("#hdntripdomestic").val('true');
    return false;
}
$(document).ready(function () {
    $("#textBoxFromdomestic").click(function () {
        $(this).val('');
    });

    $("#textBoxTodomestic").click(function () {
        $(this).val('');
    });
    $("#textBoxFromdomestic").autocomplete({
        source: function (request, response) {
            var list = [];
            $.ajax({
                type: 'POST',
                url: 'FlightServicesDomestic.aspx/GetAirfieldsforDomestic',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'prefixText':'" + request.term.toString() + "'}",
                cache: false,
                success: function (msg) {
                    response(msg.d)
                },
                error: function (errmsg) {
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            });
            response(list);
        },
        select: function (event, ui) {
            $("#textBoxFromdomestic").val(ui.item.label);
            $("#textBoxFromdomestic").val(ui.item.value);
            $(this).setCursorPosition(2);
            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300

    });
    $("#textBoxTodomestic").autocomplete({
        source: function (request, response) {
            var list = [];
            $.ajax({
                type: 'POST',
                url: 'FlightServicesDomestic.aspx/GetAirfieldsforDomestic',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'prefixText':'" + request.term.toString() + "'}",
                cache: false,
                success: function (msg) {
                    response(msg.d)
                },
                error: function (errmsg) {
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            });
            response(list);
        },
        select: function (event, ui) {
            $("#textBoxTodomestic").val(ui.item.label);
            $("#textBoxTodomestic").val(ui.item.value);
            $(this).setCursorPosition(2);
            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300
    });
});
function placeholderOnFocusdomestic(obj, defaultVal) {
    if (obj.value == "") {
        obj.value = defaultVal;
    } else if (obj.value == defaultVal) {
        obj.value = "";
    } else { }
    $("#" + obj.id).removeClass('error');
};

$(document).ready(function () {
    var $window = $(window);

    function checkDatePickerWidthDomestic() {

        var windowsize = $window.width();
        if (windowsize > 550) {
            bindDatepickerDomestic();
        }
        else {
            bindMobDatepickerDomestic();
        }
        $("#CP_DepDatedomestic").click(function () {
            $("#txtDepartdomestic").datepicker('show');
        });

    }
    // Execute on load
    checkDatePickerWidthDomestic();
    // Bind event listener
    $(window).resize(checkDatePickerWidthDomestic);
});
function bindDatepickerDomestic() {
    $("#txtDepartdomestic").datepicker({
        minDate: 3,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',//'yy-mm-dd',
        onSelect: function (dateText, inst) {
            // $('#CP_TextBoxCheckin').datepicker('option', 'minDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#txtReturndomestic").datepicker("destroy");
            $("#txtReturndomestic").val(oneDay);
            $("#txtReturndomestic").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });

            return false;
        }

    });
    //$("#txtReturndomestic").datepicker({
    //    minDate: 4,
    //    numberOfMonths: 1,
    //    buttonImageOnly: true,
    //    dateFormat: 'dd/mm/yy'
    //});
}
function bindMobDatepickerDomestic() {
    $("#txtDepartdomestic").datepicker({
        minDate: 3,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
            //$('#CP_TextBoxCheckin').datepicker('option', 'minDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#txtReturndomestic").datepicker("destroy");
            $("#txtReturndomestic").val(oneDay);
            $("#txtReturndomestic").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });

            return false;
        }

    });
    //$("#txtReturndomestic").datepicker({
    //    minDate: 4,
    //    numberOfMonths: 1,
    //    buttonImageOnly: true,
    //    dateFormat: 'dd/mm/yy'
    //});
}

function FlightValidationDomestic() {
    //debugger
    var msg = "";
    $('input').removeClass("error");
    $('div').removeClass("error");
    $("#requiredValidationdomestic").empty();

    if (($("#textBoxFromdomestic").val() == '' || $("#textBoxFromdomestic").val() == null) || $("#textBoxFromdomestic").val() == "Enter City or Airport") {
        msg += "*Enter departure Location.<br>";
        $("#textBoxFromdomestic").addClass("error");
    }
    if (($("#textBoxTodomestic").val() == '' || $("#textBoxTodomestic").val() == null) || $("#textBoxTodomestic").val() == "Enter City or Airport") {
        msg += "*Enter destination Location.<br>";
        $("#textBoxTodomestic").addClass("error");
    }
    if (($("#txtDepartdomestic").val() == '' || $("#txtDepartdomestic").val() == null || $("#txtDepartdomestic").val() == 'Enter Date')) {
        msg += "*Enter departure date.<br>";
        $("#txtDepartdomestic").addClass("error");
    }
    if ($("#hdntripdomestic").val() == "true") {
        if ($("#txtReturndomestic").val() == '' || $("#txtReturndomestic").val() == null || $("#txtReturndomestic").val() == 'Enter Date') {
            msg += "*Enter return date.<br>";
            $("#txtReturndomestic").addClass("error");
        }
    }
    if (msg.length > 0) {
        $("#requiredValidationdomestic").show();
        $("#requiredValidationdomestic")[0].innerHTML = "<span class='heading-semibold text-danger d-block' data-i18n='flight-below-fields'>Below fields are mandatory.</span>";
        return false;
    }
    else {
        return flightsearchDomestic();
    }
};
function flightsearchDomestic() {
    function formatDate(input) {
        //debugger
        var datePart = input.match(/\d+/g),
            year = datePart[2], // get 4 digits
            month = datePart[1],
            day = datePart[0];

        return year + '-' + month + '-' + day;
    }
    //var strFrom = "departure=" + $("#textBoxFromdomestic").val().split(',')[0] + "&";
    //var strdepCity = "DepCity=" + $("#textBoxFromdomestic").val().split(',')[1] + "&";
    //var strTo = "arrival=" + $("#textBoxTodomestic").val().split(',')[0] + "&";
    //var strarrCity = "arrCity=" + $("#textBoxTodomestic").val().split(',')[1] + "&";
    //var strDepartDate = "departuredate=" + formatDate($("#txtDepartdomestic").val()) + "&";
    //var strReturnDate = "arrivaldate=" + formatDate($("#txtReturndomestic").val()) + "&";
    //var isReturn = "isReturn=" + $("#hdntripdomestic").val().toString() + "&";
    //var strAdultNo = "adult=" + $("#DropDownListAdultdomestic").val() + "&";
    //var strChildNo = "child=" + $("#DropDownListChilddomestic").val() + "&";

    //var queryString = strFrom + strdepCity + strTo + strarrCity + strDepartDate + strReturnDate + isReturn   + strAdultNo + strChildNo;
    //window.location = "SearchPageDomestic.aspx?" + queryString;
    var departureCity = $("#textBoxFromdomestic").val().split(',')[0];
    var departure = $("#textBoxFromdomestic").val().split(',')[1];
    var departuredate = formatDate($("#txtDepartdomestic").val());
    var isReturn = "";
    if ($("#returnSelectMenuDomestic").val() == "Return") {
        isReturn = true;
    } else {
        isReturn = false;
    }
    var arrivalCity = $("#textBoxTodomestic").val().split(',')[0];
    var arrival = $("#textBoxTodomestic").val().split(',')[1];
    var arrivaldate = "";
    if (isReturn) {
        arrivaldate = formatDate($("#txtReturndomestic").val());
    }
    var adult = $("#qtyValueAdultDomestic").val();
    var child = $("#qtyValueChildDomestic").val();

    var arrData = {};
    arrData.departureCity = departureCity;
    arrData.departure = departure;
    arrData.arrivalCity = arrivalCity;
    arrData.arrival = arrival;
    arrData.departuredate = departuredate;
    arrData.isReturn = isReturn;
    arrData.arrivaldate = arrivaldate;
    arrData.adult = adult;
    arrData.child = child;

    $.ajax({
        type: 'POST',
        url: "../SearchPageDomestic.aspx/FlightSearchforDomestic",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(arrData),
        success: function (msg) {
            if (msg.d)
                window.location = "FlightListForDomestic.aspx";
            else
                window.location = "NoResultFound.aspx?ERR=RESULTNOTFOUND&FROM=FLIGHT";

            $("#updProgress").hide();
        },
        error: function (jqXHR, status, errorThrown) {
            window.location = "NoResultFound.aspx?ERR=RESULTNOTFOUND&FROM=FLIGHT";
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    return false;
}

function BookNowClick_Domestic() {
    //debugger;
    $.ajax({
        type: 'POST',
        url: 'FlightServicesDomestic.aspx/BookNowClick_Domestic',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "",
        cache: false,
        success: function (msg) {
            if (msg.d)
                window.location = "FlightPassengerForDomestic.aspx";
            else
                window.location = "Index.aspx";
        },
        beforeSend: function () {
            $("#updProgress").show();
        },
        error: function (errmsg) {

        }
    });
}

function showModifyFlight_Domestic() {
    $.ajax({
        type: 'POST',
        url: 'FlightListForDomestic.aspx/ModifySearchData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "",
        success: function (msg) {
            var objModifySerach = $.parseJSON(msg.d[0]);
            var DepartDate = msg.d[1];
            var ArrivalDate = msg.d[2];
            $("#textBoxFromdomestic").val(objModifySerach.DeptCity + "," + objModifySerach.OriginLocation);
            $("#textBoxTodomestic").val(objModifySerach.ArrivalCity + "," + objModifySerach.DestinationLocation);
            //$("#DropDownListAdultdomestic option[value='" + objModifySerach.Adults.toString() + "']").attr('selected', 'selected');
            //$("#DropDownListChilddomestic option[value='" + objModifySerach.Childrens.toString() + "']").attr('selected', 'selected');
            $("#qtyValueAdultDomestic").val(objModifySerach.Adults.toString());
            $("#qtyValueChildDomestic").val(objModifySerach.Childrens.toString());
            $("#chkboxRedeem").prop("checked", true);
            $("#txtDepartdomestic").val(DepartDate);
            if (objModifySerach.IsReturn.toString() == 'true') {
                $("#txtReturndomestic").val(ArrivalDate);
                $("#txtDepartdomestic").val(DepartDate);
                $("#onelidomestic").removeClass("tab-act");
                $("#retlidomestic").addClass("tab-act");
                $("#hdntripdomestic").val('true');
                $("#onelidomestic").prop("checked", false);
                $("#retlidomestic").prop("checked", true);
                $("#divrtndomestic").show();
                $("#returnSelectMenuDomestic").val("Return");
            }
            else {
                $("#onelidomestic").addClass("tab-act");
                $("#retlidomestic").removeClass("tab-act");
                $("#divrtndomestic").hide();
                $("#hdntripdomestic").val('false');
                $("#onelidomestic").prop("checked", true);
                $("#retlidomestic").prop("checked", false);
                $("#returnSelectMenuDomestic").val("One Way");
            }
            $("#returnSelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}
