<%@ Page Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductList.aspx.cs" Inherits="ExperienceProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />
    <style>
        .dvInnerBanner{
            display:none;
        }
    </style>

    <div class="dvExperienceProductList py-4 pt-lg-0">
        <div class="container-xl">
            <div class="row">
                <div class="col-12 text-center">
                    <h2 class="heading1 mb-2">Experiences</h2>
                    <p class="mb-3">Find activities, attractions, tours & more!</p>
                </div>
                <div class="dvSearch dvErrors col-12 offset-lg-3 col-lg-6 mb-5">                    
                    <div class="dvInputGroup input-group">
                        <input id="txtSearchTerm" autocomplete="off" class="input form-control" type="text" name="searchTerm" placeholder="Search Destination" required />
                        <div class="input-group-append">
                            <button type="button" id="btnSearchExperiences" class="input-group-text"><i class="fa-solid fa-magnifying-glass"></i></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="dvProductList">
            <div class="container-xl">
                <div class="dvVcData row" id="divExperienceProductList"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var pageSize = 16;
        var pageTotal = 0;
        var pageIndex = 1;
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
            fnGetExperienceProductList(pageIndex, pageSize);
        }
        $(document).ready(function () {
            $(function () {
                if ($.loadedFromBrowserCache) {
                    alert('loadedFromBrowserCache');
                }
            });
            BindBanner();
            fnGetExperienceProductList(pageIndex, pageSize);
            GetRedemptionOptions();
        });
        var IsGetExperienceProductListAjaxCalled = false;
        function fnGetExperienceProductList(pageIndex, pageSize) {
            try {
                if (!IsGetExperienceProductListAjaxCalled) {
                    IsGetExperienceProductListAjaxCalled = true;
                    var arrData = {};
                    arrData.pintPage = pageIndex;
                    arrData.pintPageSize = pageSize;
                    $.ajax({
                        type: 'POST',
                        url: 'ExperienceProductList.aspx/GetExperienceProductList',
                        contentType: 'application/json;',
                        dataType: 'json',
                        data: JSON.stringify(arrData),
                        cache: false,
                        success: function (rtnData) {
                            IsGetExperienceProductListAjaxCalled = false;
                            fnBindExperienceProductList(rtnData.d);
                            //updateVcDataSections();
                        },
                        error: function (errmsg) {
                            IsGetExperienceProductListAjaxCalled = false;
                        },
                        beforeSend: function () {
                            fnShowLoader('divExperienceProductList');
                            $("#updProgress").hide();
                        }
                    });
                }
            } catch (e) {
                IsGetExperienceProductListAjaxCalled = false;
            }
        }
        function fnShowLoader(id) {
            var html = '';
            html += '<div id="divExperienceLoader" class="spin-loader" style="margin: auto;">';
            html += '<img class="spin" width="50" src="Images/loading.gif" alt="" />';
            html += '</div>';
            $("#" + id + "").append(html);
            $("#" + id + "").show();
        }
        function fnBindExperienceProductList(data) {
            var html = '';
            if (data != '') {
                var parseData = JSON.parse(data);
                pageTotal = parseData.length;
                if (parseData != null && parseData.length > 0) {
                    for (var i = 0; i < parseData.length; i++) {
                        html += '<div class="dvProductCard col-sm-6 col-lg-3 mb-4">';
                        html += '<div class="dvItem">';
                        html += '<a href=\"javascript:void(0);\" class="anchor"  onclick=\"fnRedirectingToExperiencesProductDetails(\'' + parseData[i].uuid + '\');\">';

                        html += '<div class="img-container">';
                        if (parseData[i].image == null) {
                            html += '<img class="" alt=\"Product Image\" src =\"\"/>';
                        } else {
                            html += '<img class="" alt=\"Product Image\" src =\"' + parseData[i].image + '\"/>';
                        }
                        html += '</div>';
                        html += '</a>';
                        html += '<h2 class="px-3 pt-3 pb-2">' + parseData[i].title + '</h2>';
                        html += '<p class="px-3 pb-2">' + parseData[i].city + ', ' + parseData[i].country + '</p>';
                        html += '<div class="d-flex flex-wrap justify-content-between px-3 pb-3 mt-auto">';
                        html += '<p class="points">' + parseData[i].typeName + '</p>';
                        html += '<p class="points"><span>from</span> <span>' + FormatCurrency(parseFloat(parseData[i].convertedAmount).toFixed(2), parseData[i].convertedCurrency) + '</span>/pax<span></span></p>';
                        html += '</div>';
                       
                        html += '</div>';
                        html += '</div>';
                    }
                }
                else {
                    if ($("#divExperienceProductList").html().length == 0) {
                        html = "No products found.";
                    }
                }
            } else {
                if ($("#divExperienceProductList").html().length == 0) {
                    html = "No products found.";
                }
            }
            $("#divExperienceProductList").append(html);
            $("#divExperienceLoader").remove();
        }
        function fnRedirectingToExperiencesSerachProductList(id) {
            try {
                $("#updProgress").show();
                window.location.href = "ExperienceProductSerach.aspx?Id=" + id;
            } catch (e) {
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
        function ValidateSearchExperiencesFields() {
            var isValidated = true;
            $(".error").remove();
            var searchTerm = $.trim($('#txtSearchTerm').val());
            if (searchTerm.length == 0) {
                isValidated = false;
                $('#txtSearchTerm').closest("div").after('<span class="error h8 heading-regular text-danger">This field is required</span>');

                // (al)
                //$('#expSearchErr').html('<span class="error h8 heading-regular text-danger">This field is required</span>');
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
        function GetSearchExperiences(searchTerm) {
            if (searchTerm != '') {
                var Querystring = "searchterm=" + $.trim($("#txtSearchTerm").val()) ;

                window.location.href = "ExperiencesSearch.aspx?" + Querystring;
            }
        }
    </script>
</asp:Content>
