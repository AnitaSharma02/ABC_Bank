<%@ Page Title="Status" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="InsuranceProductStatus.aspx.cs" Inherits="InsuranceProductStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" Runat="Server">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>

    <div class="dvOrderStatus d-flex flex-wrap justify-content-center align-items-center vh-center- pb-5 pt-3 pt-lg-5">
        <div class="col-sm-8 text-center">
            <div class="bg-colour2 p-5">
                <div id="divMessage" class="dvstatus" runat="server"></div>
            </div>
        </div>
    </div>

</asp:Content>

