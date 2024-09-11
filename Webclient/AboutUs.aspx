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
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item active" data-i18n="navigation-about-us">About Us</li>
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
                                    <ul>
                                        <li class="mx-3">
                                            <strong>No Limitations:</strong> With Infinity Rewards, you won't be 
                                            restricted to one airline or one departure point. Redeem your tickets online and directly 
                                            from more than 900 airlines around the world, including low-cost carriers such 
                                            as flydubai, Air Arabia, Ryanair, and more.</li>
                                        <li class="mx-3">
                                            <strong>No Blocked Dates:</strong> There are no dates where reservations 
                                            are not allowed. You can book and travel on any date you choose.
                                        </li>
                                        <li class="mx-3">
                                            <strong>No Restrictions:</strong> Whether you're in any city, you can redeem your bank 
                                            reward points for a flight ticket, such as from London to Paris, for yourself, your family,
                                            or friends. The flexibility allows you to book tickets without limitations, making
                                            travel more convenient and rewarding.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Convenience:</strong> Easily register and book from the comfort of your home 
                                            or office.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Double Benefits:</strong> Earn extra miles through airline programs while using
                                            your Infinity Rewards points to book tickets, maximizing your rewards.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Global Hotel Options:</strong> Choose from over 450,000 hotels worldwide for 
                                            your stay.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Instant Booking:</strong> Secure your travel plans immediately with just the 
                                            touch of a button.
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
                                    <ul>
                                        <li class="mx-3">
                                            <strong>Global Car Rental Access:</strong> Choose from over 150,000 car rental 
                                            partners worldwide for your convenience.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Instant Booking & Confirmation:</strong> Secure your rental car with 
                                            immediate booking and confirmation for a hassle-free experience.
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
                                    <ul>
                                        <li class="mx-3">
                                            <strong>Wide Product Selection:</strong> Access thousands of products and appliances through the online store, including electronics, books, sports equipment, and household items.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Instant Booking & Confirmation:</strong> Enjoy immediate booking and confirmation for a seamless shopping experience.
                                        </li>
                                        <li class="mx-3">
                                            <strong>24/7 Online Shopping:</strong> Browse and purchase products at any time, from anywhere.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Top International Brands:</strong> Choose from renowned global brands such as Apple, Sony, Samsung, Toshiba, and more.
                                        </li>
                                        <li class="mx-3">
                                            <strong>Home Delivery:</strong> Have your purchases delivered directly to your home, anywhere in Mauritius.
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

