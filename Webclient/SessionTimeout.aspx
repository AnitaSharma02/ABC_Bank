<%@ Page Title="Session Timeout" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="SessionTimeout.aspx.cs" Inherits="SessionTimeout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" Runat="Server">
    <style>
        .hdr-bg{
            display:none !important;
        } 
           #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;}
    </style>
    <div class="dvSessionTimeout mt-5 pt-5">
    <div class="container-xl">
        <div class="row">
            <div class="col-12 d-flex align-items-center justify-content-center mb-2">
                <span class="h6 heading-semibold text-colour7 mb-5 pb-5">Your session has expired. Please Log in</span>
            </div>
        </div>
    </div>
</div>
</asp:Content>

