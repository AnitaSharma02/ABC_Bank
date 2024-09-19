<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link rel="stylesheet" href="\Css/shop.css">
    <!-- Custom styles for this template -->
    <%--<link rel="stylesheet" href="css/checkout-form-validation.css">--%>
    <%--<script src="js/bootstrap.bundle.js"></script>--%>
    <script src="js/checkout-form-validation.js"></script>
    <style>
       .dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner, .dvShopMenu {display:none;}
    </style>
       <div class="dvBreadcrumbs">
    <div class="container-xl">
        <nav>
            <ul class="breadcrumb px-0 py-3">
                <li class="mr-3"><a href="hoteldetails.html"><img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                </li>
                <li class="breadcrumb-item"><a href="\">Home</a></li>
                <li class="breadcrumb-item active"><a href="Shop.aspx?CategoryId=9149a75f-1f53-4ed7-b9b3-260b0fd6d606&ProductType=Physical&type=Shop&Locale=en"> Shop</a></li>
                <li class="breadcrumb-item active">Checkout</li>
            </ul>
        </nav>
    </div>
</div>
    <div class="dvShopCheckout pb-5 mt-lg-4">
        <div class="container-xl">
            <div class="row">
                <div class="col-lg-7">
                    <div class="row">
                        <div class="col-12">
                            <h2 class="h6 heading-bold bg-colour1 text-colour6 bg p-3 b-radius-top-right" data-i18n="shopcheckout-billing">Delivery Address</h2>
                        </div>
                        <div class="col-12">
                            <div id="divCheckoutPhysical" runat="server" class="bg-colour2 p-3">
                                <form class="needs-validation" novalidate>
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <label class="label" for="firstName" data-i18n="flightpassenger-first">First name</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="firstName" placeholder="" value="" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label class="label" for="lastName" data-i18n="flightpassenger-last">Last name</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="lastName" placeholder="" value="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 mb-3">
                                            <label class="label" for="email" data-i18n="flightpassenger-email">Email </label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="email" placeholder="you@example.com" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label class="label" for="email" data-i18n="shopcheckout-validphone">Phone</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="phone" placeholder="9876543210" maxlength="15" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class=" col-md-6 mb-3">
                                            <label class="label" for="address" data-i18n="shopcheckout-address">Address</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="address" placeholder="1234 Main St" />
                                            </div>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label class="label" for="address2" data-i18n="shopcheckout-addressoptional">Address 2 (Optional)</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="address2" placeholder="Apartment or suite" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 mb-3">
                                            <label class="label" for="country" data-i18n="shopcheckout-country">Country</label>
                                            <div class="dvInput input-group">
                                                <select class="form-control custom-select d-block w-100" id="country">
                                                    <option value="">Choose...</option>
                                                    <option value="KE">Kenya</option>
                                                    <option value="QA">Qatar</option>
                                                    <option value="AE">United Arab Emirates</option>
                                                    <option value="AF">Afghanistan</option>
                                                    <option value="AX">Ã…land Islands</option>
                                                    <option value="AL">Albania</option>
                                                    <option value="DZ">Algeria</option>
                                                    <option value="AS">American Samoa</option>
                                                    <option value="AD">Andorra</option>
                                                    <option value="AO">Angola</option>
                                                    <option value="AI">Anguilla</option>
                                                    <option value="AQ">Antarctica</option>
                                                    <option value="AG">Antigua and Barbuda</option>
                                                    <option value="AR">Argentina</option>
                                                    <option value="AM">Armenia</option>
                                                    <option value="AW">Aruba</option>
                                                    <option value="AU">Australia</option>
                                                    <option value="AT">Austria</option>
                                                    <option value="AZ">Azerbaijan</option>
                                                    <option value="BS">Bahamas</option>
                                                    <option value="BH">Bahrain</option>
                                                    <option value="BD">Bangladesh</option>
                                                    <option value="BB">Barbados</option>
                                                    <option value="BY">Belarus</option>
                                                    <option value="BE">Belgium</option>
                                                    <option value="BZ">Belize</option>
                                                    <option value="BJ">Benin</option>
                                                    <option value="BM">Bermuda</option>
                                                    <option value="BT">Bhutan</option>
                                                    <option value="BO">Bolivia (Plurinational State of)</option>
                                                    <option value="BQ">Bonaire, Sint Eustatius and Saba</option>
                                                    <option value="BA">Bosnia and Herzegovina</option>
                                                    <option value="BW">Botswana</option>
                                                    <option value="BV">Bouvet Island</option>
                                                    <option value="BR">Brazil</option>
                                                    <option value="IO">British Indian Ocean Territory</option>
                                                    <option value="BN">Brunei Darussalam</option>
                                                    <option value="BG">Bulgaria</option>
                                                    <option value="BF">Burkina Faso</option>
                                                    <option value="BI">Burundi</option>
                                                    <option value="CI">CÃ´te dIvoire</option>
                                                    <option value="CV">Cabo Verde</option>
                                                    <option value="KH">Cambodia</option>
                                                    <option value="CM">Cameroon</option>
                                                    <option value="CA">Canada</option>
                                                    <option value="KY">Cayman Islands</option>
                                                    <option value="CF">Central African Republic</option>
                                                    <option value="TD">Chad</option>
                                                    <option value="CL">Chile</option>
                                                    <option value="CN">China</option>
                                                    <option value="CX">Christmas Island</option>
                                                    <option value="CC">Cocos (Keeling) Islands</option>
                                                    <option value="CO">Colombia</option>
                                                    <option value="KM">Comoros</option>
                                                    <option value="CG">Congo</option>
                                                    <option value="CD">Congo, Democratic Republic of the</option>
                                                    <option value="CK">Cook Islands</option>
                                                    <option value="CR">Costa Rica</option>
                                                    <option value="HR">Croatia</option>
                                                    <option value="CU">Cuba</option>
                                                    <option value="CW">Curacao</option>
                                                    <option value="CY">Cyprus</option>
                                                    <option value="CZ">Czechia</option>
                                                    <option value="DK">Denmark</option>
                                                    <option value="DJ">Djibouti</option>
                                                    <option value="DM">Dominica</option>
                                                    <option value="DO">Dominican Republic</option>
                                                    <option value="EC">Ecuador</option>
                                                    <option value="EG">Egypt</option>
                                                    <option value="SV">El Salvador</option>
                                                    <option value="GQ">Equatorial Guinea</option>
                                                    <option value="ER">Eritrea</option>
                                                    <option value="EE">Estonia</option>
                                                    <option value="SZ">Eswatini</option>
                                                    <option value="ET">Ethiopia</option>
                                                    <option value="FK">Falkland Islands (Malvinas)</option>
                                                    <option value="FO">Faroe Islands</option>
                                                    <option value="FJ">Fiji</option>
                                                    <option value="FI">Finland</option>
                                                    <option value="FR">France</option>
                                                    <option value="GF">French Guiana</option>
                                                    <option value="PF">French Polynesia</option>
                                                    <option value="TF">French Southern Territories</option>
                                                    <option value="GA">Gabon</option>
                                                    <option value="GM">Gambia</option>
                                                    <option value="GE">Georgia</option>
                                                    <option value="DE">Germany</option>
                                                    <option value="GH">Ghana</option>
                                                    <option value="GI">Gibraltar</option>
                                                    <option value="GR">Greece</option>
                                                    <option value="GL">Greenland</option>
                                                    <option value="GD">Grenada</option>
                                                    <option value="GP">Guadeloupe</option>
                                                    <option value="GU">Guam</option>
                                                    <option value="GT">Guatemala</option>
                                                    <option value="GG">Guernsey</option>
                                                    <option value="GN">Guinea</option>
                                                    <option value="GW">Guinea-Bissau</option>
                                                    <option value="GY">Guyana</option>
                                                    <option value="HT">Haiti</option>
                                                    <option value="HM">Heard Island and McDonald Islands</option>
                                                    <option value="VA">Holy See</option>
                                                    <option value="HN">Honduras</option>
                                                    <option value="HK">Hong Kong</option>
                                                    <option value="HU">Hungary</option>
                                                    <option value="IS">Iceland</option>
                                                    <option value="IN">India</option>
                                                    <option value="ID">Indonesia</option>
                                                    <option value="IR">Iran (Islamic Republic of)</option>
                                                    <option value="IQ">Iraq</option>
                                                    <option value="IE">Ireland</option>
                                                    <option value="IM">Isle of Man</option>
                                                    <option value="IL">Israel</option>
                                                    <option value="IT">Italy</option>
                                                    <option value="JM">Jamaica</option>
                                                    <option value="JP">Japan</option>
                                                    <option value="JE">Jersey</option>
                                                    <option value="JO">Jordan</option>
                                                    <option value="KZ">Kazakhstan</option>
                                                    <option value="KI">Kiribati</option>
                                                    <option value="KP">Korea (Democratic Peoples Republic of)</option>
                                                    <option value="KR">Korea, Republic of</option>
                                                    <option value="KW">Kuwait</option>
                                                    <option value="KG">Kyrgyzstan</option>
                                                    <option value="LA">Lao Peoples Democratic Republic</option>
                                                    <option value="LV">Latvia</option>
                                                    <option value="LB">Lebanon</option>
                                                    <option value="LS">Lesotho</option>
                                                    <option value="LR">Liberia</option>
                                                    <option value="LY">Libya</option>
                                                    <option value="LI">Liechtenstein</option>
                                                    <option value="LT">Lithuania</option>
                                                    <option value="LU">Luxembourg</option>
                                                    <option value="MO">Macao</option>
                                                    <option value="MG">Madagascar</option>
                                                    <option value="MW">Malawi</option>
                                                    <option value="MY">Malaysia</option>
                                                    <option value="MV">Maldives</option>
                                                    <option value="ML">Mali</option>
                                                    <option value="MT">Malta</option>
                                                    <option value="MH">Marshall Islands</option>
                                                    <option value="MQ">Martinique</option>
                                                    <option value="MR">Mauritania</option>
                                                    <option value="MU">Mauritius</option>
                                                    <option value="YT">Mayotte</option>
                                                    <option value="MX">Mexico</option>
                                                    <option value="FM">Micronesia (Federated States of)</option>
                                                    <option value="MD">Moldova, Republic of</option>
                                                    <option value="MC">Monaco</option>
                                                    <option value="MN">Mongolia</option>
                                                    <option value="ME">Montenegro</option>
                                                    <option value="MS">Montserrat</option>
                                                    <option value="MA">Morocco</option>
                                                    <option value="MZ">Mozambique</option>
                                                    <option value="MM">Myanmar</option>
                                                    <option value="NA">Namibia</option>
                                                    <option value="NR">Nauru</option>
                                                    <option value="NP">Nepal</option>
                                                    <option value="NL">Netherlands</option>
                                                    <option value="NC">New Caledonia</option>
                                                    <option value="NZ">New Zealand</option>
                                                    <option value="NI">Nicaragua</option>
                                                    <option value="NE">Niger</option>
                                                    <option value="NG">Nigeria</option>
                                                    <option value="NU">Niue</option>
                                                    <option value="NF">Norfolk Island</option>
                                                    <option value="MK">North Macedonia</option>
                                                    <option value="MP">Northern Mariana Islands</option>
                                                    <option value="NO">Norway</option>
                                                    <option value="OM">Oman</option>
                                                    <option value="PK">Pakistan</option>
                                                    <option value="PW">Palau</option>
                                                    <option value="PS">Palestine, State of</option>
                                                    <option value="PA">Panama</option>
                                                    <option value="PG">Papua New Guinea</option>
                                                    <option value="PY">Paraguay</option>
                                                    <option value="PE">Peru</option>
                                                    <option value="PH">Philippines</option>
                                                    <option value="PN">Pitcairn</option>
                                                    <option value="PL">Poland</option>
                                                    <option value="PT">Portugal</option>
                                                    <option value="PR">Puerto Rico</option>
                                                    <option value="RE">Reunion</option>
                                                    <option value="RO">Romania</option>
                                                    <option value="RU">Russian Federation</option>
                                                    <option value="RW">Rwanda</option>
                                                    <option value="BL">Saint BarthÃ©lemy</option>
                                                    <option value="SH">Saint Helena, Ascension and Tristan da Cunha</option>
                                                    <option value="KN">Saint Kitts and Nevis</option>
                                                    <option value="LC">Saint Lucia</option>
                                                    <option value="MF">Saint Martin (French part)</option>
                                                    <option value="PM">Saint Pierre and Miquelon</option>
                                                    <option value="VC">Saint Vincent and the Grenadines</option>
                                                    <option value="WS">Samoa</option>
                                                    <option value="SM">San Marino</option>
                                                    <option value="ST">Sao Tome and Principe</option>
                                                    <option value="SA">Saudi Arabia</option>
                                                    <option value="SN">Senegal</option>
                                                    <option value="RS">Serbia</option>
                                                    <option value="SC">Seychelles</option>
                                                    <option value="SL">Sierra Leone</option>
                                                    <option value="SG">Singapore</option>
                                                    <option value="SX">Sint Maarten (Dutch part)</option>
                                                    <option value="SK">Slovakia</option>
                                                    <option value="SI">Slovenia</option>
                                                    <option value="SB">Solomon Islands</option>
                                                    <option value="SO">Somalia</option>
                                                    <option value="ZA">South Africa</option>
                                                    <option value="GS">South Georgia and the South Sandwich Islands</option>
                                                    <option value="SS">South Sudan</option>
                                                    <option value="ES">Spain</option>
                                                    <option value="LK">Sri Lanka</option>
                                                    <option value="SD">Sudan</option>
                                                    <option value="SR">Suriname</option>
                                                    <option value="SJ">Svalbard and Jan Mayen</option>
                                                    <option value="SE">Sweden</option>
                                                    <option value="CH">Switzerland</option>
                                                    <option value="SY">Syrian Arab Republic</option>
                                                    <option value="TW">Taiwan, Province of China</option>
                                                    <option value="TJ">Tajikistan</option>
                                                    <option value="TZ">Tanzania, United Republic of</option>
                                                    <option value="TH">Thailand</option>
                                                    <option value="TL">Timor-Leste</option>
                                                    <option value="TG">Togo</option>
                                                    <option value="TK">Tokelau</option>
                                                    <option value="TO">Tonga</option>
                                                    <option value="TT">Trinidad and Tobago</option>
                                                    <option value="TN">Tunisia</option>
                                                    <option value="TR">Turkey</option>
                                                    <option value="TM">Turkmenistan</option>
                                                    <option value="TC">Turks and Caicos Islands</option>
                                                    <option value="TV">Tuvalu</option>
                                                    <option value="UG">Uganda</option>
                                                    <option value="UA">Ukraine</option>
                                                    <option value="GB">United Kingdom of Great Britain and Northern Ireland</option>
                                                    <option value="UM">United States Minor Outlying Islands</option>
                                                    <option value="US">United States of America</option>
                                                    <option value="UY">Uruguay</option>
                                                    <option value="UZ">Uzbekistan</option>
                                                    <option value="VU">Vanuatu</option>
                                                    <option value="VE">Venezuela (Bolivarian Republic of)</option>
                                                    <option value="VN">Viet Nam</option>
                                                    <option value="VG">Virgin Islands (British)</option>
                                                    <option value="VI">Virgin Islands (U.S.)</option>
                                                    <option value="WF">Wallis and Futuna</option>
                                                    <option value="EH">Western Sahara</option>
                                                    <option value="YE">Yemen</option>
                                                    <option value="ZM">Zambia</option>
                                                    <option value="ZW">Zimbabwe</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-md-4 mb-3">
                                            <label class="label" for="state" data-i18n="shopcheckout-city">City</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="city" placeholder="" />
                                            </div>
                                        </div>
                                        <div class="col-md-4 mb-3">
                                            <label class="label" for="zip" data-i18n="shopcheckout-zip">Zip</label>
                                            <div class="dvInput input-group">
                                                <input type="text" class="form-control" id="zip" placeholder="" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="searchBtn col-12 mt-2">
                                            <button class="btn btn-one" onclick="var varReturn = Checkout(); event.returnValue = varReturn; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return varReturn;">Continue to checkout</button>
                                        </div>
                                    </div>
                                </form>
                                <div id="ChangePasswordValidation" class="h7 heading-semibold text-colour1 py-2"></div>
                                <div id="CP_ErrorMsgContainer">
                                    <asp:Label runat="server" ID="lblSuccessOrFailure" />
                                </div>
                            </div>
                            <div id="divCheckoutDigital" runat="server">
                                <button class="btn btn-one" onclick="var varReturn = CheckoutDigital(); event.returnValue = varReturn; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return varReturn;">Continue to checkout</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 mt-3 mt-lg-0">
                    <div id="divCartContents">
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            LoadCart();
            $("#phone").keydown(onlyNumeric);
        });
        function LoadCart() {
            $.ajax({
                type: "POST",
                url: "Checkout.aspx/LoadCart",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d != "") {
                        $('#divCartContents').html("");
                        $('#divCartContents').html(msg.d);
                    } else {
                        window.location.href = "Shop.aspx";
                    }
                    return false;
                }
            });
            return false;
        }

        /* below Checkout Function modified by amey */
        function Checkout() {
            var msg = "";
            var firstName = $("#firstName").val();
            var lastName = $("#lastName").val();
            var email = $("#email").val();
            var phone = $("#phone").val();
            var address = $("#address").val();
            var address2 = $("#address2").val();
            var city = $("#city").val();
            var zip = $("#zip").val();
            var countrycode = $("#country option:selected").val();
            var country = $("#country option:selected").text();

            // Clear previous error messages
            $(".danger").remove();

            if (firstName == "") {
                msg += "<span class='danger' data-i18n='text-enter-first'>Please enter First Name</span>";
                $("#firstName").closest(".dvInput").after(msg);
                msg = "";
            } else if (!firstName.match(/^[a-zA-Z]+$/)) {
                msg += "<span class='danger' data-i18n='hotel-booking-errorvalidfname'>Please enter valid First Name</span>";
                $("#firstName").closest(".dvInput").after(msg);
                msg = "";
            }

            if (lastName == "") {
                msg += "<span class='danger' data-i18n='text-enter-last'>Please enter Last Name</span>";
                $("#lastName").closest(".dvInput").after(msg);
                msg = "";
            } else if (!lastName.match(/^[a-zA-Z]+$/)) {
                msg += "<span class='danger' data-i18n='hotel-booking-errorvalidlname'>Please enter valid Last Name</span>";
                $("#lastName").closest(".dvInput").after(msg);
                msg = "";
            }

            if (email == "") {
                msg += "<span class='danger' data-i18n='text-please-enter-Email'>Please enter Email Id</span>";
                $("#email").closest(".dvInput").after(msg);
                msg = "";
            } else if (!isEmail(email)) {
                msg += "<span class='danger' data-i18n='text-please-enter-valid-nationalid'>Please enter valid Email Id</span>";
                $("#email").closest(".dvInput").after(msg);
                msg = "";
            }

            if (phone == "") {
                msg += "<span class='danger' data-i18n='text-please-enter-phoneno'>Please enter Phone Number</span>";
                $("#phone").closest(".dvInput").after(msg);
                msg = "";
            } else {
                var numbers = /^[0-9]+$/;
                if (!numbers.test(phone)) {
                    msg += "<span class='danger' data-i18n='text-please-enter-numeric'>Phone should be Numeric</span>";
                    $("#phone").closest(".dvInput").after(msg);
                    msg = "";
                }
            }

            if (address == "") {
                msg += "<span class='danger' data-i18n='text-please-enter-address'>Please enter Address</span>";
                $("#address").closest(".dvInput").after(msg);
                msg = "";
            }

            if (countrycode == "") {
                msg += "<span class='danger' data-i18n='text-please-enter-selectcountry'>Please select Country</span>";
                $("#country").closest(".dvInput").after(msg);
                msg = "";
            }

            if (city == "") {
                msg += "<span class='danger' data-i18n='text-please-enter-city'>Please enter City</span>";
                $("#city").closest(".dvInput").after(msg);
                msg = "";
            } else {
                var regex = new RegExp("^[a-zA-Z ]+$");
                if (!regex.test(city)) {
                    msg += "<span class='danger' data-i18n='text-please-enter-validcity'>Please enter valid City</span>";
                    $("#city").closest(".dvInput").after(msg);
                    msg = "";
                }
            }

            if (zip == "") {
                msg += "<span class='danger' data-i18n='text-please-enter-zip'>Please enter ZIP</span>";
                $("#zip").closest(".dvInput").after(msg);
                msg = "";
            } else {
                var numbers = /^[0-9]+$/;
                if (!numbers.test(zip)) {
                    msg += "<span class='danger' data-i18n='text-please-enter-zipnumeric'>Zip should be Numeric</span>";
                    $("#zip").closest(".dvInput").after(msg);
                    msg = "";
                }
            }

            if ($(".danger").length > 0) {
                $('#CP_ErrorMsgContainer').show();
                return false;
            } else {
                $.ajax({
                    type: "POST",
                    url: "Checkout.aspx/Proceed",
                    data: JSON.stringify({
                        firstName: firstName,
                        lastName: lastName,
                        email: email,
                        phone: phone,
                        address: address,
                        address2: address2,
                        city: city,
                        country: country,
                        countrycode: countrycode,
                        zip: zip
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        var data = msg.d;
                        if (data.includes(".aspx")) {
                            window.location.href = data;
                        } else if (data == "INSUFFICIENT_POINTS") {
                            $('#divCartContents').empty().html("Insufficient Points.");
                        } else {
                            $('#divCartContents').empty().html("Purchase failed!!! Please try again later.");
                        }
                        return false;
                    }
                });
            }
        }

        /* backup previous Checkout Function */
        /*function Checkout() {
            var msg = "";
            var firstName = $("#firstName").val();
            var lastName = $("#lastName").val();
            var email = $("#email").val();
            var phone = $("#phone").val();
            var address = $("#address").val();
            var address2 = $("#address2").val();
            var city = $("#city").val();
            var zip = $("#zip").val();
            var countrycode = $("#country option:selected").val();
            var country = $("#country option:selected").text();
            if (firstName == "") {
                msg += ("<span data-i18n='text-enter-first'>Please enter First Name</span><br />");
            } else if (!firstName.match(/^[a-zA-Z]+$/)) {
                msg += ("<span data-i18n='hotel-booking-errorvalidfname'>Please enter valid First Name</span><br />");
            }
            if (lastName == "") {
                msg += ("<span data-i18n='text-enter-last'>Please enter Last Name</span><br />");
            } else if (!lastName.match(/^[a-zA-Z]+$/)) {
                msg += ("<span data-i18n='hotel-booking-errorvalidlname'>Please enter valid Last Name</span><br />");
            }
            if ($('#email').val() == "") {
                msg += ("<span data-i18n='text-please-enter-Email'>Please enter Email Id</span><br />");
            }
            else if (!isEmail($('#email').val())) {
                msg += ("<span data-i18n='text-please-enter-valid-nationalid'>Please enter valid Email Id</span><br />");
            }
            if ($('#phone').val() == "") {
                msg += ("<span data-i18n='text-please-enter-phoneno'>Please enter Phone Number</span><br />");
            }
            else {
                var numbers = /^[0-9]+$/;
                if (!numbers.test($('#phone').val())) {
                    msg += ("<span data-i18n='text-please-enter-numeric'>Phone should be Numeric</span><br />");
                }
            }
            if ($('#address').val() == "") {
                msg += ("<span data-i18n='text-please-enter-address'>Please enter Address</span><br />");
            }
            if (countrycode == "") {
                msg += ("<span data-i18n='text-please-enter-selectcountry'>Please select Country</span><br />");
            }
            if (city == "") {
                msg += ("<span data-i18n='text-please-enter-city'>Please enter City</span><br />");
            }
            else {
                var regex = new RegExp("^[a-zA-Z ]+$");
                if (!regex.test(city)) {
                    msg += ("<span data-i18n='text-please-enter-validcity'>Please enter valid City</span><br />");
                }
            }
            if (zip == "") {
                msg += ("<span data-i18n='text-please-enter-zip'>Please enter ZIP</span><br />");
            }
            else {
                var numbers = /^[0-9]+$/;
                if (!numbers.test(zip)) {
                    msg += ("<span data-i18n='text-please-enter-zipnumeric'>Zip should be Numeric</span><br />");
                }
            }
            if (msg.length > 0) {

                $("#CP_lblSuccessOrFailure").html('');
                $("#ChangePasswordValidation")[0].innerHTML = msg;
                $('#CP_ErrorMsgContainer').show();
                return false;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "Checkout.aspx/Proceed",
                    data: "{ 'firstName': '" + firstName + "', 'lastName': '" + lastName + "', 'email': '" + email + "', 'phone': '" + phone + "', 'address': '" + address + "', 'address2': '" + address2 + "', 'city': '" + city + "', 'country': '" + country + "', 'countrycode': '" + countrycode + "', 'zip': '" + zip + "' }",
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
                            $('#divCartContents').empty().html("Insufficient Points.");
                        }
                        else {
                            $('#divCartContents').empty().html("Purchase failed!!! Please try again later.");
                        }
                        return false;
                    }
                });
            }
        }*/

        function CheckoutDigital() {
            $.ajax({
                type: "POST",
                url: "Checkout.aspx/ProceedDigital",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (msg) {
                    if (msg.d) {
                        window.location.href = "OrderStatus.aspx?Status=true";
                    }
                    else {
                        window.location.href = "OrderStatus.aspx?Status=false";
                    }
                    return false;
                }
            });
        }
        function isEmail(email) {
            var regex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
            return regex.test(email);
        }
        function onlyNumeric(e) {
            var key = e.charCode || e.keyCode || 0;
            return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        }
    </script>
</asp:Content>