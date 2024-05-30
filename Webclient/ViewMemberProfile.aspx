<%@ Page Title="View Member Profile" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="ViewMemberProfile.aspx.cs" Inherits="ViewMemberProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/MyAccount.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/jquery.md5.js" type="text/javascript"></script>
    <script src="Jquery/Validation.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#spnMemberName').empty().html($('.uName').html());
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
    </script>
    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        .dvShopMenu {
            display: none;
        }
    </style>

    <%--<div class="dvMember">
        <div class="d-md-block d-none">
            <div class="align-items-center bg-acc d-flex justify-content-center">
                <div class="d-flex justify-content-center align-items-center flex-column">
                    <ul>
                        <li class="d-block">
                            <div class="text-center">
                                <h2 class="h3 heading-semibold text-white" id="lblMemberName"><span data-i18n="account-welcome" class="acc-text">Welcome,</span><span class="ml-2 acc-text" id="spnMemberName"></span></h2>
                            </div>
                        </li>
                        <li class="d-block">
                            <div class="text-center">
                                <h3 class="h3 heading-semibold text-white">
                                    <span id="totAvbPointDiv" data-i18n="account-total-points">Total NPoints</span>
                                    <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-white"></span></h3>
                            </div>
                        </li>
                    </ul>
                    <div class="mt-3">
                        <a
                            href="Index.aspx"
                            class="btn btn-two"
                            id="my_account_point_redeem_now"
                            data-i18n="btn-redeem-now">Redeem Now</a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="dvMember d-md-block d-none py-5">
    <div class="container-lg">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="h1 heading-semibold text-colour1" id="lblMemberName">
                    <span data-i18n="account-welcome" class="">Welcome,</span>
                    <span class="ml-2" id="spnMemberName"></span>
                </h2>
                <h2 class="h5 heading-bold text-colour1 mt-2 mb-3">
                    <span id="totAvbPointDiv" >Total NPoints</span>
                    <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-colour1">0</span>
                </h2>
                <a
                    href="Index.aspx"
                    class="btn btn-one"
                    id="my_account_point_redeem_now"
                    data-i18n="btn-redeem-now">Redeem Now
                </a>
            </div>
        </div>
    </div>
</div>

    <div class="dvAccountMenu">
        <div class="container-lg">
            <div class="row equal-col my-3" id="AccMenu">
            </div>
        </div>
    </div>

    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-4">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item" data-i18n="bread-my-account">My Account</li>
                    <li class="breadcrumb-item active" data-i18n="bread-profile">Profile</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvProfile pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12">
                    <div class="bg-lightgray rounded p-3">
                        <div class="accordion" id="manage-accordion">
                            <div class="card">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button
                                            class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase"
                                            type="button"
                                            data-toggle="collapse"
                                            data-target="#collapse1">
                                            <span data-i18n="vp-contact-details">Contact Details</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>

                                <div id="collapse1" class="collapse show" data-parent="#manage-accordion">
                                    <div class="card-body p-3">
                                        <div class="row">
                                            <div class="form-group col-sm-3">
                                                <p class="">
                                                    <span class="d-block h7 heading-semibold" data-i18n="vp-name">Name</span>
                                                    <asp:Label runat="server" CssClass="d-block" ID="labelMemberNameValue"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <p class="">
                                                    <span class="d-block h7 heading-semibold" data-i18n="vp-mobile-no">Mobile No.</span>
                                                    <asp:Label CssClass="d-block" ID="labelMobileNo" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hfRelationRef" runat="server"></asp:HiddenField>
                                                </p>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <p class="">
                                                    <span class="d-block h7 heading-semibold" data-i18n="vp-email-id">E-mail ID</span>
                                                    <asp:Label ID="labelEmailValue" CssClass="d-block" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <p class="">
                                                    <span class="d-block h7 heading-semibold" data-i18n="vp-gender">Gender</span>
                                                    <asp:Label runat="server" CssClass="d-block" ID="lblGender"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <p class="">
                                                    <span class="d-block h7 heading-semibold" data-i18n="vp-nationality">Nationality</span>
                                                    <asp:Label ID="lblNationality" CssClass="d-block" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="form-group col-sm-3">
                                                <p class="">
                                                    <span class="d-block h7 heading-semibold" data-i18n="vp-address">Address</span>
                                                    <asp:Label ID="labelAddressValue" CssClass="d-block" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button
                                            class="btn- btn-block text-left p-3 h6 heading-semibold text-uppercase collapsed"
                                            type="button"
                                            data-toggle="collapse"
                                            data-target="#collapse2">
                                            <span data-i18n="vp-change-password">Change Password</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse2" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body p-3">
                                        <div class="row mt-4">
                                            <div class="col-lg-6">
                                                <div class="row">
                                                    <div class="col-12 position-relative fontawesome">
                                                        <label class="h8 heading-semibold">Current Password</label>
                                                        <div class="input-group mb-3">
                                                            <input
                                                                autocomplete="off"
                                                                type="password"
                                                                class="form-control"
                                                                placeholder="Enter Your Password"
                                                                onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;" 
                                                                id="txtOldPassword"/>
                                                            <div class="input-group-append">
                                                                <span toggle="#txtOldPassword" class="input-group-text bg-white toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12 position-relative fontawesome">
                                                        <label class="h8 heading-semibold">New Password</label>
                                                        <div class="input-group mb-3">
                                                            <input
                                                                autocomplete="off"
                                                               type="password"
                                                                class="form-control"
                                                                placeholder="Enter New Password"
                                                                onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;"
                                                                id="txtPassword"/>
                                                            <div class="input-group-append">
                                                                <span toggle="#txtPassword" class="input-group-text bg-white toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12 position-relative fontawesome">
                                                        <label class="h8 heading-semibold">Confirm Password</label>
                                                        <div class="input-group mb-3">
                                                            <input
                                                                autocomplete="off"
                                                                type="password"
                                                                class="form-control"
                                                                placeholder="Enter New Password"
                                                                onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;"
                                                                id="txtNewPassword"/>
                                                            <div class="input-group-append">
                                                                <span toggle="#txtNewPassword" class="input-group-text bg-white toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-8 mb-3">
                                                        <%--<button style="margin-bottom: 1px" class="btn btn-one" type="button" OnClientClick="var retValue = ChangePassword(); event.returnValue = retValue;return retValue;">Save & Continue</button>--%>
                                                         <asp:Button runat="server" ID="Button1" OnClientClick="var retValue = ChangePassword(); event.returnValue = retValue;return retValue;" CssClass="btn btn-one" Text="Save & Continue" />
                                                    </div>
                                                    <div id="ChangePasswordValidation" class="pl-3" style="color: #ff0000; font-size: 13px; float: left;">
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
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
