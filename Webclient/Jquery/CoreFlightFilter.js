var filtersData = {
    ShowHideRows: function (lobjFlightDetailsDomestic, listOfFlightDetailsInternational, selectedPoints, selectedAirlines, selectedDepartureTimeHrs, selectedArrivalTimeHrs, selectedDuration, selectedStops, flightsIdPrefix, IsDomesticReturn) {

        if (listOfFlightDetailsInternational != null) {
            var flights = listOfFlightDetailsInternational;
            for (var flightCount = 0; flightCount < flights.length; flightCount++) {
                var totalPoints = flights[flightCount].FareDetails.TotalPoints;
                var airlines = flights[flightCount].ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName;
                var duration = flights[flightCount].ListOfFlightDetails[0].TotalDuration;
                var departureTimeHrs = flights[flightCount].ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes;
                var arrivalTimeHrs = flights[flightCount].ListOfFlightDetails[0].ListOfFlightSegments[flights[flightCount].ListOfFlightDetails[0].ListOfFlightSegments.length - 1].ArrivalTimeInMinutes;
                var stops = flights[flightCount].ListOfFlightDetails[0].ListOfFlightSegments.length-1;

                var hasPoints = (selectedPoints[0] <= totalPoints && selectedPoints[1] >= totalPoints);
                var hasAirlines = selectedAirlines.contains(airlines);
                var hasDuration = (selectedDuration[0] <= duration && selectedDuration[1] >= duration);
                var hasDepartureTimeHrs = (selectedDepartureTimeHrs[0] <= departureTimeHrs && selectedDepartureTimeHrs[1] >= departureTimeHrs);
                var hasArrivalTimeHrs = (selectedArrivalTimeHrs[0] <= arrivalTimeHrs && selectedArrivalTimeHrs[1] >= arrivalTimeHrs);
                var hasStops = selectedStops.contains(stops);
                var result = hasPoints && hasAirlines && hasStops && hasDuration && hasDepartureTimeHrs && hasArrivalTimeHrs && hasStops;
                if (result) {
                    $(flightsIdPrefix + flights[flightCount].SequenceNo).show();
                }
                else {
                    $(flightsIdPrefix + flights[flightCount].SequenceNo).hide();
                }
            }
        }
        else {
            var flights = lobjFlightDetailsDomestic;
            for (var flightCount = 0; flightCount < flights.length; flightCount++) {
                var totalPoints = flights[flightCount].FareDetails.TotalPoints;
                var airlines = flights[flightCount].ListOfFlightSegments[0].Carrier.CarrierName;
                var duration = flights[flightCount].TotalDuration;
                var departureTimeHrs = flights[flightCount].ListOfFlightSegments[0].DepartureTimeInMinutes;
                var arrivalTimeHrs = flights[flightCount].ListOfFlightSegments[flights[flightCount].ListOfFlightSegments.length - 1].ArrivalTimeInMinutes;
                var stops = flights[flightCount].ListOfFlightSegments.length-1;

                var hasPoints = (selectedPoints[0] <= totalPoints && selectedPoints[1] >= totalPoints);
                var hasAirlines = selectedAirlines.contains(airlines);
                var hasDuration = (selectedDuration[0] <= duration && selectedDuration[1] >= duration);
                var hasDepartureTimeHrs = (selectedDepartureTimeHrs[0] <= departureTimeHrs && selectedDepartureTimeHrs[1] >= departureTimeHrs);
                var hasArrivalTimeHrs = (selectedArrivalTimeHrs[0] <= arrivalTimeHrs && selectedArrivalTimeHrs[1] >= arrivalTimeHrs);
                var hasStops = selectedStops.contains(stops);
                var result = hasPoints && hasAirlines && hasStops && hasDuration && hasDepartureTimeHrs && hasArrivalTimeHrs && hasStops;
                if (result) {
                    if (IsDomesticReturn) {
                        $(flightsIdPrefix + "Onward" + flights[flightCount].SequenceNo).show();
                        $(flightsIdPrefix + "Return" + flights[flightCount].SequenceNo).show();
                    }
                    else {
                        $(flightsIdPrefix + flights[flightCount].SequenceNo).show();
                    }
                }
                else {
                    if (IsDomesticReturn) {
                        $(flightsIdPrefix + "Onward" + flights[flightCount].SequenceNo).hide();
                        $(flightsIdPrefix + "Return" + flights[flightCount].SequenceNo).hide();
                    }
                    else {
                        $(flightsIdPrefix + flights[flightCount].SequenceNo).hide();
                    }
                }
            }


        }
    },
    getSlidervalues: function (value1, value2) {
        return this.getSlidervalue(value1) + " hrs - " + this.getSlidervalue(value2)+" hrs"
    },

    getSlidervalue: function (value) {
        var returnvalue = "";

        var valueMinutes = value % 60;
        var valueHrs = (value - valueMinutes) / 60;
        if (valueHrs < 10) {
            returnvalue = "0" + valueHrs.toString() + ":";
        }
        else {
            returnvalue = valueHrs.toString() + ":";
        }
        if (valueMinutes == 0) {
            returnvalue += "00";
        }
        else {
            returnvalue += valueMinutes.toString();
        }
        return returnvalue;
    }

}
var filterCriteria = {
    getPriceCriteria: function (priceSliderId) {
        return $(priceSliderId).slider("option", "values");
    },
    getAirlinesCriteria: function (checkBoxListId, filterRange) {
        var listofairlines = [];
        var $checkBoxList = $(checkBoxListId);
        for (var i = 0; i < $checkBoxList.length; i++) {
            if ($checkBoxList.eq(i).is(":checked")) {
                listofairlines.push(filterRange.Airlines[i].toString());
            }
        }
        return listofairlines;
    },
    getDepartureTimeHrs: function (departureTimeId) {
        return $(departureTimeId).slider("option", "values");
    },
    getArrivalTimeHrs: function (arrivalTimeId) {
        return $(arrivalTimeId).slider("option", "values");
    },
    getSelectedDuration: function (durationSliderId) {
        return $(durationSliderId).slider("option", "values");
    },
    getSelectedStops: function (stopsListId, filterRange) {
        var listOfStops = [];
        for (var count = 0; count <= filterRange.Stops; count++) {
            if ($(stopsListId + (count)).is(":checked")) {
                listOfStops.push((count));
            }
        }
        return listOfStops;
    }
};
Array.prototype.contains = function (needle) {
    for (i in this) {
        if (this[i] == needle) return true;
    }
    return false;
};
var IsDomestic = false;

