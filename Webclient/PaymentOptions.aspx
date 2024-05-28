<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PaymentOptions.aspx.cs" Inherits="PaymentOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/payment.css" rel="stylesheet" type="text/css" />
    <style>
        .dvRedemptionMenu,
        .dvInnerBanner{
            display:none;
        }
    </style>

    <div id="paymentpage" class="dvPaymentOptions pay-options py-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-lg-6 offset-lg-3">
                    <div class="bg-colour3 p-3">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-white text-center border p-3">
                                    <p class="h6 heading-semibold mb-2 mb-2">Total Amount to be Paid</p>
                                    <span class="h5 heading-bold" id="spanProductAmount"></span>
                                    <p class="h6 heading-regular my-3">Would you like to use your NPoints in this Purchase?</p>
                                    <div class="range-slider col-12 pt-5 pb-2 px-3">
                                        <input type="text" readonly style="border: 0; color: #f6931f; font-weight: bold;" data-value="$37">
                                        <div id="slider-range-min">
                                            <span class="ui-slider-handle ui-handle"></span>
                                        </div>
                                        <div class="d-flex justify-content-between mt-3">
                                            <p class="h6 heading-regular">0 Npts</p>
                                            <p class="h6 heading-regular" id="slidermaxvalue"></p>
                                        </div>
                                    </div>
                                    <p class="h6 heading-regular my-3">Move the slider to use your NPoints for this purchase</p>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-12">
                                <p class="h6 heading-semibold mb-2">NPoints</p>
                                <div class="bg-white d-flex flex-wrap py-3">
                                    <div class="col-12 col-sm-6 mb-3 mb-sm-0">
                                        <p class="">
                                            <span class="h6 heading-semibold d-inline-block mr-2">Balance:</span>
                                            <span class="h6 heading-semibold d-inline-block" id="lblLoyaltypoints"></span>
                                            <input type="hidden" name="name" id="hndActualProductPoints" value="0" />
                                        </p>
                                    </div>
                                    <div class="col-12 col-sm-6">
                                        <p class="h6 heading-semibold">
                                            <span class="h6 heading-semibold d-inline-block mr-2">NPoints</span>
                                            <span class="h6 heading-semibold d-inline-block" id="lblSelectedpoints">0</span>
                                            <input type="hidden" name="name" id="hndSelectedRedeemPoints" value="0" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-around mt-3">
                            <div class="col-sm-6 col-lg-12 col-xl-6 mb-3 mb-sm-0 mb-lg-3 mb-xl-0">
                                <button type="button" class="btn btn-one w-100" onclick="CreatePayment();">Proceed to Payment</button>
                            </div>
                            <div class="col-sm-6 col-lg-12 col-xl-6">
                                <button type="button" class="btn btn-one w-100" data-toggle="modal" data-target="#confirmBoxModal">
                                    Cancel Payment
                                </button>
                            </div>                            
                        </div>
                    </div>
                    <%-- <div class="spinner col-12 text-center mt-5" style="display: none;">
     <p class="text mb-3">
         Please wait..<br />
         loading your ABSHER NPoints
     </p>
     <img src="/images/loader/spinner.svg" alt="Spinner" id="spinner" class="img-fluid" />
 </div>--%>
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
    <!-- payment fail modal -->
    <div class="modal modalPopup fade" id="payFailModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <i class="fa-solid fa-xmark"></i>
                </button>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-12 mb-4 text-center mt-4 mb-0">
                            <p class="confirm-text" id="lblError"></p>
                        </div>
                        <div class="col-12 text-center">
                            <button class="btn redBtnBg w-50" id="" data-dismiss="modal">Ok</button>
                        </div>
                    </div>
                </div>
                <div class="modal-footer d-none">
                    <button type="button" class="btn redBtnBg w-100" data-dismiss="modal">Filter</button>
                </div>
            </div>
        </div>
    </div>
    <!-- payment fail modal -->

    <!-- Added for range slider -->
    <script src="Jquery/1.13.2-jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            BindSlider('0');
            BindDetails();

            GetLoyaltyPoints();

        });

        function BindDetails() {
            $("#updProgress").show();
            $.ajax({
                type: 'POST',
                url: 'PaymentOptions.aspx/BindDetails',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d != "") {
                        //$("#spanProductAmount").html(msg.d.split('|')[0]);
                        document.getElementById("spanProductAmount").innerHTML = msg.d.split('|')[0];
                        BindSlider(msg.d.split('|')[1])
                        $("#updProgress").hide();
                    }
                   
                }
            })
        }

        function GetLoyaltyPoints() {

            // $(".spinner").show();
             $("#updProgress").show();
            $.ajax({
                type: 'POST',
                url: 'PaymentOptions.aspx/GetLoyaltyPoints',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d != "") {
                        var Point = parseInt(msg.d)
                        if (Point > 0) {

                            $("#lblLoyaltypoints").html(Point.toLocaleString("en-US") + " NPoints");
                            $("#hndLoyaltyPoints").val(msg.d);
                            $("#updProgress").hide();
                            //$(".spinner").hide();
                            $("#paymentpage").show();
                            $(".Paymentloading").hide();

                        }
                        else {
                            window.location.href = "PaymentReviewConfirm.aspx";
                        }
                    }
                }
            })
        }

        function BindSlider(maxPoint) {

            $(function () {
                var isDragging = false;
                var max_value = maxPoint;

                $("#hndActualProductPoints").val(maxPoint);

                $("#slidermaxvalue").html(Math.round(max_value).toLocaleString("en-US") + " Npts");

                $("#slider-range-min").slider({
                    range: "min",
                    value: 0,
                    min: 0,
                    max: max_value,
                    slide: function (event, ui) {
                        var value = Math.round(ui.value);
                        if (value < 1) {
                            value = 0;
                        } else if (value > max_value) {
                            value = max_value;
                        }
                        var price = value.toLocaleString("en-US") + " Npts";
                        $(".ui-slider-handle.ui-handle").attr("data-value", price);

                        SetSelectedPoints(Math.round(value));
                    },
                    start: function (event, ui) {
                        isDragging = true;
                        var value = Math.round(ui.value);
                        if (value < 1) {
                            value = 0;
                        } else if (value > max_value) {
                            value = max_value;
                        }
                        var price = value.toLocaleString("en-US") + " Npts";
                        $(this).find(".ui-slider-handle.ui-handle").attr("data-value", price);

                        SetSelectedPoints(Math.round(value));
                    },
                    stop: function (event, ui) {
                        isDragging = false;
                        var value = Math.round(ui.value);
                        if (value < 1) {
                            value = 0;
                        } else if (value > max_value) {
                            value = max_value;
                        }
                        var price = value.toLocaleString("en-US") + " Npts";
                        $(this).find(".ui-slider-handle.ui-handle").attr("data-value", price);

                        SetSelectedPoints(Math.round(value));
                    }
                }).on('touchstart', function (event) {
                    isDragging = false;
                }).on('touchmove', function (event) {
                    isDragging = true;
                    event.preventDefault();
                    var touch = event.originalEvent.touches[0] || event.originalEvent.changedTouches[0];
                    var x = touch.pageX - $(this).offset().left;
                    var value = x * ($("#slider-range-min").slider("option", "max") - $("#slider-range-min").slider("option", "min")) / $("#slider-range-min").width() + $("#slider-range-min").slider("option", "min");
                    if (value < 1) {
                        value = 0;
                    } else if (value > max_value) {
                        value = max_value;
                    }
                    $("#slider-range-min").slider("value", value);
                    var price = Math.round(value).toLocaleString("en-US") + " Npts";
                    $(this).find(".ui-slider-handle.ui-handle").attr("data-value", price);

                    SetSelectedPoints(Math.round(value));

                }).on('touchend', function (event) {
                    if (isDragging) {
                        isDragging = false;
                        event.preventDefault();
                    }
                });
                var initialValue = Math.round($("#slider-range-min").slider("value"));
                if (initialValue < 1) {
                    initialValue = 0;
                } else if (initialValue > max_value) {
                    initialValue = max_value;
                }
                $(".ui-slider-handle.ui-handle").attr("data-value", initialValue.toLocaleString("en-US") + " Npts");
            });
        }

        function SetSelectedPoints(price) {

            $("#lblSelectedpoints").html(price.toLocaleString("en-US"));
            $("#hndSelectedRedeemPoints").val(price);
        }

        function CreatePayment() {

            var SelectedRedeemPoints = parseInt($("#hndSelectedRedeemPoints").val());
            var ActualProductPoints = parseInt($("#hndActualProductPoints").val());
            $("#updProgress").show();
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
                         $("#updProgress").hide();
                    }
                    else {
                        window.location.href = msg.d;
                        $("#updProgress").hide();
                    }

                }
            })

        }

        function PaymentDiscardConfirm() {
            window.location.href = "Index.aspx";
        }

    </script>

</asp:Content>

