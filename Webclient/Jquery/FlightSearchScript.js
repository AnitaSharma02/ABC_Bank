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
function showTripSummary(summaryType, sequenceNo) {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/ShowTripSummary',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrSummaryType':'" + summaryType.toString() + "'" + "," + "'pstrSequenceNo':'" + sequenceNo.toString() + "'}",
        success: function (msg) {
            var strOnwardsSummary = "";
            var strOnwardFlightInfo = "";
            var strReturnFlightInfo = "";
            var strReturnSummary = "";
            var Onwardindex = 0;
            var Returndindex = 0;
            if (summaryType == "Onward") {
                var flights = msg.d[0];
                $("#hdnOnwardSelectedFlight").val(sequenceNo);
            }
            else {
                var flights = msg.d[0];
                $("#hdnReturnSelectedFlight").val(sequenceNo);
            }
            flight = $.parseJSON(flights);
            var strSummary = "";

            var strTotal = "";
            if (summaryType == "Onward") {
                for (count = 0; count < flight.ListOfFlightSegments.length; count++) {
                    if (flight.ListOfFlightSegments.length > 1) {
                        if (Onwardindex == 0) {
                            Onwardindex = 1;
                            lastcount = flight.ListOfFlightSegments.length - 1;
                            strOnwardsSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight.ListOfFlightSegments[count].DepartureAirField.City + " (" + flight.ListOfFlightSegments[count].OriginLocation + ")";
                            strOnwardsSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight.ListOfFlightSegments[lastcount].ArrivalAirField.City + " (" + flight.ListOfFlightSegments[lastcount].DestinationLocation + ")" + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight.FareDetails.TotalPoints + "</label></li><li><label class='trpsum-miles-type'>NPoints</label></li></ul></div>"
                        }
                    } else {
                        strOnwardsSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight.ListOfFlightSegments[count].DepartureAirField.City + " (" + flight.ListOfFlightSegments[count].OriginLocation + ")";
                        strOnwardsSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight.ListOfFlightSegments[count].ArrivalAirField.City + " (" + flight.ListOfFlightSegments[count].DestinationLocation + ")" + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight.FareDetails.TotalPoints + "</label></li><li><label class='trpsum-miles-type'>NPoints+</label></li></ul></div>"
                    }

                    strOnwardFlightInfo += "<div class='trpsum-flt-logo'><img src=" + flight.ListOfFlightSegments[count].Carrier.CarrierLogoPath + " /></div><div class='fltsumm-col-1'><ul class='fltsumm-dtrow'><li><span class='trpsum-fltname robotbold'>" + flight.ListOfFlightSegments[count].Carrier.CarrierName + "</span></li><li><span class='trpsum-fltcode'>(" + flight.ListOfFlightSegments[count].Carrier.CarrierCode + flight.ListOfFlightSegments[count].FlightNo + ")</span></li></ul></div><div class='fltsumm-col-2'><ul class='fltsumm-dtrow'><li><span class='trpsum-c-code robotbold'>" + flight.ListOfFlightSegments[count].OriginLocation + "</span><img src='images/right-arrow.png' class='trpsum-c-arrow ' /><span class='trpsum-c-code robotbold'>" + flight.ListOfFlightSegments[count].DestinationLocation + "</span></li><li><label class='trpsum-dprtime'>" + flight.ListOfFlightSegments[count].DisplayDepartureTime + "</label><label class='trpsum-arrvtime'>" + flight.ListOfFlightSegments[count].DisplayArrivalTime + "</label></li></ul></div><div class='fltsumm-col-3 alR'><ul class='fltsumm-dtrow'><li><label class='trpsum-dprtdate robotbold'>" + flight.ListOfFlightSegments[count].DisplayDepartureDate + "</label></li><li>" + flight.ListOfFlightSegments[count].TotalDurationHrs + " hrs " + flight.ListOfFlightSegments[count].TotalDurationMins + " mins</li><li>1 stop</li></ul></div>";
                }
                $("#divOnwardFlightDetails").html(strOnwardsSummary);
                $("#divOnwardFlightInfo").html(strOnwardFlightInfo);
            }
            if (summaryType == "Return") {
                for (count = 0; count < flight.ListOfFlightSegments.length; count++) {
                    if (flight.ListOfFlightSegments.length > 1) {
                        if (Returndindex == 0) {
                            Returndindex = 1;
                            {
                                lastcount = flight.ListOfFlightSegments.length - 1;
                                strReturnSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight.ListOfFlightSegments[count].DepartureAirField.City + " (" + flight.ListOfFlightSegments[count].OriginLocation + ")";
                                strReturnSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight.ListOfFlightSegments[lastcount].ArrivalAirField.City + " (" + flight.ListOfFlightSegments[lastcount].DestinationLocation + ")" + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight.FareDetails.TotalPoints + "</label></li><li><label class='trpsum-miles-type'>NPoints</label></li></ul></div>"

                            }
                        }
                    } else {
                        strReturnSummary += "<ul class='fL'><li><img src='images/trpsum-ico-flight-dprt.png' /></li><li><label class='trpsum-city'>" + flight.ListOfFlightSegments[count].DepartureAirField.City + " (" + flight.ListOfFlightSegments[count].OriginLocation + ")";
                        strReturnSummary += "</label><span class='trpsum-To'>to</span><label class='trpsum-city'>" + flight.ListOfFlightSegments[count].ArrivalAirField.City + " (" + flight.ListOfFlightSegments[count].DestinationLocation + ")" + "</label></li></ul><div class='fR'><ul><li><label class='trpsum-miles'>" + flight.FareDetails.TotalPoints + "</label></li><li><label class='trpsum-miles-type'>NPoints</label></li></ul></div>"
                    }
                    strReturnFlightInfo += "<div class='trpsum-flt-logo'><img src=" + flight.ListOfFlightSegments[count].Carrier.CarrierLogoPath + " /></div><div class='fltsumm-col-1'><ul class='fltsumm-dtrow'><li><span class='trpsum-fltname'>" + flight.ListOfFlightSegments[count].Carrier.CarrierName + "</span></li><li><span class='trpsum-fltcode'>(" + flight.ListOfFlightSegments[count].Carrier.CarrierCode + flight.ListOfFlightSegments[count].FlightNo + ")</span></li></ul></div><div class='fltsumm-col-2'><ul class='fltsumm-dtrow'><li><span class='trpsum-c-code'>" + flight.ListOfFlightSegments[count].OriginLocation + "</span><img src='images/right-arrow.png' class='trpsum-c-arrow ' /><span class='trpsum-c-code'>" + flight.ListOfFlightSegments[count].DestinationLocation + "</span></li><li><label class='trpsum-dprtime'>" + flight.ListOfFlightSegments[count].DisplayDepartureTime + "</label><label class='trpsum-arrvtime'>" + flight.ListOfFlightSegments[count].DisplayArrivalTime + "</label></li></ul></div><div class='fltsumm-col-3 alR'><ul class='fltsumm-dtrow'><li><label class='trpsum-dprtdate'>" + flight.ListOfFlightSegments[count].DisplayDepartureDate + "</label></li><li>" + flight.ListOfFlightSegments[count].TotalDurationHrs + " hrs " + flight.ListOfFlightSegments[count].TotalDurationMins + " mins</li><li>1 stop</li></ul></div>";
                }
                $("#divReturnFlightDetails").html(strReturnSummary);
                $("#divReturnFlightInfo").html(strReturnFlightInfo);
            }


            if (summaryType == "Onward") {
                flightDepAmt = flight.FareDetails.TotalPoints;
            }
            else {
                flightRetAmt = flight.FareDetails.TotalPoints;
            }
            $(".fixedBot-tripSumm").show();

            strTotal += "<ul class='fR'><li><label class='trpsumm-totalmiles'>" + parseInt(flightDepAmt + flightRetAmt) + "</label></li><li><label class='trpsum-miletype'> Total NPoints</label></li></ul>";
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

function BookDomestic() {
    //debugger;
    var OnwardSequenceNo = $("#hdnOnwardSelectedFlight").val();
    var ReturnSequenceNo = $("#hdnReturnSelectedFlight").val();
    $.ajax({
        type: 'POST',
        url: 'FlightLBMSServices.aspx/BookNowClickDomestic',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrOnwardSequenceNo':'" + OnwardSequenceNo + "','pstrReturnSequnceNo':'" + ReturnSequenceNo + "'}",
        cache: false,
        success: function (msg) {
            //debugger;
            if (msg.d)
                window.location = "FlightPassenger.aspx";
            else
                window.location = "Login.aspx";
        },
        beforeSend: function () {
            $("#updProgress").show();
        },
        error: function (errmsg) {

        }
    });
}

function BookDomesticOneWay(pstrSequenceNo) {
    //debugger;
    $.ajax({
        type: 'POST',
        url: 'FlightLBMSServices.aspx/BookNowClickDomesticOneWay',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrSequenceNo':'" + pstrSequenceNo + "'}",
        cache: false,
        success: function (msg) {
            if (msg.d)
                window.location = "FlightPassenger.aspx";
            else
                window.location = "Login.aspx";
        },
        beforeSend: function () {
            $("#updProgress").show();
        },
        error: function (errmsg) {

        }
    });
}



function BookNowClick(pstrSequenceNo) {
    //debugger;
    $.ajax({
        type: 'POST',
        url: 'FlightLBMSServices.aspx/BookNowClick',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrSequenceNo':'" + pstrSequenceNo + "'}",
        cache: false,
        success: function (msg) {
            if (msg.d)
                window.location = "FlightPassenger.aspx";
            else
                window.location = "Login.aspx";
        },
        beforeSend: function () {
            $("#updProgress").show();
        },
        error: function (errmsg) {

        }
    });
}



function FlightValidation() {
    //debugger
    var msg = "";
    $('input').removeClass("error");
    $('div').removeClass("error");
    $("#requiredValidation").empty();

    if (($("#textBoxFrom").val() == '' || $("#textBoxFrom").val() == null) || $("#textBoxFrom").val() == "Enter City or Airport") {
        msg += "*Enter departure Location.<br>";
        $("#textBoxFrom").addClass("error");
    }
    if (($("#textBoxTo").val() == '' || $("#textBoxTo").val() == null) || $("#textBoxTo").val() == "Enter City or Airport") {
        msg += "*Enter destination Location.<br>";
        $("#textBoxTo").addClass("error");
    }
    if (($("#txtDepart").val() == '' || $("#txtDepart").val() == null || $("#txtDepart").val() == 'Enter Date')) {
        msg += "*Enter departure date.<br>";
        $("#txtDepart").addClass("error");
    }
    if ($("#hdntrip").val() == "true") {
        if ($("#txtReturn").val() == '' || $("#txtReturn").val() == null || $("#txtReturn").val() == 'Enter Date') {
            msg += "*Enter return date.<br>";
            $("#txtReturn").addClass("error");
        }
    }
    if (parseInt($("#qtyValueInfant").val()) > parseInt($("#qtyValueAdult").val())) {
        msg += "*Infants is more than Adults. <br>";
        $("#divInfant").addClass("error");
    }
    if (msg.length > 0) {
        $("#requiredValidation").show();

        if (msg == "*Infants is more than Adults. <br>") {
            $("#requiredValidation")[0].innerHTML = "*Infants is more than Adults.";
        }
        else {
            $("#requiredValidation")[0].innerHTML = "Below fields are mandatory.";
        }
        return false;
    }
    else {
        return clicktoflightsearch();
    }
};
function clicktoflightsearch() {
    //debugger
    //var strFrom = "departure=" + $("#textBoxFrom").val().split(',')[0] + "&";
    //var strdepCity = "DepCity=" + $("#textBoxFrom").val().split(',')[2] + "&";
    //var strTo = "arrival=" + $("#textBoxTo").val().split(',')[0] + "&";
    //var strarrCity = "arrCity=" + $("#textBoxTo").val().split(',')[2] + "&";
    //var strDepartDate = "departuredate=" + $("#txtDepart").val() + "&";
    //var strReturnDate = "arrivaldate=" + $("#txtReturn").val() + "&";
    //var isReturn = "isReturn=" + $("#hdntrip").val().toString() + "&";
    //var strAirlinePrefernce = "airline=" + $("#txtAirline").val() + "&";
    //var strAirlineIATACode = "airlineIATACode=" + $("#hdnCarrier").val() + "&";
    //var strAdultNo = "adult=" + $("#DropDownListAdult").val() + "&";
    //var strChildNo = "child=" + $("#DropDownListChild").val() + "&";
    //var strInfantNo = "infant=" + $("#DropDownListInfant").val() + "&";
    //var strEconomy = "economy=" + $("#dropDownListEconomy").val();
    //var queryString = strFrom + strdepCity + strTo + strarrCity + strDepartDate + strReturnDate + isReturn + strAirlinePrefernce + strAirlineIATACode + strAdultNo + strChildNo + strInfantNo + strEconomy;
    //window.location = "SearchPage.aspx?" + queryString;
    var departure = $("#textBoxFrom").val().split(',')[0];
    var arrival = $("#textBoxTo").val().split(',')[0];
    var departuredate = $("#txtDepart").val();
    var isReturn = "";
    var routetype = "";
    if ($("#returnSelectMenu").val() == "Return") {
        isReturn = true;
        routetype = "Round Trip";
    } else {
        isReturn = false;
        routetype = "One Way";
    }
    var arrivaldate = $("#txtReturn").val();
    if (routetype == "One Way") {
        arrivaldate = "";
    }

    var airline = $("#txtAirline").val();
    var airlineIATACode = $("#hdnCarrier").val();
    var passengerType = "";
    var adult = $("#qtyValueAdult").val();
    if (parseInt(adult) > 0) {
        passengerType = $("#qtyValueAdult").val() + " Adult ";
    }
    var child = $("#qtyValueChild").val();
    if (parseInt(child) > 0) {
        passengerType += $("#qtyValueChild").val() + " Child ";
    }
    var infant = $("#qtyValueInfant").val();
    if (parseInt(infant) > 0) {
        passengerType += $("#qtyValueInfant").val() + " Infant";
    }
    var economy = $("#economySelectMenu").val();

    var searchDetails = "";
    if (arrivaldate == "" || arrivaldate == null) {
        searchDetails = departure + " to " + arrival + ", " + routetype + ", " + economy + ", " + departuredate + ", " + passengerType;
        searchDetails = searchDetails;
    }
    else {
        searchDetails = departure + " to " + arrival + ", " + routetype + ", " + economy + ", " + departuredate + " - " + arrivaldate + ", " + passengerType;
        searchDetails = searchDetails.replace('%20', ' ');
    }

    var arrData = {};
    arrData.departure = departure;
    arrData.arrival = arrival;
    arrData.departuredate = departuredate;
    arrData.isReturn = isReturn;
    arrData.arrivaldate = arrivaldate;
    arrData.airline = airline;
    arrData.airlineIATACode = airlineIATACode;
    arrData.adult = adult;
    arrData.child = child;
    arrData.infant = infant;
    arrData.economy = economy;

    $.ajax({
        type: 'POST',
        url: "../SearchPage.aspx/FlightSearch",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(arrData),
        success: function (msg) {
            if (msg.d)
                window.location = "FlightList.aspx";
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

function onwayShow() {
    $("#divrtn").hide();
    $("#oneli").addClass("tab-act");
    $("#retli").removeClass("tab-act");
    $("#hdntrip").val('false');
    return false;
}
function RoundTripShow() {
    $("#divrtn").show();
    $("#oneli").removeClass("tab-act");
    $("#retli").addClass("tab-act");
    $("#hdntrip").val('true');
    return false;
}
$(document).ready(function () {

    $("#txtAirline").val('');
    $("#txtAirline").val('All Airlines');

    /*    $('select').selectric();  commented by sheetal on 19/06/23*/
    $("#textBoxFrom").click(function () {
        $(this).val('');
    });

    $("#textBoxTo").click(function () {
        $(this).val('');
    });

    $("#txtAirline").click(function () {
        $(this).val('');
    });
    $("#textBoxFrom").autocomplete({
        source: function (request, response) {
            var list = [];
            $.ajax({
                type: 'POST',
                url: 'FlightLBMSServices.aspx/GetAirfields',
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
            $("#textBoxFrom").val(ui.item.label);
            $("#textBoxFrom").val(ui.item.value);
            $(this).setCursorPosition(2);
            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300

    });
    $("#txtAirline").autocomplete({
        source: function (request, response) {
            var list = [];
            $.ajax({
                type: 'POST',
                url: 'FlightLBMSServices.aspx/GetCarriers',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{'prefixText':'" + request.term.toString() + "'}",
                cache: false,
                success: function (msg) {
                    response($.map(msg.d, function (item) {
                        return {
                            label: item.split('-')[0],
                            val: item.split('-')[1]
                        }
                    }))

                    var imgAirlines = $("#imgAirlines");
                    imgAirlines.attr("src", "Images/ClearTextBox.png");
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
            $("#txtAirline").val(ui.item.label);
            $("#hdnCarrier").val(ui.item.val);
            $(this).setCursorPosition(2);
            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300
    });

    $("#textBoxTo").autocomplete({
        source: function (request, response) {
            var list = [];
            $.ajax({
                type: 'POST',
                url: 'FlightLBMSServices.aspx/GetAirfields',
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
            $("#textBoxTo").val(ui.item.label);
            $("#textBoxTo").val(ui.item.value);
            $(this).setCursorPosition(2);
            return false;
        },
        minLength: 3,
        scroll: true,
        scrollHeight: 300
    });
    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
        var strResult = item.label.toString();
        var strArr = [];
        strArr = strResult.split(',');
        if (strArr.length == 3) {//for hotels
            var cityname = strArr[0];
            var countryname = strArr[1];
            var countryCode = strArr[2];
            var autoResult = "<span style='font-size:11px;padding-botom:5px'>" + cityname + "," + countryname + "," + countryCode + "</span>";
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style='' class='h7 heading-regular text-colour7'>" + autoResult + "</a>").appendTo(ul);
        }
        else if (strArr.length == 1)//for Airline
        {
            var Airline = strArr[0];
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style='' class='h7 heading-regular text-colour7'>" + Airline + "</a>").appendTo(ul);
        }
        else if (strArr.length == 2) {
            var airportname = strArr[0] + ", " + strArr[1];
            var autoResult = "<span style='font-size:12px;font-weight:bold'>" + airportname + "</span><br/>";
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style='' class='h7 heading-regular text-colour7'>" + autoResult + "</a>").appendTo(ul);
        }
        else {
            var airportname = strArr[2] + ", " + strArr[3];
            var airportcity = strArr[1] + "(" + strArr[0].replace(" ", "") + ")";
            var autoResult = "<span style='font-size:12px;font-weight:bold'>" + airportname + "</span><br/>" + "<span style='font-size:11px;font-weight:normal'>" + airportcity + "</span>";
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style='' class='h7 heading-regular text-colour7'>" + autoResult + "</a>").appendTo(ul);
        }

    };
});
function placeholderOnFocus(obj, defaultVal) {
    if (obj.value == "") {
        obj.value = defaultVal;
    } else if (obj.value == defaultVal) {
        obj.value = "";
    } else { }
    $("#" + obj.id).removeClass('error');
};

function showModifyFlight() {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/ModifySearchData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "",
        success: function (msg) {
            debugger;
            var objModifySerach = $.parseJSON(msg.d[0]);
            var DepartDate = msg.d[1];
            var ArrivalDate = msg.d[2];
            $("#textBoxFrom").val(objModifySerach.SearchDetails.OriginLocation + ',' + objModifySerach.SearchDetails.DepCode.AirportName + ',' + objModifySerach.SearchDetails.DepCode.City + ',' + objModifySerach.SearchDetails.DepCountryName);
            $("#textBoxTo").val(objModifySerach.SearchDetails.DestinationLocation + ',' + objModifySerach.SearchDetails.ArrCode.AirportName + ',' + objModifySerach.SearchDetails.ArrCode.City + ',' + objModifySerach.SearchDetails.ArrCountryName);
            $("#txtAirline").val(objModifySerach.SearchDetails.FlightType);
            $("#economySelectMenu").val(objModifySerach.SearchDetails.Cabin.toString());
            $("#chkboxRedeem").prop("checked", true);
            $("#qtyValueAdult").val(objModifySerach.SearchDetails.Adults.toString());
            $("#qtyValueChild").val(objModifySerach.SearchDetails.Childrens.toString());
            $("#qtyValueInfant").val(objModifySerach.SearchDetails.Infants.toString());
            $("#txtDepart").val(DepartDate);
            if (objModifySerach.SearchDetails.IsReturn.toString() == 'true') {
                $("#txtReturn").val(ArrivalDate);
                $("#hdnreturnDate").val(DepartDate);
                $("#oneli").removeClass("tab-act");
                $("#retli").addClass("tab-act");
                $("#hdntrip").val('true');
                $("#oneli").prop("checked", false);
                $("#retli").prop("checked", true);
                $("#returnSelectMenu").val("Return");
                $("#divrtn").show();
            }
            else {
                $("#oneli").addClass("tab-act");
                $("#retli").removeClass("tab-act");
                $("#hdntrip").val('false');
                $("#oneli").prop("checked", true);
                $("#retli").prop("checked", false);
                $("#returnSelectMenu").val("One Way");
                $("#divrtn").hide();

            }
            $("#returnSelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
            $("#economySelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}

function showhideDetails(buttonID) {
    $("." + buttonID).slideToggle('slow');
    $('.divseeflightsbtn' + buttonID).css('display', 'none');
    return false;
};

function hideshowDetails(buttonID) {
    $("." + buttonID).slideToggle();
    $('.divseeflightsbtn' + buttonID).css('display', 'block');
    return false;
};
$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    })
}

function ConvertTime(timeString) {
    var hourEnd = timeString.indexOf(":");
    var H = +timeString.substr(0, hourEnd);
    var h = H % 12 || 12;
    var ampm = H < 12 ? "AM" : "PM";
    timeString = h + timeString.substr(hourEnd, 3) + ' ' + ampm;
    return timeString;
}
function formatJSONDate(end_time, start_time) {
    if (end_time == null || start_time == null) return "";
    else {

        s = end_time.split(':');
        e = start_time.split(':');
        min = e[1] - s[1];
        hour_carry = 0;
        if (min < 0) {
            min += 60;
            hour_carry += 1;
        }
        hour = e[0] - s[0] - hour_carry;
        min = ((min / 60) * 100).toString()
        diff = hour + " hour " + min.substring(0, 2) + " minute ";
        return diff;
    }
}
//code ended
$(document).ready(function () {
    var $window = $(window);

    function checkDatePickerWidth() {

        var windowsize = $window.width();
        if (windowsize > 550) {
            bindDatepicker();
        }
        else {
            bindMobDatepicker();
        }
        $("#CP_DepDate").click(function () {
            $("#txtDepart").datepicker('show');
        });

    }
    // Execute on load
    checkDatePickerWidth();
    // Bind event listener
    $(window).resize(checkDatePickerWidth);
});


function bindDatepicker() {
    $("#txtDepart").datepicker({
        minDate: 3,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
            // $('#CP_TextBoxCheckin').datepicker('option', 'minDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#txtReturn").datepicker("destroy");
            $("#txtReturn").val(oneDay);
            $("#txtReturn").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });

            return false;
        }

    });
    //$("#txtReturn").datepicker({
    //    minDate: $("#txtDepart").val(),
    //    numberOfMonths: 1,
    //    buttonImageOnly: true,
    //    dateFormat: 'dd/mm/yy'
    //});
}
function bindMobDatepicker() {
    $("#txtDepart").datepicker({
        minDate: 3,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
            //$('#CP_TextBoxCheckin').datepicker('option', 'minDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#txtReturn").datepicker("destroy");
            $("#txtReturn").val(oneDay);
            $("#txtReturn").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });

            return false;
        }

    });
    //debugger
    //$("#txtReturn").datepicker({
    //    numberOfMonths: 1,
    //    buttonImageOnly: true,
    //    dateFormat: 'dd/mm/yy'
    //});
}
function SetPassenger(className, ControlID, minval, maxval) {
    if (className == "plus") {
        var incval = parseInt($("#" + ControlID).text()) + 1;
        if (incval <= maxval) {
            $("#" + ControlID).text(parseInt($("#" + ControlID).text()) + 1);
        }
    }
    else {
        var decval = parseInt($("#" + ControlID).text()) - 1;
        if (decval >= minval) {
            $("#" + ControlID).text(parseInt($("#" + ControlID).text()) - 1);
        }
    }

    return false;
}