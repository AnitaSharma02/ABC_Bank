<%@ Page Title="Hotels Search Wait" Language="C#" AutoEventWireup="true" CodeFile="HotelsSearchWait.aspx.cs" Inherits="HotelsSearchWait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Welcome to Siddhartha Bank</title>
    <script src="Jquery/jquery.min.js" type="text/javascript"></script>
     <script src="Jquery/purify.min.js" type="text/javascript"></script>
     <link rel="stylesheet" href="\Css/root.css" /> 
       
    <script type="text/javascript" language="javascript">
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
        $(document).ready(function () {

            var strCity1 = getQuerystring("city");
            var strCity = DOMPurify.sanitize(strCity1, { SAFE_FOR_TEMPLATES: true });
            var strCheckIn1 = getQuerystring("checkin");
            var strCheckIn = DOMPurify.sanitize(strCheckIn1, { SAFE_FOR_TEMPLATES: true });
            var strCheckOut1 = getQuerystring("checkout");
            var strCheckOut = DOMPurify.sanitize(strCheckOut1, { SAFE_FOR_TEMPLATES: true });
            var strRoomString1 = getQuerystring("roomstring");
            var strRoomString = DOMPurify.sanitize(strRoomString1, { SAFE_FOR_TEMPLATES: true });
            var strisRedeemMiles1 = getQuerystring("isRedeemMiles");
            var strisRedeemMiles = DOMPurify.sanitize(strisRedeemMiles1, { SAFE_FOR_TEMPLATES: true });
            var strRating = getQuerystring("Rating");
            var SearchDetails = strCity + ", " + strCheckIn + " to " + strCheckOut + ".";
            SearchDetails = SearchDetails.replace(/\%20/g, ' ');
            $("#LabelYourSearchDetails").text(SearchDetails);
            $.ajax({
                type: 'POST',
                url: document.URL.toString().replace("HotelsSearchWait.aspx", "HotelsSearchWait.aspx/GetHotelSearchResponse"),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                timeout: 80000,
                data: "{'pstrCity':'" + strCity + "', 'pCheckIn':'" + strCheckIn + "', 'pCheckOut':'" + strCheckOut + "', 'pRoomString':'" + strRoomString + "', 'pisRedeemMiles':'" + strisRedeemMiles + "', 'strRating':'" + strRating + "'}",
                success: function (msg) {
                    alert(msg.d);
                    if (msg.d)
                        window.location = "HotelResults.aspx";
                    else
                        window.location = "NoResultFound.aspx?ERR=RESULTNOTFOUND";


                },
                error: function (jqXHR, status, errorThrown) {
                    window.location = "ErrorPage.aspx";

                }
            });
        });

    </script>
    <style>
            
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
            float: left;
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
</head>
<body style="margin: 0 auto; width: 100%; text-align:center;overflow-x:hidden;background: #fff;">
    <form id="form1" runat="server">
        <div style="float: left; width: 100%;">
                <div class="SearchImg_Container" style="margin:60px 0 30px 0;">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logos/infinity-logo.svg" Width="240"  />
                </div>
                <div class="SearchImg_Container spclpadd">
                    <asp:Label ID="Label1" runat="server" CssClass="spclpadd h6" Text="Please wait while  we search for best available rates..."></asp:Label>
                    <%--<span data-i18n="flight-search-page" class="spclpadd">Please wait while we search for best available flights...</span>--%>
                </div>
                <asp:Label ID="LabelYourSearchDetails" Font-Bold="true" runat="server" Text="" CssClass="spclpadd h6"></asp:Label>
                <div class="cssload-wrapper">
                <div class="cssload-border">
                    <div class="cssload-whitespace">
                        <div class="cssload-line">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
