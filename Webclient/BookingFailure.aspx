<%@ Page Title="Booking Failure" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="BookingFailure.aspx.cs" Inherits="BookingFailure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(window).on('load', function () {
            $.ajax({
                type: 'POST',
                url: 'BookingFailure.aspx/BookingFail',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                cache: false,
                success: function (msg) {
                    if (msg.d != "") {
                        $("#<%=lblTransactionReferenceCode.ClientID%>").text(msg.d);
                        }
                        else {
                            window.location = "Index.aspx";
                        }
                    },
                    error: function (errmsg) {
                        window.location = "Index.aspx";
                    }
                });
            });
    </script>

    <div class="dvOrderStatus d-flex flex-wrap justify-content-center align-items-center vh-center- pb-5 pt-3 pt-lg-5">
        <div class="col-sm-8 text-center" runat="server">
            <div class="bg-colour2 p-5">
                <h2 class="h6 heading-regular text-center mb-3">We could not process your request, Please <a href="Index.aspx" class="link1" target="_self">try again</a>.</h2>
                <h2 class="h6 heading-regular text-center">Your Transaction Reference Code:
                    <asp:Label ID="lblTransactionReferenceCode" runat="server" Text=""></asp:Label></h2>
            </div>
        </div>
    </div>

</asp:Content>

