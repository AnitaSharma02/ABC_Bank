<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
     <script src="Jquery/jquery.md5.js" type="text/javascript"></script>
    <script src="Jquery/Validation.js" type="text/javascript"></script> 
    <style>
         #dvHeroSlider, .dvRedemptionMenu, #sitemap, .dvInnerBanner {
          display: none;
        }
  </style>
  <script type="text/javascript">
        $(document).ready(function () {
            $(".toggle-password").click(function () {
                $(this).toggleClass("fa-eye fa-solid fa-eye-slash");
                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }
            });
        });
  </script>
<div class="dvBreadcrumbs">
    <div class="container-xl">
        <nav>
            <ul class="breadcrumb px-0 pt-3 pb-0">
                <li class="mr-3"><a href="\">
                    <img src="images/icons/arrows/arrow-left.svg" alt=""></a>
                </li>
                <li class="breadcrumb-item"><a href="\">Home</a></li>
                <li class="breadcrumb-item">Forgot Password</li>
            </ul>
        </nav>
    </div>
</div>

 <div class="dvForgotPassword py-5">
    <div class="container-xl">
        <div class="row">
            <div class="col-md-6 offset-md-3">
                <div class="border bg-colour2 p-4">
                    <div class="row">
                         <div class="col-12 mb-2">
                             <h2 class="h5 heading-semibold text-colour7 mb-1">Forgot Password</h2>
                         </div>
                         <div id="ErrorMsgContainer" class="col-12" runat="server">
                            <asp:Label runat="server" ID="lblMessagesDetails" CssClass="h6 heading-semibold text-colour1" Text=""></asp:Label>
                        </div>
                         <div id="divForgotOTP" class="col-12 mb-3">
                             <div class="row">
                                <div class="col-12">
                                     <div id="ForgotPasswordValidation" class="h6 heading-semibold text-colour1"></div>
                                 </div>
                                 <div class="col-12 mb-4">
                                     <label class="label">ID:</label>
                                     <div class="dvInput input-group"> 
                                        <input type="text" class="form-control" id="txtMemberId" runat="server" />
                                     </div>
                                 </div>   
                                 <div class="col-12">  
                                    <input type="button" class="btn btn-one w-100" value="Continue" onclick="SendForgotPasswordOTP();" />
                                 </div>
                             </div>
                        </div>
                        <div id="divForgotPassword" class="col-12" style="display: none;">
                            <div class="row">
                                <div class="col-12 mb-3">
                                    <label class="label">OTP:</label>
                                    <div class="dvInput input-group">
                                        <input type="password" class="form-control" id="txtOTP" runat="server" maxlength="4" /></div>
                                    </div>
                                
                                <div class="col-12 mb-3">
                                    <label class="label">Password:</label>
                                    <div class="dvInput input-group">
                                        <input type="password" class="form-control" id="txtPassword" runat="server" />
                                         <div class="input-group-append">
                                             <span toggle="#CP_txtPassword" class="input-group-text bg-colour6 fa fa-solid fa-eye-slash toggle-password passwordShow"></span>
                                         </div>
                                     </div>
                                </div>
                                <div class="col-12 mb-4">
                                    <label class="label">Confirm Password:</label>
                                    <div class="dvInput input-group">
                                        <input type="password" class="form-control" id="txtConfirmPassword" runat="server" />
                                        <div class="input-group-append">
                                            <span toggle="#CP_txtConfirmPassword" class="input-group-text bg-colour6 fa fa-solid fa-eye-slash toggle-password passwordShow"></span>
                                        </div> 
                                    </div>
                                </div>
                                <div class="col-12 mb-3"> 
                                     <input type="button" class="btn btn-one w-100" value="Reset Password" onclick="var retValue = ValidateForgotPasswordOTP(); event.returnValue = retValue; (event.preventDefault) ? event.preventDefault() : event.returnValue = false; return retValue;" />
                                </div>
                                </div>
                             </div>       
                            </div>
                </div>
            </div>
        </div>
    </div>
</div>
   
</asp:Content>
