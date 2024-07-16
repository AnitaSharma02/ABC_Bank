<%@ Page Title="Refresh Cache" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="RefreshCache.aspx.cs" Inherits="RefreshCache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner, .breadcrumbBox {
            display: none;
        }
    </style>

    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active">Refresh Cache</li>
                </ul>
            </nav>
        </div>
    </div>
    <div id="inner-page-area" class="dvLoginBoxbg my-0 my-md-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-sm-6 offset-sm-3">
                    <div class="innerBox p-4">
                        <div id="siteMap" class="bread-crumb" style="display: none">
                            <asp:SiteMapPath ID="SiteMapPath1" runat="server" Visible="true" CssClass="SiteMap_Root_Stlye"
                                PathSeparator=">" PathSeparatorStyle-CssClass="PathSeparatorStyle">
                                <NodeStyle />
                                <RootNodeStyle />
                            </asp:SiteMapPath>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <h2 class="h5 heading-semibold text-colour7 mb-1" data-i18n="refresh-cache">Refresh Cache </h2>
                            </div>
                            <div class="col-12">
                                <asp:Label runat="server" ID="lblLoginError" Text="" CssClass="mt-3 h7 heading-regular text-colour7"></asp:Label>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Label ID="lblCache" CssClass="label" runat="server" Text="Cache"></asp:Label>
                                <div class="dvInput input-group">
                                    <asp:DropDownList ID="ddlCache" runat="server" class="form-control">
                                        <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="RefererSupplier" Value="RefererSupplier"></asp:ListItem>
                                        <asp:ListItem Text="ProgramMaster" Value="ProgramMaster"></asp:ListItem>
                                        <asp:ListItem Text="HotelCities" Value="HotelCities"></asp:ListItem>
                                        <asp:ListItem Text="Store" Value="Store"></asp:ListItem>
                                        <asp:ListItem Text="Insurance" Value="Insurance"></asp:ListItem>
                                        <asp:ListItem Text="ISP" Value="ISP"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Label ID="Label1" CssClass="label" runat="server" Text="UserName"></asp:Label>
                                <div class="dvInput input-group">
                                    <asp:TextBox ID="txtUN" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Label ID="Label2" CssClass="label" runat="server" Text="Password"></asp:Label>
                                <div class="dvInput input-group">
                                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12 mt-4" id="LoginDiv">
                                <asp:Button runat="server" ID="btnSubmit" Text="Continue" OnClick="btnSubmit_Click" CssClass="btn btn-one w-100" />
                            </div>
                        </div>
                        <div style="display: none;" id="overlay"></div>
                        <div style="display: none;" id="popup">
                            <img src="images/loadingPage.gif" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

