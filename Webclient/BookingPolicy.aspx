<%@ Page Title="Booking Policy" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="BookingPolicy.aspx.cs" Inherits="BookingPolicy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CP" runat="Server">
    <style>
        .dvHeroSlider,
        .dvInnerBanner,
        .dvRedemptionMenu,
        #sitemap {
            display: none;
        }
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
                    <li class="breadcrumb-item active">Booking Policy</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvBookingPolicy mb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <h2 class="heading1 mb-3">Booking Policy </h2>
                    <p class="mb-3">This booking policy (“Booking Policy”) shall be read in line with the Infinity Reward Program Terms and Conditions.</p>
                </div>
                <div class="col-12">
                    <div class="dvCommonAccordion accordion" id="static-accordion">
                        <!-- FLIGHTS -->
                        <div class="accordion-item mb-3">
                                <h2 class="accordion-header mb-0">
                                    <button class="accordion-button btn btn-block text-start p-3" type="button" data-bs-toggle="collapse"
                                        data-bs-target="#collapse1">
                                        Flights
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>

                            <div id="collapse1" class="collapse show" data-bs-parent="#static-accordion">
                                <div class="accordion-body">
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>The displayed total price includes taxes and any applicable airfare. It does not include any applicable baggage fees, airport taxes, or other costs that might need to be paid to the airline or at particular airports. Baggage is not always included in the booked tickets by all airlines or by all fare rules. Additionally, rules might change at any time without warning. To find out more about baggage restrictions, it is best to get in touch with the airline directly or visit its website.</p></li>
                                        <li class="mb-3"><p>Before your reservation is confirmed, you must pay the full cost of the ticket in points.</p></li>
                                        <li class="mb-3"><p>There will be no refund of points for 'no-shows' (i.e. flights missed for any reason whatsoever) or any partially unused flights.</p></li>
                                        <li class="mb-3"><p>For any information regarding your Frequent Flyer miles, please contact the airline directly.</p></li>
                                        <li class="mb-3"><p>Please make sure you have a passport that is valid for at least six months, a valid visa, and immigration clearance.</p></li>
                                        <li class="mb-3"><p>An infant must be under 24 months old for the duration of the entire itinerary you are booking to qualify for infant fares. This covers both outbound and incoming trips. For the return trip, a separate reservation using a kid fee is required if the infant is 24 months or older.</p></li>
                                        <li class="mb-3"><p>Infants must be accompanied by an adult at least 18 years of age. All bookings for flights are powered by GIIFT Management DMCC. However, Gift Management DMCC is not responsible for any schedule change by the airline after issuance of the ticket but will inform you of the same if informed by the airline. It is advisable to reconfirm your flight timings 24 hours before your flight departure.</p></li>
                                        <li class="mb-3"><p>Infinity Rewards and GIIFT reserve the right to alter any and all fees from time to time, without any prior notice.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Flight Amendments:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>For any amendments or queries you may email <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>. In some cases, though, you will need to contact the airline directly.</p></li>
                                        <li class="mb-3"><p>Every booking made on the Infinity Rewards platform is subject to amendment charges levied by the airline, which may vary by flight and booking class.</p></li>
                                        <li class="mb-3"><p>If you amend your booking, you will be charged the difference in fare, if any, and applicable when the amendment is made. However, if the new fare is lower than the original fare, the difference in the fare amount will not be refunded. The rebooking charges as applicable will be collected and charged to your Credit/Debit Card or reward points.</p></li>
                                        <li class="mb-3"><p>In addition to the airline's amendment charges, Infinity Rewards charges an amendment handling fee of USD 10 equivalent points per passenger. We will collect these charges from you when we make the changes to your travel plans. We will also collect the difference in fare, if any is applicable when the amendment is made.</p></li>
                                        <li class="mb-3"><p>Depending on the airline policy, some booked fares may not be amended.</li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Flight Cancellation and Refund Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>For bookings made through the Infinity Rewards platform, cancellations are allowed if the booking qualifies under the refund policy, and then points will be refunded back into your account. For any reasons not initiated by you, i.e. flights being grounded/cancelled/or any other unforeseen circumstances wherein you are denied travel, applicable reward points will be refunded back into your account within 45 working days. This again would be at the discretion of Infinity Rewards.</p></li>
                                        <li class="mb-3"><p>For bookings made through the Infinity Rewards platform, no cancellations are allowed. Hence no points will be refunded back into your account.</p></li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- FLIGHTS -->

                        <!-- HOTELS -->
                        <div class="accordion-item mb-3">
                                <h2 class="accordion-header mb-0">
                                    <button class="accordion-button btn btn-block text-start p-3 collapsed" type="button" data-bs-toggle="collapse"
                                        data-bs-target="#collapse2">
                                        Hotels
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>

                            <div id="collapse2" class="collapse" data-bs-parent="#static-accordion">
                                <div class="accordion-body">
                                    <p class="heading-semibold text-colour7 mb-2">Hotel Booking Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>The regular check-in time is after 14:00 hours (local time) and check-out time is noon. This could vary according to season and/or city. For precise information, please contact the hotel.</p></li>
                                        <li class="mb-3"><p>The hotel's ability to accommodate an early check-in or a late check-out completely depends on the availability of rooms on that specific date. Nothing in this regard may be guaranteed by Infinity Rewards. Please contact the hotel for more information.</p></li>
                                        <li class="mb-3"><p>The meal plan for each hotel stay must be agreed upon and confirmed with the hotel. Infinity Rewards is not liable for any discrepancy on this matter.</p></li>
                                        <li class="mb-3"><p>Hotel policies determine the maximum occupancy for each room.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Hotel Amendments:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>If the hotel booking is within the refund policy, the customer will receive a full return of Points, and all cancellation policies are stated before confirming the booking. Once the hotel is booked, the booking cannot be amended. For amendments, the current booking must be cancelled, and a new booking will have to be made.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Hotel Cancellation Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>The cancellation and refund policy shall be the same as for the hotel stay paid in points, i.e. if the hotel has a no cancellation and no refund policy, then Infinity Rewards shall not be eligible for a refund of the points paid for the stay.</p></li>
                                        <li class="mb-3"><p>If the hotel allows for the cancellation and refund of a hotel stay, customers must send the hotel’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation.</p></li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- HOTELS -->

                        <!-- CAR RENTALS -->
                        <div class="accordion-item mb-3">
                                <h2 class="accordion-header mb-0">
                                    <button class="accordion-button btn btn-block text-start p-3 collapsed" type="button" data-bs-toggle="collapse"
                                        data-bs-target="#collapse3">
                                        Car Rentals
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>

                            <div id="collapse3" class="collapse" data-bs-parent="#static-accordion">
                                <div class="accordion-body">
                                    <p class="heading-semibold text-colour7 mb-2">Booking Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Once car redemption is done by you, the confirmation is not immediate. It takes a minimum of 24–48 hours for confirmation. For any queries regarding the confirmation, you may email us at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p></li>
                                        <li class="mb-3"><p>Please see the Service Provider’s full terms and conditions below, which include the full name and company registered address of your Service Provider, information on and fees of extra products and services purchasable at the counter or based on your use of the rental, such as driving cross border and, if any, pick-up and drop-off grace periods, cancellation and refund policies.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Cancellation Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>If the Service Provider allows for the cancellation and refund of a booking, the Customers must send the Service Provider’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation. We shall then proceed with the refund of points on your account within 3 business days.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Experience:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Please see the Service Provider’s full terms and conditions, which include the full name and company registered address of your Service Provider, information on and fees of extra products and services purchasable at the counter, cancellation and refund policies.</p></li>
                                        <li class="mb-3"><p>If the Service Provider allows for the cancellation and refund of a booking, the Customers must send the Service Provider’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation. We shall then proceed with the refund of points on your account within 3 business days.</p></li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- CAR RENTALS -->

                        <!-- SHOP -->
                        <div class="accordion-item mb-3">
                                <h2 class="accordion-header mb-0">
                                    <button class="accordion-button btn btn-block text-start p-3 collapsed" type="button" data-bs-toggle="collapse"
                                        data-bs-target="#collapse5">
                                        Shop
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>

                            <div id="collapse5" class="collapse" data-bs-parent="#static-accordion">
                                <div class="accordion-body">
                                    <p class="heading-semibold text-colour7 mb-2">Shop Booking Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Your merchandise will be delivered within 2-7 working days (subject to availability of stock).</p></li>
                                        <li class="mb-3"><p>It takes up to 24 hours for confirmation of order upon payment. For any queries regarding the confirmation, you may email us at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p></li>
                                        <li class="mb-3"><p>Infinity Rewards shop products are delivered only within Mauritius.</p></li>
                                        <li class="mb-3"><p>Please see the Merchant’s and Courrier Service’s full terms and conditions, which include the full name and company registered address of the Merchant and Courrier Service, shipping conditions, return, cancellation, and refund policies.</p></li>
                                        <li class="mb-3"><p>If the Merchant allows for the cancellation and/or refund of an order, the Customers must send the Merchant’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation. We shall then proceed with the refund of points on your account within 3 business days.</p></li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- SHOP -->

                        <!-- GIFT VOUCHER -->
                        <div class="accordion-item mb-3">
                                <h2 class="accordion-header mb-0">
                                    <button class="accordion-button btn btn-block text-start p-3 collapsed" type="button" data-bs-toggle="collapse"
                                        data-bs-target="#collapse6">
                                        Gift Cards / Vouchers
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>

                            <div id="collapse6" class="collapse" data-bs-parent="#static-accordion">
                                <div class="accordion-body">
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Gift Voucher details will be emailed within 24 hours to your registered email address (subject to availability of stock). More details can be found in that email.</p></li>
                                        <li class="mb-3"><p>Gift vouchers have an expiry date, and the receiver must utilize the vouchers within this period. No extension of the expiry date will be allowed.</p></li>
                                        <li class="mb-3"><p>Gift vouchers can only be used once for a purchase. Any unused amount at the time of making the purchase will be forfeited. If the transaction amount exceeds the value of the Gift Voucher, the balance amount can be paid through other payment methods.</p></li>
                                        <li class="mb-3"><p>Gift vouchers do not accept cashbacks, refunds, or returns.</p></li>
                                        <li class="mb-3"><p>The merchant retains the right to reject any voucher that has been tampered with or found to be unacceptable in any way.</p></li>
                                        <li class="mb-3"><p>If you don’t receive an email, you can email us at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> mentioning the order details and your Rewards ID.</p></li>
                                        <li class="mb-3"><p>Infinity Rewards will not be responsible if a Gift Voucher is lost, stolen, damaged, or destroyed, and no replacement or refund of Union Rewards Points/Cash will be provided under any circumstances.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Exchange Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Once the order is placed, exchange is not permitted.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Cancellation Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Once the order is placed, cancellation is not permitted. Hence, no reward points will be refunded for the same.</p></li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- GIFT VOUCHER -->

                        <!-- LOUNGE -->
                        <div class="accordion-item mb-3">
                                <h2 class="accordion-header mb-0">
                                    <button class="accordion-button btn btn-block text-start p-3 collapsed" type="button" data-bs-toggle="collapse"
                                        data-bs-target="#collapse7">
                                        Lounge
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>

                            <div id="collapse7" class="collapse" data-bs-parent="#static-accordion">
                                <div class="accordion-body">
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>DragonPass is a one-stop premium airport service provider where members can enjoy airport lounge access, enjoy dining discounts in participating outlets and reserve Meet & Greet and Limousine service to enjoy a wholesome airport experience. You will be entitled to facilities like refreshments, television, complimentary internet access, shower, flight status updates, etc. Please note that the availability of facilities varies between different lounges.</p></li>
                                        <li class="mb-3"><p>Download the App from the App Store or Google Play Store by searching for “DragonPass”.</p></li>
                                        <li class="mb-3"><p>The self-purchased membership will be activated and ready to use right after the purchase. If you received your DragonPass membership from Issuer that comes along with a Membership No. and Activation Code, you will need to activate your DragonPass membership by following the activation instructions in the App. Your digital membership number can be found in the “My Cards” section or the “Profile” section by clicking on “Memberships”. Present your digital membership using the App on your mobile phone to the lounge staff for simple membership verification. Upon successful verification, you can proceed to enjoy the lounge services.</p></li>
                                        <li class="mb-3"><p>Each lounge has its policies regarding guests and children. Please check the lounge information in the lounge description provided in the App. If the lounge permits guests, please do take note that you will need to have sufficient visits topped up in your membership as one visit will be deducted from it for each guest accessing the lounge.</p></li>
                                        <li class="mb-3"><p>DragonPass membership is not transferable and is only valid up to its date of expiry. The membership may not be used by any person other than the named cardholder.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Cancellation Policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p>Once the order is placed, cancellation is not permitted. Hence, no reward points/cash will be refunded for the same.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">Miscellaneous provisions regarding the booking policy:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Availability:</p><p> Rewards are subject to availability and may vary based on demand, location, and other factors beyond our control. We cannot guarantee availability for specific dates or services.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Changes to Terms:</p> <p>The terms and conditions of the rewards program, including the booking policy, are subject to change at any time without prior notice. Please refer to our website for the latest updates.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">No Cash Value:</p> <p>Reward points have no cash value and cannot be redeemed for cash, transferred, or sold. Points can only be used as stated in the rewards program.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Pricing:</p> <p>All prices, fees, and point values are subject to change based on market conditions and program adjustments. We are not responsible for any changes in value due to external factors.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Third-Party Responsibility:</p><p> ABC Banking Corporation Ltd is not responsible for services provided by third-party vendors, including but not limited to airlines, hotels, car rental agencies, and other partners. Any disputes or issues with third-party services must be addressed with the respective provider.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Modification and Cancellation Restrictions:</p> <p>Rewards made using reward points may be subject to modification and cancellation restrictions. Refunds or point reversals may not be available in certain circumstances.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Tax and Fees Liability:</p> <p>The cardholder is responsible for any taxes, fees, or additional charges associated with rewards bookings, including resort fees, baggage fees, and other ancillary charges not covered by the rewards program.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Warranties:</p> <p>All rewards and services are provided 'as is,' without warranties of any kind, either express or implied, including but not limited to warranties of merchantability, fitness for a particular purpose, or non-infringement.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Fraud and Misuse:</p> <p>We reserve the right to terminate or suspend your rewards program access in cases of fraud, abuse, or misuse of the reward program.</p></li>
                                    </ul>

                                    <p class="heading-semibold text-colour7 mb-2">FAQs:</p>
                                    <ul style="list-style:none" class="px-0">
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Is there a way I can cancel my order or get a refund?</p> <p>Products purchased under this category cannot be cancelled or refunded.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Are the prices mentioned all-inclusive?</p> <p>Prices unless otherwise stated at the time of booking, include all applicable taxes. We reserve the right to amend advertised prices at any time. We also reserve the right to correct errors in both advertised and confirmed prices. Special note: changes and errors sometimes occur. You must check the price of your chosen arrangements at the time of booking.</p></li>
                                        <li class="mb-3"><p class="heading-semibold text-colour7">Is taking insurance mandatory for this category?</p> <p>Insurance Many suppliers/Principals require you to take out travel insurance as a condition of booking with them. In any event, we strongly advise that you take out a policy of insurance in order to cover you and your party against the cost of assistance (including repatriation) in the event of an accident or illness; loss of baggage and money; and other expenses.</p></li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- LOUNGE -->

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
