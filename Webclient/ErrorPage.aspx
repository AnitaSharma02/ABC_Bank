<%@ Page Title="Error Page" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
      #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;} 
        .errPg {
            margin-top: 12%;
            min-height: 320px;
            text-align: center;
            width: 100%;
        }
        @media (max-width: 440px) {
            .errPg {
                margin-top: 50%;
                min-height: auto;
            }
        }
    </style>

    <div class="errPg">
        Sorry we could not process your request...<br />
        Please <a class="link1" href="Index.aspx">click here</a> to try again..!!
    </div>
</asp:Content>
