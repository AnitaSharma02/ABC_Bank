
function minimumtime() {
    var currentDate = new Date();
    currentDate.setHours(currentDate.getHours() + 48, currentDate.getMinutes(), currentDate.getSeconds(), currentDate.getMilliseconds());
    return currentDate;
}


$(function () {
    var FilterRange = $("#CP_hdnCarFilterRange").val();
    
    FilterRange = $.parseJSON(FilterRange);

    //    $("#priceSlider").slider({

    //        range: true,
    //        min: FilterRange.MinRewards,
    //        max: FilterRange.MaxRewards,
    //        step: 1,
    //        values: [FilterRange.MinRewards, FilterRange.MaxRewards],
    //        slide: function (event, ui) {
    //            var textValue = ui.values[0] + "-" + ui.values[1];
    //            $("#priceRange").text(textValue);
    //        },
    //        stop: function (event, ui) {
    //            // filtersData.ShowHideRows();
    //            showImage();
    //        }
    //    });
    //default initiliztion
    //  $("#priceRange").text(FilterRange.MinRewards + " - " + FilterRange.MaxRewards);


    $("#divCarType").html("");
    for (var i = 0; i < FilterRange.CarType.length; i++) {
        $("#divCarType").append("<div class='cartype'>" +
                                    "<div style='float:left;'>" +
                                        "<input onclick='showImage();' type='checkbox' checked='checked'>" +
                                        "<label class='FloatingCarSearchNormal' for='Points'>&nbsp;" + FilterRange.CarType[i].toString() + "  </label>" +
                                    "</div>" +
                                    "<div ></div>" +
                                "</div>");
    }
    $("#divCarClass").html("");
    for (var i = 0; i < FilterRange.CarClass.length; i++) {
        var carCondition = "";
        if (FilterRange.CarClass[i].toString() == "Yes") {
            carCondition = "Air Conditioning";
        }
        else {
            carCondition = "Non-Air Conditioning";
        }
        $("#divCarClass").append("<div class='carclass'>" +
                                    "<div id='carconidtion' style='display:none;'>" + FilterRange.CarClass[i].toString() + "</div>" +
                                    "<div class='h-checkbx'>" +
                                        "<input type='checkbox' onclick='showImage();' checked id='carclass'" + FilterRange.CarClass[i].toString().replace(" ", "") + "' />" +
                                        "</div>" +
                                    "<div class='FloatingCarSearchNormal'>" + carCondition + "</div>" +
                                 "</div>");
    }
    $("#divCarTransmission").html("");
    for (var i = 0; i < FilterRange.CarTransmission.length; i++) {
        $("#divCarTransmission").append("<div class='cartransmission'>" +
                                            "<div class='h-checkbx'>" +
                                                "<input type='checkbox' onclick='showImage();' checked id='cartrans" + FilterRange.CarTransmission[i].toString().replace(" ", "") + "' />" +
                                                "<label for='Points'></label>" +
                                            "</div>" +
                                            "<div class='filterRange'>" +
                                                "<img src='Images/" + FilterRange.CarTransmission[i].toString().replace(" ", "").toLowerCase() + ".png' style='margin:-3px 3px 0;'>" +
                                            "</div>" +
                                            "<div class='FloatingCarSearchNormal'>" + FilterRange.CarTransmission[i].toString() + "</div></div>");
    }
});

