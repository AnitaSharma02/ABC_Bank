
function ClearFields() {
    $('#CP_lblTotalValue').text('');
    $('#CP_lblPointRequired').text('');
    var voucherRate = $('#CP_HDFVoucherRate').val();
    $("#CP_ddlQAR100").prop('selectedIndex', 0);
    $("#CP_ddlQAR500").prop('selectedIndex', 0);
    $("#CP_ddlQAR1000").prop('selectedIndex', 0);
    $("#CP_ddlQAR3000").prop('selectedIndex', 0);
    $("#CP_ddlQAR5000").prop('selectedIndex', 0);

    $("#divQAR100Add").css("display", "block");
    $("#divQAR500Add").css("display", "block");
    $("#divQAR1000Add").css("display", "block");
    $("#divQAR3000Add").css("display", "block");
    $("#divQAR5000Add").css("display", "block");


    $("#CP_ddlQAR100").removeAttr("disabled");
    $("#CP_ddlQAR500").removeAttr("disabled");
    $("#CP_ddlQAR1000").removeAttr("disabled");
    $("#CP_ddlQAR3000").removeAttr("disabled");
    $("#CP_ddlQAR5000").removeAttr("disabled");

    $('#CP_HDFQARAmount').val('0');
    $('#CP_HDFPoints').val('0');

    $("#CP_DivSubmitContent").removeClass("display_hide");
    $("#CP_DivGlobalRewardsConfirm").addClass("display_hide");
    $("#CP_divAvailability").addClass("display_hide");

    $.ajax({
        type: "post",
        url: "Voucher.aspx/BackToDenomination",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "",
        success: function (result) {
            if (result.d != '') {
                $("#CP_HDFVoucherRate").val(result.d);
            }
        }
    });
    return false;
}

function BackToDenomination() {
    return ClearFields();
};

function selectQty(amount, control, btncontrol) {
    var quantity = $(control).val();
    if (quantity != 0) {

        if (amount == '100') {
            $('#CP_HDFQAR100QTY').val(quantity);
        }
        else if (amount == '500') {
            $('#CP_HDFQAR500QTY').val(quantity);
        }
        else if (amount == '1000') {
            $('#CP_HDFQAR1000QTY').val(quantity);
        }
        else if (amount == '3000') {
            $('#CP_HDFQAR3000QTY').val(quantity);
        }
        else if (amount == '5000') {
            $('#CP_HDFQAR5000QTY').val(quantity);
        }
        $("#CP_lblVoucherError").text('');
        $("#CP_divVoucherError").addClass("display_hide");

        var hfamount = parseInt($('#CP_HDFQARAmount').val());
        var Totalamount;
        $.ajax({

            url: 'Voucher.aspx/CalculateAmount',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            //  data: "{'strbaseAmount':'" + hfamount + "' ,'strAmount':'" + amount + "','strQty':'" + quantity + "'}",
            data: JSON.stringify({ "strbaseAmount": hfamount, "strAmount": amount, "strQty": quantity }),
            dataType: 'json',
            success: function (msg) {
                if (msg.d) {
                    if (msg.d != "0") {
                        $("#divVoucherError").addClass("display_none");

                        $("#CP_lblVoucherError").html('');
                        $(control).prop("disabled", true);
                        $(btncontrol).hide();
                        Totalamount = (amount * quantity);
                        Totalamount = Totalamount + hfamount;
                        $('#CP_lblTotalValue').text(commaFormat(Totalamount));
                        $('#CP_lblPointRequired').text(commaFormat(msg.d));
                        $('#CP_HDFQARAmount').val(Totalamount);
                        $('#CP_HDFPoints').val(msg.d);
                    }

                }
            },
            error: function (errmsg) {

            }
        });
        return false;
    }
    else {
        $("#CP_lblVoucherError").text('Please select the quantity you require');
        $("#CP_divVoucherError").removeClass("display_hide");
    }
    return false;
}

