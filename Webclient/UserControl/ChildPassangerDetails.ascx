<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChildPassangerDetails.ascx.cs"
    Inherits="SourceControl_ChildPassangerDetails" %>

<script src="../Jquery/UserControlValidation.js" type="text/javascript"></script>
<script type="text/javascript">
    //code for ChildRepeater Date
    $(document).ready(function () {

        $("#<%=txtDOB.ClientID%>").click(function () {
            $("#<%=txtDOB.ClientID%>").datepicker('show');
        });
        $("#<%=txtExpiryDate.ClientID%>").click(function () {
            $("#<%=txtExpiryDate.ClientID%>").datepicker('show');
        });
        $("#<%=txtEffectiveDate.ClientID%>").click(function () {
            $("#<%=txtEffectiveDate.ClientID%>").datepicker('show');
        });
        //show datepicker onclick of icon
        $(".dvTxtDOBChild .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtDOB.ClientID%>").datepicker("show");
        });
        $(".dvTxtExpiryDateChild .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtExpiryDate.ClientID%>").datepicker("show");
        });
        $(".dvTxtEffectiveDateChild .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtEffectiveDate.ClientID%>").datepicker("show");
        });
        $("#<%=txtDOB.ClientID%>").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            changeYear: true,
            yearRange: "-12:+0",
          //  showButtonPanel: true,
            dateFormat: 'dd/mm/yy',
            maxDate: new Date,
            minDate: '-12Y',
            onSelect: function (dateText, inst) {
                $("#<%=txtDOB.ClientID%>").text("");
                $("#<%=txtDOB.ClientID%>").text(dateText);
                $("#<%=txtDOB.ClientID%>").val(dateText.toString());

                return false;
            }
        });

        $("#<%=txtEffectiveDate.ClientID%>").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            yearRange: "-90:-0",
            dateFormat: 'dd/mm/yy',
            maxDate: new Date,
            onSelect: function (dateText, inst) {
                $("#<%=txtEffectiveDate.ClientID%>").text("");
                $("#<%=txtEffectiveDate.ClientID%>").text(dateText);
                $("#<%=txtEffectiveDate.ClientID%>").val(dateText.toString());
                return false;
            }
        });

        $("#<%=txtExpiryDate.ClientID%>").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            yearRange: ":+20",
            dateFormat: 'dd/mm/yy',
            minDate: new Date,
            onSelect: function (dateText, inst) {
                $("#<%=txtExpiryDate.ClientID%>").text("");
                $("#<%=txtExpiryDate.ClientID%>").text(dateText);
                $("#<%=txtExpiryDate.ClientID%>").val(dateText.toString());
                return false;
            }
        });
    });   
</script>



<div class="row">
    <div class="col-md-6 mb-3">
        <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-title">Title</label>
        <div class="select_box">
            <asp:DropDownList ID="ddlTitle" CssClass="form-control" runat="server" Style="width: 100%;">
                <asp:ListItem Value="1" Selected="True">Title</asp:ListItem>
                <asp:ListItem Value="Male">Mstr</asp:ListItem>
                <asp:ListItem Value="Female">Miss</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle"
           Display="Dynamic" ErrorMessage="Enter Title" CssClass="rptErrorMassage h7 heading-regular text-danger" ValidationGroup="WebValidation"
            InitialValue="1" data-i18n="flightpassenger-error-title"></asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 mb-3">
        <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-first">First Name</label>
        <div class="input-group ">
            <asp:TextBox ID="txtFirstName" MaxLength="27" CssClass="form-control" runat="server" Text="" AutoComplete="off"></asp:TextBox>
             </div>
            <asp:RequiredFieldValidator ID="rfvAdultFirstName" runat="server" ControlToValidate="txtFirstName"
                ErrorMessage="Enter First Name" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger" Display="Dynamic" data-i18n="flightpassenger-error-enter-firstname"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revAdultFirstName" Display="Dynamic" runat="server"
                ControlToValidate="txtFirstName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
                CssClass="rptErrorMassage h7 heading-regular text-danger" ErrorMessage="Please Enter valid First Name." data-i18n="flightpassenger-error-enter-correctfirstname"></asp:RegularExpressionValidator>
         
    </div>
    <div class="col-md-6 mb-3">
        <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-last">Last Name</label>
        <div class="input-group">
            <asp:TextBox ID="txtLastName" MaxLength="27" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
             </div>
            <asp:RequiredFieldValidator ID="rfvAdultLastName" runat="server" ControlToValidate="txtLastName"
                 ErrorMessage="Enter Last Name" data-i18n="flightpassenger-error-enter-lastname" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revAdultLastName" Display="Dynamic" runat="server"
                ControlToValidate="txtLastName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
                CssClass="rptErrorMassage h7 heading-regular text-danger" data-i18n="flightpassenger-error-enter-correctlastname" ErrorMessage="Please Enter valid Last Name."></asp:RegularExpressionValidator>
       
    </div>
    <div class="col-md-6 mb-3">
        <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-date">Date of Birth</label>
        <div class="dvTxtDOBChild input-group">
            <asp:TextBox ID="txtDOB" class="form-control icnDate" runat="server" AutoComplete="off"></asp:TextBox>
            <div class="input-group-append">
                <span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span>
            </div>
            </div>
            <asp:RequiredFieldValidator ID="rfvAdultDOB" runat="server" ControlToValidate="txtDOB"
                Display="Dynamic" ErrorMessage="Enter Date of Birth" data-i18n="flightpassenger-error-enter-date" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="customChildDOBValidator" runat="server" ErrorMessage="Child  (2-12 yrs)"
                Display="Dynamic" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger" data-i18n="flightpassenger-error-enter-adults"
                OnServerValidate="IssueChildDOBValidator" ControlToValidate="txtDOB"></asp:CustomValidator> 
   </div>
