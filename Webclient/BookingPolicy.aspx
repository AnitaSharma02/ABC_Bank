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
                    <li class="mr-3">
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
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3" type="button" data-toggle="collapse"
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
                                    <ul>
                                        <li class="mb-3">The displayed total price includes taxes and any applicable airfare. It does not include any applicable baggage fees, airport taxes, or other costs that might need to be paid to the airline or at particular airports. Baggage is not always included in the booked tickets by all airlines or by all fare rules. Additionally, rules might change at any time without warning. To find out more about baggage restrictions, it is best to get in touch with the airline directly or visit its website.</li>
                                        <li class="mb-3">Before your reservation is confirmed, you must pay the full cost of the ticket in points.</li>
                                        <li class="mb-3">There will be no refund of points for 'no-shows' (i.e. flights missed for any reason whatsoever) or any partially unused flights.</li>
                                        <li class="mb-3">For any information regarding your Frequent Flyer miles, please contact the airline directly.</li>
                                        <li class="mb-3">Please make sure you have a passport that is valid for at least six months, a valid visa, and immigration clearance.</li>
                                        <li class="mb-3">An infant must be under 24 months old for the duration of the entire itinerary you are booking to qualify for infant fares. This covers both outbound and incoming trips. For the return trip, a separate reservation using a kid fee is required if the infant is 24 months or older.</li>
                                        <li class="mb-3">Infants must be accompanied by an adult at least 18 years of age. All bookings for flights are powered by GIIFT Management DMCC. However, Gift Management DMCC is not responsible for any schedule change by the airline after issuance of the ticket but will inform you of the same if informed by the airline. It is advisable to reconfirm your flight timings 24 hours before your flight departure.</li>
                                        <li class="mb-3">Infinity Rewards and GIIFT reserve the right to alter any and all fees from time to time, without any prior notice.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Flight Amendments:</p>
                                    <ul>
                                        <li class="mb-3">For any amendments or queries you may email <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>. In some cases, though, you will need to contact the airline directly.</li>
                                        <li class="mb-3">Every booking made on the Infinity Rewards platform is subject to amendment charges levied by the airline, which may vary by flight and booking class.</li>
                                        <li class="mb-3">If you amend your booking, you will be charged the difference in fare, if any, and applicable when the amendment is made. However, if the new fare is lower than the original fare, the difference in the fare amount will not be refunded. The rebooking charges as applicable will be collected and charged to your Credit/Debit Card or reward points.</li>
                                        <li class="mb-3">In addition to the airline's amendment charges, Infinity Rewards charges an amendment handling fee of USD 10 equivalent points per passenger. We will collect these charges from you when we make the changes to your travel plans. We will also collect the difference in fare, if any is applicable when the amendment is made.</li>
                                        <li class="mb-3">Depending on the airline policy, some booked fares may not be amended.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Flight Cancellation and Refund Policy:</p>
                                    <ul>
                                        <li class="mb-3">For bookings made through the Infinity Rewards platform, cancellations are allowed if the booking qualifies under the refund policy, and then points will be refunded back into your account. For any reasons not initiated by you, i.e. flights being grounded/cancelled/or any other unforeseen circumstances wherein you are denied travel, applicable reward points will be refunded back into your account within 45 working days. This again would be at the discretion of Infinity Rewards.</li>
                                        <li class="mb-3">For bookings made through the Infinity Rewards platform, no cancellations are allowed. Hence no points will be refunded back into your account.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- FLIGHTS -->

                        <!-- HOTELS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse"
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
                                    <p class="heading6 mb-2">Hotel Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">The regular check-in time is after 14:00 hours (local time) and check-out time is noon. This could vary according to season and/or city. For precise information, please contact the hotel.</li>
                                        <li class="mb-3">The hotel's ability to accommodate an early check-in or a late check-out completely depends on the availability of rooms on that specific date. Nothing in this regard may be guaranteed by Infinity Rewards. Please contact the hotel for more information.</li>
                                        <li class="mb-3">The meal plan for each hotel stay must be agreed upon and confirmed with the hotel. Infinity Rewards is not liable for any discrepancy on this matter.</li>
                                        <li class="mb-3">Hotel policies determine the maximum occupancy for each room.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Hotel Amendments:</p>
                                    <ul>
                                        <li class="mb-3">If the hotel booking is within the refund policy, the customer will receive a full return of Points, and all cancellation policies are stated before confirming the booking. Once the hotel is booked, the booking cannot be amended. For amendments, the current booking must be cancelled, and a new booking will have to be made.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Hotel Cancellation Policy:</p>
                                    <ul>
                                        <li class="mb-3">The cancellation and refund policy shall be the same as for the hotel stay paid in points, i.e. if the hotel has a no cancellation and no refund policy, then Infinity Rewards shall not be eligible for a refund of the points paid for the stay.</li>
                                        <li class="mb-3">If the hotel allows for the cancellation and refund of a hotel stay, customers must send the hotel’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- HOTELS -->

                        <!-- CAR RENTALS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse"
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
                                    <p class="heading6 mb-2">Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">Once car redemption is done by you, the confirmation is not immediate. It takes a minimum of 24–48 hours for confirmation. For any queries regarding the confirmation, you may email us at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</li>
                                        <li class="mb-3">Please see the Service Provider’s full terms and conditions below, which include the full name and company registered address of your Service Provider, information on and fees of extra products and services purchasable at the counter or based on your use of the rental, such as driving cross border and, if any, pick-up and drop-off grace periods, cancellation and refund policies.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Cancellation Policy:</p>
                                    <ul>
                                        <li class="mb-3">If the Service Provider allows for the cancellation and refund of a booking, the Customers must send the Service Provider’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation. We shall then proceed with the refund of points on your account within 3 business days.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Experience:</p>
                                    <ul>
                                        <li class="mb-3">Please see the Service Provider’s full terms and conditions, which include the full name and company registered address of your Service Provider, information on and fees of extra products and services purchasable at the counter, cancellation and refund policies.</li>
                                        <li class="mb-3">If the Service Provider allows for the cancellation and refund of a booking, the Customers must send the Service Provider’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation. We shall then proceed with the refund of points on your account within 3 business days.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- CAR RENTALS -->

                        <!-- SHOP -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse5">
                                        Shop
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse5" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading6 mb-2">Shop Booking Policy:</p>
                                    <ul>
                                        <li class="mb-3">Your merchandise will be delivered within 2-7 working days (subject to availability of stock).</li>
                                        <li class="mb-3">It takes up to 24 hours for confirmation of order upon payment. For any queries regarding the confirmation, you may email us at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</li>
                                        <li class="mb-3">Infinity Rewards shop products are delivered only within Mauritius.</li>
                                        <li class="mb-3">Please see the Merchant’s and Courrier Service’s full terms and conditions, which include the full name and company registered address of the Merchant and Courrier Service, shipping conditions, return, cancellation, and refund policies.</li>
                                        <li class="mb-3">If the Merchant allows for the cancellation and/or refund of an order, the Customers must send the Merchant’s confirmation of refund to <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of receipt of such confirmation. We shall then proceed with the refund of points on your account within 3 business days.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- SHOP -->

                        <!-- GIFT VOUCHER -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse6">
                                        Gift Cards / Vouchers
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse6" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <ul>
                                        <li class="mb-3">Gift Voucher details will be emailed within 24 hours to your registered email address (subject to availability of stock). More details can be found in that email.</li>
                                        <li class="mb-3">Gift vouchers have an expiry date, and the receiver must utilize the vouchers within this period. No extension of the expiry date will be allowed.</li>
                                        <li class="mb-3">Gift vouchers can only be used once for a purchase. Any unused amount at the time of making the purchase will be forfeited. If the transaction amount exceeds the value of the Gift Voucher, the balance amount can be paid through other payment methods.</li>
                                        <li class="mb-3">Gift vouchers do not accept cashbacks, refunds, or returns.</li>
                                        <li class="mb-3">The merchant retains the right to reject any voucher that has been tampered with or found to be unacceptable in any way.</li>
                                        <li class="mb-3">If you don’t receive an email, you can email us at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> mentioning the order details and your Rewards ID.</li>
                                        <li class="mb-3">Infinity Rewards will not be responsible if a Gift Voucher is lost, stolen, damaged, or destroyed, and no replacement or refund of Union Rewards Points/Cash will be provided under any circumstances.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Exchange Policy:</p>
                                    <ul>
                                        <li class="mb-3">Once the order is placed, exchange is not permitted.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Cancellation Policy:</p>
                                    <ul>
                                        <li class="mb-3">Once the order is placed, cancellation is not permitted. Hence, no reward points will be refunded for the same.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- GIFT VOUCHER -->

                        <!-- LOUNGE -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse7">
                                        Lounge
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse7" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <ul>
                                        <li class="mb-3">DragonPass is a one-stop premium airport service provider where members can enjoy airport lounge access, enjoy dining discounts in participating outlets and reserve Meet & Greet and Limousine service to enjoy a wholesome airport experience. You will be entitled to facilities like refreshments, television, complimentary internet access, shower, flight status updates, etc. Please note that the availability of facilities varies between different lounges.</li>
                                        <li class="mb-3">Download the App from the App Store or Google Play Store by searching for “DragonPass”.</li>
                                        <li class="mb-3">The self-purchased membership will be activated and ready to use right after the purchase. If you received your DragonPass membership from Issuer that comes along with a Membership No. and Activation Code, you will need to activate your DragonPass membership by following the activation instructions in the App. Your digital membership number can be found in the “My Cards” section or the “Profile” section by clicking on “Memberships”. Present your digital membership using the App on your mobile phone to the lounge staff for simple membership verification. Upon successful verification, you can proceed to enjoy the lounge services.</li>
                                        <li class="mb-3">Each lounge has its policies regarding guests and children. Please check the lounge information in the lounge description provided in the App. If the lounge permits guests, please do take note that you will need to have sufficient visits topped up in your membership as one visit will be deducted from it for each guest accessing the lounge.</li>
                                        <li class="mb-3">DragonPass membership is not transferable and is only valid up to its date of expiry. The membership may not be used by any person other than the named cardholder.</li>
                                    </ul>

                                    <p class="heading6 mb-2">Cancellation Policy:</p>
                                    <ul>
                                        <li class="mb-3">Once the order is placed, cancellation is not permitted. Hence, no reward points/cash will be refunded for the same.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- LOUNGE -->

                        <!-- MISC -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="btn btn-block text-left p-3 collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse8">
                                        Miscellaneous provisions regarding the booking policy
                                         <span class="arrow-icon">
                                             <i class="fa fa-caret-up"></i>
                                         </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse8" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <ul>
                                        <li class="mb-3"><strong>Availability:</strong> Rewards are subject to availability and may vary based on demand, location, and other factors beyond our control. We cannot guarantee availability for specific dates or services.</li>
                                        <li class="mb-3"><strong>Changes to Terms:</strong> The terms and conditions of the rewards program, including the booking policy, are subject to change at any time without prior notice. Please refer to our website for the latest updates.</li>
                                        <li class="mb-3"><strong>No Cash Value:</strong> Reward points have no cash value and cannot be redeemed for cash, transferred, or sold. Points can only be used as stated in the rewards program.</li>
                                        <li class="mb-3"><strong>Pricing:</strong> All prices, fees, and point values are subject to change based on market conditions and program adjustments. We are not responsible for any changes in value due to external factors.</li>
                                        <li class="mb-3"><strong>Third-Party Responsibility:</strong> ABC Banking Corporation Ltd is not responsible for services provided by third-party vendors, including but not limited to airlines, hotels, car rental agencies, and other partners. Any disputes or issues with third-party services must be addressed with the respective provider.</li>
                                        <li class="mb-3"><strong>Modification and Cancellation Restrictions:</strong> Rewards made using reward points may be subject to modification and cancellation restrictions. Refunds or point reversals may not be available in certain circumstances.</li>
                                        <li class="mb-3"><strong>Tax and Fees Liability:</strong> The cardholder is responsible for any taxes, fees, or additional charges associated with rewards bookings, including resort fees, baggage fees, and other ancillary charges not covered by the rewards program.</li>
                                        <li class="mb-3"><strong>Warranties:</strong> All rewards and services are provided 'as is,' without warranties of any kind, either express or implied, including but not limited to warranties of merchantability, fitness for a particular purpose, or non-infringement.</li>
                                        <li class="mb-3"><strong>Fraud and Misuse:</strong> We reserve the right to terminate or suspend your rewards program access in cases of fraud, abuse, or misuse of the reward program.</li>
                                    </ul>

                                    <p class="heading6 mb-2">FAQs:</p>
                                    <ul>
                                        <li class="mb-3"><strong>Is there a way I can cancel my order or get a refund?</strong> Products purchased under this category cannot be cancelled or refunded.</li>
                                        <li class="mb-3"><strong>Are the prices mentioned all-inclusive?</strong> Prices unless otherwise stated at the time of booking, include all applicable taxes. We reserve the right to amend advertised prices at any time. We also reserve the right to correct errors in both advertised and confirmed prices. Special note: changes and errors sometimes occur. You must check the price of your chosen arrangements at the time of booking.</li>
                                        <li class="mb-3"><strong>Is taking insurance mandatory for this category?</strong> Insurance Many suppliers/Principals require you to take out travel insurance as a condition of booking with them. In any event, we strongly advise that you take out a policy of insurance in order to cover you and your party against the cost of assistance (including repatriation) in the event of an accident or illness; loss of baggage and money; and other expenses.</li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <!-- MISC -->

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