var filtersData = {

    getSlidervalues: function (value1, value2) {
        return this.getSlidervalue(value1) + "-" + this.getSlidervalue(value2)
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
    },
    ShowHideRows: function () {
        var TotalCar = 0;
        //var milesrange = $("#priceSlider").slider("option", "values");
        var FilterRange = $("#CP_hdnCarFilterRange").val();
        FilterRange = $.parseJSON(FilterRange);
        
        var rows = $(".car1");
        var listOfCarType = [];
        var $carTypeCheckBoxLength = $("#divCarType input[type='checkbox']");
        var $carTypeValue = $("#divCarType .cartype");
        for (var i = 0; i < $carTypeCheckBoxLength.length; i++) {
            if ($carTypeCheckBoxLength.eq(i).is(":checked")) {
                listOfCarType.push($.trim($carTypeValue.eq(i).text()));
            }
        }

        var listOfCarClass = [];
        var $carClassCheckBoxLength = $("#divCarClass input[type='checkbox']");
        var $carClassValue = $("#divCarClass .FloatingCarSearchNormal");
        for (var i = 0; i < $carClassCheckBoxLength.length; i++) {
            if ($carClassCheckBoxLength.eq(i).is(":checked")) {
                listOfCarClass.push($carClassValue.eq(i).text());
            }
        }


        var listOfCarTransmission = [];
        var $carTransmissionCheckBoxLength = $("#divCarTransmission input[type='checkbox']");
        var $carTransmission = $("#divCarTransmission .cartransmission");
        for (var i = 0; i < $carTransmissionCheckBoxLength.length; i++) {
            if ($carTransmissionCheckBoxLength.eq(i).is(":checked")) {
                listOfCarTransmission.push($carTransmission.eq(i).text());
            }
        }

        for (var i = 0; i < rows.length; i++) {

            var row = $(rows).eq(i);
            var carType = $(row).find("input[type='hidden']")[0].value;
            var carClass = $(row).find("input[type='hidden']")[1].value;

            var Tmp = $(row).find("input[type='hidden']")[1].value;  

            var carTransmission = $(row).find("input[type='hidden']")[2].value;
            var TotalMiles = $(row).find("input[type='hidden']")[3].value;

            var hasType = listOfCarType.contains(carType);
            var hasClass = listOfCarClass.contains(carClass);
            var hasTransmission = listOfCarTransmission.contains(carTransmission);

            var Car = $(row).find(".jsondata input[type='hidden']").val();
            Car = $.parseJSON(Car);
            //var hasMiles = (TotalMiles >= milesrange[0] && TotalMiles <= milesrange[1]) ? true : false;
            //var result = hasMiles && hasType && hasClass && hasTransmission;
            var result = hasType && hasClass && hasTransmission;
            if (result) {
                $(row).show();
                TotalCar = TotalCar + 1;
            }
            else {
                $(row).hide();
            }
        }


        $("#TotalCarCount").text("Total Car(s) Found: " + TotalCar);
    }
}

Array.prototype.contains = function (needle) {
    for (i in this) {
        if (this[i] == needle) return true;
    }
    return false;
}


function OrderBy(obj, direction) {
    var old_Car_list = [];
    old_Car_list = $("#Cars").find(".Car-block");
    CarDetails = $("#divCarDetails");
    CarDetailsFade = $("#CarFade");    

    var strList = "";

    $("#hdnOrderByObject").val(obj);
    $("#hdnOrderByDirection").val(direction);

    var new_Car_list = [];
    new_Car_list = old_Car_list.sort(SortCar);

    for (var i = 0; i < new_Car_list.length; i++) {
        strList += "<div class='Car-block'>" + new_Car_list.eq(i).html() + "</div>";
    }

    strList += "<div id='divCarDetails' class='hidecontent'>" + CarDetails.html() + "</div>";
    strList += "<div id='CarFade' class='Hotel_Details_back'>" + CarDetailsFade.html() + "</div>";
    $("#Cars").html(strList);
    //filtersData.ShowHideRows();
    showImage();
    return false;
}

function SortCar(a, b) {

    var Car_A = GetJson_A(a);
    var Car_B = GetJson_B(b);
        
    if ($("#hdnOrderByObject").val() == 'duration') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            return Car_A.Vehicle[0].Name.toString().toLowerCase() > Car_B.Vehicle[0].Name.toString().toLowerCase() ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            return Car_A.Vehicle[0].Name.toString().toLowerCase() < Car_B.Vehicle[0].Name.toString().toLowerCase() ? 1 : -1;
        }
    } else if ($("#hdnOrderByObject").val() == 'departuretime') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            return Car_A.Vehicle[0].seats > Car_B.Vehicle[0].seats ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            return Car_A.Vehicle[0].seats < Car_B.Vehicle[0].seats ? 1 : -1;
        }
    } else if ($("#hdnOrderByObject").val() == 'miles') {
        if ($("#hdnOrderByDirection").val() == 'up') {
            return Car_A.Price[0].TotalPoints > Car_B.Price[0].TotalPoints ? 1 : -1;
        }
        if ($("#hdnOrderByDirection").val() == 'down') {
            return Car_A.Price[0].TotalPoints < Car_B.Price[0].TotalPoints ? 1 : -1;
        }
    } else return 1;


}

