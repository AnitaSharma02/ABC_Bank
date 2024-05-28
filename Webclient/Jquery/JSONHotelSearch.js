function hideshowFitler(obj) {
    $("#" + obj).toggle('slow');
    return false;
}


function placeholderOnFocus(obj, defaultVal) {
    if (obj.value == "") {
        obj.value = defaultVal;
    } else if (obj.value == defaultVal) {
        obj.value = "";
    } else { }
}
function gotoHotelBooking(obj) {
    var linkurl = $("#" + obj.id).parent().find("input[type='hidden']").val();
    $.ajax({
        url: 'HotelResults.aspx/BookHotel',
        type: 'POST',  // or get
        contentType: 'application/json; charset=utf-8',
        data: "{'pstrForwardURL':'" + linkurl + "'}",
        dataType: 'json',
        success: function (data) {
            if (data.d)
                window.location = linkurl;
        },
        error: function (errmsg) {
        }
    });
}

function SelectAll(obj, divElement) {
    var checkBox = $("#" + obj);
    var divElement = $("#" + divElement);

    if (checkBox.is(":checked")) {
        for (var i = 0; i <= divElement.find("input[type='checkbox']").length; i++) {
            $(divElement.find("input[type='checkbox']")[i]).prop('checked', true);
        }
    } else {
        for (var i = 0; i <= divElement.find("input[type='checkbox']").length; i++) {
            $(divElement.find("input[type='checkbox']")[i]).prop('checked', false);
        }
    }
    showImage();
    return false;
}

function uniqueArrayElemets(arrayName) {
    var newArray = new Array();
    label: for (var i = 0; i < arrayName.length; i++) {
        for (var j = 0; j < newArray.length; j++) {
            if (newArray[j] == arrayName[i])
                continue label;
        }
        newArray[newArray.length] = arrayName[i];
    }
    return newArray;
}

