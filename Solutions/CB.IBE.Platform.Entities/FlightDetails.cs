using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class FlightDetails
    {
        #region Private Variables

        int mintId = 0;
        int mintItineraryId = 0;
        string mstrType = string.Empty;
        string mstrOriginLocation = string.Empty;
        string mstrDestinationLocation = string.Empty;
        int mintSequenceNo = 0;
        DateTime mdtDepartureDate = DateTime.MinValue.ToUniversalTime();
        DateTime mdtFinalArrivalDate = DateTime.MinValue.ToUniversalTime();
        List<FlightSegment> lstFlightSegments = new List<FlightSegment>();
        FareDetails mobjFareDetails = new FareDetails();
        PaxPricingInfoList mobjPaxPricingInfoList = new PaxPricingInfoList();
        string mstrCabinType = string.Empty;
        PricingDetails mobjPaxPricingDetails = new PricingDetails();
        string mstrFlightKey = string.Empty;

        #endregion

        #region Properties

        [DataMember]
        public PaxPricingInfoList PaxPricingInfoList
        {
            get { return mobjPaxPricingInfoList; }
            set { mobjPaxPricingInfoList = value; }
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
        /// Displays Itinerary Id
        /// </summary>
        [DataMember]
        public int ItineraryId
        {
            get { return mintItineraryId; }
            set { mintItineraryId = value; }
        }


        [DataMember]
        public int SequenceNo
        {
            get { return mintSequenceNo; }
            set { mintSequenceNo = value; }
        }
        /// <summary>
        /// Displays Type (Onward/Return)
        /// </summary>
        [DataMember]
        public string Type
        {
            get { return mstrType; }
            set { mstrType = value; }
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
            get { return mdtDepartureDate; }
            set { mdtDepartureDate = value; }
        }

        /// <summary>
        /// Displays Departure Date
        /// </summary>
        [DataMember]
        public string DepartureTime
        {
            get { return mdtDepartureDate.ToString("HH:mm"); }
            set { }
        }

        /// <summary>
        /// Displays Final Arrival Date
        /// </summary>
        [DataMember]
        public DateTime FinalArrivalDate
        {
            get { return mdtFinalArrivalDate; }
            set { mdtFinalArrivalDate = value; }
        }

        /// <summary>
        /// Displays Final Arrival Date
        /// </summary>
        [DataMember]
        public string FinalArrivalTime
        {
            get { return mdtFinalArrivalDate.ToString("HH:mm"); }
            set { }
        }


        /// <summary>
        /// Displays List of Flight Segments
        /// </summary>
        [DataMember]
        public List<FlightSegment> ListOfFlightSegments
        {
            get { return lstFlightSegments; }
            set { lstFlightSegments = value; }
        }

        /// <summary>
        /// Displays Total Duration
        /// </summary>
        [DataMember]
        public int TotalDuration
        {
            get
            {
                int totalJourneyTime = lstFlightSegments.Sum(x => x.JourneyTime);
                return totalJourneyTime;
            }
            set { }
        }
        [DataMember]
        public string TotalDurationDisplay
        {
            get
            {
                return ((TotalDuration / 60) / 60).ToString("00") + " hrs " + ((TotalDuration / 60) % 60).ToString("00") + " mins ";
            }
            set { }
        }
        [DataMember]
        public int TotalDurationHrs
        {
            get
            {
                return (TotalDuration / 60) / 60;
            }
            set
            { }
        }

        [DataMember]
        public int TotalDurationMinutes
        {
            get
            {
                return (TotalDuration / 60) % 60;
            }
            set
            { }
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
        /// Displays Cabin Type
        /// </summary>
        [DataMember]
        public string CabinType
        {
            get { return mstrCabinType; }
            set { mstrCabinType = value; }
        }


        [DataMember]
        public PricingDetails PaxPricingDetails
        {
            get { return mobjPaxPricingDetails; }
            set { mobjPaxPricingDetails = value; }
        }

        [DataMember]
        public string FlightKey
        {
            get { return mstrFlightKey; }
            set { mstrFlightKey = value; }
        }

        #endregion
    }
}
