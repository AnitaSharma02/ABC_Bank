<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
    <script type="text/javascript">
        $("#CP_txtOTP").bind('keypress', function (e) {
            return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
        });
    </script>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }

        #captcha-container {
            font-size: 24px;
            font-weight: bold;
            letter-spacing: 3px;
            margin-bottom: 10px;
        }

        .tooltip-container {
            position: relative;
            display: inline-block;
        }

        .tooltip-container .tooltip {
            visibility: hidden;
            width: 150px;
            background-color: #555;
            color: #fff;
            text-align: center;
            border-radius: 5px;
            padding: 5px;
            position: absolute;
            z-index: 1;
            bottom: 125%;
            left: 50%;
            margin-left: -75px;
            opacity: 0;
            transition: opacity 0.3s;
        }

        .tooltip-container:hover .tooltip {
            visibility: visible;
            opacity: 1;
        }
        #divActivationDetails .dvInput.input-group .form-control {
        width:100%;
        }
    </style>

    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 pt-3 pb-0">
                    <li class="me-3"><a href="\">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active">Login</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="dvLogin py-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <div class="border bg-colour2 p-4">
                        <div class="row">
                            <div class="col-12 mb-2">
                                <h2 class="h5 heading-semibold text-colour7 mb-1">Login</h2>
                            </div>
                            <div class="col-12 mb-3">
                                <h5 class="h6 heading-regular" id="loginHeader">All fields are mandatory.</h5>
                            </div>
                            <div id="ErrorMsgContainer" class="dvErrors col-12" runat="server">
                                <asp:Label runat="server" ID="lblLoginError" Text="" CssClass="mt-3 h7 text-danger"></asp:Label>
                                <div id="LoginValidation" class="h7 text-danger"></div>
                            </div>
                            <%--<div class="col-12 mb-3" id="divLogin">
                                <div class="row">
                                    <div class="col-12 mb-3">
                                        <label class="label">ID</label>
                                        <div class="dvInput input-group">
                                            <input type="text" class="form-control" autocomplete="off" id="txtMemberID" runat="server" value="" />
                                        </div>
                                    </div>
                                    <div class="col-12 mb-3">
                                        <label class="label">Password:</label>
                                        <div class="dvInputGroup input-group">
                                            <input type="password" runat="server" autocomplete="off" class="form-control" id="txtPassword" />
                                             <span toggle="#CP_txtPassword" class="input-group-text bg-colour6 toggle-password passwordShow">
                                                    <i class="fa-regular fa-eye-slash"></i>
                                                </span>
                                         </div>
                                    </div>
                                    
                                    <div class="col-12 mb-3 valignM dvLabel">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block ms-1">
                                                <input id="chkRememberMe" runat="server" type="checkbox" />
                                                <span class="h6 heading-regular text-colour7">Remember me on this computer.</span>
                                                <span class="checkmark"></span>
                                            </span>
                                        </label>
                                    </div>
                                    <div class="col-12 mb-3">
                                        <asp:Button ID="BtnLoginValidation" runat="server" CssClass="btn btn-one w-100" OnClick="BtnLoginValidation_Click"
                                            Text="Continue" OnClientClick="if(!LoginValidationCodeBehind()) { return false;};" />
                                    </div>
                                    <div class="col-12">
                                        <div class="d-flex flex-wrap justify-content-between">
                                            <div class="mb-2 mb-sm-0">
                                                <asp:LinkButton ID="FormLinkPassword" CausesValidation="false" CssClass="heading-semibold link1"
                                                    runat="server" OnClientClick="var retvalue = redirectLocation('ForgotPassword.aspx'); event.returnValue= retvalue; return retvalue;">Forgot Password</asp:LinkButton>
                                            </div>
                                            <div>
                                                <asp:LinkButton ID="FormLinkActiveMembership" CssClass="heading-semibold link1" CausesValidation="false" runat="server"
                                                    OnClientClick="var retvalue = redirectLocation('Activation.aspx'); event.returnValue= retvalue; return retvalue;">Activate Membership</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-12">
                                <div id="divActivationDetails">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12 mb-3">
                                                    <label class="label">CIF</label>
                                                    <div class="tooltip-container">
                                                    <span class="exclamation"><img src="images/Info.svg" width="1" height="14" alt=""></span>
                                                            <div class="tooltip">Your CIF is a seven-digit code and is part of your account number <span style="letter-spacing: 1px;">009999<u class="text-danger h7 text-decoration-underline">9999999</u>99</span></div>
                                                  
                                                    </div>
                                                   <div class="dvInput input-group ">
                                                        <asp:TextBox ID="txtMemberId" autocomplete="off" runat="server" CssClass="form-control" onkeypress="var retValue = ActivationOTPOnEnter(event); event.returnValue = retValue; return retValue;"></asp:TextBox>
                                                     </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 mb-3">
                                            <input type="button" class="btn btn-one w-100" value="Continue" onclick="var varReturn = ActivationOTPValidation(); event.returnValue = varReturn; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return varReturn;" />
                                        </div>
                                    </div>
                                </div>

                                <div id="divActivationOTP" style="display: none;">
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <div class="row">
                                                <div class="col-12 mb-3">
                                                    <label class="label">OTP:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtOTP" runat="server" autocomplete="off" CssClass="form-control" TextMode="Password" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3 valignM dvLabel">
                                                    <label class="checkbox-container d-flex">
                                                        <span class="d-inline-block ms-1">
                                                            <input id="chkRememberMe" runat="server" type="checkbox" />
                                                            <span class="h6 heading-regular text-colour7">Remember me on this computer.</span>
                                                            <span class="checkmark"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                                <%--  <div class="col-12">
                                                    <div class="d-flex flex-wrap justify-content-between">
                                                        <div class="mb-2 mb-sm-0">
                                                            <asp:LinkButton ID="FormLinkPassword" CausesValidation="false" CssClass="heading-semibold link1"
                                                                runat="server" OnClientClick="var retvalue = redirectLocation('ForgotPassword.aspx'); event.returnValue= retvalue; return retvalue;">Forgot Password</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                                <div class="col-12 mb-3 valignM dvLabel">
                                                    <label class="checkbox-container d-flex">
                                                        <span class="d-inline-block ms-1">
                                                            <input id="chkTnC" runat="server" type="checkbox" />
                                                            <span>Please accept </span><a class="link1" href="\TermsAndConditions.aspx" target="_blank">Terms and Conditions</a>
                                                            <span class="checkmark"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Button ID="BtnActivationValidation" runat="server" CssClass="btn btn-one w-100" OnClick="BtnLoginValidation_Click" Text="Continue" OnClientClick="if (!ActivationValidationCodeBehind()) { return false;};" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="divMsg" style="display: none;">
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <div>
                                                <p class="h7 text-colour7">
                                                    1. Please enter One Time Password (OTP) that has been sent to your registered email and mobile number.
                                                </p>
                                                <p class="h7 text-colour7">
                                                    2. Create a new permanent password of your choice to access your account in the future.
                                                </p>
                                                <p class="h7 text-colour7">Your Password Should be:</p>
                                                <ul class="mx-3">
                                                    <li class="h7 text-colour7">Minimum 8 characters in length</li>
                                                    <li class="h7 text-colour7">Should contain at least one capital case character</li>
                                                    <li class="h7 text-colour7">Should contain at least one small character</li>
                                                    <li class="h7 text-colour7">Should contain at least one special character (@#$%&*) </li>
                                                    <li class="h7 text-colour7">Should contain at least one numeric digit.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#CP_txtMemberID").val('<%=pstrMembershipRef%>');
            $(".toggle-password").click(function () {
                $(this).toggleClass("fa-eye fa-solid fa-eye-slash");
                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }
            });
        });
        function showMyLoginDiv() {
            $("#divLogin").css("display", "none");
            $("#divOTP").css("display", "block");
            $("#LoginValidation")[0].innerHTML = "";
            $("#ct_SB_lblLoginError").text('');
            $("#loginHeader")[0].innerHTML = "";
            $("#loginHeader")[0].innerHTML = "All fields are mandatory.";
        }
    </script>
</asp:Content>
