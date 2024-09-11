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
        <label class="label" data-i18n="flightpassenger-title">Title</label>
        <div class="dvInput select_box">
            <asp:DropDownList ID="ddlTitle" CssClass="form-control" runat="server" Style="width: 100%;">
                <asp:ListItem Value="1" Selected="True">Title</asp:ListItem>
                <asp:ListItem Value="Male">Mstr</asp:ListItem>
                <asp:ListItem Value="Female">Miss</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle"
            Display="Dynamic" ErrorMessage="Enter Title" CssClass="rptErrorMassage danger" ValidationGroup="WebValidation"
            InitialValue="1" data-i18n="flightpassenger-error-title"></asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-first">First Name</label>
        <div class="dvInput input-group ">
            <asp:TextBox ID="txtFirstName" MaxLength="27" CssClass="form-control" runat="server" Text="" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultFirstName" runat="server" ControlToValidate="txtFirstName"
            ErrorMessage="Enter First Name" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger" Display="Dynamic" data-i18n="flightpassenger-error-enter-firstname"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultFirstName" Display="Dynamic" runat="server"
            ControlToValidate="txtFirstName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
            CssClass="rptErrorMassage danger" ErrorMessage="Please Enter valid First Name." data-i18n="flightpassenger-error-enter-correctfirstname"></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-last">Last Name</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtLastName" MaxLength="27" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultLastName" runat="server" ControlToValidate="txtLastName"
            ErrorMessage="Enter Last Name" data-i18n="flightpassenger-error-enter-lastname" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultLastName" Display="Dynamic" runat="server"
            ControlToValidate="txtLastName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
            CssClass="rptErrorMassage danger" data-i18n="flightpassenger-error-enter-correctlastname" ErrorMessage="Please Enter valid Last Name."></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-date">Date of Birth</label>
        <div class="dvInputGroup dvTxtDOBChild input-group">
            <asp:TextBox ID="txtDOB" class="form-control icnDate" runat="server" AutoComplete="off"></asp:TextBox>
            <div class="input-group-append">
                <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
            </div>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultDOB" runat="server" ControlToValidate="txtDOB"
            Display="Dynamic" ErrorMessage="Enter Date of Birth" data-i18n="flightpassenger-error-enter-date" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="customChildDOBValidator" runat="server" ErrorMessage="Child  (2-12 yrs)"
            Display="Dynamic" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger" data-i18n="flightpassenger-error-enter-adults"
            OnServerValidate="IssueChildDOBValidator" ControlToValidate="txtDOB"></asp:CustomValidator>
    </div>
</div>
<div class="row">
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-passport">Passport Number</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtPassportNo" CssClass="form-control" AutoComplete="off" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassportNo"
            Display="Dynamic" data-i18n="flightpassenger-error-passport" ErrorMessage="Enter Passport Number" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassportNo"
            Display="Dynamic" CssClass="rptErrorMassage danger" ValidationGroup="WebValidation"
            ValidationExpression="^[a-zA-Z0-9]*$" data-i18n="flightpassenger-error-correct-passport" ErrorMessage="Please Enter correct passport no. (no blank space)."></asp:RegularExpressionValidator>
       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
runat="server"  ControlToValidate="txtPassportNo"  Display="Dynamic"
ValidationExpression="^.{6,}$" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
ErrorMessage="Minimum 6 characters required." data-i18n="flightpassenger-error-correct-passportLength"></asp:RegularExpressionValidator>
        </div>
    <div class="col-md-6 mb-3">
        <label class="label" data-i18n="flightpassenger-email">E-Mail ID</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailID"
            Display="Dynamic" data-i18n="flightpassenger-error-emailid" ErrorMessage="Enter E-mail Id" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultEmail" Display="Dynamic" runat="server"
            ControlToValidate="txtEmailID" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
            CssClass="rptErrorMassage danger" ValidationGroup="WebValidation" data-i18n="flightpassenger-error-valid-emailid" ErrorMessage="Enter Valid E-mail Id">
        </asp:RegularExpressionValidator>
    </div>
