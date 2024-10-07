<%@ Page Title="Hotel Results" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="HotelResults.aspx.cs" Inherits="HotelResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <script src="Jquery/HotelSearchScript.js" type="text/javascript"></script>
    <script src="Jquery/JSONHotelSearch.js" type="text/javascript"></script>
    <link href="Css/hotel.css" rel="stylesheet" type="text/css" />

    <script src="Jquery/purify.min.js" type="text/javascript"></script>
    <asp:HiddenField ID="hdnHotelFilterRange" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="HFNoOfRooms" runat="server" />
    <asp:HiddenField ID="hdnNoAdult" runat="server" />
    <asp:HiddenField ID="hdnNoChild" runat="server" />
    <asp:HiddenField Value="" ID="hdnPaymentType" runat="server"></asp:HiddenField>

    <!--jQuery to hide filter popup div-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnClosePopup").click(function () {
                $("#divSearch").hide();
            });

            //for returning to back page 
            document.getElementsByClassName("btnBack").onclick = function () {
                location.href = "/HotelSearch.aspx";
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
            initAutocomplete(".dvHotelResult #CP_txtCity", ".dvHotelResult .dvCP_txtCity");
        });
        
    </script>

    <style>
        /*.martop {
            margin-top: 0;
        }

        .bg-black {
            background: black;
        }

        @media (max-width: 800px) {
            .hotelBox {
                padding: 0;
                margin: 0;
            }
        }*/
        .dvHeroSlider,
        .dvRedemptionMenu,.dvInnerBanner  {
            display: none !important;
        }
    </style>
  <div class="dvBreadcrumbs">
      <div class="container-xl">
          <nav>
              <ul class="breadcrumb px-0 py-3">
                  <li class="mr-3"><a href="\"><img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                  </li>
                  <li class="breadcrumb-item"><a href="\">Home</a></li>
                  <li class="breadcrumb-item"><a href="HotelSearch.aspx"> Hotel Search</a></li>
                  <li class="breadcrumb-item">Hotel Results</li>
              </ul>
          </nav>
      </div>
  </div>

    <div class="dvProductList dvHotelResult pb-5 mt-lg-4">
        <div class="container-xl">
            <div class="row">
                <!-- <div class="col-12 mb-3 d-lg-none">
              <button
                data-toggle="modal"
                data-target="#dvFilterModal"
                type="button"
                class="btn btn-one w-100"
              >
                Filter
              </button>
            </div> -->
                <div class="dvFilter modal fade col-lg-3 px-0 px-lg-3" id="dvFilterModal" tabindex="-1">
                    <div id="divSearch" class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                        <div class="modal-content border-0">
                             <div class="modal-header border-0 d-lg-block p-0">
                                <div class="modal-title dvTotalRecords border-0 p-3">
                                    <p id="totalHotel" class="h6 heading-semibold text-colour1">
                                        Total <span>Hotels</span> found <span>0</span>
                                    </p>
                                </div>
                                   <button type="button" class="close d-lg-none px-3" data-dismiss="modal">
                                       <i class="fa-solid fa-xmark"></i>
                                   </button>
                              </div>
                             <div class="modal-body p-lg-0">
                                <div class="accordion" id="filter-accordion">
                                    <div class="card my-3">
                                        <div class="card-header p-0">
                                            <h2>
                                                <button
                                                    class="btn btn-block text-left"
                                                    type="button"
                                                    data-toggle="collapse-"
                                                    data-target="#collapse1">
                                                   <span class=" h6 heading-semibold text-colour7 "> Total Points</span>
                                                  <span class="arrow-icon">
                                                      <i class="fa fa-caret-up-"></i>
                                                  </span>
                                                </button>
                                            </h2>
                                        </div>

                                        <div id="collapse1" class="collapse- show" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvRangeSlider">
                                                    <div id="priceSlider" class="mb-2"></div>
                                                    <div id="priceRange"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="dvBorderBottom">
                                        <div class="">
                                            <div class="border-bottom"></div>
                                        </div>
                                    </div>

                                    <div class="card my-3">
                                        <div class="card-header p-0">
                                            <h2>
                                                <button
                                                    class="btn btn-block text-left h6 heading-semibold text-colour7"
                                                    type="button"
                                                    data-toggle="collapse-"
                                                    data-target="#collapse2">
                                                    <span class=" h6 heading-semibold text-colour7 ">Star Rankings</span>
                                                      <span class="arrow-icon">
                                                          <i class="fa fa-caret-up-"></i>
                                                      </span>
                                                </button>
                                            </h2>
                                        </div>
                                        <div id="collapse2" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvLabel d-flex justify-content-between">
                                                    <label class="checkbox-container d-flex">
                                                        <span class="d-inline-block">
                                                            <input type="checkbox" id="ChkSelectAll" onclick="SelectAll('ChkSelectAll', 'divRatings');" checked />
                                                            <span class="checkmark"></span>
                                                        </span>
                                                        <span class="d-inline-block ml-2">Select All</span>
                                                    </label>
                                                </div>
                                                <div class="" id="divRatings"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="dvBorderBottom">
                                        <div class="">
                                            <div class="border-bottom"></div>
                                        </div>
                                    </div>

                                    <div class="card my-3">
                                        <div class="card-header p-0">
                                            <h2>
                                                <button
                                                    class="btn btn-block text-left h6 heading-semibold"
                                                    type="button"
                                                    data-toggle="collapse-"
                                                    data-target="#collapse3">
                                                   <span class=" h6 heading-semibold text-colour7 "> Hotel Amenities</span>
                                                          <span class="arrow-icon">
                                                              <i class="fa fa-caret-up-"></i>
                                                          </span>
                                                </button>
                                            </h2>
                                        </div>
                                        <div id="collapse3" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2">
                                                <div class="dvLabel d-flex justify-content-between">
                                                    <label class="checkbox-container d-flex">
                                                        <span class="d-inline-block">
                                                            <input type="checkbox" id="chkSelectAllAmenities" onclick="SelectAll('chkSelectAllAmenities', 'divBasicAmenities');" checked />
                                                            <span class="checkmark"></span>
                                                        </span>
                                                        <span class="d-inline-block ml-2">Select All</span>
                                                    </label>
                                                </div>
                                                <div id="divBasicAmenities">
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnAirCondition" display="Air Condition" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/ac-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Air Conditioning</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnBar" display="Bar" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <imgclass="ml-2" src="Images/hotelpage/icons/bar-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Bar</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnBussinessCenter" display="Bussiness Center" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/meet-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Business Centre</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnCoffee" display="Coffee" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/coffee-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Coffee Shop</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnGym" display="Gym" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/gym-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Gym</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnInternet" display="Internet Access" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/internet_access.png">
                                                            <span class="d-inline-block ml-1">Internet Access</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnPool" display="Pool" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/pool-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Pool</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnRestaurant" display="Restaurant" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/resto-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Restaurant</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnRoomService" display="Room Service" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/room-ser-icon.svg">
                                                            <span class="d-inline-block ml-1">Room Service</span>
                                                        </label>
                                                    </div>
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="amnWifi" display="Wi-fi" onclick="showImage()" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <img class="ml-2" src="Images/hotelpage/icons/wifi-icon-inactive.svg">
                                                            <span class="d-inline-block ml-1">Wi-Fi Access</span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="dvBorderBottom">
                                        <div class="">
                                            <div class="border-bottom"></div>
                                        </div>
                                    </div>

                                    <div class="card my-3">
                                        <div class="card-header p-0">
                                            <h2>
                                                <button
                                                    class="btn btn-block text-left h6 heading-semibold"
                                                    type="button"
                                                    data-toggle="collapse-"
                                                    data-target="#collapse4">
                                                     <span class=" h6 heading-semibold text-colour7">Hotel Chains</span>
                                                    <span class="arrow-icon">
                                                        <i class="fa fa-caret-up-"></i>
                                                    </span>
                                                </button>
                                            </h2>
                                        </div>
                                        <div id="collapse4" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2 d-block">
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="chkHotelChains" onclick="SelectAll('chkHotelChains', 'divHotelChain');" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <%--<img class="ml-2" src="Images/hotelpage/icons/room-ser-icon.svg">--%>
                                                            <span class="d-inline-block ml-1">Select All</span>
                                                        </label>
                                                    </div>
                                                    <div id="divHotelChain"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="dvBorderBottom">
                                        <div class="">
                                            <div class="border-bottom"></div>
                                        </div>
                                    </div>


                                    <div class="card my-3">
                                        <div class="card-header p-0">
                                            <h2>
                                                <button
                                                    class="btn btn-block text-left h6 heading-semibold"
                                                    type="button"
                                                    data-toggle="collapse-"
                                                    data-target="#collapse5">
                                                    <span class=" h6 heading-semibold text-colour7">Hotel Locations</span>
                                                    <span class="arrow-icon">
                                                        <i class="fa fa-caret-up-"></i>
                                                    </span>
                                                </button>
                                            </h2>
                                        </div>
                                        <div id="collapse5" class="collapse-" data-parent="#filter-accordion">
                                            <div class="card-body scroll-ver- px-0 pt-1 pb-2 d-block">
                                                    <div class="dvLabel d-flex justify-content-between">
                                                        <label class="checkbox-container d-flex">
                                                            <span class="d-inline-block">
                                                                <input type="checkbox" id="chkLocation" onclick="SelectAll('chkLocation', 'divLocation');" checked />
                                                                <span class="checkmark"></span>
                                                            </span>
                                                            <%--<img class="ml-2" src="Images/hotelpage/icons/room-ser-icon.svg">--%>
                                                            <span class="d-inline-block ml-1">Select All</span>
                                                        </label>
                                                    </div>
                                                    <div id="divLocation"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 mb-3 d-lg-none">
                                        <button type="button" id="btnClosePopup" class="btn btn-one w-100" value="Apply">Apply</button>
                                    </div>
                                    <div id="divBlockAll"></div>

                                </div>
                            </div>
                            <!-- <div class="modal-footer d-lg-none">
                    <button
                      type="button"
                      class="btn btn-secondary"
                      data-dismiss="modal"
                    >
                      Close
                    </button>
                    <button type="button" class="btn btn-one">
                      Apply Filters
                    </button>
                  </div> -->
                        </div>
                    </div>
                </div>
                <div class="col-lg-9">
                    <div class="row">
                        <div class="dvModify col-12 mb-3">
                            <div class="bg-colour2 d-flex flex-wrap justify-content-between align-items-center py-2 py-lg-1 px-2 px-lg-3 mb-1">
                                <button
                                    data-toggle="modal"
                                    data-target="#dvFilterModal"
                                    type="button"
                                    class="btn btn-one col-12 d-lg-none mb-2">
                                    Filter
                                </button>
                                <p class="heading-regular col-auto col-lg-10 px-0">
                                    <asp:Label CssClass="Content_Style h6 heading-regular text-colour7 col-auto px-0 mb-2 mb-lg-0" runat="server" ID="lblSearchSummary"></asp:Label>
                                    <%--<a href="#modify_search" class="btn btn-yellow" id="btnModify" data-toggle="collapse">Modify</a>--%>
                                    <%--<a class="btn btn-one" id="btnfliter">Filters</a>--%>
                                </p>
                                <button
                                    class="btn btn-one arrowBtn col-auto d-flex mt-2 mt-sm-0 collapsed"
                                    type="button"
                                    data-toggle="collapse"
                                    data-target="#dvForm">
                                    <span class="d-inline-block">Modify</span>
                                    <span class="arrow-icon ml-2">
                                        <i class="fa fa-caret-up"></i>
                                    </span>
                                </button>
                            </div>
                            <div id="dvForm" class="dvForm collapse">
                                <div class="bg-colour2 p-3">
                                
                                    <%--NEW DESIGN--%>
                                    <div class="row">
                                    <div class="col-12 order-0">
                                        <div id="HotelModifyValidation" class="dvErrors p-1 mb-2 alert alert-danger text-center h6 heading-semibold" style="display: none;"></div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg mb-3 order-1">
                                        <input type="hidden" id="hdnRoomString" />
                                        <label class="label">City</label>
                                        <div class="dvCP_txtCity dvInputGroup input-group">
                                            <input id="txtCity" class="form-control" onfocus="placeholderOnFocus(this,'Enter City Name');" onblur="placeholderOnFocus(this,'Enter City Name');" value="Enter City Name" type="text" runat="server" />
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-colour6">
                                                    <i class="fa-solid fa-location-dot"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg mb-3 order-2">
                                        <label class="label">Check-in</label>
                                        <div class="dvTextBoxCheckin dvInputGroup input-group">
                                            <input class="form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" type="text" id="TextBoxCheckin" readonly="readonly"  runat="server"/>
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-colour6">
                                                    <i class="fa-regular fa-calendar"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg mb-3 order-3">
                                        <label class="label">Check-out</label>
                                        <div class="dvTextBoxCheckout dvInputGroup input-group">
                                            <input class="form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" type="text" id="TextBoxCheckout" readonly="readonly" runat="server" />
                                            <div class="input-group-append">
                                                <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 col-lg mb-3 order-4">
                                        <label class="label invisible-">Room(s)</label>
                                        <div class="dvQty qtySelector dvInput form-control d-flex justify-content-end p-0 pr-lg-2">
                                            <span class="special-text">Rooms(s)</span>
                                            <div class="d-flex align-items-center pr-0 w-120">
                                                <div class="col-4 text-center px-0"><i role="button" class="fa fa-minus decreaseQty bg-colour2 border p-1 b-radius HotelQynUpdateminus"></i></div>
                                                <div class="col-4 text-center px-0">
                                                    <input id="qtyValue" name="NoOfRooms" type="text" class="form-control bg-transparent border-0 text-center qtyValue px-0" readonly="readonly" runat="server"/>
                                                </div>
                                                <div class="col-4 text-center px-0"><i role="button" class="fa fa-plus increaseQty bg-colour2 border p-1 b-radius HotelQynUpdateplus"></i></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-12 col-lg-3 offset-lg-9 order-6 order-lg-5">
                                         
                                        <button onclick="var retvalue = HotelSearch_Rooms(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" value="Search" class="btn btn-one w-100">Search Hotel</button>
                                    </div>
                                    <div class="col-12 order-5 order-lg-6">
                                        <div class="row">
                                            <div class="col-12 col-sm-auto mb-3" id="Room1">
                                                <div class="dvDropdown dropdown">
                                                    <button type="button" class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" data-toggle="dropdown" data-display="static" aria-expanded="false">
                                                        Passenger
                                                    </button>
                                                    <div class="dropdown-menu prevent-close p-0">
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorAdult1 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Adult(s)">Adult(s) 12+ Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseAdultCount(1)" class="fa fa-minus decreaseQtyAdult1 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueAdult1" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult1" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseAdultCount(1)" class="fa fa-plus increaseQtyAdult1 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="border-top"></div>
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorChild1 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Child(ren)">Child(ren) 2 - 11 Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseChildCount(1)" class="fa fa-minus decreaseQtyChild1 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueChild1" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild1" value="0"  readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseChildCount(1)" class="fa fa-plus increaseQtyChild1 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="dvDropdown dropdown">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        <span class="d-inline-block mr-3 heading-semibold">Room1:</span> 
                                                        <span class="d-none d-sm-inline-block">Passenger</span>
                                                    </button>
                                                    <div class="dropdown-menu prevent-close">
                                                        <div class="dvQtySelectorAdult1 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Adults 18+</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseAdultCount(1)" class="fa fa-minus decreaseQtyAdult1 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueAdult1" class="form-control bg-transparent border-0 text-center qtyValueAdult1" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseAdultCount(1)" class="fa fa-plus increaseQtyAdult1 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                        <div class="dvQtySelectorChild1 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Childrens</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseChildCount(1)" class="fa fa-minus decreaseQtyChild1 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueChild1" class="form-control bg-transparent border-0 text-center qtyValueChild1" value="0"  readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(1)" class="fa fa-plus increaseQtyChild1 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="col-12 col-sm-auto mb-3" id="Room2" style="display: none;">
                                                <div class="dvDropdown dropdown">
                                                    <button type="button" class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" data-toggle="dropdown" data-display="static" aria-expanded="false">
                                                        Passenger
                                                    </button>
                                                    <div class="dropdown-menu prevent-close p-0">
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorAdult2 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Adult(s)">Adult(s) 12+ Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseAdultCount(2)" class="fa fa-minus decreaseQtyAdult2 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueAdult2" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult2" value="1"  readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseAdultCount(2)" class="fa fa-plus increaseQtyAdult2 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="border-top"></div>
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorChild2 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Child(ren)">Child(ren) 2 - 11 Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseChildCount(2)" class="fa fa-minus decreaseQtyChild2 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueChild2" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild2" value="1"  readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseChildCount(2)" class="fa fa-plus increaseQtyChild2 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="dvDropdown dropdown text-right text-sm-left">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        <span class="d-inline-block mr-3 heading-semibold">Room2:</span> 
                                                        <span class="d-none d-sm-inline-block">Passenger</span>
                                                    </button>
                                                    <div class="dropdown-menu prevent-close dropdown-menu-right">
                                                        <div class="dvQtySelectorAdult2 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Adults 18+</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseAdultCount(2)"  class="fa fa-minus decreaseQtyAdult2 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueAdult2" class="form-control bg-transparent border-0 text-center qtyValueAdult2" value="1"  readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseAdultCount(2)" class="fa fa-plus increaseQtyAdult2 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                        <div class="dvQtySelectorChild2 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Childrens</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseChildCount(2)" class="fa fa-minus decreaseQtyChild2 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueChild2" class="form-control bg-transparent border-0 text-center qtyValueChild2" value="1"  readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(2)" class="fa fa-plus increaseQtyChild2 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="col-12 col-sm-auto mb-3" id="Room3" style="display: none;">
                                                <div class="dvDropdown dropdown">
                                                    <button type="button" class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" data-toggle="dropdown" data-display="static" aria-expanded="false">
                                                        Passenger
                                                    </button>
                                                    <div class="dropdown-menu prevent-close p-0">
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorAdult3 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Adult(s)">Adult(s) 12+ Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseAdultCount(3)" class="fa fa-minus decreaseQtyAdult3 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueAdult3" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult3" value="1" readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseAdultCount(3)" class="fa fa-plus increaseQtyAdult3 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="border-top"></div>
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorChild3 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Child(ren)">Child(ren) 2 - 11 Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseChildCount(3)" class="fa fa-minus decreaseQtyChild3 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueChild3" class="form-control bg-transparent border-0 text-center qtyValueChild3" value="1"  readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseChildCount(3)" class="fa fa-plus increaseQtyChild3 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="dvDropdown dropdown">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        <span class="d-inline-block mr-3 heading-semibold">Room3:</span> 
                                                        <span class="d-none d-sm-inline-block">Passenger</span>
                                                    </button>
                                                    <div class="dropdown-menu prevent-close">
                                                        <div class="dvQtySelectorAdult3 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Adults 18+</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseAdultCount(3)" class="fa fa-minus decreaseQtyAdult3 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueAdult3" class="form-control bg-transparent border-0 text-center qtyValueAdult3" value="1" readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseAdultCount(3)" class="fa fa-plus increaseQtyAdult3 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                        <div class="dvQtySelectorChild3 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Childrens</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseChildCount(3)" class="fa fa-minus decreaseQtyChild3 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueChild3" class="form-control bg-transparent border-0 text-center qtyValueChild3" value="1"  readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(3)" class="fa fa-plus increaseQtyChild3 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="col-12 col-sm-auto mb-3" id="Room4" style="display: none;">
                                                <div class="dvDropdown dropdown">
                                                    <button type="button" class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" data-toggle="dropdown" data-display="static" aria-expanded="false">
                                                        Passenger
                                                    </button>
                                                    <div class="dropdown-menu prevent-close dropdown-menu-sm-right dropdown-menu-lg-left p-0">
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorAdult4 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Adult(s)">Adult(s) 12+ Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseAdultCount(4)" class="fa fa-minus decreaseQtyAdult4 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueAdult4" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult4" value="1" readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseAdultCount(4)" class="fa fa-plus increaseQtyAdult4 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="border-top"></div>
                                                        <div class="dropdown-item p-2">
                                                            <div class="dvQtySelectorChild4 dvQtySelector row align-items-center">
                                                                <div class="col-12">
                                                                    <h2 class="h7 special-text" data-i18n="Child(ren)">Child(ren) 2 - 11 Yrs</h2>
                                                                </div>
                                                                <div class="col-6 col-sm-12">
                                                                    <div class="row align-items-center mt-1">
                                                                        <div class="col-4 text-left"><i role="button" onclick="DecreaseChildCount(4)" class="fa fa-minus decreaseQtyChild4 border p-1 b-radius"></i></div>
                                                                        <div class="col-4 text-center">
                                                                            <input type="text" id="qtyValueChild4" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild4" value="1"  readonly="readonly" runat="server">
                                                                        </div>
                                                                        <div class="col-4 text-right"><i role="button" onclick="IncreaseChildCount(4)" class="fa fa-plus increaseQtyChild4 border p-1 b-radius"></i></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="dvDropdown dropdown text-right text-sm-left">
                                                    <button class="h7 btn text-capitalize dropdown-toggle bg-transparent border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                        <span class="d-inline-block mr-3 heading-semibold">Room4:</span> 
                                                        <span class="d-none d-sm-inline-block">Passenger</span>
                                                    </button>
                                                    <div class="dropdown-menu prevent-close dropdown-menu-right">
                                                        <div class="dvQtySelectorAdult4 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Adults 18+</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseAdultCount(4)"  class="fa fa-minus decreaseQtyAdult4 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueAdult4" class="form-control bg-transparent border-0 text-center qtyValueAdult4" value="1" readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseAdultCount(4)" class="fa fa-plus increaseQtyAdult4 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                        <div class="dvQtySelectorChild4 dropdown-item d-flex align-items-center">
                                                            <div class="col-6 special-text h7">Childrens</div>
                                                            <div class="col-6 d-flex align-items-center pr-0">
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseChildCount(4)" class="fa fa-minus decreaseQtyChild4 border p-1 b-radius"></i></div>
                                                                <div class="col-4 text-center px-0">
                                                                    <input type="text" id="qtyValueChild4" class="form-control bg-transparent border-0 text-center qtyValueChild4" value="1"  readonly="readonly" runat="server">
                                                                </div>
                                                                <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(4)" class="fa fa-plus increaseQtyChild4 border p-1 b-radius"></i></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                               
    <%-- X-X-X-X-X-X-X-X-X-X-X-X-X-X-X-X- OLD X-X-X-X-X-X-X-X-X-X-X-X-X-X-X-X- --%>
                                   <%-- <div id="divhtl" class="col-12">
                                    <div id="HotelModifyValidation" class="dvErrors p-1 mb-2 alert alert-danger text-center h6 heading-semibold" style="display:none;"></div>
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <input type="hidden" id="hdnRoomString" />
                                            <label class="label">Destination</label>
                                            <div class="input-group">
                                                <input class="input1 ui-autocomplete-input locationIcon form-control from-icon" type="text" autocomplete="off" runat="server" id="txtCity" onfocus="placeholderOnFocus(this,'Enter City Name');" onblur="placeholderOnFocus(this,'Enter City Name');" value="Enter City Name" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6">
                                                        <i class="fa-solid fa-location-dot"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3">
                                            <label class="label">Check-In</label>
                                            <div class="dvTextBoxCheckin input-group">
                                                <input id="TextBoxCheckin" runat="server" autocomplete="off" class="input1 ui-autocomplete-input datePicker form-control cal-icon" type="text" onfocus="placeholderOnFocus(this,'dd/mm/yyyy');" onblur="placeholderOnFocus(this,'dd/mm/yyyy');" value="dd/mm/yyyy" readonly="readonly" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6">
                                                        <i class="fa-regular fa-calendar"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3">
                                            <label class="label">Check-Out</label>
                                            <div class="dvTextBoxCheckout input-group">
                                                <input class="input1 ui-autocomplete-input datePicker form-control cal-icon" autocomplete="off" type="text" onfocus="placeholderOnFocus(this,'dd/mm/yyyy');" onblur="placeholderOnFocus(this,'dd/mm/yyyy');" value="dd/mm/yyyy" id="TextBoxCheckout" runat="server" readonly="readonly" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="label">Room(s)</label>
                                            <select class="form-control" id="ddlnoofroom">
                                                    <option value="1" selected="selected">1</option>
                                                    <option value="2">2</option>
                                                    <option value="3">3</option>
                                                    <option value="4">4</option>
                                            </select>                                            
                                        </div>
                                    </div>
                                    <div class="row" id="tblDynamic"></div>                 
                                </div> --%>                                      
                            </div>
                        </div>
                    </div>
                </div>
                        
                            <div class="dvProducts row hotels">
                                <asp:Repeater ID="rptHotelList" runat="server" OnItemDataBound="rptHotelList_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="dvProductCard col-sm-6 col-md-4 mb-3 hotelrow">
                                            <div class="dvItem">
                                                <a href="/" class="anchor" onclick='<%# "return getHotelDetails(" + Eval("hotelid") + ");"%>'>
                                                        <div class="img-container">
                                                            <img src="<%#Eval("basicinfo.thumbnailimage")%>" alt="Image not Available" />
                                                        </div>
                                                          <h2 class="px-3 pt-3 pb-2"><%#Eval("basicinfo.hotelname")%></h2>
                                                            <div class="d-flex flex-wrap justify-content-between px-3 pb-3">
                                                                <p class="points">
                                                                    <asp:Label ID="lblmiles" runat="server"></asp:Label>
                                                                    <asp:Label ID="Label1" runat="server" class="">Points</asp:Label>
                                                                </p>
                                                                <p
                                                                    class="points d-none">Physical
                                                                </p>
                                                        </div>
                                                        <asp:HiddenField ID="hdnHotelID" runat="server" Value='<%#Eval("HotelID")%>' />
                                                        <div class="rating d-none" rating='<%#Eval("basicinfo.hotelratings.HotelRating[0].rating")%>'>
                                                        </div>
                                                        <div class="Amenities d-none">
                                                            <asp:Image ID="imgMeeting" runat="server" ImageUrl="~/Images/meet-icon-inactive.svg"
                                                                ToolTip="Meeting Facilities Not Available" />
                                                            <asp:Image ID="imgGym" runat="server" ImageUrl="~/Images/gym-icon-inactive.svg" ToolTip="Gym/Spa Not Available" />
                                                            <asp:Image ID="imgInternet" runat="server" ImageUrl="~/Images/wifi-icon-inactive.svg"
                                                                ToolTip="Internet/Wi-Fi Not Available" />
                                                            <asp:Image ID="imgResturent" runat="server" ImageUrl="~/Images/resto-icon-inactive.svg.png"
                                                                ToolTip="Restaurant/Coffee Shop Not Available" />
                                                            <asp:Image ID="imgswimmingPool" runat="server" ImageUrl="~/Images/pool-icon-inactive.svg.png"
                                                                ToolTip="Swimming Pool Not Available" />
                                                        </div>
                                                  </a>
                                             </div>
                                            <div class="jsondata">
                                                <asp:HiddenField ID="hdnHotelJsonData" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        
                    </div>
                </div>
            </div>
    </div>
    <script type="text/javascript">
        var edit = getUrlVars()["edit"];
        if (edit == "1") {
            $('#modify_search').addClass('show');
        }
        $(document).ready(function () {
            $('#btnfliter').click(function () {
                $('#divSearch').addClass('HotelMoveLeft');
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
            BindRoomsDynamicNew(parseInt($("#CP_qtyValue").val()));
            $('.HotelQynUpdateplus').on('click', function () {
                if (parseInt($("#CP_qtyValue").val()) < 4) {
                    BindRoomsDynamicNew(parseInt($("#CP_qtyValue").val()) + 1);
                }
            });

            $('.HotelQynUpdateminus').on('click', function () {
                if (parseInt($("#CP_qtyValue").val()) > 1) {
                    BindRoomsDynamicNew(parseInt($("#CP_qtyValue").val()) - 1);
                }
            });
            //show datepicker onclick of icon
            $(".dvTextBoxCheckin .input-group-append .input-group-text").on("click", function () {
                $("#CP_TextBoxCheckin").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvTextBoxCheckout .input-group-append .input-group-text").on("click", function () {
                $("#CP_TextBoxCheckout").datepicker("show");
            });
            var minVal = 1, maxVal = 4; // Set Max and Min values
            $(".increaseQty").on('click', function () {
                var $parentElm = $(this).parents(".qtySelector");
                $(this).addClass("clicked");
                setTimeout(function () {
                    $(".clicked").removeClass("clicked");
                }, 100);
                var value = $parentElm.find(".qtyValue").val();
                if (value < maxVal) {
                    value++;
                }
                $parentElm.find(".qtyValue").val(value);
               // BindRoomsDynamic();
            });
            $(".decreaseQty").on('click', function () {
                var $parentElm = $(this).parents(".qtySelector");
                $(this).addClass("clicked");
                setTimeout(function () {
                    $(".clicked").removeClass("clicked");
                }, 100);
                var value = $parentElm.find(".qtyValue").val();
                if (value > 1) {
                    value--;
                }
                $parentElm.find(".qtyValue").val(value);
               // BindRoomsDynamic();
            });

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

        function BindRoomsDynamicNew(Count) {
            var RoomCount = 0;
            var $parentElm = $(this).parents(".qtySelector");
            for (var i = 0; i <= 4; i++) {
                RoomCount = i + 1;
                $parentElm.find(".qtyValue").val(RoomCount);
                if (i <= Count) {
                    $("#Room" + i).show();
                } else {
                    $("#Room" + i).hide();
                }
            }
        }

        var minAdultVal = 1, maxAdultVal = 4; // Set Max and Min values for Adult
        var minChildVal = 0, maxChildVal = 2; // Set Max and Min values for Child

        function IncreaseAdultCount(Roomno) {

            //var $parentElm = document.getElementsByClassName("dvQtySelectorAdult" + Roomno);
            //var $parentElm = $(this).parents(".dvQtySelectorAdult" + Roomno);
            var $parentElm = $(".increaseQtyAdult" + Roomno).parents(".dvQtySelectorAdult" + Roomno);

            $(this).addClass("clicked");
            setTimeout(function () {
                $(".clicked").removeClass("clicked");
            }, 100);
            var value = $parentElm.find(".qtyValueAdult" + Roomno).val();
            if (value < maxAdultVal) {
                value++;
            }
            $parentElm.find(".qtyValueAdult" + Roomno).val(value);

        }

        function DecreaseAdultCount(Roomno) {

            var $parentElm = $(".increaseQtyAdult" + Roomno).parents(".dvQtySelectorAdult" + Roomno);
            $(this).addClass("clicked");
            setTimeout(function () {
                $(".clicked").removeClass("clicked");
            }, 100);
            var value = $parentElm.find(".qtyValueAdult" + Roomno).val();
            if (value > 1) {
                value--;
            }
            $parentElm.find(".qtyValueAdult" + Roomno).val(value);

        }

        function IncreaseChildCount(Roomno) {

            var $parentElm = $(".increaseQtyChild" + Roomno).parents(".dvQtySelectorChild" + Roomno);
            //var $parentElm = document.getElementsByClassName("dvQtySelectorChild" + Roomno);
            $(this).addClass("clicked");
            setTimeout(function () {
                $(".clicked").removeClass("clicked");
            }, 100);
            var value = $parentElm.find(".qtyValueChild" + Roomno).val();
            if (value < maxChildVal) {
                value++;
            }
            $parentElm.find(".qtyValueChild" + Roomno).val(value);

        }

        function DecreaseChildCount(Roomno) {
            var $parentElm = $(".increaseQtyChild" + Roomno).parents(".dvQtySelectorChild" + Roomno);
            //var $parentElm = $(this).parents(".dvQtySelectorChild" + Roomno);
            $(this).addClass("clicked");
            setTimeout(function () {
                $(".clicked").removeClass("clicked");
            }, 100);
            var value = $parentElm.find(".qtyValueChild" + Roomno).val();
            if (value > 0) {
                value--;
            }
            $parentElm.find(".qtyValueChild" + Roomno).val(value);
        }
    </script>
</asp:Content>
