<%@ Page Title="FAQ" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="FAQ.aspx.cs" Inherits="FAQs" %>

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
                    <li class="breadcrumb-item active" data-i18n="navigation-faqs">FAQs</li>
                </ul>
            </nav>
        </div>
    </div>

    <div class="dvFaqs mb-5">
        <div class="container-xl">
            <div class="row">
                <div class="col-12">
                    <div class="dvCommonAccordion accordion" id="static-accordion">
                        <!-- GENERAL -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold" type="button" data-toggle="collapse"
                                        data-target="#collapse1">
                                        General FAQs
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse1" class="collapse show" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">What is NIC Express Reward?</p>
                                    <p class="mb-3">NIC Express Reward is a comprehensive Loyalty Program from NIC Express Reward, that caters to all your travel & lifestyle needs. You can redeem your NPoints over 900 airlines, 450,000 hotels worldwide, car rentals in over 150 countries and a variety of products from leading brands available on NIC Express Reward Shop.</p>

                                    <p class="heading-semibold text-colour7">Who is NIC Express Reward offered to?</p>
                                    <p class="mb-3">The NIC Express Reward Program is offered to (add type of customers). </p>

                                    <p class="heading-semibold text-colour7">How can I access and activate my membership on the NIC Express Reward website?</p>
                                    <p class="mb-3">If you are a NIC Express Reward customer, you are automatically entitled to access the NIC Express Reward program. Simply, log on to (add website url) to activate your membership by using the following information.</p>
                                    <ul>
                                        <li class="mb-3 mx-3">NIC Express Reward Customer Identification number </li>
                                        <li class="mb-3 mx-3">Your Email Address</li>
                                        <li class="mb-3 mx-3">Your Mobile Number </li>
                                    </ul>
                                    <p class="mb-3">The above information should be identical to the bank records, as provided by you while opening your bank account. If your details do not match, NIC Express Reward will not allow you to access your account. To update your information with us call +977-1-5970101 or visit our nearest branch.</p>
                                    <p class="mb-3">Upon registration a SMS & Email will be sent to you with your access password, which you can use to log into your account. </p>

                                    <p class="heading-semibold text-colour7">How do I earn NIC Express Reward NPoints?</p>
                                    <p class="mb-3">You can earn NIC Express Reward NPoints by (add program earning NPoints details). </p>

                                    <p class="heading-semibold text-colour7">How can I get Bonus or Accelerated NPoints?</p>
                                    <p class="mb-3">You can get bonus or accelerated NIC Express Reward NPoints by (add program bonus NPoints details).</p>

                                    <p class="heading-semibold text-colour7">Is this a cash back program?</p>
                                    <p class="mb-3">NIC Express Reward is not a cash back program. It rewards you for using (bank products / program specifics). </p>

                                    <p class="heading-semibold text-colour7">How do I redeem NIC Express Reward NPoints?</p>
                                    <p class="mb-3">By logging in to NIC Express Reward portal (add website url), you can redeem your NPoints for Free Flights, Free Hotel Stays, Free Car Rentals, Utility, Gift Cards and NPoints Exchange. </p>

                                    <p class="heading-semibold text-colour7">How is NIC Express Reward different from any other 'airline NPoints' program?</p>
                                    <p class="mb-3">NIC Express Reward is a Bank Rewards Program that caters to all your travel needs. You earn NPoints for transactions performed on participating NIC Express Reward products and you can then redeem your NPoints on more than 900 airlines and 450,000 major hotels worldwide. You can redeem rewards on booking your flight tickets online directly from more than 900 airlines around the world.</p>

                                    <p class="heading-semibold text-colour7">How long is my NIC Express Reward Membership valid?</p>
                                    <p class="mb-3">You can access the NIC Express Reward portal, as long as you (add the required qualification for the program). </p>

                                    <p class="heading-semibold text-colour7">What is the validity of my NIC Express Reward NPoints?</p>
                                    <p class="mb-3">Your NIC Express Reward NPoints are valid for (period). </p>

                                    <p class="heading-semibold text-colour7">Can I use my NIC Express Reward online username and password to transact with NIC Express Reward Portal?</p>
                                    <p class="mb-3">No, the NIC Express Reward portal (add website url) does not contain any information that you may have provided to NIC Express Reward online portal and NIC Express Reward portal (add website url) is an independent property managed by 3rd party Giift (entity name to be entered based on contract). </p>

                                    <p class="heading-semibold text-colour7">What can I do with my accrued NPoints?</p>
                                    <p class="mb-3">You can redeem your NPoints for Flights, Hotels, Car rentals, Utility, Gift Cards and NPoints Exchange on NIC Express Reward Portal (add website url). </p>

                                    <p class="heading-semibold text-colour7">Do I need to register/log-in separately on NIC Express Reward website?</p>
                                    <p class="mb-3">Yes, the registration/log-in process on NIC Express Reward website is independent of your access to NIC Express Reward 's online banking. Registration / log-in on NIC Express Reward Website is a simple process where you just need to have your NIC Express Reward membership number to be able to transact on (add website url). Registration with NIC Express Reward Website allows you to redeem your NIC Express Reward NPoints. </p>

                                    <p class="heading-semibold text-colour7">Do I have to register online on (add website url) in order to use NIC Express Reward Portal for NPoints accrual, redemption, and cash transactions? </p>
                                    <p class="mb-3">Registration is optional; you can access all the features and functionalities of the website without registration.  </p>

                                    <p class="heading-semibold text-colour7">Is my e-mail address required for my NIC Express Reward account?</p>
                                    <p class="mb-3">It is mandatory to provide your email address and mobile number during the booking process. This helps us to send you the itinerary/ receipts of your booking to your e-mail address. We also communicate the same to the airline, hotels or the respective service provider, in case of unforeseen circumstances like rescheduling of flight or cancellation. </p>

                                    <p class="heading-semibold text-colour7">On what airlines, hotels and products can I redeem NIC Express Reward NPoints?</p>
                                    <p class="mb-3">You can redeem your NPoints for booking with more than 900 airlines, 450,000 hotels worldwide and car rental in over 150 countries, and shopping online from a range of products through NIC Express Reward website. </p>

                                    <p class="heading-semibold text-colour7">When can I start redeeming my NIC Express Reward?</p>
                                    <p class="mb-3">You can start redeeming your NPoints as soon as you have (add method) and have accumulated enough NIC Express Reward NPoints for redemption. </p>

                                    <p class="heading-semibold text-colour7">Where can I find my NIC Express Reward Summary?</p>
                                    <p class="mb-3">You can view your NIC Express Reward account summary in the My Account section on the NIC Express Reward website or you may call NIC Express Reward's 24 hour Call Centre on +977-1-5970101. </p>

                                    <p class="heading-semibold text-colour7">What is Redemption Booking?</p>
                                    <p class="mb-3">When you book Flights, Hotels, Cars using your NIC Express Reward NPoints, the booking is termed as a Redemption Booking. </p>

                                    <p class="heading-semibold text-colour7">Do I have to pay anything for booking the redemption tickets and hotel bookings?</p>
                                    <p class="mb-3">There are no separate redemption charges on (add website url). </p>

                                    <p class="heading-semibold text-colour7">Can I redeem my NIC Express Reward NPoints for ticket and hotel bookings for my family or friends?</p>
                                    <p class="mb-3">Yes, you can redeem your NIC Express Reward NPoints for family members and friends. </p>

                                    <p class="heading-semibold text-colour7">How do I know my airline or hotel booking or product purchase was confirmed?</p>
                                    <p class="mb-3">(Add confirmation notification process) </p>

                                    <p class="heading-semibold text-colour7">What if I don't get a confirmation at the time of booking?</p>
                                    <p class="mb-3">If you do not receive a confirmation through (Add confirmation notification process) call us on Call Centre +977-1-5970101 and we'll send you your confirmation details.</p>

                                </div>
                            </div>
                        </div>
                        <!-- GENERAL -->

                        <!-- FLIGHTS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold" type="button" data-toggle="collapse"
                                        data-target="#collapse2">
                                        Flights
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse2" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">What's an e–ticket?</p>
                                    <p class="mb-3">An e–ticket (electronic ticket) is a paperless electronic document with a unique confirmation number that neatly replaces the hassles of a paper ticket. When you purchase an e–ticket, we email it to you within (duration) of your booking. Simply print it out and bring it with you – along with a valid photo ID – to the airline counter while checking in for your flight. </p>

                                    <p class="heading-semibold text-colour7">Can Infant tickets be booked on (add website url)?</p>
                                    <p class="mb-3">Yes, Infants under 2 years of age can travel through NIC Express Reward. Make sure that the infant must be 24 months or below throughout the booked travel. If the infant is above 24 months during the return journey, the infant ticket will be cancelled which may cause booking of a separate ticket as per the child fare. Remember to carry valid age proof or birth proof documents of the infant. You can book no more than one infant per adult. </p>

                                    <p class="heading-semibold text-colour7">What is the maximum number of seats I can book?</p>
                                    <p class="mb-3">A maximum of 9 seats can be booked at one time. If you need to book for more than 9 travellers, you will have to re–start the booking process for the additional travellers. Some airlines don't allow booking more than 4 passengers (adult + children) at one time, and thus please try to search for fewer passengers. </p>

                                    <p class="heading-semibold text-colour7">I did a search for flight tickets and selected my flight. However, after providing the passenger details, I see that the fares have increased. Why?</p>
                                    <p class="mb-3">The airline fares are dynamic in nature and are based on availability of the seats on the particular flight. Often, there are instances that the seats selected by you may get sold by the time you complete your booking. Therefore, to minimize the chances of booking failures, please check the availability of the seats before you proceed with the payment. If you find that the fare has increased at this step, you have the option of going ahead with the booking or refresh the search. </p>

                                    <p class="heading-semibold text-colour7">Can I get my seats assigned post my booking confirmation?</p>
                                    <p class="mb-3">NIC Express Reward doesn't do pre–seating. Some airlines will confirm your seat assignments, their rules for doing so vary. You need to call your airline directly to check whether you get to choose your seat. </p>

                                    <p class="heading-semibold text-colour7">How do I get a boarding pass for an e–ticket?</p>
                                    <p class="mb-3">You can check-in in three ways.</p>
                                    <ol>
                                        <li class="mb-3 mx-3">Some of the airlines provide a web check-in facility, where you can select your seat and print your boarding pass online. If you use this facility, you will have to approach the check-in counter to drop your baggage.</li>
                                        <li class="mb-3 mx-3">You can also check-in using Tele check-in service provided by some of the airlines. In such cases, you will have to present your ticket and ID proof at Airline check-in counter for issuance of boarding card and for baggage drop.</li>
                                        <li class="mb-3 mx-3">Alternatively, you can check-in physically at the airport counters of the airline by presenting your ticket and ID proof. </li>
                                    </ol>

                                    <p class="heading-semibold text-colour7">How do I find out my baggage limit? </p>
                                    <p class="mb-3">Cabin and checked-in baggage limits vary from airline to airline. Some airlines have baggage weight restrictions and others have a specific number of pieces permitted. Please note that airline restrictions on baggage apply to both checked–in bags as well as cabin baggage. Kindly contact the airline directly or visit their website for accurate details regarding baggage limit. </p>

                                    <p class="heading-semibold text-colour7">I've booked my tickets but need to add my child's tickets to my booking. How do I do it?</p>
                                    <p class="mb-3">You can call us on +977-1-5970101 and we will request the airline on behalf of you to change the name. However, if the airline doesn't allow it, you'll have to cancel and re–book the ticket. </p>

                                    <p class="heading-semibold text-colour7">How do I print my ticket?</p>
                                    <p class="mb-3">You can view all your upcoming and completed trips by selecting the ‘Manage Bookings’ section on the website. You can view/print your ticket by clicking on the Booking Reference number. </p>

                                    <p class="heading-semibold text-colour7">Can I book a specials like meal, wheelchairs through NIC Express Reward?</p>
                                    <p class="mb-3">Sorry, NIC Express Reward website currently does not support this feature. Please contact the airline directly. </p>

                                    <p class="heading-semibold text-colour7">How do I cancel a flight reservation?</p>
                                    <p class="mb-3">(add cancellation process) </p>

                                    <p class="heading-semibold text-colour7">What should I do in cases like - No Flight Results Shown / No Air Search results found / Your Request cannot be processed, please try again later?</p>
                                    <p class="mb-3">To eliminate the error, clear your browser cache and temporary files (CLTR + H) and try the search again. If the error persists, it could be due to either of the two situations: we can't find flights for the specified route or the flights are not available on your specified date. You could call us on +977-1-5970101. </p>

                                    <p class="heading-semibold text-colour7">How can I enter my Frequent Flyer Program (FFP) number while booking the flight?</p>
                                    <p class="mb-3">We do not have an option of entering the frequent flyer number at the time of booking flights. You can mention your frequent flyer number with the airline directly at the time of check-in. </p>

                                    <p class="heading-semibold text-colour7">I got a blank screen when I submitted the booking form, I got charged but never received e-ticket confirmation. What should I do?</p>
                                    <p class="mb-3">We're really sorry for the inconvenience. Please don't worry. We'll call you within four hours and complete this booking offline. We suggest you do not attempt booking again as you may end up getting charged twice. If you are really in a hurry, call us on +977-1-5970101 and we'll help you to resolve this immediately. </p>

                                    <p class="heading-semibold text-colour7">How do I get the booking confirmation?</p>
                                    <p class="mb-3">We will send you an email on your registered email address to confirm your flight booking. </p>

                                    <p class="heading-semibold text-colour7">Do I need to confirm my booking before I fly?</p>
                                    <p class="mb-3">No, you don't. If you really want to, you can contact the airline directly. </p>

                                    <p class="heading-semibold text-colour7">I did not get the e-ticket no. in the confirmation received from NIC Express Reward?</p>
                                    <p class="mb-3">We'll send your e-ticket/confirmation details to your registered email address. </p>

                                    <p class="heading-semibold text-colour7">Do I have to show my e-ticket confirmation voucher at the check-in counter?</p>
                                    <p class="mb-3">Yes, you have to show e-ticket confirmation voucher. Please carry a print copy of the e-ticket as it is important. </p>

                                    <p class="heading-semibold text-colour7">Do I have to pay anything extra at the airport?</p>
                                    <p class="mb-3">Every airport has their set of charges and rules, therefore, the charges depend on the airport you're flying from. </p>

                                    <p class="heading-semibold text-colour7">Do I have to show an ID proof at time of check-in?</p>
                                    <p class="mb-3">Yes, your Photo ID proof may be checked by airline authorities. It is strongly recommended that you carry a government-issued Photo ID with you during your journey. </p>

                                    <p class="heading-semibold text-colour7">I misspelled my name while booking, how can I correct it? </p>
                                    <p class="mb-3">Allowing change of name requests entirely depends on the airline you're booked with. You can call us if your airline allows change of name requests. We can pass on your request. However, if the airline doesn't allow it, you'll have to cancel and re-book the ticket.</p>

                                    <p class="heading-semibold text-colour7">Can I book flights for friends and family using my account by NPoints or cash?</p>
                                    <p class="mb-3">Yes, you can do the booking provided you have all the details belonging to the passengers you want to book for. Provide the details when you're asked to enter traveller details at the time of booking. </p>

                                    <p class="heading-semibold text-colour7">Can I do an online check-in?</p>
                                    <p class="mb-3">You may use your PNR or Airline Booking Reference Number to do an online check-in on the website.</p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                </div>
                            </div>
                        </div>
                        <!-- FLIGHTS -->

                        <!-- FLIGHT CANCELLATION -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold" type="button" data-toggle="collapse"
                                        data-target="#collapse3">
                                        Flights - cancellation, amendments & refunds
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse3" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">How do I cancel my Flight Reservation? </p>
                                    <p class="mb-3">Please call our NIC Express Reward’s customer services at +977-1-5970101 to do the cancellation. </p>

                                    <p class="heading-semibold text-colour7">The flight I booked was cancelled by the airline, how do I get my NPoints or money back?</p>
                                    <p class="mb-3">You are entitled to a full refund if airline cancels the flight. First, we request you to cancel your booking online. Once you have done this, please send an email to support@giift.com along with your Trip ID. Please mention about your flight cancellation. After verification, we will make sure that you get a full refund. </p>

                                    <p class="heading-semibold text-colour7">What are cancellation and amendment charges?</p>
                                    <p class="mb-3">Cancellation charges totally depend on the airline, sector, class of booking, and time of cancellation. You can check fare rules online, mentioned on the booking page for further details. You can always login to NIC Express Reward and check the details on trip pages.</p>
                                    <p class="mb-3">Note: Along with charges levied by airlines, NIC Express Reward additionally charges (add charges) per passenger, per sector. </p>

                                    <p class="heading-semibold text-colour7">Latest by when can I modify my flight tickets? </p>
                                    <p class="mb-3">You can modify your flight tickets up to 48 hours before your flight's departure time. If it is later than that, you need to contact the airline directly for cancellation. </p>

                                    <p class="heading-semibold text-colour7">On cancellation, how do I receive my money back? </p>
                                    <p class="mb-3">Your money will be directly credited to your bank account (same account through which booking was held). For example, if you used your credit card, we will make an appropriate charge reversal. If you used your debit card, we will credit the money back to the debit card. For NPoints Redemption booking there will be NO refunds of NPoints. </p>

                                    <p class="heading-semibold text-colour7">How long does it take to process a refund? </p>
                                    <p class="mb-3">We usually process the refund within 4 working days of cancellation. However, it may take slightly longer to reflect in your account statement, depending on the bank's regulations. Normally, it takes about 14 days for most refunds to hit their respective accounts. </p>

                                    <p class="heading-semibold text-colour7">It has been more than 15 days and I have not got my refund yet, what do I do?  </p>
                                    <p class="mb-3">We're extremely sorry for the delay. Do register your complaint with us at support@giift.com and we will get back to you very soon. </p>

                                    <p class="heading-semibold text-colour7">I made a booking on NIC Express Reward and cancelled the booking directly with the airline. How can I claim my refund? </p>
                                    <p class="mb-3">You can claim refund only if you intimate us immediately after cancellation. Depending on your bank, the refund process generally takes 4 to 15 days (from the day we find out about the cancellation). In cases such as no show or flight cancellation, the refund process can take more than three weeks. </p>

                                </div>
                            </div>
                        </div>
                        <!-- FLIGHT CANCELLATION -->

                        <!-- HOTELS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse4">
                                        Hotels
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse4" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Can more than 2 adults stay in one room?</p>
                                    <p class="mb-3">Most hotels allow additional guests to stay in a room for an extra charge as long as the room doesn't exceed the maximum number of guests allowed per room. If you book a room that cannot accommodate your group, the hotel may cancel your reservation or require that you book additional rooms. If you have any doubts, please check directly with your hotel for their extra guest charges and the maximum number of people allowed in the room you've booked. </p>

                                    <p class="heading-semibold text-colour7">Will our Children get free stay?</p>
                                    <p class="mb-3">Please specify the number of children accompanying you by selecting the number from the drop-down box. If any charges apply for the stay of children, you will be notified of the same during the search. </p>

                                    <p class="heading-semibold text-colour7">What if I need a specific type of hotel room (non-smoking, wheelchair friendly etc)?</p>
                                    <p class="mb-3">The request is subject to the Hotel's terms and conditions. </p>

                                    <p class="heading-semibold text-colour7">How do I know if my Booking with the Hotel was successful?</p>
                                    <p class="mb-3">We will send you an Email confirmation along with the Hotel Confirmation Voucher. </p>

                                    <p class="heading-semibold text-colour7">Do I need to confirm my Booking with the Hotel?</p>
                                    <p class="mb-3">No, you don't. You can also contact the hotel directly if you prefer. However, it may take up to 12 hours for the booking to reflect at the hotel. </p>

                                    <p class="heading-semibold text-colour7">What if I do not get the confirmation while booking or get a blank page on submission of the Booking form? </p>
                                    <p class="mb-3">If a confirmation page doesn't display once you complete your booking, check your email for a confirmation. If you don't get an email confirmation within ten minutes, send an email to support@giift.com and you will receive your confirmation details. </p>

                                    <p class="heading-semibold text-colour7">How long will it take for the hotel to get my booking information?</p>
                                    <p class="mb-3">The time it takes for a hotel to get your booking information varies by specific hotel and arrival date. In most cases the hotel should receive the reservation information within 12 hours of the time you made your booking (except for nights and weekends when the hotel's reservation department is closed). Please note that this does not apply to bookings made for the same day. </p>

                                    <p class="heading-semibold text-colour7">What is my hotel's check–in time?</p>
                                    <p class="mb-3">Typically, the hotel check–in time is after 2:00 pm (local time). Check with your specific hotel for its exact check–in time. </p>

                                    <p class="heading-semibold text-colour7">Will the Hotel hold my booking as I am arriving late?</p>
                                    <p class="mb-3">Since your reservation is guaranteed with Prepaid Voucher, the hotel is obliged to hold your room until 7 AM, the day after your planned arrival date. But please check the same with your hotel. </p>

                                    <p class="heading-semibold text-colour7">What if I'm going to arrive early?</p>
                                    <p class="mb-3">If you know you're going to arrive early at your hotel, call them in advance and check with the hotel if they will be able to accommodate you as it's subject to the terms and conditions of the hotel. </p>

                                    <p class="heading-semibold text-colour7">How do I get a receipt or invoice for my hotel booking?</p>
                                    <p class="mb-3">Log in to your NIC Express Reward Account with your registered email address and password. Once you successfully login, go to "Manage Bookings" to view all your upcoming and completed trips. You can view/print your receipt by clicking on Booking Reference Number. </p>

                                    <p class="heading-semibold text-colour7">How do I cancel my hotel booking?</p>
                                    <p class="mb-3">NIC Express Reward doesn't support cancellations to hotel bookings.</p>

                                    <p class="heading-semibold text-colour7">How will I get my NPoints back after cancelling a hotel booking? </p>
                                    <p class="mb-3">NIC Express Reward NPoints for cancelled bookings are not refunded or credited back into the account. </p>

                                    <p class="heading-semibold text-colour7">How do I modify the Hotel Booking?</p>
                                    <p class="mb-3">NIC Express Reward doesn't support modifications to hotel bookings. You'll have to cancel your existing booking and make a new one.</p>

                                </div>
                            </div>
                        </div>
                        <!-- HOTELS -->

                        <!-- CARS -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse5">
                                        Cars
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse5" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">Do I get an immediate booking confirmation?</p>
                                    <p class="mb-3">Once car redemption is made, the confirmation is not immediate. It takes a minimum of 24-72 hours for a confirmation. At times when your booking is not confirmed, the NPoints will be refunded back into your account and you can repeat your attempt to book online. </p>

                                    <p class="heading-semibold text-colour7">Can I change my booking?</p>
                                    <p class="mb-3">Once the car booking is done, the existing booking cannot be amended. </p>

                                    <p class="heading-semibold text-colour7">Can I cancel my booking?</p>
                                    <p class="mb-3">For bookings made through NIC Express Reward, no cancelations are allowed if initiated voluntarily by you. Hence no NPoints will be refunded back into your account. However, if the reason for cancelation is from the service provider/supplier's end, the points will be duly credited into your account within 7-10 business days. </p>

                                    <p class="heading-semibold text-colour7">How old do I have to be to rent a car?</p>
                                    <p class="mb-3">For most car hire companies, the age requirement is between 25 and 70 years. If the driver is under 25 or over 70 years of age, you may have to pay an additional fee (charges may vary depending on city/country and type of car). </p>

                                    <p class="heading-semibold text-colour7">Is it possible to rent a car for another person through my account? </p>
                                    <p class="mb-3">Yes, as long as they meet the stipulated requirements. You would need to mention their details while making the reservation. </p>

                                    <p class="heading-semibold text-colour7">Does my car redemption include all charges or is there anything extra I have to pay?</p>
                                    <p class="mb-3">Most car rental services include in their price - Theft Protection, Collision Damage Waiver (CDW), local taxes, airport surcharges and any road fees. However, you would be responsible for any 'extra' charges at the time of car pick-up, fees for a young/additional driver, one-way fees, cross border driving. The same will be explained to you, in addition to ways to reduce costs like carrying child seats and GPS before you make your car booking. You may read more about this in the 'Terms and Conditions' section of the car rental service you are booking with. </p>

                                    <p class="heading-semibold text-colour7">Do I have to pay any deposit?</p>
                                    <p class="mb-3">Yes, the time you pick your car for rental, you will be required to leave a security deposit against possible damage to the car during the rental period. The deposit will be charged directly by the car hire company. Quite often, a credit card in the name of the main driver is required. If the condition of the car and extras is the same on return as at the time of rental, and is in accordance with the fuel policy, the security deposit will be refunded upon returning the car (refund period depends on car rental company). </p>

                                    <p class="heading-semibold text-colour7">Does the car insurance provided cover all claims?</p>
                                    <p class="mb-3">No. The car insurance provided by the rental company is limited and in case of any misfortunate event, an excess amount will have to be paid by the customer. However, full insurance can be purchased directly through the supplier at the time of collecting the car.</p>

                                </div>
                            </div>
                        </div>
                        <!-- CARS -->

                        <!-- NIC EXPRESS REWARD SHOP -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse6">
                                        NIC Express Reward Shop
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse6" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">How many days will it take to get my reward merchandise delivered?</p>
                                    <p class="mb-3">It normally takes about 2 – 3 working weeks for reward merchandise to be delivered. However, this may vary depending on availability of stock. </p>

                                    <p class="heading-semibold text-colour7">What should I do if the reward merchandise doesn't reach me within 21 working days?</p>
                                    <p class="mb-3">Please call our NIC Express Reward’s customer services at +977-1-5970101 to check on the status of your order. </p>

                                    <p class="heading-semibold text-colour7">What if the product reaches my mentioned delivery address when I was away or not available?</p>
                                    <p class="mb-3">Should this occur, the delivery service would reattempt to deliver the product thrice, so kindly ensure your availability. Post that, the following options are available to receive the product;</p>
                                    <ol type="a">
                                        <li class="mb-3 mx-3">Collect the product at the courier company.</li>
                                        <li class="mb-3 mx-3">Arrange a delivery according to your convenient date & time. In this case, delivery charges will be levied, which can be either paid directly to the delivery service or if you have enough NPoints, those will be debited from your reward account. </li>
                                    </ol>

                                    <p class="heading-semibold text-colour7">What should I do if the reward merchandise delivered to me is wrong or damaged?</p>
                                    <p class="mb-3">In the unfortunate event that this happens, please call us on +977-1-5970101 within one business day from the delivery of the product in order to report the same. In case we do not receive the request within 24 hours, no replacement will be permitted. Reward NPoints will not be refunded to you. </p>

                                    <p class="heading-semibold text-colour7">What should I do if I ordered the wrong item?</p>
                                    <p class="mb-3">Please call us on +977-1-5970101 to cancel your order. Your NPoints will be refunded to you and this process may take 2 – 3 working days. </p>

                                    <p class="heading-semibold text-colour7">Is the Delivery free of charge?</p>
                                    <p class="mb-3">The cost of shipping would be included in the NPoints utilized for redemption. However, it would vary according to the merchandise/product selected. You should not pay any more to the delivery person upon delivery of the goods. </p>

                                    <p class="heading-semibold text-colour7">Do you deliver items outside (region)?</p>
                                    <p class="mb-3">No, all items are delivered only within (region). </p>

                                    <p class="heading-semibold text-colour7">What if I want to cancel my order?</p>
                                    <p class="mb-3">To cancel your order please call us on +977-1-5970101. Upon cancelation, your NIC Express Reward NPoints will not be refunded back to you.</p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                    <p class="heading-semibold text-colour7"></p>
                                    <p class="mb-3"></p>

                                </div>
                            </div>
                        </div>
                        <!-- NIC EXPRESS REWARD SHOP -->

                        <!-- UTILITY -->
                        <div class="card mb-3">
                            <div class="card-header p-0">
                                <h2 class="mb-0">
                                    <button class="h6 btn btn-block text-left p-3 heading-semibold collapsed" type="button" data-toggle="collapse"
                                        data-target="#collapse7">
                                        Utility
                                        <span class="arrow-icon">
                                            <i class="fa fa-caret-up"></i>
                                        </span>
                                    </button>
                                </h2>
                            </div>

                            <div id="collapse7" class="collapse" data-parent="#static-accordion">
                                <div class="card-body">
                                    <p class="heading-semibold text-colour7">How many days to process the payment?</p>
                                    <p class="mb-3">Processing of payment is done within 2-3 working days.</p>
                                </div>
                            </div>
                        </div>
                        <!-- UTILITY -->
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

