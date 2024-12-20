$(window).resize(function () {
    if ($(window).width() >= 768) {
        $('#divMenu').removeClass('MoveLeft');
        $('#divMenu').addClass('flight-left-m14');
        $('#divBlockAll').css('display', 'none');
        $('#divBlockAll').hide();
        $('#divMenu').show();
    }
    else {
        $('#divMenu').removeClass('flight-left-m14');
        $('#divMenu').addClass('MoveLeft');
        $('#divMenu').hide();
    }
});
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
$(document).ready(function () {
    var PageName = location.href.split("/").slice(-1)[0];
    PageName = PageName.split("?")[0];
    if (PageName == 'StatementSummary.aspx' || PageName == 'TransactionSummary.aspx' || PageName == 'PointsExpiry.aspx' || PageName == 'ManageBooking.aspx' || PageName == 'ViewMemberProfile.aspx' || PageName == 'PurchaseEarser.aspx' || PageName == 'PointsPurchase.aspx' || PageName == 'OrderHistory.aspx' || PageName == 'OrderHistory.aspx' || PageName == "OrderDetails.aspx") {
        var html = '<div class="col-6 col-lg-2 pr-0 pr-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divStatementSummary" href="StatementSummary.aspx">Statement <br class="d-none d-lg-block" />Summary</a></div>';
        html += '<div class="col-6 col-lg-2 pl-0 pl-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divTransactionSummary" href="TransactionSummary.aspx">Transaction <br class="d-none d-lg-block" />Summary</a></div>';
        html += '<div class="col-6 col-lg-2 pr-0 pr-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divPointexp" href="PointsExpiry.aspx">Points <br class="d-none d-lg-block" />Expiry</a></div>';
        html += '<div class="col-6 col-lg-2 pl-0 pl-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divManagebooking" href="ManageBooking.aspx">Manage <br class="d-none d-lg-block" />Booking</a></div>';
        html += '<div class="col-6 col-lg-2 pr-0 pr-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divOrderhistory" href="OrderHistory.aspx">Order <br class="d-none d-lg-block" />History</a></div>';
        // html += '<div class="col-6 col-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divPointsPurchase" href="PointsPurchase.aspx"></a></div>';
        // html += '<div class="col-6 col-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divPurchaseEraser" href="PurchaseEarser.aspx"></a></div>';
        html += '<div class="col-6 col-lg-2 pl-0 pl-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divMemberProfile" href="ViewMemberProfile.aspx">My <br class="d-none d-lg-block" />Profile</a></div>';
        //html += '<div class="col-6 col-lg-2"><a class="nav-link d-flex align-items-center justify-content-around text-center heading-semibold px-3 px-lg-4 border" id="divLogout" href="javascript:Confirmsignout();">Logout</a></div>';
        $("#AccMenu").empty();
        $("#AccMenu").append(html);


        if (PageName == 'StatementSummary.aspx') {
            $('#divStatementSummary').addClass("active");
        }
        else if (PageName == 'TransactionSummary.aspx') {
            $('#divTransactionSummary').addClass("active");
        }
        else if (PageName == 'PointsExpiry.aspx') {
            $('#divPointexp').addClass("active");
        }
        else if (PageName == 'ManageBooking.aspx') {
            $('#divManagebooking').addClass("active");

        }
        else if (PageName == 'PurchaseEarser.aspx') {
            $('#divPurchaseEraser').addClass("active");
        }
        else if (PageName == 'ViewMemberProfile.aspx') {
            $('#divMemberProfile').addClass("active");
        }
        else if (PageName == 'PointsPurchase.aspx') {
            $('#divPointsPurchase').addClass("active");
        }
        else if (PageName == 'OrderHistory.aspx') {
            $('#divOrderhistory').addClass("active");
        }

    }
    $('#lnkMenu-m').click(function () {

        $('#divMenu').show();
        $('#divMenu').removeClass('flight-left-m14');
        $('#divMenu').addClass('MoveLeft');
        $('#divMenu').show();
        $('#divBlockAll').css('display', 'block');
        $('#divBlockAll').show();
        $('#divMenu').stop().animate({ 'marginLeft': '0px' }, 1000, function () {
        });
    });

    $('#divBlockAll').click(function () {
        $('#divBlockAll').hide();
        $('#divMenu').stop().animate({ 'marginLeft': '-370px' }, 200);

    });


    $('.adCntnr div.acco2:eq(0)').find('div.expand:eq(0)').addClass('openAd').end()
        .find('div.collapse:gt(0)').hide().end()
        .find('div.expand').click(function () {
            $(this).toggleClass('openAd').siblings().removeClass('openAd').end()
                .next('div.collapse').slideToggle().siblings('div.collapse:visible').slideUp();
            return false;
        });

    $('form').bind("keypress", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    });

})

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-+\s]+")|([\w-+]+(?:\.[\w-+]+)*)|("[\w-+\s]+")([\w-+]+(?:\.[\w-+]+)*))(@((?:[\w-+]+\.)*\w[\w-+]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
};

function LoginOnEnter(e) {
    var ENTER_KEY = 13;
    var code = "";
    var code = (e.keyCode ? e.keyCode : e.which);

    if (code == ENTER_KEY) {
        LoginValidation();
        return false;
    }
    return true;
};
function loaderclass() {


    //   $(".overlayforaddNote").show().SetOverlayHeightWidth();

    $('#dvLoading').show();


    // setTimeout(function () { $(".overlayforaddNote").hide() }, 4000);
    //$.fn.SetOverlayHeightWidth = function () {

    //    $(this).height($(document).height());

    //    $(this).width($(document).width());

    //};




}

function redirectLocation(url) {
    setTimeout(function () {
        loaderclass();
    }, 4000);

    window.location.href = url;
    return false;
};

function LoginValidation() {
    var msg = "";
    $("#CP_ErrorMsgContainer").hide();
    $("#LoginValidation")[0].innerHTML = "";
    $("#CP_lblLoginError").html('');
    if ($("#slcCountry option:selected").val() == '') {
        msg += "<span>Please enter Country.</span>" + "<br/>";
    }
    if ($("#CP_txtMemberName").val().length == 0) {
        msg += "<span>Please Enter Email ID</span>" + "<br/>";
    }
    if ($("#CP_txtPassword").val().length == 0) {
        msg += "<span>Please Enter Password</span>" + "<br/>";
    }
    if (msg.length > 0) {
        //$("#LoginValidation").css('color', 'red');
        $("#CP_ErrorMsgContainer").show();
        $("#LoginValidation")[0].innerHTML = msg;
    }
    else {
        var Country = $("#slcCountry option:selected").val();
        var MemberId = $("#CP_txtMemberName").val();
        var password = $("#CP_txtPassword").val();
        var Pwd = $.md5(MemberId + password);
        var RememberMe = new Boolean();
        RememberMe = $("#CP_chkRememberMe")[0].checked;
        $.ajax({
            url: 'Login.aspx/SigninUser',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberid':'" + MemberId.toString() + "'" + "," + "'pstrPassword':'" + Pwd.toString() + "'" + "," + "'pboolRememberMe':'" + RememberMe + "'," + "'pstrCountry':'" + Country + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var str = data.d;
                    var Text = str.split("+");
                    data.d = Text[0];
                    var Statustxt = Text[1];
                    var Status = Statustxt.split(":");
                    var Statustext = Status[1];
                    var newData = data.d;
                    if (Statustext == "Blocked") { AuthFail = "<span>Please check the login details you have entered and try again.</span>"; }
                    else {
                        AuthFail = "Account Block";
                    }
                    if ((newData != "") || newData == null) {
                        if (data.d == "AccountLock") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Your Account is Locked.Please </span><a class="ErrorMessageLink" onclick="showOTPDiv();">click here</a><span> to unlock the same.</span>');
                        }
                        else if (data.d == "AuthenticationFailed") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Please check the login details you have entered and try again.</span>');
                        }
                        else if (data.d == "Suspended") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Your Account is Suspended</span>');
                        }
                        else if (data.d == "Cancelled") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Your Account is Cancelled</span>');
                        }
                        else if (data.d == "InActive") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Your Account is InActive. Please use activate tab to activate your account.</span>');
                        }
                        else if (data.d == "IncorrectFormat") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Password must contain one non-alpha character,one upper case character,one lower case character and minimum 8 characters in length.</span>');
                        }
                        else {
                            // showLoginOTPDiv();
                            $("#CP_txtMemberID").val(MemberId);
                            $("#CP_hfMemberId").val(MemberId);
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html(data.d);
                            $("#CP_lblLoginError").html('');
                        }
                    }
                    else {
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        $("#CP_lblLoginError").html('<span>Please check the login details you have entered and try again.</span>');
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

    }
    return false;
};

