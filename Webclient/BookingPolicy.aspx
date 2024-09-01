<%@ Page Title="Booking Policy" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="BookingPolicy.aspx.cs" Inherits="BookingPolicy" %>

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
        <div class="container-xl">
            <nav>
                <ul class="breadcrumb px-0 py-3">
                    <li class="mr-3">
                        <a href="\">
                            <img src="images/icons/arrows/back-arrow.svg" alt="" /></a>
                    </li>
                    <li class="breadcrumb-item"><a href="\" data-i18n="bread-home">Home</a></li>
                    <li class="breadcrumb-item active" data-i18n="navigation-booking-policy">Booking Policy</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvBookingPolicy mb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <h2 class="heading1 mb-3">Booking Policy </h2>
                </div>
                <div class="col-12">
                    <div class="dvCommonAccordion accordion" id="static-accordion">
                        <!-- FLIGHTS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold" type="button" data-toggle="collapse"
                                        data-target="#collapse1">
                                        Flights
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse1" class="collapse show" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">NIC Express Reward Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">The total NIC Express Reward NPoints displayed includes air fare and applicable taxes. It excludes applicable airport taxes, baggage and other charges which may be payable by the airline or at certain airports.  Some airlines and fare rules do not include baggage as part of the booked fares. Also, rules vary from time to time without prior notice. It is advisable to contact the airline directly or visit the airline’s website to learn more about baggage limits. </li>
                                        <li class="mb-3">You are required to pay the entire NPoints prior to the confirmation of your booking.</li>
                                        <li class="mb-3">There will be no refund for 'no-shows' or any partially unused flights.</li>
                                        <li class="mb-3">For any information regarding your Frequent Flyer miles, please contact the airline directly.</li>
                                        <li class="mb-3">Kindly ensure that you have the valid visa, immigration clearance and passport with a validity of at least 6 months. </li>
                                        <li class="mb-3">To avail of infant fares, an infant must be under 24 months throughout the entire itinerary you are booking. This includes both onward and return journeys. In the event the infant is 24 months or above on the return journey, you will need to make a separate booking using child fare. </li>
                                        <li class="mb-3">Infants must be accompanied by an adult at least 18 years of age. All bookings for flights are powered by Gift Management DMCC. However, Gift Management DMCC is not responsible for any schedule change by the airline after issuance of the ticket but will inform you of the same if informed by the airline. It is advisable to reconfirm your flight timings 24 hours prior to your flight departure.</li>
                                        <li class="mb-3">NIC Express Reward and Gift Management DMCC reserve the right to alter any and all fees from time to time, without notice.</li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Check-in:</p>
                                    <ul>
                                        <li class="mb-3">The passenger needs to check-in at least 3 hours prior to departure for all International flights, and 2 hours prior to departure for all Domestic flights. However, we would always recommend you call the respective airline to understand its check-in policy timings. </li>
                                        <li class="mb-3">As per airline rules, the standard check-in time begins 3 hours prior to departure.</li>
                                        <li class="mb-3">Infants must have valid proof of age documents showing that the infant is less than 24 months old. You will be required to carry appropriate travel permissions (valid passport, visa, immigration clearance etc.) acquired before departure. NIC Express Reward or Gift Management DMCC is not responsible for lack of documents produced during check-in.</li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Amendments:</p>
                                    <ul>
                                        <li class="mb-3">NIC Express Reward can assist you with amendments to most bookings. For any amendments or queries you may call NIC Express Reward’s 24-hour Customer Support number at +977-1-5970101. In some cases, though, you will need to contact the airline directly. </li>
                                        <li class="mb-3">Every booking made on NIC Express Reward is subject to amendment charges levied by the airline, which may vary by flight and booking class. </li>
                                        <li class="mb-3">If you amend your booking, you will be charged the difference in fare, if any, and applicable when the amendment is made. However, if the new fare is lower than the original fare, the difference in the fare amount will not be refunded. The rebooking charges as applicable will be collected and charged to your NIC Express Reward loyalty account in equivalent NPoints.</li>
                                        <li class="mb-3">In addition to the airline's amendment charges, Gift Management DMCC charges an amendment handling fee of (Amount in Dollars / AED) equivalent NPoints per passenger. We will collect these charges from you when we make the changes to your travel plans. We will also collect the difference in fare, if any is applicable when the amendment is made. </li>
                                        <li class="mb-3">Depending on the airline policy, some booked fares may not be amended. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Cancelation and Refund Policy:</p>
                                    <ul>
                                        <li class="mb-3">For bookings made through NIC Express Reward, <strong>no cancelations</strong> are allowed if done voluntarily. Hence no reward NPoints will be refunded back into your account. For any reasons not initiated by you, i.e. flights being grounded/cancelled/or any other unforeseen circumstances wherein you are denied travel, applicable reward NPoints will be refunded back into your account. This again would be as per the discretion of NIC Express Reward / Gift Management DMCC. </li>
                                        <li class="mb-3">Every booking made on NIC Express Reward is subject to cancelation charges levied by the airline, which may vary by flight and booking class. </li>
                                        <li class="mb-3">Some booked fares may be non-refundable as per the specific airline's policy. Partially utilized tickets as well in most cases, are non-refundable.</li>
                                        <li class="mb-3">There would be a handling fee of (Amount in Dollars / AED) equivalent NPoints levied per passenger per ticket for any cancelations when any refund is initiated by NIC Express Reward or Gift Management DMCC as an exception. </li>
                                        <li class="mb-3">Only cancelation requests made telephonic through our Customer Support shall be entertained. NIC Express Reward or Gift Management DMCC shall not be liable to entertain any cancelation requests made through any other medium including but not limited to SMS and e-mail. </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- FLIGHTS -->

                        <!-- HOTELS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse2">
                                        Hotels
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse2" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">The standard check-in time is after 14:00 hours (local time) and check-out time is 12 noon. This could vary according to season and/or city. Please check with the hotel for clarity.</li>
                                        <li class="mb-3">An early check-in or a late check-out facility depends completely on the hotel’s room availability on that particular date. NIC Express Reward / Gift Management DMCC cannot guarantee anything in this regard. </li>
                                        <li class="mb-3">Not all hotels include complimentary breakfast. NIC Express Reward and Gift Management DMCC do not take responsibility in case the hotel refuses to offer complimentary breakfast or meals. </li>
                                        <li class="mb-3">As a general policy, a single room can accommodate a maximum of 2 adults and/or 2 adults and 2 children. </li>

                                    </ul>

                                    <p class="heading-semibold text-colour7">Reconfirmation:</p>
                                    <ul>
                                        <li class="mb-3">There is no need to reconfirm the hotel booking, once done. However, if you wish to do so you may call NIC Express Reward’s 24-hour Customer Support number at +977-1-5970101. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Amendments:</p>
                                    <ul>
                                        <li class="mb-3">Once a hotel booking is placed through BML Rewards, no change/amendment in the respective hotel booking dates is allowed. If you wish to change your check-in and check-out dates, a completely new hotel booking would have to be made. There will be no refund of NPoints granted on the original booking.</li>
                                        <li class="mb-3">Please do not call the hotel directly for reservation changes or cancelations, as the same would not be considered.</li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Cancelation and Refund Policy:</p>
                                    <ul>
                                        <li class="mb-3">For bookings made through NIC Express Reward, no cancelations are allowed, if done voluntarily. Hence no NPoints will be refunded back into your account. For any reasons not initiated by you i.e. hotel is sold out and/or any other unforeseen circumstances wherein you are denied stay, applicable NPoints will be refunded back into your account. This again is as per the discretion of NIC Express Reward or Gift Management DMCC.  </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- HOTELS -->

                        <!-- CAR RENTALS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse3">
                                        Car Rentals
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse3" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">Once car redemption is done by you, the confirmation is not immediate. It takes a minimum of 24–72 hours for a confirmation. For any queries regarding confirmation, you may call NIC Express Reward’s 24-hour Customer Support number at +977-1-5970101. </li>
                                        <li class="mb-3">In case the requested car pick-up and drop locations are different, there could be a surcharge fee in addition to applicable taxes. This will be notified at the time of booking itself by our back-end team, and only once a confirmation is sought from you the confirmation process would be carried out.</li>
                                        <li class="mb-3">The time you pick your car for rental, you will be required to leave a security deposit against possible damage to the car during the rental period. The deposit will be charged directly by the car hire company. Quite often, a credit card in the name of the main driver is required. If the condition of the car and extras is the same on return as at the time of rental, and is in accordance with the fuel policy, the security deposit will be refunded after returning the car (please note that it may take (number) working days for the money to appear in your credit card account again). </li>

                                    </ul>

                                    <p class="heading-semibold text-colour7">Amendments:</p>
                                    <ul>
                                        <li class="mb-3">Once the car booking is done, the existing booking cannot be amended. However, for exceptional cases, a new request can be made for the revised date/time, provided this request is initiated 72 hours prior to the original date of pick-up. Applicable surcharges would apply in equivalent NPoints.</li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Cancelation Policy:</p>
                                    <ul>
                                        <li class="mb-3">For bookings made through NIC Express Reward, no cancelations are allowed if done voluntarily by you. Hence no NPoints will be refunded back to your account. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Restrictions:</p>
                                    <ul>
                                        <li class="mb-3">To rent a car, you must be within 25 years to 70 years of age. Certain car suppliers however do allow to rent a car between the ages of 21-25 years. There could be a ‘young driver’ surcharge fee applicable in some cases. </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- CAR RENTALS -->

                        <!-- SHOP -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse4">
                                        Shop
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse4" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">Your reward merchandise will be delivered within (number) working weeks (subject to availability of stock).</li>
                                        <li class="mb-3">Shop products are delivered only within (region/state).</li>
                                        <li class="mb-3">Sometimes there are restrictions to deliver products to certain regions. Once an order is placed, we would be informing you of the same and the order will be cancelled if the courier faces restrictions to deliver in that region.</li>
                                        <li class="mb-3">Once an order is placed it cannot be exchanged or upgraded.</li>
                                        <li class="mb-3">For any queries regarding the confirmation/delivery, you may call NIC Express Reward’s 24-hour Customer Support number at +977-1-5970101. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Cancelation Policy:</p>
                                    <ul>
                                        <li class="mb-3">Once the order is placed, a voluntary cancelation is not permitted. Hence, no reward NPoints will be refunded for the same. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Damage Replacements:</p>
                                    <ul>
                                        <li class="mb-3">In case the product is received in a damaged condition, please intimate us within 24 hours of receiving the same. The damaged product will be replaced although no NPoints will be refunded to you. In the event we do not receive your request within 24 hours, no product replacement will be initiated. There will be no refund of reward NPoints. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Incorrect Product:</p>
                                    <ul>
                                        <li class="mb-3">At times when you receive a product different from what you requested, please intimate NIC Express Reward’s 24-hour Customer Support number at +977-1-5970101 within 24 hours of receiving the same. The product will be replaced with your requested product although no NPoints will be refunded to you. In the event we do not receive your request within 24 hours, no product replacement will be initiated. There will be no refund of reward points. </li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">Refunds:</p>
                                    <ul>
                                        <li class="mb-3">There are times when a product request is made by you but fails to reach you due to the product stock suddenly being over. In such cases, your reward NPoints will be refunded to you. </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- SHOP -->
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

