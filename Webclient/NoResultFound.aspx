<%@ Page Title="No Result Found" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="NoResultFound.aspx.cs" Inherits="NoResultFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
   <style>
    #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;}
 </style>
    <div style="min-height:380px; width: 100%; margin-top: 50px; padding:0 15px">
        <div runat="server" id="divBOOKINGFAILED" class="h5 heading-regular text-colour7" style="border: none; text-align: center; width: 100%; display: none;">
            "Sorry we could not get confirmation PNR OR TicketNumber From Airline
            <br />
            <asp:LinkButton ID="btnRedirect" runat="server" CssClass="link1" Text="Please try booking again" PostBackUrl="~/Index.aspx" />
        </div>
        <div runat="server" id="divRESULTNOTFOUND" class="h5 heading-regular text-colour7" style="border: none; text-align: center; width: 100%; display: block;">
            Infinity Rewards could not process your request.
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link1" Text="Please try again" PostBackUrl="~/Index.aspx" />
            after some time. Thank you.
        </div>
        <div runat="server" id="divHotelNoresult" class="h5 heading-regular text-colour7" style="border: none; text-align: center; width: 100%; display: block;">
            Infinity Rewards could not process your request.
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="link1" Text="Please try again" PostBackUrl="~/Index.aspx" />
            after some time. Thank you.
        </div>
    </div>
</asp:Content>
