$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    })
}
$(document).ready(function () {
    showLoading();
    GetFilterCriteria();
    //take max and min departure time
    $("#departureSlider").slider({
        range: true,
        min: 0,
        max: 1440,
        step: 15,
        values: [0, 1440],
        slide: function (event, ui) {
            var textValue = filtersData.getSlidervalues(ui.values[0], ui.values[1]);
            $("#departureRange").text(textValue);
        },
        stop: function (event, ui) {
            return FilterSlider(4, ui.values[0], ui.values[1]);
        }
    });
    //default initiliztion
    $("#departureRange").text("00:00 hrs - 24:00 hrs");
    //take max and min departure time
    $("#arrivalSlider").slider({
        range: true,
        min: 0,
        max: 1440,
        step: 15,
        values: [0, 1440],
        slide: function (event, ui) {
            var textValue = filtersData.getSlidervalues(ui.values[0], ui.values[1]);
            $("#arrivalRange").text(textValue);
        },
        stop: function (event, ui) {
            return FilterSlider(5, ui.values[0], ui.values[1]);
        }
    });
    //default initiliztion
    $("#arrivalRange").text("00:00 hrs - 24:00 hrs");

    PaintAirlines();
    PaintStops();
    GetFlightData(1);

});

function GetFlightData(pstrIsNext) {

    $('.flightDomReturnSwitchLayoutbtn').hide();
    var isMobileView = 0;
    if ($(window).width() >= 768) {
        isMobileView = '0';
    } else {
        isMobileView = '1';
    }

    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/LoadData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrIsNext':'" + pstrIsNext.toString() + "','isMobileView':'" + isMobileView + "'}",
        success: function (msg) {

            var Template = msg.d[3];
            //$("#CP_lblNoofFlight").text(msg.d[5]);
            $("#CP_LoadTemplate")[0].innerHTML = Template.toString();

            if (msg.d[2] != "") { // international flights
                InternationalData = $.parseJSON(msg.d[2]);
                $("#resultInterNational").setTemplateElement("OnwardReturnTemplate");
                $("#resultInterNational").processTemplate(InternationalData);
                $("#resultInterNational").show();
                $("#result1OneWay").hide();
                $("#result1").hide();
                $("#result2").hide();
                $("#domesticTwoWay").hide();


            } else if (msg.d[1] != "") { // domestic return flight
                if ($(window).width() <= 768) {
                    $('#depFlight').show();
                }

                DomesticReturnData = $.parseJSON(msg.d[1]);
                DomesticOnwardData = $.parseJSON(msg.d[0]);
                $("#result1").setTemplateElement("OnwardTemplate");
                $("#result1").processTemplate(DomesticOnwardData);
                $("#result2").setTemplateElement("ReturnTemplate");
                $("#result2").processTemplate(DomesticReturnData);
                $("#resultInterNational").hide();
                $("#result1OneWay").hide();
                $("#domesticTwoWay").show();
                $("#result1").show();
                $("#result2").show();


                if (msg.d[1] != "[]" && msg.d[0] != "[]") {
                    showTripSummary('Onward', DomesticOnwardData[0].SequenceNo);
                    showTripSummary('Return', DomesticReturnData[0].SequenceNo);
                } else {
                    $('#Trip_Summary_Main').hide();
                }



            } else { //domestic onwardflights
                DomesticOnwardData = $.parseJSON(msg.d[0]);
                $("#result1OneWay").setTemplateElement("OnwardOneWayTemplate");
                $("#result1OneWay").processTemplate(DomesticOnwardData);
                $("#resultInterNational").hide();
                $("#result1OneWay").show();
                $("#result1").hide();
                $("#result2").hide();
                $("#domesticTwoWay").hide();
            }
            if (msg.d[4].toString() == 'True') {
                $("#LoadNext").show();

            } else {
                $("#LoadNext").hide();
            }
            if (msg.d[5].toString() != '') {

                var totalcount = msg.d[5].toString();
                // $('#CP_lblNoofFlight').val() = totalcount.slice(21, 24);
                $("#totalvalue").val(totalcount.toString());
            }
            $(".miles").digits();
            //   $('#CP_lblNoofFlight').val($("#totalvalue").val());
            $("#lblNoofFlight").text($("#totalvalue").val());

            $(".CommaSeperated").each(function () {

                if ($(this).html() != "") {
                    $(this).html(ConvertThousandSeparator($(this).html()))
                }
            });

        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    hideImage();
}

function ConvertThousandSeparator(val) {
    return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}


function PaintAirlines() {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/PaintAirlines',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "",
        success: function (msg) {
            $("#DivAirLinesList").html(msg.d);
            updateVcDataSections();
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}

function PaintStops() {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/PaintStops',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "",
        success: function (msg) {
            $(".divStops").html(msg.d);
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}

function PaintCounts() {

    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/FlightData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        data: "",
        success: function (msg) {

            var Counts = msg.d[0];
            // $("#CP_lblNoofFlight").append(Counts.toString());
            //$("#ContentPlaceHolder1_lblNoofFlight").text(Counts.toString() + " of " + Counts.toString() + " Flights");
            $("#lblNoofFlight").text(Counts);

            var lobjSearchRequest = $.parseJSON(msg.d[1]);
            var searchdetails = "";
            var DepartDate = msg.d[2];
            var ArrivalDate = msg.d[3];

            if (lobjSearchRequest.SearchDetails.FlightType.toString() == '') {
                lobjSearchRequest.SearchDetails.FlightType = 'All Airlines';
            }

            if (lobjSearchRequest.SearchDetails.IsReturn.toString() == 'true') {
                searchdetails = " " + lobjSearchRequest.SearchDetails.DepCode.City + " → " + lobjSearchRequest.SearchDetails.ArrCode.City + ", " + lobjSearchRequest.SearchDetails.Cabin + ", " + DepartDate.toString() + " → " + ArrivalDate.toString();
            }
            else {
                searchdetails = " " + lobjSearchRequest.SearchDetails.DepCode.City + " → " + lobjSearchRequest.SearchDetails.ArrCode.City + ", " + lobjSearchRequest.SearchDetails.Cabin + ", " + DepartDate.toString();
            }

            if (lobjSearchRequest.SearchDetails.Adults != 0) {
                searchdetails = searchdetails + " | " + lobjSearchRequest.SearchDetails.Adults + " Adult(s)";
            }
            if (lobjSearchRequest.SearchDetails.Childrens != 0) {
                searchdetails = searchdetails + ", " + lobjSearchRequest.SearchDetails.Childrens + " Children";
            }
            if (lobjSearchRequest.SearchDetails.Infants != 0) {
                searchdetails = searchdetails + ", " + lobjSearchRequest.SearchDetails.Infants + " Infants";
            }
            $("#CP_LabelYourSearchDetails")[0].innerHTML = searchdetails;
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}

function GetFilterCriteria() {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/GetFilterCriteria',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        data: "",
        success: function (msg) {
            PaintCounts();
            GetFilterData();
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}

function LoadNext() {
    showLoading();
    GetFlightData(1);
}
function GetSortedFlights(parameter, direction, paramobj) {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/GetSortedFlights',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrparameter':'" + parameter.toString() + "','pstrdirection':'" + direction.toString() + "','pstrparamobj':'" + paramobj.toString() + "'}",
        success: function (msg) {
            GetFlightData(0);
        }
    });

}

function FilterAirlines(Airline) {
    var tag = Airline.toString().replace(/\s+/g, '');
    tag = tag.toString().replace(/["'()]/g, '');
    //$("#CP_lblNoofFlight").remove();

    var ischecked = $("#chk" + tag).is(":checked").toString()
    if (tag == 'SelectAll') {
        var $checkBoxList = $("#DivAirLinesList input[type='checkbox']");
        for (var i = 0; i < $checkBoxList.length; i++) {
            if (ischecked == 'false') {
                $checkBoxList.eq(i).prop("checked", false);
            } else {
                $checkBoxList.eq(i).prop("checked", true);
            }
        }
    }

    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/FilterAirlines',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrAirline':'" + Airline.toString() + "','pstrischecked':'" + ischecked.toString() + "'}",
        success: function (msg) {
            //$("#CP_lblNoofFlight").remove();
            GetFlightData(0);
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}

function FilterNoOfStops(id) {
    var ischecked = $("#chkStops" + id).is(":checked").toString()
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/FilterNoOfStops',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrid':'" + id.toString() + "','pstrischecked':'" + ischecked.toString() + "'}",
        success: function (msg) {
            GetFlightData(0);
            updateVcDataSections();
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}

function FilterSlider(id, MinVal, MaxVal) {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/FilterSlider',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrid':'" + id.toString() + "','pstrMinVal':'" + MinVal.toString() + "','pstrMaxVal':'" + MaxVal.toString() + "'}",
        success: function (msg) {
            GetFlightData(0);
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}
function showLoading() {
    $("#LoadingResult").show();
}
function hideImage() {
    $("#LoadingResult").slideUp(1000);
    return true;
}

function GetFilterData() {
    $.ajax({
        type: 'POST',
        url: 'FlightList.aspx/GetFilterData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false,
        data: "",
        success: function (msg) {
            objFilterRange = $.parseJSON(msg.d);

            $("#priceSlider").slider({
                range: true,
                max: objFilterRange.MaxPoints,
                min: objFilterRange.MinPoints,
                step: 1,
                values: [objFilterRange.MinPoints, objFilterRange.MaxPoints],
                slide: function (event, ui) {
                    var textValue = CommaSep(ui.values[0]) + " - " + CommaSep(ui.values[1]);

                    $("#priceRange").text(textValue);
                },
                stop: function (event, ui) {
                    return FilterSlider(1, ui.values[0], ui.values[1]);
                }
            });
            //default initiliztion
            $("#priceRange").text(CommaSep(objFilterRange.MinPoints) + " - " + CommaSep(objFilterRange.MaxPoints));

            //take max and min duration
            $("#durationSlider").slider({
                range: true,
                min: objFilterRange.MinDuration,
                max: objFilterRange.MaxDuration,
                step: 15,
                values: [objFilterRange.MinDuration, objFilterRange.MaxDuration],
                slide: function (event, ui) {
                    var textValue = filtersData.getSlidervalues(ui.values[0], ui.values[1]);
                    $("#durationRange").text(textValue);
                },
                stop: function (event, ui) {
                    return FilterSlider(3, ui.values[0], ui.values[1]);
                }
            });
            //default initiliztion
            $("#durationRange").text(filtersData.getSlidervalues(objFilterRange.MinDuration, objFilterRange.MaxDuration));


        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}