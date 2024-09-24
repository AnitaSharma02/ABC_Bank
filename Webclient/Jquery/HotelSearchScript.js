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

$(document).ready(function () {
    /*$('select').selectric();
    var $window = $(window);*/
    function checkHotelDatePickerWidth() {
        /*var windowsize = $window.width();
        if (windowsize > 550) {*/
            bindHotelDatepicker();
       /*}
        else {
            bindHotelMobDatepicker();
        }*/
        $("#CP_divChkin").click(function () {
            $("#CP_TextBoxCheckin").datepicker('show');
        });
        $("#CP_divChkout").click(function () {
            $("#CP_TextBoxCheckout").datepicker('show');
        });
    }
    // Execute on load
    checkHotelDatePickerWidth();
    // Bind event listener
    $(window).resize(checkHotelDatePickerWidth);

});
function bindHotelDatepicker() {
    $("#CP_TextBoxCheckin").datepicker({
        minDate: 3,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
           // $('#CP_TextBoxCheckin').datepicker('option', 'minDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#CP_TextBoxCheckout").datepicker("destroy");
            $("#CP_TextBoxCheckout").val(oneDay);
            $("#CP_TextBoxCheckout").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });

            return false;
        }

    });
    $("#CP_TextBoxCheckout").datepicker({
        minDate: 4,
        numberOfMonths: 1,
        buttonImageOnly: true,
        dateFormat: 'dd/mm/yy'
    });
}
function bindHotelMobDatepicker() {
    $("#CP_TextBoxCheckin").datepicker({
        minDate: 3,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onSelect: function (dateText, inst) {
            //$('#CP_TextBoxCheckin').datepicker('option', 'minDate', new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay));
            var toDate = new Date(inst.selectedYear, inst.selectedMonth, inst.selectedDay);
            var oneDay = new Date(toDate.getTime() + 86400000);
            oneDay = $.datepicker.formatDate('dd/mm/yy', oneDay);
            $("#CP_TextBoxCheckout").datepicker("destroy");
            $("#CP_TextBoxCheckout").val(oneDay);
            $("#CP_TextBoxCheckout").datepicker({
                minDate: oneDay,
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy'
            });

            return false;
        }

    });
    $("#CP_TextBoxCheckout").datepicker({
        minDate: 4,
        numberOfMonths: 1,
        buttonImageOnly: true,
        dateFormat: 'dd/mm/yy'
    });
}
function SearchRooms() {
    //var room = [];
    //room = $(".Modifyloadrooms .Modifyroom");
    var room = $("#CP_qtyValue").val();
    var strRoomAdult = "";
    var strRoomChild = "";
    var strRoom = "";
    for (var count = 0; count < room; count++) {
        var selectTag = [];
        selectTag = count;
        strRoomAdult += $("#CP_qtyValueAdult" + (count+1)).val() + ",";
        strRoomChild += $("#CP_qtyValueChild" + (count+1)).val() + ",";
    }
    strRoom = strRoomAdult + ":" + strRoomChild;
    $("#hdnRoomString").val(strRoom);
    if (validateHotelFields()) {
        //var strCity = "city=" + $("#CP_txtCity").val() + "&";
        //var strCheckIn = "checkin=" + $("#CP_TextBoxCheckin").val() + "&";
        //var strCheckout = "checkout=" + $("#CP_TextBoxCheckout").val() + "&";
        //var strRoomstring = "roomstring=" + $("#hdnRoomString").val() + "&";
        //var isRedeemMiles = "isRedeemMiles=" + $("#ChkRedeemHotel").is(":checked").toString();
        //var queryString = strCity + strCheckIn + strCheckout + strRoomstring + isRedeemMiles;

        var strCity = $("#CP_txtCity").val();
        var strCheckIn = $("#CP_TextBoxCheckin").val();
        var strCheckOut = $("#CP_TextBoxCheckout").val();
        var strRoomString = $("#hdnRoomString").val();
        var strisRedeemMiles = $("#ChkRedeemHotel").is(":checked").toString();
        var SearchDetails = strCity + "," + strCheckIn + " to " + strCheckOut + ".";
        SearchDetails = SearchDetails.replace(/\%20/g, ' ');

        var arrData = {};
        arrData.pstrCity = strCity;
        arrData.pCheckIn = strCheckIn;
        arrData.pCheckOut = strCheckOut;
        arrData.pRoomString = strRoomString;
        arrData.pisRedeemMiles = strisRedeemMiles;
        arrData.strRating = "All";
        $.ajax({
            type: 'POST',
            url: "../HotelsSearchWait.aspx/GetHotelSearchResponse",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            timeout: 100000,
            data: JSON.stringify(arrData),
            // data: "{'pstrCity':'" + strCity + "','pCheckIn':'" + strCheckIn + "','pCheckOut':'" + strCheckOut + "','pRoomString':'" + strRoomString + "','pisRedeemMiles':'" + strisRedeemMiles + "'}",
            success: function (msg) {
                if (msg.d)
                    window.location = "HotelResults.aspx";
                else
                    window.location = "NoResultFound.aspx?ERR=RESULTNOTFOUND";

                $("#updProgress").hide();
            },
            error: function (jqXHR, status, errorThrown) {
                window.location = "ErrorPage.aspx";

            },
            beforeSend: function () {
                $("#updProgress").show();
            }
        });
        //window.location = "HotelsSearchWait.aspx?" + queryString + "&Rating=All";
        return false;
    }
    else {
        return false;
    }
}

