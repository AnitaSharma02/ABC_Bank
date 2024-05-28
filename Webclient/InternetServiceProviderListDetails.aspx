<%@ Page Title="ISP List Details" Language="C#" MasterPageFile="~/SiteShopMaster.master" AutoEventWireup="true" CodeFile="InternetServiceProviderListDetails.aspx.cs" Inherits="InternetServiceProviderListDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPSHOP" runat="Server">
    <link rel="stylesheet" href="css/isp.css">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item"><a href="/InternetServiceProviders.aspx">Internet Service Providers</a></li>
                    <li class="breadcrumb-item active">Internet Service Provider Details</li>
                </ul>
            </nav>
        </div>
    </div>


    <div class="dvISPDetails">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 col-md-4">
                    <div class="card mb-3">
                        <a data-toggle="modal" data-target="#productModal">
                            <div class="img-container">
                                <img src="#" id="imgProductImageMain" runat="server" />
                            </div>
                            <%--<div class="d-flex flex-wrap bg-white p-3">
                                <p class="h6 heading-bold text-colour7 text-truncate" ></p>
                            </div>--%>
                        </a>
                    </div>
                </div>
                <div class="col-12 col-md-8">
                    <div class="row">
                        <div class="col-12 mb-3">
                            <h2 class="h2 heading-light text-colour1 text-truncate mb-1" id="divThumbnailServiceName" runat="server"></h2>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div id="divDynamicInputFields">
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="mb-3" id="divBuy" runat="server">
                                
                                    <a href="javascript:void(0);" class="btn btn-two mr-3" id="btnBack" runat="server">
                                        <div class="text-center">
                                            Back
                                        </div>
                                    </a>
                                    <a href="javascript:void(0);" class="btn btn-one" id="fetchUserdetails" runat="server" onclick="fnFetchUserDetails()">
                                        <div class="text-center">
                                            Fetch User Details
                                        </div>
                                    </a>

                                
                            </div>
                            <div id="divEmailErrorMsg" runat="server">
                                <h2 class="h6 heading-semibold text-danger">You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.</h2>
                            </div>
                        </div>

                        <div class="col-12">
                            <div id="divUserDetails" class="d-none">

                                <div id="CustomerDetails">
                                    <h3 class="h5 heading-semibold text-colour7 my-3">Customer Details</h3>
                                    <div class="bg-colour3 p-3">
                                        <div class="row">

                                            <!-- Username -->
                                            <div class="col-12 mb-3" id="trUsername">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Username:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="Username"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Username -->

                                            <!-- Customer Name -->
                                            <div class="col-12 mb-3" id="trCustomerName">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Customer Name:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="CustomerName"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Customer Name -->

                                            <!-- Email -->
                                            <div class="col-12 mb-3"id="trEmail">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Email:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right text-break" id="Email"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Email -->

                                            <!-- Mobile No -->
                                            <div class="col-12 mb-3" id="trMobileNo">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Mobile No.:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="MobileNo"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Mobile No -->

                                            <!-- Invoice No -->
                                            <div class="col-12 mb-3" id="trInvoiceNo">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Invoice No.:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="InvoiceNo"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Invoice No -->

                                            <!-- Branch -->
                                            <div class="col-12"  id="trBranch">
                                                <div class="bg-white p-3">
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Branch:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="Branch"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Branch -->

                                        </div>
                                    </div>
                                </div>

                                <div id="lblCurrentPlan">                              
                                        <h3 class="h5 heading-semibold text-colour7 my-3">Current Plan Details</h3>                                   
                                    <div class="bg-colour3 p-3">
                                        <div class="row">

                                            <!-- Plan Name -->
                                            <div class="col-12 mb-3" id="trPlanName">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Plan Name:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="PlanName"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Plan Name -->

                                            <!-- Plan Type -->
                                            <div class="col-12 mb-3" id="trPlanType">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Plan Type:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="PlanType"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Plan Type -->

                                            <!-- Amount/NPoints -->
                                            <div class="col-12 mb-3"  id="trCurrPlanAmount">
                                                <div class="bg-white p-3">
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Amount/NPoints:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="CurrPlanAmount"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Amount/NPoints -->

                                            <!-- Days Remaining -->
                                            <div class="col-12 mb-3" id="trDaysRemaining">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Days Remaining:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="DaysRemaining"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Days Remaining -->

                                            <!-- Due Amount/NPoints -->
                                            <div class="col-12 mb-3" id="trDueAmount">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Due Amount/NPoints:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="DueAmount"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Due Amount/NPoints -->

                                            <!-- Message -->
                                            <div class="col-12 mb-3" id="trCurrPlanMessage">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Message:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="CurrPlanMessage"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Message -->

                                            <!-- Previous Balance (Amount/NPoints) -->
                                            <div class="col-12 mb-3" id="trPreviousBalance">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Previous Balance (Amount/NPoints):</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="PreviousBalance"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Previous Balance (Amount/NPoints) -->

                                            <!-- End Date -->
                                            <div class="col-12 mb-3" id="trEndDate">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">End Date:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="EndDate"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- End Date -->

                                            <!-- Details -->
                                            <div class="col-12 mb-3"  id="trCurrPlanDetails">
                                                <div class="bg-white p-3">
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Details:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <select class="h6 heading-semibold form-control text-right" id="ddCurrPlanDetails"></select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Details -->

                                            <!-- Particular -->
                                            <div class="col-12 mb-3" id="trParticular">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Particular:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="Particular"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Particular -->

                                            <!-- Duration Code -->
                                            <div class="col-12 mb-3" id="trDurationCode">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Duration Code:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="DurationCode"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Duration Code -->

                                            <!-- Amount/NPoints -->
                                            <div class="col-12" id="trDetailsAmount">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Amount/NPoints:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="DetailsAmount"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Amount/NPoints -->
                                        </div>
                                    </div>
                                </div>

                                <div id="Packages">
                                    <h3 class="h5 heading-semibold text-colour7 my-3">Packages</h3>
                                    <div class="bg-colour3 p-3">
                                        <div class="row">

                                            <!-- Packages -->
                                            <div class="col-12 mb-3" id="trPkg">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Packages:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <select class="h6 heading-semibold form-control text-right" id="ddPkg"></select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Packages -->

                                            <!-- Details -->
                                            <div class="col-12 mb-3" id="trPkgDetails">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Details:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <select class="h6 heading-semibold form-control text-right" id="ddPkgDetails"></select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Details -->

                                            <!-- Duration Code -->
                                            <div class="col-12 mb-3" id="trPkgDetailsDurationCode">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Duration Code:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="PkgDetailsDurationCode"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Duration Code -->

                                            <!-- Amount/NPoints -->
                                            <div class="col-12 id="trPkgDetailsAmount">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Amount/NPoints:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold text-right" id="PkgDetailsAmount"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Amount/NPoints -->

                                        </div>
                                    </div>
                                </div>

                                <div id="Amount">
                                    <h3 class="h5 heading-semibold text-colour7 my-3">Amount</h3>
                                    <div class="bg-colour3 p-3">
                                        <div class="row">

                                            <!-- Amount/NPoints -->
                                            <div class="col-12  id="trAmount">
                                                <div class="bg-white p-3">
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">Amount/NPoints:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold form-control text-right" id="AmountPoints"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Amount/NPoints -->

                                        </div>
                                    </div>
                                </div>

                                <div id="TotalAmount">
                                    <h3 class="h5 heading-semibold text-colour7 my-3">NPoints</h3>
                                    <p id="duePresent" class="experienceerrormsg">Final NPoints includes Due NPoints + Package NPoints Selected.</p>
                                    <div class="bg-colour3 p-3">
                                        <div class="row">

                                            <!-- NPoints -->
                                            <div class="col-12  id="trTotalAmount">
                                                <div class="bg-white p-3" >
                                                    <div class="row">
                                                        <div class="col-md-4 col-6">
                                                            <p class="h6 text-regular">NPoints:</p>
                                                        </div>
                                                        <div class="col-md-8 col-6">
                                                            <p class="h6 heading-semibold form-control text-right" id="FinalAmount"></p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- NPoints -->

                                        </div>
                                    </div>
                                </div>

                                <div class="my-4">
                                    <span id="Span1" class="text-center">
                                        <a id="btnPayment" href="javascript:void(0);" class="btn btn-one d-none" onclick="fnPaymentRequest();">Payment</a>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="form-group pb-4 mt-3" id="divInsufficient" runat="server" style="display: none;">
                            <h2 class="h6 heading-semibold text-danger">Insufficient NPoints</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--Alert Modal -->
    <div class="dvModal modal fade" id="alertModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header justify-content-center pt-2 border-0">
                    <button type="button" class="close text-colour2" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body text-center pb-0" id="alertmessage">
                    <p class="h6 heading-semibold text-danger" id="errormessage"></p>
                </div>
                <div class="modal-footer justify-content-center border-0">
                    <button type="button" class="btn btn-one" data-dismiss="modal">Ok</button>
                    <%--<button type="button" class="btn btn-one">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var UserData = "";
            var PointRate = 0;
            var servicecode = getQuerystring('code');
            BindISPProductDetails(servicecode);

        });
        $(function () {
            $("#ddCurrPlanDetails").change(function (e) {
                //debugger
                if (UserData.CurrentPlan.Details[0].Particular != null) {
                    $("#Particular").html($('option:selected', this)[0].text);
                    var Amount = UserData.CurrentPlan.Details.find(x => x.Id == $('option:selected', this)[0].value).Amount;
                    $("#DetailsAmount").html(Amount + " NPR / " + ConvertAmountToPoints(Amount, PointRate) + " NPoints");
                }
                else {
                    $("#DurationCode").html($('option:selected', this)[0].value);
                    var Amount = UserData.CurrentPlan.Details.find(x => x.DurationCode == $('option:selected', this)[0].value).Amount;
                    $("#DetailsAmount").html(Amount + " NPR / " + ConvertAmountToPoints(Amount, PointRate) + " NPoints");
                }

            });
            $("#ddPkg").change(function (e) {
                //debugger
                var arr = UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value);
                if (UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details != null) {
                    const PackagesDetailsDropDown = document.getElementById("ddPkgDetails");
                    $(PackagesDetailsDropDown).empty();
                    for (var i = 0; i < UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details.length; i++) {
                        var option = document.createElement("option");
                        option.innerHTML = UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details[i].Duration;
                        option.value = UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details[i].DurationCode;
                        PackagesDetailsDropDown.options.add(option);
                    }
                    $("#trPkgDetails").removeClass("d-none");
                    $("#trPkgDetailsDurationCode").removeClass("d-none");
                    $("#PkgDetailsDurationCode").html(UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details[0].DurationCode);
                    $("#trPkgDetailsAmount").removeClass("d-none");
                    $("#PkgDetailsAmount").html(UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details[0].Amount + " NPR / " + ConvertAmountToPoints(UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Details[0].Amount, PointRate) + " NPoints");
                    if (UserData.CurrentPlan.DueAmount != 0) {
                        $("#duePresent").removeClass("d-none");
                        $("#FinalAmount").html(parseInt($("#DueAmount").text().split("/")[1].replace("NPoints", "")) + parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                    }
                    else {
                        $("#duePresent").addClass("d-none");
                        $("#FinalAmount").html(parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                    }
                    CheckAvailability($("#FinalAmount").text());
                }
                else {
                    $("#trPkgDetails").addClass("d-none");
                    $("#trPkgDetailsDurationCode").addClass("d-none");

                    if (UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Amount != 0) {
                        $("#trPkgDetailsAmount").removeClass("d-none");
                        $("#PkgDetailsAmount").html(UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Amount + " NPR / " + ConvertAmountToPoints(UserData.Packages.find(x => x.Id == $('option:selected', this)[0].value).Amount, PointRate) + " NPoints");
                        if (UserData.CurrentPlan.DueAmount != 0) {
                            $("#duePresent").removeClass("d-none");
                            $("#FinalAmount").html(parseInt($("#DueAmount").text().split("/")[1].replace("NPoints", "")) + parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                        }
                        else {
                            $("#duePresent").addClass("d-none");
                            $("#FinalAmount").html(parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                        }
                        CheckAvailability($("#FinalAmount").text());
                    }
                    else {
                        $("#trPkgDetailsAmount").addClass("d-none");
                    }

                }
            });
            $("#ddPkgDetails").change(function (e) {
                //debugger
                if (UserData.Packages.find(x => x.Id == $('option:selected', $("#ddPkg"))[0].value).Details.length > 0) {
                    $("#trPkgDetails").removeClass("d-none");
                    $("#trPkgDetailsDurationCode").removeClass("d-none");
                    $("#trPkgDetailsAmount").removeClass("d-none");
                    $("#PkgDetailsDurationCode").html($('option:selected', this)[0].value);
                    var Amount = UserData.Packages.find(x => x.Id == $('option:selected', $("#ddPkg"))[0].value).Details.find(x => x.DurationCode == $('option:selected', this)[0].value).Amount;
                    $("#PkgDetailsAmount").html(Amount + " NPR / " + ConvertAmountToPoints(Amount, PointRate) + " NPoints");
                    if (UserData.CurrentPlan.DueAmount != 0) {
                        $("#duePresent").removeClass("d-none");
                        $("#FinalAmount").html(parseInt($("#DueAmount").text().split("/")[1].replace("NPoints", "")) + parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                    }
                    else {
                        $("#duePresent").addClass("d-none");
                        $("#FinalAmount").html(parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                    }
                    CheckAvailability($("#FinalAmount").text());
                }
                else {
                    $("#trPkgDetails").addClass("d-none");
                    $("#trPkgDetailsDurationCode").addClass("d-none");
                    $("#trPkgDetailsAmount").addClass("d-none");
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
        function BindISPProductDetails(pstrServiceCode) {
            try {
                $.ajax({
                    type: 'POST',
                    url: 'InternetServiceProviderListDetails.aspx/BindISPProductDetails',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "{pstrServiceCode:" + pstrServiceCode + "}",
                    cache: false,
                    success: function (rtnData) {
                        //debugger;
                        fnBindDynamicFields(rtnData.d);
                    },
                    error: function (errmsg) {
                    }
                });
            } catch (e) {
            }
        }
        function fnBindDynamicFields(data) {
            //debugger
            try {
                if (data != '') {
                    var parseData = JSON.parse(data);
                    if (parseData.fields != null) {
                        var html = '';
                        html += '<div class="dvDetails">';
                        html += '<h3 class="h5 heading-semibold text-colour7 mb-3">Details</h3>';
                        html += '<div class="row">';
                        $.each(parseData.fields, function (i, currfield) {
                            if (currfield.Type.toLowerCase() == 'string') {
                                html += '<div class="col-lg-6 form-group">';
                                html += '<label class="h6 heading-regular text-colour7 mb-2">' + currfield.Title + '</label>';
                                html += '<input  onblur=\"fnValidateInput();\" placeholder=\"' + (currfield.Required ? currfield.Title + ' *' : currfield.Title) + '\" type=\"text\"  isRequired=\"' + currfield.Required + '\" dataFormat=\"text\" class=\"input form-control\" name=\"' + currfield.Title + '\"   id=\"txtpolicyno\"/>';
                                html += '</div>';
                            }
                            else if (currfield.Type.toLowerCase() == 'datetime') {
                                html += '<div class="col-lg-6 form-group">';
                                html += '<label class="h6 heading-regular text-colour7 mb-2">' + currfield.Title + '</label>';
                                html += '<div class="input-group">';
                                html += '<input onblur=\"fnValidateInput();\" placeholder=\"' + (currfield.Required ? currfield.Title + '*' : '') + '\" isRequired=\"' + currfield.Required + '\"  type=\"text\" onpaste=\"return false\" oncut=\"return false\" readonly class=\"input datePicker form-control cal-icon pr-5 hasDatepicker\"  id=\"dobdatepicker\" name=\"' + currfield.Title + '\" />';
                                html += '<div class="input-group-append"><span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span></div>';
                                html += '</div></div>';
                            }
                        });
                        html += '</div><div>';
                        html += '<div class="row"><div class="col-12">';
                        html += '<span class=\"h6 heading-semibold text-danger experienceerrormsg\" id=\"spnCompleteBookingErrorMsg\" style="display:none;"></span>';
                        html += '</div></div>';
                        html += '</div>';
                        $('#divDynamicInputFields').empty().html(html);
                        fnBindDatePicker();

                    }
                }
            } catch (e) {

            }
        }
        function fnBindDatePicker() {
            $(".dtinput").datepicker({
                //maxDate: 0,
                numberOfMonths: 1,
                changeMonth: true,
                changeYear: true,
                //yearRange: "-90:-0",
                //maxDate: '-12Y',
                dateFormat: 'yy-mm-dd',
                onSelect: function (dateText, inst) {
                    $("#" + this.id).removeClass('experienceerror');
                    $('#spnCompleteBookingErrorMsg').empty();
                    fnValidateInput();
                }
            });
            var dt = new Date();
        }
        function fnValidateInput() {
            var rtnResponse = true;
            try {
                var html = '';
                $(".bookinginput").each(function () {
                    $("#" + this.id).removeClass('experienceerror');
                    if ($("#" + this.id).hasClass("txtinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        var numericRegex = /^[a-zA-Z0-9_]*$/;
                        if (this.value != '') {
                            if (this.value.replace(/\s+/g, '') == '' || (this.value.replace(/\s+/g, '') != '' && !this.value.replace(/\s+/g, '').match(numericRegex))) {
                                $("#" + this.id).addClass('experienceerror');
                                var textmsg = $("#" + this.id).attr('name')
                                html += "<p>Please enter valid " + textmsg + "</p>";
                                //html += "<p>Please Enter Valid Data.</p>";
                                rtnResponse = false;
                            }
                        }
                        else {
                            var textmsg = $("#" + this.id).attr('name')
                            html += "<p>Please enter valid " + textmsg + "</p>";
                        }
                    }
                    if ($("#" + this.id).hasClass("dtinput") && $("#" + this.id).attr('isRequired').toLowerCase() == 'true') {
                        if (this.value.replace(/\s+/g, '') == '') {
                            $("#" + this.id).addClass('experienceerror');
                            var textmsg = $("#" + this.id).attr('name')
                            html += "<p>Please Select " + textmsg + ".</p>";
                            //html += "<p>Please Select DOB.</p>";
                            rtnResponse = false;
                        }
                    }
                });
                if (html != "") {
                    $('#spnCompleteBookingErrorMsg').empty().html(html);
                    $('#spnCompleteBookingErrorMsg').show();
                }
                else {
                    $('#spnCompleteBookingErrorMsg').empty().html(html);
                    $('#spnCompleteBookingErrorMsg').hide();
                }


            } catch (e) {
            }
            return rtnResponse;
        }
        function fnFetchUserDetails() {
            var isValid = fnValidateInput();
            if (isValid) {
                try {
                    var servicecode = getQuerystring('code');
                    $.ajax({
                        type: 'POST',
                        url: 'InternetServiceProviderListDetails.aspx/FetchUserDetails',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: "{pstrServiceCode:" + servicecode + ",UserId:'" + $("#txtpolicyno").val() + "'}",
                        cache: false,
                        success: function (data) {
                            if (data.d != "") {
                                fnBindUserDetailsFields(data.d);
                            }
                            else {
                                $('#spnCompleteBookingErrorMsg').empty().html("Invalid details entered.");
                                $('#spnCompleteBookingErrorMsg').show();
                            }

                        },
                        error: function (errmsg) {
                        }
                    });
                } catch (e) {
                }
            }

        }
        function fnBindUserDetailsFields(data) {
            //debugger
            try {
                if (data != '') {
                    if (isJson(data)) {
                        var parseData = JSON.parse(data);
                        if (parseData != null) {
                            $('#divUserDetails').removeClass("d-none");
                            UserData = parseData;
                            PointRate = parseData.PointRate;
                            //customer details
                            if (parseData.CustomerDetails.Username != null) {
                                $("#Username").html(parseData.CustomerDetails.Username);
                                $("#trUsername").removeClass("d-none");
                            }
                            else {
                                $("#trUsername").addClass("d-none");
                            }
                            if (parseData.CustomerDetails.CustomerName != "") {
                                $("#CustomerName").html(parseData.CustomerDetails.CustomerName);
                                $("#trCustomerName").removeClass("d-none");
                            }
                            else {
                                $("#trCustomerName").addClass("d-none");
                            }
                            if (parseData.CustomerDetails.Email != null) {
                                $("#Email").html(parseData.CustomerDetails.Email);
                                $("#trEmail").removeClass("d-none");
                            }
                            else {
                                $("#trEmail").addClass("d-none");
                            }
                            if (parseData.CustomerDetails.MobileNo != null) {
                                $("#MobileNo").html(parseData.CustomerDetails.MobileNo);
                                $("#trMobileNo").removeClass("d-none");
                            }
                            else {
                                $("#trMobileNo").addClass("d-none");
                            }
                            if (parseData.CustomerDetails.InvoiceNo != null) {
                                $("#InvoiceNo").html(parseData.CustomerDetails.InvoiceNo);
                                $("#trInvoiceNo").removeClass("d-none");
                            }
                            else {
                                $("#trInvoiceNo").addClass("d-none");
                            }
                            if (parseData.CustomerDetails.Branch != null) {
                                $("#Branch").html(parseData.CustomerDetails.Branch);
                                $("#trBranch").removeClass("d-none");
                            }
                            else {
                                $("#trBranch").addClass("d-none");
                            }

                            //CurrentPlan
                            if (parseData.CurrentPlan.PlanName == null && parseData.CurrentPlan.Amount == 0) {
                                $("#lblCurrentPlan").addClass("d-none");
                            } else {
                                $("#lblCurrentPlan").removeClass("d-none");
                            }
                            if (parseData.CurrentPlan.PlanName != null) {
                                $("#PlanName").html(parseData.CurrentPlan.PlanName);
                                $("#trPlanName").removeClass("d-none");
                            }
                            else {
                                $("#trPlanName").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.PlanType != null) {
                                $("#PlanType").html(parseData.CurrentPlan.PlanType);
                                $("#trPlanType").removeClass("d-none");
                            }
                            else {
                                $("#trPlanType").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.Amount != 0) {
                                $("#CurrPlanAmount").html(parseData.CurrentPlan.Amount + " NPR / " + ConvertAmountToPoints(parseData.CurrentPlan.Amount, PointRate) + " NPoints");
                                $("#trCurrPlanAmount").removeClass("d-none");
                            }
                            else {
                                $("#trCurrPlanAmount").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.DaysRemaining != 0) {
                                $("#DaysRemaining").html(parseData.CurrentPlan.DaysRemaining);
                                $("#trDaysRemaining").removeClass("d-none");
                            }
                            else {
                                $("#trDaysRemaining").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.DueAmount != 0) {
                                $("#DueAmount").html(parseData.CurrentPlan.DueAmount + " NPR / " + ConvertAmountToPoints(parseData.CurrentPlan.DueAmount, PointRate) + " NPoints");
                                $("#trDueAmount").removeClass("d-none");
                            }
                            else {
                                $("#trDueAmount").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.Message != null) {
                                $("#CurrPlanMessage").html(parseData.CurrentPlan.Message);
                                $("#trCurrPlanMessage").removeClass("d-none");
                            }
                            else {
                                $("#trCurrPlanMessage").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.PreviousBalance != 0) {
                                $("#PreviousBalance").html(parseData.CurrentPlan.PreviousBalance + " NPR / " + ConvertAmountToPoints(parseData.CurrentPlan.PreviousBalance, PointRate) + " NPoints");
                                $("#trPreviousBalance").removeClass("d-none");
                            }
                            else {
                                $("#trPreviousBalance").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.EndDate != null) {
                                $("#EndDate").html(parseData.CurrentPlan.EndDate);
                                $("#trEndDate").removeClass("d-none");
                            }
                            else {
                                $("#trEndDate").addClass("d-none");
                            }
                            if (parseData.CurrentPlan.Details != null) {
                                const CurrPlanDetailsDropDown = document.getElementById("ddCurrPlanDetails");
                                $(CurrPlanDetailsDropDown).empty();
                                for (var i = 0; i < parseData.CurrentPlan.Details.length; i++) {
                                    var option = document.createElement("option");
                                    if (parseData.CurrentPlan.Details[i].Duration == null) {
                                        option.innerHTML = parseData.CurrentPlan.Details[i].Particular;
                                        option.value = parseData.CurrentPlan.Details[i].Id;
                                    }
                                    else {
                                        option.innerHTML = parseData.CurrentPlan.Details[i].Duration;
                                        option.value = parseData.CurrentPlan.Details[i].DurationCode;
                                    }

                                    CurrPlanDetailsDropDown.options.add(option);
                                }
                                $("#trCurrPlanDetails").removeClass("d-none");
                                if (parseData.CurrentPlan.Details[0].Particular != null) {
                                    $("#trParticular").removeClass("d-none");
                                    $("#Particular").html(parseData.CurrentPlan.Details[0].Particular);
                                    $("#trDurationCode").addClass("d-none");
                                }
                                else {
                                    $("#trDurationCode").removeClass("d-none");
                                    $("#trParticular").addClass("d-none");
                                    $("#DurationCode").html(parseData.CurrentPlan.Details[0].DurationCode);
                                }
                                $("#trDetailsAmount").removeClass("d-none");
                                $("#DetailsAmount").html(parseData.CurrentPlan.Details[0].Amount + " NPR / " + ConvertAmountToPoints(parseData.CurrentPlan.Details[0].Amount, PointRate) + " NPoints");
                            }
                            else {
                                $("#trCurrPlanDetails").addClass("d-none");
                                $("#trParticular").addClass("d-none");
                                $("#trDurationCode").addClass("d-none");
                                $("#trDetailsAmount").addClass("d-none");
                            }
                            //Packages
                            if (parseData.Packages.length > 0) {
                                $("#Packages").removeClass("d-none");
                                $("#lblPackages").removeClass("d-none");
                                const PackagesDropDown = document.getElementById("ddPkg");
                                $(PackagesDropDown).empty();
                                for (var i = 0; i < parseData.Packages.length; i++) {
                                    var option = document.createElement("option");
                                    option.innerHTML = parseData.Packages[i].Package;
                                    option.value = parseData.Packages[i].Id;
                                    PackagesDropDown.options.add(option);
                                }
                                $("#trPkg").removeClass("d-none");
                                if (parseData.Packages[0].Details != null) {
                                    const PackagesDetailsDropDown = document.getElementById("ddPkgDetails");
                                    $(PackagesDetailsDropDown).empty();
                                    for (var i = 0; i < parseData.Packages[0].Details.length; i++) {
                                        var option = document.createElement("option");
                                        option.innerHTML = parseData.Packages[0].Details[i].Duration;
                                        option.value = parseData.Packages[0].Details[i].DurationCode;
                                        PackagesDetailsDropDown.options.add(option);
                                    }
                                    $("#trPkgDetails").removeClass("d-none");
                                    $("#trPkgDetailsDurationCode").removeClass("d-none");
                                    $("#PkgDetailsDurationCode").html(parseData.Packages[0].Details[0].DurationCode);
                                    $("#trPkgDetailsAmount").removeClass("d-none");
                                    $("#PkgDetailsAmount").html(parseData.Packages[0].Details[0].Amount + " NPR / " + ConvertAmountToPoints(parseData.Packages[0].Details[0].Amount, PointRate) + " NPoints");
                                }
                                else {
                                    $("#trPkgDetails").addClass("d-none");
                                    $("#trPkgDetailsDurationCode").addClass("d-none");
                                    if (parseData.Packages[0].Amount != 0) {
                                        $("#trPkgDetailsAmount").removeClass("d-none");
                                        $("#PkgDetailsAmount").html(parseData.Packages[0].Amount + " NPR / " + ConvertAmountToPoints(parseData.Packages[0].Amount, PointRate) + " NPoints");
                                    }
                                    else {
                                        $("#trPkgDetailsAmount").addClass("d-none");
                                    }
                                }
                            }
                            else {
                                $("#Packages").addClass("d-none");
                                $("#lblPackages").addClass("d-none");
                                $("#trPkg").addClass("d-none");
                            }

                            if (parseData.Amount == 0) {
                                $("#lblAmount").addClass("d-none");
                                $("#Amount").addClass("d-none");
                            } else {
                                $("#lblAmount").removeClass("d-none");
                                $("#Amount").removeClass("d-none");
                                $("#AmountPoints").html(parseData.Amount + " NPR / " + parseInt(ConvertAmountToPoints(parseData.Amount, PointRate)) + " NPoints");
                            }
                            //Final Amount
                            if (parseData.CurrentPlan.DueAmount == 0 && parseData.Packages.length == 0) {
                                $("#duePresent").addClass("d-none");
                                $("#FinalAmount").html(parseInt(ConvertAmountToPoints(parseData.Amount, PointRate)));
                            }
                            else if (parseData.CurrentPlan.DueAmount == 0 && parseData.Packages.length > 0) {
                                $("#duePresent").addClass("d-none");
                                $("#FinalAmount").html(parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                            }
                            else if (parseData.CurrentPlan.DueAmount != 0 && parseData.Packages.length > 0) {
                                $("#duePresent").removeClass("d-none");
                                $("#FinalAmount").html(parseInt($("#DueAmount").text().split("/")[1].replace("NPoints", "")) + parseInt($("#PkgDetailsAmount").text().split("/")[1].replace("NPoints", "")));
                            }
                            CheckAvailability($("#FinalAmount").text());
                        }
                    }
                    else {
                        $("#errormessage").html(data);
                        $('#alertModal').modal('show');
                    }
                }
            }
            catch (e) {

            }
        }
        function isJson(str) {
            try {
                JSON.parse(str);
                return true;
            } catch (e) {
                return false;
            }
            return true;
        }
        function CheckAvailability(amount) {
            $.ajax({
                type: "POST",
                url: "InternetServiceProviderListDetails.aspx/CheckAvailability",
                data: "{pntamount:" + amount + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        $("#CP_CPSHOP_divInsufficient").hide();
                        $("#btnPayment").removeClass('d-none');
                        return true;
                    }
                    else {
                        $("#CP_CPSHOP_divInsufficient").show();
                        $("#btnPayment").addClass('d-none');
                        return false;
                    }
                }
            });
            return false;
        }
        function fnPaymentRequest() {
            try {
                $.ajax({
                    type: "POST",
                    url: "InternetServiceProviderListDetails.aspx/CheckoutGenerateOTP",
                    data: "{pntamount:" + $("#FinalAmount").text() + ",PackageId:'" + $("#ddPkg").val() + "',DurationCode:'" + $("#ddPkgDetails").val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        var data = msg.d;
                        if (data.includes(".aspx")) {
                            window.location.href = data;
                        }
                        else if (data == "SESSION_TIME_OUT") {
                            $("#errormessage").html("Your session time out. Please login again.");
                            $('#alertModal').modal('show');
                        }
                        else {
                            $("#errormessage").html("Purchase failed!!! Please try again later.");
                            $('#alertModal').modal('show');
                        }
                        return false;
                    },
                    error: function (err) {
                    }
                });
            }
            catch (e) {
            }
        }
        function ConvertAmountToPoints(pstrAmount, pfltPointrate) {
            var points = 0;
            points = Math.ceil(pstrAmount / pfltPointrate);
            return points;
        }
    </script>
</asp:Content>

