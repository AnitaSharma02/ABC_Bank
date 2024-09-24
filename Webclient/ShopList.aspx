<%@ Page Title="Shop List" Language="C#" MasterPageFile="SiteShopMaster.master" AutoEventWireup="true" CodeFile="ShopList.aspx.cs" Inherits="ShopList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPSHOP" runat="Server">
    <style>     
     .dvInnerBanner,
     .dvShopMenu .navbar-toggler{
         display:none;
     }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav id="divBreadbrums" runat="server">
              <%--  <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="hoteldetails.html">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active">Shop</li>
                    <li class="breadcrumb-item active">Shop List</li>
                </ul>--%>
            </nav>
        </div>
    </div>

    <div class="dvProductList pb-5">
        <div class="container-xl">
            <div class="row">
                <div class="dvFilter modal fade col-lg-3 px-0 px-lg-3" id="dvFilterModal" tabindex="-1">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content border-0">
                            <div class="modal-header border-0 d-lg-block p-0">
                                <div class="modal-title dvTotalRecords border-0 p-3">
                                    <p class="heading6 text-colour1"><span>Total Records found</span> <span id="spnTotalCount"></span></p>
                                </div>
                                <button type="button" class="close d-lg-none px-3" data-dismiss="modal">
                                   <i class="fa-solid fa-xmark"></i>
                                </button>
                              </div>
                            <div class="modal-body p-lg-0">
                                <div class="accordion" id="filter-accordion">
                                    <div id="divFilters" runat="server">
                                       <%-- <div class="card"></div>
                                        <div class="dvBorderBottom">
                                            <div class="col-12">
                                                <div class="border-bottom my-3"></div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-9">
                    <div class="row">
                        <div class="dvSortBy col-12 mb-3">
                            <div class="bg-colour2 b-radius d-flex flex-wrap justify-content-between align-items-center p-2 mb-1">
                                <button data-toggle="modal" data-target="#dvFilterModal" type="button" class="btn btn-one col-12 d-lg-none mb-2">Filter </button>
                                <p class="col-8 px-0"></p>
                                <div class="dropdown heading-regular col-lg-3 px-0">
                                    <select name="sortBy" id="sortBy" onchange="SortProducts(); return false;" class="form-control">
                                        <%--<option value=""></option>--%>
                                        <option value="priority-descending;name-ascending" selected>Featured</option>
                                        <option value="best-selling">Best Selling</option>
                                        <option value="title-ascending">Alphabetically, A-Z</option>
                                        <option value="title-descending">Alphabetically, Z-A</option>
                                        <option value="price_inr-ascending">Price, low to high</option>
                                        <option value="price_inr-descending">Price, high to low</option>
                                        <option value="createddate-descending">Date, new to old</option>
                                        <option value="createddate-ascending">Date, old to new</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="dvProducts col-12">
                            <div class="row">
                                <div id="dvResult" class="products col-12" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ajaxStart(function () {
            $("#updProgress").hide();
        }).ajaxStop(function () {
            $("#updProgress").hide();
        });
        $(document).ready(function () {
            $('.shop').hide();
            BindProducts(1);
            BindBanner();


            $(document).on('show.bs.modal', '.modal', function () {
                $("body").css("padding-right", "0");
            });

            $(document).on('hide.bs.modal', '.modal', function () {
                $("body").css("padding-right", "0");
            });
        });
       
        function BindProducts(PageNo) {
            var CategoryId = getQuerystring("CategoryId");
            var ProductName = getQuerystring("ProductName");
            var Sort = getQuerystring("sort");
            var Terms = getQuerystring("terms");
            SearchProducts(CategoryId, ProductName, Sort, Terms, PageNo);
            $(window).scrollTop(0);
        }
        function SortProducts() {
            var Sort = $("#sortBy").val();
            if (Sort != '') {
                var url = new URL(window.location.href);
                url.searchParams.set("sort", Sort);
                window.location.replace(url.href)
            }
            return false;
        }
        function FilterProducts() {
            var Terms = "";
            $('#CP_CPSHOP_divFilters>.card').each(function () {
                var filter = $(this).attr("id") + ':';
                var values = '';
                var checkBoxList = $(':checkbox', this);
                for (var i = 0; i < checkBoxList.length; i++) {
                    if (checkBoxList[i].checked) {
                        if (values != '') {
                            values += ',' + checkBoxList[i].value;
                        } else {
                            values += checkBoxList[i].value;
                        }
                    }
                }
                if (values != '') {
                    if (Terms != '') {
                        Terms += ';' + filter + values;
                    } else {
                        Terms += filter + values;
                    }
                }
            });
            if (Terms != '') {
                var url = new URL(window.location.href);
                url.searchParams.set("terms", Terms);
                window.location.replace(url.href)
            }
            else {
                var url = new URL(window.location.href);
                url.searchParams.set("terms", "");
                window.location.replace(url.href)
            }
            return false;
        }
        function SearchProducts(CategoryId, ProductName, Sort, Terms, PageNo) {
            $.ajax({
                type: 'POST',
                url: 'ShopList.aspx/SearchProducts',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{CategoryId:'" + CategoryId + "',ProductName:'" + ProductName + "',Sort:'" + Sort + "',Terms:'" + Terms + "',PageNo:" + PageNo + "}",
                success: function (msg) {
                    if (msg.d) {
                        GetProductList(PageNo);
                        Filter();
                        if ((getQuerystring("sort") == 'priority-descending%3Bname-ascending') || (getQuerystring("sort") == '')) {
                            $("#sortBy").value = "Feature";
                        }
                        else {
                            $("#sortBy").val(getQuerystring("sort"));
                        }
                        return false;
                    }
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            })
            return false;
        }
        function GetProductList(PageNo) {
            $.ajax({
                type: 'POST',
                url: 'ShopList.aspx/GetProductList',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "{PageNo:" + PageNo + "}",
                success: function (msg) {
                    if (msg.d != '') {
                        var data = msg.d.split('||');
                        $("#CP_CPSHOP_dvResult").html(data[0]);
                        $("#spnTotalCount").empty().html(data[1]);
                        $('.shop').show();
                    }
                    return false;
                }
            })
            return false;
        }
        function Filter() {
            $.ajax({
                type: 'POST',
                url: 'ShopList.aspx/Filter',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: "",
                success: function (msg) {
                    if (msg.d != '') {
                        $("#CP_CPSHOP_divFilters").html(msg.d);
                    }
                    return false;
                }
            })
            return false;
        }
        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }
        function toggleAccordion(o) {
            $(o).toggleClass("active");
            var panel = $(o).next("div");
            panel.toggle();
        }
    </script>
</asp:Content>