function ModifySearchRooms() {
    var room = [];
    room = $("#ddlnoofroom").val();
    var strRoomAdult = "";
    var strRoomChild = "";
    var strRoom = "";
    for (var count = 0; count < room; count++) {
        var selectTag = [];
        selectTag = count;
        var dropDownIndex = "";
        dropDownIndex = count + 1;
        strRoomAdult += $("#" + "ddlAdultModify" + dropDownIndex).val() + ",";
        strRoomChild += $("#" + "ddlChildModify" + dropDownIndex).val() + ",";
    }
    strRoom = strRoomAdult + ":" + strRoomChild;
    $("#hdnRoomString").val(strRoom);
    if (ModifyvalidateHotelFields()) {

        var strCity = "city=" + $("#CP_txtCity").val() + "&";
        var strCheckIn = "checkin=" + $("#CP_TextBoxCheckin").val() + "&";
        var strCheckout = "checkout=" + $("#CP_TextBoxCheckout").val() + "&";
        var strRoomstring = "roomstring=" + $("#hdnRoomString").val() + "&";
        //        var isRedeemMiles = "isRedeemMiles=" + $("#ChkRedeemHotel").is(":checked").toString();
        var isRedeemMiles = "isRedeemMiles=true";
        var queryString = strCity + strCheckIn + strCheckout + strRoomstring + isRedeemMiles;
        window.location = "HotelsSearchWait.aspx?" + queryString;
        return false;
    } else {
        return false;
    }
}

function ModifyvalidateHotelFields() {
    var msg = "";
    if ($("#CP_txtCity").val().length == 0 || $("#CP_txtCity").val() == "Enter City Name") {
        msg += "Please Enter City Name.<br>";
        $("#CP_txtCity").addClass("error");
    }
    if ($("#CP_TextBoxCheckin").val().length == 0 || $("#CP_TextBoxCheckin").val() == "Enter Check in Date") {
        msg += "Please Enter Check in Date.<br>";
        $("#CP_TextBoxCheckin").addClass("error");
    }
    if ($("#CP_TextBoxCheckout").val().length == 0 || $("#CP_TextBoxCheckout").val() == "Enter Check out Date") {
        msg += "Please Enter Check out Date.<br>";
        $("#CP_TextBoxCheckout").addClass("error");
    }
    if (msg.length > 0) {
        $("#HotelModifyValidation").show();
        $("#HotelModifyValidation")[0].innerHTML = "<span class='heading-semibold text-danger d-block'>Below fields are mandatory.</span>";
        return false;
    }
    else
        return true;
}

