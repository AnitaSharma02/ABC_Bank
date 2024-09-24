<%@ Page Title="Search Page for Domestic" Language="C#" AutoEventWireup="true" CodeFile="SearchPageDomestic.aspx.cs" Inherits="SearchPageDomestic" %>

<!DOCTYPE html>
<html lang="en" dir="ltr">
<head runat="server">
     <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width" />
    <title>Welcome to Siddhartha Bank</title>
    <script type="text/javascript" src="Jquery/jquery.min.js"></script>
    <script src="Jquery/mobile-detect.min.js" type="text/javascript"></script>
     <!--Language Libs -->
    <script src="Jquery/i18n/jquery.i18n.min.js" type="text/javascript"></script>
    <script src="Jquery/i18n/jquery.i18n.emitter.bidi.js" type="text/javascript"></script>
    <script src="Jquery/i18n/i18n.custom.js" type="text/javascript"></script>
     <link rel="preconnect" href="https://fonts.googleapis.com">
 <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
 <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700;900&display=swap" rel="stylesheet">
    <link href="Css/root.css" rel="stylesheet" />
    <link href="Css/global.css" rel="stylesheet" />
     <style>
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
            font-family: 'Roboto', sans-serif !important;
            font-style: normal !important;
            font-weight: 700 !important;
            font-size: 17px !important;
            line-height: 21px !important;
            color: #000 !important;
        }

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
            var departure = getQuerystring("DepCity")
            var arrival = getQuerystring("arrCity")
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
            var searchDetails = "";
            if (arrivaldate == "" || arrivaldate == null) {
                searchDetails = departure + " to " + arrival + ", " + routetype + ", " + formatDate(departuredate) + ", " + passengerType;
                searchDetails = searchDetails;
            }
            else {
                searchDetails = departure + " to " + arrival + ", " + routetype + ", " + formatDate(departuredate) + " - " + formatDate(arrivaldate) + ", " + passengerType;
                searchDetails = searchDetails.replace('%20', ' ');
            }
            function formatDate(input) {
                var datePart = input.match(/\d+/g),
                    year = datePart[0], // get 4 digits
                    month = datePart[1],
                    day = datePart[2];

                return day + '/' + month + '/' + year;
            }
            $("#LabelYourSearchDetailsforDomestic").text(decodeURIComponent(searchDetails));
            $.ajax({
                type: 'POST',
                url: document.URL.toString().replace("SearchPageDomestic.aspx", "SearchPageDomestic.aspx/FlightSearchforDomestic"),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d)
                        window.location = "FlightListForDomestic.aspx";
                    else
                        window.location = "NoResultFound.aspx?ERR=RESULTNOTFOUND";
                },
                error: function (jqXHR, status, errorThrown) {
                    window.location = "ErrorPage.aspx";
                }
            });
        });
      </script>
</head>
<body style="margin: 0 auto; width: 100%;  font-family: 'Roboto', sans-serif, sans-serif;background: #fff;">
    <form id="form1" runat="server">
        <div style="float: left; width: 100%;"> 
            <div class="SearchImg_Container" style="margin: 60px 0 30px 0;">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logos/infinity-logo.svg" Width="240" />
            </div>
            <div class="Search_lbl" >
                <div class="SearchImg_Container">
                    <span class="spclpadd">Please wait while we search for best available flights...</span>
                </div>
                <asp:Label ID="LabelYourSearchDetailsforDomestic"  Font-Size="16px" Font-Bold="true" runat="server" Text="" CssClass="spclpadd"></asp:Label>

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
