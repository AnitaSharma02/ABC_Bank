<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdultPassangerDetails.ascx.cs"
    Inherits="SourceControl_AdultPassangerDetails" %>

<script src="../Jquery/UserControlValidation.js" type="text/javascript"></script>
<script type="text/javascript">
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
        $(".dvTxtDOBAdult .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtDOB.ClientID%>").datepicker("show");
        });
        $(".dvTxtExpiryDateAdult .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtExpiryDate.ClientID%>").datepicker("show");
        });
        $(".dvTxtEffectiveDateAdult .input-group-append .input-group-text").on("click", function () {
            $("#<%=txtEffectiveDate.ClientID%>").datepicker("show");
        });
        const currentDate = new Date();
        var newdate = currentDate.setFullYear(currentDate.getFullYear() - 12);
       
        $("#<%=txtDOB.ClientID%>").datepicker({
            numberOfMonths: 1,
            changeMonth: true,
            changeYear: true,
            //showButtonPanel: true,
            yearRange: "-90:-0",
            dateFormat: 'dd/mm/yy',
            maxDate: new Date(newdate),
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
        <label class="label">Title</label>
        <div class="dvInput select_box">
            <asp:DropDownList ID="ddlTitle" class="form-control" runat="server">
                <asp:ListItem Value="Male" Selected="True">Mr</asp:ListItem>
                <asp:ListItem Value="Female">Ms</asp:ListItem>
                <asp:ListItem Value="Female">Mrs</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle"
            Display="Dynamic" ErrorMessage="Enter Title" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage text-danger" InitialValue="1"></asp:RequiredFieldValidator>
    </div>
    <div class="col-md-6 mb-3">
        <label class="label">First Name</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtFirstName" class="form-control" MaxLength="27" runat="server" Text="" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultFirstName" runat="server" ControlToValidate="txtFirstName"
            Display="Dynamic" ErrorMessage="Enter First Name" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage text-danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultFirstName" Display="Dynamic" runat="server"
            ControlToValidate="txtFirstName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
            CssClass="rptErrorMassage text-danger" ErrorMessage="Please Enter valid First Name."></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label">Last Name</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtLastName" class="form-control" MaxLength="27" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultLastName" runat="server" ControlToValidate="txtLastName"
            Display="Dynamic" ErrorMessage="Enter Last Name" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage text-danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultLastName" Display="Dynamic" runat="server"
            ControlToValidate="txtLastName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
            CssClass="rptErrorMassage text-danger" ErrorMessage="Please Enter valid Last Name."></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label">Date of Birth</label>
        <div class="dvInputGroup dvTxtDOBAdult input-group">
            <asp:TextBox ID="txtDOB" class="form-control icnDate" runat="server" AutoComplete="off"></asp:TextBox>
            <div class="input-group-append">
                <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
            </div>
        </div>
        <asp:RequiredFieldValidator ID="rfvAdultDOB" runat="server" ControlToValidate="txtDOB"
            Display="Dynamic" ErrorMessage="Enter Date of Birth" ValidationGroup="WebValidation"
            CssClass="rptErrorMassage text-danger"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="CustomValidatorAdultDOB" runat="server" ErrorMessage="Adults (12+ yrs)"
            Display="Dynamic" ValidationGroup="WebValidation" OnServerValidate="IssueAdultDateValidator"
            ControlToValidate="txtDOB" CssClass="rptErrorMassage text-danger">
        </asp:CustomValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label">Passport Number</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtPassportNo" MaxLength="15" class="form-control" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassportNo" Display="Dynamic"
            ErrorMessage="Enter Passport Number" ValidationGroup="WebValidation" CssClass="rptErrorMassage text-danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassportNo"
            Display="Dynamic" ValidationGroup="WebValidation" CssClass="rptErrorMassage text-danger"
            ValidationExpression="^[a-zA-Z0-9]*$" ErrorMessage="Please Enter correct passport no. (no blank space)."></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
            runat="server" ControlToValidate="txtPassportNo" Display="Dynamic"
            ValidationExpression="^.{6,}$" ValidationGroup="WebValidation" CssClass="rptErrorMassage text-danger"
            ErrorMessage="Minimum 6 characters required."></asp:RegularExpressionValidator>

    </div>
    <div class="col-md-6 mb-3">
        <label class="label">E-Mail ID</label>
        <div class="dvInput input-group">
            <asp:TextBox ID="txtEmailID" class="form-control" runat="server" AutoComplete="off"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ValidationGroup="WebValidation" ID="RequiredFieldValidator2"
            runat="server" ControlToValidate="txtEmailID" Display="Dynamic" ErrorMessage="Enter E-mail Id"
            CssClass="rptErrorMassage text-danger"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revAdultEmail" Display="Dynamic" runat="server"
            ControlToValidate="txtEmailID" ValidationGroup="WebValidation" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
            CssClass="rptErrorMassage text-danger" ErrorMessage="Enter Valid E-mail Id">
        </asp:RegularExpressionValidator>

    </div>
</div>
<div id="AdditionalInfo" runat="server">
    <div class="row">
        <div class="col-md-6 mb-3" id="divNationality" runat="server">
            <label class="label">Nationality</label>

            <div class="dvInput input-group" id="divNationalityData" runat="server">
                <asp:DropDownList ID="drpNationality" class="form-control" runat="server">
                    <asp:ListItem Text="Select" Selected="true" Value="">
                    </asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                ControlToValidate="drpNationality"
                ErrorMessage="Enter Nationality" ValidationGroup="WebValidation" CssClass="rptErrorMassage text-danger"
                Enabled="false"></asp:RequiredFieldValidator>

        </div>
        <div class="col-md-6 mb-3" id="divPassportPlace" runat="server">
            <label class="label">Passport Issue Place</label>
            <div class="dvInput input-group">
                <asp:TextBox ID="txtPassportIssueLocation" class="form-control" runat="server" Text=""
                    AutoComplete="off"></asp:TextBox>
            </div>
            <%--<div class="invalid-feedback" id="ErrPassportPlace" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultLocation" runat="server" ControlToValidate="txtPassportIssueLocation" Display="Dynamic"
                ErrorMessage="Enter Passsport Issue Location" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage text-danger" Enabled="false"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="REVtxtPassportIssueLocation" Display="Dynamic" runat="server"
                ControlToValidate="txtPassportIssueLocation" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z ]+$"
                CssClass="rptErrorMassage text-danger" ErrorMessage="Please Enter valid Place."></asp:RegularExpressionValidator>


            <%--</div>--%>
        </div>
        <div class="col-md-6 mb-3" id="divTelephone" runat="server">
            <label class="label">Mobile Number</label>
            <div class="dvInput input-group">
                <asp:TextBox ID="txtTelephone" class="form-control" runat="server" Text="" MaxLength="12"
                    AutoComplete="off"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="rfvAdultTelephone" Display="Dynamic" runat="server"
                ControlToValidate="txtTelephone" ErrorMessage="Enter Mobile Number"
                ValidationGroup="WebValidation"
                CssClass="rptErrorMassage text-danger"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revAdulttelephone" runat="server" Display="Dynamic"
                ControlToValidate="txtTelephone" ValidationGroup="WebValidation" ValidationExpression="^[0-9]+$"
                CssClass="rptErrorMassage text-danger" ErrorMessage="Enter Valid Number.(max 12 digit.)">
            </asp:RegularExpressionValidator>


        </div>

        <div class="col-md-6 mb-3" id="divPassportissue" runat="server">
            <label class="label">Passport Issue Date</label>
            <div class="dvTxtEffectiveDateAdult dvInputGroup input-group">
                <asp:TextBox ID="txtEffectiveDate" class="form-control" runat="server" Text="" AutoComplete="off"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                </div>
            </div>
            <%--<div class="invalid-feedback" id="ErrPassportissue" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate"
                Display="Dynamic" ErrorMessage="Enter Date of Issuance" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage text-danger" Enabled="false"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="customIssueDateValidator" ValidationGroup="WebValidation"
                runat="server" ErrorMessage="Issue date must be greater than DOB" CssClass="rptErrorMassage text-danger"
                OnServerValidate="IssueDateValidator" ControlToValidate="txtEffectiveDate" Display="Dynamic"
                Enabled="false"></asp:CustomValidator>
            <%--</div>--%>
        </div>
        <div class="col-md-6 mb-3" id="divPassportexpiry" runat="server">
            <label class="label">Passport Expiry Date</label>
            <div class="dvTxtExpiryDateAdult dvInputGroup input-group">
                <asp:TextBox ID="txtExpiryDate" class="form-control" runat="server" Text=""
                    AutoComplete="off"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text bg-colour6"><i class="fa-regular fa-calendar"></i></span>
                </div>
            </div>
            <%--<div class="invalid-feedback" id="ErrPassportexpiry" runat="server">--%>
            <asp:RequiredFieldValidator ID="rfvAdultExpiryDate" ValidationGroup="WebValidation"
                runat="server" ControlToValidate="txtExpiryDate" ErrorMessage="Enter Expiry Date"
                CssClass="rptErrorMassage text-danger" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomExpiryDate" ValidationGroup="WebValidation" runat="server"
                ErrorMessage="Expiry date must be greater than issue date" CssClass="rptErrorMassage text-danger"
                OnServerValidate="IssueExpiryValidator" ControlToValidate="txtExpiryDate" Display="Dynamic"
                Enabled="false"></asp:CustomValidator>
            <%--</div>--%>
        </div>
    </div>
</div>
