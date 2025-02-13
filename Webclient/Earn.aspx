<%@ Page Title="Earn" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="earn.aspx.cs" Inherits="earn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
   <style> 
    .dvInnerBanner,
    .dvRedemptionMenu,
    #sitemap{
        display: none;
    }
    .table .thead-light th{background:#cb4f56;color:#fff;}
</style>
 
 <div class="dvBreadcrumbs">
     <div class="container-xl">
         <nav>
             <ul class="breadcrumb px-0 py-3">
                 <li class="me-3">
                     <a href="\">
                         <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                 </li>
                 <li class="breadcrumb-item"><a href="\">Home</a></li>
                 <li class="breadcrumb-item active">Earn</li>
             </ul>
         </nav>
     </div>
 </div>

<div class="dvEarn mb-5">
    <div class="container-xl">
        <div class="row">
            <div class="col-12">
                <h2 class="heading1 mb-3">Earn </h2>
                <p class="mb-3">
                    Earn more Infinity points with every transaction.
                </p>
                <p class="heading-semibold text-colour7">Infinity Rewards</p>
                <p class="mb-3">ABC Banking Corporation Infinity Rewards Programme is one of the best rewards programmes in Mauritius, offering you the highest earning potential and a wide choice of redemption options through our selected partners. You can earn Infinity points and redeem them for free flights, hotels, gift vouchers and much more.</p>
                
                <p class="heading-semibold text-colour7">Earning Infinity Points</p>
                <p class="mb-3">It is simple! The more you use your credit card, both locally and internationally, the more rewards you earn.</p>
                <p class="mb-3">All international card transactions must be made in non-MUR currency to be eligible for earning Infinity Points. </p>
                
                <p class="heading-semibold text-colour7 mb-3">Credit Card</p> 
                
                <div class="table-responsive">
                    <table class="table table-bordered">
                  <thead class="thead-light">
                    <tr>
                      <th scope="col" class="bg-colour1">Product Name</th>
                      <th scope="col" class="bg-colour1">Domestic POS</th>
                      <th scope="col" class="bg-colour1">International POS</th> 
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <th scope="row">Platinum</th>
                      <td>MUR 1000 = 1 Point</td>
                      <td>MUR 1000 = 1 Point </td> 
                    </tr>
                    
                  </tbody>
                </table>
                </div>

                <p class="heading-semibold text-colour7">Tracking Infinity Points</p>
                <p class="mb-3">You can easily track the number of Infinity Points you have earned through the Infinity Rewards website, online and mobile banking, and monthly e-statements.</p>

                <p class="heading-semibold text-colour7">How to Activate my Infinity Rewards Account?</p>
                <p class="heading-semibold text-colour7 mb-3">First time Activation:</p>
                <p class="heading-semibold text-colour7">Activation and Viewing Infinity Rewards Programme through Infinity Rewards Web Portal</p>
                  
                 <ul class="mt-3 px-0" style="list-style: none;">
                     <li class="mb-3">1. Log into the Infinity Rewards Portal and Click on the Activation tab. You will be asked to enter your ABC Banking Customer ID (CIF). </li>
                     <li class="mb-3">2. Tick the box to agree on the Terms & Conditions of the Infinity Rewards programme. </li>
                 </ul>
                <p class="heading-semibold text-colour7 mb-3">Upon entering the above:</p>
                 <ul class="mt-3 px-0" style="list-style: none;">
                     <li class="mb-3">1. You will be asked to enter an OTP (OTP will be sent to your registered mobile number and email).</li>
                     <li class="mb-3">2. You will be prompted to create a new password / Retype the password</li>
                     <li class="mb-3">3. Your ABC Banking registered email ID will be automatically displayed.</li>
                     <li class="mb-3">4. Tick the box to agree on the Terms & Conditions and click “Continue”.</li>
                     <li class="mb-3">5. Congratulations! Your account is activated, and you can access your rewards.</li>
                 </ul>
                <p class="mb-3"><span class="heading-semibold text-colour7">Note:</span> If your email ID is not registered with ABC Banking, Infinity Rewards activation will not be possible. You will need to update your email ID by visiting the ABC Banking branch before proceeding to ABC Banking Infinity Rewards activation. </p>

                <p class="heading-semibold text-colour7 mb-3">Activation and Viewing Infinity Rewards points through ABC Banking Internet Banking and Mobile Banking App</p>
                <p class="mb-3">Customers can Activate their Infinity Rewards account by logging in ABC Banking Mobile App as per below:</p>
                 <ul class="mt-3 px-0" style="list-style: none;">
                     <li class="mb-3">1. Log into the ABC Banking Mobile App.</li>
                     <li class="mb-3">2. Click on Infinity Rewards.</li>
                     <li class="mb-3">3. Key in your Customer Unique Identifier (CIF)</li>
                     <li class="mb-3">4. You will receive an OTP for authentication on your registered Mobile number or Email Address.</li>
                     <li class="mb-3">5. Set your Password (must comply with ABC Banking’s password policy)</li>
                     <li class="mb-3">6. Your Infinity Rewards account will be activated, and you will have access to your rewards account.</li>
                 </ul> 
            </div>
        </div>
    </div>
</div> 
 
<script>
    $(document).ready(function () {
        BindBanner();
    });
</script>
</asp:Content>



