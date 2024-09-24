<%@ Page Title="Car Search" Language="C#" AutoEventWireup="true" CodeFile="CarSearchWait.aspx.cs" Inherits="CarSearchWait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Welcome to GIM Rewards</title>
    <script src="Jquery/jquery.min.js" type="text/javascript"></script>
    <script src="Jquery/purify.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="\Css/root.css" />
    <link rel="stylesheet" href="\Css/global.css" />
    <style type="text/css">
        .cssload-wrapper {
            width: 50%;
            height: 6px;
            margin: 0 auto;
            margin-top: 40px;
            text-align: center;
        }

        .cssload-border {
            background: #ddd none repeat scroll 0 0;
            height: 40%;
            padding: 0;
            position: relative;
            width: 100%;
            clear: both;
            top: 30px;
        }

        .cssload-whitespace {
            overflow: hidden;
            height: 100%;
            width: 100%;
            margin: 0 auto;
            overflow: hidden;
            position: relative;
        }

        .martop {
            margin-top: 2em
        }

        .cssload-line {
            position: absolute;
            height: 100%;
            width: 100%;
            background-color: var(--bg-colour1);
            animation: cssload-slide 5.75s steps(40) infinite;
            -o-animation: cssload-slide 5.75s steps(40) infinite;
            -ms-animation: cssload-slide 5.75s steps(40) infinite;
            -webkit-animation: cssload-slide 5.75s steps(40) infinite;
            -moz-animation: cssload-slide 5.75s steps(40) infinite;
        }

        @keyframes cssload-slide {
            0% {
                left: -100%;
            }

            100% {
                left: 100%;
            }
        }

        @-o-keyframes cssload-slide {
            0% {
                left: -100%;
            }

            100% {
                left: 100%;
            }
        }

        @-ms-keyframes cssload-slide {
            0% {
                left: -100%;
            }

            100% {
                left: 100%;
            }
        }

        @-webkit-keyframes cssload-slide {
            0% {
                left: -100%;
            }

            100% {
                left: 100%;
            }
        }

        @-moz-keyframes cssload-slide {
            0% {
                left: -100%;
            }

            100% {
                left: 100%;
            }
        }

        .Search_lbl {
            width: 100%;
            text-align: center;
            font-size: 18px;
        }

        .SearchImg_Container {
            width: 100%;
            
            margin: 0% 0% 10px 0%;
            text-align: center;
        }

        .containerBG {
            margin-bottom: 3%;
        }

        .cssload-wrapper {
            margin-top: 0px;
        }

        .spclpadd {
            padding: 0 7px;
            font-family: var(--heading-semibold);
            font-style: normal !important;
            font-weight: 700 !important;
            font-size: 17px !important;
            line-height: 21px !important;
            color: #000 !important;
        }

        #Label1 {
            padding: 0 7px;
            font-family: var(--heading-semibold);
            font-style: normal !important;
            font-weight: 700 !important;
            font-size: 17px !important;
            line-height: 21px !important;
            color: #000 !important;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.search);
            if (results == null)
                return "";
            else
                var qs = regex.exec(decodeURIComponent(window.location.href));
        }
        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(decodeURIComponent(window.location.href));
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

        $(document).ready(function () {

            var PickupLocation = getQuerystring("PickupLocation");
            PickupLocation = PickupLocation.replace('%20', ' ');
            PickupLocation = PickupLocation.replace('%20', ' ');

            var PickupLocationId = getQuerystring("PickupLocationId");
            PickupLocationId = PickupLocationId.replace('%20', ' ');
            PickupLocationId = PickupLocationId.replace('%20', ' ');

            var DropoffLocation = getQuerystring("DropoffLocation");
            DropoffLocation = DropoffLocation.replace('%20', ' ');
            DropoffLocation = DropoffLocation.replace('%20', ' ');

            var DropoffLocationId = getQuerystring("DropoffLocationId");
            DropoffLocationId = DropoffLocationId.replace('%20', ' ');
            DropoffLocationId = DropoffLocationId.replace('%20', ' ');

            var PickupDate = getQuerystring("PickupDate");
            PickupDate = PickupDate.replace('%20', '');

            var PickupTime = getQuerystring("PickupTime");
            PickupTime = PickupTime.replace('%20', ' ');

            var DropoffDate = getQuerystring("DropoffDate");
            DropoffDate = DropoffDate.replace('%20', '');

            var DropoffTime = getQuerystring("DropoffTime");
            DropoffTime = DropoffTime.replace('%20', ' ');

            var DiscountCode = getQuerystring("DiscountCode");
            DiscountCode = DiscountCode.replace('%20', ' ');
            DiscountCode = DiscountCode.replace('%20', ' ');

            var DriverAge = getQuerystring("DriverAge");
            DriverAge = DriverAge.replace('%20', ' ');
            DriverAge = DriverAge.replace('%20', ' ');

           

            var SearchDetails = PickupLocation.replace('+', ' ') + ' To ' + DropoffLocation.replace('+', ' ') + ', ' + PickupDate + ', ' + PickupTime + ' - ' + DropoffDate + ', ' + DropoffTime;
          
            $("#LabelYourSearchDetails").text(SearchDetails.trim());

            $.ajax({
                type: 'POST',
                url: document.URL.toString().replace("CarSearchWait.aspx", "CarSearchWait.aspx/CarSearch"),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: '',
                success: function (msg) {
                    if (msg.d)
                        window.location = "CarList.aspx";
                    else
                        window.location = "NoResultFound.aspx?ERR=RESULTNOTFOUND";
                },
                error: function () {
                    window.location = "ErrorPage.aspx";
                }
            });
        });

    </script>
</head>
<body style="overflow:hidden">
    <form id="form1" runat="server">
        <div class="vh-center d-flex justify-content-center align-items-center">
            <div class="SearchImg_Container" style="margin: 60px 0 30px 0;">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logos/infinity-logo.svg" Width="240" />
            </div>
            <div class="Search_lbl">
                <div class="SearchImg_Container spclpadd">
                    <asp:Label ID="Label1" runat="server" CssClass="spclpadd h6" Text="Please wait while  we search for best available rates..."></asp:Label>
                    <%--<span class="spclpadd">Please wait while we search for best available flights...</span>--%>
                </div>
                <asp:Label ID="LabelYourSearchDetails" runat="server" Font-Size="16px" Font-Bold="true" Text="" CssClass="spclpadd"></asp:Label>
                <div class="cssload-wrapper">
                    <div class="cssload-border">
                        <div class="cssload-whitespace">
                            <div class="cssload-line">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