Array.max = function (array) {
    return Math.max.apply(Math, array);
};
Array.min = function (array) {
    return Math.min.apply(Math, array);
};
Array.prototype.contains = function (needle) {
    for (i in this) {
        if (this[i] == needle) return true;
    }
    return false;
}
$(document).ready(function () {
    $('.modify').click(function () {
        $('.modify-box').toggle();
    });
    var FilterHotelRange = $("#CP_hdnHotelFilterRange").val();
    FilterHotelRange = $.parseJSON(FilterHotelRange);
    $("#priceSlider").slider({
        range: true,
        min: FilterHotelRange.MinPrice,
        max: FilterHotelRange.MaxPrice,
        step: 1,
        values: [FilterHotelRange.MinPrice, FilterHotelRange.MaxPrice],
        slide: function (event, ui) {
            var textValue = CommaSep(ui.values[0]) + " - " + CommaSep(ui.values[1]);
            $("#priceRange").text(textValue);
        },
        stop: function (event, ui) {
            filtersData.ShowHideRows();
        }
    });
    var strRating = "";
    var flag1star = false;
    var flag2star = false;
    var flag3star = false;
    var flag4star = false;
    var flag5star = false;
    var flagNorated = false;
    var rowcount = $(".hotels .hotelrow").length;
    for (var hotelcount = 0; hotelcount < rowcount; hotelcount++) {

        var hotelrow = ($(".hotels .hotelrow")[hotelcount]);
        var rating = $(hotelrow).find(".rating").attr('rating');
        rating = parseInt(rating);

        strrating = "";

        for (var count = 0; count < rating; count++) {
            if (rating <= 5) {
                strrating += "<img src='Images/icons/other/star-fill.svg' />";
            }
            else {
                strrating = "Not Rated";
            }
        }

        $(hotelrow).find('.rating').html(strrating);
        if (rating == 5) {
            flag5star = true;
        }
        if (rating == 4) {
            flag4star = true;
        }
        if (rating == 3) {
            flag3star = true;
        }
        if (rating == 2) {
            flag2star = true;
        }
        if (rating == 1) {
            flag1star = true;
        }
        if (rating > 5) {
            flagNorated = false;
        }

    }
    var strHtml = "";
    /*Code for */
    if (flag5star == true) {
        $("#divRatings").append('<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" value="5" display="5" onclick="showImage()" checked="checked" /><span class="checkmark"></span></span><div><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/></div></label></div>');
        strHtml += "5,";
        $("#Chk5").prop('checked', true);
        $("#ChkAll").prop('checked', false);
    }
    if (flag4star == true) {
        $("#divRatings").append('<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" value="4" display="4" onclick="showImage()" checked="checked" /><span class="checkmark"></span></span><div><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/></div></label></div>');
        strHtml += "4,";
        $("#Chk4").prop('checked', true);
        $("#ChkAll").prop('checked', false);
    }
    if (flag3star == true) {
        $("#divRatings").append('<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" value="3" display="3" onclick="showImage()" checked="checked" /><span class="checkmark"></span></span><div><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/></div></label></div>');
        strHtml += "3,";
        $("#Chk3").prop('checked', true);
        $("#ChkAll").prop('checked', false);
    }
    if (flag2star == true) {
        $("#divRatings").append('<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" value="2" display="2" onclick="showImage()" checked="checked" /><span class="checkmark"></span></span><div><img src="Images/icons/other/star-fill.svg" style="FL"/><img src="Images/icons/other/star-fill.svg" style="FL"/></div></label></div>');
        strHtml += "2,";
        $("#Chk2").prop('checked', true);
        $("#ChkAll").prop('checked', false);
    }
    if (flag1star == true) {
        $("#divRatings").append('<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" value="1" display="1" onclick="showImage()" checked="checked" /><span class="checkmark"></span></span><div><img src="Images/icons/other/star-fill.svg" style="FL"/></div></label></div>');
        strHtml += "1,";
        $("#Chk1").prop('checked', true);
        $("#ChkAll").prop('checked', false);
    }
    if (flagNorated == true) {
        $("#divRatings").append('<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" id="chkRatingNotAvailble" display="NOTAVAILABLE" onclick="showImage()" checked="checked" /><span class="checkmark"></span></span><div><img src="Images/icons/other/star-fill.svg" style="FL"/></div></label></div>');
    }
    if (flag5star == true && flag4star == true && flag3star == true && flag2star == true && flag1star == true) {
        $("#ChkAll").prop('checked', true);
        $("#Chk1").prop('checked', false);
        $("#Chk2").prop('checked', false);
        $("#Chk3").prop('checked', false);
        $("#Chk4").prop('checked', false);
        $("#Chk5").prop('checked', false);
        $("#hdnRating").val('All');
    }
    $("#hdnRating").val(strHtml);
    $("#priceRange").text(CommaSep(FilterHotelRange.MinPrice.toString()) + "-" + CommaSep(FilterHotelRange.MaxPrice.toString()));

    for (var count = 0; count < FilterHotelRange.ListOfLocation.length; count++) {
        if (FilterHotelRange.ListOfLocation[count].toString() != "") {
            $("#divLocation").append("<div class='Hotel_Tab_width'><div class='width10P'><input type='checkbox' display='" + FilterHotelRange.ListOfLocation[count] + "' checked  onclick='filtersData.ShowHideRows()' /></div><div class='aminities_text'> " + FilterHotelRange.ListOfLocation[count] + "</div></div>");
        }
    }

    $("#totalHotel").html("<span data-i18n='hotel-list-hotel-found'> Total Hotel(s) Found: </span>");
    //var HotelChain = "<li class='listHotel'><input type='checkbox' display='None' checked onclick='filtersData.ShowHideRows()' /> None</li>";
    var HotelChain = '<div class="dvLabel d-flex justify-content-between pr-2"><label class= "checkbox-container d-flex"><span class="d-inline-block mr-2"><input type="checkbox" display="None" checked onclick="filtersData.ShowHideRows()" /><span class="checkmark"></span></span><div></div><span class="d-inline-block">None</span></label></div>';
    for (var count = 0; count < FilterHotelRange.ListOfHotelChain.length; count++) {
        if (FilterHotelRange.ListOfHotelChain[count].toString() != "") {
            HotelChain += "<li class='listHotel'><input type='checkbox' display='" + FilterHotelRange.ListOfHotelChain[count] + "' checked onclick='filtersData.ShowHideRows()' /> " + FilterHotelRange.ListOfHotelChain[count] + "</li>";
        }
    }

    $("#divHotelChain").append(HotelChain);
    filtersData.ShowHideRows()
});

