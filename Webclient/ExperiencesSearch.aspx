<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperiencesSearch.aspx.cs" Inherits="ExperiencesSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />

    <%-- <div class="dvBreadcrumbs mt-3 mb-3">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="hoteldetails.html">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="Index.aspx">Home</a></li>
                    <li class="breadcrumb-item"><a href="ExperienceProductList.aspx">Experiences</a></li>
                    <li class="breadcrumb-item active">Search</li>
                </ul>
            </nav>
        </div>
    </div>--%>
    <div class="dvProductList dvExperienceSearch pb-5 mt-3" id="dvSearchProducts">
        <div class="container-xl">
            <div class="row">
                <div class="dvFilter modal fade col-lg-3 px-0 px-lg-3 mb-3" id="dvFilterModal" tabindex="-1">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content border-0 b-radius">
                            <div class="modal-header border-0 p-0">
                                <div class="modal-title border-0 p-3">
                                    <h5 class="h6 heading-semibold text-colour1">
                                        <i class="fa fa-filter mx-0"></i>
                                        <span>Filters</span>
                                    </h5>
                                </div>
                                <button type="button" class="close d-lg-none px-3" data-dismiss="modal">
                                    <i class="fa-solid fa-xmark"></i>
                                </button>
                                <%--<i class="fa fa-filter mx-0"></i>
                                 <span class="h7 heading-semibold">Filters Results</span>--%>
                            </div>
                            <div class="modal-body p-lg-0">
                                <div class="accordion" id="filter-accordion">
                                    <div class="card my-3 mb-lg-3 mt-lg-0">
                                        <div class="card-header p-0">
                                            <h2 class="mb-0">
                                                <button class="btn btn-block text-left heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse1"><span class="heading6 mb-2 text-capitalize">Types</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>
                                        <div id="collapse1" class="collapse- show" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2" id="divtype">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="dvBorderBottom">
                                        <div class="">
                                            <div class="border-bottom my-3"></div>
                                        </div>
                                    </div>

                                    <div class="card">
                                        <div class="card-header p-0">
                                            <h2 class="mb-0">
                                                <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse2"><span class="heading6 mb-2 text-capitalize">Categories</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>
                                        <div id="collapse2" class="collapse- show" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver px-0 pt-1 pb-2" id="divCategories">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dvBorderBottom">
                                        <div class="">
                                            <div class="border-bottom my-3"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-center p-lg-2">
                                <button type="button" class="btn btn-one w-100" onclick="ClearFilters();">Reset</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-9">
                    <div class="row">
                        <div class="dvModify col-12 mb-3">
                            <div class="row justify-content-between align-items-center mb-1">
                                <div class="col-lg-12">
                                    <button data-toggle="modal" data-target="#dvFilterModal" type="button" class="btn btn-one col-12 d-lg-none mb-2">Filter </button>
                                    <div class="row justify-content-end align-items-center">
                                        <div class="dvSearch dvErrors col-12 col-lg-12">
                                            <div>
                                                <div class="dvInputGroup input-group">
                                                    <input id="txtSearchTerm" autocomplete="off" class="input form-control" type="text" name="searchTerm" value="" placeholder="Search Destination" />
                                                    <div class="input-group-append">
                                                        <button type="button" id="btnSearchExperiences" class="input-group-text bg-colour1"><i class="fa-solid fa-magnifying-glass"></i></button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="dvShowLoader" class="dvTotalRecords col-12">
                                            <div class="bg-colour2 b-radius p-3 my-3 ">
                                                <p class="h7 heading-semibold text-colour7" id="recommendedtxt"></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvProducts col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="row" id="productlist">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var pageSize = 20;
        var pageTotal = 0;
        var pageIndex = 1;
        var resultCount = 0;
        var searchTerm = getQuerystring("searchterm");
        var typesArray = [];
        var categoriesArray = [];
        $(window).scroll(function () {
            if (pageIndex == 2 || pageIndex <= pageTotal) {
                
                if (!IsGetExperienceProductListAjaxCalled) {

                    if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {

                        GetRecords();
                    }
                }
            }
        });
        function GetRecords() {

            pageIndex++;
            fnGetTypesAndCategory();
            fnGetSearchItem(searchTerm, pageIndex, pageSize, typesArray, categoriesArray);
           // fnShowLoader('productlist');
        }
            $(document).ready(function () {
                BindBanner();
                GetRedemptionOptions();
                $(function () {
                    if ($.loadedFromBrowserCache) {
                        alert('loadedFromBrowserCache');
                    }
                });
                if (searchTerm != null && searchTerm != "") {
                    fnGetTypesAndCategory();
                    fnGetSearchItem(searchTerm, pageIndex, pageSize, typesArray, categoriesArray);
                }
                else {
                    window.location = "ExperienceProductList.aspx";
                }
            });

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
            var IsGetExperienceProductListAjaxCalled = false;
            function fnGetSearchItem(searchTerm, pageIndex, pageSize, types, categories) {
                try {
                    if (!IsGetExperienceProductListAjaxCalled) {
                        IsGetExperienceProductListAjaxCalled = true;
                        if (searchTerm != '') {
                            var arrData = {};
                            arrData.pintPage = pageIndex;
                            arrData.pintPageSize = pageSize;
                            arrData.pstrsearchTerm = searchTerm;
                            arrData.types = types;
                            arrData.categories = categories;
                            $.ajax({
                                type: 'POST',
                                url: 'ExperiencesSearch.aspx/SearchExperiences',
                                contentType: 'application/json;',
                                dataType: 'json',
                                data: JSON.stringify(arrData),
                                cache: false,
                                success: function (rtnData) {
                                    if (rtnData.d != null) {
                                        fnBindSearchItem(rtnData.d);

                                        IsGetExperienceProductListAjaxCalled = false;
                                    }
                                    else {
                                        window.location.href = 'ErrorPage.aspx';
                                    }
                                },
                                error: function (errmsg) {
                                    window.location.href = 'ErrorPage.aspx';
                                },
                                beforeSend: function () {
                                    fnShowLoader('productlist');
                                    $("#updProgress").hide();
                                }
                            });
                        }
                    }
                } catch (e) {
                    IsGetExperienceProductListAjaxCalled = false;
                }
            }
            function fnShowLoader(id) {
                var html = '';
                html += '<div id="divExperienceLoader" class="spin-loader text-center col-12" style="margin: auto;">';
                html += '<img class="spin" width="50" src="Images/loading.gif" alt="" />';
                html += '</div>';
                $("#" + id + "").append(html);
                $("#" + id + "").show();
            }
            function fnBindSearchItem(data) {
                var html = '';
                if (data != '') {
                   
                    var parseData = JSON.parse(data); resultCount = (parseData.meta.pagination.total);
                    $("#txtSearchTerm").val(parseData.ExperiencesCriteria.searchTerm);
                    $("#recommendedtxt").empty().append("Showing " + resultCount + " recommended things to do in results <span>with keyword '" + parseData.ExperiencesCriteria.searchTerm + "'</span>");
                    pageTotal = parseData.data.length;

                    $.each(parseData.data, function (i) {
                        html += "<div class=\"dvProductCard col-sm-6 col-lg-4 mb-4\">";
                        html += '<div class="dvItem">';
                        html += "<a href = \"javascript:void(0);\" class=\"anchor\" onclick=\"fnRedirectingToExperiencesProductDetails(\'" + parseData.data[i].uuid + "\');\">";
                        html += "<div class=\"img-container\">";
                        html += "<img alt = \"Product Image\" src =\"" + parseData.data[i].image + " \" />";
                        html += "</div>";
                        html += "<h2 class='px-3 pt-3 pb-2'>" + parseData.data[i].title + "</h2>";
                        html += "<p class='px-3 pb-2'>" + parseData.data[i].city + ',' + parseData.data[i].country + "</p>";
                        html += "<div class=\"d-flex flex-wrap justify-content-between px-3 pb-3 mt-auto\">";
                        html += "<p class=\"points\">" + parseData.data[i].typeName + "</p>";
                        html += "<p class=\"points\"><span>from</span> <span>" + FormatCurrency(parseData.data[i].basePrice, parseData.data[i].convertedCurrency) + "</span> <span>/pax</span></p>";
                        html += "</div>";
                        html += "</a>";
                        html += "</div>";
                        html += "</div>";
                    });
                    $("#productlist").append(html);
                    $("#divExperienceLoader").remove();
                    if (typesArray.length == 0) {
                        $(".chktypes").prop('checked', true);
                    }
                    else {
                        if (typesArray.length > 0) {
                            $.each(typesArray, function (i) {
                                checkWithValue(typesArray[i])
                            })
                        }
                    }
                    if (categoriesArray.length == 0) {
                        $(".chkcategories").prop('checked', true);
                    }
                    else {
                        if (categoriesArray.length > 0) {
                            $.each(categoriesArray, function (i) {
                                checkWithValue(categoriesArray[i])
                            })
                        }
                    }
                    $('.chktypes').each(function () {
                        $(this).off("click");
                        $(this).click(function () {
                            CreateTypeArray();
                            if (ValidateSearchExperiencesFields()) {
                                fnGetSearchItem($("#txtSearchTerm").val(), pageIndex, pageSize, typesArray, categoriesArray)
                            }
                        });
                    });
                    $('.chkcategories').each(function () {
                        $(this).off("click");
                        $(this).click(function () {
                            CreateCategoriesArray();
                            if (ValidateSearchExperiencesFields()) {
                                fnGetSearchItem($("#txtSearchTerm").val(), pageIndex, pageSize, typesArray, categoriesArray)
                            }
                        });
                    });
                    $("#chkTypesCheckAll").off("click");
                    $("#chkTypesCheckAll").click(function () {
                        debugger
                        $(".chktypes").prop('checked', $(this).prop('checked'));
                        CreateTypeArray();
                        if (ValidateSearchExperiencesFields()) {
                            fnGetSearchItem($("#txtSearchTerm").val(), pageIndex, pageSize, typesArray, categoriesArray)
                        }
                    });
                    $("#chkCategoriesCheckAll").off("click");
                    $("#chkCategoriesCheckAll").click(function () {
                        debugger
                        $(".chkcategories").prop('checked', $(this).prop('checked'));
                        CreateCategoriesArray();
                        if (ValidateSearchExperiencesFields()) {
                            fnGetSearchItem($("#txtSearchTerm").val(), pageIndex, pageSize, typesArray, categoriesArray)
                        }
                    });
                }
            }
            function fnRedirectingToExperiencesProductDetails(id) {
                try {
                    $("#updProgress").show();
                    window.location.href = "ExperienceProductDetails.aspx?uuid=" + id;
                } catch (e) {
                }
            }
            function FormatCurrency(decValue, currencyCode) {
                if (currencyCode != "" && currencyCode != null) {
                    return currencyCode + " " + parseFloat(decValue).toLocaleString(window.document.documentElement.lang);
                }
                else {
                    return parseFloat(decValue).toLocaleString(window.document.documentElement.lang);
                }
            }
            $('#btnSearchExperiences').on('click', function (e) {
                e.preventDefault();
                $(this).prop('disabled', true);
                $('#updProgress').show();
                if (ValidateSearchExperiencesFields()) {
                    GetSearchExperiences($("#txtSearchTerm").val());
                    //search code here
                } else {
                    $(this).prop('disabled', false);
                    $('#updProgress').hide();
                }
            });
            $("#txtSearchTerm").keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    var pageNo = 1;
                    $('#updProgress').show();
                    if (ValidateSearchExperiencesFields()) {
                        GetSearchExperiences($("#txtSearchTerm").val());
                        //search code here
                    } else {
                        $('#updProgress').hide();
                    }
                }
            });
            function ValidateSearchExperiencesFields() {
                var isValidated = true;
                $(".error").remove();
                var searchTerm = $.trim($('#txtSearchTerm').val());
                if (searchTerm.length == 0) {
                    isValidated = false;
                    $('#txtSearchTerm').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');
                } else if (searchTerm.length > 0) {
                    if (searchTerm.length >= 3) {
                        var filter = /^[a-zA-Z0-9\s]*$/;
                        if (!filter.test(searchTerm)) {
                            isValidated = false;
                            $('#txtSearchTerm').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter a valid product name</span>');
                        }
                    } else if (searchTerm.length < 3) {
                        isValidated = false;
                        $('#txtSearchTerm').closest("div").after('<span class="error h8 heading-regular text-danger">Please enter atleast 3 characters</span>');
                    }
                }
                return isValidated;
            }
            function GetSearchExperiences(searchTerm) {
                if (searchTerm != '') {
                    window.location.href = 'ExperiencesSearch.aspx?searchterm=' + searchTerm;
                }
            }
            function ClearFilters() {
                $(".error").remove();
                $("#txtSearchTerm").val(searchTerm);
                $("#chkTypesCheckAll").prop('checked', true);
                $("#chkCategoriesCheckAll").prop('checked', true);
                $(".chktypes").prop('checked', true);
                $(".chkcategories").prop('checked', true);
                CreateTypeArray();
                CreateCategoriesArray();
                if (ValidateSearchExperiencesFields()) {
                    fnGetSearchItem($("#txtSearchTerm").val(), pageIndex, pageSize, typesArray, categoriesArray)
                }
            }
            function CreateTypeArray() {
                typesArray = [];
                $('.chktypes').each(function () {
                    if (this.checked) {
                        typesArray.push($(this).val());
                    }
                    else {
                        var index = typesArray.indexOf($(this).val());
                        if (index > -1) {
                            typesArray.splice(index, 1);
                        }
                    }
                });
            }
            function CreateCategoriesArray() {
                categoriesArray = [];
                $('.chkcategories').each(function () {
                    if (this.checked) {
                        categoriesArray.push($(this).val());
                    }
                    else {
                        var index = categoriesArray.indexOf($(this).val());
                        if (index > -1) {
                            categoriesArray.splice(index, 1);
                        }
                    }
                });
            }
            function checkWithValue(val) {
                $(":checkbox").filter(function () {
                    return this.value == val;
                }).prop("checked", "true");
            }
            function fnGetTypesAndCategory() {
                $.ajax({
                    type: 'POST',
                    url: 'ExperiencesSearch.aspx/GetTypesAndCategory',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: '',
                    cache: false,
                    success: function (rtnData) {
                        if (rtnData.d != null) {
                            fnBindTypesAndCategory(rtnData.d);

                        }
                        else {
                            window.location.href = 'ErrorPage.aspx';
                        }
                    },
                    error: function (errmsg) {
                        window.location.href = 'ErrorPage.aspx';
                    },
                    beforeSend: function () {
                      //  fnShowLoader('dvShowLoader');
                        $("#updProgress").hide();
                    }
                });
            }
            function fnBindTypesAndCategory(data) {

                var html = '';
                if (data != '') {
                    var parseData = JSON.parse(data);
                    if (parseData.types != null && parseData.types != '') {
                        //html += '<div class="dvLabel d-flex justify-content-between">';
                        //html += '<label class="checkbox-container d-flex">';
                        //html += '<span class="d-inline-block">';
                        //html += '<input id="chkTypesCheckAll" type="checkbox" checked="checked"  value="SelectAll"/>';
                        //html += '<span class="checkmark"></span>';
                        //html += '</span>';
                        //html += '<span class="d-inline-block ml-2">Select All</span>';
                        //html += '</label>';
                        //html += '</div>';
                        $.each(parseData.types, function (i) {
                            html += '<div class="dvLabel d-flex justify-content-between">';
                            html += '<label class="checkbox-container d-flex">';
                            html += '<span class="d-inline-block">';
                            html += '<input type="checkbox" class="chktypes" value="' + parseData.types[i] + '" />';
                            html += '<span class="checkmark"></span>';
                            html += '</span>';
                            html += '<span class="d-inline-block ml-2">' + parseData.types[i] + '</span>';
                            html += '</label>';
                            html += '</div>';

                        });
                    }
                    else {
                        html += '<div class="dvLabel d-flex justify-content-between">';
                        html += '<span class="d-inline-block">No type available.</span>';
                        html += '</div>';
                    }
                    $("#divtype").empty().append(html);
                    html = "";
                    if (parseData.categories != null && parseData.categories != '') {
                        //html += '<div class="dvLabel d-flex justify-content-between">';
                        //html += '<label class="checkbox-container d-flex">';
                        //html += '<span class="d-inline-block">';
                        //html += '<input id="chkCategoriesCheckAll" type="checkbox" checked="checked" value="SelectAll" />';
                        //html += '<span class="checkmark"></span>';
                        //html += '</span>';
                        //html += '<span class="d-inline-block ml-2">Select All</span>';
                        //html += '</label>';
                        //html += '</div>';
                        $.each(parseData.categories, function (i) {
                            html += '<div class="dvLabel d-flex justify-content-between">';
                            html += '<label class="checkbox-container d-flex">';
                            html += '<span class="d-inline-block">';
                            html += '<input type="checkbox" class="chkcategories" value="' + parseData.categories[i] + '" />';
                            html += '<span class="checkmark"></span>';
                            html += '</span>';
                            html += '<span class="d-inline-block ml-2">' + parseData.categories[i] + '</span>';
                            html += '</label>';
                            html += '</div>';
                        });
                    }
                    else {
                        html += '<div class="dvLabel d-flex justify-content-between">';
                        html += '<span class="d-inline-block">No category available.</span>';
                        html += '</div>';
                    }
                    $("#divCategories").empty().append(html);
                    fnGetSearchItem(searchTerm, pageIndex, pageSize, typesArray, categoriesArray);
                }
            }
    </script>
</asp:Content>

