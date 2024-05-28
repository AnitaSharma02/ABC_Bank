<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ForceChangePassword.aspx.cs" Inherits="ForceChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        .scrollmenu {
            text-align: center;
            margin: 0px auto;
        }

        .Login {
            border-top: none !important;
        }

        .logintabs a {
            background-color: transparent;
            color: #666;
            float: left;
            font-size: 14px;
            height: 50px;
            line-height: 48px;
            text-align: center;
            vertical-align: middle;
            text-transform: uppercase;
            width: 49.7%;
            border: 1px solid #ddd;
            margin-right: -1px;
        }

            .logintabs a:hover, .logintabs .select {
                background: #ffac34;
                color: #fff !important;
            }

        .flt-contehldr .flt-divR {
            margin: 0% 0 0 0% !important;
        }


        .myacc-login {
            background: none !important;
        }

        .otpInstru h2 {
            font-size: 14px !important;
            font-weight: bold !important;
            text-align: left;
            text-decoration: underline;
            margin-bottom: 0px !important;
            padding-bottom: 0px;
        }

        ul.otpInstru li {
            display: block;
            text-align: left;
            margin-top: 5px;
            font-size: 12px;
        }

        @-webkit-keyframes autofill {
            to {
                color: #666;
                background: transparent;
            }
        }

        input:-webkit-autofill {
            -webkit-animation-name: autofill;
            -webkit-animation-fill-mode: both;
        }

        @media only screen and (max-width: 800px) and (min-width: 480px) {
            .flt-contehldr .flt-divR {
                width: 100% !important;
            }
        }

        @media screen and (max-width: 480px) {
            .myacc-login {
                width: 100% !important;
            }
        }

        .chideReferral {
            display: none;
        }

        .nav-pills .nav-link.active, .nav-pills .show > .nav-link {
            color: #fff;
            background-color: #ffac34;
        }
    </style>
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script src="Jquery/jquery.md5.js" type="text/javascript"></script>
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />

    <script src="Jquery/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Jquery/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="Jquery/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="Jquery/jquery.ui.position.js" type="text/javascript"></script>
    <script src="Jquery/jquery-ui.min.js" type="text/javascript"></script>
    <script src="Jquery/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //var Regi = getUrlParameter('registration')
            //if (Regi == '1') {
            //    TabSlider('htl', 'flt');
            //}

            //var Referral = getUrlParameter('ReferralCode')
            //if (Referral != '') {
            //    TabSlider('htl', 'flt');
            //    $('CP_txtReferralCode').val(Referral);
            //}

            $("#updProgress").ajaxStart(function () {
                //alert("ajax start");
                $(this).show();
            }).ajaxStop(function () {
                //alert("ajax stop");
                $(this).hide();
            });
            $(".toggle-password").click(function () {
                $(this).toggleClass("fa-eye fa-solid fa-eye-slash");
                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }
            });
            showErrorMsg();
        });

        function showErrorMsg() {
            console.log("asdsad");
            if ($("#CP_lblError").text() == "Your password have been expired" && DefaultlangSelected == true) {
                $("#CP_lblError").text("Mohon ubah kata sandi Anda");
            }
        }

        function showMyDiv() {
            $("#divActivationDetails").css("display", "none");
            $("#divActivationOTP").css("display", "block");
            GetMemberDetails();
            return false;
        }
        $("#CP_txtOTP").bind('keypress', function (e) {
            return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
        });


    </script>
    <link href="Css/MyAccount.css" rel="stylesheet" type="text/css" />
    <%--<input type="hidden" value="<%=isReferralActive %>" id="hdnIsReferralActive" />--%>
    <!-- <link href="Css/MyAccount.css" rel="stylesheet" type="text/css" /> -->
    <section class="mt-100 pt-100 pb-100 mb-100 bgimage" style="height: 100vh;">
        <div class="container">
            <div class="page-header text-center pb-4">
                <h3 class="txt-login-regist force_change_password">Force Change Password</h3>
            </div>
            <div class="row">
                <div class="col-md-12 col-12 text-center">
                    <p class="mandatory-info" id="spnMandatoryMessage">
                        <asp:Label runat="server" ID="lblError" Text="" ForeColor="Red" Visible="false"></asp:Label>
                    </p>
                </div>
                <div class="col-md-12 col-12 text-center">
                    <p class="OTPMessage" id="spnOTPMobilenOMessage"></p>
                </div>
                <div class="card bg-white p-0 col-md-4 col-10">
                    <div class="card-body">
                        <div class="tab-content mt-3">
                            <div class="tab-pane active" id="logintab" role="tabpanel" aria-labelledby="logintab">
                                <div id="ErrorMsgContainer" class=" ErrorMsgContainer mtop_10" runat="server">
                                    <asp:Label runat="server" ID="lblLoginError" Text="" ForeColor="Red"></asp:Label>
                                    <div id="ForceChangeValidation" style="text-align: center;"></div>
                                </div>
                                <div id="divLogin">
                                    <label for="CP_txtOldPassword" class="txt-username oldpass">Old Password:</label>
                                    <asp:HiddenField ID="hfRelationRef" runat="server"></asp:HiddenField>
                                    <div class="input-group mb-3">
                                        <input type="password"  autocomplete="off" class="form-control" aria-describedby="inputtxtPassword" id="txtOldPassword"
                                            onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;" />
                                        <div class="input-group-append">
                                            <span toggle="#txtOldPassword" class="input-group-text bg-white toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                        </div>
                                        <%--<div class="input-group-append">
									<span class="input-group-text bg-white" id="inputtxtMemberName"><i class="fa fa-user fa-2x"></i></span>
								</div>--%>
                                    </div>

                                    <label for="CP_txtPassword" class="txt-password Newpass">New Password:</label>
                                    <div class="input-group mb-3">
                                        <input type="password" autocomplete="off" class="form-control" aria-describedby="inputtxtPassword" id="txtPassword"
                                            onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;" />
                                        <div class="input-group-append">
                                            <span toggle="#txtPassword" class="input-group-text bg-white toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                        </div>
                                    </div>

                                    <label for="CP_txtNewPassword" class="txt-password repass">Confirm Password:</label>
                                    <div class="input-group mb-3">
                                        <input type="password" autocomplete="off" class="form-control" aria-describedby="inputtxtPassword"
                                            id="txtNewPassword" onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;" />
                                        <div class="input-group-append">
                                            <span toggle="#txtNewPassword" class="input-group-text bg-white toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                        </div>
                                    </div>

                                    <div class="form-group loginbtn txt-login-btn mt-3">
                                        <input type="button" class="blue_button btnContinue" value="Continue" onclick="var retvalue = ChangePassword(); event.returnValue = retvalue; return retvalue;" />
                                    </div>
                                    <div id="ChangePasswordValidation" class="pl-3" style="color: #ff0000; font-size: 13px; float: left;">
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="pri-pol">
                        <h2 class="h2 heading-regular">Password Policy:</h2>
                        <ul class="p-3">
                            <li class="heading-regular">Minimum 8 characters in length</li>
                            <li class="heading-regular">Should contain at least one capital case character</li>
                            <li class="heading-regular">Should contain at least one small case character</li>
                            <li class="heading-regular">Should contain at least one special character (!@#$%0?)
                            </li>
                            <li class="heading-regular">Should contain at least one numeric digit</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script type="text/javascript">
        function showMyLoginDiv() {
            $("#divLogin").css("display", "none");
            $("#divOTP").css("display", "block");
            $("#LoginValidation")[0].innerHTML = "";
            $("#ct_SB_lblLoginError").text('');

        }
        function showOTPDiv() {
            $("#divLogin").css("display", "none");
            $("#divOTP").css("display", "block");
            $("#CP_txtMemberID").val($("#CP_txtMemberName").val())
            $("#LoginValidation")[0].innerHTML = "";
            $("#ct_SB_lblLoginError").text('');
            GenerateOTPonLogin();
        }
        function showLoginDiv() {
            $("#divLogin").css("display", "block");
            $("#divOTP").css("display", "none");
            $("#LoginValidation")[0].innerHTML = "";
            $("#ct_SB_lblLoginError").text('');
        }
    </script>
</asp:Content>

