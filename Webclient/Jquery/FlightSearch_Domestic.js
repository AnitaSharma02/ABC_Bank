$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    })
}
var SelectedOnwardFlightId = "";
var SelectedReturnFlightId = "";
$(document).ready(function () {
    showLoading();
    PaintCounts();
    GetFlightData(1);
});
function SelectedOnewayFlight(FlightId,ctrl) {
    //debugger
    $('.oneway').removeClass('active');
    $(ctrl).addClass('active');
    SelectedOnwardFlightId = FlightId;
    loadtripSelected(SelectedReturnFlightId, SelectedOnwardFlightId);
}
function showTripSummary(FlightId,ctrl) {
    //debugger
    $('.inboundflight').removeClass('active');
    $(ctrl).addClass('active');
    SelectedReturnFlightId = FlightId;
    loadtripSelected(SelectedReturnFlightId, SelectedOnwardFlightId);
}
function loadtripSelected(SelectedReturnFlightId, SelectedOnwardFlightId) {
    //debugger;
    $.ajax({
        type: 'POST',
        url: 'FlightListForDomestic.aspx/ShowTripSummary',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'pstrReturnFlightId':'" + SelectedReturnFlightId.toString() + "'" + "," + "'pstrOnwardFlightId':'" + SelectedOnwardFlightId.toString() + "'}",
        success: function (msg) {
            //debugger;
            if (msg.d[2] != "") {
                var Template = msg.d[2];
                $("#CP_LoadTemplate")[0].innerHTML = Template.toString();

                DomesticFinalData = $.parseJSON(msg.d[0]);
                $("#finalresult").setTemplateElement("FinalResultTemplate");
                $("#finalresult").processTemplate(DomesticFinalData);
            }
            else {

                BookNowClick_Domestic();
            }        
            return false;
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}
function GetFlightData(pstrIsNext) {
    var isMobileView = 0;
    if ($(window).width() >= 768) {
        isMobileView = '0';
    } else {
        isMobileView = '1';
    }

    $.ajax({
        type: 'POST',
        url: 'FlightListForDomestic.aspx/LoadData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async:'true',
        data: "{'pstrIsNext':'" + pstrIsNext.toString() + "'}",
        success: function (msg) {
            //debugger
            var Template = msg.d[2];
            $("#CP_LoadTemplate")[0].innerHTML = Template.toString();

            if (msg.d[1] != "") { // domestic return flight
                if ($(window).width() <= 768) {
                    $('#depFlight').show();
                }

                DomesticReturnData = $.parseJSON(msg.d[1]);
                DomesticOnwardData = $.parseJSON(msg.d[0]);
                DomesticFinalData = $.parseJSON(msg.d[5]);
                SelectedOnwardFlightId = DomesticOnwardData[0].FlightId;
                SelectedReturnFlightId = DomesticReturnData[0].FlightId;
                $("#finalresult").setTemplateElement("FinalResultTemplate");
                $("#finalresult").processTemplate(DomesticFinalData);
                $("#result1").setTemplateElement("OutboundTemplate");
                $("#result1").processTemplate(DomesticOnwardData);
                $("#result2").setTemplateElement("InboundTemplate");
                $("#result2").processTemplate(DomesticReturnData);
                $("#resultInterNational").hide();
                $("#result1OneWay").hide();
                $("#domesticTwoWay").show();
                $("#result1").show();
                $("#result2").show();
                $("#finalresult").show();
                $("#" + DomesticOnwardData[0].FlightNo + "").addClass("active");
                $("#" + DomesticReturnData[0].FlightNo + "").addClass("active");
            }
            else { //domestic onwardflights
                //debugger;
                DomesticOnwardData = $.parseJSON(msg.d[0]);
                $("#result1OneWay").setTemplateElement("OutboundOneWayTemplate");
                $("#result1OneWay").processTemplate(DomesticOnwardData);
                $("#resultInterNational").hide();
                $("#result1OneWay").show();
                $("#result1").hide();
                $("#result2").hide();
                $("#finalresult").hide();
                $("#domesticTwoWay").hide();
            }
            if (msg.d[3].toString() == 'True') {
                $("#LoadNext").show();

            } else {
                $("#LoadNext").hide();
            }
            if (msg.d[4].toString() != '') {

                var totalcount = msg.d[4].toString();
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
            CheckAvailability($("#TotalFareAmount").text());

        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    hideImage();
}
function PaintCounts() {

    $.ajax({
        type: 'POST',
        url: 'FlightListForDomestic.aspx/FlightData',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "",
        success: function (msg) {
            var lobjSearchRequest = $.parseJSON(msg.d[0]);
            var searchdetails = "";
            var DepartDate = msg.d[1];
            var ArrivalDate = msg.d[2];
            if (lobjSearchRequest.IsReturn.toString() == 'true') {
                searchdetails = " " + lobjSearchRequest.OriginLocation + " ⇄ " + lobjSearchRequest.DestinationLocation + ", " + DepartDate.toString() + " ⇄ " + ArrivalDate.toString();
            }
            else {
                searchdetails = " " + lobjSearchRequest.OriginLocation + " → " + lobjSearchRequest.DestinationLocation + ", " + DepartDate.toString();
            }

            if (lobjSearchRequest.Adults != 0) {
                searchdetails = searchdetails + " | " + lobjSearchRequest.Adults + " Adult(s)";
            }
            if (lobjSearchRequest.Childrens != 0) {
                searchdetails = searchdetails + ", " + lobjSearchRequest.Childrens + " Children";
            }
            $("#CP_LabelYourSearchDetails")[0].innerHTML = searchdetails;
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });

}
function ConvertThousandSeparator(val) {
    return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
function showLoading() {
    $("#LoadingResult").show();
}
function hideImage() {
    $("#LoadingResult").slideUp(1000);
    return true;
}
function CheckAvailability(amount) {
    $.ajax({
        type: "POST",
        url: "FlightListForDomestic.aspx/CheckAvailability",
        data: "{pntamount:" + amount + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            if (msg.d) {
                $("#divInsufficient").hide();
                $("#btnRedeemFlight").removeClass('d-none');
                return true;
            }
            else {
                $("#divInsufficient").show();
                $("#divInsufficient").text("Insufficient NPoints");
                $("#btnRedeemFlight").addClass('d-none');
                return false;
            }
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    return false;
}