function ForgotPassword() {

    var msg = "";
    $("#CP_ErrorMsgContainer").hide();
    $("#ForgotPasswordValidation")[0].innerHTML = "";
    if ($("#CP_txtMemberId").val().length == 0) {
        msg += + "<br/>";
    } else {
        if (!AcceptAlphanumericOnly($('#CP_txtMemberId').val())) {
            msg += "<span>Please enter valid Email Id. </span><br/>";
        }
    }
    if (msg.length > 0) {
        $("#ForgotPasswordValidation")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
        return false;
    }
    else {

        var MemberId = $("#CP_txtMemberId").val();
        jQuery("#overlay").css('display', 'block');
        jQuery("#popup").css('display', 'block');
        jQuery("#popup").fadeIn(500);

        $.ajax({
            url: 'ForgotPassword.aspx/MemberShipReference',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberId':'" + MemberId.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    $("#ForgotPasswordValidation")[0].innerHTML = '';

                    var newData = data.d;
                    if (data.d == "Success") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '<span>Your new Password reset link has been sent to your registered email id.</span>';
                        $("#ForgetPasswordDiv").hide();
                        $("#ForgetButtonDiv").hide();
                    }
                    else if (data.d == "Blocked") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '<span>Your Account is Blocked. Please contact BOK. </span>'

                    }
                    else if (data.d == "InActive") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '<span>Your Account is InActive. Please Activate Your Account.</span>';

                    }
                    else if (data.d == "Suspended") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '</span>Your Account is Suspended. Please contact BOK.</span>';
                    }
                    else if (data.d == "Cancelled") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '<span>Your Account is Cancelled. Please contact BOK.</span>';

                    }
                    else if (data.d == "NoRecord") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '</span>No Record Found. Please contact BOK.</span>';

                    }
                    else if (data.d == "Failure") {
                        $("#PasswordValidation").html();
                        $("#CP_ErrorMsgContainer").show();
                        $("#ForgotPasswordValidation")[0].innerHTML = '</span>Please try again.</span>';
                    }
                }
                catch (e) {
                    jQuery("#overlay").css('display', 'none');
                    jQuery("#popup").css('display', 'none');
                    return false;
                }
                jQuery("#overlay").css('display', 'none');
                jQuery("#popup").css('display', 'none');
                return false;
            },
            error: function (errmsg) {

            }
        });
    }
    jQuery("#overlay").css('display', 'none');
    jQuery("#popup").css('display', 'none');
    return false;
};


function ChangePassword() {

    var msg = "";
    $('#CP_ErrorMsgContainer').hide();
    $("#ChangePasswordValidation")[0].innerHTML = "";
    if ($('#txtOldPassword').val() == "") {
        msg += "<span>Please provide a valid Current password.</span>" + "<br/>";
    }
    if ($('#txtOldPassword ').val() != "") {
        if (($('#txtOldPassword').val().length < 8) && ($('#txtOldPassword').val().length > 1)) {
            msg += "<span>Current password field has to be minimum eight characters.</span>" + "<br/>";
        }
        if (($('#txtOldPassword').val() == $('#txtPassword').val()) && ($('#txtOldPassword').val().length > 1)) {
            msg += "<span>New Password should not be same as Current password.</span>" + "<br/>";
        }
    }
    if ($('#txtNewPassword').val() != $('#txtPassword').val()) {
        msg += "<span>New Password and confirm password should be same.</span>" + "<br/>";
    }
    if ($('#txtPassword').val() == "") {
        msg += "<span>Please enter your new password.</span>" + "<br/>";
    }
    if (($('#txtPassword').val().length < 8) && ($('#txtPassword').val().length > 1)) {
        msg += "<span>New password field has to be minimum eight characters.</span>" + "<br/>";
    }

    if ($('#txtNewPassword').val() == "") {
        msg += "<span>Please confirm your new password.</span>" + "<br/>";
    }

    if (($('#txtNewPassword').val().length < 8) && ($('#txtNewPassword').val().length > 1)) {
        msg += "<span>Confirm password field has to be minimum eight characters.</span>" + "<br/>";
    }
    if (!CheckPasswordPolicy($('#txtPassword').val())) {
        msg += "<span>Password must contain one numeric digit,one upper case character,one lower case character,one special character (!@#$%*()?)and minimum 8 characters in length.</span>" + "<br/>";
    }
    if (msg.length > 0) {
        $("#ChangePasswordValidation")[0].innerHTML = msg;
        return false;
    }
    else {
        var salt = $("#CP_hfRelationRef").val();
        var OldPassword = $("#txtOldPassword").val();
        var NewPassword = $("#txtPassword").val();
        var OldPwd = OldPassword;
        var NewPwd = NewPassword;
        $.ajax({
            url: 'ViewMemberProfile.aspx/ChangePassword',
            type: 'POST',
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrOldPassword':'" + OldPwd.toString() + "'" + "," + "'pstrNewPassword':'" + NewPwd.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {

                    var newData = data.d;
                    if (data.d == "Success") {
                        $("#txtOldPassword").val('');
                        $("#txtPassword").val('');
                        $("#txtNewPassword").val('');
                        $("#ChangePasswordValidation").html('<span>Your password has been changed.</span>');
                    }
                    else if (data.d == "Failure") {
                        $("#ChangePasswordValidation").html('<span>Password Changed Failure.</span>');
                    }
                    else if (data.d == "IncorrectFormat") {
                        $("#ChangePasswordValidation").html('<span>Password must contain one non-alpha character,one upper case character,one lower case character and minimum 8 characters in length.</span>');
                    } else {
                        $("#ChangePasswordValidation").html('<span>Please enter correct password.</span>');
                    }

                }
                catch (e) {
                    alert(e);

                    return false;
                }
                return false;

            },
            error: function (errmsg) {

            }
        });
    }
    jQuery("#overlay").css('display', 'none');
    jQuery("#popup").css('display', 'none');
    return false;
};

function ValidateOnEnter(e) {

    var ENTER_KEY = 13;
    var code = "";
    var code = (e.keyCode ? e.keyCode : e.which);

    if (code == ENTER_KEY) {
        ChangePassword();
        return false;
    }
    return true;
};

function ChangePasswordValidation(e) {
    if (e != null && (e != 'undefined' && e != '')) {
        var ENTER_KEY = 13;
        var code = "";
        var code = (e.keyCode ? e.keyCode : e.which);

        if (code == ENTER_KEY) {
            __doPostBack('btnSave', 'onclick');
            return false;
        }

    }
    else {
        __doPostBack('btnSave', 'onclick');
        return false;
    }
    return true;
}
function ChangePasswordValidationMob(e) {
    if (e != null && (e != 'undefined' && e != '')) {
        var ENTER_KEY = 13;
        var code = "";
        var code = (e.keyCode ? e.keyCode : e.which);

        if (code == ENTER_KEY) {
            __doPostBack('btnSaveMob', 'onclick');
            return false;
        }

    }
    else {
        __doPostBack('btnSaveMob', 'onclick');
        return false;
    }
    return true;
}