</div>
<div class="row">
    <div class="col-md-6 mb-3">
        <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-passport">Passport Number</label>
        <div class="input-group">
            <asp:TextBox ID="txtPassportNo" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassportNo"
                Display="Dynamic" data-i18n="flightpassenger-error-passport" ErrorMessage="Enter Passport Number" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassportNo"
                Display="Dynamic" CssClass="rptErrorMassage h7 heading-regular text-danger" ValidationGroup="WebValidation"
                ValidationExpression="^[a-zA-Z0-9]*$" data-i18n="flightpassenger-error-correct-passport" ErrorMessage="Please Enter correct passport no. (no blank space)."></asp:RegularExpressionValidator>
    </div>
    <div class="col-md-6 mb-3">
        <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-email">E-Mail ID</label>
        <div class="input-group">
            <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailID"
                Display="Dynamic" data-i18n="flightpassenger-error-emailid" ErrorMessage="Enter E-mail Id" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage h7 heading-regular text-danger"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revAdultEmail" Display="Dynamic" runat="server"
                ControlToValidate="txtEmailID" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                CssClass="rptErrorMassage h7 heading-regular text-danger" ValidationGroup="WebValidation" data-i18n="flightpassenger-error-valid-emailid" ErrorMessage="Enter Valid E-mail Id">
            </asp:RegularExpressionValidator> 
    </div>