function validateHotelFields() {
    var msg = "";
    if ($("#CP_txtCity").val().length == 0 || $("#CP_txtCity").val() == "Enter City Name") {
        msg += "Please Enter City Name.<br>";
        $("#CP_txtCity").addClass("error");
    }
    if ($("#CP_TextBoxCheckin").val().length == 0 || $("#CP_TextBoxCheckin").val() == "Enter Date") {
        msg += "Please Enter Check in Date.<br>";
        $("#CP_TextBoxCheckin").addClass("error");
    }
    if ($("#CP_TextBoxCheckout").val().length == 0 || $("#CP_TextBoxCheckout").val() == "Enter Date") {
        msg += "Please Enter Check out Date.<br>";
        $("#CP_TextBoxCheckout").addClass("error");
    }
    if (msg.length > 0) {
        $("#HotelModifyValidation").show();
        $("#HotelModifyValidation")[0].innerHTML = "<span class='heading-semibold text-danger d-block'>Below fields are mandatory.</span>";
        return false;
    }
    else
        return true;
}
function loadRoomsOnPage(noofRooms) {
    var strTemplate = "";
    var room = [];
    var AdultPerRoom = $("#CP_hdnNoAdult").val();
    var ChildPerRoom = $("#CP_hdnNoChild").val();
    var SplitAdult = [];
    var SplitChild = [];
    var lintAdultCount = 0;
    var lintChildCount = 0;
    SplitAdult = AdultPerRoom.split(',');
    SplitChild = ChildPerRoom.split(',');
    for (var count = 0; count < noofRooms; count++) {
        for (lintAdultCount; lintAdultCount <= SplitAdult.length - 1; lintAdultCount++) {

            strTemplate += GenrateAdultScript(SplitAdult[lintAdultCount], count);
            lintAdultCount = lintAdultCount + 1;
            break;
        }
        for (lintChildCount; lintChildCount <= SplitChild.length - 1; lintChildCount++) {

            strTemplate += GenrateChildScript(SplitChild[lintChildCount], count);
            lintChildCount = lintChildCount + 1;
            break;
        }

        if (count > lintAdultCount) {

            strTemplate += GenrateAdultScript(1, count);
            strTemplate += GenrateChildScript(0, count);

        }

       

    }
    strTemplate += '<div class="col-md-6 mt-3 d-flex flex-column align-items-end justify-content-end mb-3 mb-sm-0">';
    strTemplate += '<div class="invisible d-none w-100">';
    strTemplate += '<div class="dvLabel d-flex justify-content-between">';
    strTemplate += '<label class="checkbox-container d-flex">';
    strTemplate += '<span class="d-inline-block">';
    strTemplate += '<input type="checkbox" disabled="disabled" checked="checked" />';
    strTemplate += '<span class="checkmark"></span>';
    strTemplate += '</span>';
    strTemplate += '<span class="d-inline-block ml-2">Redeem Points</span>';
    strTemplate += '</label>';
    strTemplate += '</div>';
    strTemplate += '</div>';
    strTemplate += '<button onclick="var retvalue = SearchRooms(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" value="Search" class="btn btn-one w-100">Search Hotel</button>';
    strTemplate += '</div>';
    //strTemplate = "<div class='addRoom'>" + strTemplate + "</div>";
    //BindSelectSelectric();
    return strTemplate;
}

function GenrateAdultScript(adult, count) {
    var htmlString = "";
    if (count % 2 == 0) {
        count = count + 1;
        htmlString += "<div class='col-md-6 col-sm-12 room-" + 2 + "'><label class='rooms-cont'><span class='h8 heading-semibold'> Room </span> <span class='h8 heading-semibold'>" + count + "</span></label>";
    }
    else {
        count = count + 1;
        htmlString += "<div class='col-md-6 col-sm-12 rooms" + 1 + "'><label class='rooms-cont'><span class='h8 heading-semibold'> Room </span> <span class='h8 heading-semibold'>" + count + "</span></label>";
    }
    htmlString += "<div class='row'><div class='col-md-6 col-6'>";
    htmlString += "<label class='h8 heading-semibold'>Adult(s) 12+ Yrs</label>";
    htmlString += "<select id='ddlAdult" + count + "' class='form-control'>";
    for (var i = 1; i < 5; i++) {
        if (i == adult) {
            htmlString += "<option value='" + i + "' selected='selected' >" + i + "</option>";
        }
        else {
            htmlString += "<option value='" + i + "'>" + i + "</option>";
        }
    }
    htmlString += "</select></div>";
    return htmlString;
}