function ValidateLoginOTP() {
    var msg = "";
    $("#CP_lblLoginError").html('');
    $("#LoginValidation")[0].innerHTML = "";
    $("#CP_ErrorMsgContainer").hide();
    if ($("#CP_txtOTP").val() == "") {
        msg = "Enter OTP";
    }
    if (msg.length > 0) {
        $("#CP_ErrorMsgContainer").show();
        //$("#LoginValidation").css('color', 'red');
        $("#LoginValidation")[0].innerHTML = msg;
    }
    else {
        var MemberId = $("#CP_txtMemberID").val();
        var OTP = $("#CP_txtOTP").val();

        jQuery("#overlay").css('display', 'block');
        jQuery("#popup").css('display', 'block');
        jQuery("#popup").fadeIn(500);
        $.ajax({
            url: 'Login.aspx/ValidateOTP',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberid':'" + MemberId.toString() + "'" + "," + "'pstrOTP':'" + OTP.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if ((newData != "") || newData == null) {
                        if (data.d == "InvalidOTP") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Invalid OTP.</span>');
                        }
                        else if (data.d == "Invalid Credentials") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Invalid Credentials.</span>');
                        }
                        else if (data.d == "InvalidToken") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Please check the Authentication Token.</span>');
                        }
                        else if (data.d == "InvalidExtLogin") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#CP_lblLoginError").html('<span>Invalid External Login Credentials.</span>');
                        } else if (data.d == "AccountLock") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            $("#divOTP").hide();

                            //  $("#CP_lblLoginError").html('Your Account is Locked.Please <a class="ErrorMessageLink" onclick="showOTPDivNew();">click here</a> to unlock the same.');
                            $("#CP_lblLoginError").html('<span>Your Account is Locked.Please kindly contact Infinity Rewards customer care to unlock the same.</span>');


                        }
                        else {
                            $("#LoginValidation")[0].innerHTML = "";
                            window.location.href = data.d;
                        }
                    }
                    else {
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        $("#CP_lblLoginError").html('<span>Please check the details you have entered and try again.</span>');
                    }
                }
                catch (e) {
                    alert(e);
                    jQuery("#overlay").css('display', 'none');
                    jQuery("#popup").css('display', 'none');
                    return false;
                }
                jQuery("#overlay").css('display', 'none');
                jQuery("#popup").css('display', 'none');
                return false;
            },
            error: function (errmsg) {

            }
        });
        jQuery("#overlay").css('display', 'none');
        jQuery("#popup").css('display', 'none');
        return false;

    }
};

function LoginMsgCodeBehind(data) {


    var newData = data;

    if (data != null || (data != "") || data != 'undefined') {

        if (data == "InvalidOTP") {
            // showLoginOTPDiv();
            var MemberId = $("#CP_hfMemberId").val()
            $("#CP_txtMemberID").val(MemberId);
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Invalid OTP.</span>');
        }
        else if (data == "AccountLock") {
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#divOTP").hide();
            $("#CP_lblLoginError").html('<span>Your Account is Locked.Please kindly contact Infinity Rewards customer care to unlock the same.</span>');
        }
        else if (data.d == "SomethingWentWrong") {
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Something Went Wrong please try again.</span>');
        }
        else if (data.d == "InvalidToken") {
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Please check the Authentication Token.</span>');
        }
        else if (data.d == "InvalidExtLogin") {
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Invalid External Login Credentials.</span>');
        }
        else if (data != "") {
            $("#LoginValidation")[0].innerHTML = "";
            window.location.href = data;
        }
        else {
            //   showLoginOTPDiv();
            var MemberId = $("#CP_hfMemberId").val()
            $("#CP_txtMemberID").val(MemberId);

            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Please check the details you have entered and try again.</span>');
        }
    }
    else {
        //  showLoginOTPDiv();
        var MemberId = $("#CP_hfMemberId").val()
        $("#CP_txtMemberID").val(MemberId);
        $("#LoginValidation")[0].innerHTML = "";
        $("#CP_ErrorMsgContainer").show();
        $("#CP_lblLoginError").html('<span>Please check the details you have entered and try again.</span>');
    }
}

function LoginOTPOnEnter(e) {

    var ENTER_KEY = 13;
    var code = "";
    var code = (e.keyCode ? e.keyCode : e.which);

    if (code == ENTER_KEY) {
        UnLockMemberByOTP();
        return false;
    }
    return true;
};

function UnLockMemberByOTP() {

    var msg = "";
    $("#LoginValidation")[0].innerHTML = "";
    $("#CP_ErrorMsgContainer").hide();
    if ($('#CP_txtRIM').val() == '') {
        msg += $.i18n("text-please-enter-rim") + "<br/>";
    }
    if ($('#CP_txtUnblockOTP').val() == '') {
        msg += "<span>Please enter OTP</span><br/>";
    }

    if (msg.length > 0) {
        //$("#LoginValidation").css('color', 'red');
        $("#LoginValidation").css('display', 'block');
        $("#LoginValidation")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
    }
    else {
        var MemberId = $("#CP_txtRIM").val();
        var OTP = $("#CP_txtUnblockOTP").val();
        jQuery("#overlay").css('display', 'block');
        jQuery("#popup").css('display', 'block');
        jQuery("#popup").fadeIn(500);
        $.ajax({
            url: 'Login.aspx/UnlockMember',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberid':'" + MemberId.toString() + "'" + "," + "'pstrOTP':'" + OTP.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (data.d == "Invalid Credentials") {
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#CP_lblLoginError").text('');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                    }
                    else if (data.d == "Invalid Credentials") {
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#CP_lblLoginError").text('');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                    }
                    else if (data.d == "Invalid OTP") {
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#CP_lblLoginError").text('');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                    }
                    else {
                        $("#CP_txtMemberID").val('');
                        $("#CP_txtPwd").val('');
                        $("#divLogin").css("display", "none");
                        $("#divOTP").css("display", "none");
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                        $("#divUnblockOTP").css("display", "none");
                        $("#CP_lblLoginError").text('');
                    }

                }
                catch (e) {
                    alert(e);
                    jQuery("#overlay").css('display', 'none');
                    jQuery("#popup").css('display', 'none');
                    return false;
                }
                jQuery("#overlay").css('display', 'none');
                jQuery("#popup").css('display', 'none');
                return false;
            },
            error: function (errmsg) {

            }
        });
        jQuery("#overlay").css('display', 'none');
        jQuery("#popup").css('display', 'none');
        return false;

    }
};

function ActivationOTPOnEnter(e) {

    var ENTER_KEY = 13;
    var code = "";
    var code = (e.keyCode ? e.keyCode : e.which);

    if (code == ENTER_KEY) {
        ActivationOTPValidation();
        return false;
    }
    return true;
};

