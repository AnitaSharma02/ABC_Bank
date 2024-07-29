<%@ Page Title="Car Search" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarSearch.aspx.cs" Inherits="CarSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/car.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/CarResultScript.js" type="text/javascript"></script>
    <style type="text/css">
        #sitemap {
            display: none;
        }
         
    </style>

    <%--<div class="banner top_banner">
        <img class="w-100" src="../Images/carpage/car-banner.jpg" />
    </div>--%>
    <div class="dvCarSearch mb-5">
        <div class="container-lg">
            <div class="dvForm row">
                <div class="bg-colour2 p-3">
                    <div class="row">
                        <div class="col-12">
                            <div id="CarValidationError" runat="server" data-i18n="flight-below-fields" class="p-1 mb-2 alert alert-danger text-danger text-center h6 heading-semibold" style="display: none;"></div>
                        </div>
                        <div class="col-12 col-lg-10 order-xs-1">
                            <div class="form-row">
                                <div class="col-lg-4 col-md-12 col-12 mb-3">
                                    <label for="validationDefaultUsername" class="label" data-i18n="car-pickup-location">Pick up location?</label>
                                    <div class="dvInputGroup input-group">
                                        <input type="text" class="form-control" id="txtpickupLocation" data-i18n="[placeholder]car-enter-pickup-location" placeholder="Please enter a pick-up location" aria-describedby="inputGroupPrepend2">
                                        <input type="hidden" id="hndpickupLocationId" value="">
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-white">
                                                <i class="fa-solid fa-location-dot"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label class="label" data-i18n="car-pickup-date">Pick-Up Date</label>
                                    <div class="dvInputGroup input-group">
                                        <input class="input form-control" data-i18n="[placeholder]car-enter-date" placeholder="Enter Date"
                                            type="text" id="txtpickupDate" readonly="readonly" />
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-white">
                                                <i class="fa-regular fa-calendar"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label for="exampleFormControlSelect1" class="label" data-i18n="car-time">Time</label>
                                    <div class="dvInput input-group">
                                        <select class="form-control " id="ddlPickupTime">
                                            <option>00:00</option>
                                            <option>00:15</option>
                                            <option>00:30</option>
                                            <option>00:45</option>
                                            <option>01:00</option>
                                            <option>01:15</option>
                                            <option>01:30</option>
                                            <option>01:45</option>
                                            <option>02:00</option>
                                            <option>02:15</option>
                                            <option>02:30</option>
                                            <option>02:45</option>
                                            <option>03:00</option>
                                            <option>03:15</option>
                                            <option>03:30</option>
                                            <option>03:45</option>
                                            <option>04:00</option>
                                            <option>04:15</option>
                                            <option>04:30</option>
                                            <option>04:45</option>
                                            <option>05:00</option>
                                            <option>05:15</option>
                                            <option>05:30</option>
                                            <option>05:45</option>
                                            <option>06:00</option>
                                            <option>06:15</option>
                                            <option>06:30</option>
                                            <option>06:45</option>
                                            <option>07:00</option>
                                            <option>07:15</option>
                                            <option>07:30</option>
                                            <option>07:45</option>
                                            <option>08:00</option>
                                            <option>08:15</option>
                                            <option>08:30</option>
                                            <option>08:45</option>
                                            <option>09:00</option>
                                            <option>09:15</option>
                                            <option>09:30</option>
                                            <option>09:45</option>
                                            <option>10:00</option>
                                            <option>10:15</option>
                                            <option>10:30</option>
                                            <option>10:45</option>
                                            <option>11:00</option>
                                            <option>11:15</option>
                                            <option>11:30</option>
                                            <option>11:45</option>
                                            <option selected="selected">12:00</option>
                                            <option>12:15</option>
                                            <option>12:30</option>
                                            <option>12:45</option>
                                            <option>13:00</option>
                                            <option>13:15</option>
                                            <option>13:30</option>
                                            <option>13:45</option>
                                            <option>14:00</option>
                                            <option>14:15</option>
                                            <option>14:30</option>
                                            <option>14:45</option>
                                            <option>15:00</option>
                                            <option>15:15</option>
                                            <option>15:30</option>
                                            <option>15:45</option>
                                            <option>16:00</option>
                                            <option>16:15</option>
                                            <option>16:30</option>
                                            <option>16:45</option>
                                            <option>17:00</option>
                                            <option>17:15</option>
                                            <option>17:30</option>
                                            <option>17:45</option>
                                            <option>18:00</option>
                                            <option>18:15</option>
                                            <option>18:30</option>
                                            <option>18:45</option>
                                            <option>19:00</option>
                                            <option>19:15</option>
                                            <option>19:30</option>
                                            <option>19:45</option>
                                            <option>20:00</option>
                                            <option>20:15</option>
                                            <option>20:30</option>
                                            <option>20:45</option>
                                            <option>21:00</option>
                                            <option>21:15</option>
                                            <option>21:30</option>
                                            <option>21:45</option>
                                            <option>22:00</option>
                                            <option>22:15</option>
                                            <option>22:30</option>
                                            <option>22:45</option>
                                            <option>23:00</option>
                                            <option>23:15</option>
                                            <option>23:30</option>

                                        </select>

                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label class="label" data-i18n="car-drop-off-date">Drop off date</label>
                                    <div class="dvInputGroup input-group">
                                        <input class="input datePicker form-control" data-i18n="[placeholder]car-enter-date" placeholder="Enter Date" type="text" id="txtDropoffDate" readonly="readonly" />
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-white">
                                                <i class="fa-regular fa-calendar"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label for="exampleFormControlSelect2" class="label" data-i18n="car-time">Time</label>
                                    <div class="dvInput input-group">
                                        <select class="form-control" id="ddlDropoffTime">
                                            <option>00:00</option>
                                            <option>00:15</option>
                                            <option>00:30</option>
                                            <option>00:45</option>
                                            <option>01:00</option>
                                            <option>01:15</option>
                                            <option>01:30</option>
                                            <option>01:45</option>
                                            <option>02:00</option>
                                            <option>02:15</option>
                                            <option>02:30</option>
                                            <option>02:45</option>
                                            <option>03:00</option>
                                            <option>03:15</option>
                                            <option>03:30</option>
                                            <option>03:45</option>
                                            <option>04:00</option>
                                            <option>04:15</option>
                                            <option>04:30</option>
                                            <option>04:45</option>
                                            <option>05:00</option>
                                            <option>05:15</option>
                                            <option>05:30</option>
                                            <option>05:45</option>
                                            <option>06:00</option>
                                            <option>06:15</option>
                                            <option>06:30</option>
                                            <option>06:45</option>
                                            <option>07:00</option>
                                            <option>07:15</option>
                                            <option>07:30</option>
                                            <option>07:45</option>
                                            <option>08:00</option>
                                            <option>08:15</option>
                                            <option>08:30</option>
                                            <option>08:45</option>
                                            <option>09:00</option>
                                            <option>09:15</option>
                                            <option>09:30</option>
                                            <option>09:45</option>
                                            <option>10:00</option>
                                            <option>10:15</option>
                                            <option>10:30</option>
                                            <option>10:45</option>
                                            <option>11:00</option>
                                            <option>11:15</option>
                                            <option>11:30</option>
                                            <option>11:45</option>
                                            <option selected="selected">12:00</option>
                                            <option>12:15</option>
                                            <option>12:30</option>
                                            <option>12:45</option>
                                            <option>13:00</option>
                                            <option>13:15</option>
                                            <option>13:30</option>
                                            <option>13:45</option>
                                            <option>14:00</option>
                                            <option>14:15</option>
                                            <option>14:30</option>
                                            <option>14:45</option>
                                            <option>15:00</option>
                                            <option>15:15</option>
                                            <option>15:30</option>
                                            <option>15:45</option>
                                            <option>16:00</option>
                                            <option>16:15</option>
                                            <option>16:30</option>
                                            <option>16:45</option>
                                            <option>17:00</option>
                                            <option>17:15</option>
                                            <option>17:30</option>
                                            <option>17:45</option>
                                            <option>18:00</option>
                                            <option>18:15</option>
                                            <option>18:30</option>
                                            <option>18:45</option>
                                            <option>19:00</option>
                                            <option>19:15</option>
                                            <option>19:30</option>
                                            <option>19:45</option>
                                            <option>20:00</option>
                                            <option>20:15</option>
                                            <option>20:30</option>
                                            <option>20:45</option>
                                            <option>21:00</option>
                                            <option>21:15</option>
                                            <option>21:30</option>
                                            <option>21:45</option>
                                            <option>22:00</option>
                                            <option>22:15</option>
                                            <option>22:30</option>
                                            <option>22:45</option>
                                            <option>23:00</option>
                                            <option>23:15</option>
                                            <option>23:30</option>
                                        </select>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-lg-10 order-xl-12 order-md-2 order-sm-2 order-lg-12 order-xs-2">
                            <div class="form-row dvParent">
                                <div class="col-12 col-lg-4 col-xl-4">
                                    <div class="dvLabel d-flex justify-content-between">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="checkbox" id="chkDropoffLocation" checked>
                                                <span class="checkmark"></span>
                                            </span>
                                            <span class="h7 d-inline-block ml-2" data-i18n="car-return-same-location">Return to same location?</span>
                                        </label>
                                    </div>

                                    <div class="form-group dvInput1 fade-out ">
                                        <label class="label" data-i18n="car-dropoff-location">Drop off location?</label>
                                        <div class="dvInputGroup dvDopoffLocation input-group">
                                            <input type="text" class="form-control" id="txtDopoffLocation" data-i18n="[placeholder]car-enter-location" placeholder="Enter Location">
                                            <input type="hidden" id="hndDopoffLocationId" value="">
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-white">
                                                    <i class="fa-solid fa-location-dot"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-lg-3 col-xl-3">
                                    <div class="dvLabel d-flex justify-content-between">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="checkbox" id="chkDriverAge" checked>
                                                <span class="checkmark"></span>
                                            </span>
                                            <span class="h7 d-inline-block ml-2" data-i18n="car-driver-age-years">Driver aged 30-65 years?</span>
                                        </label>
                                    </div>

                                    <div class="form-group dvInput2 fade-out">
                                        <label class="label" data-i18n="car-driver-age">Driver age</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" onkeypress="return validateNumber(event)" id="txtDriverAge" data-i18n="[placeholder]car-enter-driver-age" placeholder="Please enter driver age">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-lg-3 col-xl-3" style="display: none;">
                                    <div class="dvLabel d-flex justify-content-between">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="checkbox" id="chkDiscountCode">
                                                <span class="checkmark"></span>
                                            </span>
                                            <span class="h7 d-inline-block ml-2" data-i18n="car-discount-code1">Discount code?</span>
                                        </label>
                                    </div>

                                    <div class="form-group dvInput3 fade-out">
                                        <label class="label" data-i18n="car-discount-code">Discount code</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="txtDiscountCode" placeholder="Please enter Discount code">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-2 order-xl-2 col-lg-2 order-lg-2 order-md-12 order-sm-12 order-xs-12 mt-4">
                            <button class="btn btn-one w-100" type="submit" onclick="var retvalue = CarValidation(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" data-i18n="car-search-btn">Search Car</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // this script is to toggle inputs in carsearch section
            const checkboxes = document.querySelectorAll('.dvParent input[type="checkbox"]');
            const dvInput = [".dvInput1", ".dvInput2", ".dvInput3"].map((selector) => document.querySelector(selector));
            checkboxes.forEach((checkbox, index) => {
                checkbox.addEventListener("change", function () {
                    dvInput.forEach((div, i) => {
                        if (i === 0) {
                            div.classList.toggle("fade-in", !checkboxes[0].checked);
                            div.classList.toggle("fade-out", checkboxes[0].checked);
                        } else if (i === 1) {
                            div.classList.toggle("fade-in", !checkboxes[1].checked);
                            div.classList.toggle("fade-out", checkboxes[1].checked);
                        } else if (i === 2) {
                            div.classList.toggle("fade-in", checkboxes[2].checked);
                            div.classList.toggle("fade-out", !checkboxes[2].checked);
                        }
                    });
                });
            });

            // this script is to keep multiple autocomplete inside their respective input's fields div section
            function initAutocomplete(inputSelector, containerSelector) {
                $(inputSelector).autocomplete({
                    appendTo: $(containerSelector),
                    open: function (event, ui) {
                        var $autocompleteMenu = $(this).autocomplete("widget");
                        $autocompleteMenu.addClass("myClass").css({
                            "max-height": 400,
                            "overflow-x": "hidden"
                        });
                    }
                });
            }
            //just mention your id and the div className
            //initAutocomplete(".pageParentClass #id", "pageParentClass .className");
            initAutocomplete(".dvCarSearch #txtpickupLocation", ".dvCarSearch .dvPickupLocation");
            initAutocomplete(".dvCarSearch #txtDopoffLocation", ".dvCarSearch .dvDopoffLocation");
        });

        $(document).ready(function () {
            BindBanner();
            GetRedemptionOptions();
        });


    </script>
</asp:Content>
