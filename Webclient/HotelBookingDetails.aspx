<%@ Page Title="Hotel Booking Details" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="HotelBookingDetails.aspx.cs" Inherits="HotelBookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <link href="Css/hotel.css" rel="stylesheet" type="text/css" />
    <style>
         #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner  {display:none;}
    </style>
   <div class="dvBreadcrumbs">
        <div class="container-lg">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3"><a href="\"><img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item"><a href="HotelDetails.aspx"> Hotel Details</a></li>
                    <li class="breadcrumb-item">Hotel Booking Details</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvHotelBookingDetails pb-5 mt-lg-4">
        <div class="container-lg">
            <div class="row">
                <div class="col-12">
                    <div id="divError" class="ErrorMsgContainer" runat="server">
                        <asp:Label ID="lblError" CssClass="errorMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="row">
                        <div class="col-12">
                            <h2 class="h6 heading-semibold text-colour6 bg p-3">Personal Details</h2>
                        </div>
                        <div class="col-12">
                            <div style="display: none">
                                <asp:TextBox ID="txtState" runat="server" CssClass="textBoxHotelBooking" Style="height: 1.7vw; width: 52%"></asp:TextBox>
                                <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="textbox TxtBox_Width85"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="bg-lightgray p-3 mb-3">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">Title.</label>
           <div class="select">
             <div class="selectBtn form-control" data-type="firstOption">Select</div>
             <div class="selectDropdown">
               <div class="option" data-type="firstOption">Mr.</div>
               <div class="option" data-type="secondOption">Mrs.</div>
               <div class="option" data-type="thirdOption">Mrs.</div>
             </div>
           </div>--%>
                                        <label class="label">Title*</label>
                                        <div class="dvInput input-group">
                                            <asp:DropDownList ID="ddlPersonalTitle" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Mr." Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Ms." Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Mrs." Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">First Name</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="First Name"
             />
           </div>--%>
                                        <label class="label">First Name*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtFirstName" Style=""
                                            CssClass="h8 heading-regular text-danger" ID="RequiredFieldValidator1"
                                            runat="server" ErrorMessage="Enter First Name" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator runat="server" ID="cusCustom" CssClass="h8 heading-regular text-danger d-none"
                                            ControlToValidate="txtFirstName" OnServerValidate="custom_NameValidate" ErrorMessage="Enter only alpha" />
                                        <asp:RegularExpressionValidator ID="revAdultFirstName" Display="Dynamic" runat="server"
                                            ControlToValidate="txtFirstName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z'.\s]{1,50}"
                                            CssClass="h8 heading-regular text-danger" ErrorMessage="Please Enter valid First Name."></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">Last Name</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="Last Name"
             />--%>
                                        <label class="label">Last Name*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtLastname" CssClass="h8 heading-regular text-danger"
                                            ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Last Name" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator runat="server" ID="CustomValidatorLastName" CssClass="h8 heading-regular text-danger d-none"
                                            ControlToValidate="txtLastname" OnServerValidate="custom_NameValidate" ErrorMessage="Enter only alpha" />
                                        <asp:RegularExpressionValidator ID="revtxtLastname" Display="Dynamic" runat="server"
                                            ControlToValidate="txtLastname" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z'.\s]{1,50}"
                                            CssClass="h8 heading-regular text-danger" ErrorMessage="Please Enter valid Last Name."></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">City</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="City"
             />--%>
                                        <label class="label">City*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtCity" CssClass="h8 heading-regular text-danger"
                                            ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter City" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCity" Display="Dynamic" runat="server"
                                            ControlToValidate="txtCity" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z'.\s]{1,50}"
                                            CssClass="h8 heading-regular text-danger" ErrorMessage="Please Enter valid City."></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">Country</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="Country"
             />--%>
                                        <label class="label">Country*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtCountry" CssClass="h8 heading-regular text-danger"
                                            ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Country" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revtxtCountry" Display="Dynamic" runat="server"
                                            ControlToValidate="txtCountry" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z'.\s]{1,50}"
                                            CssClass="h8 heading-regular text-danger" ErrorMessage="Please Enter valid Country."></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-md-6 d-none mb-3">
                                        <%--<label class="label">Postal Code</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="Postal Code"
             />--%>
                                        <label class="label">State</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-6 d-none mb-3">
                                        <%--<label class="label">Mobile (without country code)</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="Mobile without country code"
             />--%>
                                        <label class="label">Phone Number</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">Email Id</label>
           <div class="input-group mb-3">
             <input
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="Email Id"
             />--%>
                                        <label class="label txt-postal-code">Postal Code*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtPostalCode" CssClass="h8 heading-regular text-danger"
                                            ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Postal Code"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter Only Digits"
                                            Display="Dynamic" ControlToValidate="txtPostalCode" CssClass="h8 heading-regular text-danger" ValidationExpression="^[0-9]+$">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <%--<label class="label">Special Address</label>
           <div class="input-group mb-3">
             <textarea
               autocomplete="off"
               id="datepicker2"
               type="text"
               class="form-control"
               placeholder="Special Address"
             ></textarea>--%>
                                        <label class="label">Mobile No (without country code)*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtMobileNo" CssClass="h8 heading-regular text-danger"
                                            ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Mobile No" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Only Digits"
                                             Display="Dynamic" ControlToValidate="txtMobileNo" CssClass="h8 heading-regular text-danger" ValidationExpression="^[0-9]+$"> </asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="label">E-Mail Address*</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="txtEmailID" ID="RequiredFieldValidator9"
                                            CssClass="h8 heading-regular text-danger" runat="server" ErrorMessage="Enter Email" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Email Address not valid"
                                            Display="Dynamic" CssClass="h8 heading-regular text-danger" ControlToValidate="txtEmailID" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"></asp:RegularExpressionValidator>

                                    </div>
                                    <div class="col-12 mb-3">
                                        <label class="label">Special Request</label>
                                        <div class="dvInput input-group">
                                            <asp:TextBox ID="txtSpecialRequest" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 mb-3">
                                        <div class="dvLabel">
                                            <label class="checkbox-container d-flex">
                                                <span class="d-inline-block">
                                                    <asp:CheckBox ID="chkAcceptAgreements" runat="server" />
                                                    <span class="checkmark"></span>
                                                </span>
                                                <span class="d-inline-block ml-2">I have read and agree to NIC Asia <a href="TermsandConditions.aspx" target="_blank">Terms & Conditions</a> and the <a href="BookingPolicy.aspx" target="_blank">Booking & Cancellation Policy</a> of the respective service provider.</span>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-12 mb-2">
                                        <div class="dvLabel">
                                       <label class="checkbox-container d-flex">
                                           <span class="d-inline-block">
                                               <asp:CheckBox ID="chkCancellationPolicy" runat="server" />
                                               <span class="checkmark"></span>
                                           </span>
                                           <span class="d-inline-block ml-2">I agree to redeem
                                               <asp:Label ID="lblTotalCharge" runat="server" Text=""></asp:Label>. I also understand and accept that.the redeemed NPoints cannot be refunded or credited upon cancellation of a hotel booking.</span>
                                       </label>
                                        </div>
                                    </div>
                                    <div class="col-12 mb-2">
                                        <div id="ErrorMsgContainer" class="h8 heading-regular text-danger" runat="server">
                                            <div id="LoginValidation"></div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="" id="Bookbtn" runat="server">
                                            <label class="">
                                                <asp:Button ID="btnBook" runat="server" OnClientClick="return onAcceptArgument();" OnClick="btnBook_Click" class="btn btn-one" Text="BOOK NOW"></asp:Button>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="row">
                        <div class="col-12">
                            <div class="bg d-flex justify-content-between align-items-center">
                                <h2 class="h6 heading-semibold text-colour6 bg p-3">Hotel Details</h2>
                                <a href="HotelResults.aspx?edit=1" class="btn btn-two mr-2">Edit</a>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="bg-lightgray p-3">
                                <div class="row">
                                    <div class="col-12">
                                        <h2 class="h5 heading-regular text-colour7 mb-1"><asp:Label runat="server" class="h6 heading-semibold text-colour7" ID="lblHotelName"></asp:Label></h2>
                                        <p class="h7 heading-regular text-colour7">
                                            <asp:Label class="" ID="lblAddress" runat="server"></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="border my-3"></div>
                                <div class="row align-items-center justify-content-lg-between">
                                    <div class="col-6 offset-3 offset-sm-0 col-sm-2 mb-3 mb-sm-0 pr-lg-1">
                                        <div class="img-container">
                                            <asp:Image ID="imgHotel" runat="server" />
                                        </div>
                                    </div>
                                    <div
                                        class="col-4 col-sm-3 col-lg-3 d-flex flex-column align-items-center justify-content-center text-center px-lg-1">
                                        <img src="Images/icons/other/time.png" alt="" />
                                        <div class="mt-lg-1">
                                            <p class="h7 heading-regular text-colour7">Check-in</p>
                                            <p class="h7 heading-regular text-colour7"><asp:Label runat="server" ID="lblCheckinDate"></asp:Label></p>
                                        </div>
                                    </div>
                                    <div
                                        class="col-4 col-sm-3 col-lg-3 col-xl-4 d-flex align-items-center flex-column text-center px-lg-1">
                                        <img src="Images/icons/other/time.png" alt="" />
                                        <div class="mt-lg-1">
                                            <p class="h7 heading-regular text-colour7"><asp:Label ID="lblNoofNights" runat="server"></asp:Label></p>
                                            <p class="h7 heading-regular text-colour7"><asp:Label runat="server" ID="lblNoOfAdult"></asp:Label></p>
                                        </div>
                                    </div>
                                    <div
                                        class="col-4 col-sm-3 col-lg-3 d-flex align-items-center flex-column text-center pl-lg-1">
                                        <img src="Images/icons/other/time.png" alt="" />
                                        <div class="mt-lg-1">
                                            <p class="h7 heading-regular text-colour7">Check-out</p>
                                            <p class="h7 heading-regular text-colour7"><asp:Label runat="server" ID="lblCheckoutDate"></asp:Label></p>
                                        </div>
                                    </div>
                                </div>
                                <div class="border my-3"></div>
                                <div class="row">
                                    <div class="col-12 d-flex justify-content-between">
                                        <span class="h6 heading-semibold text-colour7">
                                            Total NPoints 
                                        </span>
                                        <span class="h6 heading-semibold text-colour7">
                                            <asp:Label runat="server" ID="lblTotalMiles"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script type="text/javascript" language="javascript">
        function onAcceptArgument() {
            var msg = "";
            $("#CP_ErrorMsgContainer").hide();
            if (($("#CP_chkAcceptAgreements").prop("checked") == true) && ($("#CP_chkCancellationPolicy").prop("checked") == true)) {
                return true;
            }
            if ($("#CP_chkCancellationPolicy").prop("checked") == false) {
                msg += "Accept cancellation policy<br/>";
            }
            if ($("#CP_chkAcceptAgreements").prop("checked") == false) {
                msg += "Accept Terms And Conditions<br/>";
            }
            if (msg.length > 0) {
                $("#LoginValidation")[0].innerHTML = msg;
                $("#CP_ErrorMsgContainer").show();
                return false;
            }
            e.preventDefault();
        }
    </script>
</asp:Content>