function GetJson_A(obj) {
    var Car_A = $(obj).find(".jsondata input[type='hidden']").val();
    if (Car_A != undefined) {
        Car_A = Car_A.replace(" \ ", " ");
        Car_A = $.parseJSON(Car_A);
    }
    return Car_A;
}

function GetJson_B(obj) {
    var Car_B = $(obj).find(".jsondata input[type='hidden']").val();
    if (Car_B != undefined) {
        Car_B = Car_B.replace(" \ ", " ");
        Car_B = $.parseJSON(Car_B);
    }
    return Car_B;
}

$(document).ready(function () {

    $("#CP_Image1").click(function () {
        $("#divCarType").hide();
        $("#CP_Image1").hide();
        $("#hideCarType").show();
    });
    $("#CP_Image2").click(function () {
        $("#divCarType").show();
        $("#hideCarType").hide();
        $("#CP_Image1").show();
    });
    $("#CP_Image7").click(function () {
        $("#divCarClass").hide();
        $("#CP_Image7").hide();
        $("#hideCarClass").show();
    });
    $("#CP_Image6").click(function () {
        $("#divCarClass").show();
        $("#CP_Image7").show();
        $("#hideCarClass").hide();
    });

    $("#CP_Image3").click(function () {
        $("#divCarTransmission").hide();
        $("#hideCarTransmission").show();
        $("#CP_Image3").hide();
    });
    $("#CP_Image8").click(function () {
        $("#divCarTransmission").show();
        $("#hideCarTransmission").hide();
        $("#CP_Image3").show();
    });
    $("#CP_Image4").click(function () {
        $("#divCarAirCondition").hide();
        $("#hideCarAirCondition").show();
        $("#CP_Image4").hide();
    });
    $("#CP_Image9").click(function () {
        $("#divCarAirCondition").show();
        $("#hideCarAirCondition").hide();
        $("#CP_Image4").show();
    });
});
function showImage() {
    $("#LoadingResult").show();
    $("#Fadebg").show();
    $("#Fadebg").css("margin-left", "-1px");
    if ($(this).scrollTop() > 200) {
        $("#Fadebg").css("top", "0px");
        $("#LoadingResult").css("top", "40%");
    }
    else {
        $("#Fadebg").css("top", 200 - parseInt($(this).scrollTop()) + "px");
        $("#LoadingResult").css("top", "60%");
    }
    setTimeout(function () {
        filtersData.ShowHideRows()
        hideImage();
    }, 0);
}

function hideImage() {
    $("#LoadingResult").fadeOut(100);
    $("#Fadebg").hide();
    return true;
}

//function LocationSearchCarSearchMobile() {
//    var msg = "";

//    var ddllCountry = $('#CP_ddlCountryMobile');
//    var ddllCity = $('#CP_ddlCityMobile');
//    var ddllLocation = $('#CP_ddlLocationMobile');
//    var ddllDropCountry = $('#CP_ddlDropCountryMobile');
//    var ddllDropCity = $('#CP_ddlDropCityMobile');
//    var ddllDropLocation = $('#CP_ddlDropLocationMobile');
//    var chkDropLocation = $('#CP_droploctionMobile');
//    var divddlDrop = $('#divDropddlMobile');
//    var ltrDriverAge = $('#trDriverAge');

//    if ($.trim(ddllCountry.find('option:selected').text()) == "Select Country" || $.trim(ddllCountry.find('option:selected').text()) == "") {
//        msg = "Please Select PickUp Country." + "<br/>";
//    }
//    else if ($.trim(ddllCity.find('option:selected').text()) == "Select City" || $.trim(ddllCity.find('option:selected').text()) == "") {
//        msg = "Please Select PickUp City." + "<br/>";
//    }
//    else if ($.trim(ddllLocation.find('option:selected').text()) == "Select Location" || $.trim(ddllLocation.find('option:selected').text()) == "") {
//        msg = "Please Select Pickup Location." + "<br/>";
//    }
//    else if ($.trim($("#txtPickUpDate").val()) == "") {

