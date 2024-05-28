using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using CB.IBE.Platform.Masters.Entities;


namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class ItineraryDetails
    {
        #region Private Variables

        int mintId = 0;

        string mstrItineraryTripId = string.Empty;
        string mstrItinerarySearchId = string.Empty;
        int mintSequenceNo = 0;
        string mstrOriginLocation = string.Empty;
        string mstrDestinationLocation = string.Empty;
        DateTime mdateDepartureDate = DateTime.MinValue.ToUniversalTime();
        DateTime mdateArrivalDate = DateTime.MinValue.ToUniversalTime();
        bool mboolIsMultipleCarrier = false;
        string mstrType = string.Empty;
        string mstrBaggageAllowance = string.Empty;
        List<FlightDetails> mlstListOfFlightDetails = new List<FlightDetails>();
        List<PassengerDetails> mlstTravelerInfo = new List<PassengerDetails>();
        PassengerDetails mobjBillingInfo = new PassengerDetails();
        PassengerDetails mobjDeliveryInfo = new PassengerDetails();
        string mstrFareKey = string.Empty;
        string mstrItineraryReference = string.Empty;
        FareDetails mobjFareDetails = new FareDetails();
        string mstrOnwardDuration = string.Empty;
        string mstrReturnDuration = string.Empty;
        string mstrCabinType = string.Empty;
        int mintAirSearchId = 0;
        private PaxPricingInfoList mobjPaxPricingInfoList = new PaxPricingInfoList();
        string mstrMemberId = string.Empty;
        int mintAdults = 0;
        int mintChildrens = 0;
        int mintInfants = 0;
        RefererDetails mobjRefererDetails = new RefererDetails();
        PaymentDetails mobjPaymentDetails = new PaymentDetails();
        string mstrTransactionReference = string.Empty;
        BookingPaymentDetails mobjBookingPaymentDetails = new BookingPaymentDetails();
        string mstrPromoCode = string.Empty;
        PricingDetails mobjPaxPricingDetails = new PricingDetails();

        #endregion

        #region Properties
        /// <summary>
        /// Displays Sequence No
        /// </summary>
        [DataMember]
        public PaymentDetails PaymentDetails
        {
            get { return mobjPaymentDetails; }
            set { mobjPaymentDetails = value; }
        }

        /// <summary>
        /// Displays Sequence No
        /// </summary>
        [DataMember]
        public int SequenceNo
        {
            get { return mintSequenceNo; }
            set { mintSequenceNo = value; }
        }

        /// <summary>
        /// Displays Id
        /// </summary>  
        [DataMember]
        public int Id
        {
            get { return mintId; }
            set { mintId = value; }
        }


        /// <summary>
        /// Displays ItineraryTripId
        /// </summary>
        [DataMember]
        public string ItineraryTripId
        {
            get { return mstrItineraryTripId; }
            set { mstrItineraryTripId = value; }
        }

        /// <summary>
        /// Displays Origin Location
        /// </summary>
        [DataMember]
        public string OriginLocation
        {
            get { return mstrOriginLocation; }
            set { mstrOriginLocation = value; }
        }

        /// <summary>
        /// Displays Destination Location
        /// </summary>
        [DataMember]
        public string DestinationLocation
        {
            get { return mstrDestinationLocation; }
            set { mstrDestinationLocation = value; }
        }

        /// <summary>
        /// Displays Departure Date
        /// </summary>
        [DataMember]
        public DateTime DepartureDate
        {
            get { return mdateDepartureDate; }
            set { mdateDepartureDate = value; }
        }

        /// <summary>
        /// Displays Arrival Date
        /// </summary>
        [DataMember]
        public DateTime ArrivalDate
        {
            get { return mdateArrivalDate; }
            set { mdateArrivalDate = value; }
        }

        /// <summary>
        /// Displays Is Multiple Carrier
        /// </summary>
        [DataMember]
        public bool IsMultipleCarrier
        {
            get
            {
                //TO DO -Check Multiple carrier
                return mboolIsMultipleCarrier;
            }
            set { mboolIsMultipleCarrier = value; }
        }

        /// <summary>
        /// Displays Type (Oneway/RoundTrip)
        /// </summary>
        [DataMember]
        public string Type
        {
            get { return mstrType; }
            set { mstrType = value; }
        }

        /// <summary>
        /// Displays Baggage Allowance
        /// </summary>
        [DataMember]
        public string BaggageAllowance
        {
            get { return mstrBaggageAllowance; }
            set { mstrBaggageAllowance = value; }
        }

        /// <summary>
        /// Displays List of Flight Details
        /// </summary>
        [DataMember]
        public List<FlightDetails> ListOfFlightDetails
        {
            get { return mlstListOfFlightDetails; }
            set { mlstListOfFlightDetails = value; }
        }



        /// <summary>
        /// Displays FareKey
        /// </summary>
        [DataMember]
        public string FareKey
        {
            get { return mstrFareKey; }
            set { mstrFareKey = value; }
        }
        /// <summary>
        /// Displays Itinerary Reference
        /// </summary>
        [DataMember]
        public string ItineraryReference
        {
            get { return mstrItineraryReference; }
            set { mstrItineraryReference = value; }
        }

        /// <summary>
        /// Displays Fare Details
        /// </summary>
        [DataMember]
        public FareDetails FareDetails
        {
            get { return mobjFareDetails; }
            set { mobjFareDetails = value; }
        }


        /// <summary>
        /// Displays Onward Duration
        /// </summary>
        [DataMember]
        public string OnwardDuration
        {
            get { return mstrOnwardDuration; }
            set { mstrOnwardDuration = value; }
        }

        /// <summary>
        /// Displays Return Duration
        /// </summary>
        [DataMember]
        public string ReturnDuration
        {
            get { return mstrReturnDuration; }
            set { mstrReturnDuration = value; }
        }

        /// <summary>
        /// Cabin Type
        /// </summary>
        [DataMember]
        public string CabinType
        {
            get { return mstrCabinType; }
            set { mstrCabinType = value; }
        }

        /// <summary>
        /// Displays List of Passengers (Traveler Info)
        /// </summary>
        [DataMember]
        public List<PassengerDetails> TravelerInfo
        {
            get { return mlstTravelerInfo; }
            set { mlstTravelerInfo = value; }
        }

        /// <summary>
        /// Displays Billing Info
        /// </summary>
        [DataMember]
        public PassengerDetails BillingInfo
        {
            get { return mobjBillingInfo; }
            set { mobjBillingInfo = value; }
        }

        /// <summary>
        /// Displays Delivery Info
        /// </summary>
        [DataMember]
        public PassengerDetails DeliveryInfo
        {
            get { return mobjDeliveryInfo; }
            set { mobjDeliveryInfo = value; }
        }

        /// <summary>
        /// Displays  Air SearchId
        /// </summary>
        [DataMember]
        public int AirSearchId
        {
            get { return mintAirSearchId; }
            set { mintAirSearchId = value; }
        }

        [DataMember]
        public PaxPricingInfoList PaxPricingInfoList
        {
            get { return mobjPaxPricingInfoList; }
            set { mobjPaxPricingInfoList = value; }
        }

        /// <summary>
        /// Displays MemberId
        /// </summary>
        [DataMember]
        public string MemberId
        {
            get { return mstrMemberId; }
            set { mstrMemberId = value; }
        }

        /// <summary>
        /// Displays Adults
        /// </summary>
        [DataMember]
        public int Adults
        {
            get { return mintAdults; }
            set { mintAdults = value; }
        }

        /// <summary>
        /// Displays Childrens
        /// </summary>
        [DataMember]
        public int Childrens
        {
            get { return mintChildrens; }
            set { mintChildrens = value; }
        }

        /// <summary>
        /// Displays Infants
        /// </summary>
        [DataMember]
        public int Infants
        {
            get { return mintInfants; }
            set { mintInfants = value; }
        }

        /// <summary>
        /// Displays RefererDetails
        /// </summary>
        [DataMember]
        public RefererDetails RefererDetails
        {
            get { return mobjRefererDetails; }
            set { mobjRefererDetails = value; }
        }
        /// <summary>
        /// Displays External Reference
        /// </summary>
        [DataMember]
        public string TransactionReference
        {
            get { return mstrTransactionReference; }
            set { mstrTransactionReference = value; }
        }

        /// <summary>
        /// Displays ItinerarySearchId
        /// </summary>
        [DataMember]
        public string ItinerarySearchId
        {
            get { return mstrItinerarySearchId; }
            set { mstrItinerarySearchId = value; }
        }
        /// <summary>
        /// Displays Booking Payment Details
        /// </summary>
        [DataMember]
        public BookingPaymentDetails BookingPaymentDetails
        {
            get { return mobjBookingPaymentDetails; }
            set { mobjBookingPaymentDetails = value; }
        }

        /// <summary>
        /// Displays Promo Code
        /// </summary>
        [DataMember]
        public string PromoCode
        {
            get { return mstrPromoCode; }
            set { mstrPromoCode = value; }
        }

        /// <summary>
        /// PaxPricingDetails
        /// </summary>
        [DataMember]
        public PricingDetails PaxPricingDetails
        {
            get { return mobjPaxPricingDetails; }
            set { mobjPaxPricingDetails = value; }
        }
        #endregion
    }
}