function sortingData(parameter, direction, paramobj) {
    var strList = "";
    $("#hdnOrderByObject").val(parameter);
    $("#hdnOrderByDirection").val(direction);
    var DomesticOnwardData = $("[id*=hdnOnwardSearchResponse]").val();
    var DomesticReturnData = $("[id*=hdnReturnSearchResponse]").val();
    var InternationalData = $("[id*=hdnSearchResponse]").val();
    var listOfFlightDetailsDomestic;
    var listOfFlightDetailsInternational;
    var strPrefix;
    var strSuffix;
    var strPrefix1
    var strPrefix2
    var strSuffix1;
    var strSuffix2;
    var flights;
    var flightContainer;
    var DataRowWrapperStart;
    var DataRowWrapperEnd;
    var idHodlder;
    if (InternationalData != "") { // international flights
        InternationalData = $.parseJSON(InternationalData);
        listOfFlightDetailsInternational = InternationalData;
        strPrefix = "<tr class='Rightpannel_customDivFlight seq_SEQUNCENO'>";
        strSuffix = "</tr>";
        //        strPrefix1="<tr class='showdetail seq_SEQUNCENO'>";
        //        strSuffix1="</tr>";
        strPrefix2 = "<tr class='rowspace seq_SEQUNCENO'>";
        strSuffix2 = "</tr>";
        DataRowWrapperStart = "<table class='flightcontainerInternational'>";
        DataRowWrapperEnd = "</table>";
        idHodlder = "";
        flightContainer = ".flightcontainerInternational";
        flights = listOfFlightDetailsInternational;
        IsDomestic = false;
    } else if (DomesticReturnData != "") { // domestic return flight
        DomesticReturnData = $.parseJSON(DomesticReturnData);
        listOfFlightDetailsDomestic = DomesticReturnData;
        strSuffix = "</tr>";
        strPrefix1 = "";
        strSuffix1 = "";
        strPrefix2 = "<tr class='rowspace seq_SEQUNCENO White_BG'>";
        strSuffix2 = "</tr>";
        flights = listOfFlightDetailsDomestic;
        if (paramobj == 'DomesticTwoWayOnward') {
            strPrefix = "<tr class='seq_OnwardSEQUNCENO Domestic_TwoWay_TR_BG'>";
            DataRowWrapperStart = "<table class='DomesticTwoWayMainTable flightContainerOnward' cellpadding='0' cellspacing='0'>";
            DataRowWrapperEnd = "</table>";
            flightContainer = ".flightContainerOnward";
            idHodlder = 'Onward';
        }
        else {
            strPrefix = "<tr class='seq_ReturnSEQUNCENO TwoWay_Return_BG'>";
            DataRowWrapperStart = "<table class='DomesticTwoWayMainTable Width100 flightContainerReturn' cellpadding='0' cellspacing='0'>";
            DataRowWrapperEnd = "</table>";
            flightContainer = ".flightContainerReturn";
            idHodlder = 'Return';
        }

        IsDomestic = true;
    } else { //domestic onwardflights
        DomesticOnwardData = $.parseJSON(DomesticOnwardData);
        listOfFlightDetailsDomestic = DomesticOnwardData;
        strPrefix = "<tr class='Rightpannel_customDivFlight seq_SEQUNCENO'>";
        strSuffix = "</tr>";
        strPrefix1 = "<tr class='showdetail seq_SEQUNCENO'>";
        strSuffix1 = "</tr>";
        strPrefix2 = "<tr class='rowspace seq_SEQUNCENO'>";
        strSuffix2 = "</tr>";

        DataRowWrapperStart = "<table class='flightcontainerDomesticOneway'>";
        DataRowWrapperEnd = "</table>";
        idHodlder = "";
        flightContainer = ".flightcontainerDomesticOneway";
        flights = listOfFlightDetailsDomestic;
        IsDomestic = true;
    }
    var sortedflights = flights.sort(SortFlight);
    for (var i = 0; i < sortedflights.length; i++) {
        strList += strPrefix.replace('SEQUNCENO', sortedflights[i].SequenceNo) + $(".seq_" + idHodlder + sortedflights[i].SequenceNo).html() + strSuffix;
        if (strPrefix1 != "") {
            //strList += strPrefix1.replace('SEQUNCENO',sortedflights[i].SequenceNo)+$(".showdetail.seq_"+ sortedflights[i].SequenceNo).html()+strSuffix1;
        }
        if (strPrefix2 != "") {
            strList += strPrefix2.replace('SEQUNCENO', sortedflights[i].SequenceNo) + $(".rowspace.seq_" + sortedflights[i].SequenceNo).html() + strSuffix2;
        }
    }
    strList = DataRowWrapperStart + strList + DataRowWrapperEnd;
    $(flightContainer).html(strList);
    //FliterData();
};
function SortFlight(flight1, flight2) {
    if ($("#hdnOrderByObject").val() == 'name') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            if (IsDomestic)
                return flight1.ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() > flight2.ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() ? 1 : -1;
            else
                return flight1.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() > flight2.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            if (IsDomestic)
                return flight1.ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() < flight2.ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() ? 1 : -1;
            else
                return flight1.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() < flight2.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() ? 1 : -1;
        }
    }
    if ($("#hdnOrderByObject").val() == 'departuretime') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            if (IsDomestic)
                return parseInt(flight1.ListOfFlightSegments[0].DepartureTimeInMinutes) > parseInt(flight2.ListOfFlightSegments[0].DepartureTimeInMinutes) ? 1 : -1;
            else
                return parseInt(flight1.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes) > parseInt(flight2.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes) ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            if (IsDomestic)
                return parseInt(flight1.ListOfFlightSegments[0].DepartureTimeInMinutes) < parseInt(flight2.ListOfFlightSegments[0].DepartureTimeInMinutes) ? 1 : -1;
            else
                return parseInt(flight1.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes) < parseInt(flight2.ListOfFlightDetails[0].ListOfFlightSegments[0].DepartureTimeInMinutes) ? 1 : -1;
        }
    }
    if ($("#hdnOrderByObject").val() == 'arrivaltime') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            if (IsDomestic)
                return parseInt(flight1.ListOfFlightSegments[flight1.ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) > parseInt(flight2.ListOfFlightSegments[flight2.ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) ? 1 : -1;
            else
                return parseInt(flight1.ListOfFlightDetails[0].ListOfFlightSegments[flight1.ListOfFlightDetails[0].ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) > parseInt(flight2.ListOfFlightDetails[0].ListOfFlightSegments[flight2.ListOfFlightDetails[0].ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            if (IsDomestic)
                return parseInt(flight1.ListOfFlightSegments[flight1.ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) < parseInt(flight2.ListOfFlightSegments[flight2.ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) ? 1 : -1;
            else
                return parseInt(flight1.ListOfFlightDetails[0].ListOfFlightSegments[flight1.ListOfFlightDetails[0].ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) < parseInt(flight2.ListOfFlightDetails[0].ListOfFlightSegments[flight2.ListOfFlightDetails[0].ListOfFlightSegments.length - 1].ArrivalTimeInMinutes) ? 1 : -1;
        }
    }
    if ($("#hdnOrderByObject").val() == 'miles') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            if (IsDomestic)
                return parseInt(flight1.FareDetails.TotalPoints) > parseInt(flight2.FareDetails.TotalPoints) ? 1 : -1;
            else
                return parseInt(flight1.ListOfFlightDetails[0].FareDetails.TotalPoints) > parseInt(flight2.ListOfFlightDetails[0].FareDetails.TotalPoints) ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            if (IsDomestic)
                return flight1.ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() < flight2.ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() ? 1 : -1;
            else
                return flight1.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() < flight2.ListOfFlightDetails[0].ListOfFlightSegments[0].Carrier.CarrierName.toLowerCase() ? 1 : -1;
        }
    }
};