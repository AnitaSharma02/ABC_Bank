<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="FlightListForDomestic.aspx.cs" Inherits="FlightListForDomestic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <script src="Jquery/CoreFlightFilter.js" type="text/javascript"></script>
    <script src="Jquery/JTemplateCreator.js" type="text/javascript"></script>
    <link href="Css/flight.css" rel="stylesheet" type="text/css" />
    <style>
        .dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu {
            display: none;
        }
    </style>
    <script src="Jquery/FlightSearchScript_Domestic.js"></script>
    <script src="Jquery/FlightSearch_Domestic.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var edit = getUrlVars()["edit"];
            if (edit == "1") {
                $('#dvForm').addClass('show');
                showModifyFlight_Domestic();
            }

            //for returning to back page 
            document.getElementsByClassName("btnBack").onclick = function () {
                location.href = "/FlightSearch.aspx";
            };

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
            initAutocomplete(".dvFlightListDomestic #textBoxFromdomestic", ".dvFlightListDomestic .dvTextBoxFromdomestic");
            initAutocomplete(".dvFlightListDomestic #textBoxTodomestic", ".dvFlightListDomestic .dvTextBoxTodomestic");
        });
        

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
    <!--jQuery to hide filter popup div-->
    <script type="text/javascript">
        $(document).ready(function () {

        });

        document.getElementsByClassName(".btnBack").onclick = function () {
            location.href = "FlightSearch.aspx";
        };
    </script>

    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="me-3"><a href="\">
                        <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="FlightSearch.aspx"> Domestic Flight Search</a></li>
                    <li class="breadcrumb-item active">Domestic Flight List</li>
                </ul>
            </nav>
        </div>
    </div>

     <div class="dvFlightListDomestic dvProductList">
      
        <div class="container-xl">
            <div class="row">
                <div class="dvModify col-12 mb-3">
                    <div
                        class="bg-colour2 d-flex flex-wrap justify-content-between align-items-center py-2 px-2 px-lg-3 mb-1">
                        <%--<button
                      data-bs-toggle="modal"
                      data-bs-target="#dvFilterModal"
                      type="button"
                      class="btn btn-one col-12 d-lg-none mb-2"
                    >
                      Filter
                    </button>--%>
                        <p class="col-auto col-lg-10 px-0">
                            <asp:Label ID="LabelYourSearchDetails" runat="server" CssClass="Content_Style heading-semibold" Text=""></asp:Label>
                        </p>

                        <button
                            class="btn btn-one arrowBtn col-auto d-flex mt-2 mt-sm-0 collapsed modify"
                            type="button"
                            data-bs-toggle="collapse"
                            data-bs-target="#dvForm">
                            <span class="d-inline-block">Modify</span>
                            <span class="arrow-icon ms-2">
                                <i class="fa fa-caret-up"></i>
                            </span>
                        </button>
                    </div>
                    <div id="dvForm" class="dvForm bg-colour2 collapse">                         
                        <div class="bg-colour2 px-3 pt-2">      
                                <div class="dvErrors p-1 mb-2 alert alert-danger text-danger text-center h6 heading-semibold" id="requiredValidationdomestic" style="display:none;"></div>
                                 <div class="row dvLabel r- d-none">
                                    <div class="col-6 col-sm-4 col-md-3 col-lg-2 selTravel mb-3">
                                        <label class="h8 heading-semibold text-colour7 radio-container d-flex">
                                            <span class="d-inline-block ms-1">
                                                <input id="onelidomestic" class="radio " name="iternarydomestic" onchange="return onewayShowDomestic();" type="radio"><span class="pe-3 ps-2 h6 heading-regular text-colour7">One-Way</span>
                                                <span class="radiomark"></span>
                                            </span>
                                        </label>
                                    </div>
                                    <div class="col-6 col-sm-4 col-md-9 col-lg-10 selTravel mb-3">
                                        <label class="h8 heading-semibold text-colour7 radio-container d-flex">
                                            <span class="d-inline-block ms-1">
                                                <input class="radio" name="iternarydomestic" checked="checked" onchange="return RoundTripShowDomestic();" id="retlidomestic" type="radio"><span class="pe-3 ps-2 h6 heading-regular text-colour7">Return</span>
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
                                                <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-bs-toggle="dropdown" aria-expanded="false">
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
                                             <%--<div class="dvDropdown dropdown text-sm-center text-lg-start">
                                                 <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                    Economy
                                                 </button>
                                                 <div class="dropdown-menu dropdown-menu-right dropdown-menu-sm-left">
                                                     <a class="dropdown-item h7" href="#">Business</a>
                                                     <a class="dropdown-item h7" href="#">First</a> 
                                                 </div>
                                              </div>--%>
                                         </div>
                                         <div class="col-12 col-md-4 col-sm-4 col-lg-3 mb-3">
                                             <div class="dvDropdown dropdown text-sm-end text-lg-start">
                                                 <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                   Passenger
                                                 </button> 
                                                 <div class="dropdown-menu prevent-close w-250 dropdown-menu-sm-right">
                                                     <div class="dvQtySelectorAdultDomestic dvQtySelector dropdown-item d-flex align-items-center">
                                                           <div class="col-6 special-text h7">Adult(s) 12+ Yrs</div>
                                                           <div class="col-6 d-flex align-items-center pe-0">
                                                              <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('AdultDomestic')" class="fa fa-minus decreaseQtyAdultDomestic border p-1 b-radius"></i></div>
                                                              <div class="col-4 text-center px-0"><input type="text" id="qtyValueAdultDomestic" class="form-control bg-transparent border-0 text-center qtyValueAdultDomestic" value="1" readonly="readonly"></div>
                                                              <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('AdultDomestic',4)" class="fa fa-plus increaseQtyAdultDomestic border p-1 b-radius"></i></div>
                                                          </div>
                                                       </div>
                                                       <div class="dvQtySelectorChildDomestic dvQtySelector dropdown-item d-flex align-items-center">
                                                          <div class="col-6 special-text h7">Child(ren) 2 - 11 Yrs</div>
                                                          <div class="col-6 d-flex align-items-center pe-0">
                                                              <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseCount('ChildDomestic')" class="fa fa-minus decreaseQtyChildDomestic border p-1 b-radius"></i></div>
                                                              <div class="col-4 text-center px-0"><input type="text" id="qtyValueChildDomestic" class="form-control bg-transparent border-0 text-center qtyValueChildDomestic" value="0" readonly="readonly"></div>
                                                              <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseCount('ChildDomestic',2)" class="fa fa-plus increaseQtyChildDomestic border p-1 b-radius"></i></div>
                                                          </div>
                                                      </div> 
                                                       <div class="dvQtySelectorInfantDomestic dvQtySelector dropdown-item d-flex- align-items-center d-none">
                                                          <div class="col-6 special-text h7">Infant(s) Below 2 Yrs</div>
                                                          <div class="col-6 d-flex align-items-center pe-0">
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
                                       <label class="h8 heading-semibold text-colour7">From</label>
                                        <div class="dvTextBoxFromdomestic input-group">
                                            <input class="input from-icon form-control pe-5" id="textBoxFromdomestic" onfocus="placeholderOnFocusdomestic(this,'Enter City or Airport');" onblur="placeholderOnFocusdomestic(this,'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                             <span class="input-group-text bg-colour6"><i class="fa-solid fa-location-dot"></i></span>
                                            
                                        </div>
                                        <input type="hidden" id="hdntripdomestic" value="true" />
                                        </div> 
                                         <div class="col-12 col-md-6 col-lg-auto d-none mb-3 d-xl-flex flex-xl-column justify-content-xl-center align-items-xl-center text-center  ">
                                            <label class="invisible">i</label>
                                            <img src="images/flightpage/flight-arrow.png" />
                                        </div>
                                    <div class="col-12 col-md-6 col-lg mb-3">
                                        <label class="h8 heading-semibold text-colour7">To</label>
                                        <div class="dvTextBoxTodomestic input-group">
                                            <input class="input from-icon form-control pe-5" id="textBoxTodomestic" onfocus="placeholderOnFocusdomestic(this,'Enter City or Airport');" onblur="placeholderOnFocusdomestic(this,'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                             <span class="input-group-text bg-colour6"><i class="fa-solid fa-location-dot"></i></span>
                                          </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg mb-3">
                                        <label class="h8 heading-semibold text-colour7">Departure</label>
                                        <div class="input-group">
                                            <input id="txtDepartdomestic" class="input datePicker form-control cal-icon pe-5" onfocus="placeholderOnFocusdomestic(this,'Enter Date');" onblur="placeholderOnFocusdomestic(this,'Enter Date');"" value="Enter Date" type="text" readonly="readonly" />
                                             <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                         </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg mb-3" id="divrtndomestic">
                                                    <label class="h8 heading-semibold text-colour7">Return</label>
                                                    <div class="input-group"> 
                                                        <input class="input datePicker form-control cal-icon pe-5" id="txtReturndomestic" onfocus="placeholderOnFocusdomestic(this,'Enter Date');"
                                                        onblur="placeholderOnFocusdomestic(this,'Enter Date');" value="Enter Date" type="text" readonly="readonly" />
                                                        <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                     </div>
                                                </div>                                                                                
                                    <div class="col-12">
                                            <div class="row">
                                                <div class="col-6 col-md-4 col-xl-3 mb-3 mb-md-0 r- d-none">
                                                    <label class="h8 heading-semibold text-colour7">Adult(12+Yrs)</label>
                                                    <div class="input-bg">
                                                        <select id="DropDownListAdultdomestic" class="dropdown-select form-select">
                                                            <option value="1" selected="selected">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                            <option value="5">5</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-6 col-md-4 col-xl-3 mb-3 mb-md-0 r- d-none">
                                                    <label class="h8 heading-semibold text-colour7">Children(2-11Yrs)</label>
                                                    <div class="input-bg">
                                                        <select id="DropDownListChilddomestic" class="dropdown-select form-select">
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
                                                   <button onclick="var retvalue = FlightValidationDomestic(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" class="btn btn-one w-100" value="Search">Search Flight</button>
                                                </div>
                                            </div>
                                        </div>
                                  </div> 
         
                        </div>

                        <%--
                            OLD DESIGN
                            <div class="d-flex flex-wrap py-3">
                            <div class="col-12">
                                <div class="mb-3 selWay">
                                    <input id="onelidomestic" class="radio" name="iternarydomestic" onchange="return onewayShowDomestic();" type="radio"><span class="heading-semibold heading-bold">One-Way</span>
                                    <input class="radio" name="iternarydomestic" checked="checked" onchange="return RoundTripShowDomestic();" id="retlidomestic" type="radio"><span class="heading-semibold">Return</span>
                                </div>
                            </div>
                            <div class="col-sm-6 mb-3">
                                <label class="h8 heading-semibold">From</label>
                                <div class="input-group mb-3">
                                    <input class="input form-control" id="textBoxFromdomestic" onfocus="placeholderOnFocusdomestic(this, 'Enter City or Airport');"
                                        onblur="placeholderOnFocusdomestic(this, 'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                    <input type="hidden" id="hdntripdomestic" value="true" />
                                     <span class="input-group-text bg-colour6"><i class="fa fa-plane" aria-hidden="true"></i></span>
                                 </div>
                            </div>
                            <div class="col-sm-6 mb-3">
                                <label class="h8 heading-semibold">To</label>
                                <div class="input-group mb-3">
                                    <input class="input form-control" id="textBoxTodomestic" onfocus="placeholderOnFocusdomestic(this, 'Enter City or Airport');"
                                        onblur="placeholderOnFocusdomestic(this, 'Enter City or Airport');" value="Enter City or Airport" type="text" />
                                     <span class="input-group-text bg-colour6"><i class="fa fa-plane" aria-hidden="true"></i></span>
                                 </div>
                            </div>
                            <div class="col-sm-6 mb-3">
                                <label class="h8 heading-semibold">Departure</label>
                                <div class="input-group mb-3">
                                    <input id="txtDepartdomestic" class="input form-control" onfocus="placeholderOnFocusdomestic(this, 'Enter Date');"
                                        onblur="placeholderOnFocusdomestic(this, 'Enter Date');" value="Enter Date" type="text" readonly="readonly" />
                                     <span class="input-group-text bg-colour6"><i class="fa fa-calendar-o"></i></span>
                                 </div>
                            </div>
                            <div class="col-sm-6 mb-3" id="divrtndomestic">
                                <label class="h8 heading-semibold">Return</label>
                                <div class="input-group mb-3">
                                    <input class="input form-control" id="txtReturndomestic" onfocus="placeholderOnFocusdomestic(this,'Enter Date');"
                                        onblur="placeholderOnFocusdomestic(this, 'Enter Date');" value="Enter Date" type="text" readonly="readonly" />
                                   <span class="input-group-text bg-colour6"><i class="fa fa-calendar-o"></i></span>
                                 </div>
                            </div>
                            <div class="col-12">
                                <!-- <div id="tblDynamic" class="row">  -->
                                <div class="row">
                                    <div class="col-6 col-md-3 mb-3">
                                        <label class="h8 heading-semibold d-none w-100 mb-1">Room 1</label>
                                        <label class="h8 heading-semibold">Adult(s) 12+ Yrs</label>
                                        <select id="DropDownListAdultdomestic" class="dropdown-select form-select">
                                            <option value="1" selected="selected">1</option>
                                            <option value="2">2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                            <option value="5">5</option>
                                        </select>
                                    </div>
                                    <div class="col-6 col-md-3 mb-3">
                                        <label class="h8 heading-semibold d-none w-100 mb-1">Room 1</label>
                                        <label class="h8 heading-semibold">Child(ren) 2 - 11 Yrs</label>
                                        <select id="DropDownListChilddomestic" class="dropdown-select form-select">
                                            <option value="0" selected="selected">0</option>
                                            <option value="1">1</option>
                                            <option value="2">2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                            <option value="5">5</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6 text-center">
                                        <div class="d-none">
                                            <label class="h8 heading-semibold d-flex align-items-center">
                                                <input name="vehicle" value="Bike" checked="checked" disabled="disabled" type="checkbox" />
                                                <span class="ms-2 h8 heading-semibold">Redeem Points</span>
                                            </label>
                                        </div>
                                        <button onclick="var retvalue = FlightValidationDomestic(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" class="purple-btn Search hvr-sweep-to-right" value=" Search ">Search Flight</button>

                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div class="col-md-12 col-lg-12">
                    <div class="row">
                        <div class="SearchDetail SearchBorder col-12 d-none">
                            <%-- <asp:Label ID="LabelYourSearchDetails" runat="server" CssClass="Content_Style" Text=""></asp:Label>--%>
                            <a href="#modify_search" class="btn btn-one" id="btnModify" data-bs-toggle="collapse">Modify</a>
                        </div>
                    </div>
                    <div class="bg-colour2 serviceWidget collapse" id="modify_search">
                        <div id="divflt">
                            <!--Flight Search Panel Start -->
                            <div class="dvErrors Searchvalid" id="requiredValidation"></div>
                            <div class="form_box mt-4">
                                <%-- <div class="mb-3 selWay">
                                <input id="onelidomestic" class="radio" name="iternarydomestic" onchange="return onewayShowDomestic();" type="radio"><span>One-Way</span>
                                <input class="radio" name="iternarydomestic" checked="checked" onchange="return RoundTripShowDomestic();" id="retlidomestic" type="radio"><span>Return</span>
                            </div>--%>
                                <div class="row mb-3">
                                    <div class="col-md-6">
                                        <label>From</label>
                                        <%-- <input class="input dep-icon form-control" id="textBoxFromdomestic" value="Enter City or Airport" type="text" />
                                    <input type="hidden" id="hdntripdomestic" value="true" />--%>
                                    </div>
                                    <div class="col-md-6">
                                        <label>To</label>
                                        <%--<input class="input arv-icon form-control" id="textBoxTodomestic" value="Enter City or Airport" type="text" />--%>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-6 col-6">
                                        <label>Departure</label>
                                        <%--<input id="txtDepartdomestic" class="input datePicker form-control" value="Enter Date" type="text" readonly="readonly" />
                                        --%>
                                    </div>
                                    <div class="col-md-6 col-6">
                                        <label>Return</label>
                                        <%--<input class="input datePicker form-control" id="txtReturndomestic" value="Enter Date" type="text" readonly="readonly" />--%>
                                    </div>
                                </div>
                                <div class="row mb-3">
                                    <div class="col-md-12 selAge">
                                        <div class="row">
                                            <div class="col-md-4 col-6">
                                                <div class="input-bg">
                                                    <label>Adult(s) 12+ Yrs</label>
                                                    <%-- <select id="DropDownListAdultdomestic" class="dropdown-select form-select">
                                                    <option value="1" selected="selected">1</option>
                                                    <option value="2">2</option>
                                                    <option value="3">3</option>
                                                    <option value="4">4</option>
                                                    <option value="5">5</option>
                                                </select>--%>
                                                </div>

                                            </div>
                                            <div class="col-md-4 col-6">
                                                <div class="input-bg">
                                                    <label>Child(ren) 2 - 11 Yrs</label>
                                                    <%--<select id="DropDownListChilddomestic" class="dropdown-select form-select">
                                                    <option value="0" selected="selected">0</option>
                                                    <option value="1">1</option>
                                                    <option value="2">2</option>
                                                    <option value="3">3</option>
                                                    <option value="4">4</option>
                                                    <option value="5">5</option>
                                                </select>--%>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-6 valignM">
                                        <input name="vehicle" value="Bike" checked="checked" disabled="disabled" type="checkbox" />
                                        <label class="chkbox-txt-redeem-points">Redeem Points</label>
                                    </div>
                                    <div class="col-md-6 col-6 searchBtn">
                                        <%--<button onclick="var retvalue = FlightValidationDomestic(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" class="btn btn-one" value=" Search ">Search Flight</button>--%>
                                    </div>
                                </div>
                            </div>
                            <!--Flight Search Panel Ends-->
                        </div>
                    </div>
                    <div class="row">
                        <div class="dvNote col-12 mb-3">
                            <p>
                                <span class="h7 heading-semibold d-block">Note:</span>
                                <span class="h7 heading-regular d-block">All timings are local. Fare not guaranteed until ticketed.</span>
                                <span class="h7 heading-regular d-block">Infinity Rewards Points displayed for redemption include Surcharges and Taxes.</span>
                                <span class="heading-semibold d-inline-block pt-2 text-danger" id="divInsufficient"></span>
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="dvFinalResultOfTwoWay col-12" id="finalresultoftwoway">
                            <div id="finalresult" style="display: none;"></div>
                        </div>
                    </div>
                    <%--<p class="note"><span>All timings are local. Fare not guaranteed until ticketed.</span> <span>Infinity Rewards Points displayed for redemption include Surcharges and Taxes.</span></p>--%>
                    <div class="row flightBlk">
                        <div class="resultContent col-12">
                            <div class="row dvShowAndReturn">
                                <div class="col-12">
                                    <h2 class="mb-2 h6 heading-bold text-colour1" id="depFlight" onclick="return showreturn();" style="display: none">
                                        Return Flights
                                    </h2>
                                    <h2 class="mb-2 h6 heading-bold text-colour1" id="retFlight" onclick="return showdeparture();" style="display: none">
                                        Departure Flights
                                    </h2>
                                </div>
                            </div>
                            <div class="row dvResult1OneWay">
                                <div class="col-12" id="result1OneWay" style="display: none;"></div>
                            </div>
                            <div class="row equal-col dvdomesticTwoWay" id="domesticTwoWay" style="display: none;">
                                <div id="result1" class="dvResult1 col-6" style="display: none;"></div>
                                <div id="result2" class="dvResult2 col-6" style="display: none;"></div>
                            </div>
                            <div class="row dvResultInterNational">
                                <div class="col-12" id="resultInterNational" style="display: none;"></div>
                            </div>
                            <div class="row dvTripSummaryMain">
                                <div class="col-12 fixedBot-tripSumm" id="Trip_Summary_Main" style="display: none">                            
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
                                        <div id="ViewDetails" style="text-decoration: underline; cursor: pointer; color: #fff; font-size: 12px;" onclick="return toggleInfo();">
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
                            </div>
                            <input type="hidden" id="hdnOnwardSelectedFlight" />
                            <input type="hidden" id="hdnReturnSelectedFlight" />
                            <div id="LoadTemplate" runat="server"></div>
                            <div class="clr"></div>
                            <%--<div class="bgHeader my-5" id="LoadNext" onclick="return LoadNext();">
                                <label class="btn btn-one">
                                    <asp:Label ID="Label2" runat="server" CLASS="" Text="SHOW MORE FLIGHTS"> </asp:Label>
                                </label>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>

            <input type="hidden" id="totalvalue" />
        </div>

    </div>
    <script>
        //arrowBtn - this will work on all bootstrap single collapse, just add arrowBtn className on buttons
        (() => {
            const arrowBtn = document.querySelectorAll(".arrowBtn");
            arrowBtn.forEach((item) => {
                let x = item.classList;
                x.add("collapsed");
                item.classList.contains("collapsed") ? x : arrowBtn;
            });
        })();
        //arrowBtn - this will work on all bootstrap single collapse, just add arrowBtn className on buttons

        // this script is for jquery ui selectmenu in modify section
        $(function () {
            $("#passengerSelectMenuDomestic").selectmenu({}).selectmenu("menuWidget").addClass("select-menu-css");
        });
    </script>
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
            showModifyFlight_Domestic(); $('#dvForm');
        });
        function toggleInfo() {
            $("#DomflightInfo").slideToggle("slow");
            return false;
        }
    </script>


</asp:Content>