</div>
<div id="AdditionalInfo" runat="server">
    <div class="row">
        <div class="col-md-6 mb-3" id="divNationality" runat="server">
            <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-nationality">Nationality</label>
            <div class="input-group" id="divNationalityData" runat="server">
                <asp:DropDownList ID="drpNationality" class="form-control" runat="server">
                    <asp:ListItem Text="Select" Selected="true" Value="">
                    </asp:ListItem>
                    <asp:ListItem Text="Afghanistan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Albania" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Algeria" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="American Samoa" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Andorra" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Angola" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Anguilla" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Antarctica" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Antigua and Barbuda" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Argentina" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Armenia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Aruba" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Ascension Island" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Australia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Austria" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Azerbaijan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bahamas" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bahrain" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bangladesh" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Barbados" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Belarus" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Belgium" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Belize" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Benin" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bermuda" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bhutan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bolivia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bosnia and Herzegovina" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Botswana" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bouvet Island" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Brazil" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="British Indian Ocean Territory" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Brunei Darussalam" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Bulgaria" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Burkina Faso" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Burundi" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cambodia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cameroon" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Canada" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cape Verde" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cayman Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Central African Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Chad" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Chile" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="China" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Christmas Island" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cocos (Keeling) Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Colombia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Comoros" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Congo, Democratic Republic of the" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Congo, Republic of" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cook Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Costa Rica" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cote dIvoire" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Croatia/Hrvatska" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cuba" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Curacao" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Cyprus" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Czech Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Denmark" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Djibouti" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Dominica" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Dominican Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="East Timor" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Ecuador" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Egypt" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="El Salvador" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Equatorial Guinea" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Eritrea" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Estonia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Ethiopia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Falkland Islands (Malvina)" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Faroe Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Fiji" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Finland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="France" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="French Guiana" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="French Polynesia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="French Southern Territories" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Gabon" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Gambia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Georgia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Germany" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Ghana" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Gibraltar" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Greece" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Greenland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Grenada" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guadeloupe" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guam" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guatemala" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guernsey" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guinea" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guinea-Bissau" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Guyana" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Haiti" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Heard and McDonald Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Holy See (City Vatican State)" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Honduras" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Hong Kong" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Hungary" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Iceland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="India" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Indonesia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Iran" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Iraq" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Ireland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Isle of Man" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Israel" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Italy" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Jamaica" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Japan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Jersey" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Jordan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Kazakhstan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Kenya" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Kiribati" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Korea, Democratic Peoples Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Kuwait" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Kyrgyzstan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Lao Peoples Democratic Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Latvia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Lebanon" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Lesotho" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Liberia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Libya" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Liechtenstein" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Lithuania" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Luxembourg" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Macau" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Macedonia, Former Yugoslav Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Madagascar" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Malawi" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Malaysia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Maldives" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mali" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Malta" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Marshall Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Martinique" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mauritania" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mauritius" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mayotte" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mexico" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Micronesia, Federal State of" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Moldova" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Monaco" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mongolia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Montserrat" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Morocco" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Mozambique" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Myanmar" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Namibia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Nauru" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Nepal" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Netherlands Antilles" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Netherlands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="New Caledonia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="New Zealand" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Nicaragua" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Niger" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Nigeria" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Niue" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Norfolk Island" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Northern Mariana Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Norway" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Oman" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Pakistan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Palau" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Palestinian Territories" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Panama" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Papua New Guinea" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Paraguay" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Peru" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Philippines" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Pitcairn Island" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Poland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Portugal" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Puerto Rico" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Qatar" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Republic of Korea" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Reunion Island" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Romania" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Russian Federation" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Rwanda" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Saint Helena" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Saint Kitts and Nevis" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Saint Lucia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Saint Vincent and the Grenadines" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="San Marino" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Sao Tome and Principe" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Saudi Arabia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Seborga" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Senegal" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Serbia And Montenegro" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Seychelles" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Sierra Leone" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Singapore" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Slovak Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Slovenia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Solomon Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Somalia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="South Africa" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="South Georgia and Sandwich Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Spain" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Sri Lanka" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="St. Pierre and Miquelon" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Sudan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Suriname" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Svalbard and Jan Mayen Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Swaziland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Sweden" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Switzerland" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Syrian Arab Republic" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Taiwan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Tajikistan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Tanzania" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Thailand" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Togo" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Tokelau" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Tonga" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Trinidad and Tobago" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Tunisia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Turkey" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Turkmenistan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Turks and Caicos Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Tuvalu" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="UAE" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Uganda" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Ukraine" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="United Kingdom" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Uruguay" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="US Minor Outlying Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="USA" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Uzbekistan" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Vanuatu" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Venezuela" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Vietnam" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Virgin Islands (British)" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Virgin Islands (USA)" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Wallis and Futuna Islands" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Western Sahara" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Western Samoa" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Yemen" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Yugoslavia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Zambia" Value="UAE">
                    </asp:ListItem>
                    <asp:ListItem Text="Zimbabwe" Value="UAE">
                    </asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpNationality"
                Display="Dynamic" ErrorMessage="Enter Nationality" data-i18n="flightpassenger-error-nationality" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger"
                Enabled="false"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 mb-3" id="divPassportPlace" runat="server">
            <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-passport-place">Passport Issue Place</label>
            <div class="input-group">
                <asp:TextBox ID="txtPassportIssueLocation" CssClass="form-control" runat="server" Text=""
                    AutoComplete="off"></asp:TextBox>
                </div>
                <%-- <div class="invalid-feedback" id="ErrPassportPlace" runat="server">--%>
                <asp:RequiredFieldValidator ID="rfvAdultLocation" ValidationGroup="WebValidation"
                    Display="Dynamic" runat="server" ControlToValidate="txtPassportIssueLocation" data-i18n="flightpassenger-enter-place" ErrorMessage="Enter Passsport Issue Location"
                    CssClass="rptErrorMassage h7 heading-regular text-danger" Enabled="false"></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="REVtxtPassportIssueLocation" Display="Dynamic" runat="server"
                    ControlToValidate="txtPassportIssueLocation" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z]+$"
                    CssClass="rptErrorMassage h7 heading-regular text-danger" ErrorMessage="Please Enter valid Place." data-i18n="hotel-booking-errorvalidpassportLocation"></asp:RegularExpressionValidator>
            
                <%--</div>--%> 
        </div>
        <div class="col-md-6 mb-3" id="divTelephone" runat="server">
            <label class="h8 heading-semibold text-colour7" data-i18n="flightpassenger-telephone">Telephone Number</label>
            <div class="input-group">
                <asp:TextBox ID="txtTelephone" class="form-control" runat="server" Text="" MaxLength="12"
                    AutoComplete="off"></asp:TextBox>
                </div>
                <div class="invalid-feedback" id="ErrTelephone" runat="server">
                    <asp:RequiredFieldValidator ID="rfvAdultTelephone" Display="Dynamic" runat="server"
                        ControlToValidate="txtTelephone" ErrorMessage="Enter Mobile Number" data-i18n="hotel-booking-errormobile" ValidationGroup="WebValidation"
                        CssClass="rptErrorMassage h7 heading-regular text-danger" Enabled="false"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revAdulttelephone" runat="server" Display="Dynamic"
                        ControlToValidate="txtTelephone" ValidationGroup="WebValidation" ValidationExpression="^[0-9]+$"
                        CssClass="rptErrorMassage h7 heading-regular text-danger" ErrorMessage="Enter Valid Number.(max 12 digit.)" data-i18n="hotel-booking-erroronlydigit"
                        Enabled="false">
                    </asp:RegularExpressionValidator>
                 
            </div>
        </div>
        <div class="col-md-6 mb-3">
            <label class="h8 heading-semibold text-colour7" id="divPassportissue" runat="server" data-i18n="flightpassenger-passport-date">Passport Issue Date</label>
            <div class="dvTxtEffectiveDateChild input-group">
                <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control icnDate"
                    Text="" AutoComplete="off"></asp:TextBox>
                 <div class="input-group-append">
                    <span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span>
                </div>
                </div>
                <%--<div class="invalid-feedback" id="ErrPassportissue" runat="server">--%>
                <asp:RequiredFieldValidator ID="rfvAdultEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate"
                    Display="Dynamic" ErrorMessage="Enter Date of Issuance" data-i18n="flightpassenger-error-passsport-issue" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="customIssueDateValidator" runat="server" data-i18n="flightpassenger-error-passsport-issue-date" ErrorMessage="Issue date must be greater than DOB"
                    CssClass="rptErrorMassage h7 heading-regular text-danger" ValidationGroup="WebValidation" OnServerValidate="IssueDateValidator"
                    Display="Dynamic" ControlToValidate="txtEffectiveDate" Enabled="false"></asp:CustomValidator>
                <%--</div>--%> 
        </div>
        <div class="col-md-6 mb-3">
            <label class="h8 heading-semibold text-colour7" id="divPassportexpiry" runat="server" data-i18n="flightpassenger-passport-expiry">Passport Expiry Date</label>
            <div class="dvTxtExpiryDateChild input-group">
                <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control icnDate" Text=""
                    AutoComplete="off"></asp:TextBox>
                 <div class="input-group-append">
                    <span class="input-group-text bg-white"><i class="fa-regular fa-calendar"></i></span>
                </div>
                </div>
                <%-- <div class="invalid-feedback" id="ErrPassportexpiry" runat="server">--%>
                <asp:RequiredFieldValidator ID="rfvAdultExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                    Display="Dynamic" ErrorMessage="Enter Expiry Date" data-i18n="flightpassenger-error-passsport-expiry" ValidationGroup="WebValidation" CssClass="rptErrorMassage h7 heading-regular text-danger"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomExpiryDate" runat="server" ErrorMessage="Expiry date must be greater than issue date" data-i18n="flightpassenger-error-passsport-expiry-date"
                    CssClass="rptErrorMassage h7 heading-regular text-danger" ValidationGroup="WebValidation" OnServerValidate="IssueExpiryValidator"
                    Display="Dynamic" ControlToValidate="txtExpiryDate" Enabled="false"></asp:CustomValidator>
                <%--</div>--%>
            
        </div>
    </div>
</div>
