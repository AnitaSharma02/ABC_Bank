<%@ Page Title="Flight Search" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="FlightSearch.aspx.cs" Inherits="FlightSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <%--<link href="Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" /> --%> 
    <link rel="stylesheet" href="\Css/flight.css" />
    <%--selectric--%>
    <%--<link href="Css/dropDown.css" rel="stylesheet" type="text/css" />
    <script src="Jquery/jquery.selectric.min.js" type="text/javascript"></script>--%>
    <%--for Flight--%>
    <script src="Jquery/FlightSearchScript.js" type="text/javascript"></script>
    <script src="Jquery/FlightSearchScript_Domestic.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var textBoxTo = $("#textBoxTo").val();
            var textBoxTodomestic = $("#textBoxTodomestic").val();
            if (textBoxTo != "Enter City or Airport") {
                $("#txtDepart").datepicker().datepicker("setDate", new Date());
                $("#txtReturn").datepicker().datepicker("setDate", new Date());
            }
            if (textBoxTodomestic != "Enter City or Airport") {
                $("#txtDepartdomestic").datepicker().datepicker("setDate", new Date());

                $("#txtReturndomestic").datepicker().datepicker("setDate", new Date());
            }
            BindBanner();
            GetRedemptionOptions();
            $("#dvHeroSlider").hide();
            $("#dvInnerBanner").attr("src", "images/flightpage/flight-banner.jpg");
            var ActiveTab = $("ul.nav-pills li a.active");
            if (ActiveTab[0].innerHTML.includes("International")) {
                $("#topdestinations").removeClass("d-none");
            }
            else {
                $("#topdestinations").addClass("d-none");
            }
            $('.tab').on('click', function () {
                $('.tab').removeClass('active');
                $(this).addClass('active');
                if ($(this)[0].innerHTML.includes("International")) {
                    $("#pills-international").addClass("show");
                    $("#topdestinations").removeClass("d-none");
                    $("#tabdomestic").removeClass("show");
                    $("#tabdomestic").removeClass("active");
                    $("#pills-international").addClass("active");
                    $("#textBoxFrom").val($("#textBoxFrom").val());
                    $("#textBoxTo").val($("#textBoxTo").val());
                    $("#txtDepart").val($("#txtDepart").val());
                    $("#txtReturn").val($("#txtReturn").val());
                }
                else {
                    $("#tabdomestic").addClass("show");
                    $("#topdestinations").addClass("d-none");
                    $("#pills-international").removeClass("show");
                    $("#pills-international").removeClass("active");
                    $("#tabdomestic").addClass("active");
                    $("#textBoxFromdomestic").val($("#textBoxFromdomestic").val());
                    $("#textBoxTodomestic").val($("#textBoxTodomestic").val());
                    $("#txtDepartdomestic").val($("#txtDepartdomestic").val());
                    $("#txtReturndomestic").val($("#txtReturndomestic").val());

                }
            })
            //scrollTop
            $('html, body').animate({
                scrollTop: $('.dvFlightSearch').offset().top
            }, 'slow');

            //show datepicker onclick of icon
            $(".dvTxtDepart .input-group-append .input-group-text").on("click", function () {
                $("#txtDepart").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvTxtReturn .input-group-append .input-group-text").on("click", function () {
                $("#txtReturn").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvTxtDepartdomestic .input-group-append .input-group-text").on("click", function () {
                $("#txtDepartdomestic").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvTxtReturndomestic .input-group-append .input-group-text").on("click", function () {
                $("#txtReturndomestic").datepicker("show");
            });

            // this script is for jquery ui selectmenu
            $(function () {
                $("#returnSelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
                $("#economySelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
                $("#passengerSelectMenu").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
                $("#returnSelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
                $("#economySelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
                $("#passengerSelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
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
            initAutocomplete(".dvFlightSearch #textBoxFrom", ".dvFlightSearch .dvTextBoxFrom");
            initAutocomplete(".dvFlightSearch #textBoxTo", ".dvFlightSearch .dvTextBoxTo");
            initAutocomplete(".dvFlightSearch #textBoxFromdomestic", ".dvFlightSearch .dvTextBoxFromdomestic");
            initAutocomplete(".dvFlightSearch #textBoxTodomestic", ".dvFlightSearch .dvTextBoxTodomestic");
        });


        function SelectDestinationToInSearchOption(DestinationTo) {
            $('#textBoxTo').val(DestinationTo);
            var textBoxTo = $("#textBoxTo").val();
            if (textBoxTo != "Enter City or Airport") {
                $("#txtDepart").datepicker().datepicker("setDate", new Date());
                $("#txtReturn").datepicker().datepicker("setDate", new Date());
            }
        }



        //document.addEventListener("DOMContentLoaded", (event) => {



        //    setTimeout(function () {
        //        var targetNode = document.getElementById('returnSelectMenu-button');

        //        if (targetNode) {
        //            var observer = new MutationObserver(function (mutationsList, observer) {
        //                for (var mutation of mutationsList) {
        //                    if (mutation.type === 'attributes' && mutation.attributeName === 'aria-labelledby') {

        //                        var trip = $("#returnSelectMenu option:selected").val();

        //                        if (trip == 'Return') {
        //                            $("#hdntrip").val('true');
        //                            $("#divrtn").show();
        //                            $("#returnSelectMenu").val("Return");
        //                        }

        //                        if (trip == 'One Way') {
        //                            $("#hdntrip").val('false');
        //                            $("#divrtn").hide();
        //                            $("#returnSelectMenu").val("One Way");
        //                        }

        //                    }
        //                }
        //            });

        //            observer.observe(targetNode, { attributes: true });
        //        }
        //        else if (targetNode == null) {
        //            location.reload();
        //        }


        //        var targetNode = document.getElementById('returnSelectMenuDomestic-button');

        //        if (targetNode) {
        //            var observer = new MutationObserver(function (mutationsList, observer) {
        //                for (var mutation of mutationsList) {
        //                    if (mutation.type === 'attributes' && mutation.attributeName === 'aria-labelledby') {

        //                        var trip = $("#returnSelectMenuDomestic option:selected").val();

        //                        if (trip == 'Return') {
        //                            $("#hdntrip").val('true');
        //                            $("#divrtndomestic").show();
        //                            $("#returnSelectMenuDomestic").val("Return");
        //                        }

        //                        if (trip == 'One Way') {
        //                            $("#hdntrip").val('false');
        //                            $("#divrtndomestic").hide();
        //                            $("#returnSelectMenuDomestic").val("One Way");
        //                        }

        //                    }
        //                }
        //            });

        //            observer.observe(targetNode, { attributes: true });
        //        }
        //        else if (targetNode == null) {
        //            location.reload();
        //        }

        //    }, 1000);
        //});

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

            checkAndObserve('#returnSelectMenuDomestic-button', (targetNode) => {
                observeAttributes(targetNode, '#returnSelectMenuDomestic', '#hdntripdomestic', '#divrtndomestic');
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
    
<div class="dvNewDesign">
   <div class="dvFlightSearch">
        <a name="flightScrollupAnchor" id="flightScrollupAnchor">
           <section class="innerPages mb-5">
                <div class="container-xl">
                    <div class="row"> 
                        <div class="col-md-12">
                            <ul class="nav nav-pills d-none" id="pills-tab" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active tab" id="pills-international-tab" data-toggle="pill" href="#pills-international" role="tab" aria-controls="pills-international" aria-selected="true" data-i18n="flightsearch-international">International Flight</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link domesticBtn tab" id="tabdomesticTab" data-toggle="pill" href="#tabdomestic" role="tab" aria-controls="tabdomestic"  aria-selected="false" data-i18n="flightsearch-domestic" visible="false" runat="server">Domestic Flight</a>
                                </li>
                             </ul>
                            <div class="tab-content" id="pills-tabContent">
                                 <div class="dvInternational tab-pane fade show active" id="pills-international" role="tabpanel" aria-labelledby="pills-international-tab">
                                     <div class="bg-colour2 px-3 pt-2">                                       
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
                                                         <select id="returnSelectMenu">
                                                            <option selected="selected">Return</option>
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
                                                <div class="row"> 
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
                                                    <div class="dvTxtDepart dvInputGroup input-group">
                                                        <input id="txtDepart" class="input datePicker form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');"" value="Enter Date" type="text" readonly="readonly" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-md-6 col-lg mb-3" id="divrtn">
                                                    <label class="label" data-i18n="flightsearch-return-label">Return</label>
                                                    <div class="dvInputGroup input-group">
                                                        <input class="input datePicker form-control cal-icon pr-5" id="txtReturn" onfocus="placeholderOnFocus(this,'Enter Date');"
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
                                                        <div class="col-12">
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
                                                                <div class="col-12 col-md-12 col-lg-3 offset-lg-9 col-xl-2 offset-xl-10 mb-3">
                                                                    <button onclick="var retvalue = FlightValidation(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" class="btn btn-one w-100" value="Search" data-i18n="flightsearch-search-btn">Search Flight</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                                
                                              </div>
                                         
                                     </div>
                                 </div>
                                 <div class="dvDomestic tab-pane fade" id="tabdomestic" role="tabpanel" aria-labelledby="tabdomesticTab">
                                    <div class="bg-colour2 px-3 pt-2">
                                      
                                            <div class="p-1 mb-2 alert alert-danger text-danger text-center h6 heading-semibold" id="requiredValidationdomestic" style="display:none;"></div>
                                             <div class="row dvLabel r- d-none">
                                                <div class="col-6 col-sm-4 col-md-3 col-lg-2 selTravel mb-3">
                                                    <label class="label radio-container d-flex">
                                                        <span class="d-inline-block ml-1">
                                                            <input id="onelidomestic" class="radio " name="iternarydomestic" onchange="return onewayShowDomestic();" type="radio"><span data-i18n="flightsearch-one-way" class="pr-3 pl-2 h6 heading-regular text-colour7">One-Way</span>
                                                            <span class="radiomark"></span>
                                                        </span>
                                                    </label>
                                                </div>
                                                <div class="col-6 col-sm-4 col-md-9 col-lg-10 selTravel mb-3">
                                                    <label class="label radio-container d-flex">
                                                        <span class="d-inline-block ml-1">
                                                            <input class="radio" name="iternarydomestic" checked="checked" onchange="return RoundTripShowDomestic();" id="retlidomestic" type="radio"><span class="pr-3 pl-2 h6 heading-regular text-colour7" data-i18n="flightsearch-return">Return</span>
                                                            <span class="radiomark"></span>
                                                        </span>
                                                     </label>
                                                  </div>   
                                                </div>
                                                 <div class="row">
                                                     <div class="col-6 col-md-4 col-sm-4 col-lg-2 mb-3">
                                                         <select id="returnSelectMenuDomestic">
                                                           <option selected="selected">Return</option>
                                                           <option>One Way</option>
                                                         </select>
                                                        <%--<div class="dvDropdown dropdown">
                                                            <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                               Return
                                                            </button>
                                                            <div class="dropdown-menu">
                                                                <a class="dropdown-item h7" href="#">One-Way</a>
                                                                <a class="dropdown-item h7" href="#">Return</a> 
                                                            </div>
                                                         </div>--%>
                                                    </div>
                                                     <div class="col-6 col-md-4 col-sm-4 col-lg-2 mb-3 d-none">
                                                         <select id="economySelectMenuDomestic">
                                                          <option selected="selected">Economy</option>
                                                          <option>Business</option>
                                                             <option>First</option>
                                                        </select>
                                                         <%--<div class="dvDropdown dropdown text-sm-center text-lg-left">
                                                             <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                                Economy
                                                             </button>
                                                             <div class="dropdown-menu dropdown-menu-right dropdown-menu-sm-left">
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
                                                                 <div class="dvQtySelectorAdultDomestic dvQtySelector dropdown-item d-flex align-items-center">
                                                                       <div class="col-6 special-text h7">Adult(s) 12+ Yrs</div>
                                                                       <div class="col-6 d-flex align-items-center pr-0">
                                                                          <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('AdultDomestic')" class="fa fa-minus decreaseQtyAdultDomestic border p-1 b-radius"></i></div>
                                                                          <div class="col-4 text-center px-0"><input type="text" id="qtyValueAdultDomestic" class="form-control bg-transparent border-0 text-center qtyValueAdultDomestic" value="1" readonly="readonly"></div>
                                                                          <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('AdultDomestic',4)" class="fa fa-plus increaseQtyAdultDomestic border p-1 b-radius"></i></div>
                                                                      </div>
                                                                   </div>
                                                                   <div class="dvQtySelectorChildDomestic dvQtySelector dropdown-item d-flex align-items-center">
                                                                      <div class="col-6 special-text h7">Child(ren) 2 - 11 Yrs</div>
                                                                      <div class="col-6 d-flex align-items-center pr-0">
                                                                          <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('ChildDomestic')" class="fa fa-minus decreaseQtyChildDomestic border p-1 b-radius"></i></div>
                                                                          <div class="col-4 text-center px-0"><input type="text" id="qtyValueChildDomestic" class="form-control bg-transparent border-0 text-center qtyValueChildDomestic" value="0" readonly="readonly"></div>
                                                                          <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('ChildDomestic',2)" class="fa fa-plus increaseQtyChildDomestic border p-1 b-radius"></i></div>
                                                                      </div>
                                                                  </div> 
                                                                   <div class="dvQtySelectorInfantDomestic dvQtySelector dropdown-item d-flex- align-items-center d-none">
                                                                      <div class="col-6 special-text h7">Infant(s) Below 2 Yrs</div>
                                                                      <div class="col-6 d-flex align-items-center pr-0">
                                                                          <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('InfantDomestic')" class="fa fa-minus decreaseQtyInfantDomestic border p-1 b-radius"></i></div>
                                                                          <div class="col-4 text-center px-0"><input type="text" id="qtyValueInfantDomestic" class="form-control bg-transparent border-0 text-center qtyValueInfantDomestic" value="0" readonly="readonly"></div>
                                                                          <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('InfantDomestic',2)" class="fa fa-plus increaseQtyInfantDomestic border p-1 b-radius"></i></div>
                                                                      </div>
                                                                  </div>
                                                             </div>
                                                          </div>
                                                     </div>
                                                  </div>
                                                 <div class="row equal-col form_box">
                                                <div class="col-12 col-md-6 col-lg mb-3">
                                                   <label class="label" data-i18n="flightsearch-from-label">From</label>
                                                    <div class="dvTextBoxFromdomestic input-group">
                                                        <input class="input from-icon form-control pr-5" id="textBoxFromdomestic" onfocus="placeholderOnFocusdomestic(this,'Enter City or Airport');" onblur="placeholderOnFocusdomestic(this,'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text bg-colour6"><i class="fa-solid fa-location-dot"></i></span>
                                                        </div>
                                                    </div>
                                                    <input type="hidden" id="hdntripdomestic" value="true" />
                                                    </div> 
                                                     <div class="col-12 col-md-6 col-lg-auto d-none mb-3 d-xl-flex flex-xl-column justify-content-xl-center align-items-xl-center text-center  ">
                                                        <label class="invisible">i</label>
                                                        <img src="images/flightpage/flight-arrow.png" />
                                                    </div>
                                                <div class="col-12 col-md-6 col-lg mb-3">
                                                    <label class="label" data-i18n="flightsearch-to-label">To</label>
                                                    <div class="dvTextBoxTodomestic input-group">
                                                        <input class="input from-icon form-control pr-5" id="textBoxTodomestic" onfocus="placeholderOnFocusdomestic(this,'Enter City or Airport');" onblur="placeholderOnFocusdomestic(this,'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text bg-colour6"><i class="fa-solid fa-location-dot"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-md-6 col-lg mb-3">
                                                    <label class="label" data-i18n="flightsearch-departure-label">Departure</label>
                                                    <div class="input-group">
                                                        <input id="txtDepartdomestic" class="input datePicker form-control cal-icon pr-5" onfocus="placeholderOnFocusdomestic(this,'Enter Date');" onblur="placeholderOnFocusdomestic(this,'Enter Date');"" value="Enter Date" type="text" readonly="readonly" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 col-md-6 col-lg mb-3" id="divrtndomestic">
                                                    <label class="label" data-i18n="flightsearch-return-label">Return</label>
                                                    <div class="input-group"> 
                                                        <input class="input datePicker form-control cal-icon pr-5" id="txtReturndomestic" onfocus="placeholderOnFocusdomestic(this,'Enter Date');"
                                                        onblur="placeholderOnFocusdomestic(this,'Enter Date');" value="Enter Date" type="text" readonly="readonly" />
                                                        <div class="input-group-append">
                                                            <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>                                                                              
                                                <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-6 col-md-4 col-xl-3 mb-3 mb-md-0 r- d-none">
                                                                <label class="label" data-i18n="flightsearch-adult-label">Adult(12+Yrs)</label>
                                                                <div class="input-bg">
                                                                    <select id="DropDownListAdultdomestic" class="dropdown-select form-control">
                                                                        <option value="1" selected="selected">1</option>
                                                                        <option value="2">2</option>
                                                                        <option value="3">3</option>
                                                                        <option value="4">4</option>
                                                                        <option value="5">5</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="col-6 col-md-4 col-xl-3 mb-3 mb-md-0 r- d-none">
                                                                <label class="label" data-i18n="flightsearch-child-label">Children(2-11Yrs)</label>
                                                                <div class="input-bg">
                                                                    <select id="DropDownListChilddomestic" class="dropdown-select form-control">
                                                                        <option value="0" selected="selected">0</option>
                                                                        <option value="1">1</option>
                                                                        <option value="2">2</option>
                                                                        <option value="3">3</option>
                                                                        <option value="4">4</option>
                                                                        <option value="5">5</option>
                                                                    </select>
                                                                </div>
                                                            </div>                                                
                                                            <div class="col-12 col-md-12 col-lg-3 offset-lg-9 col-xl-2 offset-xl-10 mb-3">
                                                               <button onclick="var retvalue = FlightValidationDomestic(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" class="btn btn-one w-100" value="Search" data-i18n="flightsearch-search-btn">Search Flight</button>
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
           </section>
        </a>
   <div class="dvProductList mb-5" id="topdestinations">
      <div class="container-xl">
            <div class="row"> 
                <div class="col-12">
                     <div class="row">
                        <div class="dvFlightSearchProducts col-12">
                            <div class="row">
                                <div class="text-center m-auto my-3">
                                    <h2 class="h1 heading-semibold text-colour1 pb-4 px-3" data-i18n="flightsearch-top-destination">Top Destinations For Your Next Holiday</h2>
                                </div>
                            </div>
                            <div class="row">
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('SIN, Changi International, Singapore, SINGAPORE');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorSIN">
                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img1.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>
                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-sg">Singapore</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-sg-desc">
                                            Singapore, officially the Republic of Singapore, is a sovereign island city-state in maritime Southeast Asia.
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 1,234 <span data-i18n="flightsearch-points">Points</span>
                                        </p>

                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('BKK, Suvarnabhumi International Apt, Bangkok, THAILAND​');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorBKK">

                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img2.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>

                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-th">Thailand</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-th-desc">
                                            Thailand is a Southeast Asian country. It's known for tropical beaches, opulent royal palaces
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 4,500 <span data-i18n="flightsearch-points">Points </span>
                                        </p>



                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('PAR, All Airports, Paris, FRANCE​');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorPAR">
                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img3.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>
                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-pa">Paris</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-pa-desc">
                                            Paris, France's capital, is a major European city and a global center for art, fashion, gastronomy and culture.
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 5,000 <span data-i18n="flightsearch-points">Points </span>
                                        </p>


                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('CHC, Christchurch International, Christchurch, NEW ZEALAND');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorCHC">

                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img4.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>

                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-nz">New Zealand</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-nz-desc">
                                            New Zealand is an island country in the south western Pacific Ocean. It consists of two main landmasses
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 5,300 <span data-i18n="flightsearch-points">Points </span>
                                        </p>



                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('LHR, Heathrow International, London, UNITED KINGDOM​​');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorLHR">
                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img5.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>
                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-ln">London</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-ln-desc">
                                            London, the capital of England and the United Kingdom, is a 21st-century city with history stretching
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 6,000 <span data-i18n="flightsearch-points">Points </span>
                                        </p>


                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('DXB, Dubai International, Dubai, UNITED ARAB EMIRATES​​');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorDXB">
                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img6.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>
                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-db">Dubai</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-db-desc">
                                            Dubai is the most populous city in the UAE and the capital of the Emirate of Dubai,
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 3,000 <span data-i18n="flightsearch-points">Points </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('ZRH, Zurich Airport, Zurich, SWITZERLAND​​');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorZRH">
                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img7.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>
                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-zu">Zurich</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-zu-desc">
                                            Zürich is the largest city in Switzerland and the capital of the canton of Zürich.
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 4,000 <span data-i18n="flightsearch-points">Points </span>
                                        </p>
                                    </div>
                                </div>
                                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('GOI, Goa Airport, Goa, INDIA​​');">
                                    <div class="dvItem">
                                        <a class="anchor" href="#flightScrollupAnchor" rel="" id="anchorGOI">
                                            <div class="img-container">
                                                <img src="Images/flightpage/flight-img8.jpg" style="cursor: pointer;" />
                                            </div>
                                        </a>
                                        <h2 class="mt-3 mb-2 mx-3" data-i18n="flightsearch-go">Goa</h2>
                                        <p class="px-3 mb-3" data-i18n="flightsearch-go-desc">
                                            Goa, a state on India's West coast, is a former Portuguese colony with a rich history.
                                        </p>
                                        <p class="points px-3 mb-3 mt-auto">
                                            <span data-i18n="flightsearch-from">From</span> 5,000 <span data-i18n="flightsearch-points">Points </span>
                                        </p>
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


</asp:Content>