function ActivationOTPValidation() {

    $('#CP_ErrorMsgContainer').hide();
    $("#CP_lblLoginError")[0].innerHTML = "";
    var msg = "";

    if ($('#CP_txtMemberId').val() == '') {
        msg += "Please enter Member ID.<br/>";
    }
    if (msg.length > 0) {
        $("#CP_lblLoginError")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
        return false;
    }
    else {
        var MemberId = $("#CP_txtMemberId").val();
        $.ajax({
            url: 'Login.aspx/GenerateOTP',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: "{'pstrMemberId':'" + MemberId.trim().toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (newData != "") {
                        if (newData == "Success") {
                            $("#divActivationDetails").hide();
                            $("#divActivationOTP").show();
                            // $("#CP_txtotpMemberId").val(MemberId);
                            $("#CP_hfMemberId").val(MemberId);
                            $("#CP_lblLoginError")[0].innerHTML = "Your OTP has been sent to your Mobile No & Email.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid_Details") {
                            $("#CP_lblLoginError")[0].innerHTML = "Invalid Details.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid Email") {
                            $("#CP_lblLoginError")[0].innerHTML = "Invalid Member ID.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid QID") {
                            $("#CP_lblLoginError")[0].innerHTML = "Invalid QID.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid Code") {
                            $("#CP_lblLoginError")[0].innerHTML = "Invalid Security Code.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "InvalidMember") {
                            $("#CP_lblLoginError")[0].innerHTML = "Invalid details or Member not registered.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "OTPFAILED") {
                            $("#CP_lblLoginError")[0].innerHTML = "Failed to generate OTP.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Already Activated") {

                            $("#CP_lblLoginError")[0].innerHTML = "Account is already activated.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "CaseSensitive_MemberId") {
                            $("#CP_lblLoginError")[0].innerHTML = "Member ID is case sensitive. Please check the Member ID you have entered and try again.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else {
                            $("#CP_lblLoginError")[0].innerHTML = data.d + ' <br />';
                            $("#CP_ErrorMsgContainer").show();
                        }
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
};

function ActivationValidation() {
    $('#CP_ErrorMsgContainer').hide();
    $("#CP_lblMessagesDetails")[0].innerHTML = "";
    var msg = "";
    if ($('#CP_txtEQID').val() == '') {
        msg += $.i18n("text-please-enter-rim") + "<br/>";
    }
    if ($('#CP_txtOTP').val() == '') {
        msg += "Please enter received One Time Password (OTP).<br/>";
    }
    else if (!AcceptNumbersonly($('#CP_txtOTP').val())) {
        msg += "Enter Valid One Time Password (OTP).<br/>";
    }
    if ($('#CP_txtPassword').val() == '') {
        msg += "Please enter New Password.<br/>";
    }
    if (($('#CP_txtPassword').val().length < 8) && ($('#CP_txtPassword').val().length > 1)) {
        msg += "New password field has to be minimum eight characters." + "<br/>";
    }
    if ($('#CP_txtConfirmpassword').val() == '') {
        msg += "Please enter confirm New Password.<br/>";
    }
    if (($('#CP_txtConfirmpassword').val().length < 8) && ($('#CP_txtConfirmpassword').val().length > 1)) {
        msg += "Confirm new password field has to be minimum eight characters." + "<br/>";
    }
    if (($('#CP_txtPassword').val() != $('#CP_txtConfirmpassword').val()) && ($('#CP_txtPassword').val().length > 1)) {
        msg += "The new password and the confirmed new password must be same." + "<br/>";
    }
    if (!$('#CP_chkTerms').is(':checked')) {
        msg += "Please accept Terms & Conditions.<br/>";
    }
    if (!CheckPasswordPolicy($('#CP_txtPassword').val())) {
        msg += "Password must contain one numeric digit, one upper case character, one lower case character, one special character (!@#$%*()?) and minimum 8 characters in length." + "<br/>";
    }
    if (msg.length > 0) {
        $("#CP_lblMessagesDetails")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
        return false;
    }
    else {
        var QID = $("#CP_txtEQID").val();
        var OTP = $("#CP_txtOTP").val();
        var Password = $("#CP_txtPassword").val();
        var Pwd = $.md5(QID + Password);
        $('#CP_ErrorMsgContainer').hide();
        $("#CP_lblMessagesDetails")[0].innerHTML = "";

        $.ajax({
            url: 'Activation.aspx/MemberActivation',
            type: 'POST',
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrQID':'" + QID.trim().toString() + "'" + "," + "'pstrOTP':'" + OTP.toString() + "'" + "," + "'pstrPwd':'" + Pwd.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    $('#CP_txtotpMemberId').val(QID);
                    var newData = data.d;
                    if (newData != "") {
                        $('#CP_ErrorMsgContainer').hide();
                        $("#CP_lblMessagesDetails")[0].innerHTML = "";
                        if (data.d == "Success") {
                            $("#CP_lblMessagesDetails").css("color", "#2915c4");
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Thank you for activating your account. Please <a class='ErrorMessageLink' href='Login.aspx'>click here</a> to continue.";
                            $("#CP_ErrorMsgContainer").show();
                            $("#divActivationOTP").hide();
                            $("#InfoHeader").hide();
                        }
                        else if (newData == "Invalid_OTP") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Enter Valid OTP.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid_Credentials") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Invalid Details.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid_EmailId") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Invalid EmailId.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Account_Activated") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Account Already Activated.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid_QID") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Invalid QID.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid_RIM") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Invalid RIM.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Invalid_Program") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Invalid Program.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Incorrect_Format") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "Password must contain one numeric digit,one upper case character,one lower case character,one special character (!@#$%*()?) and minimum 8 characters in length.";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else {
                            $("#CP_lblMessagesDetails")[0].innerHTML = 'One or more of the credentials you have entered is incorrect and does not match with our records. Please reenter your details so that we can activate your Account.';
                            $("#CP_ErrorMsgContainer").show();
                        }
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
};

function GetMemberDetailsForLink() {
    var RIM = $("#CP_txtERIM").val()
    $.ajax({
        url: 'Activation.aspx/GetMemberDetailsForLink',
        type: 'POST',
        contentType: 'application/json; charset =utf-8',
        data: "{'pstrRIM':'" + RIM.toString() + "'}",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;
                //debugger;
                if (data.d != "") {
                    $("#CP_txtEmailID").val(newData[0]);
                    $("#CP_txtEQID").val(newData[1]);
                }
                else {
                    $("#CP_txtEmailID").val('');
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
};

function GenerateBlockOTP() {
    var RIM = $("#CP_txtMemberName").val()
    $("#CP_txtEQID").val(RIM);
    $.ajax({
        url: 'Login.aspx/GenerateBlockOTP',
        type: 'POST',
        contentType: 'application/json; charset =utf-8',
        data: "{'pstrRIM':'" + RIM.toString() + "'}",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;
                if (data.d == "Success") {
                    $("#CP_lblLoginError").text('OTP has been sent to your Mobile No and Email Id.');
                    $("#CP_txtRIM").val(RIM);
                }
                else {
                    $("#CP_lblLoginError").text('Unblock OTP Generation Failed');
                    $("#CP_txtRIM").val(RIM);
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
};

function ExtGenerateBlockOTP() {
    var RIM = $("#txtMemberName").val()
    $("#txtRIM").val(RIM);
    $.ajax({
        url: 'Login.aspx/GenerateBlockOTP',
        type: 'POST',
        contentType: 'application/json; charset =utf-8',
        data: "{'pstrRIM':'" + RIM.toString() + "'}",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;
                if (data.d == "Success") {
                    $("#lblLoginError").text('OTP has been sent to your Mobile No and Email Id.');
                    $("#txtRIM").val(RIM);
                }
                else {
                    $("#lblLoginError").text('Unblock OTP Generation Failed');
                    $("#txtRIM").val(RIM);
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
};

function CheckPasswordPolicy(pwd) {
    var Isvalid = false;
    $.ajax({
        url: 'Activation.aspx/GetPasswordPolicy',
        type: 'POST',
        async: false,
        contentType: 'application/json; charset =utf-8',
        data: "",
        dataType: 'json',
        success: function (data) {
            try {
                var pattern = new RegExp(data.d.toString());
                if (pwd.match(pattern)) {
                    Isvalid = true;
                }

            }
            catch (e) {
            }
        },
        error: function (errmsg) {
        }
    });
    return Isvalid;
}

function ExtLoginValidation() {
    //debugger;
    var msg = "";
    $("#ErrorMsgContainer").hide();
    $("#LoginValidation")[0].innerHTML = "";
    $("#lblLoginError").html('');
    var vdf = $("#txtMemberName").val();

    if ($("#txtMemberName").val().length == 0) {
        msg += $.i18n("text-please-enter-rim") + "<br/>";
    }
    if ($("#txtPassword").val().length == 0) {
        msg += "<span>Please enter Password</span><br/>";
    }

    //if (($('#txtPassword').val().length < 8) && ($('#txtPassword').val().length > 1)) {
    //    msg += "Password length has to be minimum eight characters.";
    //}

    if (msg.length > 0) {
        //$("#LoginValidation").css('color', 'red');
        $("#ErrorMsgContainer").show();
        $("#LoginValidation")[0].innerHTML = msg;
    }

    else {
        var MemberId = $("#txtMemberName").val();
        var password = $("#txtPassword").val();
        var Pwd = $.md5(MemberId + password);
        var RememberMe = new Boolean();
        RememberMe = $("#chkRememberMe")[0].checked;
        $.ajax({
            url: 'Login.aspx/SigninUser',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberid':'" + MemberId.toString() + "'" + "," + "'pstrPassword':'" + Pwd.toString() + "'" + "," + "'pboolRememberMe':'" + RememberMe + "'}",
            dataType: 'json',
            success: function (data) {
                try {

                    var newData = data.d;

                    if ((newData != "") || newData == null) {
                        if (data.d == "AccountLock") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Your Account is Locked.Please </span><a class="ErrorMessageLink" onclick="showOTPDiv();">click here</a><span> to unlock the same.</span>');
                        }
                        else if (data.d == "AuthenticationFailed") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Please check the login details you have entered and try again.</span>');
                        }
                        else if (data.d == "Suspended") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Your Account is Suspended</span>');
                        }
                        else if (data.d == "Cancelled") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Your Account is Cancelled</span>');
                        }
                        else if (data.d == "InActive") {
                            //$("#LoginValidation").css('color', 'red');
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Your Account is InActive. Please use activate tab to activate your account.</span>');
                        }
                        else if (data.d == "IncorrectFormat") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Password must contain one non-alpha character,one upper case character,one lower case character and minimum 8 characters in length.</span>');
                        }
                        else {
                            //  showLoginOTPDiv();
                            $("#txtMemberID").val(MemberId);
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html(data.d);
                        }
                    }
                    else {
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#ErrorMsgContainer").show();
                        $("#lblLoginError").html('<span>Please check the login details you have entered and try again.</span>');
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
    }
    return false;
};

function ExtValidateLoginOTP() {
    var msg = "";
    $("#lblLoginError").html('');
    $("#LoginValidation")[0].innerHTML = "";
    $("#ErrorMsgContainer").hide();

    if (msg.length > 0) {
        $("#ErrorMsgContainer").show();
        //$("#LoginValidation").css('color', 'red');
        $("#LoginValidation")[0].innerHTML = msg;
    }
    else {
        var MemberId = $("#txtMemberID").val();
        var OTP = $("#txtOTP").val();

        jQuery("#overlay").css('display', 'block');
        jQuery("#popup").css('display', 'block');
        jQuery("#popup").fadeIn(500);
        $.ajax({
            url: 'Login.aspx/ValidateOTP',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberid':'" + MemberId.toString() + "'" + "," + "'pstrOTP':'" + OTP.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if ((newData != "") || newData == null) {
                        if (data.d == "InvalidOTP") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Invalid OTP.</span>');
                        }
                        else if (data.d == "Invalid Credentials") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Invalid Credentials.</span>');
                        }
                        else if (data.d == "InvalidToken") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Please check the Authentication Token.</span>');
                        }
                        else if (data.d == "InvalidExtLogin") {
                            $("#LoginValidation")[0].innerHTML = "";
                            $("#ErrorMsgContainer").show();
                            $("#lblLoginError").html('<span>Invalid External Login Credentials.</span>');
                        }
                        else {
                            $("#LoginValidation")[0].innerHTML = "";
                            window.location.href = data.d;
                        }
                    }
                    else {
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#ErrorMsgContainer").show();
                        $("#lblLoginError").html('<span>Please check the details you have entered and try again.</span>');
                    }
                }
                catch (e) {
                    alert(e);
                    jQuery("#overlay").css('display', 'none');
                    jQuery("#popup").css('display', 'none');
                    return false;
                }
                jQuery("#overlay").css('display', 'none');
                jQuery("#popup").css('display', 'none');
                return false;
            },
            error: function (errmsg) {

            }
        });
        jQuery("#overlay").css('display', 'none');
        jQuery("#popup").css('display', 'none');
        return false;

    }
};

function ExtUnLockMemberByOTP() {

    var msg = "";
    $("#LoginValidation")[0].innerHTML = "";
    $("#ErrorMsgContainer").hide();
    if ($('#txtRIM').val() == '') {
        msg += $.i18n("text-please-enter-rim") + "<br/>";
    }
    if ($('#txtUnblockOTP').val() == '') {
        msg += "<span>Please enter OTP</span><br/>";
    }

    if (msg.length > 0) {
        //$("#LoginValidation").css('color', 'red');
        $("#LoginValidation").css('display', 'block');
        $("#LoginValidation")[0].innerHTML = msg;
        $("#ErrorMsgContainer").show();
    }
    else {
        var MemberId = $("#txtRIM").val();
        var OTP = $("#txtUnblockOTP").val();
        jQuery("#overlay").css('display', 'block');
        jQuery("#popup").css('display', 'block');
        jQuery("#popup").fadeIn(500);
        $.ajax({
            url: 'Login.aspx/UnlockMember',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberid':'" + MemberId.toString() + "'" + "," + "'pstrOTP':'" + OTP.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (data.d == "Invalid Credentials") {
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#lblLoginError").text('');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                    }
                    else if (data.d == "Invalid Credentials") {
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#lblLoginError").text('');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                    }
                    else if (data.d == "Invalid OTP") {
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#lblLoginError").text('');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                    }
                    else {
                        $("#txtMemberID").val('');
                        $("#txtPwd").val('');
                        $("#divLogin").css("display", "none");
                        $("#divOTP").css("display", "none");
                        $("#LoginValidation").css('color', '#ff0000');
                        $("#LoginValidation")[0].innerHTML = "";
                        $("#ErrorMsgContainer").show();
                        $("#LoginValidation")[0].innerHTML = data.d + ' <br />';
                        $("#divUnblockOTP").css("display", "none");
                        $("#lblLoginError").text('');
                    }

                }
                catch (e) {
                    alert(e);
                    jQuery("#overlay").css('display', 'none');
                    jQuery("#popup").css('display', 'none');
                    return false;
                }
                jQuery("#overlay").css('display', 'none');
                jQuery("#popup").css('display', 'none');
                return false;
            },
            error: function (errmsg) {

            }
        });
        jQuery("#overlay").css('display', 'none');
        jQuery("#popup").css('display', 'none');
        return false;

    }
};

function ExtLoginOnEnter(e) {
    var ENTER_KEY = 13;
    var code = "";
    var code = (e.keyCode ? e.keyCode : e.which);

    if (code == ENTER_KEY) {
        ExtLoginValidation();
        return false;
    }
    return true;
};

function SendForgotPasswordOTP() {
    var msg = "";
    $('#CP_ErrorMsgContainer').hide();
    $("#CP_lblMessagesDetails")[0].innerHTML = "";
    if ($('#CP_txtMemberId').val() == '') {
        msg += "<span>Please enter Member ID.</span><br/>";
    } else {
        if (!AcceptAlphanumericOnly($('#CP_txtMemberId').val())) {
            msg += "<span>Please enter valid Member ID.</span><br/>";
        }
    }
    if (msg.length > 0) {
        $("#CP_lblMessagesDetails")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
        return false;
    }
    else {
        var MemberId = $("#CP_txtMemberId").val();
        $.ajax({
            url: 'ForgotPassword.aspx/SendForgotPasswordOTP',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: "{'pstrMemberId':'" + MemberId.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (newData != "") {
                        if (newData == "Success") {
                            $("#divForgotOTP").hide();
                            $("#divForgotPassword").show();
                            $("#divMsg").show();
                        }
                        else if (newData == "Invalid_Member") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Your account has not been activated. please </span><a class='ErrorMessageLink text-underline' href='Activation.aspx'>activate</a><span> to continue.</span>";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Failure") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "<span>We could not generate your OTP - please try again.</span>";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "Not_Activated") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Your account has not been activated. please <a class='ErrorMessageLink text-underline' href='Activation.aspx'>activate</a> to continue.</span>";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else if (newData == "CaseSensitive_MemberId") {
                            $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Member ID is case sensitive. Please check the Member ID you have entered and try again.</span>";
                            $("#CP_ErrorMsgContainer").show();
                        }
                        else {
                            $("#CP_lblMessagesDetails")[0].innerHTML = data.d + ' <br />';
                            $("#CP_ErrorMsgContainer").show();
                        }
                    }
                    else {

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
};

function ValidateForgotPasswordOTP() {
    var msg = "";
    $("#CP_ErrorMsgContainer").hide();
    $("#CP_lblMessagesDetails")[0].innerHTML = "";

    if ($("#CP_txtOTP").val().length == 0) {
        msg += "<span>Please Enter OTP.</span><br/>";
    }
    if ($("#CP_txtPassword").val().length == 0) {
        msg += "<span>Please Enter Password.</span><br/>";
    }
    if ($("#CP_txtConfirmPassword").val().length == 0) {
        msg += "<span>Please Enter Confirm Password.</span><br/>";
    }
    if (($('#CP_txtPassword').val() != $('#CP_txtConfirmPassword').val()) && ($('#CP_txtPassword').val().length > 1)) {
        msg += "<span>The password and the confirmed password you enter must be the same.</span>" + "<br/>";
    }

    if (!CheckPasswordPolicy($('#CP_txtPassword').val())) {
        msg += "<span>Password must contain one numeric digit,one upper case character,one lower case character,one special character (!@#$%*()?) and minimum 8 characters in length.</span>" + "<br/>";
    }

    //var isvalid = IsValidationPd($('#CP_txtPassword').val());
    //if (!isvalid) {
    //    msg += "Incorrect password. Please try again." + "<br/>";
    //}

    if (msg.length > 0) {
        $("#CP_lblMessagesDetails")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
        return false;
    }
    else {
        var MemberId = $("#CP_txtMemberId").val();
        var MId = MemberId;
        var otp = $("#CP_txtOTP").val();
        var password = $("#CP_txtPassword").val();
        var Pwd = password;

        $.ajax({
            url: 'ForgotPassword.aspx/ValidateForgotPasswordOTP',
            type: 'POST',  // or get
            contentType: 'application/json; charset =utf-8',
            data: "{'pstrMemberId':'" + MemberId.toString() + "'" + "," + "'pstrOTP':'" + otp.toString() + "'" + "," + "'pstrPassword':'" + Pwd.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {
                    var newData = data.d;
                    if (data.d == "Success") {
                        $("#CP_lblMessagesDetails")[0].innerHTML = '<span>Your password has been reset successfully. Please </span><a href="Login.aspx">login</a>';
                        $("#CP_ErrorMsgContainer").show();
                        $("#CP_txtOTP")[0].value = "";
                        $("#CP_txtPassword")[0].value = "";
                        $("#CP_txtConfirmPassword")[0].value = "";
                        $("#divForgotOTP").hide();
                        $("#divForgotPassword").hide();
                        $("#divMsg").hide();
                    }
                    else if (data.d == "Invalid_OTP") {
                        $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Your OTP is invalid. Please enter a valid OTP.</span>";
                        $("#CP_ErrorMsgContainer").show();
                    }
                    else if (data.d == "Password_NotMatched") {
                        $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Your passwords do not match - please enter the same password.</span>";
                        $("#CP_ErrorMsgContainer").show();
                    }
                    else if (data.d == "PasswordPolicy_NotMatched") {
                        $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Incorrect password. Please try again.</span>";
                        $("#CP_ErrorMsgContainer").show();
                    }
                    else if (data.d == "Failure") {
                        $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Failed to reset password.</span>";
                        $("#CP_ErrorMsgContainer").show();
                    }
                    else {
                        $("#CP_lblMessagesDetails")[0].innerHTML = "TEST";
                        $("#CP_ErrorMsgContainer").show();
                    }
                }
                catch (e) {
                    return false;
                }
                return false;
            },
            error: function (errmsg) {
                alert(errmsg);
            }
        });
    }
    return false;
};

//function IsValidationPd(pwd) {
//    var pattern = new RegExp(/^(?=.{8,})(?=.*[a-zA-Z])((?=.*\d)|(?=.*[!@#$%*()]))/);
//    return pattern.test(pwd);
//};

function AcceptNumbersonly(number) {
    var pattern = new RegExp(/^-?\d*[.]?\d*$/);
    return pattern.test(number);
};

function isEmail(email) {
    var regex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    return regex.test(email);
}

function AcceptAlphanumericOnly(val) {
    var regex = /^[a-zA-Z0-9_]+$/;
    return regex.test(val);
}

function ActivationValidationCodeBehind() {
   
    $('#CP_ErrorMsgContainer').hide();
    $("#CP_lblLoginError")[0].innerHTML = "";
    var msg = "";
    if ($('#CP_txtOTP').val() == '') {
        msg += "<span>Please enter received One Time Password (OTP)</span><br/>";
    }
    else if (!AcceptNumbersonly($('#CP_txtOTP').val())) {
        msg += "<span>Enter Valid One Time Password (OTP).</span><br/>";
    }
   
    var Tnc = new Boolean();
    Tnc = $("#CP_chkTnC")[0].checked;

    if (Tnc == false) {
        msg += "<span>Please accept Terms and Conditions </span><br/>";
    }
    if (msg.length > 0) {
        $("#CP_lblLoginError")[0].innerHTML = msg;
        $("#CP_ErrorMsgContainer").show();
        return false;
    }
    else {
        return true;
    }
}

function MemberActivationCodeBehind(data) {
    try {
        var responsedata = data.split("|");
        var memberId = responsedata[1];
        var newData = responsedata[0];
        var url = responsedata[2];
        if (newData != "") {
            $('#CP_ErrorMsgContainer').hide();
            $("#CP_lblMessagesDetails")[0].innerHTML = "";
            if (newData == "Success") {
                //$("#CP_lblMessagesDetails").css("color", "#2915c4");
                //$("#CP_lblMessagesDetails")[0].innerHTML = "<span>Thank you for activating your account. Please <a class='ErrorMessageLink' href='Login.aspx'>click here</a> to continue.</span>";
                //$("#CP_ErrorMsgContainer").show();
                $("#divActivationOTP").hide();
                $("#divActivationDetails").hide();
                $("#InfoHeader").hide();
                window.location.href = url;
            }
            else if (newData == "Invalid_OTP") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Enter Valid OTP.</span>";
                $("#CP_ErrorMsgContainer").show();
                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable

            }
            else if (newData == "Invalid_Credentials") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Invalid Credentials.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "Invalid_MemberId") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Invalid Member ID.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "Invalid_SecurityCode") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Invalid Security Code.</span>";
                $("#CP_ErrorMsgContainer").show();
                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "Account_Activated") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Account Already Activated.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "Invalid_RIM") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Invalid RIM.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "Invalid_Program") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Invalid Program.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "Incorrect_Format") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Password must contain one numeric digit,one upper case character,one lower case character,one special character (!@#$%*()?) and minimum 8 characters in length.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else if (newData == "CaseSensitive_MemberId") {
                $("#CP_lblMessagesDetails")[0].innerHTML = "<span>Member ID is case sensitive. Please check the Member ID you have entered and try again.</span>";
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
            else {
                $("#CP_lblMessagesDetails")[0].innerHTML = '<span>One or more of the credentials you have entered is incorrect and does not match with our records. Please reenter your details so that we can activate your Account.</span>';
                $("#CP_ErrorMsgContainer").show();

                $("#divActivationOTP").show();
                $("#divActivationDetails").hide();

                $("#CP_hfMemberId").val(memberId);
                $("#CP_txtSecurityCode").val('');

                $("#CP_txtotpMemberId").prop('disabled', false); //enable
                $("#CP_txtotpMemberId").val(memberId);
                $("#CP_txtotpMemberId").prop('disabled', true); //disable
            }
        }
    }
    catch (e) {
        return false;
    }
    return false;
}

function MaskEmailID(myemailId) {

    //Masking 
    var maskid = "";
    var prefix = myemailId.substring(0, myemailId.lastIndexOf("@"));
    var postfix = myemailId.substring(myemailId.lastIndexOf("@"));
    for (var i = 0; i < prefix.length; i++) {
        if (i == 0 || i == prefix.length - 1) { ////////
            maskid = maskid + prefix[i].toString();
        }
        else {
            maskid = maskid + "*";
        }
    }
    maskid = maskid + postfix;

    $("#CP_txtEmailID").prop('disabled', false); //enable
    $("#CP_txtEmailID").val(maskid);
    $("#CP_txtEmailID").prop('disabled', true); //disable

}

function LoginValidationCodeBehind() {
    var msg = "";
    $("#CP_ErrorMsgContainer").hide();
    $("#LoginValidation")[0].innerHTML = "";
    $("#CP_lblLoginError").html('');
    if ($("#CP_txtMemberID").val().length == 0) {
        msg += "<span>Please enter Member ID</span><br/>";
    }
    if ($('#CP_txtSecurityCode').val() == '') {
        msg += "<span>Please enter Security Code</span><br/>";
    }
    if ($("#CP_txtPassword").val().length == 0) {
        msg += "<span>Please enter Password </span><br/>";
    }
    if (msg.length > 0) {
        //$("#LoginValidation").css('color', 'red');
        $("#CP_ErrorMsgContainer").show();
        $("#LoginValidation")[0].innerHTML = msg;
        $('#CP_BtnLoginValidation').prop('disabled', false);
        return false;
    }
    else {
        setTimeout(function () {
            $('#CP_BtnLoginValidation').prop('disabled', true);
        }, 1);
        return true;
    }
}

function MemberLoginCodeBehind(data) {
    var MemberId = $("#CP_txtMemberID").val();
    var str = data;
    var Text = str.split("+");
    data = Text[0];
    var Statustxt = Text[1];
    var Status = Statustxt.split(":");
    var Statustext = Status[1];
    var newData = data;
    if (Statustext == "Blocked") { AuthFail = "<span>Please check the login details you have entered and try again.</span>"; }
    else {
        AuthFail = "Account Block";
    }
    if ((newData != "") || newData == null) {
        if (data == "AccountLock") {
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Your Account is Locked.Please </span><a class="ErrorMessageLink" onclick="showOTPDiv();">click here</a><span> to unlock the same.</span>');
        }
        else if (data == "Success") {
            window.location = "Index.aspx";
        }
        else if (data == "Exceed OTP limit") {
            $("#CP_chkRememberMe")[0].checked = false;
            $("#CP_chkTnC")[0].checked = false;
            //window.location.reload(); 
            $("#CP_lblLoginError")[0].innerHTML = "<span>Exceed OTP Limit,Please try again</span>";
            $("#CP_ErrorMsgContainer").show();
            $("#divActivationDetails").show();
            $("#divActivationOTP").hide();
        }
        else if (data == "AuthenticationFailed") {
            //$("#LoginValidation").css('color', 'red');
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Please check the login details you have entered and try again.</span>');
        }
        else if (data == "Suspended") {
            //$("#LoginValidation").css('color', 'red');
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Your Account is Suspended</span>');
        }
        else if (newData == "Invalid_OTP") {
            $("#CP_lblLoginError")[0].innerHTML = "<span>Enter Valid OTP.</span>";
            $("#CP_ErrorMsgContainer").show();
            $("#divActivationDetails").hide();
            $("#divActivationOTP").show();
        }
        else if (newData == "Invalid_Credentials") {
            $("#CP_lblLoginError")[0].innerHTML = "<span>Invalid Credentials.</span>";
            $("#CP_ErrorMsgContainer").show();
        }
        else if (data == "Cancelled") {
            //$("#LoginValidation").css('color', 'red');
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Your Account is Cancelled</span>');
        }
        else if (data == "InActive") {
            //$("#LoginValidation").css('color', 'red');
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Your Account is InActive. Please use activate tab to activate your account.</span>');
        }
        else if (data == "IncorrectFormat") {
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Password must contain one non-alpha character,one upper case character,one lower case character and minimum 8 characters in length.</span>');
        }
        else if (data == "Invalid_MemberId") {
            //$("#LoginValidation").css('color', 'red');
            $("#divActivationDetails").hide();
            $("#divActivationOTP").show();
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Please check the login details you have entered and try again. </span>' + (typeof (Statustext) == 'undefined' ? '' : 'your Status is ' + Statustext));
        }
        else if (newData == "CaseSensitive_MemberId") {
            //$("#LoginValidation").css('color', 'red');
            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Member ID is case sensitive. Please check the login details you have entered and try again.</span>');
        }
      
        else {
            // showLoginOTPDiv();
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_hfMemberId").val(MemberId);
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html(data);
        }
    }
    else {
        $("#LoginValidation")[0].innerHTML = "";
        $("#CP_ErrorMsgContainer").show();
        $("#CP_lblLoginError").html('<span>Please check the login details you have entered and try again.</span>');
    }
    $('#CP_BtnLoginValidation').prop('disabled', false);
    return false;
}

function LoginMsgCodeBehind(data) {

    var responsdata = data.split("|");

    data = responsdata[0];
    var MemberId = responsdata[1];

    if (data != null || (data != "") || data != 'undefined') {

        if (data == "InvalidOTP") {
            // showLoginOTPDiv();

            $("#CP_hfMemberId").val(responsdata[1]);
            $("#CP_txtMemberID").prop('disabled', false); //enable
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_txtMemberID").prop('disabled', true); //disabled

            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Invalid OTP.</span>');
        }
        else if (data == "AccountLock") {
            // showLoginOTPDiv();

            $("#CP_hfMemberId").val(responsdata[1]);
            $("#CP_txtMemberID").prop('disabled', false); //enable
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_txtMemberID").prop('disabled', true); //disabled


            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#divOTP").hide();
            $("#CP_lblLoginError").html('<span>Your Account is Locked.Please kindly contact Infinity Rewards customer care to unlock the same.</span>');
        }
        else if (data == "SomethingWentWrong") {
            // showLoginOTPDiv();

            $("#CP_hfMemberId").val(responsdata[1]);
            $("#CP_txtMemberID").prop('disabled', false); //enable
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_txtMemberID").prop('disabled', true); //disabled

            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Something Went Wrong please try again.</span>');
        }
        else if (data == "InvalidToken") {
            // showLoginOTPDiv();

            $("#CP_hfMemberId").val(responsdata[1]);
            $("#CP_txtMemberID").prop('disabled', false); //enable
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_txtMemberID").prop('disabled', true); //disabled

            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Please check the Authentication Token.</span>');
        }
        else if (data == "InvalidExtLogin") {
            // showLoginOTPDiv();

            $("#CP_hfMemberId").val(responsdata[1]);
            $("#CP_txtMemberID").prop('disabled', false); //enable
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_txtMemberID").prop('disabled', true); //disabled

            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Invalid External Login Credentials.</span>');
        }
        else {
            // showLoginOTPDiv();

            $("#CP_hfMemberId").val(responsdata[1]);
            $("#CP_txtMemberID").prop('disabled', false); //enable
            $("#CP_txtMemberID").val(MemberId);
            $("#CP_txtMemberID").prop('disabled', true); //disabled

            $("#LoginValidation")[0].innerHTML = "";
            $("#CP_ErrorMsgContainer").show();
            $("#CP_lblLoginError").html('<span>Please check the details you have entered and try again.</span>');
        }
    }
    else {
        // showLoginOTPDiv();
        var MemberId = $("#CP_hfMemberId").val()
        $("#CP_txtMemberID").val(MemberId);
        $("#LoginValidation")[0].innerHTML = "";
        $("#CP_ErrorMsgContainer").show();
        $("#CP_lblLoginError").html('<span>Please check the details you have entered and try again.</span>');
    }
}

function FillDropDown() {
    $.ajax({
        url: 'Activation.aspx/BindSecurityQuestions',
        type: 'POST',  // or get
        contentType: 'application/json; charset =utf-8',
        data: "{}",
        dataType: 'json',
        success: function (data) {
            try {
                var newData = data.d;
                if (data.d[0] != "") {
                    var Qusetion1 = "<option value='1'>" + data.d[0] + "</option>";
                    var Qusetion2 = "<option value='2'>" + data.d[1] + "</option>";
                    var Qusetion3 = "<option value='3'>" + data.d[2] + "</option>";
                    $("#CP_ddlSecurityQuestion1").append(Qusetion1);
                    $("#CP_ddlSecurityQuestion1").append(Qusetion2);
                    $("#CP_ddlSecurityQuestion1").append(Qusetion3);
                    $("#CP_ddlSecurityQuestion2").append(Qusetion1);
                    $("#CP_ddlSecurityQuestion2").append(Qusetion2);
                    $("#CP_ddlSecurityQuestion2").append(Qusetion3);

                    $("#CP_hfSecurityQuestion1").val(data.d[0]);
                    $("#CP_hfSecurityQuestion2").val(data.d[0]);
                }
                else {

                }
            }
            catch (e) {

                jQuery("#overlay").css('display', 'none');
                jQuery("#popup").css('display', 'none');
                return false;
            }
            jQuery("#overlay").css('display', 'none');
            jQuery("#popup").css('display', 'none');
            return false;
        },
        error: function (errmsg) {

        }
    });
}

function ForceChangePasswordOnEnter(e) {
    var ENTER_KEY = 13;
    var code = "";
    var code = (e.keyCode ? e.keyCode : e.which);

    if (code == ENTER_KEY) {
        ForceChangePasswordValidation();
        return false;
    }
    return true;
}

function ForceChangePasswordValidation() {
    var msg = "";
    $("#CP_ErrorMsgContainer").hide();
    $("#ForceChangeValidation")[0].innerHTML = "";
    $("#CP_lblLoginError").html('');
    var vdf = $("#CP_txtMemberName").val();

    if ($("#CP_txtOldPassword").val().length == 0) {
        msg += (DefaultlangSelected ? "Please enter Old Password.<br />" : "Mohon masukan Old Password.<br/>");
    }
    if ($("#CP_txtNewPassword").val().length == 0) {
        msg += DefaultlangSelected ? "Please enter New Password.<br />" : "Mohon masukan New Password.<br/>";
    }
    if ($("#CP_txtConfirmPassword").val().length == 0) {
        msg += DefaultlangSelected ? "Please enter Confirm Password.<br />" : "Mohon masukan Confirm Password.<br/>";
    }

    if (($('#CP_txtConfirmPassword').val().length < 8) && ($('#CP_txtConfirmPassword').val().length > 1)) {
        msg += DefaultlangSelected ? "Password length has to be minimum eight characters.<br />" : "Password harus berisi: Minimal 8 karakter.<br/>";
    }

    if (msg.length > 0) {
        //$("#ForceChangeValidation").css('color', 'red');
        $("#CP_ErrorMsgContainer").show();
        $("#ForceChangeValidation")[0].innerHTML = msg;
    }

    else {
        var OldPassword = $("#CP_txtOldPassword").val();
        var NewPassword = $("#CP_txtNewPassword").val();
        var ConfirmPassword = $("#CP_txtConfirmPassword").val();
        //var Pwd = $.md5(MemberId + password);                
        $.ajax({
            url: 'ForceChangePassword.aspx/ForceChangePasswordForMember',
            type: 'POST',  // or get
            contentType: 'application/json; charset=utf-8',
            data: "{'pstrOldPassword':'" + OldPassword.toString() + "'" + "," + "'pstrNewPassword':'" + NewPassword.toString() + "'" + "," + "'pstrConfirmPassword':'" + ConfirmPassword.toString() + "'}",
            dataType: 'json',
            success: function (data) {
                try {

                    var newData = data.d;

                    if ((newData != "") || newData == null) {
                        if (data.d == "Success") {
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            $("#divLogin").css("display", "none");
                            $("#CP_ErrorMsgContainer").show();
                            msg = (DefaultlangSelected ? 'Password has been changed successfully. <a href="Login.aspx" style="color: black"> Click to Login</a>.' : 'Kata sandi berhasil diubah. <a href="Login.aspx" style="color: black">Klik untuk Login</a>.')
                            $("#CP_lblLoginError").html(msg);
                        }
                        else if (data.d == "Failed") {
                            //$("#ForceChangeValidation").css('color', 'red');
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            msg = (DefaultlangSelected ? 'Change Password Failure, please try again.' : 'Ubah Kata Sandi Gagal, silakan coba lagi.')
                            $("#CP_lblLoginError").html(msg);
                        }
                        else if (data.d == "NotMatched") {
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            msg = (DefaultlangSelected ? 'New Password and Old Password is not matching.' : 'Kata Sandi Baru dan Kata Sandi Lama tidak cocok.');
                            $("#CP_lblLoginError").html(msg);
                        }
                        else if (data.d == "Invalid") {
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            msg = (DefaultlangSelected ? 'Old Password is not Valid.' : 'Kata Sandi Lama Tidak Valid.');
                            $("#CP_lblLoginError").html(msg);
                        }
                        else if (data.d == "ResetPassword") {
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            msg = (DefaultlangSelected ? 'Please try using different password.' : 'Silakan coba menggunakan kata sandi yang berbeda.');
                            $("#CP_lblLoginError").html(msg);
                        }
                        else if (data.d == "InvalidFormat") {
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            $("#CP_ErrorMsgContainer").show();
                            msg = (DefaultlangSelected ? 'Password must contain one non-alpha character,one upper case character,one lower case character.' : 'Kata sandi harus berisi satu karakter non - alfa, satu karakter huruf besar, satu karakter huruf kecil.');
                            $("#CP_lblLoginError").html(msg);
                        }
                        else {
                            $("#ForceChangeValidation")[0].innerHTML = "";
                            window.location.href = data.d;
                        }
                    }
                    else {
                        $("#ForceChangeValidation")[0].innerHTML = "";
                        $("#CP_ErrorMsgContainer").show();
                        msg = (DefaultlangSelected ? 'Change Password Failure, please try again.' : 'Ubah Kata Sandi Gagal, silakan coba lagi.');
                        $("#CP_lblLoginError").html(msg);
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
    }
    return false;
};