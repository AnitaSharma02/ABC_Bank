<%@ Page Title="Flight List" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="FlightList.aspx.cs" Inherits="FlightList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <script src="Jquery/FlightSearchScript.js" type="text/javascript"></script>
    <script src="Jquery/FlightSearch.js" type="text/javascript"></script>
    <script src="Jquery/CoreFlightFilter.js" type="text/javascript"></script>
    <script src="Jquery/JTemplateCreator.js" type="text/javascript"></script>
    <%--<link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="Css/jquery.ui.slider.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="Css/dropDown.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/jquery.selectric.min.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="\Css/flight.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var edit = getUrlVars()["edit"];
            if (edit == "1") {
                $('#modify_search').addClass('show');
            }
            $('#btnfliter').click(function () {
                $('#divSearch').addClass('MoveLeft');
                $('#divSearch').show();
                $('#divBlockAll').css('display', 'block');
                $('#divBlockAll').show();
                 $('#divSearch').stop().animate({ 'left': '0px' }, 500);
                return false;
            });
            $('#divBlockAll').click(function () {
                $('#divBlockAll').hide();
                 $('#divSearch').stop().animate({ 'left': '-768px' }, 200);

            });
            //show datepicker onclick of icon
            $(".dvTxtDepart .input-group-append .input-group-text").on("click", function () {
                $("#txtDepart").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvTxtReturn .input-group-append .input-group-text").on("click", function () {
                $("#txtReturn").datepicker("show");
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
            initAutocomplete(".dvFlightList #textBoxFrom", ".dvFlightList .dvTextBoxFrom");
            initAutocomplete(".dvFlightList #textBoxTo", ".dvFlightList .dvTextBoxTo");
        });
        //for returning to back page 
        document.getElementsByClassName("btnBack").onclick = function () {
        location.href = "/FlightSearch.aspx";
        };

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        document.addEventListener("DOMContentLoaded", (event) => {
            function checkAndObserve(selector, callback) {
                const element = document.querySelector(selector);

                if (element) {
                    callback(element);
                } else {
                    const observer = new MutationObserver((mutations, obs) => {
                        const element = document.querySelector(selector);
                        if (element) {
                            callback(element);
                            obs.disconnect();
                        }
                    });
                    observer.observe(document.body, { childList: true, subtree: true });
                }
            }

            function observeAttributes(targetNode, tripSelector, hiddenInputSelector, divSelector) {
                const observer = new MutationObserver((mutationsList) => {
                    for (const mutation of mutationsList) {
                        if (mutation.type === 'attributes' && mutation.attributeName === 'aria-labelledby') {
                            const trip = $(tripSelector + " option:selected").val();
                            if (trip == 'Return') {
                                $(hiddenInputSelector).val('true');
                                $(divSelector).show();
                                $(tripSelector).val("Return");
                            } else if (trip == 'One Way') {
                                $(hiddenInputSelector).val('false');
                                $(divSelector).hide();
                                $(tripSelector).val("One Way");
                            }
                        }
                    }
                });
                observer.observe(targetNode, { attributes: true });
            }

            checkAndObserve('#returnSelectMenu-button', (targetNode) => {
                observeAttributes(targetNode, '#returnSelectMenu', '#hdntrip', '#divrtn');
            });
        });
        function IncreaseCount(Type, maxVal) {

            var $parentElm = $(".increaseQty" + Type).parents(".dvQtySelector" + Type);

            $(this).addClass("clicked");
            setTimeout(function () {
                $(".clicked").removeClass("clicked");
            }, 100);
            var value = $parentElm.find(".qtyValue" + Type).val();
            if (value < maxVal) {
                value++;
            }
            $parentElm.find(".qtyValue" + Type).val(value);

        }

        function DecreaseCount(Type) {
            var minval = 0;
            var $parentElm = $(".increaseQty" + Type).parents(".dvQtySelector" + Type);
            $(this).addClass("clicked");
            setTimeout(function () {
                $(".clicked").removeClass("clicked");
            }, 100);
            var value = $parentElm.find(".qtyValue" + Type).val();

            if (Type == 'Adult' || Type == 'AdultDomestic') {
                minval = 1;
            }

            if (value > minval) {
                value--;
            }
            $parentElm.find(".qtyValue" + Type).val(value);

        }


    </script>
    <!--jQuery to hide filter popup div-->
    <script type="text/javascript">
            $(document).ready(function () {
                $("#btnClosePopup").click(function () {
                    $("#divSearch").hide();
                });
            });

            document.getElementsByClassName(".btnBack").onclick = function () {
                location.href = "FlightSearch.aspx";
            };
    </script>
    <style>
        #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;}
        
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\"><img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="FlightSearch.aspx"> Flight Search</a></li>
                    <li class="breadcrumb-item">Flight List</li>
                </ul>
            </nav>
        </div>
    </div>
    <div class="dvFlightList dvProductList pb-4">
        <div class="container-xl">
            <div class="row">
                <div class="dvFilter modal fade col-lg-3" id="dvFilterModal" tabindex="-1">
                    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content border-0 b-radius">
                            <div class="modal-header border-0 d-lg-block p-0">
                                 <div class="modal-title dvTotalRecords border-0 p-3">
                                    <p class="heading6 text-colour1"><span data-i18n="flightlist-total">Total Flights Found</span> <span class="filter-text" id="lblNoofFlight"></span></p>
                                </div>
                                <button type="button" class="close d-lg-none px-3" data-dismiss="modal">
                                     <i class="fa-solid fa-xmark"></i>
                                 </button>
                            </div>
                             <div class="modal-body p-lg-0">
                                <div class="accordion" id="filter-accordion">
                                    <div class="card my-3">
                                        <div class="card-header p-0">
                                            <h2 class="mb-0">
                                                <button class="btn btn-block text-left heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse2"><span data-i18n="flightlist-stops" class="heading6 mb-2 text-capitalize">Stops</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>
                                        <div id="collapse2" class="collapse- show" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="">
                                                    <div class="dvLabel divStops airList"></div>
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
                                                <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse1"><span data-i18n="flightlist-total-points" class="heading6 mb-2 text-capitalize">Total Points</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>

                                        <div id="collapse1" class="collapse- show" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvRangeSlider flt-stop-chkbox robothik f_sliderW fl">
                                                    <div id="priceSlider" class="PriceSlider">
                                                    </div>
                                                    <span id="priceRange" class="h7 heading-regular text-colour7 d-block mt-2"></span>
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
                                                <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse3"><span data-i18n="flightlist-airlines" class="heading6 mb-2 text-capitalize">Airlines</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>
                                        <div id="collapse3" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvLabel airList" id="DivAirLinesList"></div>
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
                                                <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse4"><span data-i18n="flightlist-total-duration" class="heading6 mb-2 text-capitalize">Total Duration</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>
                                        <div id="collapse4" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvRangeSlider flt-stop-chkbox robothik f_sliderW fl">
                                                    <div id="durationSlider" class="PriceSlider"></div>
                                                    <span id="durationRange" class="h7 heading-regular text-colour7 d-block mt-2"></span>
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
                                                <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse5"><span data-i18n="flightlist-departure" class="heading6 mb-2 text-capitalize">Departure Time</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>

                                        <div id="collapse5" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvRangeSlider flt-stop-chkbox robothik f_sliderW fl">
                                                    <div id="departureSlider" class="PriceSlider"></div>
                                                    <span id="departureRange" class="h7 heading-regular text-colour7 d-block mt-2"></span>
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
                                                <button class="btn btn-block text-left collapsed heading-semibold" type="button" data-toggle="collapse-" data-target="#collapse6"><span data-i18n="flightlist-arrival-time" class="heading6 mb-2 text-capitalize">Arrival Time</span> <span class="arrow-icon"><i class="fa fa-caret-up-"></i></span></button>
                                            </h2>
                                        </div>

                                        <div id="collapse6" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvRangeSlider flt-stop-chkbox robothik f_sliderW fl">
                                                    <div id="arrivalSlider" class="PriceSlider"></div>
                                                    <span id="arrivalRange" class="h7 heading-regular text-colour7 d-block mt-2"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card">
                                        <div class="applyBtn" style="display: none;">
                                            <button type="button" id="btnClosePopup" class="btn btn-one" value="Apply">Apply</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     
                </div>
                <div class="col-lg-9">
                    <div class="row">
                        <div class="dvModify col-12 mb-3">
                            <div class="bg-colour2">
                                <div class="d-flex flex-wrap justify-content-between align-items-center py-2 py-lg-1 px-2 px-lg-3 mb-1">
                                    <button data-toggle="modal" data-target="#dvFilterModal" type="button" class="btn btn-one col-12 d-lg-none mb-2" data-i18n="flightlist-button-filters">Filter </button>
                                    <asp:Label ID="LabelYourSearchDetails" runat="server" CssClass="Content_Style h6 heading-regular text-colour7 col-auto px-0 mb-2 mb-lg-0" Text=""></asp:Label>
                                    <button class="btn btn-one arrowBtn col-auto d-flex collapsed" type="button" data-toggle="collapse" onclick="showModifyFlight()" data-target="#dvForm"><span class="d-inline-block" data-i18n="flightlist-modify">Modify</span> <span class="arrow-icon ml-2"><i class="fa fa-caret-up"></i></span></button>
                                </div>
                            </div>
                            <div id="dvForm" class="dvForm collapse bg-colour2 py-3">
                                <div class="bg-colour2 px-3">  
                                    <div class="p-1 mb-2 alert alert-danger text-center text-danger h6 heading-semibold" id="requiredValidation" style="display:none;"></div>
                                        <div class="row dvLabel r- d-none">
                                                <div class="col-6 col-sm-4 col-md-3 col-lg-2 selTravel mb-3">
                                                    <label class="radio-container d-flex">
                                                        <span class="d-inline-block ml-1">
                                                        <input id="oneli" class="radio " name="iternary" onchange="return onwayShow();" type="radio"><span data-i18n="flightsearch-one-way" class="pr-3 pl-2 h6 heading-regular text-colour7">One-Way</span>
                                                        <span class="radiomark"></span>
                                                        </span>
                                                    </label>
                                                </div>
                   
                                                <div class="col-6 col-sm-4 col-md-9 col-lg-10 selTravel mb-3">
                                                    <label class="radio-container d-flex">
                                                        <span class="d-inline-block ml-1">
                                                        <input  id="retli" class="radio" name="iternary" onchange="return RoundTripShow();" type="radio" checked="checked"><span class="pr-3 pl-2 h6 heading-regular text-colour7" data-i18n="flightsearch-return">Return</span>
                                                        <span class="radiomark"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>
                                        <div class="row">
                                            <div class="dvReturnSelectMenu col-6 col-md-4 col-sm-4 col-lg-2 mb-3">
                                                <select id="returnSelectMenu" onchange="JourneyTypeChanged(this);">
                                                    <option>Return</option>
                                                    <option>One Way</option>
                                                    </select>
                                                <%--<div class="dvDropdown dropdown">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        Return
                                                    </button>
                                                    <div class="dropdown-menu prevent-close">
                                                        <a class="dropdown-item h7" href="#">One-Way</a>
                                                        <a class="dropdown-item h7" href="#">Return</a> 
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="col-6 col-md-4 col-sm-4 col-lg-2 mb-3">
                                                <select id="economySelectMenu">
                                                    <option selected="selected">Economy</option>
                                                    <option>Business</option>
                                                    <option>First</option>
                                                </select>
                                                <%--<div class="dvDropdown dropdown text-sm-center text-lg-left">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        Economy
                                                    </button>
                                                    <div class="dropdown-menu prevent-close dropdown-menu-right dropdown-menu-sm-left">
                                                        <a class="dropdown-item h7" href="#">Business</a>
                                                        <a class="dropdown-item h7" href="#">First</a> 
                                                    </div>
                                                    </div>--%>
                                            </div>
                                            <div class="col-12 col-md-4 col-sm-4 col-lg-3 mb-3">
                                                <div class="dvDropdown dropdown text-sm-right text-lg-left">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        Passenger
                                                    </button>
                                                    <div class="dropdown-menu prevent-close w-250 dropdown-menu-sm-right">
                                                    <div class="dvQtySelectorAdult dvQtySelector dropdown-item d-flex align-items-center">
                                                        <div class="col-6 special-text h7">Adult(s) 12+ Yrs</div>
                                                        <div class="col-6 d-flex align-items-center pr-0">
                                                            <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('Adult')" class="fa fa-minus decreaseQtyAdult border p-1 b-radius"></i></div>
                                                            <div class="col-4 text-center px-0"><input type="text" id="qtyValueAdult" class="form-control bg-transparent border-0 text-center qtyValueAdult" value="1" readonly="readonly"></div>
                                                            <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('Adult',4)" class="fa fa-plus increaseQtyAdult border p-1 b-radius"></i></div>
                                                        </div>
                                                    </div>
                                                    <div class="dvQtySelectorChild dvQtySelector dropdown-item d-flex align-items-center">
                                                        <div class="col-6 special-text h7">Child(ren) 2 - 11 Yrs</div>
                                                        <div class="col-6 d-flex align-items-center pr-0">
                                                            <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('Child')" class="fa fa-minus decreaseQtyChild border p-1 b-radius"></i></div>
                                                            <div class="col-4 text-center px-0"><input type="text" id="qtyValueChild" class="form-control bg-transparent border-0 text-center qtyValueChild" value="0" readonly="readonly"></div>
                                                            <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('Child',2)" class="fa fa-plus increaseQtyChild border p-1 b-radius"></i></div>
                                                        </div>
                                                    </div> 
                                                    <div class="dvQtySelectorInfant dvQtySelector dropdown-item d-flex align-items-center">
                                                        <div class="col-6 special-text h7">Infant(s) Below 2 Yrs</div>
                                                        <div class="col-6 d-flex align-items-center pr-0">
                                                            <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('Infant')" class="fa fa-minus decreaseQtyInfant border p-1 b-radius"></i></div>
                                                            <div class="col-4 text-center px-0"><input type="text" id="qtyValueInfant" class="form-control bg-transparent border-0 text-center qtyValueInfant" value="0" readonly="readonly"></div>
                                                            <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('Infant',2)" class="fa fa-plus increaseQtyInfant border p-1 b-radius"></i></div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                    </div>
                                            </div>
    
                                        </div>
                                        <div class="row equal-col-"> 
                                        <div class="col-12 d-none">
                                            <div class="row">
                                                <div class="col-6 mb-3">
                                                    <select class="form-control">
                                                        <option value="Economy" selected="selected">Economy</option>
                                                        <option value="Business">Business</option>
                                                        <option value="First">First</option>
                                                    </select>
                                                </div>
                                                <div class="col-6 mb-3">
                                                    <select id="" class="dropdown-select form-control">
                                                        <option value="1" selected="selected">1 Adult</option>
                                                        <option value="2">2 Adult</option>
                                                        <option value="3">3 Adult</option>
                                                        <option value="4">4 Adult</option>
                                                        <option value="5">5 Adult</option>
                                                    </select>
                                                </div>
                                                <div class="col-6 mb-3">
                                                    <select id="" class="dropdown-select form-control">
                                                        <option value="0" selected="selected">0 Child</option>
                                                        <option value="1">1 Child</option>
                                                        <option value="2">2 Child</option>
                                                        <option value="3">3 Child</option>
                                                        <option value="4">4 Child</option>
                                                        <option value="5">5 Child</option>
                                                    </select>
                                                </div>
                                                <div class="col-6 mb-3">
                                                    <select id="" class="dropdown-select form-control">
                                                        <option value="0" selected="selected">0 Infant(s)</option>
                                                        <option value="1">1 Infant(s)</option>
                                                        <option value="2">2 Infant(s)</option>
                                                        <option value="3">3 Infant(s)</option>
                                                        <option value="4">4 Infant(s)</option>
                                                        <option value="5">5 Infant(s)</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3">
                                                <label class="label" data-i18n="flightsearch-from-label">From</label>
                                                <div class="dvTextBoxFrom dvInputGroup input-group">
                                                    <input class="form-control" id="textBoxFrom" onfocus="placeholderOnFocus(this,'Enter City or Airport');" onblur="placeholderOnFocus(this,'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                                    <input type="hidden" id="hdntrip" value="true" />
                                                    <div class="input-group-append">
                                                        <span class="input-group-text bg-colour6"><i class="fa-solid fa-location-dot"></i></span>
                                                    </div>
                                                </div>
                                            </div>    
                                                <div class="col-12 col-md-6 col-lg-auto d-none mb-3 d-xl-flex flex-xl-column justify-content-xl-center align-items-xl-center text-center  ">
                                                        <label class="invisible">i</label>
                                                        <img src="images/flightpage/flight-arrow.png" />
                                                    </div>
                                        <div class="col-12 col-md-6 col-lg mb-3">
                                            <label class="label" data-i18n="flightsearch-to-label">To</label>
                                            <div class="dvTextBoxTo dvInputGroup input-group">
                                                <input class="input form-control" id="textBoxTo" onfocus="placeholderOnFocus(this,'Enter City or Airport');" onblur="placeholderOnFocus(this,'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6"><i class="fa-solid fa-location-dot"></i></span>
                                                </div>
                                            </div>
                                            </div>
                                        <div class="col-12 col-md-6 col-lg mb-3">
                                            <label class="label" data-i18n="flightsearch-departure-label">Departure</label>
                                            <div class="dvInputGroup input-group">
                                                <input id="txtDepart" class="input datePicker form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');"" value="Enter Date" type="text" readonly="readonly" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3" id="divrtn">
                                            <label class="label" data-i18n="flightsearch-return-label">Return</label>
                                            <div class="dvInputGroup input-group">
                                                <input class="input datePicker form-control cal-icon" id="txtReturn" onfocus="placeholderOnFocus(this,'Enter Date');"
                                                onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" type="text" readonly="readonly" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-lg-4 selTravel mb-3 r- d-none">
                                            <label class="label" data-i18n="flightsearch-travel-class-label">Travel Class</label>
                                            <div class="dvInput input-group">
                                                <select class="form-control" id="dropDownListEconomy">
                                                    <option value="Economy" selected="selected" data-i18n="flightsearch-travel-class-economy">Economy</option>
                                                    <option value="Business" data-i18n="flightsearch-travel-class-business">Business</option>
                                                    <option value="First" data-i18n="flightsearch-travel-class-first">First</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3">
                                                <label class="label" data-i18n="flightsearch-airline-preference-label">Airline Preference</label>
                                                <div class="dvInput input-group">
                                                    <input type="hidden" id="hdnCarrier" />
                                                    <input class="input air-icon form-control" id="txtAirline" onfocus="placeholderOnFocus(this, 'All Airlines');"
                                                onblur="placeholderOnFocus(this, 'All Airlines');" data-i18n="[value]flight-airline-preference-all" value="All Airlines" type="text" />
                                                </div>
                                            </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-6 col-sm-4 col-md-4 col-xl-3 mb-3 mb-md-0 r- d-none">
                                                    <label class="label" data-i18n="flightsearch-adult-label">Adult(s) 12+ Yrs</label>
                                                        <select id="DropDownListAdult" class="dropdown-select form-control">
                                                            <option value="1" selected="selected">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                            <option value="5">5</option>
                                                        </select>
                                                </div>
                                                <div class="col-6 col-sm-4 col-md-4 col-xl-3 mb-3 mb-md-0 r- d-none">
                                                    <label class="label" data-i18n="flightsearch-child-label">Child(ren) 2 - 11 Yrs</label>
                                                        <select id="DropDownListChild" class="dropdown-select form-control">
                                                            <option value="0" selected="selected">0</option>
                                                            <option value="1">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                            <option value="5">5</option>
                                                        </select>
                                                </div>
                                                <div class="col-6 col-sm-4 col-md-4 col-xl-3 mb-3 r- d-none">
                                                    <label class="label" data-i18n="flightsearch-infant-label">Infant(s) Below 2 Yrs</label>
                                                    <select id="DropDownListInfant" class="dropdown-select form-control">
                                                        <option value="0" selected="selected">0</option>
                                                        <option value="1">1</option>
                                                        <option value="2">2</option>
                                                        <option value="3">3</option>
                                                        <option value="4">4</option>
                                                        <option value="5">5</option>
                                                    </select>
                                                </div>
                                                <div class="col-12 mb-md-0">
                                                    <div class="row">
                                                        <div class="col-md-12 dvLabel invisible d-none">
                                                            <label class="label checkbox-container d-flex">
                                                                <span class="d-inline-block ml-1">
                                                                    <input name="vehicle" value="Bike" checked="checked" disabled="disabled" type="checkbox" />
                                                                    <span data-i18n="flight-redeem-pts-label">Redeem Points</span>
                                                                    <span class="checkmark" style="top:2px;"></span>
                                                                </span>
                                                            </label>
                                                        </div>
                                                        <div class="col-12 col-md-12 col-lg-3 offset-lg-9">
                                                            <button onclick="var retvalue = FlightValidation(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" class="btn btn-one w-100" value="Search" data-i18n="flightsearch-search-btn">Search Flight</button>
                                                        </div>
                                                    </div>
                                                </div>
                   
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                            </div>
                        </div>

                        <div class="dvNote col-12 mb-3">
                            <span data-i18n="flightlist-all-timigs-local" class="h7 heading-regular text-colour7 ">All timings are local. Fare not guaranteed until ticketed.</span>
                            <span data-i18n="flightlist-brandname-points" class="h7 heading-regular text-colour7 ">Infinity Rewards Points displayed for redemption include Surcharges and Taxes.</span>

                        </div>
                        <div class="dvProducts col-12">
                            <div class="row">
                                <div class="btn-filter" id="depFlight" onclick="return showreturn();" style="display: none">
                                    Return Flights
                                </div>
                                <div class="btn-filter" id="retFlight" onclick="return showdeparture();" style="display: none">
                                    Departure Flights
                                </div>
                                <div id="result1OneWay" style="display: none;"></div>
                                <div class="flt-return" id="domesticTwoWay" style="display: none; padding-bottom: 88px;">
                                    <div id="result1" class="flt-res1" style="display: none;"></div>
                                    <div id="result2" class="flt-res2" style="display: none;"></div>
                                </div>
                                <div class="col-12" id="resultInterNational" style="display: none;">
                                </div>
                                <div id="Trip_Summary_Main" style="display: none" class="fixedBot-tripSumm">
                                    <div class="trpsum-flthdr robot">
                                        <div class="trpsum-flthdr-L">
                                            <div id="divOnwardFlightDetails"></div>
                                        </div>
                                        <div class="trpsum-flthdr-R">
                                            <div id="divReturnFlightDetails"></div>
                                        </div>
                                        <div class="trpsum-flthdr-Miles">
                                            <div id="divTotal" style="width: 100%; float: left;"></div>
                                            <br />
                                            <div id="ViewDetails" style="text-decoration: underline; cursor: pointer; color: #fff; font-size: 12px;" onclick="return toggleInfo();" data-i18n="btn-view-details">
                                                View Details
                                            </div>
                                        </div>
                                    </div>
                                    <div class="trp-fltsumm-info fL" style="display: none" id="DomflightInfo">
                                        <div class="trpsum-flt-oneway">
                                            <div id="divOnwardFlightInfo"></div>
                                        </div>
                                        <div class="trpsum-flt-twoway">
                                            <div id="divReturnFlightInfo"></div>
                                        </div>
                                        <div class="trpsum-btnBook">
                                            <ul class="fR">
                                                <li>
                                                    <div class="btn" style="width: 100px;">
                                                        <a href="javascript:void(0);" onclick="var retvalue = BookDomestic(); event.returnValue= retvalue;event.preventDefault(); return retvalue;">
                                                            <button class="btn txt-btn-booknow"></button>
                                                        </a>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <input type="hidden" id="hdnOnwardSelectedFlight" />
                                <input type="hidden" id="hdnReturnSelectedFlight" />
                                <div id="LoadTemplate" runat="server"></div>
                                <div class="clr"></div>
                                <div class="bgHeader col-12 text-center" id="LoadNext" onclick="return LoadNext();">
                                    <label class="btn btn-one">
                                        <asp:Label ID="Label2" runat="server" CLASS="" Text="SHOW MORE FLIGHTS" data-i18n="button-more-flights"> </asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <input type="hidden" id="totalvalue" />
        </div>

    </div>


    <script type="text/javascript">
        function showreturn() {
            $("#depFlight").hide();
            $("#retFlight").show();
            $("#result1").hide();
            $("#result2").show();
            return false;
        }
        function showdeparture() {
            $("#depFlight").show();
            $("#retFlight").hide();
            $("#result2").hide();
            $("#result1").show();
            return false;
        }
        $('.modify').click(function () {
            //showModifyFlight(); $('.modify-box').slideToggle();
           showModifyFlight(); $('#divflt');
        });
        function toggleInfo() {
            $("#DomflightInfo").slideToggle("slow");
            return false;
        }

        // this script is for jquery ui selectmenu
        $(function () {
            //$("#returnSelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
          
            $("#passengerSelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
            
            //$("#economySelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
            $("#passengerSelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
        }); 

    </script>
</asp:Content>
