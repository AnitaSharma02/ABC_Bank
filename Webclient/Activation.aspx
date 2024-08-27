<%@ Page Title="Activation" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Activation.aspx.cs" Inherits="Activation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/account.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/jquery.md5.js" type="text/javascript"></script>
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#CP_txtOTP").bind('keypress', function (e) {
            return (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) ? false : true;
        });
        $(document).ready(function () {
            var Username = getQuerystring("user_name");
            if (Username != null && Username != "")
            {
                $("#CP_txtMemberId").val(Username);
                $('#CP_txtMemberId').attr('readonly', true);
            }
            $(".toggle-password").click(function () {
                $(this).toggleClass("fa-eye fa-eye-slash");
                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }
            });
        });
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
    </script>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner, .breadcrumbBox {
            display: none;
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active">Activation</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvLoginBoxbg my-0 my-md-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-sm-6 offset-sm-3">
                    <div class="innerBox p-4">
                        <div class="row">
                            <div class="col-12 mb-2">
                                <h2 class="h5 heading-semibold text-colour7 mb-1" data-i18n="navigation-activation">Activation</h2>
                            </div>
                            <div class="col-12 mb-3">
                                <h5 class="h6 heading-regular text-colour7" id="InfoHeader" data-i18n="text-all-fields-mandatory">All fields are mandatory.</h5>
                            </div>
                            <div id="ErrorMsgContainer" class="col-12" runat="server">
                                <asp:Label runat="server" ID="lblMessagesDetails" CssClass="h6 heading-semibold text-colour1" Text=""></asp:Label>
                            </div>
                            <div class="col-12">
                                <div id="divActivationDetails">
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <div class="row">
                                                <div class="col-12 mb-3">
                                                    <label class="label" data-i18n="text-member-id">ID</label>
                                                    <div class="dvInput input-group">
                                                        <asp:TextBox ID="txtMemberId" autocomplete="off" runat="server" CssClass="form-control" onkeypress="var retValue = ActivationOTPOnEnter(event); event.returnValue = retValue; return retValue;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="col-12 mb-3 valignM dvLabel">
                                            <label class="checkbox-container d-flex">
                                                <span class="d-inline-block ml-1">
                                                    <input id="chkTnC" runat="server" type="checkbox" />
                                                    <span data-i18n="text-login-please-accept-terms">Please accept <a href="\TermsAndConditions.aspx">Terms and Conditions</a>.</span>
                                                    <span class="checkmark"></span>
                                                </span>
                                            </label>
                                        </div>--%>
                                        <div class="col-12 mb-3">
                                            <input type="button" data-i18n="[value]btn-continue" class="btn btn-one w-100" value="Continue" onclick="var varReturn = ActivationOTPValidation(); event.returnValue = varReturn; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return varReturn;" />
                                        </div>
                                        <div class="col-12">
                                            <div class="d-flex flex-wrap justify-content-between">
                                                <div>
                                                    <asp:LinkButton ID="FormLinkLogin" CausesValidation="false" runat="server"
                                                        OnClientClick="var retvalue = redirectLocation('Login.aspx'); event.returnValue= retvalue; return retvalue;"
                                                        data-i18n="text-back-to-login" CssClass="heading-semibold link1">Back to Login</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divActivationOTP" style="display: none;">
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <div class="row">
                                                <div class="col-12">
                                                    <label class="h6 heading-semibold text-colour7" data-i18n="text-member-id">ID</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtotpMemberId" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                        <asp:HiddenField ID="hfMemberId" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <div id="InfiPlanetIdValidation"></div>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <label class="label" data-i18n="text-otp">OTP:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtOTP" runat="server" autocomplete="off" CssClass="form-control" TextMode="Password" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <label class="label" data-i18n="text-new-password">New Password:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtPassword" autocomplete="off" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                        <div class="input-group-append">
                                                            <span toggle="#CP_txtPassword" class="input-group-text bg-colour6 toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <div>
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
                                                <div class="col-12 mb-3">
                                                    <label class="label" data-i18n="text-confirm-password">Confirm New Password:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtConfirmpassword" autocomplete="off" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                        <div class="input-group-append">
                                                            <span toggle="#CP_txtConfirmpassword" class="input-group-text bg-colour6 toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="false">
                                                        <ContentTemplate>
                                                                <div class="d-inline-block">
                                                                    <asp:Image ID="ImgCaptcha" runat="server" ImageUrl="~/captcha.ashx" CssClass="" />
                                                                </div>
                                                                <div class="d-inline-block mt-3 mt-md-0">
                                                                    <asp:LinkButton ID="lnkBtnRefresh" runat="server" OnClick="lnkBtnRefresh_Click" CssClass="btn btn-one">Refresh</asp:LinkButton>
                                                                </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkBtnRefresh" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-12 mb-3">
                                                    <label class="label" data-i18n="text-Security-Code">Security Code:</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtSecurityCode" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-12 mb-3 valignM dvLabel">
                                                    <label class="checkbox-container d-flex">
                                                        <span class="d-inline-block ml-1">
                                                            <input id="chkTnC" runat="server" type="checkbox" />
                                                            <span data-i18n="text-login-please-accept">Please accept </span><a class="link1" href="\TermsAndConditions.aspx" data-i18n="text-terms-conditions">Terms and Conditions</a>
                                                            <span class="checkmark"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Button ID="BtnActivationValidation" data-i18n="[value]btn-continue" runat="server" CssClass="btn btn-one w-100" OnClick="BtnActivationValidation_Click" Text="Continue" OnClientClick="if (!ActivationValidationCodeBehind()) { return false;};" />
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
</asp:Content>
