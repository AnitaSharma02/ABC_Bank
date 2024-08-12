<%@ Page Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductList.aspx.cs" Inherits="ExperienceProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/experience.css" />


    <div class="dvExperience mb-5">
        <div class="container">
            <div class="row">
                <div class="expHead mx-auto col-12 text-center">
                    <h2 class="heading-semibold mt-5 mb-2 mt-md-5 mt-lg-5">Experiences</h2>
                    <p class="pb-2">Find activities, attractions, tours & more!</p>
                    <div class="expSearch">
                        <div class="dvSearch">
                            <div class="input-group input-group-lg">
                                <input id="txtSearchTerm" autocomplete="off" class="form-control" type="text" name="searchTerm" placeholder="Search Destination" required />
                                <div class="input-group-prepend">
                                    <span id="btnSearchExperiences" type="button" class="input-group-text">
                                        <img src="../images/icons/other/search-icon.svg"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="dvCardBox mt-5 mb-4 pt-5 pb-5">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="row no-gutters" id="divExperienceProductList">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                if (parseData != null  && parseData.length > 0) {
                    for (var i = 0; i < parseData.length; i++) {
                        html += '<div class="col-sm-6 col-lg-3 mb-4">';
                        html += '<a href=\"javascript:void(0);\" class="d-block shadow-sm bg-white border h-100"  onclick=\"fnRedirectingToExperiencesProductDetails(\'' + parseData[i].uuid + '\');\">';
                        html += '<div class="h-100 d-flex flex-column">';

                        html += '<div class="img-container">';
                        if (parseData[i].image == null) {
                            html += '<img class="w-100 img-fluid" alt=\"Product Image\" src =\"\"/>';
                        } else {
                            html += '<img class="w-100 img-fluid" alt=\"Product Image\" src =\"' + parseData[i].image + '\"/>';
                        }
                        html += '</div >';

                        html += '<div class="p-3">';
                        html += '<div class="card_detail">';
                        html += '<h2 class="h6 heading-semibold mt-2 mb-2">' + parseData[i].title + '</h2>';
                        html += ' <p class="h7 heading-regular">' + parseData[i].city + ',' + parseData[i].country+'</p>';
                        html += '</div>';
                        html += '<div class="cardPoints d-flex justify-content-between align-items-center pt-4">';
                        html += '<p class="h8 heading-semibold text-colour5 text-truncate">'+ parseData[i].typeName+'</p>';
                        html += '<p class="h8 heading-semibold text-colour5">from <span class="font-weight-bold">' + FormatCurrency(parseData[i].basePrice, parseData[i].convertedCurrency)+'</span> /pax</p>';
                        html += '</div>';
                        html += '</div>';

                        html += '</div >';
                        html += '</a >';
                        html += '</div >';
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
        function FormatCurrency( decValue,  currencyCode)
        {
            if (currencyCode != "" && currencyCode != null) {
                return currencyCode+" "+parseFloat(decValue).toLocaleString(window.document.documentElement.lang);
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
        function GetSearchExperiences(searchTerm)
        {
            if (searchTerm != '')
            {
                window.location.href = 'ExperiencesSearch.aspx?searchterm=' + searchTerm;
            }
        }
    </script>
</asp:Content>