//        msg = "Please Select Pickup Date." + "<br/>";
//    }
//    else if ($.trim($("#txtDropOffDate").val()) == "") {

//        msg = "Please Select Drop Off Date." + "<br/>";
//    }
//    else if (!$("#CP_chkDriverMobile").is(":checked")) {
//        if ($.trim($("#CP_txtDiverAgeMobile").val()) < 21 || $.trim($("#CP_Data_txtDiverAgeMobile").val()) > 70) {
//            msg = "Driver age is not valid." + "<br/>";
//        }
//    }
//    else if (!$("#CP_droploctionMobile").is(":checked")) {
//        if ($.trim(ddllDropCountry.val()) == "Select Country" || $.trim(ddllDropCountry.val()) == "") {
//            msg = "Please Select PickUp Country." + "<br/>";
//        }
//        else if ($.trim(ddllDropCity.val()) == "Select City" || $.trim(ddllDropCity.val()) == "") {
//            msg = "Please Select PickUp City." + "<br/>";
//        }
//        else if ($.trim(ddllDropLocation.val()) == "Select Location" || $.trim(ddllDropLocation.val()) == "") {
//            msg = "Please Select Pickup Location." + "<br/>";
//        }
//    }
//    else if ($.trim($("#txtPickUpDate").val()) != "" || $.trim($("#txtPickUpDate").val()) != "") {
//        var PickUptDate = $("#txtPickUpDate").val();
//        var PickUpHr = $.trim($("#ddlFromTimeMobile").val());
//        var PickUpMin = $.trim($("#ddlMinFromMobile").val());

//        var DropDate = $("#txtDropOffDate").val();
//        var DropHr = $.trim($("#ddlDropFromTimeMobile").val());
//        var DropMin = $.trim($("#ddlDropMinFromMobile").val());

//        var PickUpDateArr = PickUptDate.split('/');
//        var PickupformatedDate = PickUpDateArr[1] + '/' + PickUpDateArr[0] + '/' + PickUpDateArr[2];
//        var DropDateArr = DropDate.split('/');
//        var DropformatedDate = DropDateArr[1] + '/' + DropDateArr[0] + '/' + DropDateArr[2];

//        var PickupDateTime = new Date(PickupformatedDate + ' ' + PickUpHr + ':' + PickUpMin);
//        var DropOffDateTime = new Date(DropformatedDate + ' ' + DropHr + ':' + DropMin);

//        var diff = DropOffDateTime - PickupDateTime;

//        var diffSeconds = diff / 1000;
//        var HH = Math.floor(diffSeconds / 3600);
//        var MM = Math.floor(diffSeconds % 3600) / 60;

//        if (HH < 1) {
//            msg = "your rental must be for at least 1 hours or more";
//        }
//    }

//    else {
//    }

//    if (msg.length > 0) {
//        $("#CP_DivCarErrorMobile").show();
//        //$("#CP_Data_DivCarErrorMobile")[0].innerHTML = msg;

//        return !(msg.length > 0);
//    }
//    else {

//        strPickupCountry = "pickupCountry=" + ddllCountry.find('option:selected').text() + "&";
//        strPickupCity = "pickupCity=" + ddllCity.find('option:selected').text() + "&";
//        var strPickupLocation = "pickupLocation=" + $('#hdnLocationId').val() + "&";

//        var strPickupLocationName = "pickupLocationName=" + ddllLocation.find('option:selected').text() + "&";
//        var strDropofCountry = "";
//        var strDropofCity = "";
//        var strDropoffLocation = "";
//        if ($("#CP_droploctionMobile").attr('checked')) {
//            strDropofCountry = "dropofCountry=" + ddllCountry.find('option:selected').text() + "&";
//            strDropofCity = "dropofCity=" + ddllCity.find('option:selected').text() + "&";
//            strDropoffLocation = "dropoffLocation=" + $('#hdnLocationId').val() + "&";
//            strDropoffLocationName = "dropoffLocationName=" + ddllLocation.find('option:selected').text() + "&";
//        }
//        else {

