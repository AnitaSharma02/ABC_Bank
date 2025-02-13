<%@ Page Title="Validate OTP" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ValidateOTP.aspx.cs" Inherits="ValidateOTP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <input type="hidden" id="hdnResendOTPEnableTime" value="<%=ResendOTPEnableTime%>" />
    <link href="Css/experience.css" rel="stylesheet" type="text/css" />
    <%--<script src="Jquery/purify.min.js" type="text/javascript"></script>--%>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 pt-3 pb-0">
                    <li class="me-3"><a href="hoteldetails.html">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active">Validate OTP</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="dvValidateOtp py-5 text-center">
        <div class="container-xl">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <div class="border bg-colour2 p-4">
                        <div class="row">
                            <div class="col-12 mb-2">
                                <h2 class="h5 heading-semibold text-colour7 mb-1">Validate OTP</h2>
                            </div>
                            <div class="col-12 mb-3">
                                <h2 class="h7 text-colour7">
                                    <p class="mb-3">One Time Password (OTP) has been sent to your registered mobile number and Email.</p>
                                    <p class="mb-3">Once received please enter the OTP below to complete your request.</p>
                                </h2>
                            </div>
                            <div class="col-12">
                                <div id="divActivationDetails" class="row">
                                    <div class="col-12">
                                        <label class="label">One Time Password</label>
                                        <div class="input-group">
                                            <input type="password" class="robot form-control" maxlength="4" id="txtOTP" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2">
                                        <asp:Button ID="btnResendOTP" runat="server" Text="Resend OTP" CssClass="btn btn-two" OnClick="btnResendOTP_Click" Style="display: none" />
                                        <div id="divCountdownTimer" class="h7 heading-semibold text-colour7" data-i18n="resendTest"></div>
                                    </div>
                                    <div class="col-12 mb-2">
                                        <asp:Label ID="lblResendOTPMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="col-12 mb-2">
                                        <asp:Button ID="btnContinue" runat="server" CssClass="btn btn-one w-100" OnClientClick="var retvalue = BookingValidation();event.returnValue= retvalue;if(event.preventDefault)event.preventDefault();  return retvalue;"
                                            Text="Continue" />
                                    </div>
                                </div>
                            </div>
                            <div id="ErrorMsgContainer" class="dvErrors col-12" runat="server">
                                <div id="validationResult"></div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            StartTimerResendOTP();
            $('#divOTPDetails').on('keyup keypress', function (e) {
                var keyCode = e.keyCode || e.which;
                if (keyCode === 13) {
                    e.preventDefault();
                    return false;
                }
            });
        });
        function StartTimerResendOTP() {
            ResendOTPEnableTime = Math.floor(parseInt($('#hdnResendOTPEnableTime').val()) / 60) + ":" + Math.floor(parseInt($('#hdnResendOTPEnableTime').val() % 60));
            fnActivateTimer(ResendOTPEnableTime, 'divCountdownTimer', 'CP_btnResendOTP');
        }
        function showMyDiv() {
            $("#OTP1").css("display", "none");
            $("#OTP2").css("display", "block");
            var lstrflag1 = getQuerystring("flag")
            var lstrflag = DOMPurify.sanitize(lstrflag1.value, { SAFE_FOR_TEMPLATES: true });
            if (lstrflag == 'Cashback') {
                $("#CP_ButtonBook").attr("src", "Images/Continue.png");
            }
            else {
                $("#CP_ButtonBook").attr("src", "Images/BookNow.png");
            }
            return false;
        }
        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }
        function BookingValidation() {
           
            $("#validationResult").hide();
            var msg = "";
            $("#validationResult")[0].innerHTML = '';
            if ($('#CP_txtOTP').val() == '') {
                msg += "Please enter received One Time Password.<br/>";
            }
            if (msg.length > 0) {
                $("#validationResult").show();
                $("#validationResult")[0].innerHTML = msg;
                $("#validationResult").css('color', '#ff0000');
                return false;
            }
            else {
                var pstrOTP = $("#CP_txtOTP").val();
                var strFlag1 = getQuerystring("flag");
                
                //var strFlag = DOMPurify.sanitize(strFlag1, { SAFE_FOR_TEMPLATES: true });
                
                $.ajax({
                    url: 'ValidateOTP.aspx/CheckOTP',
                    type: 'POST',  // or get
                    contentType: 'application/json; charset =utf-8',
                    data: "{'pstrOTP':'" + pstrOTP.toString() + "'" + "," + "'strFlag':'" + strFlag1 + "'}",
                    dataType: 'json',
                    success: function (data) {
                        
                        if (data.d != null && data.d != "") {

                            if (data.d == 'Exceed OTP limit') {
                                $("#validationResult").show();
                                $("#validationResult")[0].innerHTML = data.d + ' <br />';
                                $("#validationResult").css('color', '#ff0000');
                                return false;
                            }
                            else if (data.d == 'Invalid OTP') {
                                $("#validationResult").show();
                                $("#validationResult")[0].innerHTML = data.d + ' <br />';
                                $("#validationResult").css('color', '#ff0000');
                                return false;
                            }
                            else {
                                window.location = data.d;
                            }
                        }
                    },
                    error: function (errmsg) {
                    }
                });
                return false;
            }
        }
        function fnCallCheckout() {
            try {
                $.ajax({
                    type: "POST",
                    url: "ProductDetails.aspx/Checkout",
                    data: "{lstrProductId:'',lintQty:0,lstrUserInputMetas:''}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (data.includes(".aspx")) {
                            window.location.href = msg.d;
                        }
                        else {
                            if (msg.d == "INSUFFICIENT_POINTS") {
                                $('#divErrorMsg').empty().html("Insufficient Points.");
                            }
                            else {
                                $('#divErrorMsg').empty().html("Purchase failed!!! Please try again later.");
                            }
                        }
                    }
                });
            } catch (e) {
            }
        }
    </script>
</asp:Content>
