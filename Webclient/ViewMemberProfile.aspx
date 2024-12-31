<%@ Page Title="View Member Profile" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="ViewMemberProfile.aspx.cs" Inherits="ViewMemberProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/account.css" rel="stylesheet" type="text/css" />
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
                                <h2 class="h3 heading-semibold text-white" id="lblMemberName"><span class="acc-text">Welcome,</span><span class="ml-2 acc-text" id="spnMemberName"></span></h2>
                            </div>
                        </li>
                        <li class="d-block">
                            <div class="text-center">
                                <h3 class="h3 heading-semibold text-white">
                                    <span id="totAvbPointDiv">Total Points</span>
                                    <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-white"></span></h3>
                            </div>
                        </li>
                    </ul>
                    <div class="mt-3">
                        <a
                            href="Index.aspx"
                            class="btn btn-two"
                            id="my_account_point_redeem_now">Redeem Now</a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="dvMember d-md-block d-none py-5">
    <div class="container-xl">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="h1 heading-semibold text-colour1" id="lblMemberName">
                    <span class="">Welcome,</span>
                    <span id="spnMemberName"></span>
                </h2>
                <h2 class="h5 heading-bold text-colour1 mt-2 mb-3">
                    <span id="totAvbPointDiv" >Total Points</span>
                    <span id="spnMemberCurrentBal" class="heading-bold text-colour1">0</span>
                </h2>
                <a
                    href="Index.aspx"
                    class="btn btn-one"
                    id="my_account_point_redeem_now">Redeem Now
                </a>
            </div>
        </div>
    </div>
</div>

    <div class="dvAccountMenu">
        <div class="container-xl">
            <div class="row equal-col my-3" id="AccMenu">
            </div>
        </div>
    </div>

    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-4">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                     <li class="breadcrumb-item" ><a href="StatementSummary.aspx">My Account</a></li>
                    <li class="breadcrumb-item active">Profile</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvProfile pb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <div class="border b-radius p-3">
                        <div class="dvCommonAccordion accordion" id="manage-accordion">
                            <div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button
                                            class="btn- btn-block text-left p-3 h6 text-uppercase"
                                            type="button"
                                            data-toggle="collapse"
                                            data-target="#collapse1">
                                            <span>Contact Details</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>

                                <div id="collapse1" class="collapse show" data-parent="#manage-accordion">
                                    <div class="card-body p-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="bg-colour6 p-3">
                                                    <div class="row">
                                                        <div class="col-sm-3 mb-3">
                                                            <p class="">
                                                                <span class="h7 d-block heading-semibold text-colour7">Name</span>
                                                                <asp:Label runat="server" CssClass="d-block" ID="labelMemberNameValue"></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-3 mb-3">
                                                            <p class="">
                                                                <span class="h7 d-block heading-semibold text-colour7">Mobile No.</span>
                                                                <asp:Label CssClass="d-block" ID="labelMobileNo" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hfRelationRef" runat="server"></asp:HiddenField>
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-3 mb-3">
                                                            <p class="">
                                                                <span class="h7 d-block heading-semibold text-colour7">E-mail ID</span>
                                                                <asp:Label ID="labelEmailValue" CssClass="d-block" runat="server"></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-3 mb-3">
                                                            <p class="">
                                                                <span class="h7 d-block heading-semibold text-colour7">Gender</span>
                                                                <asp:Label runat="server" CssClass="d-block" ID="lblGender"></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <p class="">
                                                                <span class="h7 d-block heading-semibold text-colour7">Nationality</span>
                                                                <asp:Label ID="lblNationality" CssClass="d-block" runat="server"></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <p class="">
                                                                <span class="h7 d-block heading-semibold text-colour7">Address</span>
                                                                <asp:Label ID="labelAddressValue" CssClass="d-block" runat="server"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="card mb-3">
                                <div class="card-header p-0">
                                    <h2 class="mb-0">
                                        <button
                                            class="btn- btn-block text-left p-3 h6 text-uppercase collapsed"
                                            type="button"
                                            data-toggle="collapse"
                                            data-target="#collapse2">
                                            <span>Change Password</span>
                                            <span class="arrow-icon">
                                                <i class="fa fa-caret-up"></i>
                                            </span>
                                        </button>
                                    </h2>
                                </div>
                                <div id="collapse2" class="collapse" data-parent="#manage-accordion">
                                    <div class="card-body p-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="bg-colour6 p-3">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="row">
                                                                <div class="col-12 position-relative fontawesome">
                                                                    <label class="label">Current Password</label>
                                                                    <div class="dvInputGroup input-group mb-3">
                                                                        <input
                                                                            autocomplete="off"
                                                                            type="password"
                                                                            class="form-control"
                                                                            placeholder="Enter Your Password"
                                                                            onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;"
                                                                            id="txtOldPassword" />
                                                                        <div class="input-group-append">
                                                                            <span toggle="#txtOldPassword" class="input-group-text bg-colour6 toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-12 position-relative fontawesome">
                                                                    <label class="label">New Password</label>
                                                                    <div class="dvInputGroup input-group mb-3">
                                                                        <input
                                                                            autocomplete="off"
                                                                            type="password"
                                                                            class="form-control"
                                                                            placeholder="Enter New Password"
                                                                            onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;"
                                                                            id="txtPassword" />
                                                                        <div class="input-group-append">
                                                                            <span toggle="#txtPassword" class="input-group-text bg-colour6 toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-12 position-relative fontawesome">
                                                                    <label class="label">Confirm Password</label>
                                                                    <div class="dvInputGroup input-group mb-3">
                                                                        <input
                                                                            autocomplete="off"
                                                                            type="password"
                                                                            class="form-control"
                                                                            placeholder="Enter New Password"
                                                                            onkeypress="var retValue = ValidateOnEnter(event); event.returnValue = retValue; return retValue;"
                                                                            id="txtNewPassword" />
                                                                        <div class="input-group-append">
                                                                            <span toggle="#txtNewPassword" class="input-group-text bg-colour6 toggle-password passwordShow fa fa-solid fa-eye-slash"></span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-8 mb-3">
                                                                   <asp:Button runat="server" ID="Button1" OnClientClick="var retValue = ChangePassword(); event.returnValue = retValue;return retValue;" CssClass="btn btn-one" Text="Save & Continue" />
                                                                </div>
                                                                <div id="ChangePasswordValidation" class="pl-3 text-danger">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <div class="pri-pol">
                                                                <h2 class="h6 heading-bold text-colour7">Password Policy:</h2>
                                                                <ul class="p-3">
                                                                    <li>Minimum 8 characters in length</li>
                                                                    <li>Should contain at least one capital case character</li>
                                                                    <li>Should contain at least one small case character</li>
                                                                    <li>Should contain at least one special character (!@#$%0?)
                                                                    </li>
                                                                    <li>Should contain at least one numeric digit</li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