var filtersData =
{
    ShowHideRows: function () {
        var TotalHotel = 0;
        var ListOfLocation = [];

        ListOfLocation = $("#divLocation").find("input[type='checkbox']");
        var Location = [];
        for (var count = 0; count < ListOfLocation.length; count++) {
            if (ListOfLocation.eq(count).is(":checked")) {
                Location.push(ListOfLocation.eq(count).attr('display'));
            }
        }
        Location.push("");

        var ListOfChain = [];
        ListOfChain = $("#divHotelChain").find("input[type='checkbox']");
        var Chains = [];
        for (var count = 0; count < ListOfChain.length; count++) {
            if (ListOfChain.eq(count).is(":checked")) {
                Chains.push(ListOfChain.eq(count).attr('display'));
            }
        }

        var ListOfBasicAmentites = [];
        ListOfBasicAmentites = $("#divBasicAmenities").find("input[type='checkbox']");
        var Amenities = [];
        for (var count = 0; count < ListOfBasicAmentites.length; count++) {
            if (ListOfBasicAmentites.eq(count).is(":checked")) {
                Amenities.push(ListOfBasicAmentites.eq(count).attr('display'));
            }
        }

        var ListOfRating = [];
        ListOfRating = $("#divRatings").find("input[type='checkbox']");
        var Ratings = [];
        for (var count = 0; count < 5; count++) {
            if (ListOfRating.eq(count).is(":checked")) {
                Ratings.push(ListOfRating.eq(count).attr('display'));
            }
        }
        var notAvailableChecked = $("#chkRatingNotAvailble").is(":checked");
        var hotels = $(".hotels").find(".hotelrow");
        var counter = 0;
        for (var count = 0; count < hotels.length; count++) {
            hotel = $.parseJSON(hotels.eq(count).find(".jsondata input[type='hidden']").val());
            if (hotel != null) {
                var roomPrice = 0;
                var milesrange = $("#priceSlider").slider("option", "values");
                for (var priceCount = 0; priceCount < hotel.roomrates.RoomRate.length; priceCount++) {
                    roomPrice += parseInt(hotel.roomrates.RoomRate[priceCount].TotalPrice);
                }

                roomPrice = parseInt(hotel.roomrates.RoomRate[0].TotalPoints);
                var hasMiles = false;
                if (roomPrice >= milesrange[0] && roomPrice <= milesrange[1]) {
                    hasMiles = true;
                }
                //            
                var hasRating = false;
                if (hotel.basicinfo.hotelratings.HotelRating != null && hotel.basicinfo.hotelratings.HotelRating.length >= 1) {
                    if (hotel.basicinfo.hotelratings.HotelRating[0].rating <= 5) {
                        hasRating = Ratings.contains(parseInt(hotel.basicinfo.hotelratings.HotelRating[0].rating));
                    }
                    if (notAvailableChecked && hotel.basicinfo.hotelratings.HotelRating[0].rating > 5) {
                        hasRating = true;
                    }
                }
                var hasLocation = false;
                hasLocation = Location.contains(hotel.basicinfo.locality);
                var hasChain = false;
                hasChain = Chains.contains(hotel.basicinfo.chain);
                if (Chains.contains('None')) {
                    if (hotel.basicinfo.chain == '')
                        hasChain = true;
                }
                var AmenitiesCategory = hotel.basicinfo.hotelamenities.Amenities;
                var hasAmenities = false;

                if (AmenitiesCategory != null) {
                    for (var amnCount = 0; amnCount < AmenitiesCategory.length && !hasAmenities; amnCount++) {
                        for (var selectedAmenities = 0; selectedAmenities < Amenities.length && !hasAmenities; selectedAmenities++) {
                            for (var basicAmenity = 0; basicAmenity < AmenitiesCategory[amnCount].Amenities.hotelamenity.length; basicAmenity++) {
                                if (AmenitiesCategory[amnCount].Amenities.hotelamenity[basicAmenity].Value.toString().toLowerCase().indexOf(Amenities[selectedAmenities].toString().toLowerCase()) != "-1") {
                                    hasAmenities = true;
                                    break;
                                }
                            }
                        }
                    }
                }


                var result = hasMiles && hasLocation && hasChain && hasRating && hasAmenities;
                if (result) {
                    hotels.eq(count).show(); counter++;

                    TotalHotel = TotalHotel + 1;
                } else {
                    hotels.eq(count).hide();

                }

                //$("#totalHotel").html('');
                $("#DivTotalHotel").html('');
                //$("#totalHotel").html("<span data-i18n='hotel-list-hotel-found'> " + TotalHotel + "");
                //$("#totalHotel").html("<span data-i18n='hotel-list-hotel-found' class='totalHotelfound'> Total Hotel(s) Found: </span>" + " <p class='totalHotel'>" + TotalHotel + "</p >");
                $("#totalHotel").html("<span> Total <span>Hotels</span> found <span>" + TotalHotel + "</span></span>");
            }
        }
    }
}

function showImage() {
    filtersData.ShowHideRows()
}

function hideImage() {
    $("#LoadingResult").fadeOut(100);
    $("#Fadebg").hide();
    return true;
}

