<%@ Page Title="FAQ" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQs" %>

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
                    <li class="breadcrumb-item active">FAQs</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvFaqs mb-5">
        <div class="container-xl">
            <div class="row">
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
                                    <p class="heading-semibold text-colour7">How do I cancel a flight reservation?</p>
                                    <p class="mb-3">For bookings made through Infinity Rewards, cancellations are allowed if the booking qualifies under refund policy, then points will be refunded back into your account. For any reasons not initiated by you, i.e. flights being grounded/canceled/or any other unforeseen circumstances wherein you are denied travel, applicable reward points will be refunded back into your rewards account within 45 working days.</p>

                                    <p class="heading-semibold text-colour7">Can I book a multi-city trip?</p>
                                    <p class="mb-3">Multi-city booking is not available. In order to book a multi-city travel, you will have to book individual sectors separately.</p>

                                    <p class="heading-semibold text-colour7">Can I do an online check-in?</p>
                                    <p class="mb-3">Online check-in is not always offered by every airline on its website. If the website does permit online check-in, you may do so by using your Airline Booking Reference number.</p>

                                    <p class="heading-semibold text-colour7">Can I enter my Frequent Flyer number at the time of booking?</p>
                                    <p class="mb-3">Currently, we do not accept the Frequent Flyer number. You can provide your Frequent Flyer number at the airline counter at the time of check-in.</p>

                                    <p class="heading-semibold text-colour7">I’ve booked my tickets but now need to add my child’s tickets to my booking. How do I proceed for this?</p>
                                    <p class="mb-3">You would need to contact your airline directly to book tickets for your child.</p>

                                    <p class="heading-semibold text-colour7">I did not receive an e-mail confirmation. What do I do?</p>
                                    <p class="mb-3">If you do not receive a confirmation e-mail from us, there is a possibility that an improper e-mail address was registered in our records, or your Internet Service Provider blocked the e-mail as a ‘spam’. In this case, we suggest you check your email and spam folder. You can also contact the bank’s Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p>

                                    <p class="heading-semibold text-colour7">What are the payment options when booking flights?</p>
                                    <p class="mb-3">You can pay with Infinity Rewards Points.</p>
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
                                    <p class="heading-semibold text-colour7">How do I cancel a flight reservation?</p>
                                    <p class="mb-3">For bookings made through Infinity Rewards, cancellations are allowed if the booking qualifies under refund policy, then points will be refunded back into your account. For any reasons not initiated by you, i.e. flights being grounded/canceled/or any other unforeseen circumstances wherein you are denied travel, applicable reward points will be refunded back into your rewards account within 45 working days.</p>

                                    <p class="heading-semibold text-colour7">Can I book a multi-city trip?</p>
                                    <p class="mb-3">Multi-city booking is not available. In order to book a multi-city travel, you will have to book individual sectors separately.</p>

                                    <p class="heading-semibold text-colour7">Can I do an online check-in?</p>
                                    <p class="mb-3">Online check-in is not always offered by every airline on its website. If the website does permit online check-in, you may do so by using your Airline Booking Reference number.</p>

                                    <p class="heading-semibold text-colour7">Can I enter my Frequent Flyer number at the time of booking?</p>
                                    <p class="mb-3">Currently, we do not accept the Frequent Flyer number. You can provide your Frequent Flyer number at the airline counter at the time of check-in.</p>

                                    <p class="heading-semibold text-colour7">I’ve booked my tickets but now need to add my child’s tickets to my booking. How do I proceed for this?</p>
                                    <p class="mb-3">You would need to contact your airline directly to book tickets for your child.</p>

                                    <p class="heading-semibold text-colour7">I did not receive an e-mail confirmation. What do I do?</p>
                                    <p class="mb-3">If you do not receive a confirmation e-mail from us, there is a possibility that an improper e-mail address was registered in our records, or your Internet Service Provider blocked the e-mail as a ‘spam’. In this case, we suggest you check your email and spam folder. You can also contact the bank’s Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p>

                                    <p class="heading-semibold text-colour7">What are the payment options when booking flights?</p>
                                    <p class="mb-3">You can pay with Infinity Rewards Points.</p>

                                    <p class="heading-semibold text-colour7">How do I cancel my hotel booking?</p>
                                    <p class="mb-3">Customers can cancel their hotel reservations up to 24-48 hours before check-in. Customers can cancel their hotel reservations by emailing Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> prior to the 24–48-hour check-in period. Once a hotel reservation has been made, no changes will be accepted.</p>

                                    <p class="heading-semibold text-colour7">Can I change the dates of my hotel booking?</p>
                                    <p class="mb-3">Once the hotel is booked, the booking cannot be amended. For amendments, the current booking has to be canceled, and a new booking will have to be made.</p>

                                    <p class="heading-semibold text-colour7">Can I book a room which more than two adults will occupy?</p>
                                    <p class="mb-3">Most hotels allow additional guests in a room for an extra charge, but the number of occupants should not exceed the maximum number of guests allowed per room by the hotel. You would need to directly check with the hotel for such bookings, as it varies according to the terms and conditions of the hotel.</p>

                                    <p class="heading-semibold text-colour7">Our children will be traveling with us; will there be any hotel charge for them as well?</p>
                                    <p class="mb-3">When making your booking, select the number of children traveling with you from the 'Children' drop-down box. If you select one child, our search will give you the price of a Double Room with a child, not including an extra bed. If you want an extra bed in the room, you need to increase the number of passengers in your search.</p>

                                    <p class="heading-semibold text-colour7">Can I request a room preference such as smoking/wheelchair friendly etc.?</p>
                                    <p class="mb-3">Yes, you can. Although, you will have to check with the hotel directly for such requests and book online.</p>

                                    <p class="heading-semibold text-colour7">I did not get an email confirmation. What do I do?</p>
                                    <p class="mb-3">
                                        If you do not receive a confirmation e-mail from us, there is a possibility that an improper e-mail address was registered in our records, or your Internet Service Provider blocked the e-mail as a ‘spam’ in which case we suggest you check the address and your spam folder. You can also contact us by sending an e-mail at infinity@abcbanking.mu or calling ABC Banking Corporation’s Customer Support. It is important at the time of contacting us that you convey information on:
                                    </p>
                                    <ul>
                                        <li>Name against which reservation was made</li>
                                        <li>Location (city) and name of the hotel</li>
                                        <li>Dates of check-in / check-out</li>
                                        <li>CIF Number</li>
                                    </ul>

                                    <p class="heading-semibold text-colour7">I am arriving late; will the hotel hold my room until I arrive?</p>
                                    <p class="mb-3">Yes, the hotel will hold your room booking until 7 am the day after your planned arrival date, as your reservation is a confirmed booking. However, please check with the hotel for details.</p>

                                    <p class="heading-semibold text-colour7">I am arriving earlier than planned; will the hotel accommodate me?</p>
                                    <p class="mb-3">In such cases, we suggest you contact the hotel in advance to check for accommodation, as terms and conditions vary with different hotels.</p>

                                    <p class="heading-semibold text-colour7">How can I get a receipt or invoice for my hotel booking?</p>
                                    <p class="mb-3">You can view all your upcoming and completed trips by selecting the ‘Manage Bookings’ section. Further, you can view/print your receipt by selecting the Booking Reference number.</p>

                                    <p class="heading-semibold text-colour7">What is the difference between a Double and a Twin Room?</p>
                                    <p class="mb-3">A Double Room has 1 King-sized bed, whereas a Twin Room has 2 Single or Queen-sized beds.</p>

                                    <p class="heading-semibold text-colour7">Do I need to reconfirm my hotel booking?</p>
                                    <p class="mb-3">No, this is not necessary. However, if you wish, you may contact the hotel directly.</p>

                                    <p class="heading-semibold text-colour7">I need to avail an early check-in/late check-out. Can this be done?</p>
                                    <p class="mb-3">This depends completely on the hotel’s room availability on that particular date. Infinity Rewards cannot guarantee anything in this regard.</p>

                                    <p class="heading-semibold text-colour7">Are meals/breakfast included in the hotels booked?</p>
                                    <p class="mb-3">Not all hotels include complimentary breakfast. Please ensure this is checked on the program website under the respective hotel’s ‘View Detail’ section, before redeeming the hotel.</p>

                                    <p class="heading-semibold text-colour7">Can I make a hotel booking for today’s check-in?</p>
                                    <p class="mb-3">Sorry, this is not possible. Hotel reservations have to be booked a minimum of 3 days in advance.</p>

                                    <p class="heading-semibold text-colour7">Can I request connected rooms if I am booking 2 rooms, or a smoking-room preference etc.?</p>
                                    <p class="mb-3">Most of the hotels are booked directly through international suppliers. We can add these requests as a comment to the hotel; however, Infinity Rewards does not guarantee the confirmation of the same. These requests would have to be made directly at the hotel at the time of check-in.</p>

                                    <p class="heading-semibold text-colour7">What are the payment options when booking hotels?</p>
                                    <p class="mb-3">You can use reward points to book hotels.</p>
                                </div>

                            </div>
                        </div>
                        <!-- HOTELS -->

                        <!-- CAR RENTAL -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse3">
                                        Car Rental
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse3" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Do I get an immediate booking confirmation?</p>
                                    <p class="mb-3">Once car redemption is made, the confirmation is not immediate. It takes a minimum of 24 - 48 hours for confirmation. For any queries regarding confirmation, you can contact us via e-mail at Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>. In case the booking does not get confirmed, the points/cash will be refunded back into your account and you can repeat your attempt to book online.</p>

                                    <p class="heading-semibold text-colour7">Can I make an amendment to my booking?</p>
                                    <p class="mb-3">Amendments are possible for most bookings; however, the request needs to be received at least 48 hours prior to the date of booking. Confirmation would depend on the service provider's availability. In some cases, amendments may not be possible, as per the car rental policy.</p>

                                    <p class="heading-semibold text-colour7">Will there be any extra charges if I wish to make an amendment to my booking?</p>
                                    <p class="mb-3">Extra amendment surcharges would be incurred if there is a change in the duration, location, or the type of car.</p>

                                    <p class="heading-semibold text-colour7">Can I cancel my booking?</p>
                                    <p class="mb-3">For bookings made through Infinity Rewards, no cancellations are allowed if initiated voluntarily by you. Hence, no points/cash will be refunded back to your account. However, if the reason for cancellation is from the service provider/supplier’s end, the points will be duly credited into your account within 7 business days.</p>

                                    <p class="heading-semibold text-colour7">How old do I have to be to rent a car?</p>
                                    <p class="mb-3">For most car hire companies, the age requirement is between 21 and 70 years. If the driver is under 25 or over 70 years of age, you might have to pay an additional fee. (Charges may vary depending on city/country and type of car.)</p>

                                    <p class="heading-semibold text-colour7">Is it possible to rent a car for another person through my account?</p>
                                    <p class="mb-3">Yes, as long as they meet the stipulated requirements. You would need to fill in their details while making the reservation.</p>

                                    <p class="heading-semibold text-colour7">Does my car redemption include all charges or is there anything extra I have to pay?</p>
                                    <p class="mb-3">Most of the car rental services include in their price - Theft Protection, Collision Damage Waiver (CDW), local taxes, airport surcharges, and any road fees. Although, you would be responsible for any ‘extra’ charges at the time of car pick-up, fees for a young/additional driver, one-way fees. The same will be explained to you, in addition to ways to reduce costs like carrying child seats, GPS, before you make your car booking. You may read more about this in the Terms and Conditions section of the car rental service you are booking with.</p>
                                </div>
                            </div>
                        </div>
                        <!-- CAR RENTAL -->

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
                                    <p class="heading-semibold text-colour7">How can I dispute my Infinity Rewards transactions/purchases?</p>
                                    <p class="mb-3">You can dispute the transaction by emailing Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>, which will log the complaint.</p>

                                    <p class="heading-semibold text-colour7">How many days will it take to get my merchandise delivered?</p>
                                    <p class="mb-3">Your merchandise will be delivered within 6-10 working days (subject to stock availability).</p>

                                    <p class="heading-semibold text-colour7">What should I do if the merchandise doesn’t reach me within 7 working days?</p>
                                    <p class="mb-3">Please email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> to check on the status of your order.</p>

                                    <p class="heading-semibold text-colour7">What if I want to exchange/upgrade my order?</p>
                                    <p class="mb-3">Once an order has been placed, it cannot be exchanged or upgraded.</p>

                                    <p class="heading-semibold text-colour7">What should I do if the delivered item is wrong or damaged?</p>
                                    <p class="mb-3">In the unfortunate event that the order is incorrect or the item(s) is damaged, please email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> within 24 hours of delivery. Replacements or refunds cannot be processed if we receive the request after 24 hours of delivery.</p>

                                    <p class="heading-semibold text-colour7">How do I look for a product on the Infinity Rewards platform?</p>
                                    <p class="mb-3">Navigation is easy. Select the category or use the search engine to find the product you are looking for.</p>

                                    <p class="heading-semibold text-colour7">What are my options for paying for an order?</p>
                                    <p class="mb-3">You may redeem your reward points.</p>

                                    <p class="heading-semibold text-colour7">I have a missing item in my order</p>
                                    <p class="mb-3">For any missing items in your order, please email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> with your order number and the missing item's name and number. Our team will look into your request and will get back to you in 2-3 business days.</p>

                                    <p class="heading-semibold text-colour7">How can I undo an order cancellation?</p>
                                    <p class="mb-3">Cancellation requests are final. To receive the items, simply make a new order.</p>

                                    <p class="heading-semibold text-colour7">How will I know if an item is out of stock?</p>
                                    <p class="mb-3">Any item that is sold out will be mentioned "Out of Stock" on the product page. In rare cases, a product in your cart may become out of stock as you're in the check-out process and a notification will pop-up during the process.</p>

                                    <p class="heading-semibold text-colour7">Can I change or amend the items in my order once it has been placed?</p>
                                    <p class="mb-3">Currently, this service is not available. To change or add an item, the order has to be canceled and a new order has to be made.</p>

                                    <p class="heading-semibold text-colour7">Can I ship an order to multiple addresses?</p>
                                    <p class="mb-3">No, we can currently only ship to one address per order.</p>

                                    <p class="heading-semibold text-colour7">My payments (Points) are debited; however, the order was not processed?</p>
                                    <p class="mb-3">In case the payments are debited and the order was not processed, you can email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p>
                                </div>

                            </div>
                        </div>
                        <!-- SHOP -->

                        <!-- GIFT VOUCHERS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse5">
                                        Gift Vouchers
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse5" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">How many days will it take to get my Gift Voucher delivered?</p>
                                    <p class="mb-3">Your Gift Voucher details will be emailed within 24 hours to your registered email address (subject to the availability of stock). More details can be found on the brand's terms and conditions in the Infinity Rewards App.</p>

                                    <p class="heading-semibold text-colour7">What if I want to exchange my Gift Voucher?</p>
                                    <p class="mb-3">Once an order has been placed, it cannot be exchanged.</p>

                                    <p class="heading-semibold text-colour7">What do I do if I have not received the Gift Voucher within 24 hours?</p>
                                    <p class="mb-3">You can email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a> mentioning the details and your order ID.</p>

                                    <p class="heading-semibold text-colour7">What if my Gift Voucher is lost, stolen, damaged, or destroyed?</p>
                                    <p class="mb-3">Infinity Rewards will not be responsible if a Gift Voucher is lost, stolen, damaged, or destroyed, and no replacement or refund of points will be provided in any circumstance.</p>

                                    <p class="heading-semibold text-colour7">Is there a way I can cancel my order?</p>
                                    <p class="mb-3">Once the Gift Voucher is purchased, voluntary cancellation is not permitted. Hence, reward points will not be refunded for the cancellation.</p>
                                </div>
                            </div>
                        </div>
                        <!-- GIFT VOUCHERS  -->

                        <!-- LOUNGES -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse6">
                                        Lounges
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse6" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">What is an Airport Lounge?</p>
                                    <p class="mb-3">An airport lounge is a facility operated at many airports for selected passengers, offering comforts beyond those afforded in the airport terminal itself, such as more comfortable seating, quieter environments, and often better access to customer service representatives. Other accommodations may include private meeting rooms, telephones, wireless internet access, and other business services, along with provisions to enhance passenger comfort, such as free beverages, snacks/food, magazines, and showers.</p>

                                    <p class="heading-semibold text-colour7">Can I cancel my Lounge purchase?</p>
                                    <p class="mb-3">Once purchased, the lounge voucher is non-refundable.</p>

                                    <p class="heading-semibold text-colour7">How do I access the airport lounge?</p>
                                    <p class="mb-3">You will receive a voucher code in your registered email address. Present this voucher code at the lounge reception to access the lounge.</p>

                                    <p class="heading-semibold text-colour7">What facilities can I expect to find in a lounge?</p>
                                    <p class="mb-3">You will be entitled to facilities like refreshments, television, complimentary internet access, shower, and flight status updates, etc. Please note that the availability of facilities varies between different lounges and refer to the terms and conditions available in the email communication.</p>
                                </div>

                            </div>
                        </div>
                        <!-- LOUNGES -->

                        <!-- EXPERIENCES -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse7">
                                        Experiences
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse7" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Is there a way I can cancel my order or get a refund?</p>
                                    <p class="mb-3">Please refer to the terms and conditions mentioned under each experience as this may vary accordingly. In case of bookings eligible for cancellation, please email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p>

                                    <p class="heading-semibold text-colour7">Can I amend my booking?</p>
                                    <p class="mb-3">Please refer to the terms and conditions mentioned under each experience as this may vary accordingly. In case of bookings eligible for amendments, email Customer Support at <a class="link1" href="mailto:infinity@abcbanking.mu">infinity@abcbanking.mu</a>.</p>

                                    <p class="heading-semibold text-colour7">Is taking insurance mandatory for this category?</p>
                                    <p class="mb-3">Many experiences, as stated in the booking terms and conditions, may require you to take out travel insurance as a condition of booking with them. In any event, we strongly advise that you take out a policy of insurance to cover you and your party against the cost of assistance (including repatriation) in the event of an accident or illness, loss of baggage and money, and other expenses.</p>

                                    <p class="heading-semibold text-colour7">Are there any restrictions or eligibility criteria for booking Experiences?</p>
                                    <p class="mb-3">Some experiences may have specific requirements or restrictions, such as age limits, physical fitness levels, or group sizes. Please review the details of each experience to ensure it aligns with your requirements.</p>
                                </div>

                            </div>
                        </div>
                        <!-- EXPERIENCES -->
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

