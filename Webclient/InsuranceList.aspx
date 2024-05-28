<%@ Page Title="Insurance List" Language="C#" MasterPageFile="SiteMaster.master" AutoEventWireup="true" CodeFile="InsuranceList.aspx.cs" Inherits="InsuranceList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="css/isp.css">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>

    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item active" data-i18n="navigation-insurance">Insurance</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="dvInsuranceList">
        <div class="container-lg">
            <div id="dvResult" class="dvProducts products"></div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            GetProductList(1);
        });
        function GetProductList(PageNo) {
            $.ajax({
                type: 'POST',
                url: 'InsuranceList.aspx/GetProductList',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{PageNo:" + PageNo + "}",
                success: function (msg) {
                    if (msg.d != '') {
                        var data = msg.d.split('||');
                        $("#dvResult").html(data[0]);
                        $("#spnTotalCount").empty().html(data[1]);
                    }
                    return false;
                },
                beforeSend: function () {
                    $("#updProgress").show();
                },
                error: function (errmsg) {
                }
            })
            return false;
        }
    </script>
</asp:Content>