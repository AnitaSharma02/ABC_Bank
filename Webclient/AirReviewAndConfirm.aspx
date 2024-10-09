<%@ Page Title="Air Review And Confirm" Language="C#" AutoEventWireup="true" CodeFile="AirReviewAndConfirm.aspx.cs"
    Inherits="AirReviewAndConfirm" MasterPageFile="~/SiteMaster.master" %>

<%@ Register Src="~/UserControl/UCCTItinerayDetails.ascx" TagName="ItineraryDetails"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/flight.css" />
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#errorDiv").hide();
            PaintCounts();
        });
        function PaintCounts() {
            $.ajax({
                type: 'POST',
                url: 'FlightList.aspx/FlightData',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    var Counts = msg.d[0];
                    // $("#CP_lblNoofFlight").text("> " + Counts.toString() + " of " + Counts.toString() + " Flights");
                    var lobjSearchRequest = $.parseJSON(msg.d[1]);
                    var searchdetails = "";
                    var DepartDate = msg.d[2];
                    var ArrivalDate = msg.d[3];
                }
            });
        };
        function onAcceptArgument() {
            var msg = "";
            if ((document.getElementById('CP_chkAcceptAgreements').checked == true) && (document.getElementById('CP_chkAcceptPayMiles').checked == true)) {
                $("#lblBookNow").hide();
                return true;
            }
            //if (document.getElementById('CP_chkAcceptAgreements').checked == false) {
            //    if (document.getElementById('CP_chkAcceptPayMiles').checked == false) {
            //        msg = "Accept Terms And Conditions and agree to pay Points";
            //    }
            //}
            //else {

            //}
            if (document.getElementById('CP_chkAcceptAgreements').checked == false) {
                msg += "Accept Terms And Conditions" + "<br/>";
            }
            if (document.getElementById('CP_chkAcceptPayMiles').checked == false) {
                msg += "Accept & agree to pay Points" + "<br/>";
            }
            if (msg.length > 0) {
                $("#errorDiv")[0].innerHTML = msg;
                $("#errorDiv").show();
                return false;
            }
        }

    </script>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active"><a href="FlightPassenger.aspx">Flight Passenger</a></li>
                    <li class="breadcrumb-item active">Air Review And Confirm</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvFlightAirReview py-2 py-md-5 ">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <div class="vouch-main border mb-5">
                        <div class="">
                            <h2 class="h6 heading-bold text-colour6 bg-colour1 p-3">Review & Confirm</h2>
                        </div>

                        <div class="airBox" id="innerPageAboutUs">
                            <div class="">
                                <!--StepsPage-->
                                <div class="container-xl">
                                    <div id="divError" runat="server" class="dvErrors ErrorMsgContainer text-capitalise">
                                        <asp:Label ID="lblError" CssClass="red-text" runat="server"></asp:Label>
                                    </div>
                                    <uc:ItineraryDetails ID="ucItinarary" runat="server" />

                                    <div class="row mt-3 dvLabel">
                                        <div class="col-12 mb-1">
                                            <label class="checkbox-container d-flex">
                                                <span class="d-inline-block ml-1">
                                                    <input id="chkAcceptAgreements" type="checkbox" value="rewards Points" runat="server" />
                                                    <span>I have read and agree to Infinity Rewards</span> <a class="link1" href="TermsandConditions.aspx" target="_blank">Terms & Conditions </a><span>and the </span><a href="BookingPolicy.aspx" target="_blank">Booking & Cancellation policy</a>
                                                    <span>of the respective service provider.</span>
                                                    <span class="checkmark"></span>
                                                </span>
                                            </label>
                                        </div>
                                        <div class="col-12">
                                            <label class="checkbox-container d-flex">
                                                <span class="d-inline-block ml-1">
                                                    <input id="chkAcceptPayMiles" type="checkbox" value="rewards Points" runat="server" />
                                                    <span>I agree to redeem</span>
                                                    <asp:Label ID="lblTotalPoints" runat="server" />
                                                    <span>Points. I also understand and accept that the redeemed Infinity Rewards Points cannot be refunded or credited upon Cancellation of a flight booking.</span>
                                                    <span class="checkmark"></span>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="text-danger h6 heading-regular" id="errorDiv">
                                    </div>
                                    <br />
                                    <div class="d-flex flex-row justify-content-end mb-2 text-right">
                                        <div class="mr-2">
                                            <asp:Button ID="btnBack" CssClass="btn btn-two" runat="server" OnClientClick="var retvalue = redirectLocation('FlightList.aspx'); event.returnValue= retvalue;event.preventDefault(); return retvalue;"
                                                Text="Back" />
                                        </div>
                                        <div id="lblBookNow">
                                            <asp:Button ID="btnBookNow" CssClass="btn btn-one" runat="server" OnClientClick="return onAcceptArgument();"
                                                OnClick="btnBookNow_Click" Text="Proceed to Pay" />
                                        </div>
                                    </div>
                                </div>
                                <!--StepsPage-->
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
