<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfantPassangerDetails.ascx.cs"
    Inherits="SourceControl_InfantPassangerDetails" %>

<script src="../Jquery/UserControlValidation.js" type="text/javascript"></script>
<script type="text/javascript">
    //code for InfantRepeater Date
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
        $(".dvTxtDOBInfant .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtDOB.ClientID%>").datepicker("show");
        });
        $(".dvTxtExpiryDateInfant .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtExpiryDate.ClientID%>").datepicker("show");
        });
        $(".dvTxtEffectiveDateInfant .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtEffectiveDate.ClientID%>").datepicker("show");
        });
        $("#<%=txtDOB.ClientID%>").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            changeYear: true,
            //showButtonPanel: true,
            yearRange: "-90:-0",
            dateFormat: 'dd/mm/yy',
            maxDate: new Date,
            minDate: '-3Y',
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
        <label class="label" data-i18n="flightpassenger-title">Title</label>
        <div class="dvInput select_box">
            <asp:DropDownList ID="ddlTitle" class="form-control" runat="server">
                <asp:ListItem Value="1" Selected="True">Title</asp:ListItem>
                <asp:ListItem Value="Male">Mstr</asp:ListItem>
                <asp:ListItem Value="Female">Miss</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle"
            Display="Dynamic" data-i18n="flightpassenger-error-title" ErrorMessage="Enter Title" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage danger" InitialValue="1"></asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-first">First Name</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtFirstName" class="form-control" MaxLength="27" runat="server" Text="" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultFirstName" runat="server" ControlToValidate="txtFirstName"
            Display="Dynamic" data-i18n="flightpassenger-error-enter-firstname" ErrorMessage="Enter First Name" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultFirstName" Display="Dynamic" runat="server"
            ControlToValidate="txtFirstName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
            CssClass="rptErrorMassage danger" data-i18n="flightpassenger-error-enter-correctfirstname" ErrorMessage="Please Enter valid First Name."></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-last">Last Name</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtLastName" class="form-control" MaxLength="27" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultLastName" runat="server" ControlToValidate="txtLastName"
            Display="Dynamic" data-i18n="flightpassenger-error-enter-lastname" ErrorMessage="Enter Last Name" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultLastName" Display="Dynamic" runat="server"
            ControlToValidate="txtLastName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
            CssClass="rptErrorMassage danger" data-i18n="flightpassenger-error-enter-correctlastname" ErrorMessage="Please Enter valid Last Name."></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-date">Date of Birth</label>
        <div class="dvTxtDOBInfant dvInputGroup input-group">
            <asp:TextBox ID="txtDOB" class="form-control icnDate" runat="server" AutoComplete="off"></asp:TextBox>

            <div class="input-group-append">
                <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
            </div>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultDOB" runat="server" ControlToValidate="txtDOB"
            Display="Dynamic" data-i18n="flightpassenger-error-enter-date" ErrorMessage="Enter Date of Birth" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="CustomValidateInfantDOB" runat="server" data-i18n="flightpassenger-error-enter-adults" ErrorMessage="Infant (0-2 yrs)"
            Display="Dynamic" CssClass="rptErrorMassage danger" ValidationGroup="WebValidation"
            OnServerValidate="IssueInfantDateValidator" ControlToValidate="txtDOB"></asp:CustomValidator>

    </div>
</div>
<div class="row">

    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-passport">Passport Number</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtPassportNo" class="form-control" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassportNo"
            Display="Dynamic" data-i18n="flightpassenger-error-passport" ErrorMessage="Enter Passport Number" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassportNo"
            Display="Dynamic" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
            ValidationExpression="^[a-zA-Z0-9]*$" data-i18n="flightpassenger-error-correct-passport" ErrorMessage="Please Enter correct passport no. (no blank space)."></asp:RegularExpressionValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
runat="server"  ControlToValidate="txtPassportNo"  Display="Dynamic" 
ValidationExpression="^.{6,}$" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
ErrorMessage="Minimum 6 characters required." 
ForeColor="Red" data-i18n="flightpassenger-error-correct-passportLength"></asp:RegularExpressionValidator>
    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-email">E-Mail ID</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtEmailID" class="form-control" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ValidationGroup="WebValidation" ID="RequiredFieldValidator2"
            runat="server" ControlToValidate="txtEmailID" Display="Dynamic" data-i18n="flightpassenger-error-emailid" ErrorMessage="Enter E-mail Id"
            CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultEmail" Display="Dynamic" runat="server"
            ControlToValidate="txtEmailID" ValidationGroup="WebValidation" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
            CssClass="rptErrorMassage danger" data-i18n="flightpassenger-error-valid-emailid" ErrorMessage="Enter Valid E-mail Id">
        </asp:RegularExpressionValidator>

    </div>
