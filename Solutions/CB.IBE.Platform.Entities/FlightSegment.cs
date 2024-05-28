using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class FlightSegment
    {
        #region Private Variables

        string mstrOriginLocation = string.Empty;
        string mstrDestinationLocation = string.Empty;
        DateTime mdtDepartureDate = DateTime.MinValue.ToUniversalTime();
        DateTime mdtArrivalDate = DateTime.MinValue.ToUniversalTime();
        string mstrBaggageAllowance = string.Empty;
        string mstrFlightNo = string.Empty;
        int mintSequenceNo = 0;
        int mintJourneyTime = 0;
        string mintTerminalId = string.Empty;
        string mstrEquipmentCode = string.Empty;
        string mstrAircraftType = string.Empty;
        string mstrAirlinePNR = string.Empty;
        string mstrAirlinelIATACode = string.Empty;
        string mstrOperatingAirline = string.Empty;
        string mstrMarketingAirline = string.Empty;
        int mintStops = 0;
        Carrier mobjCarrier = new Carrier();
        Carrier mobjOperatingCarrier = new Carrier();
        AirField mobjDepartureAirFieldField = new AirField();
        AirField mobjReturnAirFieldField = new AirField();
        string mstrCabinType = string.Empty;
        List<SegmentStopDetails> mobjStopsDetails = new List<SegmentStopDetails>();
        string mstrSegmentKey = string.Empty;
        List<string> mstrFareKeyList = new List<string>();
        string mstrDepTerminal = string.Empty;
        string mstrArrTerminal = string.Empty;

        

        #endregion

        #region Properties
        /// <summary>
        /// Displays AircraftType 
        /// </summary>
        [DataMember]
        public string AircraftType
        {
            get { return mstrAircraftType; }
            set { mstrAircraftType = value; }
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


        [DataMember]
        public string DisplayDepartureDate
        {
            get { return mdtDepartureDate.ToString("dd-MM-yyyy"); }
            set { }
        }
        [DataMember]
        public string DisplayDepartureTime
        {

            get { return mdtDepartureDate.ToString("HH:mm"); }
            set { }
        }
        [DataMember]
        public int DepartureTimeInMinutes
        {
            get
            {
                var ts = DepartureDate;
                return ts.Minute + (ts.Hour * 60);
            }
            set { }
        }

        /// <summary>
        /// Displays Arrival Date
        /// </summary>
        [DataMember]
        public DateTime ArrivalDate
        {
            get { return mdtArrivalDate; }
            set { mdtArrivalDate = value; }
        }

        [DataMember]
        public string DisplayArrivalDate
        {
            get { return mdtArrivalDate.ToString("dd-MM-yyyy"); }
            set { }
        }
        [DataMember]
        public string DisplayArrivalTime
        {
            get { return mdtArrivalDate.ToString("HH:mm"); }
            set { }
        }
        [DataMember]
        public int ArrivalTimeInMinutes
        {
            get
            {
                var ts = ArrivalDate;
                return ts.Minute + (ts.Hour * 60);
            }
            set { }
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
        /// Displays Sequence No
        /// </summary>
        [DataMember]
        public int SequenceNo
        {
            get { return mintSequenceNo; }
            set { mintSequenceNo = value; }
        }
        /// <summary>
        /// Displays Journey Time
        /// </summary>
        [DataMember]
        public int JourneyTime
        {
            get { return mintJourneyTime; }
            set { mintJourneyTime = value; }
        }

        /// <summary>
        /// Displays Journey Time
        /// </summary>
        [DataMember]
        public string DisplayJourneyTime
        {
            get { return ((mintJourneyTime / 60) / 60) + ":" + ((mintJourneyTime / 60) % 60); }
            set { }
        }

        [DataMember]
        public int TotalDurationHrs
        {
            get { return ((mintJourneyTime / 60) / 60); }
            set { }
        }
        [DataMember]
        public int TotalDurationMins
        {
            get { return ((mintJourneyTime / 60) % 60); }
            set { }
        }


        /// <summary>
        /// Displays Flight No
        /// </summary>
        [DataMember]
        public string FlightNo
        {
            get { return mstrFlightNo; }
            set { mstrFlightNo = value; }
        }
        /// <summary>
        /// Displays Terminal Id
        /// </summary>
        [DataMember]
        public string TerminalId
        {
            get { return mintTerminalId; }
            set { mintTerminalId = value; }
        }
        /// <summary>
        /// Displays Equipment Code
        /// </summary>
        [DataMember]
        public string EquipmentCode
        {
            get { return mstrEquipmentCode; }
            set { mstrEquipmentCode = value; }
        }
        /// <summary>
        /// Displays AirlinePNR
        /// </summary>
        [DataMember]
        public string AirlinePNR
        {
            get { return mstrAirlinePNR; }
            set { mstrAirlinePNR = value; }
        }
        /// <summary>
        /// Displays Airlinel IATACode
        /// </summary>
        [DataMember]
        public string AirlinelIATACode
        {
            get { return mstrAirlinelIATACode; }
            set { mstrAirlinelIATACode = value; }
        }
        /// <summary>
        /// Displays Operating Airline
        /// </summary>
        [DataMember]
        public string OperatingAirline
        {
            get { return mstrOperatingAirline; }
            set { mstrOperatingAirline = value; }
        }
        /// <summary>
        /// Displays Marketing Airline
        /// </summary>
        [DataMember]
        public string MarketingAirline
        {
            get { return mstrMarketingAirline; }
            set { mstrMarketingAirline = value; }
        }

        /// <summary>
        /// No Of Stops
        /// </summary>
        /// 
        [DataMember]
        public int Stops
        {
            get { return mintStops; }
            set { mintStops = value; }
        }

        [DataMember]
        public Carrier Carrier
        {
            get { return mobjCarrier; }
            set { mobjCarrier = value; }
        }

        [DataMember]
        public Carrier OperatingCarrier
        {
            get { return mobjOperatingCarrier; }
            set { mobjOperatingCarrier = value; }
        }

        [DataMember]
        public AirField DepartureAirField
        {
            get { return mobjDepartureAirFieldField; }
            set { mobjDepartureAirFieldField = value; }
        }
        [DataMember]
        public AirField ArrivalAirField
        {
            get { return mobjReturnAirFieldField; }
            set { mobjReturnAirFieldField = value; }
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
        /// Displays Stops Details
        /// </summary>
        [DataMember]
        public List<SegmentStopDetails> StopsDetails
        {
            get { return mobjStopsDetails; }
            set { mobjStopsDetails = value; }
        }

        [DataMember]
        public string SegmentKey
        {
            get { return mstrSegmentKey; }
            set { mstrSegmentKey = value; }
        }

        [DataMember]
        public List<string> FareKeyList
        {
            get { return mstrFareKeyList; }
            set { mstrFareKeyList = value; }
        }

        [DataMember]
        public string DepTerminal
        {
            get { return mstrDepTerminal; }
            set { mstrDepTerminal = value; }
        }

        [DataMember]
        public string ArrTerminal
        {
            get { return mstrArrTerminal; }
            set { mstrArrTerminal = value; }
        }
        #endregion
    }
}
