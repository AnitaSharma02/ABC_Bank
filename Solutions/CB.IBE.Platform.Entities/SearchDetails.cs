using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class SearchDetails
    {
        #region Private Variables

        int mintId = 0;
        int mintSearchId = 0;
        string mstrMemberId = string.Empty;
        bool mboolIsReturn = false;
        string mstrOriginLocation = string.Empty;
        string mstrDestinationLocation = string.Empty;
        DateTime mdateDepartureDate = DateTime.MinValue.ToUniversalTime();
        DateTime mdateArrivalDate = DateTime.MinValue.ToUniversalTime();
        bool mboolIsSmokingAllowed = false;
        int mintNoOfStops = 0;
        string mstrFlightTypePrefLevel = string.Empty;
        string mstrFlightType = string.Empty;
        string mstrAirEquipType = string.Empty;
        string mstrCabinPrefLevel = string.Empty;
        string mstrCabin = string.Empty;
        string mstrAirlinePrefCode = string.Empty;
        string mstrTicketDistribPrefLevel = string.Empty;
        string mstrTicketDistributeType = string.Empty;
        int mintAdults = 0;
        int mintChildrens = 0;
        int mintInfants = 0;
        DateTime mdateCreatedDate = DateTime.MinValue.ToUniversalTime();
        string mstrSessionId = string.Empty;
        int mintReferrerId = 0;
        string mstrDepCountryName = string.Empty;
        string mstrArrCountryName = string.Empty;
        string mstrSearchType = string.Empty;

        private AirField mobjDepCode = new AirField();
        private AirField mobjArrCode = new AirField();

        #endregion


        #region Properties


        /// <summary>
        /// Displays SearchType(International-1/Domestic-0)
        /// </summary>
        [DataMember]
        public string SearchType
        {
            get { return mstrSearchType; }
            set { mstrSearchType = value; }
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
        /// Displays Search Id
        /// </summary>
        [DataMember]
        public int SearchId
        {
            get { return mintSearchId; }
            set { mintSearchId = value; }
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
        /// Displays IsReturn
        /// </summary>
        [DataMember]
        public bool IsReturn
        {
            get { return mboolIsReturn; }
            set { mboolIsReturn = value; }
        }

        /// <summary>
        /// Displays OriginLocation
        /// </summary>
        [DataMember]
        public string OriginLocation
        {
            get { return mstrOriginLocation; }
            set { mstrOriginLocation = value; }
        }

        /// <summary>
        /// Displays DestinationLocation
        /// </summary>
        [DataMember]
        public string DestinationLocation
        {
            get { return mstrDestinationLocation; }
            set { mstrDestinationLocation = value; }
        }

        /// <summary>
        /// Displays DepartureDate
        /// </summary>
        [DataMember]
        public DateTime DepartureDate
        {
            get { return mdateDepartureDate; }
            set { mdateDepartureDate = value; }
        }

        /// <summary>
        /// Displays ArrivalDate
        /// </summary>
        [DataMember]
        public DateTime ArrivalDate
        {
            get { return mdateArrivalDate; }
            set { mdateArrivalDate = value; }
        }

        /// <summary>
        /// Displays IsSmokingAllowed
        /// </summary>
        [DataMember]
        public bool IsSmokingAllowed
        {
            get { return mboolIsSmokingAllowed; }
            set { mboolIsSmokingAllowed = value; }
        }

        /// <summary>
        /// Displays NoOfStops
        /// </summary>
        [DataMember]
        public int NoOfStops
        {
            get { return mintNoOfStops; }
            set { mintNoOfStops = value; }
        }

        /// <summary>
        /// Displays FlightTypePrefLevel
        /// </summary>
        [DataMember]
        public string FlightTypePrefLevel
        {
            get { return mstrFlightTypePrefLevel; }
            set { mstrFlightTypePrefLevel = value; }
        }

        /// <summary>
        /// Displays FlightType
        /// </summary>
        [DataMember]
        public string FlightType
        {
            get { return mstrFlightType; }
            set { mstrFlightType = value; }
        }

        /// <summary>
        /// Displays AirEquipType
        /// </summary>
        [DataMember]
        public string AirEquipType
        {
            get { return mstrAirEquipType; }
            set { mstrAirEquipType = value; }
        }

        /// <summary>
        /// Displays CabinPrefLevel
        /// </summary>
        [DataMember]
        public string CabinPrefLevel
        {
            get { return mstrCabinPrefLevel; }
            set { mstrCabinPrefLevel = value; }
        }

        /// <summary>
        /// Displays Cabin
        /// </summary>
        [DataMember]
        public string Cabin
        {
            get { return mstrCabin; }
            set { mstrCabin = value; }
        }

        /// <summary>
        /// Displays AirlinePrefCode
        /// </summary>
        [DataMember]
        public string AirlinePrefCode
        {
            get { return mstrAirlinePrefCode; }
            set { mstrAirlinePrefCode = value; }
        }

        /// <summary>
        /// Displays TicketDistribPrefLevel
        /// </summary>
        [DataMember]
        public string TicketDistribPrefLevel
        {
            get { return mstrTicketDistribPrefLevel; }
            set { mstrTicketDistribPrefLevel = value; }
        }

        /// <summary>
        /// Displays TicketDistributeType
        /// </summary>
        [DataMember]
        public string TicketDistributeType
        {
            get { return mstrTicketDistributeType; }
            set { mstrTicketDistributeType = value; }
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
        /// Displays Created Date
        /// </summary>
        [DataMember]
        public DateTime CreatedDate
        {
            get { return mdateCreatedDate; }
            set { mdateCreatedDate = value; }
        }

        /// <summary>
        /// Displays Session Id
        /// </summary>
        [DataMember]
        public string SessionId
        {
            get { return mstrSessionId; }
            set { mstrSessionId = value; }
        }

        /// <summary>
        /// Displays Referrer Id
        /// </summary>
        [DataMember]
        public int ReferrerId
        {
            get { return mintReferrerId; }
            set { mintReferrerId = value; }
        }

        /// <summary>
        /// Displays DepCountryName
        /// </summary>
        [DataMember]
        public string DepCountryName
        {
            get { return mstrDepCountryName; }
            set { mstrDepCountryName = value; }
        }
        /// <summary>
        /// Displays ArrCountryName
        /// </summary>
        [DataMember]
        public string ArrCountryName
        {
            get { return mstrArrCountryName; }
            set { mstrArrCountryName = value; }
        }


        [DataMember]
        public AirField DepCode
        {
            get { return mobjDepCode; }
            set { mobjDepCode = value; }
        }

        [DataMember]
        public AirField ArrCode
        {
            get { return mobjArrCode; }
            set { mobjArrCode = value; }
        }

        #endregion
    }
}
