<%@ Page Title="Hotel Search" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="HotelSearch.aspx.cs" Inherits="HotelSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <meta name="viewport" content="width=device-width" />
    <%--<link href="\Css/jquery.ui.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="\Css/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="\Css/HotelMain.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="\Css/hotel.css" />

    <!--selectric-->
    <%--<link href="\Css/dropDown.css" rel="stylesheet" type="text/css" />
    <script src="\Jquery/jquery.selectric.min.js" type="text/javascript"></script>--%>

    <%--for Hotel--%>
    <script src="Jquery/HotelSearchScript.js" type="text/javascript"></script>
    <style>
        div#sitemap {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var hotelcity = $("#CP_txtCity").val();
            if (hotelcity != "Enter City Name") {
                $("#CP_TextBoxCheckin").datepicker().datepicker("setDate", new Date());

                $("#CP_TextBoxCheckout").datepicker().datepicker("setDate", new Date());
            }

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

            GetRedemptionOptions();
            //$("#dvHeroSlider").hide();
            //$("#dvInnerBanner").attr("src", "images/hotelpage/hotel-banner.jpg");

            //scrollTop
            $('html, body').animate({
                scrollTop: $('.dvHotelSearch').offset().top
            }, 'slow');

            //show datepicker onclick of icon
            $(".dvTextBoxCheckin .input-group-append .input-group-text").on("click", function () {
                $("#CP_TextBoxCheckin").datepicker("show");
            });
            //show datepicker onclick of icon
            $(".dvTextBoxCheckout .input-group-append .input-group-text").on("click", function () {
                $("#CP_TextBoxCheckout").datepicker("show");
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
            initAutocomplete(".dvHotelSearch #CP_txtCity", ".dvHotelSearch .dvCP_txtCity");
        });
        function SelectDestinationToInSearchOption(DestinationTo) {
            $('#CP_txtCity').val(DestinationTo);
            var hotelcity = $("#CP_txtCity").val();
            if (hotelcity != "Enter City Name") {
                $("#CP_TextBoxCheckin").datepicker().datepicker("setDate", new Date());

                $("#CP_TextBoxCheckout").datepicker().datepicker("setDate", new Date());
            }
        }

        function BindRoomsDynamicNew(Count) {
            var RoomCount = 0;
            for (var i = 0; i <= 4; i++) {
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

   <div class="dvHotelSearch pb-5" id="hotelscrollupAnchor">
            <div class="container-xl">
                <div class="row">
                    <div class="col-12">
                        <div class="row">
                            <div id="dvForm" class="dvForm col-12">
                                <div class="bg-colour2 b-radius p-3">
                                     <div class="row">
                                        <div class="col-12 order-0">
                                            <div id="HotelModifyValidation" class="dvErrors p-1 mb-2 alert alert-danger text-center h6 heading-semibold" style="display: none;"></div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3 order-1">
                                            <input type="hidden" id="hdnRoomString" />
                                            <label class="label">City</label>
                                            <div class="dvCP_txtCity dvInputGroup input-group">
                                                <input id="CP_txtCity" class="form-control" onfocus="placeholderOnFocus(this,'Enter City Name');" onblur="placeholderOnFocus(this,'Enter City Name');" value="Enter City Name" type="text" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6">
                                                        <i class="fa-solid fa-location-dot"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3 order-2">
                                            <label class="label">Check-in</label>
                                            <div class="dvInputGroup input-group">
                                                <input class="form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" type="text" id="CP_TextBoxCheckin" readonly="readonly" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6">
                                                        <i class="fa-regular fa-calendar"></i>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3 order-3">
                                            <label class="label">Check-out</label>
                                            <div class="dvInputGroup input-group">
                                                <input class="form-control" onfocus="placeholderOnFocus(this,'Enter Date');" onblur="placeholderOnFocus(this,'Enter Date');" value="Enter Date" type="text" id="CP_TextBoxCheckout" readonly="readonly" />
                                                <div class="input-group-append">
                                                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg mb-3 order-4">
                                            <label class="label invisible-">Room(s)</label>
                                            <div class="dvInput dvQty qtySelector form-control d-flex justify-content-end p-0 pr-lg-2">
                                                <span class="special-text">Rooms(s)</span>
                                                <div class="d-flex align-items-center pr-0 w-120">
                                                    <div class="col-4 text-center px-0"><i role="button" class="fa fa-minus decreaseQty bg-colour2 border p-1 b-radius HotelQynUpdateminus"></i></div>
                                                    <div class="col-4 text-center px-0">
                                                        <input id="qtyValue" name="NoOfRooms" type="text" class="form-control bg-transparent border-0 text-center qtyValue px-0" value="1" readonly="readonly" runat="server"/>
                                                    </div>
                                                    <div class="col-4 text-center px-0"><i role="button" class="fa fa-plus increaseQty bg-colour2 border p-1 b-radius HotelQynUpdateplus"></i></div>
                                                </div>
                                            </div>
                                            <%--<label class="label">Room(s)</label>
                                            <select class="form-control" id="ddlnoofroom">
                                                <option value="1" selected="selected">1</option>
                                                <option value="2">2</option>
                                                <option value="3">3</option>
                                                <option value="4">4</option>
                                            </select>--%>
                                        </div>
                                        <div class="col-12 col-md-6 col-lg-3 order-6 order-lg-5">
                                            <label class="invisible d-none d-lg-block">i</label>
                                            <button onclick="var retvalue = SearchRooms(); event.returnValue= retvalue;event.preventDefault(); return retvalue;" type="button" value="Search" class="btn btn-one w-100">Search Hotel</button>
                                        </div>
                                        <div class="col-12 d-none order-6">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="row" id="tblDynamic"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 order-0 mb-lg-3">
                                            <div class="row">
                                                <div class="col-12 col-sm-auto mb-3 mb-lg-0" id="Room1" >
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
                                                                                <input type="text" id="qtyValueAdult1" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult1" value="1" readonly="readonly">
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
                                                                                <input type="text" id="qtyValueChild1" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild1" value="0" readonly="readonly">
                                                                            </div>
                                                                            <div class="col-4 text-right"><i role="button" onclick="IncreaseChildCount(1)" class="fa fa-plus increaseQtyChild1 border p-1 b-radius"></i></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="dvDropdown dropdown">
                                                        <button class="h7 btn text-capitalize dropdown-toggle bg-transparent d-flex align-items-center border-0 p-0 text-colour6" type="button" data-toggle="dropdown" aria-expanded="false">
                                                            <span class="d-inline-block mr-3 heading-semibold">Room1:</span> 
                                                            <span class="d-none d-sm-inline-block">Passenger</span>
                                                        </button>
                                                        <div class="dropdown-menu prevent-close">
                                                            <div class="dvQtySelectorAdult1 dropdown-item d-flex align-items-center">
                                                                <div class="col-6 special-text h7">Adults 18+</div>
                                                                <div class="col-6 d-flex align-items-center pr-0">
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseAdultCount(1)" class="fa fa-minus decreaseQtyAdult1 border p-1 b-radius"></i></div>
                                                                    <div class="col-4 text-center px-0">
                                                                        <input type="text" id="qtyValueAdult1" class="form-control bg-transparent border-0 text-center qtyValueAdult1" value="1" readonly="readonly" runat="server">
                                                                    </div>
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseAdultCount(1)" class="fa fa-plus increaseQtyAdult1 border p-1 b-radius"></i></div>
                                                                </div>
                                                            </div>
                                                            <div class="dvQtySelectorChild1 dropdown-item d-flex align-items-center">
                                                                <div class="col-6 special-text h7">Childrens</div>
                                                                <div class="col-6 d-flex align-items-center pr-0">
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseChildCount(1)" class="fa fa-minus decreaseQtyChild1 border p-1 b-radius"></i></div>
                                                                    <div class="col-4 text-center px-0">
                                                                        <input type="text" id="qtyValueChild1" class="form-control bg-transparent border-0 text-center qtyValueChild1" value="0" readonly="readonly" runat="server">
                                                                    </div>
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(1)" class="fa fa-plus increaseQtyChild1 border p-1 b-radius"></i></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="col-12 col-sm-auto mb-3 mb-lg-0" id="Room2" style="display: none;" >
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
                                                                                <input type="text" id="qtyValueAdult2" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult2" value="1" readonly="readonly">
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
                                                                                <input type="text" id="qtyValueChild2" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild2" value="1" readonly="readonly">
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
                                                                        <input type="text" id="qtyValueAdult2" class="form-control bg-transparent border-0 text-center qtyValueAdult2" value="1" readonly="readonly" runat="server">
                                                                    </div>
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseAdultCount(2)" class="fa fa-plus increaseQtyAdult2 border p-1 b-radius"></i></div>
                                                                </div>
                                                            </div>
                                                            <div class="dvQtySelectorChild2 dropdown-item d-flex align-items-center">
                                                                <div class="col-6 special-text h7">Childrens</div>
                                                                <div class="col-6 d-flex align-items-center pr-0">
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseChildCount(2)" class="fa fa-minus decreaseQtyChild2 border p-1 b-radius"></i></div>
                                                                    <div class="col-4 text-center px-0">
                                                                        <input type="text" id="qtyValueChild2" class="form-control bg-transparent border-0 text-center qtyValueChild2" value="1" readonly="readonly" runat="server">
                                                                    </div>
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(2)" class="fa fa-plus increaseQtyChild2 border p-1 b-radius"></i></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="col-12 col-sm-auto mb-3 mb-lg-0" id="Room3" style="display: none;" >
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
                                                                                <input type="text" id="qtyValueAdult3" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult3" value="1" readonly="readonly">
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
                                                                                <input type="text" id="qtyValueChild3" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild3" value="1" readonly="readonly">
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
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="DecreaseAdultCount(3)" onclick="IncreaseAdultCount(3)" class="fa fa-minus decreaseQtyAdult3 border p-1 b-radius"></i></div>
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
                                                                        <input type="text" id="qtyValueChild3" class="form-control bg-transparent border-0 text-center qtyValueChild3" value="1" readonly="readonly" runat="server">
                                                                    </div>
                                                                    <div class="col-4 text-center px-0"><i role="button" onclick="IncreaseChildCount(3)" class="fa fa-plus increaseQtyChild3 border p-1 b-radius"></i></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="col-12 col-sm-auto mb-3 mb-lg-0" id="Room4" style="display: none;" >
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
                                                                                <input type="text" id="qtyValueAdult4" class="form-control p-0 bg-transparent border-0 text-center qtyValueAdult4" value="1" readonly="readonly">
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
                                                                                <input type="text" id="qtyValueChild4" class="form-control p-0 bg-transparent border-0 text-center qtyValueChild4" value="1" readonly="readonly">
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
                                                                        <input type="text" id="qtyValueChild4" class="form-control bg-transparent border-0 text-center qtyValueChild4" value="1" readonly="readonly" runat="server">
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
    <div class="dvHotelProductList mb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12 text-center m-auto my-3">
                    <h2 class="heading1 pb-4 px-3">Popular Hotels Around The Globe</h2>
                </div>
            </div>
            <div class="row">
                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('SG,Singapore,Singapore');">
                    <div class="dvItem">
                        <a class="anchor" href="#hotelscrollupAnchor" rel="" id="hotelAnchorSG">
                            <div class="img-container">
                                <img class="w-100" src="Images/hotelpage/hotel-img1.jpg" style="cursor: pointer;" />
                            </div>
                        </a>
                        <h2 class="mt-3 mb-2 mx-3">leonardo</h2>
                        <p class="px-3 mb-3">Seminyak-Beach-Denpasar, -BA, Indonesia</p>
                        <div class="d-flex justify-content-between align-items-center mt-auto px-3">
                            <p class="points mb-0 d-none">
                                From 4,000 Points
                            </p>
                            <div class="dvicon">
                                <img src="Images/hotelpage/hotel-icon1.svg" />
                                <img src="Images/hotelpage/hotel-icon2.svg" />
                                <img src="Images/hotelpage/hotel-icon3.svg" />
                                <img src="Images/hotelpage/hotel-icon4.svg" />
                                <img src="Images/hotelpage/hotel-icon5.svg" />
                            </div>
                        </div>
                        <div class="rating px-3 mb-3">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                        </div>

                    </div>
                </div>
                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('TH,Thailand,Bangkok');">
                    <div class="dvItem">
                        <a class="anchor" href="#hotelscrollupAnchor" rel="" id="hotelAnchorTH">
                            <div class="img-container">
                                <img class="w-100" src="Images/hotelpage/hotel-img2.jpg" style="cursor: pointer;" />
                            </div>
                        </a>
                        <h2 class="mt-3 mb-2 mx-3">The-Hoxton</h2>
                        <p class="px-3 mb-3">Rome, Italy</p>
                        <div class="d-flex justify-content-between align-items-center mt-auto px-3">
                            <p class="points mb-0 d-none">
                                From 5,000 Points
                            </p>
                            <div class="dvicon">
                                <img src="Images/hotelpage/hotel-icon1.svg" />
                                <img src="Images/hotelpage/hotel-icon2.svg" />
                                <img src="Images/hotelpage/hotel-icon3.svg" />
                                <img src="Images/hotelpage/hotel-icon4.svg" />
                                <img src="Images/hotelpage/hotel-icon5.svg" />
                            </div>
                        </div>
                        <div class="rating px-3 mb-3">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                        </div>

                    </div>
                </div>
                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('FR,France,Paris');">
                    <div class="dvItem">
                        <a class="anchor" href="#hotelscrollupAnchor" rel="" id="hotelAnchorFR">
                            <div class="img-container">
                                <img class="w-100" src="Images/hotelpage/hotel-img3.jpg" style="cursor: pointer;" />
                            </div>
                        </a>
                        <h2 class="mt-3 mb-2 mx-3">The-Singular-Patagonia</h2>
                        <p class="px-3 mb-3">Puerto-Natales</p>
                        <div class="d-flex justify-content-between align-items-center mt-auto px-3">
                            <p class="points mb-0 d-none">From 6,000 Points</p>
                            <div class="dvicon">
                                <img src="Images/hotelpage/hotel-icon1.svg" />
                                <img src="Images/hotelpage/hotel-icon2.svg" />
                                <img src="Images/hotelpage/hotel-icon3.svg" />
                                <img src="Images/hotelpage/hotel-icon4.svg" />
                                <img src="Images/hotelpage/hotel-icon5.svg" />
                            </div>
                        </div>
                        <div class="rating px-3 mb-3">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                        </div>

                    </div>
                </div>
                <div class="dvProductCard col-sm-6 col-md-4 col-lg-3 mb-4" onclick="SelectDestinationToInSearchOption('NZ,New Zealand,Christchurch');">
                    <div class="dvItem">
                        <a class="anchor" href="#hotelscrollupAnchor" rel="" id="hotelAnchorNZ">
                            <div class="img-container">
                                <img class="w-100" src="Images/hotelpage/hotel-img4.jpg" style="cursor: pointer;" />
                            </div>
                        </a>
                        <h2 class="mt-3 mb-2 mx-3">Taj falaknuma palace</h2>
                        <p class="px-3 mb-3">Hyderabad, India</p>
                        <div class="d-flex justify-content-between align-items-center mt-auto px-3">
                            <p class="points mb-0 d-none">From 4,800 Points</p>
                            <div class="dvicon">
                                <img src="Images/hotelpage/hotel-icon1.svg" />
                                <img src="Images/hotelpage/hotel-icon2.svg" />
                                <img src="Images/hotelpage/hotel-icon3.svg" />
                                <img src="Images/hotelpage/hotel-icon4.svg" />
                                <img src="Images/hotelpage/hotel-icon5.svg" />
                            </div>
                        </div>
                        <div class="rating px-3 mb-3">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-fill.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                            <img src="images/icons/other/star-blank.svg" alt="">
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        /*$("#ddlnoofroom").selectmenu({}).selectmenu("menuWidget").addClass("overflow");
        $("#adult").selectmenu({}).selectmenu("menuWidget").addClass("overflow");
        $("#child").selectmenu({}).selectmenu("menuWidget").addClass("overflow");*/
        $(document).ready(function () {
            BindRoomsDynamic();

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
                BindRoomsDynamic();
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
                BindRoomsDynamic();
            });

            var minAdultVal = 1, maxAdultVal = 4; // Set Max and Min values for Adult
            var minChildVal = 0, maxChildVal = 2; // Set Max and Min values for Child

            //$(".increaseQtyAdult1").on('click', function () {
            //    var $parentElm = $(this).parents(".dvQtySelectorAdult1");
            //    $(this).addClass("clicked");
            //    setTimeout(function () {
            //        $(".clicked").removeClass("clicked");
            //    }, 100);
            //    var value = $parentElm.find(".qtyValueAdult1").val();
            //    if (value < maxAdultVal) {
            //        value++;
            //    }
            //    $parentElm.find(".qtyValueAdult1").val(value);
            //});

            //$(".decreaseQtyAdult1").on('click', function () {
            //    var $parentElm = $(this).parents(".dvQtySelectorAdult1");
            //    $(this).addClass("clicked");
            //    setTimeout(function () {
            //        $(".clicked").removeClass("clicked");
            //    }, 100);
            //    var value = $parentElm.find(".qtyValueAdult1").val();
            //    if (value > 1) {
            //        value--;
            //    }
            //    $parentElm.find(".qtyValueAdult1").val(value);
            //});

            //$(".increaseQtyChild1").on('click', function () {
            //    var $parentElm = $(this).parents(".dvQtySelectorChild1");
            //    $(this).addClass("clicked");
            //    setTimeout(function () {
            //        $(".clicked").removeClass("clicked");
            //    }, 100);
            //    var value = $parentElm.find(".qtyValueChild1").val();
            //    if (value < maxChildVal) {
            //        value++;
            //    }
            //    $parentElm.find(".qtyValueChild1").val(value);
            //});

            //$(".decreaseQtyChild1").on('click', function () {
            //    var $parentElm = $(this).parents(".dvQtySelectorChild1");
            //    $(this).addClass("clicked");
            //    setTimeout(function () {
            //        $(".clicked").removeClass("clicked");
            //    }, 100);
            //    var value = $parentElm.find(".qtyValueChild1").val();
            //    if (value > 0) {
            //        value--;
            //    }
            //    $parentElm.find(".qtyValueChild1").val(value);
            //});
        });
    </script>

</asp:Content>