</div>
<div id="AdditionalInfo" runat="server">
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
                Display="Dynamic" ErrorMessage="Enter Nationality" data-i18n="flightpassenger-error-nationality" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
                Enabled="false"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-6 mb-3" id="divPassportPlace" runat="server">
            <label class="label" data-i18n="flightpassenger-passport-place">Passport Issue Place</label>
            <div class="dvInput input-group">
                <asp:TextBox ID="txtPassportIssueLocation" CssClass="form-control" runat="server" Text=""
                    AutoComplete="off"></asp:TextBox>
            </div>
            <%-- <div class="invalid-feedback" id="ErrPassportPlace" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultLocation" ValidationGroup="WebValidation"
                Display="Dynamic" runat="server" ControlToValidate="txtPassportIssueLocation" data-i18n="flightpassenger-enter-place" ErrorMessage="Enter Passsport Issue Location"
                CssClass="rptErrorMassage danger" Enabled="false"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="REVtxtPassportIssueLocation" Display="Dynamic" runat="server"
                ControlToValidate="txtPassportIssueLocation" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z]+$"
                CssClass="rptErrorMassage danger" ErrorMessage="Please Enter valid Place." data-i18n="hotel-booking-errorvalidpassportLocation"></asp:RegularExpressionValidator>

            <%--</div>--%>
        </div>
        <div class="col-md-6 mb-3" id="divTelephone" runat="server">
            <label class="label" data-i18n="flightpassenger-telephone">Telephone Number</label>
            <div class="dvInput input-group">
                <asp:TextBox ID="txtTelephone" class="form-control" runat="server" Text="" MaxLength="12"
                    AutoComplete="off"></asp:TextBox>
            </div>
            <div class="invalid-feedback" id="ErrTelephone" runat="server">
                <asp:RequiredFieldValidator ID="rfvAdultTelephone" Display="Dynamic" runat="server"
                    ControlToValidate="txtTelephone" ErrorMessage="Enter Mobile Number" data-i18n="hotel-booking-errormobile" ValidationGroup="WebValidation"
                    CssClass="rptErrorMassage danger" Enabled="false"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revAdulttelephone" runat="server" Display="Dynamic"
                    ControlToValidate="txtTelephone" ValidationGroup="WebValidation" ValidationExpression="^[0-9]+$"
                    CssClass="rptErrorMassage danger" ErrorMessage="Enter Valid Number.(max 12 digit.)" data-i18n="hotel-booking-erroronlydigit"
                    Enabled="false">
                </asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="col-md-6 mb-3">
            <label class="label" id="divPassportissue" runat="server" data-i18n="flightpassenger-passport-date">Passport Issue Date</label>
            <div class="dvTxtEffectiveDateChild dvInputGroup input-group">
                <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control icnDate"
                    Text="" AutoComplete="off"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                </div>
            </div>
            <%--<div class="invalid-feedback" id="ErrPassportissue" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate"
                Display="Dynamic" ErrorMessage="Enter Date of Issuance" data-i18n="flightpassenger-error-passsport-issue" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
                Enabled="false"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="customIssueDateValidator" runat="server" data-i18n="flightpassenger-error-passsport-issue-date" ErrorMessage="Issue date must be greater than DOB"
                CssClass="rptErrorMassage danger" ValidationGroup="WebValidation" OnServerValidate="IssueDateValidator"
                Display="Dynamic" ControlToValidate="txtEffectiveDate" Enabled="false"></asp:CustomValidator>
            <%--</div>--%>
        </div>
        <div class="col-md-6 mb-3">
            <label class="label" id="divPassportexpiry" runat="server" data-i18n="flightpassenger-passport-expiry">Passport Expiry Date</label>
            <div class="dvTxtExpiryDateChild dvInputGroup input-group">
                <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control icnDate" Text=""
                    AutoComplete="off"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                </div>
            </div>
            <%-- <div class="invalid-feedback" id="ErrPassportexpiry" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                Display="Dynamic" ErrorMessage="Enter Expiry Date" data-i18n="flightpassenger-error-passsport-expiry" ValidationGroup="WebValidation" CssClass="rptErrorMassage danger"
                Enabled="false"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomExpiryDate" runat="server" ErrorMessage="Expiry date must be greater than issue date" data-i18n="flightpassenger-error-passsport-expiry-date"
                CssClass="rptErrorMassage danger" ValidationGroup="WebValidation" OnServerValidate="IssueExpiryValidator"
                Display="Dynamic" ControlToValidate="txtExpiryDate" Enabled="false"></asp:CustomValidator>
            <%--</div>--%>
        </div>
    </div>
</div>
