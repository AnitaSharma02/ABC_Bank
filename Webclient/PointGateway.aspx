<%@ Page Title="Point Gateway" Language="C#" AutoEventWireup="true" CodeFile="PointGateway.aspx.cs" Inherits="PointGateway" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Siddhartha Bank</title>
    <script src="Jquery/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(window).on('load', function () {
            var flag = getParameterByName("flag");
            //Hotel
            if (flag == "Hotel") {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/BookHotel',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "HotelVoucher.aspx";
                        } else {
                            window.location = "BookingFailure.aspx";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "BookingFailure.aspx";
                    }
                });
            }
            //Car
            else if (flag == "Car") {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/BookCar',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "CarVoucher.aspx";
                        } else {
                            window.location = "BookingFailure.aspx";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "BookingFailure.aspx";
                    }
                });
            } else if (flag == "Shop") {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/CheckoutShop',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "/OrderStatus.aspx?Status=true";
                        } else {
                            window.location = "/OrderStatus.aspx?Status=false";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "/OrderStatus.aspx?Status=false";
                    }
                });
            }
            else if (flag == "ShopDigital") {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/PurchaseShopDigital',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "/OrderStatus.aspx?Status=true";
                        } else {
                            window.location = "/OrderStatus.aspx?Status=false";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "/OrderStatus.aspx?Status=false";
                    }
                });
            }
             //Package
            else if (flag == "Package") {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/BookPackage',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "ExperienceProductStatus.aspx?Success=true";
                        } else {
                            window.location = "ExperienceProductStatus.aspx?Success=false";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "ExperienceProductStatus.aspx?Success=false";
                    }
                });
            }
            //INSURANCE
            else if (flag == "Insurance")
            {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/InsurancePaymentRequest',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        //debugger
                        if (msg.d) {
                            window.location = "InsuranceProductStatus.aspx?Success=true";
                        }
                        else {
                            window.location = "InsuranceProductStatus.aspx?Success=false";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "InsuranceProductStatus.aspx?Success=false";
                    }
                });
            }
            //Khalti Air Booking
            else if (flag == "KhaltiAir")
            {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/BookForKhaltiFlight',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "AirReceipt_Domestic.aspx";
                        } else {
                            window.location = "BookingFailure.aspx";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "BookingFailure.aspx";
                    }
                });
            }
            //Khalti ISP
            else if (flag == "ISP")
            {
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/BookForKhaltiISP',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "InsuranceProductStatus.aspx?Success=true";
                        } else {
                            window.location = "InsuranceProductStatus.aspx?Success=false";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "InsuranceProductStatus.aspx?Success=false";
                    }
                });
            }
            else {
                //Flight
                $.ajax({
                    type: 'POST',
                    url: 'PointGateway.aspx/BookFlight',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: "",
                    cache: false,
                    success: function (msg) {
                        if (msg.d) {
                            window.location = "AirReceipt.aspx";
                        } else {
                            window.location = "BookingFailure.aspx";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "BookingFailure.aspx";
                    }
                });
            }
        });
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    </script>
    <style>
        @font-face {
            font-family: 'Conv_tt0009m';
            src: url('../fonts/tt0009m.eot');
            src: local('☺'), url('../fonts/tt0009m.woff') format('woff'), url('../fonts/tt0009m.ttf') format('truetype'), url('../fonts/tt0009m.svg') format('svg');
            font-weight: normal;
            font-style: normal;
        }

        .Search_lbl {
            width: 100%;
            text-align: center;
            font-size: 18px;
            line-height: 34px;
        }

        .SearchImg_Container {
            width: 100%;
            float: left;
            margin: 2% 0 1%;
            text-align: center;
        }
    </style>
</head>
<body style="margin: 0 auto; width: 100%; font-family: 'Conv_tt0009m',Sans-Serif;">
    <form id="form1" runat="server">
        <div style="float: left; width: 100%;">
            <div class="Search_lbl">
                <div class="SearchImg_Container">
                    <asp:Label ID="Label1" runat="server" Text="Please wait while we process your request..."></asp:Label><br />
                    <asp:Label ID="LabelYourSearchDetails" runat="server" Text="Please do not press back or refresh button...."></asp:Label>
                </div>
                <div class="SearchImg_Container">
                    <img alt="" src="Images/ldrlogo.gif" border="0" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
