function SelectHadyatiAmount(amounts) {
    $.ajax({
        url: 'PrepaidCard.aspx/CalCulatePrepaidCardPoints',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        //data: "{'amount':'" + amounts + "'}",
        data: "{'amount':'" + amounts + "'" + "," + "'QPOST':'" + false + "'}",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;
                if (newData != "") {
                    if (data.d.split('|')[0] == "Success") {
                        $("#lblselectedpoints").show();
                        $("#lblpoints").show();

                        $("#lblQPostpoints").hide();
                        $("#lblselectedQPostpoints").hide();
                        $("#trQPostpoints").hide();
                        $("#lblTotalpoints").hide();
                        $("#lblselectedTotalpoints").hide();


                        $("#lblselectedQPostpoints")[0].innerHTML = data.d.split('|')[2];

                        $("#lblselectedTotalpoints")[0].innerHTML = data.d.split('|')[1];

                        $("#lblselectedpoints")[0].innerHTML = data.d.split('|')[3];
                        $("#post").prop("checked", false).attr('disabled', false);
                        $("#bpickup").prop("checked", true).attr('disabled', false);
                        $("#btn").hide();
                        $("#label").hide();
                        $("#divbtncontinue").show();
                        $("#message").hide();
                        $("#delivarylblmessage")[0].innerHTML = $.i18n('text-from-delivery-message');
                        //$("#delivarylblmessage")[0].innerHTML = "Your Hadiyati card will be sent to QIB Service delivery center in C-ring road in 2 working days from the date of redemption.";
                        $("#delivarylblmessage").show();
                        $("#delivarytd").show(); 
                        $("#divlable").hide();
                    }
                    else {
                        $("#lblselectedpoints").hide();
                        $("#lblpoints").hide();
                        $("#post").prop("checked", false).attr('disabled', true);
                        $("#bpickup").prop("checked", false).attr('disabled', true);
                        $("#btn").hide();
                        $("#label").hide();
                        $("#message").hide();
                        $("#delivarylblmessage").hide();
                        $("#divbtncontinue").hide();
                        $("#delivarytd").hide();
                        $("#divlable").hide();
                    }
                }
                else {
                    $("#divmain").hide();
                    $("#delivarylbldiv").hide();
                    $("#divbtncontinue").hide();
                    $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong. ";
                    $("#btn").hide();
                    $("#lable").hide();
                }
            }
            catch (e) {
                return false;
            }
            return false;
        },
        error: function (errmsg) {

        }
    });
    return false;
}

