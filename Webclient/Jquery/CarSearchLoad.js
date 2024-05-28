$(document).ready(function () {
    //Drop down event binding start
    $('#CP_DDLCountry,#CP_DDlCity,#CP_DDLLocation,#CP_ddlDropCountry,#CP_ddlDropCity,#CP_ddlDropLocation,#CP_ddlFromTime,#CP_ddlMinFrom,#CP_ddlDropFromTime,#CP_ddlDropMinFrom').selectric({
        maxHeight: 100,
        disableOnMobile: false,
        responsive: true
    });

    $(".divDropContainer").hide();


    //Drop down event binding start
    $('#CP_DDLCountry').on('change', function () {
        if ($('#CP_DDLCountry option:selected').val() == "more") {
            BindGlobalCountryList($('#CP_DDLCountry'), $('#CP_DDlCity'), $('#CP_DDLLocation'), 'select');
        //    PickUpLocationChange($('#CP_DDLCountry option:selected'), $('#CP_DDlCity option:selected'), $('#CP_DDLLocation option:selected'), $('#CP_ddlDropCountry'), $('#CP_ddlDropCity'), $('#CP_ddlDropLocation'));

        }
        else {
            BindGlobalCityList($('#CP_DDlCity'), $('#CP_DDLLocation'), $('#CP_DDLCountry option:selected').text(), '', "false");
            //PickUpLocationChange($('#CP_DDLCountry option:selected'), $('#CP_DDlCity option:selected'), $('#CP_DDLLocation option:selected'), $('#CP_ddlDropCountry'), $('#CP_ddlDropCity'), $('#CP_ddlDropLocation'));
        }
        ClearDateTime();
        ClearDropDateTime();

        $('#CP_ddlDropCountry').empty();
        $('#CP_ddlDropCountry').append("<option value='select'>Select Country</option>");

        $('#CP_ddlDropCity').empty();
        $('#CP_ddlDropCity').append("<option value='select'>Select City</option>");

        $('#CP_ddlDropLocation').empty();
        $('#CP_ddlDropLocation').append("<option value='select'>Select Location</option>");

        $('#CP_ddlDropCountry').selectric("refresh");
        $('#CP_ddlDropLocation').selectric("refresh");
        $('#CP_ddlDropCity').selectric("refresh");
    });


    $('#CP_CarTextBoxCheckin').blur(function () {
        //BindGlobalLocationList($('#CP_DDLCountry option:selected').val(), $('#CP_DDlCity option:selected').text(), $('#CP_DDLLocation'), '');
        $("#CP_ddlMinFrom").val("00");
        $("#CP_ddlDropMinFrom").val("00");
        $("#CP_ddlMinFrom").selectric('refresh');
        $("#CP_ddlDropMinFrom").selectric('refresh');

    });

    $('#CP_CarTextBoxCheckout').blur(function () {
        //BindGlobalLocationList($('#CP_DDLCountry option:selected').val(), $('#CP_DDlCity option:selected').text(), $('#CP_DDLLocation'), '');
        $("#CP_ddlDropMinFrom").val("00");
        $("#CP_ddlDropMinFrom").selectric('refresh');

    });

    $('#CP_DDlCity').on('change', function () {
        BindGlobalLocationList($('#CP_DDLCountry option:selected').val(), $('#CP_DDlCity option:selected').text(), $('#CP_DDLLocation'), '', 'false');
        ClearDateTime();
        ClearDropDateTime();
        $('#CP_ddlDropCountry').empty();
        $('#CP_ddlDropCountry').append("<option value='select'>Select Country</option>");

        $('#CP_ddlDropCity').empty();
        $('#CP_ddlDropCity').append("<option value='select'>Select City</option>");

        $('#CP_ddlDropLocation').empty();
        $('#CP_ddlDropLocation').append("<option value='select'>Select Location</option>");

        $('#CP_ddlDropCountry').selectric("refresh");
        $('#CP_ddlDropLocation').selectric("refresh");
        $('#CP_ddlDropCity').selectric("refresh");
    });

    $('#CP_DDLLocation').on('change', function () {
        $('#CP_ddlDropCountry').empty();
        $('#CP_ddlDropCountry').append("<option value='" + $('#CP_DDLCountry option:selected').val() + "'>" + $('#CP_DDLCountry option:selected').text() + "</option>");
        //PickUpLocationChange($('#CP_DDLCountry option:selected'), $('#CP_DDlCity option:selected'), $('#CP_DDLLocation option:selected'), $('#CP_ddlDropCountry'), $('#CP_ddlDropCity'), $('#CP_ddlDropLocation'));
        PickUpLocationChange($('#CP_DDLCountry option:selected').val(), $('#CP_DDlCity option:selected'), $('#CP_DDLLocation option:selected'), $('#CP_ddlDropCountry'), $('#CP_ddlDropCity'), $('#CP_ddlDropLocation'), "false");
        ClearDateTime();
        ClearDropDateTime();
        $('#CP_ddlDropCountry').empty();
        $('#CP_ddlDropCountry').append("<option value='select'>Select Country</option>");

        $('#CP_ddlDropCity').empty();
        $('#CP_ddlDropCity').append("<option value='select'>Select City</option>");

        $('#CP_ddlDropLocation').empty();
        $('#CP_ddlDropLocation').append("<option value='select'>Select Location</option>");

        $('select').selectric('refresh');
    });

    $('#CP_ddlDropCity').on('change', function () {
        DropoffCityChange($('#CP_ddlDropCountry option:selected').val(), $('#CP_ddlDropCity option:selected').text(), $('#CP_ddlDropLocation'), "", "false");
        ClearDropDateTime();
    });
    //Drop Down binding ends.

    
    $("#CP_droploction").click(function () {
        if ($("#CP_DDLLocation option:selected").val() == "select") {
            $("#CP_CarValidationError").show();
            $("#CP_CarValidationError")[0].innerHTML = 'Please provide with pickup location first.';
            $("#CP_droploction").prop('checked', true);
        } else {
            $("#CP_CarValidationError").show();
            $("#CP_CarValidationError")[0].innerHTML = '';
            $("#CP_CarValidationError").hide();
            if (this.checked) {
                $('#divDropContainer').hide();

                if ($(window).width() > 999) {
                    $('#divDropTime').css('margin-left', '60%');
                }
            }
            else {
                $('#divDropContainer').show();
                //copy only selected Country dropdown values.
               // $('#CP_ddlDropCountry').empty();
                //$('#CP_ddlDropCountry').append("<option value='" + $('#CP_DDLCountry option:selected').val() + "'>" + $('#CP_DDLCountry option:selected').text() + "</option>");
                $('#divDropTime').css('margin-left', '0px');
                //debugger;
                //PickUpLocationChange($('#CP_DDLCountry option:selected'), $('#CP_DDlCity option:selected'), $('#CP_DDLLocation option:selected'), $('#CP_ddlDropCountry'), $('#CP_ddlDropCity'), $('#CP_ddlDropLocation'));
                $('select').selectric('refresh');

                //-----------------end copy values---------------------//
            }
        }
    });

    $('#CP_ddlDropLocation')
            .on('change', function () {
                ClearDropDateTime();
            });
    //    $('#CP_ddlDropCity').on('change', function () {
    //        if ($('#CP_ddlDropCountry option:selected').val() != ''
    //                        && $('#CP_ddlDropCountry option:selected').val() != 'Select Country'
    //                        && $('#CP_ddlDropCountry option:selected').val() != undefined
    //                        && $('#CP_ddlDropCountry option:selected').val() != 'select'
    //                        && $('#CP_ddlDropCity option:selected').val() != ''
    //                        && $('#CP_ddlDropCity option:selected').val() != 'Select City'
    //                        && $('#CP_ddlDropCity option:selected').val() != undefined
    //                        && $('#CP_ddlDropCity option:selected').val() != 'select') {
    //            BindGlobalLocationList($('#CP_ddlDropCountry option:selected').val(), $('#CP_ddlDropCity option:selected').text(), $('#CP_ddlDropLocation'), '');
    //        }
    //    });
    //Drop Down binding ends.

    $("#CP_txtDiverAge").bind('keypress', function (e) {
        return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
    });

    $("#CP_txtDiverAgeMobile").bind('keypress', function (e) {
        return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
    });


    $("#CP_chkDriver").click(function () {
        if (this.checked) {
            $("#CP_txtDiverAge").hide();
        }
        else {
            $("#CP_txtDiverAge").show();
        }
    });

    $("#CP_droploction").click(function () {
        if (this.checked) {
            $(".divDropContainer").hide();
        }
        else {
            $(".divDropContainer").show();
        }
    });


    $('#divBlockAll').click(function () {
        $('#divBlockAll').hide();
        $('#divSearch').stop().animate({ 'marginLeft': '-370px' }, 200);

    });
    //End of assigning events
    $('#ddlCityMobile').attr('disabled', true);
    $('#ddlLocationMobile').attr('disabled', true);

    $('#txtPickUpDate').attr('disabled', true);
    $('#txtDropOffDate').attr('disabled', true);
});
//        $(window).load(function () {
//            if ($(window).width() >= 768) {
//                $("html, body").animate({ scrollTop: 350 }, 1000);
//                return false;
//            }
//        });
//        function RedirectPageTo(param) {
//            if (param != '' && param != undefined) {
//                if (param == 'flight')
//                    window.location.href = ('FlightSearch.aspx')
//                else if (param == 'car')
//                    window.location.href = ('CarSearch.aspx')
//                else if (param == 'hotel')
//                    window.location.href = ('HotelSearch.aspx')
//                else if (param == 'cashback')
//                    window.location.href = ('Cashback.aspx')
//                else if (param == 'voucher')
//                    window.location.href = ('VoucherPurchase.aspx')
//            }
//        }
        $(function () {
            $('.date').blur(function () {
                var timeStr1 = $(this).val();
                var timeStr = timeStr1.split(" ");
                var flag = false;
                var strValue = timeStr[0];
                var objRegExp = /^\d{1,2}(\/)\d{1,2}\1\d{4}$/
                if (strValue == "") {
                    return false;
                }

                // alert(strValue);
                if (!objRegExp.test(strValue)) {


                    if (strValue == "__/__/____") {
                        return false;
                    }
                    $("#CP_CarValidationError")[0].innerHTML = "Please Enter Date in DD/MM/YYYY Format";
                    $("#DivCarErrorMobile")[0].innerHTML = "Please Enter Date in DD/MM/YYYY Format";
                    $(this).val('');
                    return false;
                }

                else {
                    var strSeparator = "/"

                    var arrayDate = strValue.split(strSeparator);
                    //create a lookup for months not equal to Feb.
                    var arrayLookup = {
                        1: 31, 3: 31,
                        4: 30, 5: 31,
                        6: 30, 7: 31,
                        8: 31, 9: 30,
                        10: 31, 11: 30, 12: 31
                    }
                    var intDay = parseInt(arrayDate[0], 10);
                    var intMonth = parseInt(arrayDate[1], 10);


                    //check if month value and day value agree
                    if (arrayLookup[intMonth] != null) {
                        if (intDay <= arrayLookup[intMonth] && intDay != 0)
                        //return true; //found in lookup table, good date..
                            flag = true;
                        //isValidTime();
                    }

                    //check for February 
                    if (intMonth == 2) {
                        var intYear = parseInt(arrayDate[2]);
                        if (intDay > 0 && intDay < 29) {
                            //return true;
                            flag = true;
                            //isValidTime();

                        }
                        else if (intDay == 29) {
                            if ((intYear % 4 == 0) && (intYear % 100 != 0) ||
                         (intYear % 400 == 0)) {
                                // year div by 4 and ((not div by 100) or div by 400) ->ok
                                //return true; 
                                flag = true;
                                //isValidTime();
                            }
                        }
                    }
                }
                if (flag == false) {

                    $(this).val('');

                    $("#CP_CarValidationError")[0].innerHTML = "Please Enter Correct Date";
                    //$("#DivCarErrorMobile")[0].innerHTML = "Please Enter Correct Date";
                    return false; //any other values, bad date
                }
                else {
                    $("#CP_CarValidationError")[0].innerHTML = "";
                    // $("#DivCarErrorMobile")[0].innerHTML = "";
                }
            });
        });

        function ClearDateTime() {
            //if ($("#CP_CarTextBoxCheckin").val() != "") {
            $("#CP_CarTextBoxCheckin").val("");
            $("#CP_ddlFromTime").html('');
            $("#CP_ddlFromTime").append("<option>00</option>");
            $("#CP_ddlFromTime").selectric('refresh');
            //document.getElementById('CP_ddlMinFrom').selectedIndex = 0;
            $("#CP_ddlMinFrom").val("00");
            $("#CP_ddlMinFrom").selectric('refresh');
            //}
        }

        function ClearDropDateTime() {
            //if ($("#CP_CarTextBoxCheckout").val() != "") {
            $('#CP_CarTextBoxCheckout').val('');
            $("#CP_ddlDropFromTime").html('');
            $("#CP_ddlDropFromTime").append("<option>00</option>");
            $("#CP_ddlDropFromTime").selectric('refresh');
            //document.getElementById('CP_ddlDropMinFrom').selectedIndex = 0;
            $("#CP_ddlDropMinFrom").val("00");
            $("#CP_ddlDropMinFrom").selectric('refresh');
            //}
        }
