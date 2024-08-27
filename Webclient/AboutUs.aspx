<%@ Page Title="About Us" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        #dvHeroSlider {
            display: none;
        }

        .dvRedemptionMenu {
            display: none;
        }

        #sitemap {
            display: none;
        }

        .dvInnerBanner {
            display: none
        }
    </style>
    <div class="dvBreadcrumbs">
        <div class="container-lg">
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
        <div class="container-lg">
            <div class="row">
                <div class="col-12">
                    <h2 class="heading1 mb-3">About The Program</h2>
                    <p class="">NIC Express Reward is the most comprehensive rewards program, tailored to your needs, and gives you additional benefits and a rich experience every time you use the bank's products and services.</p>
                    <p class="">To deliver the best, we designed the Express NIC Express Reward program to suit your lifestyle. Now, you can earn Points for using our banking products. you can then replace NIC Express Rewards Points by:</p>

                    <div class="accordion mt-3" id="static-accordion">
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
                                        <li class=" mx-3">More than 900 airlines: with NIC Express Reward, you won't be restricted to one airline or one departure point, and you can redeem your tickets online and directly from more than 900 airlines around the world, including low-cost airlines such as flydubai, air arabia, ryanair and more than 900 airlines: with NIC Express Reward  rewards, you won't be restricted to one airline or one departure point, but you can redeem your tickets online and directly from more than 900 airlines around the world, including economic airlines such as ryanair and others.</li>
                                        <li class=" mx-3">There are no dates where reservations are not allowed: you can book and travel on any date you choose.</li>
                                        <li class=" mx-3">No restrictions: you can book your ticket even if you are in any City and want to exchange your reward Points from the bank for a flight ticket from London to Paris, for example, for you, your family, or friends.</li>
                                        <li class=" mx-3">Comfort: you can register comfortably in your home or office.</li>
                                        <li class=" mx-3">Double benefits: enjoy additional rewards by earning "extra miles" for airline programs when traveling on their flights and using your NIC Express Reward Points to book tickets.</li>
                                        <li class=" mx-3">Hotels: more than 450,000 hotels around the world.</li>
                                        <li class=" mx-3">Book immediately at the touch of a button.</li>
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
                                        <li class=" mx-3">Car rental is available in 150,000 of our car rental partners around the world.</li>
                                        <li class=" mx-3">Immediate booking and confirmation.</li>
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
                                        <li class=" mx-3">Through the online store you can access thousands of products and appliances from electronics, books, sports supplies, and household items.</li>
                                        <li class=" mx-3">Immediate booking and confirmation.</li>
                                        <li class=" mx-3">You can shop online and see all products at any time.</li>
                                        <li class=" mx-3">Choose from thousands of products for international brands such as apple, sony, samsung, toshiba and others.</li>
                                        <li class=" mx-3">Products will be delivered to your home anywhere in Nigeria.</li>
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

