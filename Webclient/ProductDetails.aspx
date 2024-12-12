<%@ Page Title="Product Details" Language="C#" MasterPageFile="SiteShopMaster.master" AutoEventWireup="true" CodeFile="ProductDetails.aspx.cs" Inherits="ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPSHOP" runat="Server">
    <link rel="stylesheet" href="\Css/shop.css" />
    <style> 
        /*#divDynamicContent {
            max-height: 400px;
            overflow-x: hidden;
        }*/
        .dvInnerBanner,
        .dvRedemptionMenu{
            display:none;
        }
    </style> 
<div class="dvBreadcrumbs">
     <div class="container-xl">
         <nav  id="divBreadbrums" runat="server">
             <%--<ul class="breadcrumb px-0 py-3">
                 <li class="mr-3"><a href="hoteldetails.html"><img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                 </li>
                 <li class="breadcrumb-item"><a href="\">Home</a></li>
                 <li class="breadcrumb-item active">Shop</li>
                 <li class="breadcrumb-item active">Product Details</li>
             </ul>--%>
         </nav>
     </div>
 </div>
    
    <div class="dvProductDetail my-4">
        <div class="container-xl prodDetail dvProductDetails">
            <div class="row">
                <!-- Image -->
                <div class="dvThumbSwiperSlider col-lg-4 col-xl-4">
                    <div class="border b-radius p-3">
                        <div class="swiper dvThumbBannerSlide">
                            <div class="swiper-wrapper" id="imgProductImageMain" runat="server">
                                <%--<div class="swiper-slide img-container">
                                <img id="imgProductImageMain" runat="server" />
                             </div>--%>
                            </div>
                            <div class="swiper-button-next mr-3">
                                <img src="images/icons/arrows/right-yellow-arrow-2.svg" />
                            </div>
                            <div class="swiper-button-prev ml-3">
                                <img src="images/icons/arrows/left-yellow-arrow-2.svg" />
                            </div>
                        </div>
                        <div class="my-2"></div>
                        <div class="swiper dvThumbSlide">
                            <div class="swiper-wrapper" id="divThumbnailImages" runat="server">
                            </div>
                            <div class="swiper-button-next">
                                <img src="images/icons/arrows/right-yellow-arrow-2.svg" />
                            </div>
                            <div class="swiper-button-prev">
                                <img src="images/icons/arrows/left-yellow-arrow-2.svg" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Add to cart -->
                <div class="dvProductInfo col-lg-8 col-xl-8">
                    <div class="dvInfo row mb-3 mt-4 mt-lg-0"">
                        <div class="col-12">
                          <h2 class="h5 heading-bold text-colour7 mb-2" id="spanProductName" runat="server"></h2>
                            <div id="ratings" runat="server"></div>
                          <h2 class="h6 heading-semibold text-colour7 mb-3"><span id="spanPoints" runat="server"></span></h2>
                        </div>
                    </div>
                    <div class="dvAdd row mb-4">
                        <div class="dvAddToCart col-12">
                            <div class="row">
                                <div class="form-group col-12 col-sm-3" id="divQuantity" runat="server">
                                     <h2 class="h6 heading-regular text-colour7 mb-2">Quantity</h2>
                                      <div class="dvPlusMinusButtons">                                            
                                          <div class="input-group">
                                              <div class="input-group-prepend">
                                                  <button type="button" class="btn btn-one" data-type="minus" data-field="" onclick="quantityMinus()">
                                                      <i class="fa fa-minus"></i>
                                                  </button>
                                              </div>
                                              <span id="qtyRealtime" runat="server" style="display: none;" />
                                              <input type="text" class="form-control text-center text-colour7 border" runat="server" id="quantity" name="quantity" strp="1" readonly="readonly" min="1" max="100" value="1" />
                                              <div class="input-group-append">
                                                  <button type="button" class="btn btn-one" data-type="plus" data-field="" onclick="quantityPlus()">
                                                      <i class="fa fa-plus"></i>
                                                  </button>
                                              </div>
                                          </div>
                                      </div>
                                       <%-- <div class="row align-items-center">
                                          <div class="plus col-auto pr-0">
                                            <button type="button" class="btn btn-addtocart p-0" data-type="minus" data-field="" onclick="quantityMinus()">
                                              <i class="fa fa-minus"></i>
                                            </button>
                                          </div>
                                          <div class="value col-4 px-0">
                                            <input type="text" class="form-control text-center text-colour7" runat="server" id="quantity" name="quantity" strp="1" value="1" readonly="readonly" />
                                          </div>
                                          <div class="minus col-auto pl-0">
                                            <button type="button" class="btn btn-addtocart p-0" data-type="plus" data-field="" onclick="quantityPlus()">
                                              <i class="fa fa-plus"></i>
                                            </button>
                                          </div>
                                       </div>--%>
                                </div>
                                 <div class="form-group col-12 col-sm-3">
                                     <h2 id="lbldivPoints" runat="server" class="h6 heading-regular text-colour7 mb-2">Total Points</h2>
                                     <div class="border b-radius h6 text-colour7 heading-semibold p-2" id="divspanpoints" runat="server">
                                     </div>
                                 </div>
                            </div>
                         </div>
                    </div>
                    <div class="row">  
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <h3><span ></span></h3>
                                    
                                    <h2 class="price"></h2>
                                </div>
                            </div>
                        </div> 
                    </div>
                    <div class="dvDenominations row">  
                        <div class="form-group" id="divColor" runat="server">
                            <label>Color</label>
                            <span id="spanColor" runat="server"></span>
                            <br />
                        </div>
                        <div class="form-group" id="divStorage" runat="server">
                            <label>Storage</label>
                            <span id="spanStorage" runat="server"></span>
                        </div>
                        <div class="form-group" id="divSize" runat="server">
                            <label>Size</label><br />
                            <span id="spanSize" runat="server"></span>
                        </div>
                        <div class="col-12 mb-3" id="divDenomination" runat="server">
                            <div class="row">  
                            <div class="col-12">
                                <label id="lblDenomination" runat="server" class="h6 heading-regular text-colour7 mb-2">Denominations :</label>
                            </div>
                            <div id="spanDenomination" runat="server" class="col-12"></div>
                            </div>
                        </div>
                             
                    </div>
                    <div class="row mb-4">
                        <div class="dvButtons col-12" id="divBuy" runat="server">
                            <div class="row">
                               <div class="col-12 col-sm-4 col-xl-3 mb-3 mb-sm-0">
                                    <a href="javascript:void(0);" class="btn btn-two w-100 order-sm-0" id="btnBack" runat="server">
                                        <div class="text-center">
                                            Back
                                        </div>
                                    </a>                                   
                               </div>
                               <div class="col-12 col-sm-4 col-xl-3 mb-3 mb-sm-0">
                                   <a href="javascript:void(0);" class="btn btn-one w-100 order-sm-2" id="btnredeem" runat="server"  onclick="Checkout()" >
                                      <div class="text-center">
                                          Redeem
                                      </div>
                                  </a>
                               </div>
                               <div class="col-12 col-sm-4 col-xl-3" id="addToCartButton">
                                    <a href="javascript:void(0);" class="btn btn-two w-100 order-sm-1" onclick="AddItemToCart()">
                                        <div class="text-center">
                                            Add To Cart
                                        </div>
                                    </a>
                               </div>
                              </div>
                        </div>
                    </div>
                    <div class="row">  
                        <div class="col-12">
                            <div class="form-group" id="divOutOfStock" runat="server" style="display: none;">
                                <h2 class="h6 heading-semibold text-danger">Out Of Stock</h2>
                            </div>
                            <div class="form-group" id="divInsufficient" runat="server" style="display: none;">
                                <h2 class="h6 heading-semibold text-danger">Insufficient Points</h2>
                            </div>
                             <div class="form-group" id="divEmailErrorMsg" runat="server">
                                <h2 class="h6 heading-semibold text-danger">You cannot proceed for redemption since there is no email address updated , kindly contact bank to update the email address.</h2>
                            </div>
                        </div>
                    </div>
                    <div class="dvDetails row mb-4">
                      <div class="dvTabs col-12">
                        <nav>
                         <div class="nav nav-tabs flex-nowrap scroll-hoz border-bottom-0" id="nav-tab" role="tablist" >
                            <button class="heading-semibold nav-link text-capitalize active mr-2" id="description-tab" data-toggle="tab" data-target="#description" type="button"> description </button>
                            <button class="heading-semibold nav-link text-capitalize" id="terms-conditions-tab" data-toggle="tab" data-target="#terms-conditions" type="button">Terms And Conditions</button>
                            <button class="heading-semibold nav-link text-capitalize" id="divSpecificationtab" data-toggle="tab" data-target="#specification" type="button" runat="server" visible="false" > Specifications </button>
                          </div>
                        </nav>
                        <div class="tab-content" id="nav-tabContent">
                          <div class="tab-pane fade show active border p-3" id="description">
                            <div id="divDescription" runat="server"></div>
                          </div>
                          <div class="tab-pane fade border p-3" id="terms-conditions">
                             <div id="divTermsandCondition" runat="server"></div>
                          </div>
                          <div class="tab-pane fade border p-3" id="specification">
                               <div id="divSpecification" runat="server">
                          </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="dvDescription row mt-4 d-none">
                        <div class="col-12 col-lg-12 mb-5">
                            <ul class="widgetTab">
                                <li><a class="tablinks active" onclick="openCity(event, 'Description-blk')">Description</a>
                                </li>
                                <li id="litermsandcondition" runat="server" style="display: none;"><a class="tablinks" onclick="openCity(event, 'TermsandCondition-blk')"><i class="fa fa-edit d-none"></i>Terms And Conditions</a></li>
                            </ul>

                            <div id="Description-blk" class="tabcontent" style="display: block;">
                                
                            </div>

                            <div id="TermsandCondition-blk" class="tabcontent">
                               
                            </div>
                            <div id="Specifications-blk" class="tabcontent">
                               
                            </div>

                            <div id="Review-blk" class="tabcontent">
                                <div id="reviews" class="card-body">
                                </div>
                                <div id="writeReview" class="card-body">
                                    <input id="productRating" type="hidden" value="" />
                                    <span id="star1" class="fa fa-star-o checked" onclick="rate(1);return false;"></span>
                                    <span id="star2" class="fa fa-star-o checked" onclick="rate(2);return false;"></span>
                                    <span id="star3" class="fa fa-star-o checked" onclick="rate(3);return false;"></span>
                                    <span id="star4" class="fa fa-star-o checked" onclick="rate(4);return false;"></span>
                                    <span id="star5" class="fa fa-star-o checked" onclick="rate(5);return false;"></span>
                                    <div>
                                        <div class="reviewForm">
                                            <div class="row">
                                                <div class="col-12 col-lg-3">
                                                    <label>Title:</label>
                                                    <input id="reviewTitle" class="form-control inputHeight" type="text" />
                                                </div>
                                                <div class="col-12 col-lg-7">
                                                    <label>Review:</label>
                                                    <textarea id="reviewText" class="form-control inputHeight"></textarea>
                                                </div>

                                                <div class="col-12 col-lg-2">
                                                    <a href="javascript:void(0);" class="blue_button mt-4" style="width: 100%;" onclick="addReview();">Submit
                                                    </a>
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
    </div>
        <!-- Scrollable modal -->
        <!-- Modal for all vouchers -->
      <div class="dvCommonModal modal fade dvVouchersPopup" id="formpopup" tabindex="-1">
          <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
              <%--<div class="modal-header"> 
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true" class="text-colour6">&times;</span>
                </button>
              </div>--%>
              <div class="modal-body">
                  <button type="button" class="close" data-dismiss="modal">
                      <i class="fa-solid fa-xmark"></i>
                  </button>
                  <div id="divDynamicContent">
                  </div>
                   <div id="divErrorMsg" class="dvErrors text-danger"></div>
              </div>
             <div class="modal-footer justify-content-center border-0"  id="divsubmit"> 
                      <span id="Span1" runat="server">
                       <a id="abtnContinue" href="javascript:void(0);" class="btn btn-one" onclick="submitUserInputMetas();">Continue</a>
                   </span>
                </div>
            </div>
          </div>
        </div>
        
       
       <%--<div class="hover_bkgr_fricc">
            <span class="helper"></span>
            <div>
                  <div class="popupCloseButton">&times;</div>
                    <div id="divDynamicContent">
                   </div>
                <div id="divErrorMsg" class="dvErrors text-danger"></div>
                <div class="text-center" id="divsubmit">
                    <span id="Span5" runat="server">
                        <a id="abtnContinue" href="javascript:void(0);" class="btn btn-one" onclick="submitUserInputMetas();">Continue</a>
                    </span>
                </div>
            </div>
        </div> --%>
    

    <!-- Modal Quantity plus minus-->
    <div class="dvCommonModal modal fade" id="dvQuantityModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title" id="exampleModalLabel"></h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body text-center" id="divmessage">
                    <p class="h6 heading-semibold text-colour7" id="popupmessage"></p>
                </div>
                <div class="modal-footer justify-content-center border-0">
                    <button type="button" class="btn btn-one" data-dismiss="modal">Ok</button>
                    <%--<button type="button" class="btn btn-one">Save changes</button>--%>
                </div>
            </div>
        </div>
    </div>

    <!--Alert Modal -->
    <div class="dvCommonModal dvAlertModal modal fade" id="dvAlertModal" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header border-0">
                    <h5 class="modal-title">
                        <span>Alert</span>
                    </h5>
                    <button type="button" class="close" data-dismiss="modal">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
                <div class="modal-body text-center" id="alertmessage">
                    <p class="h6 text-colour7 heading-semibold" id="errormessage"></p>
                </div>
                <div class="modal-footer justify-content-center border-0 px-0">
                    <button type="button" class="btn btn-one" data-dismiss="modal">Ok</button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfProductId" runat="server" />
    <asp:HiddenField ID="hfProductType" runat="server" />
    <asp:HiddenField ID="hfUserInputMetasAvailable" runat="server" />
    <asp:HiddenField ID="hfCheckoutType" runat="server" />
    <input type="hidden" id="hfColor" runat="server" value="" />
    <input type="hidden" id="hfSize" runat="server" value="" />
    <input type="hidden" id="hfStorage" runat="server" value="" />
    <input type="hidden" id="hfValue" runat="server" value="" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#formpopup').modal('hide');
            var ProductType = getQuerystring("ProductType");
            var ProductId = getQuerystring("ProductId");
            if (ProductType == "physical") {
                $('#addToCartButton').show()
            }
            else if (ProductType == "digital") {
                $('#addToCartButton').hide()
            }
            $('.popupCloseButton').click(function () {
                var IDs = [];
                $("#divDynamicContent").find("input").each(function () {
                    if ($(this).attr("id") != "jsonform-1-elt-fields.operator") {
                        IDs.push(($(this).attr("id")));
                    }
                });
                IDs.forEach(ClearFields);
                $('#formpopup').modal('hide');
            });
            IsValidProduct(ProductId);
        });

        function rate(rating) {
            $("#productRating").val(rating);
            for (var i = 1; i <= 5; i++) {
                if (i <= rating) {
                    $("#star" + i).removeClass("fa fa-star-o checked");
                    $("#star" + i).addClass("fa fa-star checked");
                } else {
                    $("#star" + i).removeClass("fa fa-star checked");
                    $("#star" + i).addClass("fa fa-star-o checked");
                }
            }
            return false;
        }
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
        function quantityPlus() {
            var maxQuantity = $('#CP_CPSHOP_quantity').attr('max');
            var quantity = parseInt($('#CP_CPSHOP_quantity').val());
            if (quantity < maxQuantity) {
                $('#CP_CPSHOP_quantity').val(quantity + 1);
                LoadVariants();
            }
            else {
                var pop = document.getElementById("divmessage");
                pop.innerHTML = "You can order only maximum " + maxQuantity + " quantity of this product.";
                var header = document.getElementById("exampleModalLabel");
                header.innerHTML = "Maximum " + maxQuantity + " Quantity";
                $('#dvQuantityModal').modal('show');
                //alert("You can order only maximum " + maxQuantity + " quantity of this product.");
            }
        }
        function quantityMinus() {
            var minQuantity = $('#CP_CPSHOP_quantity').attr('min');
            var quantity = parseInt($('#CP_CPSHOP_quantity').val());
            if (quantity > minQuantity) {
                $('#CP_CPSHOP_quantity').val(quantity - 1);
                LoadVariants();
            }
            else {
                var pop = document.getElementById("divmessage");
                pop.innerHTML = "You can order minimum " + minQuantity + " quantity of this product.";
                var header = document.getElementById("exampleModalLabel");
                header.innerHTML = "Minimum " + minQuantity + " Quantity";
                $('#dvQuantityModal').modal('show');
                //alert("You can order minimum " + minQuantity + " quantity of this product.");
            }
        }
        function GetPointsonQuantityChange(quantity) {
            $.ajax({
                type: "POST",
                url: "ProductDetails.aspx/GetPointsonQuantityChange",
                data: "{lintQty:" + quantity + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d != null) {
                        $('#CP_CPSHOP_spanPoints').text(msg.d)
                        $('#CP_CPSHOP_divspanpoints').text(msg.d)
                    }
                }
            });
        }
        function RedirectProduct(productId) {
            var product = productId.substring(2, 8);
            var newUrl = window.location.origin + window.location.pathname + "?ProductId=" + product;
            window.location.href = newUrl;
            return false;
        }
        function changeImage(img) {
            var src = img.src;
            document.getElementById("CP_CPSHOP_imgProductImageMain").src = src;
        }
        function AddItemToCart() {
            $("#CP_CPSHOP_hfCheckoutType").val("normal");
            var lstrProductType = $('#CP_CPSHOP_hfProductType').val();
            var lstrMetasAvailable = $('#CP_CPSHOP_hfUserInputMetasAvailable').val();
            if (lstrProductType == "Digital" && lstrMetasAvailable == "true") {
                //$('.hover_bkgr_fricc').show();
                $('#formpopup').modal('show');
            }
            else {
                var lstrProductId = $('#CP_CPSHOP_hfProductId').val();
                var lintQty = parseInt($('#CP_CPSHOP_quantity').val());
                $.ajax({
                    type: "POST",
                    url: "ProductDetails.aspx/AddItemToCart",
                    data: "{lstrProductId:'" + lstrProductId + "',lintQty:" + lintQty + ",lstrUserInputMetas:''}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        if (msg.d == "QUANTITYERROR") {
                            //alert("Item already added, please update the quantity in the cart.")
                            var pop = document.getElementById("alertmessage");
                            pop.innerHTML = "Item already added, please update the quantity in the cart.";
                            $('#dvAlertModal').modal('show');
                            window.location.href = "Cart.aspx";
                        }
                        else if (msg.d == "SUCCESS") {
                            window.location.href = "Cart.aspx";
                        }
                    }
                });
            }
            return false;
        }
        function Checkout() {
            //debugger;
            $("#CP_CPSHOP_hfCheckoutType").val("exp");
            var lstrProductType = $('#CP_CPSHOP_hfProductType').val();
            var lstrMetasAvailable = $('#CP_CPSHOP_hfUserInputMetasAvailable').val();
            if (lstrProductType == "Digital" && lstrMetasAvailable == "true") {
                //$('.hover_bkgr_fricc').show();
                $('#formpopup').modal('show');
                 
            }
            else {
                var lstrProductId = $('#CP_CPSHOP_hfProductId').val();
                var lintQty = parseInt($('#CP_CPSHOP_quantity').val());
                $.ajax({
                    type: "POST",
                    url: "ProductDetails.aspx/CheckoutGenerateOTP",
                    data: "{lstrProductId:'" + lstrProductId + "',lintQty:" + lintQty + ",lstrUserInputMetas:''}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        var data = msg.d;
                        if (data.includes(".aspx")) {
                            window.location.href = data;
                        }
                        else if (data == "INSUFFICIENT_POINTS") {
                            var pop = document.getElementById("alertmessage");
                            pop.innerHTML = "Insufficient Points.";
                            $('#dvAlertModal').modal('show');
                            //alert("Insufficient points.");
                        }
                        else if (data == "SESSION_TIME_OUT") {
                            var pop = document.getElementById("alertmessage");
                            pop.innerHTML = "Your session time out. Please login again.";
                            $('#dvAlertModal').modal('show');
                            //alert("Your session time out. Please login again.");
                        }
                        else if (data == "Invalid Product") {
                            //alert("Invalid Product")
                            var pop = document.getElementById("alertmessage");
                            pop.innerHTML = "Invalid Product.";
                            $('#dvAlertModal').modal('show');
                        }
                        else {
                            var pop = document.getElementById("alertmessage");
                            pop.innerHTML = "Purchase failed!!! Please try again later.";
                            $('#dvAlertModal').modal('show');
                            //alert("Purchase failed!!! Please try again later.");
                        }
                        return false;
                    },
                    error: function (err) {
                    }
                });
            }
            return false;
        }
        function openCity(evt, cityName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }
        function ShowUserInput() {
            jQuery("#overlay").css('display', 'block');
            jQuery("#popup").css('display', 'block');
            jQuery("#popup").fadeIn(500);
            return false;
        }
        function submitUserInputMetas() {
            $('#abtnContinue').addClass('disabled');
            $("#updProgress").show();
            $('#divErrorMsg').html('');
            var IDs = [];
            $("#divDynamicContent").find("input").each(function () {
                if ($(this).attr("id") != "jsonform-1-elt-fields.operator") {
                    IDs.push(($(this).attr("id")));
                }
            });
            var formelt = $('#divDynamicContent');
            var msg = "";
            IDs.forEach(ValidateFields);
            function ValidateFields(item, index) {
                var data = document.getElementById(item).value;
                if (data.length == 0) {
                    $label = $("label[for='" + item + "']");
                    if ($label.length > 0) {
                        msg += "<span>Please enter " + $label[0].innerHTML + ".</span><br/>";
                    }
                }
                else {
                    if (item.toLowerCase().includes("email")) {
                        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                        if (!filter.test(data)) {
                            msg += "<span>Please enter a valid email address </span><br/>";
                        }
                    }
                    else if (item.toLowerCase().includes("phonenum") || item.toLowerCase().includes("mobile")) {
                        var filter = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
                        if (!filter.test(data)) {
                            msg += "<span>Please enter a valid Phone Number</span><br/>";
                        }
                    }
                    else if (item.toLowerCase().includes("name")) {
                        var filter = /^[a-zA-Z\s]*$/;
                        if (!filter.test(data)) {
                            $label = $("label[for='" + item + "']");
                            if ($label.length > 0) {
                                msg += "<span>Please enter a valid " + $label[0].innerHTML + ".</span><br/>";
                            }
                        }
                    }
                    else if (item.toLowerCase().includes("number")) {
                        var filter = /^[0-9]+$/;
                        if (!filter.test(data)) {
                            $label = $("label[for='" + item + "']");
                            if ($label.length > 0) {
                                msg += "<span>Please enter a valid " + $label[0].innerHTML + ".</span><br/>";
                            }
                        }
                    }
                    else if (item.toLowerCase().includes("msisdn")) {
                        var filter = /^[0-9]+$/;
                        if (data.length < 12) {
                            msg += "<span>Please enter a valid msisdn with country code.</span><br/>";
                        }
                        if (!filter.test(data)) {
                            msg += "<span>Please enter a valid msisdn.</span><br/>";
                        }
                    }
                    else if (item.toLowerCase().includes("jpnumber")) {
                        var filter = /^[0-9]+$/;
                        if (data.length > 30) {
                            msg += "<span>JP Number should be less then 30</span><br/>";
                        }
                        if (!filter.test(data)) {
                            msg += "<span>Please enter a valid JP Number</span><br/>";
                        }
                    }
                    else if (item.toLowerCase().includes("member_id")) {
                        var filter = /^[0-9]+$/;
                        if (data.length > 30) {
                            msg += "<span>Membership Number should be less then 30</span><br/>";
                        }
                        if (!filter.test(data)) {
                            msg += "<span>Please enter a valid Membership Number</span><br/>";
                        }
                    }
                    else if (item.toLowerCase().includes("activecardno")) {
                        var filter = /^UL+\d{9}$/;
                        if (!filter.test(data)) {
                            $label = $("label[for='" + item + "']");
                            msg += "<span>Please enter a valid " + $label[0].innerHTML +".</span><br/>";
                        }
                    }
                    else if (item.toLowerCase().includes("id")) {
                        var filter = /^[0-9]+$/;
                        if (!filter.test(data)) {
                            $label = $("label[for='" + item + "']");
                            msg += "<span>Please enter a valid " + $label[0].innerHTML +".</span><br/>";
                        }
                    }
                }
            }
            if (msg.length > 0) {
                /*$('#abtnContinue').show();*/
                $('#abtnContinue').removeClass('disabled');
                $("#updProgress").hide();
                $("#CP_ErrorMsgContainer").show();
                $("#divErrorMsg")[0].innerHTML = msg;
            }
            else {
                var customerData = JSON.stringify(formelt.jsonFormValue());
                $("#updProgress").show();
                $('#divErrorMsg').html('');
                var lstrProductId = $('#CP_CPSHOP_hfProductId').val();
                var lintQty = parseInt($('#CP_CPSHOP_quantity').val());
                var lstrCheckoutType = $('#CP_CPSHOP_hfCheckoutType').val();
                if (lstrCheckoutType == "normal") {
                    $.ajax({
                        type: "POST",
                        url: "ProductDetails.aspx/AddItemToCart",
                        data: "{lstrProductId:'" + lstrProductId + "',lintQty:" + lintQty + ",lstrUserInputMetas:'" + customerData + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function (msg) {
                            if (msg.d == "QUANTITYERROR") {
                                var pop = document.getElementById("alertmessage");
                                pop.innerHTML = "Item already added, please update the quantity in the cart.";
                                $('#dvAlertModal').modal('show');
                                // alert("Item already added, please update the quantity in the cart.")
                                window.location.href = "Cart.aspx";
                            }
                            else if (msg.d == "SUCCESS") {
                                window.location.href = "Cart.aspx";
                            }
                            else if (msg.d == "Invalid Product") {
                                //alert("Invalid Product")
                                var pop = document.getElementById("alertmessage");
                                pop.innerHTML = "Invalid Product.";
                                $('#dvAlertModal').modal('show');
                            }
                        }
                    });
                }
                else if (lstrCheckoutType == "exp") {
                    $.ajax({
                        type: "POST",
                        url: "ProductDetails.aspx/CheckoutGenerateOTP",
                        data: "{lstrProductId:'" + lstrProductId + "',lintQty:" + lintQty + ",lstrUserInputMetas:'" + customerData + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function (msg) {
                            var data = msg.d;
                            if (data.includes(".aspx")) {
                                window.location.href = data;
                            }
                            else {
                                if (data == "INSUFFICIENT_POINTS") {
                                    $('#divErrorMsg').empty().html("<span>Insufficient Points.</span>");
                                }
                                else if (data == "Invalid Product") {
                                    $('#divErrorMsg').empty().html("<span>Invalid Product.</span>");
                                    //alert("Invalid Product")
                                    //var pop = document.getElementById("alertmessage");
                                    //pop.innerHTML = "Invalid Product.";
                                    //$('#dvAlertModal').modal('show');
                                }
                                else {
                                    IDs.forEach(ClearFields);
                                    $('#divErrorMsg').empty().html("<span>Purchase failed!!! Please try again later.</span>");
                                }
                            }
                        }
                    });
                }
            }
            return false;
        }
        function ColorChange(color, ctrl) {
            $('.btnColorChange').removeClass('selected');
            $(ctrl).addClass('selected');
            $('#CP_CPSHOP_hfColor').val(color);
            LoadVariants();
        }
        function SizeChange(size, ctrl) {
            $('.btnSizeChange').removeClass('selected');
            $(ctrl).addClass('selected');
            $('#CP_CPSHOP_hfSize').val(size);
            LoadVariants();
        }
        function StorageChange(storage, ctrl) {
            $('.btnStorageChange').removeClass('selected');
            $(ctrl).addClass('selected');
            $('#CP_CPSHOP_hfStorage').val(storage);
            LoadVariants();
        }
        function DenominationChange(denomination, ctrl) {
            $('#CP_CPSHOP_quantity').val('1');
            $('.btnDenominationChange').removeClass('selected');
            $(ctrl).addClass('selected');
            $('#CP_CPSHOP_hfValue').val(denomination);
            $('#CP_CPSHOP_quantity').val(1);
            LoadVariants();
        }
        function LoadVariants() {
            $('#CP_CPSHOP_divOutOfStock').hide();
            var lstrProductId = $('#CP_CPSHOP_hfProductId').val();
            var color = $('#CP_CPSHOP_hfColor').val() == undefined ? "" : $('#CP_CPSHOP_hfColor').val();
            var size = $('#CP_CPSHOP_hfSize').val() == undefined ? "" : $('#CP_CPSHOP_hfSize').val();
            var storage = $('#CP_CPSHOP_hfStorage').val() == undefined ? "" : $('#CP_CPSHOP_hfStorage').val();
            var denomination = $('#CP_CPSHOP_hfValue').val() == undefined ? "" : $('#CP_CPSHOP_hfValue').val();
            var quantity = $('#CP_CPSHOP_quantity').val();
            $.ajax({
                type: "POST",
                url: "ProductDetails.aspx/BindProductByVariation",
                data: "{pstrProductId:'" + lstrProductId + "',pstrCOlor:'" + color + "',pstrSize:'" + size + "',pstrStorage:'" + storage + "',pstrDenomination:'" + denomination + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    var data = JSON.parse(msg.d[0]);
                    if (data != '') {
                        $('#CP_CPSHOP_hfProductId').val(data[0].Id);
                        $('#CP_CPSHOP_imgProductImageMain').attr("src", data[0].PrimaryImage.Url);
                        $('#CP_CPSHOP_spanProductName').text(data[0].Name);
                        $('#CP_CPSHOP_spanPoints').text(ConvertThousandSeparator(data[0].Price.SalePrice.Amount * quantity) + " " + "Points");
                        $('#CP_CPSHOP_divspanpoints').text(ConvertThousandSeparator(data[0].Price.SalePrice.Amount * quantity));
                        for (var i = 0; i < data[0].Images.length; i++) {
                            $('#CP_CPSHOP_divThumbnailImages').html('<img src="' + data[0].Images[i].Url + '" class="img-thumbnail" height="100" width="100" onclick="changeImage(this)"/>');
                        }
                        CheckAvailability(data[0].Price.SalePrice.Amount, quantity);
                    }
                    else {
                        $('#CP_CPSHOP_hfProductId').val('');
                        $('#CP_CPSHOP_divOutOfStock').show();
                    }
                },
                error: function (err) {
                }
            });
        }
        function ConvertThousandSeparator(val) {
            return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        }
        function ClearFields(item, index) {
            document.getElementById(item).value = "";
        }

        function CheckAvailability(amount, quantity) {
            $.ajax({
                type: "POST",
                url: "ProductDetails.aspx/CheckAvailability",
                data: "{pntamount:" + amount + ", pntQuantity:" + quantity + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {

                    if (msg.d) {
                        $("#CP_CPSHOP_divBuy").removeAttr("style");
                        $("#CP_CPSHOP_divInsufficient").hide();
                        return true;
                    }
                    else {
                        $("#CP_CPSHOP_divBuy").attr("style", "display:none!Important");
                        $("#CP_CPSHOP_divInsufficient").show();
                        return false;
                    }
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            });
            return false;
        }
        function IsValidProduct(ProductId) {
            var quantity = $('#CP_CPSHOP_quantity').val();
            $.ajax({
                type: "POST",
                url: "ProductDetails.aspx/ProductValiditydetails",
                data: "{pstrProductId:'" + ProductId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d != "") {
                        //debugger
                        var productdetails = $.parseJSON(msg.d);
                        if (productdetails.IsBuyable && productdetails.Price.SalePrice.TruncatedAmount > 0) {
                           // $("#CP_CPSHOP_divBuy").removeAttr("style");
                            $('#CP_CPSHOP_divOutOfStock').hide();
                            CheckAvailability(productdetails.Price.SalePrice.Amount, quantity)
                        }
                        else {
                            $("#CP_CPSHOP_divBuy").attr("style", "display:none!Important");
                            $('#CP_CPSHOP_divOutOfStock').show();
                        }
                    }
                    else {
                        $("#CP_CPSHOP_divBuy").attr("style", "display:none!Important");
                        $('#CP_CPSHOP_divOutOfStock').show();
                    }
                },
                beforeSend: function () {
                    $("#updProgress").show();
                }
            });
            return false;
        }
    </script>
<!-- <script>
  //remove modal from 992px and above
  const filterModal = document.querySelector(".dvFilter");
  window.innerWidth > 991 ? filterModal.classList.remove("modal", "fade") : null;
</script> -->
<script>
    var swiper = new Swiper(".dvThumbSlide", {
        spaceBetween: 10,
        slidesPerView: 4,
        freeMode: true,
        watchSlidesProgress: true,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
    });
    var swiper2 = new Swiper(".dvThumbBannerSlide", {
        spaceBetween:20,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev",
        },
        thumbs: {
            swiper: swiper,
        },
    });
</script>
</asp:Content>