function ConfirmVoucher() {
    $("#loading").show();

    var ddlQAR100Val = '0';
    var ddlQAR500Val = '0';
    var ddlQAR1000Val = '0';
    var ddlQAR3000Val = '0';
    var ddlQAR5000Val = '0';

    var hdfQAR100 = $('#CP_HDFQAR100QTY').val();
    var hdfQAR500 = $('#CP_HDFQAR500QTY').val();
    var hdfQAR1000 = $('#CP_HDFQAR1000QTY').val();
    var hdfQAR3000 = $('#CP_HDFQAR3000QTY').val();
    var hdfQAR5000 = $('#CP_HDFQAR5000QTY').val();

    if (hdfQAR100 != '0') {
        ddlQAR100Val = $("#CP_ddlQAR100").val();
    }
    if (hdfQAR500 != '0') {
        ddlQAR500Val = $("#CP_ddlQAR500").val();
    }
    if (hdfQAR1000 != '0') {
        ddlQAR1000Val = $("#CP_ddlQAR1000").val();
    }
    if (hdfQAR3000 != '0') {
        ddlQAR3000Val = $("#CP_ddlQAR3000").val();
    }
    if (hdfQAR5000 != '0') {
        ddlQAR5000Val = $("#CP_ddlQAR5000").val();
    }

    $('#CP_HDFQAR100QTY').val('0');
    $('#CP_HDFQAR500QTY').val('0');
    $('#CP_HDFQAR1000QTY').val('0');
    $('#CP_HDFQAR3000QTY').val('0');
    $('#CP_HDFQAR5000QTY').val('0');

    var totalQARValue = $("#CP_HDFQARAmount").val();
    var totalPoints = $("#CP_HDFPoints").val();

    var voucherRate = 0;
    if (totalPoints != '') {
        $.ajax(
            {
                url: 'Voucher.aspx/CheckAvailability',
                type: 'POST',
                // or get
                contentType: 'application/json; charset =utf-8',
                //  data: "{'strtotalPoints':'" + totalPoints + "'}",
                data: JSON.stringify({ "strtotalPoints": totalPoints }),
                dataType: 'json',
                success: function (msg) {
                    if (msg.d) {

                        if (msg.d.search('.aspx') != -1) {
                            window.location.href = msg.d;
                        }
                        else {
                            if (msg.d == '0') {
                                $("#CP_divVoucherError").addClass("display_hide");
                                $("#CP_lblVoucherError").text('');

                                $("#CP_DivGlobalRewardsConfirm").removeClass("display_hide");
                                $("#CP_DivSubmitContent").addClass("display_hide");
                                $("#CP_divAvailability").removeClass("display_hide");
                                $("#CP_divBtnConfirm").addClass("display_hide");

                            }
                            else {

                                $("#CP_divVoucherError").addClass("display_hide");
                                $("#CP_lblVoucherError").text('');

                                $("#CP_DivGlobalRewardsConfirm").removeClass("display_hide");
                                $("#CP_DivSubmitContent").addClass("display_hide");;
                                $("#CP_divBtnConfirm").removeClass("display_hide");;
                            }

                            $('#CP_lblQAR100Quantity').text(ddlQAR100Val);
                            $('#CP_lblQAR500Quantity').text(ddlQAR500Val);
                            $('#CP_lblQAR1000Quantity').text(ddlQAR1000Val);
                            $('#CP_lblQAR3000Quantity').text(ddlQAR3000Val);
                            $('#CP_lblQAR5000Quantity').text(ddlQAR5000Val);

                            $('#CP_lblQar100VoucherVal').text(commaFormat(ddlQAR100Val * 100));
                            $('#CP_lblQar500VoucherVal').text(commaFormat(ddlQAR500Val * 500));
                            $('#CP_lblQar1000VoucherVal').text(commaFormat(ddlQAR1000Val * 1000));
                            $('#CP_lblQar3000VoucherVal').text(commaFormat(ddlQAR3000Val * 3000));
                            $('#CP_lblQar5000VoucherVal').text(commaFormat(ddlQAR5000Val * 5000));

                            voucherRate = $('#CP_HDFVoucherRate').val();

                            $('#CP_lblQAR100PointsRequired').text(commaFormat((ddlQAR100Val * 100) / voucherRate));
                            $('#CP_lblQAR500PointsRequired').text(commaFormat((ddlQAR500Val * 500) / voucherRate));
                            $('#CP_lblQAR1000PointsRequired').text(commaFormat((ddlQAR1000Val * 1000) / voucherRate));
                            $('#CP_lblQAR3000PointsRequired').text(commaFormat((ddlQAR3000Val * 3000) / voucherRate));
                            $('#CP_lblQAR5000PointsRequired').text(commaFormat((ddlQAR5000Val * 5000) / voucherRate));


                            $('#CP_trQAR100').addClass("display_hide");
                            $('#CP_trQAR500').addClass("display_hide");
                            $('#CP_trQAR1000').addClass("display_hide");
                            $('#CP_trQAR3000').addClass("display_hide");
                            $('#CP_trQAR5000').addClass("display_hide");

                            if (ddlQAR100Val != '0' && hdfQAR100 != '0') {
                                $('#CP_trQAR100').removeClass("display_hide");
                            }
                            if (ddlQAR500Val != '0' && hdfQAR500 != '0') {
                                $('#CP_trQAR500').removeClass("display_hide");
                            }
                            if (ddlQAR1000Val != '0' && hdfQAR1000 != '0') {
                                $('#CP_trQAR1000').removeClass("display_hide");
                            }
                            if (ddlQAR3000Val != '0' && hdfQAR3000 != '0') {
                                $('#CP_trQAR3000').removeClass("display_hide");
                            }
                            if (ddlQAR5000Val != '0' && hdfQAR5000 != '0') {
                                $('#CP_trQAR5000').removeClass("display_hide");
                            }

                            var totalVoucherValue = (ddlQAR100Val * 100) + (ddlQAR500Val * 500) + (ddlQAR1000Val * 1000) + (ddlQAR3000Val * 3000) + (ddlQAR5000Val * 5000);
                            var totalVoucherPoints = totalVoucherValue / voucherRate;

                            $('#CP_lblTotalVoucherValue').text(commaFormat(totalVoucherValue));
                            $('#CP_lblRequirPointToRedeem').text(commaFormat(totalVoucherPoints));
                            $('#CP_lblComercialbankreward').text('NIC Asia Points');

                            $("#loading").hide();

                            $('#CP_divVoucherSummary').addClass("display_hide");
                            $('#CP_Voucher1').addClass("display_hide");
                        }

                    }
                    else { }
                }
            });
        return false;
    }
    else {
        $("#CP_lblVoucherError").text('Please select the quantity you require');
        $("#CP_divVoucherError").removeClass("display_hide");
    }
    $("#loading").hide();
    return false;

};
