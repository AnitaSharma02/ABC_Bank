<%@ Page Title="Order Details" Language="C#" MasterPageFile="SiteShopMaster.master" AutoEventWireup="true" CodeFile="OrderDetails.aspx.cs" Inherits="OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPSHOP" runat="Server">
    <link href="Css/account.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#spnMemberName').empty().html($('.uName').html());
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

    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="OrderHistory.aspx">Order History</a></li>
                    <li class="breadcrumb-item active">Tracking</li>
                </ul>
            </nav>
        </div>
    </div>

    <%--<div class="container-xl">
        <div class="row">
            <div class="col-12">
                <h6 class="orderD">Order ID: <span></span></h6>

                <div class="track">
                </div>
            </div>
            <div class="col-12">
                <div class="immrt30" style="margin-bottom: 60px;">
                </div>
            </div>
        </div>
    </div>--%>

    <div class="dvOrderDetails pb-5">
        <div class="container-xl">
            <div class="dvOrderId row">
                <div class="col-12 mb-3">
                    <p class="h6 heading-regular">Order ID: <span id="spanOrderId" runat="server" class="h6 heading-bold text-colour1"></span></p>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <div class="bg-colour2 p-3" runat="server" id="divOrderCard">
                        <div class="row">
                            <div class="col-12">
                                <div class="bg-colour6 p-3 orderCard" >
                                    <div class="row">
                                        <div class="col-sm-6 col-md-3 col-lg-6 mb-2 mb-lg-3">
                                            <p class="h6 heading-semibold text-colour7">Estimated Delivery Time:</p>
                                            <p id="spanDeliveryTimeEst" class="h6 heading-regular text-colour7" runat="server"></p>
                                        </div>
                                        <div class="col-sm-6 col-md-3 col-lg-6 mb-2 mb-lg-3" style="display: none;">
                                            <p class="h6 heading-semibold text-colour7">Shipping By:</p>
                                            <p class="h6 heading-regular text-colour7" id="spanShippingBy" runat="server"></p>
                                        </div>
                                        <div class="col-sm-6 col-md-3 col-lg-6 mb-2 mb-lg-3">
                                            <p class="h6 heading-semibold text-colour7">Status:</p>
                                            <p class="h6 heading-regular text-colour7" id="spanOrderStatus" runat="server"></p>
                                        </div>
                                        <div class="col-sm-6 col-md-3 col-lg-6 mb-2 mb-lg-3">
                                            <p class="h6 heading-semibold text-colour7">Tracking #:</p>
                                            <p class="h6 heading-regular text-colour7" id="spanTrackingNo" runat="server"></p>
                                        </div>
                                        <div class="col-sm-6 col-md-3 col-lg-6 mb-lg-3">
                                            <p class="h6 heading-semibold text-colour7">Details: </p>
                                            <p class="h6 heading-regular text-colour7" id="spanOrderNotes" runat="server"></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="dvDeliveryTrack row justify-content-md-between mx-lg-n4 my-3" runat="server" id="dvDeliveryTrack">
                <%--<div class="dvLine border d-none d-md-block px-3"></div>--%>
               <%-- <div class="col-6 col-md-auto mb-3 mt-3 my-3">
                    <div class="d-flex flex-column flex-sm-row align-items-center bg-colour6 px-md-1 px-lg-3" runat="server" id="divOrderConfirmed">
                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30 mr-sm-2">1</span>
                        <span class="h6 heading-regular">Order Confirmed</span>
                    </div>
                </div>
                <div class="col-6 col-md-auto mb-3 mt-3 my-3">
                    <div class="d-flex flex-column flex-sm-row align-items-center bg-colour6 px-md-1 px-lg-3" runat="server" id="divOrderPicked">
                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30 mr-sm-2">2</span>
                        <span class="h6 heading-regular">Picked by courier</span>
                    </div>
                </div>
                <div class="col-6 col-md-auto mb-3 mt-md-3 my-3">
                    <div class="d-flex flex-column flex-sm-row align-items-center bg-colour6 px-md-1 px-lg-3" runat="server" id="divOtw">
                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30 mr-sm-2">3</span>
                        <span class="h6 heading-regular">On the way</span>
                    </div>
                </div>
                <div class="col-6 col-md-auto mb-3 mt-md-3 my-3">
                    <div class="d-flex flex-column flex-sm-row align-items-center bg-colour6 px-md-1 px-lg-3" runat="server" id="divDelivered">
                        <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30 mr-sm-2">4</span>
                        <span class="h6 heading-regular">Delivered</span>
                    </div>
                </div>--%>
            </div>
            <%--<div class="dvStepTrack row">
                <div class="dvOrder-steps d-none d-md-block">
                    <div class="step-line"></div>
                    <div class="steps d-flex align-items-center justify-content-between">
                        <div class="dvOrderTrack-step">
                            <span class="step" runat="server" id="divOrderConfirmed">
                                <span class="step-circle future rounded-circle">1</span>
                                <span class="d-block d-md-inline heading-bold">Order confirmed</span>
                            </span>
                        </div>
                        <div class="dvOrderTrack-step">
                            <span class="step" runat="server" id="divOrderPicked">
                                <span class="step-circle future rounded-circle">2</span>
                                <span class="d-block d-md-inline heading-bold">Picked by courier</span>
                            </span>
                        </div>
                        <div class="dvOrderTrack-step">
                            <span class="step" runat="server" id="divOtw">
                                <span class="step-circle future rounded-circle">3</span>
                                <span class="d-block d-md-inline heading-bold">On the way</span>
                            </span>
                        </div>
                        <div class="dvOrderTrack-step">
                            <span class="step" runat="server" id="divDelivered">
                                <span class="step-circle active rounded-circle">4</span>
                                <step class="d-block d-md-inline active stepText heading-bold">Delivered</step>
                            </span>
                        </div>
                    </div>
                </div>
            </div>--%>



            <div class="row">
                <div class="col-12">
                    <div class="bg-colour2 p-3 mt-md-0">
                        <div class="row">
                            <div class="col-12" id="divOrderDetails" runat="server"></div>
                            <div><input type="hidden" id="hdfViewdetailsInfo" value="" runat="server" /></div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>

    <!-- Modal -->
    <div class="dvCommonModal dvOrderDetailsPopup modal fade" id="dvOrderDetailsModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
               <div class="modal-header border-0">
                   <h5 class="modal-title">Details</h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
               </div> 
                <div class="modal-body bg-colour2 p-3">
                    <div id="divDynamicContent"></div>
                </div>
                <!-- <div class="modal-footer">
              <button type="button" class="btn btn-two" data-dismiss="modal">Close</button>
              <button type="button" class="btn btn-one">Save changes</button>
            </div> -->
            </div>
        </div>
    </div>
    <!-- Modal -->

    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#CP_CPSHOP_hdfViewdetailsInfo").val() != "") {
                $("#Viewbutton").show();
            } else {
                $("#Viewbutton").hide();
            }
        });
        function ViewDetails() {
            var lsthtml = $("#CP_CPSHOP_hdfViewdetailsInfo").val();
            if (lsthtml != "") {
                $("#divDynamicContent").html(lsthtml);
            }
            else {
                $("#divDynamicContent").html("Details not found.");
            }
        }
    </script>
</asp:Content>

