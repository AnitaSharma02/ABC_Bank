<%@ Page Title="Experience Product Serach" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ExperienceProductSerach.aspx.cs" Inherits="ExperienceProductSerach" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="css/experiences.css">
    <style type="text/css">
        #updProgress {
            display: none !important;
        }

        .MyExperiences .row {
            margin: 0px -15px 15px !important;
            width: auto;
        }

        .MyExperiences {
            background: #f1f1f1;
            padding: 20px 0;
        }

        .card-body {
            margin-left: 20px;
        }

        .main {
            padding: 0;
            margin: 25px 0 0 0;
            min-height: 200px;
            margin-top: 105px !important;
        }

        #divSearchCart {
            display: none !important;
        }
    </style>
    <script type="text/javascript">
       /* document.getElementById("pageName").innerHTML = "Experience";*/
        $(document).ready(function () {
            fnGetExperienceProductListByPlaceId();
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
        function fnShowLoader(id) {
            var html = '';
            html += '<div id="divExperienceLoader" class="spin-loader">';
            html += '<img class="spin" width="50" src="Images/loader/spinner-2.gif" alt="" />';
            html += '</div>';
            $("#" + id + "").empty().html(html);
            $("#" + id + "").show();
        }
        function fnGetExperienceProductListByPlaceId() {
            try {
                fnShowLoader('divExperienceProductSerach');
                var placeId = getQuerystring('Id');
                var isPrivate = false;
                var isNew = false;
                switch ($('#slcProductFilter option:selected').val().toLowerCase()) {
                    case "isNew":
                        isNew = true;
                        break;
                    case "isPrivate":
                        isPrivate = true;
                        break;
                }
                var isRecommended = 'asc';
                var guidePrice = $('#slcProductSort option:selected').val();
                var arrCategoryIds = [];
                $(".searchCategoryIds").each(function () {
                    if ($(this).prop("checked") == true) {
                        arrCategoryIds.push(this.id);
                    }
                });
                var arrAttributeIds = [];
                $(".searchAttributeIds").each(function () {
                    if ($(this).prop("checked") == true) {
                        arrAttributeIds.push(this.id);
                    }
                });
                var pstrSearchWithin = $('#txtSearchWithin').val();
                var arrData = {};
                arrData.pstrPlaceId = placeId;
                arrData.pblnIsPrivate = isPrivate;
                arrData.pblnIsNew = isNew;
                arrData.pstrIsRecommended = isRecommended;
                arrData.pstrGuidePrice = guidePrice;
                arrData.pstrSearch = pstrSearchWithin;
                arrData.plstCategoryIds = arrCategoryIds;
                arrData.plstAttributeIds = arrAttributeIds;
                $.ajax({
                    type: 'POST',
                    url: 'ExperienceProductSerach.aspx/GetExperienceProductListByPlaceId',
                    contentType: 'application/json;',
                    dataType: 'json',
                    data: JSON.stringify(arrData),
                    cache: false,
                    success: function (rtnData) {
                        fnBindExperienceProductSerachData(rtnData.d);
                    },
                    error: function (errmsg) {
                    },
                    beforeSend: function () {
                        $("#updProgress").hide();
                    }
                });
            } catch (e) {
            }
        }
        function fnBindExperienceProductSerachData(data) {
            try {
                var html = '';
                if (data != '') {
                    var parseData = JSON.parse(data);
                    if (parseData.data != null && parseData.status.toLowerCase() == 'success' && parseData.data.length > 0) {
                        for (var i = 0; i < parseData.data.length; i++) {
                            html += '<div class="RightBox d-flex flex-column flex-md-row align-items-center">';
                            html += '<div class="">';
                            html += '<div class="imgBoxs">';
                            html += '<img src=\"' + parseData.data[i].previewImage.urlSmall + '\" alt=\"' + parseData.data[i].name + '\" />';
                            html += '</div>';
                            html += '</div>';
                            html += '<div class="">';
                            html += '<div class="contentBoxarea px-md-3 px-4 py-md-3 py-4">';
                            html += '<h2>' + parseData.data[i].name + '</h2>';
                            html += '<p>' + parseData.data[i].description + '</p>';
                            html += '<div class="align-items-baseline d-flex flex-md-row justify-content-between">';
                            html += '<h6 class="px-4">' + parseData.data[i].guidePriceFormattedText + '</h6>';
                            html += '<input type=\"button\" class=\"bookNow\" id=\"' + parseData.data[i].id + '\" onclick=\"fnRedirectToProductDetails(\'' + parseData.data[i].id + '\');\" value=\"Book Now\"/>';
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                            html += '</div>';
                        }
                        //if ($('#divCategoryAccordionData').html() == '') {
                        fnBindCategoryData(parseData.categoryTree);
                        fnApplyAccordion();
                        //}
                        //if ($('#divAttributeAccordionData').html() == '') {
                        fnBindAttributeData(parseData.attributeTree);
                        fnApplyAccordion();
                        //}
                    } else {
                        html += 'No product found.';
                    }
                } else {
                    html += 'No product found.';
                }
                $('#divExperienceProductSerach').empty().html(html);
            } catch (e) {
            }
        }
        function fnBindCategoryData(categoryTree) {
            try {
                $('#divCategoryAccordionData').empty();
                for (var i = 0; i < categoryTree.length; i++) {
                    var html = '';
                    html += '<div class="card">';
                    html += '<div class="card-header" id="heading-' + i + '">';
                    html += '<h5 class="mb-0">';
                    html += '<a role="button" data-toggle="collapse" href="#collapse-' + i + '" aria-expanded="false" aria-controls="collapse-' + i + '">' + categoryTree[i].label + '</a>';
                    html += '</h5>';
                    html += '</div>';
                    html += '<div id="collapse-' + i + '" class="collapse" data-parent="#divCategoryAccordion" aria-labelledby="heading-' + i + '">';
                    html += '<div class="card-body">';
                    for (var j = 0; j < categoryTree[i].branches.length; j++) {
                        html += '<div id="divCategoryAccordion-' + i + '-' + j + '">';
                        html += '<div class="card">';
                        html += '<div class="card-header" id="heading-' + i + '-' + j + '">';
                        html += '<h5 class="mb-0">';
                        html += '<a class="collapsed" role="button" data-toggle="collapse" href="#collapse-' + i + '-' + j + '" aria-expanded="false" aria-controls="collapse-' + i + '-' + j + '">';
                        html += '<span class="padrgt">' + categoryTree[i].branches[j].label + '</span>';
                        html += '</a>';
                        html += '</h5>';
                        html += '</div>';
                        html += '<div id="collapse-' + i + '-' + j + '" class="collapse" data-parent="#divCategoryAccordion-' + i + '-' + j + '" aria-labelledby="heading-' + i + '-' + j + '">';
                        html += '<div class="card-body">';
                        for (var k = 0; k < categoryTree[i].branches[j].branches.length; k++) {
                            html += '<div id="divCategoryAccordion-' + i + '-' + j + '-' + k + '">';
                            html += '<input type="checkbox" onclick="fnCheckCheckBox();" ' + (categoryTree[i].branches[j].branches[k].isSelected ? 'checked=\"checked\"' : "") + ' class="searchCategoryIds" id="' + categoryTree[i].branches[j].branches[k].id + '" name="' + categoryTree[i].branches[j].branches[k].id + '" value="' + categoryTree[i].branches[j].branches[k].id + '">';
                            html += '<label for="' + categoryTree[i].branches[j].branches[k].id + '">' + categoryTree[i].branches[j].branches[k].label + ' (' + categoryTree[i].branches[j].branches[k].count + ')</label>';
                            html += '</div>';
                        }
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                        html += '</div>';
                    }
                    html += '</div>';
                    html += '</div>';
                    $('#divCategoryAccordionData').append(html);
                }
            } catch (e) {
            }
        }
        function fnCheckCheckBox(id) {
            try {
                fnGetExperienceProductListByPlaceId();
            } catch (e) {
            }
        }
        function fnResetCategoryAttributeData() {
            try {
                $(".searchCategoryIds").each(function () {
                    $(this).prop('checked', false);
                });
                $(".searchAttributeIds").each(function () {
                    $(this).prop('checked', false);
                });
                fnGetExperienceProductListByPlaceId();
            } catch (e) {
            }
        }
        function fnBindAttributeData(attributeTree) {
            try {
                $('#divAttributeAccordionData').empty();
                for (var i = 0; i < attributeTree.length; i++) {
                    var html = '';
                    html += '<div id="divAttributeAccordion-1000-' + i + '">';
                    html += '<div class="card">';
                    html += '<div class="card-header" id="heading-1000-' + i + '">';
                    html += '<h5 class="mb-0">';
                    html += '<a class="collapsed" role="button" data-toggle="collapse" href="#collapse-1000-' + i + '" aria-expanded="false" aria-controls="collapse-1000-' + i + '">';
                    html += '<span class="padrgt">' + attributeTree[i].label + '</span>';
                    html += '</a>';
                    html += '</h5>';
                    html += '</div>';
                    html += '<div id="collapse-1000-' + i + '" class="collapse" data-parent="#divAttributeAccordion-1000-' + i + '" aria-labelledby="heading-1000-' + i + '">';
                    html += '<div class="card-body">';
                    for (var j = 0; j < attributeTree[i].branches.length; j++) {
                        html += '<input type="checkbox" onclick="fnCheckCheckBox();" ' + (attributeTree[i].branches[j].isSelected ? 'checked=\"checked\"' : "") + ' class="searchAttributeIds" id="' + attributeTree[i].branches[j].id + '" name="' + attributeTree[i].branches[j].id + '" value="' + attributeTree[i].branches[j].id + '">';
                        html += '<label for="' + attributeTree[i].branches[j].id + '">' + attributeTree[i].branches[j].label + ' (' + attributeTree[i].branches[j].count + ')</label><br/>';
                    }
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    html += '</div>';
                    $('#divAttributeAccordionData').append(html);
                }
            } catch (e) {
            }
        }
        function fnRedirectToProductDetails(id) {
            try {
                $("#updProgress").show();
                window.location.href = "ExperienceProductDetails.aspx?Id=" + id;
            } catch (e) {
            }
        }
        function searchKeyPress(e) {
            // look for window.event in case event isn't passed in
            e = e || window.event;
            if (e.keyCode == 13) {
                if ($('#txtSearchWithin').val().length > 3) {
                    fnGetExperienceProductListByPlaceId();
                }
                return false;
            }
            return true;
        }
        function fnApplyAccordion() {
            $('.accordion').each(function () {
                var $accordian = $(this);
                $accordian.find('.accordion-head').on('click', function () {
                    $(this).parent().find(".accordion-head").removeClass('open close');
                    $(this).removeClass('open').addClass('close');
                    $accordian.find('.accordion-body').slideUp();
                    if (!$(this).next().is(':visible')) {
                        $(this).removeClass('close').addClass('open');
                        $(this).next().slideDown();
                    }
                });
            });
        }
    </script>
    <div class="MyExperiences">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <div class="searchAccor">
                        <div class="categoryBox">
                            <p>Categories</p>
                            <a href="javascript:void(0)" onclick="fnResetCategoryAttributeData();">Reset</a>
                        </div>
                        <div id="divCategoryAccordion">
                            <div id="divCategoryAccordionData"></div>
                        </div>
                    </div>
                    <div class="searchAccor">
                        <div id="divAttributeAccordion" class="categoryBox">
                            <div class="card">
                                <div class="card-header" id="heading-1000">
                                    <h5 class="mb-0">
                                        <a role="button" data-toggle="collapse" href="#collapse-1000" aria-expanded="false" aria-controls="collapse-1000">Advanced Attribute</a>
                                    </h5>
                                </div>
                                <div id="collapse-1000" class="collapse" data-parent="#divAttributeAccordion" aria-labelledby="heading-1000">
                                    <div id="divAttributeAccordionData" class="card-body"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="sortFilterBox">
                        <div class="sortFilter">
                            <div class="row">
                                <div class="searchBlk">
                                    <input type="text" id="txtSearchWithin" onkeypress="return searchKeyPress(event);" required />
                                    <label>Refine your search within</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label>Sort</label>
                                    <select id="slcProductSort" onchange="fnGetExperienceProductListByPlaceId();" class="form-controlarrow ">
                                        <option value="">Recommended</option>
                                        <option value="asc">Price low to high</option>
                                        <option selected="selected" value="desc">Price high to low</option>
                                    </select>
                                </div>
                                <div class="col-md-6">
                                    <label>Filter</label>
                                    <select id="slcProductFilter" onchange="fnGetExperienceProductListByPlaceId();" class="form-controlarrow">
                                        <option value="">None</option>
                                        <option value="isNew">Recently Added Product</option>
                                        <option value="isPrivate">Private Tour</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divExperienceProductSerach"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

