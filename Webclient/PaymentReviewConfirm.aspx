<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PaymentReviewConfirm.aspx.cs" Inherits="PaymentReviewConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/payment.css" rel="stylesheet" type="text/css" />
    <style>
        .dvRedemptionMenu,
        .dvInnerBanner {
            display: none;
        }
    </style>

    <div id="paymentpage" class="dvPaymentOptions pay-options py-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-lg-6 offset-lg-3">
                    <div class="bg-colour2 p-3">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour6 border p-3">
                                    <div class="row">
                                        <%--<div class="col-12 mb-3">
                                                <p class="mb-2">Product Name</p>
                                                <span class="heading-bold" id="spanProductName">Lulu gift Voucher</span>
                                            </div>--%>
                                        <div class="col-lg-6 offset-lg-3 text-center mb-3">
                                            <p>Total Points to be Paid</p>
                                            <span class="heading-bold" id="spanProductAmount"></span>
                                        </div>
                                        <div class="col-lg-6 offset-lg-3 text-center mb-3">
                                            <p>Total NPoint to be redeem</p>
                                            <span class="heading-bold" id="spanredeemPoint"></span>
                                        </div>
                                        <div class="col-lg-6 offset-lg-3 text-center mb-3">
                                            <p>Total Amount to be redeem</p>
                                            <span class="heading-bold" id="spanredeemAmount"></span>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-lg-12 col-sm-6 col-xl-6 mb-3 mb-sm-0 mb-lg-3 mb-xl-0">
                                <%--<a class="btn blue_button w-100 mt-4" onclick="ConfirmPayment();">Confirm Payment</a>--%>
                                <asp:Button ID="btnStripePayment" runat="server" CssClass="btn btn-one w-100" Text="Confirm Payment" OnClientClick="fnShowLoaderOnSubmitClick();" OnClick="btnStripePayment_Click" />
                            </div>
                            <div class="col-lg-12 col-sm-6 col-xl-6">
                                <button type="button" class="btn btn-one w-100" data-toggle="modal" data-target="#confirmBoxModal">
                                    Cancel Payment
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>

      <!-- Modal -->
    <div class="modal modalPopup fade" id="confirmBoxModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="heading-semibold text-colour6"" id="exampleModalLabel">Confirm</h5>
                    <button type="button" class="close d-flex" data-dismiss="modal" aria-label="Close">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12 mb-4">
                            <p class="">Are you sure you want to discard the payment process?</p>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-6">
                                    <button class="btn btn-two w-100" id="" data-dismiss="modal">No</button>
                                </div>
                                <div class="col-6">
                                    <span class="btn btn-two w-100" onclick="PaymentDiscardConfirm();">Yes</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <!-- Added for range slider -->
    <script src="Jquery/1.13.2-jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            BindPaymentDetails();

        });

        function BindPaymentDetails() {

            $.ajax({
                type: 'POST',
                url: 'PaymentReviewConfirm.aspx/BindPaymentDetails',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d != "") {

                        $("#spanProductAmount").empty().html("Points " + msg.d[0]);//ProductAmount
                        $("#spanredeemPoint").empty().html("Points " + msg.d[1]);//Selectedpoints
                        // $("#CP_hndSelectedAbsherpoints").val(msg.d[2]);//hndSelectedAbsherpoints
                        $("#spanredeemAmount").empty().html("MUR " + msg.d[3]);//RemainingAmount

                    }
                    $("#updProgress").hide();
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            })
        }

        function CreatePayment() {

            var SelectedRedeemPoints = parseInt($("#hndSelectedRedeemPoints").val());
            var ActualProductPoints = parseInt($("#hndActualProductPoints").val());

            $.ajax({

                type: "POST",
                url: "PaymentOptions.aspx/CreatePayment",
                data: "{'lintSelectedRedeemPoints':'" + SelectedRedeemPoints + "'" + "," + "'lintActualProductPoints':'" + ActualProductPoints + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg.d == "Insuficient_Balance") {
                        //alert("Insuficient Balance.")
                        $("#btnFailPaymentModal").click();
                        $("#lblError").html("Insufficient Balance.");

                    }
                    else {
                        window.location.href = msg.d;
                    }
                    $("#updProgress").hide();
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            })

        }
        function PaymentDiscardConfirm() {
            window.location.href = "Index.aspx";
        }
        function fnShowLoaderOnSubmitClick() {
            //debugger      
            $("#updProgress").css("display", "block");
            setTimeout(function () {
                $("#updProgress").css("display", "none");
            }, 6000);
        }
    </script>

</asp:Content>

