<%@ Page Title="Experience Product List" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductList.aspx.cs" Inherits="ExperienceProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/experiences.css">
    <style>
        #divSearchCart{
            display:none !important;
        }
    </style>
    <script type="text/javascript">
    /*    document.getElementById("pageName").innerHTML = "Experience";*/
        var pageSize = 40;
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
            fnGetExperienceProductList(pageIndex, pageSize);
            $("#txtExperiencesProductSearch").autocomplete({
                source: function (request, response) {
                    var list = [];
                    var alphanumericRegex = /^[0-9a-zA-Z_]+$/;
                    var pstrSearchText = request.term.toString();
                    if (pstrSearchText.replace(/\s+/g, '') != '' && !pstrSearchText.replace(/\s+/g, '').match(alphanumericRegex)) {
                        $("#txtExperiencesProductSearch").addClass('experienceerror');
                        return response(list);
                    }
                    else {
                        $.ajax({
                            type: 'POST',
                            url: 'ExperienceProductList.aspx/GetSearchList',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            data: "{'pstrSearchText':'" + pstrSearchText + "'}",
                            cache: false,
                            success: function (msg) {
                                $("#updProgress").hide();
                                if (msg.d != '') {
                                    response($.map(msg.d, function (item) {
                                        return {
                                            label: item.title,
                                            val: item.id,
                                            type: item.type
                                        }
                                    }));
                                } else {
                                    list.push({ label: 'No result found.', val: '', type: '' });
                                    response(list);
                                }
                            },
                            error: function (errmsg) {
                                response(list);
                            },
                            beforeSend: function () {
                                $("#updProgress").show();
                            }
                        });
                    }
                },
                select: function (event, ui) {
                    if (ui.item.val != '') {
                        if (ui.item.type.toLowerCase() == 'product') {
                            fnRedirectingToExperiencesProductDetails(ui.item.val);
                        } else {
                            fnRedirectingToExperiencesSerachProductList(ui.item.val);
                        }
                    }
                    return false;
                },
                minLength: 3,
                scroll: true,
                scrollHeight: 300
            });
        });
        var IsGetExperienceProductListAjaxCalled = false;
        function fnGetExperienceProductList(pageIndex, pageSize) {
            try {
                if (!IsGetExperienceProductListAjaxCalled) {
                    IsGetExperienceProductListAjaxCalled = true;
                    var arrData = {};
                    arrData.pintPage = pageIndex;
                    arrData.pintLimit = pageSize;
                    arrData.pstrSort = 'asc';
                    arrData.pstrPlaceName = $('#slcCountries option:selected').val();
                    arrData.pstrGuidePrice = 'asc';
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
            html += '<div id="divExperienceLoader" class="spin-loader">';
            html += '<img class="spin" width="50" src="Images/loader/spinner-2.gif" alt="" />';
            html += '</div>';
            $("#" + id + "").append(html);
            $("#" + id + "").show();
        }
        function fnBindExperienceProductList(data) {
            var html = '';
            if (data != '') {
                var parseData = JSON.parse(data);
                pageTotal = parseData.totalCount;
                if (parseData.data != null && parseData.status.toLowerCase() == 'success' && parseData.data.length > 0) {
                    for (var i = 0; i < parseData.data.length; i++) {
                        html += '<div class="col-sm-6 col-lg-3 mb-4"><div class="experience">';
                        if (parseData.data[i].previewImage == null) {
                            html += '<div title=\"Product Image\"><span><img class="w-100 img-fluid" alt=\"Product Image\" src =\"\"></span></div>';
                        } else {
                            html += '<div title=\"Product Image\"><span><img class="w-100 img-fluid" alt=\"Product Image\" src =\"' + parseData.data[i].previewImage.urlSmall + '\"></span></div>';
                        }
                        html += '<div class="card_detail"><h3><span>' + parseData.data[i].name + '</span></h3><h5>' + parseData.data[i].holibobGuidePrice.grossFormattedText + '</h5>';
                        html += '<ul class="moreExp">';
                        html += '<li><i class="bi bi-lightning-fill"></i><p>Instant Confirmation</p></li>';
                        var durationHtml = fnSetProductMinAndMaxDuration(parseData.data[i].minDuration, parseData.data[i].maxDuration);
                        if (durationHtml != '') {
                            html += '<li><i class="bi bi-stopwatch"></i><p>' + durationHtml + '</p></li>';
                        }
                        if (parseData.data[i].cancellationPolicy.hasFreeCancellation) {
                            html += '<li><i class="bi bi-x-circle"></i><p>Free Cancellation</p></li>';
                        }
                        if (parseData.data[i].place.cityName != '') {
                            html += '<li><i class="bi bi-geo-alt-fill"></i><p>' + parseData.data[i].place.cityName + '</p></li>';
                        }
                        html += '</ul >';
                        html += '<a href=\"javascript:void(0);\" onclick=\"fnRedirectingToExperiencesProductDetails(\'' + parseData.data[i].id + '\');\" class="btn btn-one">Learn More</a></div>';
                        html += '</div></div>';
                    }
                } else {
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
        function fnSetProductMinAndMaxDuration(minDuration, maxDuration) {
            var html = '';
            try {
                if (minDuration != null && maxDuration != null && minDuration != 'P0D' && maxDuration != 'P0D' && minDuration != maxDuration) {
                    html = minDuration + ' - ' + maxDuration;
                } else if (minDuration != null && maxDuration != null && minDuration != 'P0D' && maxDuration != 'P0D' && minDuration == maxDuration) {
                    html = minDuration;
                }
                if (html != '') {
                    html = html.replace(/PT/g, '');
                    html = html.replace(/P/g, '');
                    html = html.replace(/T/g, '');
                    html = html.replace(/H/g, ' hours ');
                    html = html.replace(/M/g, ' minutes');
                    html = html.replace(/D/g, ' day');
                }
            } catch (e) {
            }
            return html;
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
                window.location.href = "ExperienceProductDetails.aspx?Id=" + id;
            } catch (e) {
            }
        }
        function fnCallExperienceProductList() {
            try {
                pageIndex = 1;
                $('#divExperienceProductList').empty();
                fnGetExperienceProductList(pageIndex, pageSize);
            } catch (e) {
            }
        }
    </script>
    <section>
        <div class="container">
            <div class="row">
                <div class="expHead mx-auto col-md-12">
                    <h3 class="heading-yellow mt-2 mb-2 mt-lg-5">Experiences</h3>
                    <p>Enjoy a handpicked selection of experiences from our collection.</p>
                    <div class="expSearch">
                        <div class="searchBlk">
                            <input id="txtExperiencesProductSearch" type="text" name="search" required />
                            <label>Search Destination</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="MyExperiences">
        <div class="container">
            <div class="row">
                <div class="col-md-12 pt-5">
                    <div class="expHead d-none">
                        <ul class="moreExp row">
                            <li class="col-md-3 col-6"><i class="bi bi-trophy-fill"></i>
                                <p>Best rates and availability</p>
                            </li>
                            <li class="col-md-3 col-6"><i class="bi bi-x-circle"></i>
                                <p>Flexible cancellation policies</p>
                            </li>
                            <li class="col-md-3 col-6"><i class="bi bi-lightning-fill"></i>
                                <p>Live and instant booking</p>
                            </li>
                            <li class="col-md-3 col-6"><i class="bi bi-people-fill"></i>
                                <p>Trusted suppliers</p>
                            </li>
                        </ul>
                    </div>
                    <div class="glist">
                        <select id="slcCountries" class="form-control" onchange="fnCallExperienceProductList()" style="display: none;">
                            <option value="">All</option>
                            <option value="Bahrain">Bahrain</option>
                            <option value="Egypt">Egypt</option>
                            <option value="India">India</option>
                            <option value="Oman">Oman</option>
                            <option value="Qatar">Qatar</option>
                            <option value="Saudi Arabia">Saudi Arabia</option>
                            <option value="United Arab Emirates">United Arab Emirates</option>
                            <option value="United Kingdom">United Kingdom</option>
                            <option value="United States">United States</option>
                            <option selected="selected" value="Nepal">Nepal</option>
                        </select>
                        <div class="row" id="divExperienceProductList"></div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

