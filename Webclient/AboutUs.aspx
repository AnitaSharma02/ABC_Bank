<%@ Page Title="About Us" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        .dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        #sitemap{
            display: none;
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\">Home</a></li>
                    <li class="breadcrumb-item active">About Us</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvAboutUs py-3">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <h2 class="heading1 mb-3">About the Infinity Rewards:</h2>
                    <p class="mb-3">
                         Infinity Rewards is ABC Banking Corporation’s most comprehensive rewards program, 
                        tailored to your needs. It gives you additional benefits and a rich experience every
                        time you use the bank's products and services.
                    </p>
                    <p class="mb-3">
                        In an effort to deliver the best, we designed the Infinity Rewards program to suit 
                        your lifestyle. Now, you can earn points for using our banking products. 
                        You can then replace the Infinity Rewards points by*:
                    </p>

                    <div class="dvCommonAccordion accordion mt-3" id="static-accordion">
                        <!-- Airlines -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold" type="button" data-toggle="collapse"
                                        data-target="#collapse1">
                                        Airlines
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse1" class="collapse show" data-parent="#static-accordion">
                                <div class="card-body">
                                   <ul class="px-0" style="list-style:none">
     <li class="mb-3">
         <p class="heading-semibold text-colour7">No Limitations:</p><p> With Infinity Rewards, you won't be 
         restricted to one airline or one departure point. Redeem your tickets online and directly 
         from more than 900 airlines around the world, including low-cost carriers such 
         as flydubai, Air Arabia, Ryanair, and more.</p></li>
     <li class="mb-3">
         <p class="heading-semibold text-colour7">No Blocked Dates:</p><p> There are no dates where reservations 
         are not allowed. You can book and travel on any date you choose.</p>
     </li>
     <li class="mb-3">
         <p class="heading-semibold text-colour7">No Restrictions:</p><p> Whether you're in any city, you can redeem your bank 
         reward points for a flight ticket, such as from London to Paris, for yourself, your family,
         or friends. The flexibility allows you to book tickets without limitations, making
         travel more convenient and rewarding.</p>
     </li>
     <li class="mb-3">
         <p class="heading-semibold text-colour7">Convenience:</p> <p>Easily register and book from the comfort of your home 
         or office.</p>
     </li>
     <li class="mb-3">
         <p class="heading-semibold text-colour7">Double Benefits:</p><p> Earn extra miles through airline programs while using
         your Infinity Rewards points to book tickets, maximizing your rewards.</p>
     </li>
     <li class="mb-3">
         <p class="heading-semibold text-colour7">Global Hotel Options:</p> <p>Choose from over 450,000 hotels worldwide for 
         your stay.</p>
     </li>
     <li class="mb-3">
         <p class="heading-semibold text-colour7">Instant Booking:</p> <p>Secure your travel plans immediately with just the 
         touch of a button.</p>
     </li>
 </ul>
                                </div>
                            </div>

                        </div>
                        <!-- Airlines -->

                        <!-- Car Rental -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse2">
                                        Car Rental Companies
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse2" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                   <ul class="px-0" style="list-style:none">
    <li class="mb-3">
        <p class="heading-semibold text-colour7">Global Car Rental Access:</p> <p>Choose from over 150,000 car rental 
        partners worldwide for your convenience.</p>
    </li>
    <li class="mb-3">
        <p class="heading-semibold text-colour7">Instant Booking & Confirmation:</p><p> Secure your rental car with 
        immediate booking and confirmation for a hassle-free experience.</p>
    </li>
</ul>
                                </div>
                            </div>
                        </div>
                        <!-- Car Rental -->

                        <!-- Online Store -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse3">
                                        Online Store
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse3" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                     <ul class="px-0" style="list-style:none">
      <li class="mb-3">
          <p class="heading-semibold text-colour7">Wide Product Selection:</p><p> Access thousands of products and appliances through the online store, including electronics, books, sports equipment, and household items.</p>
      </li>
      <li class="mb-3">
          <p class="heading-semibold text-colour7">Instant Booking & Confirmation:</p><p> Enjoy immediate booking and confirmation for a seamless shopping experience.</p>
      </li>
      <li class="mb-3">
          <p class="heading-semibold text-colour7">24/7 Online Shopping:</p><p> Browse and purchase products at any time, from anywhere.</p>
      </li>
      <li class="mb-3">
          <p class="heading-semibold text-colour7">Top International Brands:</p><p> Choose from renowned global brands such as Apple, Sony, Samsung, Toshiba, and more.</p>
      </li>
      <li class="mb-3">
          <p class="heading-semibold text-colour7">Home Delivery:</p> <p>Have your purchases delivered directly to your home, anywhere in Mauritius.</p>
      </li>
  </ul>
                                </div>
                            </div>
                        </div>
                        <!-- Online Store -->
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