function GenrateChildScript(child, count) {
    var htmlString = "";
    htmlString += "<div class='col-md-6 col-6'>";
    htmlString += "<label class='h8 heading-semibold'>Child(ren) 2 - 11 Yrs</label>";
    htmlString += "<select id='ddlChild" + count + "' class='form-control'>";
    for (var i = 0; i < 3; i++) {
        if (i == child) {
            htmlString += "<option value='" + i + "' selected='selected' >" + i + "</option>";
        }
        else {
            htmlString += "<option value='" + i + "'>" + i + "</option>";
        }
    }
    htmlString += "</select></div></div></div>";
    
    return htmlString;
}

function placeholderOnFocus(obj, defaultVal) {
    if (obj.value == "") {
        obj.value = defaultVal;
    } else if (obj.value == defaultVal) {
        obj.value = "";
    } else { }
    $("#" + obj.id).removeClass('error');
}


$(document).ready(function () {
    $("#ImgCheckin").click(function () {
        $("#TextBoxCheckin").datepicker('show');
    });
    $("#ImgCheckout").click(function () {
        $("#TextBoxCheckout").datepicker('show');
    });

    BindRoomsDynamic(1);
    $("#TextBoxCheckin").click(function () {
        $(this).datepicker('show');
    });
    $("#TextBoxCheckout").click(function () {
        $(this).datepicker('show');
    });
    $("#CP_txtCity").click(function () {
        $(this).val('');
    });
    $("#CP_txtCity").autocomplete({
        source: function (request, response) {
            var list = [];
            $.ajax({
                type: 'POST',
                url: 'FlightLBMSServices.aspx/GetAllHotelCities',
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
        },

        select: function (event, ui) {
            $("#CP_txtCity").val(ui.item.label);
            $("#CP_txtCity").val(ui.item.value);

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
            var autoResult = "<span style='font-size:12px;font-weight:bold'>" + cityname + "," + countryname + "," + countryCode + "</span>";
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style=''>" + autoResult + "</a>").appendTo(ul);
        }
        else if (strArr.length == 1)//for Airline
        {
            var Airline = strArr[0];
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style=''>" + Airline + "</a>").appendTo(ul);
        }
        else {
            var airportname = strArr[2] + ", " + strArr[3];
            var airportcity = strArr[1] + "(" + strArr[0].replace(" ", "") + ")";
            var autoResult = "<span style='font-size:11px'>" + airportname + "</span><br/>" + "<span style='font-size:11px;font-weight:normal'>" + airportcity + "</span>";
            return $("<li style='width:100%'></li>").data("item.autocomplete", item).append("<a style=''>" + autoResult + "</a>").appendTo(ul);
        }

    };

    var strnoofrooms = $("#CP_HFNoOfRooms").val();
    $('#ddlnoofroom').on('change', function () {
        BindRoomsDynamic($('#ddlnoofroom').find('option:selected').val());
    });

    if (strnoofrooms != undefined) {
        var strTemplate = loadRoomsOnPage(strnoofrooms);
        $("#tblDynamic").html(strTemplate);
        $("#ddlnoofroom").val(strnoofrooms);
        //$("#ddlnoofroom").selectric('refresh');
        //$('#tblDynamic select').selectric("refresh");
    }

});
function BindRoomsDynamic(Count) {
    if ($('#tblDynamic').length != 0) {
        $('#tblDynamic')[0].innerHTML = '';
        var htmlString = "";
        var RoomCount = 0;
        for (var i = 0; i < Count; i++) {
            /*if (i % 2 == 0) {
                htmlString += "<div class='rooms1'>";
            }
            else {
                htmlString += "<div class='room-2'>";
            } */
            RoomCount = i + 1;
            htmlString += "<div class='col-md-6 mb-3'>";
            htmlString += "<label><span class='h8 heading-semibold'> Room </span> <span class='h8 heading-semibold'>" + RoomCount + "</span></label>";
            htmlString += "<div class='row'>";
            htmlString += "<div class='col-6 col-md-6'>";
            htmlString += "<label class='h8 heading-semibold'>Adults(12+ Yrs)</label><div class=''><select class='form-control' id='ddlAdult" + i + "'>";
            htmlString += "<option value='1'>1</option>";
            htmlString += "<option value='2'>2</option>";
            htmlString += "<option value='3'>3</option>";
            htmlString += "<option value='4'>4</option>";
            htmlString += "</select></i></div>";
            htmlString += "</div><div class='col-6 col-md-6'>";
            htmlString += "<label class='h8 heading-semibold'>Children(2 - 11Yrs)</label><div class=''><select class='form-control' id='ddlChild" + i + "'>";
            htmlString += "<option value='0'>0</option>"
            htmlString += "<option value='1'>1</option>"
            htmlString += "<option value='2'>2</option>"
            htmlString += "</select></div></div>";
            htmlString += "</div>"
            htmlString += "</div>"

            

            /*htmlString += "<label class='rooms-cont'>Room " + RoomCount + "</label><div class='room-col'><label>Adult(s) 12+ Yrs</label>";
            htmlString += "<select class='dropdown-select down-arrow' id='ddlAdult" + i + "'>";
            htmlString += "<option value='1'>1</option>"
            htmlString += "<option value='2'>2</option>"
            htmlString += "<option value='3'>3</option>"
            htmlString += "<option value='4'>4</option>"
            htmlString += "</select>";
            htmlString += "</div><div class='room-col'>";
            htmlString += "<label>Child(s) 2-11 Yrs</label>";
            htmlString += "<select id='ddlChild" + i + "' class='dropdown-select down-arrow'>";
            htmlString += "<option value='0'>0</option>"
            htmlString += "<option value='1'>1</option>"
            htmlString += "<option value='2'>2</option>"
            htmlString += "</select>";
            htmlString += "</div></div>"; */
        }
        /*htmlString += "</div>";
        htmlString += '<div class="col-12 searchBtn">';
        htmlString += '<div class="invisible d-none w-100 mt-3">';
        htmlString += '<div class="dvLabel d-flex justify-content-between">';
        htmlString += '<label class="checkbox-container d-flex">';
        htmlString += '<span class="d-inline-block">';
        htmlString += '<input type="checkbox" disabled="disabled" checked="checked" />';
        htmlString += '<span class="checkmark"></span>';
        htmlString += '</span>';
        htmlString += '<span class="d-inline-block ml-2">Redeem Points</span>';
        htmlString += '</label>';
        htmlString += '</div>';
        htmlString += '</div>';
        htmlString += '<button onclick="var retvalue = SearchRooms(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" value="Search" class="btn btn-one custom-width">Search Hotel</button>';
        htmlString += '</div>';*/

        //htmlString += '<div class="col-md-6 mt-3 mt-md-0 d-flex flex-column align-items-end justify-content-end mb-3">';
        //htmlString += '<div class="d-none w-100">';
        //htmlString += '<div class="dvLabel d-flex justify-content-between">';
        //htmlString += '<label class="checkbox-container d-flex">';
        //htmlString += '<span class="d-inline-block">';
        //htmlString += '<input type="checkbox" disabled="disabled" checked="checked" />';
        //htmlString += '<span class="checkmark"></span>';
        //htmlString += '</span>';
        //htmlString += '<span class="d-inline-block ml-2">Redeem Points</span>';
        //htmlString += '</label>';
        //htmlString += '</div>';
        //htmlString += '</div>';
        //htmlString += '<button onclick="var retvalue = SearchRooms(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" value="Search" class="btn btn-one w-100">Search Hotel</button>';
        //htmlString += '</div>';
        $('#tblDynamic').empty().append(htmlString);
        //$('#tblDynamic select').selectric("refresh");

    }

}

/*function BindSelectSelectric() {

}*/

function getHotelDetails(hotelId) {
    $.ajax({
        type: "POST",
        url: "HotelResults.aspx/SetHotelId",
        data: "{'pstrHotelId':'" + hotelId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var linkurl = "HotelDetails.aspx";
            window.location.href = linkurl;
        }
    });


}