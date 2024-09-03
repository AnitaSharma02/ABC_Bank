<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="CarList.aspx.cs" Inherits="CarList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/car.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/CarResultScript.js" type="text/javascript"></script>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
            display: none;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            GetCarList();

        });

    </script>

   
    <div class="dvCarList dvProductList">
        <div class="container-xl">
            <div class="row dvDeliveryTrack">
                <div class="col-4 mb-lg-3">
                    <div class="dvLine border d-none d-md-block px-3"></div>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center active">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">1</span>
                                <a class="h7 heading-bold bg-colour6 px-3 text-center text-colour1">Your Car</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4 mb-lg-3">
                    <div class="dvLine border d-none d-md-block px-3"></div>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">2</span>
                                <a class="h7 bg-colour6 px-3 text-center text-colour7" id="hrefBookingDetailsId" runat="server">Deal</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-4 mb-lg-3">
                    <%--<div class="dvLine border d-none d-md-block px-3"></div>--%>
                    <div class="row justify-content-md-center">
                        <div class="col-md-auto my-3">
                            <div class="d-flex flex-column flex-sm-row align-items-center">
                                <span class="d-flex align-items-center justify-content-center bg-colour2 p-3 rounded-circle w-30 h-30">3</span>
                                <a class="h7 bg-colour6 px-3 text-center text-colour7">Payment</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="dvSidebar col-lg-3 mb-3 mb-lg-0"">
                    <div class="border b-radius p-3 mb-3">
                        <div class="row">
                            <div class="col-12 leftBoxCont">
                                <div class="row">
                                    <%--<div class="col-12 col-md-12 col-lg-6 mb-1">
                                        <i class="fa-solid fa-car-side"></i>
                                    </div>--%>
                                    <div class="col-12 col-sm-6 col-md-4 col-lg-12 mb-3">
                                        <h2 class="heading6"><i class="fa-solid fa-location-dot"></i> <span data-i18n="carlist-pickup-location">PICK UP LOCATION</span></h2>
                                        <p id="spnpickuplocation" class="h7" ></p>
                                        <p id="spnpickupdate" class="h7"></p>
                                    </div>
                                    <div class="col-12 col-sm-6 col-md-4 col-lg-12 mb-3">
                                        <h2 class="heading6"><i class="fa-solid fa-location-dot"></i> <span data-i18n="carlist-car-dropoff-location">CAR DROP OFF LOCATION</span></h2>
                                        <p id="spndroppoffLocation" class="h7" ></p>
                                        <p id="spndropoffdate" class="h7"></p>
                                    </div>
                                    <div class="col-12 col-md-4 col-lg-12 mb-3">
                                        <h2 class="heading6"><i class="fa-solid fa-location-dot"></i> <span data-i18n="carlist-driver-residence-country">Driver's Residence Country</span></h2>
                                        <p id="spndriverresidenceCountry" class="h7"></p>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <button type="button" class="btn btn-one w-100 editBtn" data-i18n="carlist-edit" onclick="bindcountry()">Edit</button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 popupForm">
                                <div class="row">
                                    <div class="col-12 d-flex justify-content-between align-items-center mb-3 popupHead">
                                        <span class="heading6">Get Your Quote Now!</span>
                                        <a href="#" class="btn btn-three closeBtn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div id="CarValidationError" runat="server" data-i18n="carlist-below-fields" class="p-1 mb-2 alert alert-danger text-danger text-center h6 heading-semibold" style="display: none;"></div>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-row dvParent">
                                            <div class="col-lg-12 col-md-12 col-12 mb-2 mt-2">
                                                <label for="validationDefaultUsername" class="label" data-i18n="carlist-pickup-location">Pick up location?</label>
                                                <div class="dvInputGroup input-group dvPickupLocation">
                                                    <input type="text" class="form-control" id="txtpickupLocation" data-i18n="[placeholder]carlist-please-enter-pickup-location" placeholder="Please enter a pick-up location" aria-describedby="inputGroupPrepend2" required>
                                                    <input type="hidden" id="hndpickupLocationId" value="">
                                                    <div class="input-group-append">
                                                        <span class="input-group-text bg-colour6">
                                                            <i class="fa-solid fa-location-dot"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-12 col-lg-12 mb-2 mt-2">
                                                <label class="label" data-i18n="carlist-pickup-date">Pick-Up Date</label>
                                                <div class="dvInputGroup input-group">
                                                    <input class="input form-control" data-i18n="[value]carlist-enter-date" value="Enter Date" onfocus="placeholderOnFocus(this);"
                                                        type="text" id="txtpickupDate" readonly="readonly" />
                                                    <div class="input-group-append">
                                                        <span class="input-group-text bg-colour6">
                                                            <i class="fa-regular fa-calendar"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-12 col-lg-12 mb-2 mt-2">
                                                    <label for="exampleFormControlSelect1" class="label" data-i18n="carlist-pickup-time">Pick-Up Time</label>
                                                    <div class="dvInput input-group">
                                                    <select class="form-control" id="ddlPickupTime">
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
                                                <div class="col-12 mb-3">
                                                <div class="dvLabel">
                                                    <label class="checkbox-container d-flex">
                                                    <span class="d-inline-block">
                                                        <input type="checkbox" id="chkDropoffLocation"  />
                                                        <span class="checkmark"></span>
                                                    </span>
                                                    <span class="d-inline-block ml-2 heading-medium h8 pt-1" data-i18n="carlist-return-car-to-same-location"> Return car to same location?</span
                                                    >
                                                    </label>
                                                </div>
                                                </div>
                                            <div class="col-12 col-lg-12 col-xl-12">
                                                <div class="form-group dvInput1 fade-out">
                                                    <label class="label"  data-i18n="carlist-car-dropoff-location">Car Drop off location?</label>
                                                    <div class="dvInputGroup input-group dvDopoffLocation">
                                                        <input type="text" class="form-control" id="txtDopoffLocation">
                                                        <input type="hidden" id="hndDopoffLocationId" value="">
                                                        <div class="input-group-append">
                                                        <span class="input-group-text bg-colour6">
                                                            <i class="fa-solid fa-location-dot"></i>
                                                        </span>
                                                    </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 col-md-12 col-lg-12 mb-2 mt-2">
                                                <label class="label" data-i18n="carlist-drop-off-date">Drop off date</label>
                                                <div class="dvInputGroup input-group">
                                                    <input class="input form-control" value="Enter Date" onfocus="placeholderOnFocus(this);"
                                                        onblur="placeholderOnFocus(this);" data-i18n="[value]carlist-check-out-input" type="text" id="txtDropoffDate" readonly="readonly" />
                                                    <div class="input-group-append">
                                                        <span class="input-group-text bg-colour6">
                                                            <i class="fa-regular fa-calendar"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-12 col-lg-12 mb-2 mt-2">
                                                    <label for="exampleFormControlSelect2" class="label" data-i18n="carlist-pickup-time">Pick-Up Time</label>
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
                                                <div class="col-12 mb-3">
                                                <div class="dvLabel">
                                                    <label class="checkbox-container d-flex">
                                                    <span class="d-inline-block">
                                                        <input type="checkbox" id="chkDriverAge" />
                                                        <span class="checkmark"></span>
                                                    </span>
                                                    <span class="d-inline-block ml-2 heading-medium h8 pt-1" data-i18n="carlist-driver-aged"> Driver aged 30-65 years?</span
                                                    >
                                                    </label>
                                                </div>
                                                </div>
                                            <div class="col-12 col-lg-12 col-xl-12">
                                                    <div class="form-group dvInput2">
                                                    <label class="label" id="lbldriverage" data-i18n="carlist-driver-age">Driver age</label>
                                                    <div class="input-group">
                                                        <input type="text" class="form-control" id="txtDriverAge" onkeypress="return validateNumber(event)">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 col-md-12 col-lg-12 mb-2 mt-2" style="display:none;">
                                                <label class="label" data-i18n="carlist-discount">Discount</label>
                                                <div class="dvInputGroup input-group">
                                                    <input class="input form-control" value="Discount" onfocus="placeholderOnFocus(this);"
                                                        type="text" id="Text1" runat="server" readonly="readonly" />
                                                    <div class="input-group-append">
                                                        <span class="input-group-text bg-colour6">
                                                                <i class="fa-solid fa-percent"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="col-12 mb-3">
                                                <div class="dvLabel">
                                                    <label class="checkbox-container d-flex">
                                                    <span class="d-inline-block">
                                                        <input type="checkbox" id="gridCheck3" checked/>
                                                        <span class="checkmark"></span>
                                                    </span>
                                                    <span class="d-inline-block ml-2 heading-medium h8 pt-1" data-i18n="carlist-driver-residenc-country">Driver's Residence Country: </span ><span id="spndriverCountry" class="ml-1 heading-medium h8 pt-1"> </span>
                                                    </label>
                                                </div>
                                                </div>
                                            <div class="col-12 col-lg-12 col-xl-12">
                            
                                                <div class="form-group dvInput3 fade-out">
                                                    <label class="label" data-i18n="carlist-driver-residenc-country">Driver's Residence Country</label>
                                                    <div class="">
                                                            <div class="dvPickupLocation w-100 input-group">
                                                            <%--   <input type="text" class="form-control" id="txtDriverResidence">--%>
                                                                <select id="txtDriverResidence" class="form-control" onchange="handleSelectChange(event)"></select>
                                                            <input type="hidden" id="hnddriverLocationId" value="" />
                                                                <input type="hidden" id="hnddriverLocationname" value="" />
                                                                <%-- <div class="input-group-append">
                                                                    <div class="input-group-text"><i class="fa-solid fa-angle-down h8"></i></div>
                                                            </div>--%>
                                                            </div>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="col-12 col-lg-12 col-xl-12">
                                                    <button class="btn btn-one" type="submit" onclick="var retvalue = CarValidation(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" data-i18n="carlist-search">Search</button>
                                                </div>
                                        </div>
                                    </div>

              

                                </div>
                            </div>
                        </div>
                    </div>

                    <button data-toggle="modal" data-target="#dvFilterModal" type="button" class="btn btn-one w-100 d-lg-none" data-i18n="flightlist-button-filters">Filter </button>

                    <div class="dvFilter modal fade mb-3" id="dvFilterModal" tabindex="-1">
                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                            <div class="modal-content border-0 b-radius">
                                <div class="modal-header border-0 p-0">
                                    <div class="modal-title border-0 p-3">
                                        <h5 class="h6 heading-semibold text-colour1">
                                            <i class="fa fa-filter mx-0"></i>
                                            <span data-i18n="carlist-filter-results">Filters</span>
                                        </h5>
                                    </div>
                                    <button type="button" class="close d-lg-none px-3" data-dismiss="modal">
                                        <i class="fa-solid fa-xmark"></i>
                                    </button>

                                    <%--<i class="fa fa-filter mx-0"></i>
                                    <span class="h7 heading-semibold" data-i18n="carlist-filter-results">Filters Results</span>--%>
                                </div>
                                <div class="modal-body p-lg-0">
                                    <div class="accordion" id="filter-accordion">
                                        <div class="card my-3 mb-lg-3 mt-lg-0">
                                            <div class="card-header p-0">
                                                <h2 class="mb-0">
                                                    <button class="btn btn-block text-left heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse1"><span class="heading6" data-i18n="carlist-passengers">Passengers</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                                </h2>
                                            </div>
                                            <div id="collapse1" class="collapse- show" data-parent="#filter-accordion">
                                                <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                    <div class="dvLabel">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input name="PASSENGERS" onchange="FilterCarList('');" value="3,4" type="checkbox" id="gridCheckPASSENGERS3-4" />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <span class="d-inline-block ml-2">3 to 4</span>
                                                        </label>
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input name="PASSENGERS" onchange="FilterCarList('');" value="5,6" type="checkbox" id="gridCheckPASSENGERS5-6">
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <span class="d-inline-block ml-2">5 to 6 </span>
                                                        </label>
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input name="PASSENGERS" onchange="FilterCarList('');" value="7,8" type="checkbox" id="gridCheckPASSENGERS7-8">
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <span class="d-inline-block ml-2">7 to 8 </span>
                                                        </label>
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input name="PASSENGERS" onchange="FilterCarList('');" value="8,9" type="checkbox" id="gridCheckPASSENGERS8-9">
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <span class="d-inline-block ml-2">8 to 9 </span>
                                                        </label>
                                                    </div>
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
                                                    <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse2"><span class="heading6" data-i18n="carlist-transmission">Transmission</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                                </h2>
                                            </div>
                                            <div id="collapse2" class="collapse- show" data-parent="#filter-accordion">
                                                <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                    <div class="dvLabel">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input class="form-check-input chkTransmission" name="Transmission" onchange="FilterCarList();" type="checkbox" data-i18n="[value]carlist-manual" value="Manual" id="gridCheckManual">
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <span class="d-inline-block ml-2" data-i18n="carlist-manual">Manual</span>
                                                        </label>
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input class="form-check-input chkTransmission" name="Transmission" onchange="FilterCarList();" type="checkbox" data-i18n="[value]carlist-automatic" value="Automatic" id="gridCheckAutomatic">
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <span class="d-inline-block ml-2" data-i18n="carlist-automatic">Automatic</span>
                                                        </label>
                                                    </div>
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
                                <div class="modal-footer justify-content-center border-left border-right border-bottom p-lg-2">
                                    <button type="button" class="btn btn-one w-100" onclick="FilterCarList('All');" data-i18n="carlist-reset">Reset</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-9">
        <div class="dvCarSelection row flex-nowrap scroll-hoz mx-0 equal-col">
            <div class="col-5 col-sm-3 mb-3">
                <label class="checkbox-container b-radius">
                    <input type="checkbox" checked name="VehicleType" value="small" onchange="FilterCarList('');" />
                    <div class="checkmark d-block p-2 text-center b-radius">
                        <img src="https://cdn.enjoytravel.com/img/site-images/small-car.jpg" alt="" />
                        <p class="h7 heading-bold text-colour7" data-i18n="carlist-small">Small</p>
                        <%--<span class="d-none d-lg-block">from 119,39C</span>--%>
                    </div>
                </label>
            </div>
            <div class="col-5 col-sm-3 mb-3">
                <label class="checkbox-container b-radius">
                    <input type="checkbox" name="VehicleType" value="medium" onchange="FilterCarList('');" />
                    <div class="checkmark d-block p-2 text-center b-radius">
                        <img src="https://cdn.enjoytravel.com/img/site-images/small-car.jpg" alt="" />
                        <p class="h7 heading-bold text-colour7" data-i18n="carlist-medium">Medium</p>
                        <%--<span class="d-none d-lg-block">from 119,39C</span>--%>
                    </div>
                </label>
            </div>
            <div class="col-5 col-sm-3 mb-3">
                <label class="checkbox-container b-radius">
                    <input type="checkbox" name="VehicleType" value="large" onchange="FilterCarList('');" />
                    <div class="checkmark d-block p-2 text-center b-radius">
                        <img src="https://cdn.enjoytravel.com/img/site-images/small-car.jpg" alt="" />
                        <p class="h7 heading-bold text-colour7" data-i18n="carlist-large">Large</p>
                        <%--  <span class="d-none d-lg-block">from 119,39C</span>--%>
                    </div>
                </label>
            </div>
            <div class="col-5 col-sm-3 mb-3">
                <label class="checkbox-container b-radius">
                    <input type="checkbox" name="VehicleType" value="luxury" onchange="FilterCarList('');" />
                    <div class="checkmark d-block p-2 text-center b-radius">
                        <img src="https://cdn.enjoytravel.com/img/site-images/small-car.jpg" alt="" />
                        <p class="h7 heading-bold text-colour7" data-i18n="carlist-luxury">Luxury</p>
                        <%--<span class="d-none d-lg-block">from 119,39C</span>--%>
                    </div>
                </label>
            </div>
        </div>
        <div class="dvTotalRecords my-3">
            <div class="b-radius bg-colour5 p-3">
                <span id="spancarcount" class="heading6 text-colour1">0</span>
                <span class="heading6 text-colour1" data-i18n="carlist-cars-found">cars found</span>
                <a href="#" class="link1 ml-3">
                    <i class="fa-solid fa-location-dot"></i>
                    <span class="" data-i18n="carlist-view-locations-on-map">View locations on a map</span>
                </a>
            </div>
        </div>

        <div id="divCarListContainer">
        </div>

    </div>
            </div>
        </div>
    </div>

    <!-- CarLarge modal pop up start-->
    <div class="dvCommonModal dvMoreInfoModal modal fade pr-lg-0" id="dvMoreInfoModal">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable modal-lg">
            <div class="modal-content">
                 <div class="modal-header border-0">         
                    <h5 class="modal-title">
                        <i class="fa-solid fa-circle-info"></i>
                        <span data-i18n="carlist-important-information">Important information</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                  </div>                
                <div class="modal-body" id="divmoreInfoDetails"></div>
            </div>
        </div>
    </div>
    <!-- CarLarge modal pop up end-->


    <script>
        const editBtn = document.querySelector(".editBtn");
        const leftBoxCont = document.querySelector(".leftBoxCont");
        const popupForm = document.querySelector(".popupForm");
        popupForm.style.display = 'none';
        const closeBtn = document.querySelector(".closeBtn");
        editBtn.addEventListener('click', function () {
            leftBoxCont.style.display = 'none';
            popupForm.style.display = 'block';
        });
        closeBtn.addEventListener("click", function () {
            leftBoxCont.style.display = "block";
            popupForm.style.display = "none";
        });
    </script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const checkboxes = document.querySelectorAll('.dvParent input[type="checkbox"]');
            const dvInput = [".dvInput1", ".dvInput2", ".dvInput3"].map((selector) => document.querySelector(selector));
            checkboxes.forEach((checkbox, index) => {
                checkbox.addEventListener("change", function () {
                    dvInput.forEach((div, i) => {
                        if (i === 2) {

                            div.classList.toggle("fade-in", !checkboxes[2].checked);
                            div.classList.toggle("fade-out", checkboxes[2].checked);
                        }
                    });
                });
            });
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
            initAutocomplete(".dvCarList #txtpickupLocation", ".dvCarList .dvPickupLocation");
            initAutocomplete(".dvCarList #txtDopoffLocation", ".dvCarList .dvDopoffLocation");
        });


    </script>
</asp:Content>
