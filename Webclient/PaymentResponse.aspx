<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentResponse.aspx.cs" Inherits="PaymentResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
