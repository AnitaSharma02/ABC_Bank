<%@ Page Title="Search Page" Language="C#" AutoEventWireup="true" CodeFile="SearchPage.aspx.cs" Inherits="SearchPage" %>

<!DOCTYPE html>
<html lang="en" dir="ltr">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width" />
    <title>Welcome to Infinity Rewards</title>
    <script type="text/javascript" src="Jquery/jquery.min.js"></script>
    <script src="Jquery/mobile-detect.min.js" type="text/javascript"></script>
     <!--Language Libs --> 
    <link rel="stylesheet" href="\Css/root.css" /> 
    <script type="text/javascript">
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
            var departure = getQuerystring("departure")
            var arrival = getQuerystring("arrival")
            var departuredate = getQuerystring("departuredate");
            var isReturn = "";
            isReturn = getQuerystring("isReturn");
            var routetype = "";
            if (isReturn == "true") {
                routetype = "Round Trip";
            } else {
                routetype = "One Way";
            }
            var arrivaldate = getQuerystring("arrivaldate");
            if (routetype == "One Way") {
                arrivaldate = "";
            }
            var passengerType = "";
            var adult = getQuerystring("adult");
            if (parseInt(adult) > 0) {
                passengerType = getQuerystring("adult") + " Adult(s) ";
            }
            var child = getQuerystring("child");
            if (parseInt(child) > 0) {
                passengerType += getQuerystring("child") + " Child(ren) ";
            }
            var infant = getQuerystring("infant");
            if (parseInt(infant) > 0) {
                passengerType += getQuerystring("infant") + " Infant(s) ";
            }


            var classType = getQuerystring("economy");
            var searchDetails = "";
            if (arrivaldate == "" || arrivaldate == null) {
                searchDetails = departure + " to " + arrival + ", " + routetype + ", " + classType + ", " + departuredate + ", " + passengerType;
                searchDetails = searchDetails;
            }
            else {
                searchDetails = departure + " to " + arrival + ", " + routetype + ", " + classType + ", " + departuredate + " - " + arrivaldate + ", " + passengerType;
                searchDetails = searchDetails.replace('%20', ' ');
            }

            $("#LabelYourSearchDetails").text(decodeURIComponent(searchDetails));
            $.ajax({
                type: 'POST',
                url: document.URL.toString().replace("SearchPage.aspx", "SearchPage.aspx/FlightSearch"),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d)
                        window.location = "FlightList.aspx";
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
            line-height: 21px !important;
            color: #000 !important;
        }
    </style>
</head>
<body style="margin: 0 auto; width: 100%;font-family: var(--heading-semibold);background: #fff;">
    <form id="form1" runat="server">
        <div style="float: left; width: 100%;"> 
            <div class="SearchImg_Container" style="margin: 60px 0 30px 0;">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logos/infinity-logo.svg" Width="240" />
            </div>
            <div class="Search_lbl" >
                <div class="SearchImg_Container">
                    <span data-i18n="flight-search-page" class="spclpadd h6">Please wait while we search for best available flights...</span>
                </div>
                <asp:Label ID="LabelYourSearchDetails"  Font-Bold="true" runat="server" Text="" CssClass="spclpadd h6"></asp:Label>

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