function Confirmsignout() {
    $('.hover_app_info').show();
    //var confirm_value = document.createElement("INPUT");
    //confirm_value.type = "hidden";
    //confirm_value.name = "confirm_value";
    //if (confirm("Do you want to Signout?")) {
    //    confirm_value.value = "Yes";
    //    window.location.href = "Logout.aspx";

    //} else {
    //    confirm_value.value = "No";
    //}
    //document.forms[0].appendChild(confirm_value);
}
function selectDeliveryOption(data) {
    if (data == "post") {
        var redemptionamount = $("#ddlAmount option:selected").val();
        //redemptionamount = parseInt(redemptionamount)
        //var amounts = 20 + redemptionamount
        //amounts = amounts.toString();
        $.ajax({
            url: 'PrepaidCard.aspx/CalCulatePrepaidCardPoints',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            //data: "{'amount':'" + redemptionamount + "'}",
            data: "{'amount':'" + redemptionamount + "'" + "," + "'QPOST':'" + true + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (newData != "") {
                        if (data.d.split('|')[0] == "Success") {
                            $("#lblselectedpoints").show();
                            $("#lblpoints").show();
                            var oldpoints = $("#lblselectedpoints")[0].innerHTML;
                            oldpoints = parseInt(oldpoints);
                            var newpoints = data.d.split('|')[1];
                            newpoints = parseInt(newpoints)
                            //var delivarychargepoints = newpoints - oldpoints;
                            //$("#divsubmit").hide();

                            $("#trQPostpoints").show();
                            $("#lblQPostpoints").show();
                            $("#lblselectedQPostpoints").show();
                            $("#lblTotalpoints").show();
                            $("#lblselectedTotalpoints").show();

                            $("#lblselectedQPostpoints")[0].innerHTML = data.d.split('|')[2];

                            $("#lblselectedTotalpoints")[0].innerHTML = data.d.split('|')[1];


                            $("#lblselectedpoints")[0].innerHTML = data.d.split('|')[3];
                            var points = data.d.split('|')[1].toString();
                            $("#message").show();
                            $("#delivarylblmessage").show();
                            $("#delivarylblmessage")[0].innerHTML = $.i18n('text-quilent-against-qatar-post-delivery');
                            //$("#delivarylblmessage")[0].innerHTML = "The equivalent points against Qatar Post Delivery charges of 25 QR will be added in your total redeem points. Qatar Post delivery time is minimum 5 working days.";
                            $("#divbtncontinue").show();
                        }
                        else {
                            $("#divmain").hide();
                            $("#delivarylbldiv").show();
                            $("#divbtncontinue").hide();
                            $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong. ";
                            $("#btn").hide();
                            $("#lable").hide();
                        }
                    }
                    else {
                        $("#divmain").hide();
                        $("#delivarylbldiv").show();
                        $("#divbtncontinue").hide();
                        $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong. ";
                        $("#btn").hide();
                        $("#lable").hide();
                    }
                }
                catch (e) {
                    return false;
                }
                return false;
            },
            error: function (errmsg) {

            }
        });
        return false;
    }
    else if (data == "bpickup") {
        var redemptionamount = $("#ddlAmount option:selected").val();
        $.ajax({
            url: 'PrepaidCard.aspx/CalCulatePrepaidCardPoints',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            //data: "{'amount':'" + redemptionamount + "'}",
            data: "{'amount':'" + redemptionamount + "'" + "," + "'QPOST':'" + false + "'}",
            
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (newData != "") {
                        if (data.d.split('|')[0] == "Success") {

                            $("#trQPostpoints").hide();
                            $("#lblQPostpoints").hide();
                            $("#lblselectedQPostpoints").hide();
                            $("#lblTotalpoints").hide();
                            $("#lblselectedTotalpoints").hide();

                            $("#lblselectedQPostpoints")[0].innerHTML = data.d.split('|')[2];

                            $("#lblselectedTotalpoints")[0].innerHTML = data.d.split('|')[1];

                            $("#lblselectedpoints")[0].innerHTML = data.d.split('|')[3];
                            $("#message").hide();
                            $("#delivarylblmessage")[0].innerHTML = $.i18n('text-your-hadiyati-card-will-be-sent');
                            //$("#delivarylblmessage")[0].innerHTML = "Your Hadiyati card will be sent to QIB Service delivery center in C-ring road in 2 working days from the date of redemption.";
                            $("#delivarylblmessage").show();
                            $("#divbtncontinue").show();
                        }
                        else {
                            $("#divmain").hide();
                            $("#delivarylbldiv").show();
                            $("#divbtncontinue").hide();
                            $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong. ";
                            $("#btn").hide();
                            $("#lable").hide();
                        }

                    }
                    else {
                        $("#divmain").hide();
                        $("#delivarylbldiv").show();
                        $("#divbtncontinue").hide();
                        $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong. ";
                        $("#btn").hide();
                        $("#lable").hide();
                    }
                }
                catch (e) {
                    return false;
                }
                return false;
            },
            error: function (errmsg) {

            }
        });
        return false;
    }
}


