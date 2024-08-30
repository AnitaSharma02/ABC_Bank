
$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    })
}

$(document).ready(function () {
    //    var pathname = window.location.href.substr(window.location.href.lastIndexOf("/") + 1);
    //    var PageName = pathname.substr(0, pathname.lastIndexOf('?'));
    //    var hotelId = window.location.href.substr(window.location.href.lastIndexOf("=") + 1);
    GetHotelInfo()
    BindHotelDetails();
    BindNextHotel();
});

function BindNextHotel() {
    $.ajax({
        type: "POST",
        url: "HotelDetails.aspx/BindNextHotel",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            HotelNextList = msg.d;
            var HotelList = '';
            HotelList += '<div class="row">'
            for (icount = 0; icount < HotelNextList.length; icount++) {
                HotelList += '<div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4"><div class="dvItem"><a class="anchor" onclick="return getHotelDetails(' + HotelNextList[icount].hotelid + ')"><div class="img-container"><img onError="this.onerror=null;this.src=&quot;images/no-image.png&quot;" src="' + HotelNextList[icount].basicinfo.thumbnailimage + '" /></div><h2 class="px-3 pt-3 pb-2">' + HotelNextList[icount].basicinfo.hotelname + '</h2><div class="d-flex flex-wrap justify-content-between px-3 pb-3"><p class="points">' + HotelNextList[icount].roomrates.RoomRate[0].ratebreakdown.rate[0].RatePoint + ' Points </p><p class="points"></p></div><div class="rating px-3 mb-3">'
                    for (irating = 0; irating < parseInt(HotelNextList[icount].basicinfo.starrating); irating++) {
                        HotelList += '<img class="mr-1" src="Images/icons/other/star-fill.svg">';
                    }
                HotelList += '</div></a></div></div>'
                //HotelList += '<div class="rating">';
                /*for (irating = 0; irating < parseInt(HotelNextList[icount].basicinfo.starrating); irating++) {
                    HotelList += '<img src="Images/icons/other/star-fill.svg">';
                }*/
                //HotelList += '</div>';
            }
            HotelList += '</div>'
            //HotelList += "<div class='clr'></div>";

            $("#NextHotelList").append(HotelList);
            $(".AmtStylePoint").digits();
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    return false;
}
function BindHotelDetails() {
    $.ajax({
        type: "POST",
        url: "HotelDetails.aspx/BindHotelDetails",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            HotelResponse = $.parseJSON(msg.d[0]);
            var HotelRating = "";
            var rptRoom = "";
            for (icount = 0; icount < HotelResponse[0].roomrates.RoomRate.length; icount++) {
                rptRoom += "<div class='col-12'> ";
                rptRoom += "<div class='row justify-content-between'><div class='col-12 col-md-6 col-lg-5 col-xl-6 mb-2 mb-md-0'><h2 class='h6 heading-regular text-colour7'>" + HotelResponse[0].roomrates.RoomRate[icount].roomtype.roomdescription + " (inclusive of all taxes)</h2></div><div class='col-6 col-sm-4 col-md-2 col-lg-3 col-xl-2 mb-3 mb-sm-0'><span class='d-inline-block h6 heading-semibold text-colour7 totalPointValue'>" + HotelResponse[0].roomrates.RoomRate[icount].ratebreakdown.rate[0].RatePoint + " <span class='d-inline-block h7 heading-semibold text-colour7'>Points</span></span><span class='d-inline-block h7 heading-regular text-colour7'>  (per room per night)</span></div>";
                rptRoom += "<div class='col-6 col-sm-4 col-md-2 mb-3 mb-sm-0'><span class='d-inline-block h6 heading-semibold text-colour7 totalPointValue'>" + HotelResponse[0].roomrates.RoomRate[icount].TotalPoints + " <span class='d-inline-block h7 heading-semibold text-colour7'>Points</span></span><span class='d-inline-block h7 heading-regular text-colour7'> for " + msg.d[1] + " night(s)</span></div>";
                rptRoom += "<div class='col-12 col-sm-4 col-md-2 text-md-right'><button class='btn btn-one w-100 totalPointValue' onclick='return Bookroom(&quot;" + HotelResponse[0].roomrates.RoomRate[icount].roomtype.roomtypecode + "&quot;);'>Book Now</button></div></div></div>";
                rptRoom += "<div class='col-12 my-3'><div class='border-bottom'></div></div>"
            }
            $("#minrate").html("<span class='h3 heading-semibold text-colour7 totalPointValue'>" + HotelResponse[0].roomrates.RoomRate[0].TotalPoints + "</span><span class='h6 heading-regular text-colour7'> Points</span> <span class='h6 heading-regular text-colour7'>(for " + msg.d[1] + " night(s))</span>");
            $("#rptRoomDetails").append(rptRoom);
            for (irating = 0; irating < parseInt(HotelResponse[0].basicinfo.starrating); irating++) {
                HotelRating += '<img class="mr-1" src="Images/icons/other/star-fill.svg">';
            }
            $("#divrating").append(HotelRating);
            $(".totalPointValue").digits();

        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    return false;
}
function Bookroom(roomtypecode) {
    var pstrroomtypecode = roomtypecode;
    $.ajax({
        type: "POST",
        url: "HotelDetails.aspx/Bookroom",
        data: "{'pstrroomtypecode':'" + pstrroomtypecode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            window.location.href = data.d;
            return false;
        },
        error: function (response) {
            alert(response);
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    return false;
}
function GetHotelInfo() {
    $.ajax({
        type: "POST",
        url: "HotelDetails.aspx/GetHotelInfo",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //Call
            value = $.parseJSON(msg.d);
            $("#HotelName").text(value.basicinfo[0].hotelname);
            $("#HotelAddress").append(value.basicinfo[0].address);
            //$("#HotelCity").append(value.basicinfo[0].city + '&nbsp;' + value.basicinfo[0].zip + '&nbsp;' + value.basicinfo[0].country);

            if (value.basicinfo[0].communicationinfo.email != "") {
                var html = "";
                html += '<li class="btmSpace"><div class="divw20"><span class="ico_email"></span></div><div class="divw80">';

                var array = value.basicinfo[0].communicationinfo.email.split(",");
                if (array.length == 1) {
                    html += ' <a href="mailto:' + array[i] + '">' + array[i] + '</a>';
                }
                else {
                    for (i = 0; i < array.length; i++) {
                        html += ' <a href="mailto:' + array[i] + '">' + array[i] + '</a>,<br>';
                    }
                }
                html += '</div></li>';
                $("#contact2").append(html);
            }
            else {

                //$("#contact2").append('<div class="btmSpace amey"><div class="divw20"><span class="ico_email"></span></div><div class="divw80"><a href="javascript:void(0);">Not Available</a></div></div>');
                $("#contact2").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-phone.png" alt="" /><span class="ml-2">91-11-27052700</span></div>');
            }
            if (value.basicinfo[0].communicationinfo.fax != "") {
                var html = "";
                //html += '<li class="btmSpace amey"><div class="divw20"><span class="ico_fax"></span></div><div class="divw80">';
                //html += '<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-fax.png" alt="" /><span class="ml-2"></span></div>';

                var array = value.basicinfo[0].communicationinfo.fax.split(",");
                for (i = 0; i < array.length; i++) {
                    html += array[i];
                }
                //html += '</div></li>';
                $("#contact2").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-fax.png" alt="" /><span class="ml-2">' + html + '</span></div>');
            }
            else {
                //$("#contact2").append('<li class="btmSpace"><div class="divw20"><span class="ico_fax"></span></div><div class="divw80">Not Available</div></li>');
                $("#contact2").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-fax.png" alt="" /><span class="ml-2">Not Available</span></div>');
            }
            if (value.basicinfo[0].communicationinfo.phone != "") {
                var html = "";
                //html += '<li class="btmSpace ameys"><div class="divw20"><span class="ico_phone"></span></div><div class="divw80">';
                //html += '<li class="btmSpace ameys"><div class="divw20"><span class="ico_phone"></span></div><div class="divw80">';

                var array = value.basicinfo[0].communicationinfo.phone.split(",");
                for (i = 0; i < array.length; i++) {
                    html += array[i];
                }
                //html += '</div></li>';
                $("#contact1").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-fax.png" alt="" /><span class="ml-2">' + html + '</span></div>');

            }
            else {
                //$("#contact1").append('<li class="btmSpace"><div class="divw20"><span class="ico_phone"></span></div><div class="divw80">Not Available</div></li>');
                $("#contact1").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-fax.png" alt="" /><span class="ml-2">Not Available</span></div>');
            }
            if (value.basicinfo[0].communicationinfo.website != "") {
                var html = "";
                //html += '<li class="btmSpace ameysa"><div class="divw20"><span class="ico_weblink"></span></div><div class="divw80">';
                html += '<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-weblink.png" alt="" /><span class="ml-2">91-11-27052700</span></div>';

                var array = value.basicinfo[0].communicationinfo.website.split(",");
                for (i = 0; i < array.length; i++) {
                    html += '<a class="websitelnk" href="http://' + array[i] + '" target="_blank">' + array[i] + ',</a><br>';
                }
                //html += '</div></li>';
                $("#contact1").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-weblink.png" alt="" /><span class="ml-2">' + html + '</span></div>');
            }
            else {
                //$("#contact1").append('<li class="btmSpace ameysa"><div class="divw20"><span class="ico_weblink"></span></div><div class="divw80"><a class="websitelnk">Not Available</a></div></li>');
                $("#contact1").append('<div class="col-6 d-flex align-items-center"><img src="images/icons/other/ico-weblink.png" alt="" /><span class="ml-2">Not Available</span></div>');
            }
            if (value.basicinfo[0].overview != "" || value.otherinfo.Description != "") {
                $("#OverView").append('<h2 class="h4 heading-semibold text-colour7 bg-colour2 p-3 mb-3">About This Hotel</h2><p>' + value.basicinfo[0].overview + "</p><p>" + value.otherinfo.Description + "</p>");
            }
            else {
                $("#OverView").append('<h2 class="Header" data-i18n="hotel-detail-aboutthis">About This Hotel</h2><p> Not Available</p>');
            }
            $("#imgMap").attr("src", "http://maps.googleapis.com/maps/api/staticmap?&size=300x200&markers=" + value.otherinfo.locationinfo.latitude + "," + value.otherinfo.locationinfo.longitude + "&maptype=roadmap&zoom=14&sensor=false");

            var imageid = 0;
            var counter = 1;
            if (value.otherinfo.imageinfo != " " && value.otherinfo.imageinfo != null) {
                //var html = '<img id="Imgwide" class="img-fluid" src="' + value.otherinfo.imageinfo[0].wideangleimageurl + '" />';
                //$("#BannerImage").append(html);

                value.otherinfo.imageinfo.forEach((image) => {
                    //image.thumbnailimageurl
                    //image.wideangleimageurl
                    $("#BannerImage").append(`<div class="swiper-slide img-container"><img src='${image.wideangleimageurl}' /></div>`);
                    $("#ThumbBannerImage").append(`<div class="swiper-slide img-container"><img src='${image.thumbnailimageurl}' /></div>`);
                });

                /*$('#divpre').click(function () {
                    if (imageid > 0) {
                        imageid = imageid - 1;
                        var temppresrc = '' + value.otherinfo.imageinfo[imageid].wideangleimageurl;
                        $("#Imgwide").attr('src', temppresrc);
                    }

                });
                $('#divNext').click(function () {
                    if (imageid <= value.otherinfo.imageinfo.length - 1) {
                        imageid = imageid + 1;
                        if (imageid < value.otherinfo.imageinfo.length) {
                            var tempnextsrc = '' + value.otherinfo.imageinfo[imageid].wideangleimageurl;
                            $("#Imgwide").attr('src', tempnextsrc);
                        }
                    }
                });
                $('#Imgwide').click(function () { return false; }); // Adds another click event
                $('#Imgwide').off('click');*/
            } else { $("#BannerImage").hide(); }
            //Code for Binding All Amenities in popup
            var html2 = '<div class="col-12">'
            var AllAmenities = "";
            for (var countAmenities = 0; countAmenities < value.basicinfo[0].hotelamenities.Amenities.length; countAmenities++) {
                AllAmenities += value.basicinfo[0].hotelamenities.Amenities[countAmenities].category + ",";
                //html2 += '<h2>' + value.basicinfo[0].hotelamenities.Amenities[countAmenities].category + '</h2>';
                $("#DivAmenitiesCategoryName").append(value.basicinfo[0].hotelamenities.Amenities[countAmenities].category);
                html2 += '<div class="row">'
                for (var countAmenitiesType = 0; countAmenitiesType < value.basicinfo[0].hotelamenities.Amenities[countAmenities].Amenities.hotelamenity.length - 1; countAmenitiesType++) {
                    AllAmenities += value.basicinfo[0].hotelamenities.Amenities[countAmenities].Amenities.hotelamenity[countAmenitiesType].Value + ",";

                    html2 += '<div class="col-12">' + '<div class="border-bottom py-1"> &rArr; ' + value.basicinfo[0].hotelamenities.Amenities[countAmenities].Amenities.hotelamenity[countAmenitiesType].Value + '</div></div>';

                }
                html2 += '<div/>'
                //html2 += '</table>';

            }
            html2 += '</div>';
            $("#DivAmenities").append(html2);
            //Code for Binding Basic Amenities
            BindBasicAmenities(AllAmenities);


        },
        error: function (response) {
            alert(response);
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
    return false;
}
function BindBasicAmenities(AllAmenities) {
    var AmenitiesValue = AllAmenities.split(",");
    var BasicAmenities = "<div class='dvAmenities row mt-2'><div class='col-12'><h2 class='h4 heading-semibold text-colour7'> Amenities</h2></div>";
    var Air = 0; var rest = 0; var spa = 0;
    var meeting = 0; var pool = 0; var wifi = 0;
    for (i = 0; i < AmenitiesValue.length - 1; i++) {
        //if (AmenitiesValue[i].toLowerCase() == "Air conditioning".toLowerCase()) {

        //    if (Air == 0) {
        //        BasicAmenities += "<div class='col-12'><img src='Images/AC_Car_Icon.png'><span>Air Conditioning</span></div>";
        //        Air = Air + 1;
        //    }
        //}
        if (AmenitiesValue[i].toLowerCase() == "meeting facilities" || AmenitiesValue[i].toLowerCase() == "conference suite" || AmenitiesValue[i].toLowerCase() == "meeting rooms" || AmenitiesValue[i].toLowerCase() == "business center") {

            if (meeting == 0) {
                BasicAmenities += "<div class='col-12'><img src='Images/hotelpage/icons/meet-icon.svg'><span> &nbsp; Business Centre </span></div>";
                meeting = meeting + 1;
            }
        }
        if (AmenitiesValue[i].toLowerCase() == "restau" || AmenitiesValue[i].toLowerCase() == "restaurant" || AmenitiesValue[i].toLowerCase() == "coffee shop") {

            if (rest == 0) {
                BasicAmenities += "<div class='col-12'><img src='Images/hotelpage/icons/coffee-icon.svg'><span> &nbsp; Coffee Shop </span></div>";
                BasicAmenities += "<div class='col-12'><img src='Images/hotelpage/icons/resto-icon.svg'><span> &nbsp; Restaurant </span></div>";
                rest = rest + 1;
            }
        }
        if (AmenitiesValue[i].toLowerCase() == "spa" || AmenitiesValue[i].toLowerCase() == "gym" || AmenitiesValue[i].toLowerCase() == "fitness center" || AmenitiesValue[i].toLowerCase() == "health club") {

            if (spa == 0) {
                BasicAmenities += "<div class='col-12'><img src='Images/hotelpage/icons/gym-icon.svg'><span> &nbsp; Gym </span></div>";
                spa = spa + 1;
            }
        }
        if (AmenitiesValue[i].toLowerCase() == "pool" || AmenitiesValue[i].toLowerCase() == "outdoor pool") {

            if (pool == 0) {
                BasicAmenities += "<div class='col-12'><img src='Images/hotelpage/icons/pool-icon.svg'><span>&nbsp; Pool </span></div>";
                pool = pool + 1;
            }
        }
        if (AmenitiesValue[i].toLowerCase() == "complimentary wi-fi access" || AmenitiesValue[i].toLowerCase() == "internet" || AmenitiesValue[i].toLowerCase() == "wi-fi" || AmenitiesValue[i].toLowerCase() == "wi-fi access") {

            if (wifi == 0) {
                BasicAmenities += "<div class='col-12'><img src='Images/hotelpage/icons/wifi-icon.svg'><span> &nbsp; Wi-Fi Access </span></div>";
                wifi = wifi + 1;
            }
        }
    }
    //BasicAmenities += "</div><div onclick='return showAmenities()' class='linkAmenities' data-i18n='hotel-view-more'>View More</div>";
    //BasicAmenities += '</div><button data-toggle="modal" data-target="#dvAmenitiesModal" class="btn btn-one mb-3">View Amenities</button >';
    //$("#BasicAmenities").append(BasicAmenities);
    return false;
}
/*
function showAmenities() {
    $("html").scrollTop(0);
    $("#DivAmenities").addClass("Hotel_Details_Cotent");
    $("#DivAmenities").removeClass("hidecontent");
    $("#Fade").show();
    return false;
}
function CloseDetails() {
    $("#DivAmenities").addClass("hidecontent");
    $("#DivAmenities").removeClass("Hotel_Details_Cotent");
    $("#Fade").hide();
    return false;
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
        },
        beforeSend: function () {
            $("#updProgress").show();
        }
    });
}