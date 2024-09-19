<%@ Page Title="No Result Found" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="NoResultFound.aspx.cs" Inherits="NoResultFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>
    <div class="dvNoResultFound py-5 text-center">
        <div class="container-xl">
            <div class="row">
                <div runat="server" id="divBOOKINGFAILED" class="col-12" style="display: none;">
                    <p class="h5 heading-regular text-colour7">
                        "Sorry we could not get confirmation PNR OR TicketNumber From Airline
                        <asp:LinkButton ID="btnRedirect" runat="server" CssClass="link1" Text="Please try booking again" PostBackUrl="~/Index.aspx" />
                    </p>
                </div>
                <div runat="server" id="divRESULTNOTFOUND" class="col-12">
                    <p class="h5 heading-regular text-colour7">
                        Infinity Rewards could not process your request.
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link1" Text="Please try again" PostBackUrl="~/Index.aspx" />
                        after some time. Thank you.
                    </p>
                </div>
                <div runat="server" id="divHotelNoresult" class="col-12">
                    <p class="h5 heading-regular text-colour7">
                        Infinity Rewards could not process your request.
                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="link1" Text="Please try again" PostBackUrl="~/Index.aspx" />
                        after some time. Thank you.
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