</div>
<div id="AdditionalInfo" runat="server" style="display: none;">
    <div class="row">
        <div class="col-md-6 mb-3" id="divNationality" runat="server">
            <label class="label" data-i18n="flightpassenger-nationality">Nationality</label>
            <div class="dvInput input-group" id="divNationalityData" runat="server">
                <asp:DropDownList ID="drpNationality" class="form-control" runat="server">
                    <asp:ListItem Text="Select" Selected="true" Value="">
                    </asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpNationality"
                Display="Dynamic" data-i18n="flightpassenger-error-nationality" ErrorMessage="Enter Nationality" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
                Enabled="false"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 mb-3" id="divPassportPlace" runat="server">
            <label class="label" data-i18n="flightpassenger-passport-place">Passport Issue Place</label>
            <div class="dvInput input-group">
                <asp:TextBox ID="txtPassportIssueLocation" class="form-control" runat="server" Text=""
                    AutoComplete="off"></asp:TextBox>
            </div>
            <%-- <div class="invalid-feedback" id="ErrPassportPlace" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultLocation" Display="Dynamic" runat="server" ControlToValidate="txtPassportIssueLocation"
                data-i18n="flightpassenger-error-passsport" ErrorMessage="Enter Passsport Issue Location" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage danger" Enabled="false"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="REVtxtPassportIssueLocation" Display="Dynamic" runat="server"
                ControlToValidate="txtPassportIssueLocation" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z]+$"
                CssClass="rptErrorMassage danger" ErrorMessage="Please Enter valid Place." data-i18n="hotel-booking-errorvalidpassportLocation"></asp:RegularExpressionValidator>

            <%-- </div>--%>
        </div>
        <div class="col-md-6 mb-3" id="divTelephone" runat="server">
            <label class="label" data-i18n="flightpassenger-telephone">Telephone Number</label>
            <div class="dvInput input-group">
                <asp:TextBox ID="txtTelephone" runat="server" Text="" MaxLength="12"
                    AutoComplete="off"></asp:TextBox>
            </div>
            <div class="invalid-feedback" id="ErrTelephone" runat="server">
                <asp:RequiredFieldValidator ID="rfvAdultTelephone" Display="Dynamic" runat="server"
                    ControlToValidate="txtTelephone" data-i18n="flightpassenger-error-mobile" ErrorMessage="Enter Mobile Number" ValidationGroup="WebValidation"
                    CssClass="rptErrorMassage danger" Enabled="false"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revAdulttelephone" runat="server" Display="Dynamic"
                    ControlToValidate="txtTelephone" ValidationGroup="WebValidation" ValidationExpression="^[0-9]+$"
                    CssClass="rptErrorMassage danger" data-i18n="flightpassenger-error-valid-mobile" ErrorMessage="Enter Valid Number.(max 12 digit.)"
                    Enabled="false">
                </asp:RegularExpressionValidator>
            </div>

        </div>

        <div class="col-md-6 mb-3" id="divPassportissue" runat="server">
            <label class="label" data-i18n="flightpassenger-passport-date">Passport Issue Date</label>
            <div class="dvTxtEffectiveDateInfant dvInputGroup input-group">
                <asp:TextBox ID="txtEffectiveDate" class="form-control icnDate" runat="server"
                    Text="" AutoComplete="off"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                </div>
            </div>
            <%--<div class="invalid-feedback" id="ErrPassportissue" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate"
                Display="Dynamic" data-i18n="flightpassenger-error-passsport-issue" ErrorMessage="Enter Date of Issuance" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage danger" Enabled="false"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="customIssueDateValidator" ValidationGroup="WebValidation"
                runat="server" data-i18n="flightpassenger-error-passsport-issue-date" ErrorMessage="Issue date must be greater than DOB" CssClass="rptErrorMassage danger"
                OnServerValidate="IssueDateValidator" ControlToValidate="txtEffectiveDate" Display="Dynamic"
                Enabled="false"></asp:CustomValidator>
            <%-- </div>--%>
        </div>
        <div class="col-md-6 mb-3">
            <label class="label" id="divPassportexpiry" runat="server" data-i18n="flightpassenger-passport-expiry">Passport Expiry Date</label>
            <div class="dvTxtExpiryDateInfant dvInputGroup input-group">
                <asp:TextBox ID="txtExpiryDate" class="form-control icnDate" runat="server" Text=""
                    AutoComplete="off"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                </div>
            </div>
            <%-- <div class="invalid-feedback" id="ErrPassportexpiry" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultExpiryDate" ValidationGroup="WebValidation"
                runat="server" ControlToValidate="txtExpiryDate" data-i18n="flightpassenger-error-passsport-expiry" ErrorMessage="Enter Expiry Date"
                CssClass="rptErrorMassage danger" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomExpiryDate" ValidationGroup="WebValidation" runat="server"
                data-i18n="flightpassenger-error-passsport-expiry-date" ErrorMessage="Expiry date must be greater than issue date" CssClass="rptErrorMassage danger"
                OnServerValidate="IssueExpiryValidator" ControlToValidate="txtExpiryDate" Display="Dynamic"
                Enabled="false"></asp:CustomValidator>
            <%--</div>--%>
        </div>
    </div>
</div>
