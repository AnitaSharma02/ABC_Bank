<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdultPassangerDetails_Domestic.ascx.cs" 
    Inherits="SourceControl_AdultPassangerDetails_Domestic" %>

<script src="../Jquery/UserControlValidation.js" type="text/javascript"></script>

    <div class="row">
         <div class="col-sm-4 mb-3 col-12">
           <label class="h8 heading-semibold text-colour7">Title</label>
          <div class="select_box">
            <asp:DropDownList ID="ddlTitle" class="form-control" runat="server">
                <asp:ListItem Value="Male" Selected="True">MR</asp:ListItem>
                <asp:ListItem Value="Female">MS</asp:ListItem>
                <asp:ListItem Value="Female">MRS</asp:ListItem>
            </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle"
                Display="Dynamic" ErrorMessage="Enter Title" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage" InitialValue="1"></asp:RequiredFieldValidator>
         </div>
         
        <div class="col-sm-4 col-12 mb-3">
            <label data-i18n="flightpassenger-first" class="h8 heading-semibold text-colour7">First Name</label>
            <asp:TextBox ID="txtFirstName" class="form-control" MaxLength="27" runat="server" Text="" AutoComplete="off"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAdultFirstName" runat="server" ControlToValidate="txtFirstName"
                Display="Dynamic" ErrorMessage="Enter First Name" data-i18n="flightpassenger-error-enter-firstname" ValidationGroup="WebValidation"
                CssClass="danger"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revAdultFirstName" Display="Dynamic" runat="server"
                ControlToValidate="txtFirstName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
                CssClass="danger" ErrorMessage="Please Enter valid First Name." data-i18n="hotel-booking-errorvalidfname"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-4 col-12 mb-3">
            <label data-i18n="flightmb-2passenger-last" class="h8 heading-semibold text-colour7">Last Name</label>
            <asp:TextBox ID="txtLastName" class="form-control" MaxLength="27" runat="server" AutoComplete="off"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAdultLastName" runat="server" ControlToValidate="txtLastName"
                Display="Dynamic" data-i18n="flightpassenger-error-enter-lastname" ErrorMessage="Enter Last Name" ValidationGroup="WebValidation"
                CssClass="danger" ></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="revAdultLastName" Display="Dynamic" runat="server"
                ControlToValidate="txtLastName" ValidationGroup="WebValidation" ValidationExpression="^[a-zA-Z.\s]{1,50}"
                CssClass="danger" ErrorMessage="Please Enter valid Last Name." data-i18n="hotel-booking-errorvalidlname"></asp:RegularExpressionValidator> 
        </div>
     </div>
    <div class="row d-flex" id="AdditionalInfo" runat="server">
        <div class="col-sm-4 col-12 mb-3" id="divNationality" runat="server" >
            <label data-i18n="flightpassenger-nationality" class="h8 heading-semibold text-colour7">Nationality</label>
           <%-- <label data-i18n="flightpassenger-nationality">Nationality</label>--%>
            <div class="select_box" id="divNationalityData" runat="server">
                <asp:DropDownList ID="drpNationality" class="form-control" runat="server" >
                 <asp:ListItem Text="Nepal" Value="NP">
                </asp:ListItem>
            </asp:DropDownList>

            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpNationality"
                ErrorMessage="Enter Nationality" ValidationGroup="WebValidation" CssClass="rptErrorMassage" Display="Dynamic"
                Enabled="false"></asp:RequiredFieldValidator>
        </div>
       
        <div class="col-sm-4 col-12 mb-3" id="divGender" runat="server">
            <label data-i18n="flightpassenger-gender" class="h8 heading-semibold text-colour7">Gender</label>
            <div class="select_box">
                <asp:DropDownList ID="ddlGender" class="form-control" runat="server">
                    <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                    <asp:ListItem Value="F">Female</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="ddlGender"
                Display="Dynamic" ErrorMessage="Select Gender" ValidationGroup="WebValidation"
                CssClass="rptErrorMassage" InitialValue="1"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-4 col-12 mb-3" id="divType" runat="server">
            <label data-i18n="flightpassenger-Type" class="h8 heading-semibold text-colour7">Type</label>
            <asp:TextBox ID="txtType" class="form-control" MaxLength="27" Text="ADULT" AutoComplete="off" runat="server" readonly="true"></asp:TextBox>
        </div>
    </div>