//            strDropofCountry = "dropofCountry=" + ddllDropCountry.find('option:selected').text() + "&";
//            strDropofCity = "dropofCity=" + ddllDropCity.find('option:selected').text() + "&";
//            strDropoffLocation = "dropoffLocation=" + $("#hdnDropLocationId").val() + "&";
//            strDropoffLocationName = "dropoffLocationName=" + ddllDropLocation.find('option:selected').text() + "&";
//        }


//        var isdropoff = "isdropoff=" + $("#CP_Data_droploctionMobile").is(":checked").toString() + "&";
//        var strDepartDate = "departDate=" + $.trim($("#txtPickUpDate").val()) + "&";
//        var strDepartTime = "departTime=" + $.trim($("#ddlFromTimeMobile").val()) + ":" + $.trim($("#ddlMinFromMobile").val()) + "&";
//        var strReturnDate = "arrDate=" + $.trim($("#txtDropOffDate").val()) + "&";
//        var strReturnTime = "arrTime=" + $.trim($("#ddlDropFromTimeMobile").val()) + ":" + $.trim($("#ddlDropMinFromMobile").val()) + "&";
//        var isdriverage = "isdriverage=" + $("#CP_chkDriverMobile").is(":checked").toString() + "&";
//        var strdriverage = "";

//        if ($("#CP_chkDriverMobile").is(":checked")) {
//            strdriverage = "driverage=" + 21 + "&";
//        }
//        else {
//            strdriverage = "driverage=" + $("#CP_txtDiverAgeMobile").val() + "&";
//        }
//        var strRedeemPoints;
//        if ($("#CP_CheckBoxRedeemCarMobile").is(":checked")) {
//            strRedeemPoints = "isRedeemMiles=" + "true" + "&";
//        }
//        else {
//            strRedeemPoints = "isRedeemMiles=" + "false" + "&";
//        }

//        var hdnddlFromTime = "hdnddlFromTime=" + $("#CP_hdnddlFromTime").val() + "&";
//        var hdnddlToTime = "hdnddlToTime=" + $("#CP_hdnddlToTime").val();
//        var queryString = strPickupCountry + strDropofCountry + strPickupCity + strDropofCity + strPickupLocation + strPickupLocationName + strDropoffLocation + strDropoffLocationName + isdropoff + strDepartDate + strDepartTime + strReturnDate + strReturnTime + isdriverage + strdriverage + hdnddlFromTime + strRedeemPoints + hdnddlToTime;
//        window.location = "CarSearchWait.aspx?" + queryString;
//        return false;
//    }
//}

function ToggleCustomizeSearch() {
    $("#divCustomizesarch").slideToggle(function () {
        if ($('#divCustomizesarch').css("display") == 'none') {
            $('#btnPlusCustomizesearch').attr('src', 'Images/drop1.png');
        } else {
            $('#btnPlusCustomizesearch').attr('src', 'Images/drop2.png');
        }
    });
    var img = $("#ImgCustomize");
    var Path = img[0].src;
    var ImagePath = Path.substring(Path.lastIndexOf("/") + 1);
    if (ImagePath == "Ico_Minus.png") {
        img.attr("src", "Images/Icon_Plus.png");
    }
    else {
        img.attr("src", "Images/Icon_Minus.png");
    }
    return false;
}


function ToggleModifySearch() {

    $("#divModifySearch").slideToggle(function () {
        if ($('#divModifySearch').css("display") == 'none') {
            $('#btnPlusModifysearch').attr('src', 'Images/drop1.png');
        } else {
            $('#btnPlusModifysearch').attr('src', 'Images/drop2.png');
        }
    });
    var img = $("#ImgDetail");
    var Path = img[0].src;
    var ImagePath = Path.substring(Path.lastIndexOf("/") + 1);
    if (ImagePath == "Ico_Minus.png") {
        img.attr("src", "Images/Icon_Plus.png");
    }
    else {
        img.attr("src", "Images/Icon_Minus.png");
    }

    return false;
}
