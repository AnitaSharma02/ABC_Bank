var defaultDropdownsize = 5;
$(document).ready(function () {
    var pathname = (window.location.href.substr(window.location.href.lastIndexOf("/") + 1)).toLowerCase();
});
function BindGlobalCountryList(ddlCountry, ddlCity, ddlLocation, SelectedVal) {
    var args = arguments.length

    ddlCountry.html('');
    ddlCountry.append("<option value='select'>Select Country</option>");

    ddlCity.html('');
    ddlCity.append("<option value='select'>Select City</option>");

    ddlLocation.html('');
    ddlLocation.append("<option value='select'>Select Location</option>");
    $("#CarValidationError").html("");
    $("#CarValidationError").hide();
    $.ajax({
        type: 'POST',
        url: 'Index.aspx/GetAllCountryList',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{}",
        cache: false,
        async: false,
        success: function (msg) {
            var Country = $.parseJSON(msg.d);
            if (Country != null) {
                for (var CountCountry = 0; CountCountry < Country.length; CountCountry++) {
                    if (SelectedVal != '' && SelectedVal != '' && SelectedVal == $.trim(Country[CountCountry].Value)) {
                        ddlCountry.append('<option value="' + $.trim(Country[CountCountry].Value) + '" selected="selected">' + Country[CountCountry].Value + '</option>');
                    }
                    else {
                        ddlCountry.append('<option value="' + $.trim(Country[CountCountry].Value) + '">' + Country[CountCountry].Value + '</option>');
                    }
                }
                ddlCountry.selectric('refresh');
                //if (Country.length == 1) {
                //    BindGlobalCityList(ddlCity, ddlLocation, $.trim(Country[0].Value), '')
                //}
            }
            else {

            }
        },
        error: function (errmsg) {
        }
    });

    return false;
}
function BindGlobalCityList(pCtrlCity, pCtrlLocation, pstrCountry, pstrSelectedCityValue, session) {
    if (pstrCountry.toLowerCase() != "select" && pstrCountry.toLowerCase() != "select") {
        $.ajax({
            type: 'POST',
            url: 'Index.aspx/GetPickupCity',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'Country':'" + pstrCountry + "','session':'" + session + "'}",
            cache: false,
            async: false,
            success: function (msg) {
                if (msg.d != "Error_pickupcity") {
                    $("#CP_DDlCity").removeClass("schfldserr");
                    $("#CP_DDlCity option[value='*Required']").remove();
                    var location = $.parseJSON(msg.d);
                    //bind all city data to the city drop down control.
                    pCtrlCity.empty();

                    pCtrlCity.append("<option value='select' selected='selected'>Select</option>");

                    for (var Countlocation = 0; Countlocation < location.CarPickUpCityListResponce.PickUpCityListResponce.Items[0].City.length; Countlocation++) {
                        if (pstrSelectedCityValue == location.CarPickUpCityListResponce.PickUpCityListResponce.Items[0].City[Countlocation].Value) {
                            pCtrlCity.append("<option selected='selected' value=" + location.CarPickUpCityListResponce.PickUpCityListResponce.Items[0].City[Countlocation].Value + ">" + location.CarPickUpCityListResponce.PickUpCityListResponce.Items[0].City[Countlocation].Value + "</option>");
                        }
                        else {
                            pCtrlCity.append("<option value=" + location.CarPickUpCityListResponce.PickUpCityListResponce.Items[0].City[Countlocation].Value + ">" + location.CarPickUpCityListResponce.PickUpCityListResponce.Items[0].City[Countlocation].Value + "</option>");
                        }
                    }
                    pCtrlCity.selectric('refresh')
                    pCtrlLocation.empty();
                    pCtrlLocation.append("<option value='select' selected='selected'>Select</option>");

                    $("#CP_ddlDropCountry").empty();
                    $("#CP_ddlDropCountry").append("<option value='select' selected='selected'>Select</option>");
                    $("#CP_ddlDropCity").empty();
                    $("#CP_ddlDropCity").append("<option value='select' selected='selected'>Select</option>");
                    $("#CP_ddlDropLocation").empty();
                    $("#CP_ddlDropLocation").append("<option value='select' selected='selected'>Select</option>");

                    pCtrlLocation.selectric('refresh')
                    $(".CarTextBoxCheckin").val("");
                    $(".CarTextBoxCheckout").val("");
                    //}
                }
                else {
                    $("#CarValidationError").html("No City Found, Please Try Again");
                    $("#CarValidationError").show();
                }
                return false;
            },
            error: function (errmsg) {
            }
        });
    }
    else {
        pCtrlCity.empty();
        pCtrlLocation.empty();
        pCtrlCity.append("<option value='select' selected='selected'>Select</option>");
        pCtrlLocation.append("<option value='select' selected='selected'>Select</option>");
        pCtrlCity.selectric('refresh')
        pCtrlLocation.selectric('refresh')
    }
}
function BindGlobalLocationList(pstrCountry, pstrCity, pCtrlLocation, pstrSelectedLocationText, session) {

    pCtrlLocation.empty();
    pCtrlLocation.append("<option value='select' selected='selected'>Select</option>");
    pCtrlLocation.removeClass("schfldserr");
    if (pstrCountry != 'Select' && pstrCountry != 'select' &&
        pstrCity != 'select' && pstrCity != 'Select') {
        $.ajax({
            type: 'POST',
            url: 'Index.aspx/GetPickupLocation',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'Country':'" + pstrCountry + "','City':'" + pstrCity + "','session':'" + session + "'}",
            cache: false,
            async: false,
            success: function (msg) {
                if (msg.d != "Error_PickupLocation") {
                    var location = $.parseJSON(msg.d);
                    $('.CheckInTextClear').empty();
                    $('.CheckOutTextClear').empty();
                    $("#CP_CarTextBoxCheckin").val("");
                    $("#CP_CarTextBoxCheckout").val("");
                    pCtrlLocation.empty();
                    //if (location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location.length == 1) {
                    //    pCtrlLocation.empty();
                    //    pCtrlLocation.html(location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[0].Value);
                    //}
                    //else {

                    if (pstrSelectedLocationText == '' || pstrSelectedLocationText == undefined || pstrSelectedLocationText == null) {
                        pCtrlLocation.append("<option value='select' selected='selected'>Select</option>");
                    }
                    else {
                        pCtrlLocation.append("<option value='select'>Select</option>");
                    }
                    for (var Countlocation = 0; Countlocation < location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location.length; Countlocation++) {

                        if ($.trim(location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[Countlocation].Value) == pstrSelectedLocationText) {
                            pCtrlLocation.append("<option selected='selected' value=" + location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[Countlocation].id + ">" + location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[Countlocation].Value + "</option>");
                        } else {
                            pCtrlLocation.append("<option value=" + location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[Countlocation].id + ">" + location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[Countlocation].Value + "</option>");
                        }


                    }
                    //}
                    pCtrlLocation.selectric('refresh');
                }
                else {
                    $("#CarValidationError").html("No Location Found, Please Try Again");
                    $("#CarValidationError").show();
                }
                return false;
            },
            error: function (errmsg) {
            }
        });
    }
    pCtrlLocation.selectric('refresh');
    return false;
}
function PickUpLocationChange(pCtrlCountry, pCtrlCity, pCtrlLocation, pCtrlDropCountry, pCtrlDropCity, pCtrlDropLocation, session) {

    pCtrlDropCountry.selectric('destroy');
    pCtrlDropCity.selectric('destroy');
    pCtrlDropLocation.selectric('destroy');
    pCtrlDropCountry.empty();
    pCtrlDropCity.empty();
    pCtrlDropLocation.empty();
    if (pCtrlCountry != "select country" && pCtrlLocation.val().toLowerCase() != "select" && pCtrlCity.text().toLowerCase() != "select city") {
        $.ajax({
            type: 'POST',
            url: 'Index.aspx/GetDropOffDetails',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'Country':'" + pCtrlCountry + "','locationId':'" + pCtrlLocation.val() + "','City':'" + pCtrlCity.text() + "','session':'" + session + "'}",
            cache: false,
            success: function (msg) {

                var location = $.parseJSON(msg.d);
                if (location.CarDropOffCountryListResponse != null && location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items != null && location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country != null) {
                    for (var Countlocation = 0; Countlocation < location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country.length; Countlocation++) {
                        pCtrlDropCountry.append("<option value=" + location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country[Countlocation].Value + ">" + location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country[Countlocation].Value + "</option>");

                    }

                    if (location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country.length > 1) {
                        pCtrlDropCountry.val(pCtrlCountry);
                        //                    $("#ContentPlaceHolder1_ddlDropCountry option[value='" + $('#ContentPlaceHolder1_DivDDLCountry').text() + "']").attr("selected", true);
                    }
                    else {
                        pCtrlDropCountry.val(location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country[0].Value)
                    }
                    if (location.CarDropOffCityListResponce != null && location.CarDropOffCityListResponce.DropOffCityListResponce.Items != null && location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City != null) {
                        for (var Countlocation = 0; Countlocation < location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City.length; Countlocation++) {
                            pCtrlDropCity.append("<option value=" + location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value + ">" + location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value + "</option>");
                        }

                        if (location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City.length > 1) {
                            pCtrlDropCity.val(pCtrlCity.val());
                            //                    $("#ContentPlaceHolder1_ddlDropCity option[value='" + $('#ContentPlaceHolder1_DivDDLCity').text() + "']").attr("selected", true);
                        }
                        else {
                            pCtrlDropCity.val(location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[0].Value);
                        }

                        if (location.CarDropOffLocationListResponce != null && location.CarDropOffLocationListResponce.DropOffLocationListResponce.Items != null && location.CarDropOffLocationListResponce.DropOffLocationListResponce.Items[0].Location != null) {
                            for (var Countlocation = 0; Countlocation < location.CarDropOffLocationListResponce.DropOffLocationListResponce.Items[0].Location.length; Countlocation++) {
                                pCtrlDropLocation.append("<option value=" + location.CarDropOffLocationListResponce.DropOffLocationListResponce.Items[0].Location[Countlocation].id + ">" + location.CarDropOffLocationListResponce.DropOffLocationListResponce.Items[0].Location[Countlocation].Value + "</option>");
                            }
                            if (location.CarDropOffLocationListResponce.DropOffLocationListResponce.Items[0].Location.length == 1) {
                                pCtrlDropLocation.val(pCtrlLocation.val());
                                //$('#ContentPlaceHolder1_ddlDropLocation option:nth(0)').attr("selected", "selected");
                            }
                            else {
                                //var indx = $("#ContentPlaceHolder1_DDLLocation").prop("selectedIndex");
                                ///$('#ContentPlaceHolder1_ddlDropLocation option:nth(' + indx + ')').attr("selected", "selected");
                                pCtrlDropLocation.val(pCtrlLocation.val());
                            }
                        }
                        else {
                            $("#CarValidationError").html("No DropOffLocation Found, Please Try Again");
                        }
                    }
                    else {
                        $("#CarValidationError").html("No DropOffLocation Found, Please Try Again");
                    }

                }
                else {
                    $("#CarValidationError").html("No DropOffCountry Found, Please Try Again");
                }



                $('select').selectric({
                    maxHeight: 100,
                    disableOnMobile: false,
                    responsive: true
                });
                return false;
            },
            error: function (errmsg) {
            }
        });
    }
}
function DropoffCityChange(pstrCountry, pstrCity, pCtrlLocation, pstrSelectedLocationText, session) {//GetDropOffLocation
    pCtrlLocation.empty();
    pCtrlLocation.append("<option value='select' selected='selected'>Select Location</option>");
    if (pstrCountry != 'Select Country' && pstrCountry != 'select' && pstrCity != 'select' && pstrCity != 'Select City') {
        $("#CarValidationError").html("");
        $("#CarValidationError").hide();
        $.ajax({
            type: 'POST',
            url: 'Index.aspx/GetDropOffLocationChange',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'Country':'" + pstrCountry + "','locationId':'" + $("#CP_DDLLocation").find('option:selected').prop("value") + "','City':'" + pstrCity + "','session':'" + session + "'}",
            cache: false,
            success: function (msg) {
                if (msg.d != "Error_PickupLocation") {
                    var location = $.parseJSON(msg.d);
                    $('.CheckInTextClear').empty();
                    $('.CheckOutTextClear').empty();
                    $("#CarTextBoxCheckin").val("");
                    $("#CarTextBoxCheckout").val("");
                    pCtrlLocation.empty();
                    //if (location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location.length == 1) {
                    //    pCtrlLocation.append("<option value=" + location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[0].id + ">" + location.CarPickUpLocationListResponce.PickUpLocationListResponce.Items[0].Location[0].Value + "</option>");
                    //}
                    //else {

                    if (pstrSelectedLocationText == '' || pstrSelectedLocationText == undefined || pstrSelectedLocationText == null) {
                        pCtrlLocation.append("<option value='select' selected='selected'>Select Location</option>");
                    }
                    else {
                        pCtrlLocation.append("<option value='select'>Select Location</option>");
                    }
                    for (var Countlocation = 0; Countlocation < location.DropOffLocationListResponce.Items[0].Location.length; Countlocation++) {

                        if ($.trim(location.DropOffLocationListResponce.Items[0].Location[Countlocation].Value) == pstrSelectedLocationText) {
                            pCtrlLocation.append("<option selected='selected' value=" + location.DropOffLocationListResponce.Items[0].Location[Countlocation].id + ">" + location.DropOffLocationListResponce.Items[0].Location[Countlocation].Value + "</option>");
                        } else {
                            pCtrlLocation.append("<option value=" + location.DropOffLocationListResponce.Items[0].Location[Countlocation].id + ">" + location.DropOffLocationListResponce.Items[0].Location[Countlocation].Value + "</option>");
                        }
                    }
                    //}
                    pCtrlLocation.selectric('refresh');
                }
                else {
                    $("#CarValidationError").html("No Location Found, Please Try Again");
                    $("#CarValidationError").show();
                }
                return false;
            },
            error: function (errmsg) {
            }
        });
    }
    pCtrlLocation.selectric('refresh');
    return false;
}
function BindDropOffCountry(ddlCountry, ddlCity, ddlLocation, SelectedVal, session) {//GetDropOffCountry
    var args = arguments.length;
    ddlCountry.html('');
    ddlCountry.append("<option value='select'>Select Country</option>");
    ddlCity.html('');
    ddlCity.append("<option value='select'>Select City</option>");
    ddlLocation.html('');
    ddlLocation.append("<option value='select'>Select Location</option>");
    $("#CarValidationError").html("");
    $("#CarValidationError").hide();
    $.ajax({
        type: 'POST',
        url: 'Index.aspx/BindDropOffCountry',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: "{'Country':'" + $('#CP_DDLCountry option:selected').text() + "','locationId':'" + $('#CP_DDLLocation option:selected').val() + "','City':'" + $('#CP_DDlCity option:selected').text() + "','session':'" + session + "'}",
        cache: false,
        async: false,
        success: function (msg) {
            var location = $.parseJSON(msg.d);
            //dropoffcountrybinding starts here
            if (location.CarDropOffCountryListResponse != null && location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items != null && location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country != null) {
                for (var Countlocation = 0; Countlocation < location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country.length; Countlocation++) {
                    ddlCountry.append('<option value="' + $.trim(location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country[Countlocation].Value) + '">' + location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country[Countlocation].Value + '</option>');
                }
                if (location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country.length > 1) {
                    ddlCountry.val(ddlCountry.val());
                    //                    $("#ContentPlaceHolder1_ddlDropCountry option[value='" + $('#ContentPlaceHolder1_DivDDLCountry').text() + "']").attr("selected", true);
                }
                else {
                    ddlCountry.val(location.CarDropOffCountryListResponse.DropOffCountryListResponce.Items[0].Country[0].Value)
                }
                ddlCountry.selectric('refresh');
            }
        },
        error: function (errmsg) {
        }
    });

    return false;
}
function BindDropOffCity(pCtrlCity, pCtrlLocation, pstrCountry, pstrSelectedCityValue, session) {//GetDropOffCity
    if (pstrCountry.toLowerCase() != "select country" && pstrCountry.toLowerCase() != "select") {
        $("#CarValidationError").html("");
        $("#CarValidationError").hide();
        $.ajax({
            type: 'POST',
            url: 'Index.aspx/GetDropOffCityByDropCountry',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: "{'Country':'" + pstrCountry + "','locationId':'" + $('#CP_DDLLocation option:selected').val() + "','City':'" + $('#CP_DDlCity option:selected').text() + "','dropOffCountry':'" + $('#CP_ddlDropCountry option:selected').text() + "','session':'" + session + "'}",
            cache: false,
            async: false,
            success: function (msg) {
                if (msg.d != "Error_DropOffCity") {
                    var location = $.parseJSON(msg.d);
                    //bind all city data to the city drop down control.
                    pCtrlCity.empty();


                    var initialized = 0;
                    for (var Countlocation = 0; Countlocation < location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City.length; Countlocation++) {
                        if (pstrSelectedCityValue == location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value) {
                            pCtrlCity.append("<option selected='selected' value=" + location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value + ">" + location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value + "</option>");
                            initialized = 1;
                        }
                        else {
                            pCtrlCity.append("<option value=" + location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value + ">" + location.CarDropOffCityListResponce.DropOffCityListResponce.Items[0].City[Countlocation].Value + "</option>");
                        }
                    }
                    if (initialized == 0) {
                        pCtrlCity.append("<option value='select' selected='selected'>Select City</option>");
                    }
                    pCtrlCity.selectric('refresh')
                    pCtrlLocation.empty();
                    pCtrlLocation.append("<option value='select' selected='selected'>Select Location</option>");
                    pCtrlLocation.selectric('refresh')
                    $(".CarTextBoxCheckin").val("");
                    $(".CarTextBoxCheckout").val("");
                }
                else {
                    $("#CarValidationError").html("No City Found, Please Try Again");
                    $("#CarValidationError").show();
                }
                return false;
            },
            error: function (errmsg) {
            }
        });
    }
    else {
        pCtrlCity.empty();
        pCtrlLocation.empty();
        pCtrlCity.append("<option value='select' selected='selected'>Select City</option>");
        pCtrlLocation.append("<option value='select' selected='selected'>Select Location</option>");
        pCtrlCity.selectric('refresh')
        pCtrlLocation.selectric('refresh')
    }
}