function onclickcontinue() {
    
    $.ajax({
        url: 'PrepaidCard.aspx/CheckAvailability',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: "",
        dataType: 'json',
        success: function (data) {

            try {
                var newData = data.d;
                
                if (newData != "") {
                    newData = newData.split('|')[0];
                    if (data.d.split('|')[0] == "Success") {
                        $("#divPrepaidCard").hide();
                        $("#divConfirmation").show();
                        var AvailablePoints = data.d.split('|')[1];
                        var RequiredPoints = data.d.split('|')[2];
                        if (parseInt(RequiredPoints) > parseInt(AvailablePoints)) {
                            $("#lblmessage")[0].innerHTML = $.i18n('text-you-have-insufficient-points');
                            //$("#lblmessage")[0].innerHTML = "You have insufficient points";
                            $("#submitbtn").hide();
                            $("#divPrepaidCard").show();
                            $("#divConfimationMessage").hide();
                            $("#divlable").show();
                      
                        }
                        else if (parseInt(RequiredPoints) <= parseInt(AvailablePoints)) {
                            $("#lblmessage")[0].innerHTML = "";
                            $("#divConfimationMessage").show();
                            $("#lblConfirmationMessage")[0].innerHTML = $.i18n('text-you-have-selected') + data.d.split('|')[3] + $.i18n('text-qe-hadiyadi-prepaid') + '<br>';
                            if (data.d.split('|')[4] > 0) {
                                $("#lblConfirmationMessage")[0].innerHTML += $.i18n('text-qatar-post-delivery') + '</br>';
                                //$("#lblConfirmationMessage")[0].innerHTML += "Qatar POST delivery charge of 25 QR will be added in your total redeemed points.</br>";
                            }
                            $("#lblConfirmationMessage")[0].innerHTML += $.i18n('text-kindly-confirm-redeem') + data.d.split('|')[2] + $.i18n('text-absher-rewards-redemption');
                            //$("#lblConfirmationMessage")[0].innerHTML += "Kindly confirm to redeem " + data.d.split('|')[2] + " Absher Rewards for this redemption.";
                            $("#btn").show();
                            $("#ChkbBxtnc").attr("style", "display:block");
                            $("#submitbtn").show();
                        }
                       else {
                            $("#lblmessage")[0].innerHTML = "";
                            $("#btn").show();
                            $("#ChkbBxtnc").attr("style", "display:block");
                            $("#submitbtn").show();
                        }
 

                      
                    }
                    else {
                        $("#divmain").hide();
                        $("#delivarylbldiv").show();
                        $("#divbtncontinue").hide();
                        $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong ";
                        $("#btn").hide();
                        $("#lable").hide();
                    }

                }
                else {
                    $("#divmain").hide();
                    $("#delivarylbldiv").show();
                    $("#divbtncontinue").hide();
                    $("#delivarylblmessage")[0].innerHTML = "Something Went Wrong ";
                    $("#btn").hide();
                    $("#lable").hide();
                }
            }
            catch (e) {
                return false;
            }
            return false;
        },
        error: function (errmsg) {

        }
    });
    return false;
}


function onclicksubmit() {
    $.ajax({

        url: 'PrepaidCard.aspx/PrepaidCardRedeempoints',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: "",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;
                
                if (newData != "") {
                    $("#divPrepaidCard").hide();
                    $("#divConfirmation").hide();
                    $("#btn").hide();
                    $("#divFinal").show();
                    $("#chkAgreed").prop("checked", false);
                    $("#ChkbBxtnc").attr("style", "display:none");
                    if (newData == "SUCCESS") {
                        $("#lblConfirmationMessage")[0].innerHTML = $.i18n('text-your-redemption-has-been-sent');
                        //$("#lblConfirmationMessage")[0].innerHTML = "Your redemption request has been well received. We will soon send your Hadiyati card based on the selected delivery option. ";
                    }
                    else if (newData == "INSUFFICIENT_POINTS") {
                        $("#lblConfirmationMessage")[0].innerHTML = $.i18n('text-you-have-insufficient-points');
                        //$("#lblConfirmationMessage")[0].innerHTML = "You have insufficient points";
                    }
                    else if (newData == "PointsMismatch") {
                        $("#lblConfirmationMessage")[0].innerHTML = "Failed to process your request Please try again.";
                    }
                    else {
                        $("#lblConfirmationMessage")[0].innerHTML = "Failed to process your request. Please try again.";
                    }
                }
                else {
                    alert("error");
                }
            }
            catch (e) {
                return false;
            }
            return false;
        },
        error: function (errmsg) {

        }
    });
    return false;
}


function onbackclick() {
    $("#divPrepaidCard").show();
    $("#divConfirmation").hide();
    $("#divFinal").hide();
    $("#lblConfirmationMessage")[0].innerHTML = "";
    $("#lblmessage")[0].innerHTML = "";
    $("#btn").hide();
    document.getElementById("submitbtn").disabled = true;
    $("#chkAgreed").prop("checked", false);
    $("#ChkbBxtnc").attr("style", "display:none");
}



function InitialBinding() {
    $('#divConfirm').hide();
    $('#divCashback').show();
    //$('#txtExpiryDate').val('');
    //$('#txtCashBackPoint').val('');
    //BindCardList();
    GetAvailablePoints();
    $('#lblTotalPoint').val(glblAvailableAmt);
}
