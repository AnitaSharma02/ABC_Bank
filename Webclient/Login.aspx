<%@ Page Title="Login" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 pt-3 pb-0">
                    <li class="mr-3"><a href="\">
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
                                <asp:Label runat="server" ID="lblLoginError" Text="" CssClass="mt-3 h7 heading-regular text-danger" ForeColor="Red"></asp:Label>
                                <div id="LoginValidation" class="h6 heading-semibold text-colour1"></div>
                            </div>
                            <div class="col-12 mb-3" id="divLogin">
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
                                            <div class="input-group-append">
                                                <span toggle="#CP_txtPassword" class="input-group-text bg-colour6 toggle-password passwordShow">
                                                    <i class="fa-regular fa-eye-slash"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mb-3 valignM dvLabel">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block ml-1">
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
