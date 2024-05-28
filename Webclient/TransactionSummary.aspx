<%@ Page Title="Transaction Summary" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="TransactionSummary.aspx.cs" Inherits="TransactionSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="\Css/MyAccount.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />--%>
    <script src="Jquery/Validation.js" type="text/javascript"></script>
    <script type="text/javascript">
        $.fn.digits = function () {
            return this.each(function () {
                $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
        $(document).ready(function () {
            $(".FromDate").datepicker("option", { disabled: true });
            $(".Todate").datepicker("option", { disabled: true });
            $('#spnMemberName').empty().html($('.uName').html());
            $(".milesPoint").digits();
            $(".CommaSeperated").each(function () {

                if ($(this).html() != "") {
                    $(this).html(ConvertThousandSeparator($(this).html()))
                }
            });
            if (!$("#CP_RdlSearchtypeCust").prop("checked")) {
                BindTransactionDetails(0);
            }
        });
        function placeholderOnFocus(obj, defaultVal) {
            if (obj.value == "") {
                obj.value = defaultVal;
            } else if (obj.value == defaultVal) {
                obj.value = "";
            } else { }
            $("#" + obj.id).removeClass('error');
        };
        $(function () {
            $(".FromDate").datepicker({
                numberOfMonths: 1,
                showButtonPanel: false,
                nextText: '',
                dateFormat: 'dd/mm/yy',
                maxDate: new Date,
                onSelect: function (dateText, inst) {
                    $(".Todate").val('');
                    $(".Todate").datepicker("destroy");
                    $(".Todate").datepicker({
                        minDate: dateText,
                        maxDate: new Date,
                        numberOfMonths: 1,
                        showButtonPanel: false,
                        prevText: '',
                        dateFormat: 'dd/mm/yy',
                        nextText: ''
                    });
                }
            });
            //show datepicker onclick of icon
            $(".dvCP_FromDate .input-group-append .input-group-text").on("click", function () {
                $("#CP_FromDate").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvCP_Todate .input-group-append .input-group-text").on("click", function () {
                $("#CP_Todate").datepicker("show");
            });

        });
        function ConvertThousandSeparator(val) {
            return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }

        function BindTransactionDetails(PageNo) {
            $.ajax({
                type: 'POST',
                url: 'TransactionSummary.aspx/BindTransactionDetails',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{skip:" + PageNo + ",FromDate:'" + $("#CP_FromDate").val() + "',Todate:'" + $("#CP_Todate").val() + "'}",
                success: function (msg) {
                    if (msg.d != '') {
                        $("#divtransactionSummaryData").html(msg.d);
                        return false;
                    }
                },
                beforeSend: function () {
                    $("#updProgress").show();
                },
                error: function (err) {
                    return false;
                }
            })
        }

        function btnCustomSearch() {
            if (($("#CP_FromDate").val() != "" && $("#CP_Todate").val() != "") && ($("#CP_FromDate").val() != "Enter Date" && $("#CP_Todate").val() != "Enter Date"))
            {
                BindTransactionDetails(0);
            }
        }

        function RdlSearchtypeAll_CheckedChanged() {
            // Check #x
            $("#RdlSearchtypeAll").prop("checked", true);
            // Uncheck #x
            $("#RdlSearchtypeCust").prop("checked", false);
            $("#CP_tblsearchinfo").attr("style", "display:none");
            $("#CP_FromDate").val("");
            $("#CP_Todate").val("");
            BindTransactionDetails(0);
        }
        function RdlSearchtypeCust_CheckedChanged() {
            // Check #x
            $("#RdlSearchtypeCust").prop("checked", true);
            // Uncheck #x
            $("#RdlSearchtypeAll").prop("checked", false);
            $("#CP_tblsearchinfo").attr("style", "display:block");
            $("#divtransactionSummaryData").html("");
        }
    </script>
    <style>
        #dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        .dvShopMenu{
            display: none;
        }
    </style>

    <%--<div class="dvMember">
        <div class="d-md-block d-none">
            <div class="align-items-center bg-acc d-flex justify-content-center">
                <div class="d-flex justify-content-center align-items-center flex-column">
                    <ul>
                        <li class="d-block">
                            <div class="text-center">
                                <h2 class="h3 heading-semibold text-white" id="lblMemberName"><span data-i18n="account-welcome" class="acc-text">Welcome,</span><span class="ml-2 acc-text" id="spnMemberName"></span></h2>
                            </div>
                        </li>
                        <li class="d-block">
                            <div class="text-center">
                                <h3 class="h3 heading-semibold text-white">
                                    <span id="totAvbPointDiv" data-i18n="account-total-points">Total NPoints</span>
                                    <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-white"></span>
                                </h3>
                            </div>
                        </li>
                    </ul>
                    <div class="mt-3">
                        <a
                            href="Index.aspx"
                            class="btn btn-two"
                            id="my_account_point_redeem_now"
                            data-i18n="btn-redeem-now">Redeem Now</a>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="dvMember d-md-block d-none py-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12 text-center">
                    <h2 class="h1 heading-light text-colour1" id="lblMemberName">
                        <span data-i18n="account-welcome" class="">Welcome,</span>
                        <span class="ml-2" id="spnMemberName"></span>
                    </h2>
                    <h2 class="h5 heading-bold text-colour1 mt-2 mb-3">
                        <span id="totAvbPointDiv">Total NPoints</span>
                        <span id="spnMemberCurrentBal" class="ml-2 heading-bold text-colour1">0</span>
                    </h2>
                    <a
                        href="Index.aspx"
                        class="btn btn-one"
                        id="my_account_point_redeem_now"
                        data-i18n="btn-redeem-now">Redeem Now
                    </a>
                </div>
            </div>
        </div>
    </div>

    <div class="dvAccountMenu">
        <div class="container-lg">
            <div class="row equal-col my-3" id="AccMenu">
            </div>
        </div>
    </div>

    <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-4">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item" data-i18n="bread-my-account">My Account</li>
                    <li class="breadcrumb-item active" data-i18n="bread-transaction">Transaction Summary</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvTransactionSummary pb-5">
        <div class="container-lg">
            <div class="row">
                <div class="col-12">
                    <div class="bg-lightgray rounded px-3">
                        <%--   <div id="divCurrency" class="myacc-user" runat="server" visible="false">
                            Select Currency :
                            <asp:DropDownList ID="ddlProgramCurrency" runat="server" AppendDataBoundItems="true"
                                OnSelectedIndexChanged="ddlCurrencyName_SelectedIndexChanged" AutoPostBack="true"
                                Style="padding-top: 0.1vw; padding-bottom: 0.1vw; margin-top: 1vw; margin-bottom: 0.7vw; color: #bbbcbe" CssClass="form-control">
                            </asp:DropDownList>
                        </div>--%>
                        <div class="dvRadios row mb-3 pt-3">
                            <div class="col-6 col-sm-4 col-md-3 col-xl-2 pr-1 pr-sm-3 d-sm-flex">
                                <div class="dvLabel d-flex justify-content-between my-sm-auto">
                                    <label class="radio-container d-flex">
                                        <span class="d-inline-block">
                                           <%-- <asp:RadioButton runat="server" ID="RdlSearchtypeAll" GroupName="RdlSearchtype" AutoPostBack="true"
                                                OnCheckedChanged="RdlSearchtypeAll_CheckedChanged" Checked="true" />--%>
                                            <input type="radio" id="RdlSearchtypeAll" onclick="RdlSearchtypeAll_CheckedChanged()" checked="checked"/>
                                            <span class="radiomark"></span>
                                        </span>
                                        <span class="d-inline-block ml-3 heading-light" data-i18n="ts-all-transaction">All Transactions</span>
                                    </label>
                                </div>
                            </div>
                            <div class="col-6 col-sm-4 col-md-3 col-xl-2 pl-1 pl-sm-3 d-sm-flex">
                                <div class="dvLabel d-flex justify-content-between my-sm-auto">
                                    <label class="radio-container d-flex">
                                        <span class="d-inline-block">
                                           <%-- <asp:RadioButton runat="server" ID="RdlSearchtypeCust" GroupName="RdlSearchtype"
                                                AutoPostBack="true" OnCheckedChanged="RdlSearchtypeCust_CheckedChanged" />--%>
                                            <input type="radio" id="RdlSearchtypeCust" onclick="RdlSearchtypeCust_CheckedChanged()"/>
                                            <span class="radiomark"></span>
                                        </span>
                                        <span class="d-inline-block ml-3 heading-light" data-i18n="ts-custom-search">Custom Search</span>
                                    </label>
                                </div>
                            </div>
                            <div class="col-12 col-sm-4 offset-md-3 col-md-3 offset-xl-5 mt-3 mt-sm-0 d-sm-flex">
                                <asp:Button ID="btnExport" runat="server" Text="Download" data-i18n="[value]ts-download" OnClick="btnExport_Click" class="btn btn-one w-100 m-sm-auto" />
                            </div>
                        </div>
                        <div id="tblsearchinfo" runat="server" class="dvCustomSearch" style="display: none">
                            <div class="row">
                                <div class="col-12 col-sm-6 col-md-4 mb-3">
                                    <label class="heading-regular" data-i18n="ts-label-from">From</label>
                                    <div class="dvCP_FromDate input-group">
                                        <asp:TextBox ID="FromDate" runat="server" CssClass="FromDate form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" Text="Enter Date" readonly="true"/>
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FromDate" ErrorMessage="Enter From Date<br/>" Style="color: #ff0000" Display="Dynamic" ValidationGroup="DateSearch"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-sm-6 col-md-4 mb-3">
                                    <label class="heading-regular" data-i18n="ts-label-to">To</label>
                                    <div class="dvCP_Todate input-group">
                                        <asp:TextBox ID="Todate" runat="server" CssClass="Todate form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" Text="Enter Date" readonly="true"/>
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span>
                                        </div>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Todate" ErrorMessage="Enter To Date." Style="color: #ff0000" Display="Dynamic" ValidationGroup="DateSearch"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 col-md-4 mb-3">
                                    <label class="heading-regular d-none d-md-block invisible" data-i18n="ts-label-to">button</label>
                                    <%-- <asp:Button ID="btnSearch" runat="server" ValidationGroup="DateSearch" Text="Search" CssClass="btn btn-one w-100" OnClientClick="BindTransactionDetails(0)"/>--%>
                                    <%--<button id="btnSearch" class="btn btn-one w-100 m-sm-auto" onclick="return BindTransactionDetails(0)">Search</button>--%>
                                    <input type="button" id="btnSearch" class="btn btn-one w-100 m-sm-auto" onclick="return btnCustomSearch()" value="Search" />
                                </div>
                            </div>
                        </div>
                        <div class="dvTransaction row">
                            <%--<asp:Repeater ID="RepSummaryInfo" runat="server">
                                <ItemTemplate>
                                    <div class="col-12 mb-3">
                                        <div class="bg-white p-3">
                                            <div class="row">
                                                <div class="col-12 col-sm-6">
                                                    <h2 class="h6 heading-bold text-capitalize text-colour7"><%#Eval("LoyaltyTxnType").ToString()=="Bonus"?"Bonus":Eval("MerchantName")%></h2>
                                                    <h2 class="h6 heading-regular text-capitalize">
                                                        <%#Eval("TransactionType").ToString() =="Debit" ?"Redeemed NPoints":"Earned NPoints"%>
                                                        <span class="h6 heading-bold text-colour7"><%#Eval("Points")%></span>
                                                    </h2>
                                                    <h2 class="h6 heading-regular text-capitalize">Amount Paid
                                                        <span class="heading-bold text-colour1">NPR </span><span class="h6 heading-bold text-colour7"><%#Eval("TransactionDetailBreakage.SourceAmount ")%></span>
                                                    </h2>
                                                    <h2 class="h6 heading-bold text-success"><%#Eval("TransactionType").ToString() =="Debit" ?"Redeemed":"Spend"%></h2>
                                                </div>
                                                <div
                                                    class="col-12 col-sm-6 text-sm-right d-sm-flex align-items-sm-end justify-content-sm-center flex-sm-column">
                                                    <h2 class="h6 heading-bold d-flex align-items-center">
                                                        <span class="heading-bold text-colour1">NPR</span> <span class="heading-bold text-colour7 pl-1"><%#Eval("Amounts")%></span>
                                                    </h2>
                                                    <h2 class="h7 heading-regular text-capitalize"><%#Eval("ProcessingDate", "{0:dd MMM yyyy}")%></h2>
                                                    <h2 class="h7 heading-regular text-capitalize"><%#Eval("TransactionDate", "{0:dd MMM yyyy}")%></h2>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>--%>
                            <div id="divtransactionSummaryData" class="col-12"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
