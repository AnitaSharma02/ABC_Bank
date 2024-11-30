<%@ Page Title="Car Search" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarSearch.aspx.cs" Inherits="CarSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/Car.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/CarResultScript.js" type="text/javascript"></script>
    <style type="text/css">
        #sitemap,
        .dvInnerBanner{
            display: none;
        }         
    </style>
    <div class="dvCarSearch mb-5">
        <div class="container-xl">
            <div class="dvForm row">
                <div class="bg-colour2 p-3">
                    <div class="row">
                        <div class="col-12">
                            <div id="CarValidationError" runat="server" class="dvErrors p-1 mb-2 alert alert-danger text-center h6 heading-semibold" style="display: none;"></div>
                        </div>
                        <div class="col-12 col-lg-10">
                            <div class="row">
                                <div class="col-lg-4 col-md-12 col-12 mb-3">
                                    <label for="validationDefaultUsername" class="label">Pick up location?</label>
                                    <div class="dvPickupLocation dvInputGroup input-group">
                                        <input type="text" class="form-control" id="txtpickupLocation" placeholder="Please enter a pick-up location" aria-describedby="inputGroupPrepend2">
                                        <input type="hidden" id="hndpickupLocationId" value="">
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-colour6">
                                                <i class="fa-solid fa-location-dot"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label class="label">Pick-Up Date</label>
                                    <div class="dvInputGroup input-group">
                                        <input class="input form-control" placeholder="Enter Date"
                                            type="text" id="txtpickupDate" readonly="readonly" />
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-colour6">
                                                <i class="fa-regular fa-calendar"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label for="exampleFormControlSelect1" class="label">Time</label>
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
                                    <label class="label">Drop off date</label>
                                    <div class="dvInputGroup input-group">
                                        <input class="input datePicker form-control" placeholder="Enter Date" type="text" id="txtDropoffDate" readonly="readonly" />
                                        <div class="input-group-append">
                                            <span class="input-group-text bg-colour6">
                                                <i class="fa-regular fa-calendar"></i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-2 mb-3">
                                    <label for="exampleFormControlSelect2" class="label">Time</label>
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
                            <div class="dvParent row">
                                <div class="col-12 col-md-6 col-lg-4 mb-3 mb-lg-0 z-1">
                                    <div class="dvLabel d-flex justify-content-between">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="checkbox" id="chkDropoffLocation" checked>
                                                <span class="checkmark"></span>
                                            </span>
                                            <span class="h7 d-inline-block ml-2">Return to same location?</span>
                                        </label>
                                    </div>

                                    <div class="dvInput1 fade-out">
                                        <label class="label">Drop off location?</label>
                                        <div class="dvDopoffLocation dvInputGroup input-group">
                                            <input type="text" class="form-control" id="txtDopoffLocation" placeholder="Enter Location">
                                            <input type="hidden" id="hndDopoffLocationId" value="">
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-colour6">
                                                    <i class="fa-solid fa-location-dot"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4 mb-3 mb-lg-0">
                                    <div class="dvLabel d-flex justify-content-between">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="checkbox" id="chkDriverAge" checked>
                                                <span class="checkmark"></span>
                                            </span>
                                            <span class="h7 d-inline-block ml-2">Driver aged 30-65 years?</span>
                                        </label>
                                    </div>

                                    <div class="dvInput2 fade-out">
                                        <label class="label">Driver age</label>
                                        <div class="dvInput input-group">
                                            <input type="text" class="form-control" onkeypress="return validateNumber(event)" id="txtDriverAge" placeholder="Please enter driver age">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6 col-lg-4" style="display: none;">
                                    <div class="dvLabel d-flex justify-content-between">
                                        <label class="checkbox-container d-flex">
                                            <span class="d-inline-block">
                                                <input type="checkbox" id="chkDiscountCode">
                                                <span class="checkmark"></span>
                                            </span>
                                            <span class="h7 d-inline-block ml-2">Discount code?</span>
                                        </label>
                                    </div>

                                    <div class="dvInput3 fade-out">
                                        <label class="label">Discount code</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="txtDiscountCode" placeholder="Please enter Discount code">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-2 order-xl-2 col-lg-2 order-lg-2 order-md-12 order-sm-12">
                            <label class="label d-none d-lg-inline-block"></label>
                            <button class="btn btn-one w-100" type="submit" onclick="var retvalue = CarValidation(); event.returnValue= retvalue;event.preventDefault(); return retvalue;">Search Car</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>


        <%-- this div is temporary because we dont have content. Remove this div if you get some content on this page --%>
        <%--<div class="d-none d-lg-block">
            <div style="padding:15rem 0">
                
            </div>
        </div>--%>
        <%-- this div is temporary because we dont have content. Remove this div if you get some content on this page --%>

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

            // for scrollTop
            let offsetTop;
            // Adjust offset based on screen width breakpoints
            if (window.innerWidth < 576) {
                offsetTop = 150; // Offset for screens below 576px
            } else if (window.innerWidth < 768) {
                offsetTop = 450; // Offset for screens between 576px and 767px
            } else if (window.innerWidth < 992) {
                offsetTop = 550; // Offset for screens between 768px and 991px
            } else if (window.innerWidth < 1200) {
                offsetTop = 550; // Offset for screens between 992px and 1199px
            } else {
                offsetTop = 550; // Offset for screens 1200px and above
            }

            // Smooth scroll to the top with the calculated offset
            $('html, body').animate({
                scrollTop: offsetTop
            }, 600); // Smooth scroll duration

        });


    </script>
</asp:Content>
