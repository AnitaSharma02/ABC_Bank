using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class PNRDetails
    {
        #region Private Variables

        ItineraryDetails mlobjItineraryDetails = new ItineraryDetails();
        int mintId = 0;
        string mstrGDSPNR = string.Empty;
        string mstrAirLinePNR = string.Empty;
        string mstrTotalBasePrice = string.Empty;
        string mstrTotalDefaultPrice = string.Empty;
        string mstrTotalTxnPrice = string.Empty;
        DateTime mdtCreatedDate = DateTime.MinValue.ToUniversalTime();
        string mstrIPAddress = string.Empty;
        string mstrRefereralIdentifier = string.Empty;
        int mintRefererId = 0;
        string mstrBookingReference = string.Empty;
        string mstrUserReference = string.Empty;
        int mintStatus = 0;
        int mintSupplierId = 0;
        string mstrSessionId = string.Empty;
        string mstrTripId = string.Empty;
        int mintTotalPoints = 0;
        bool mbIsActive = true;
        string mstrContactNo = string.Empty;
        string mstrEmailID = string.Empty;
        #endregion



        #region Properties

        [DataMember]
        public ItineraryDetails ItineraryDetails
        {
            get { return mlobjItineraryDetails; }
            set { mlobjItineraryDetails = value; }
        }

        /// <summary>
        /// Displays AirLinePNR
        /// </summary>
        [DataMember]
        public string AirLinePNR
        {
            get { return mstrAirLinePNR; }
            set { mstrAirLinePNR = value; }
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
        /// Displays GDSPNR
        /// </summary>
        [DataMember]
        public string GDSPNR
        {
            get { return mstrGDSPNR; }
            set { mstrGDSPNR = value; }
        }
        /// <summary>
        /// Displays Total Base Price
        /// </summary>
        [DataMember]
        public string TotalBasePrice
        {
            get { return mstrTotalBasePrice; }
            set { mstrTotalBasePrice = value; }
        }
        /// <summary>
        /// Displays Total Default Price
        /// </summary>
        [DataMember]
        public string TotalDefaultPrice
        {
            get { return mstrTotalDefaultPrice; }
            set { mstrTotalDefaultPrice = value; }
        }
        /// <summary>
        /// Displays Total Txn Price
        /// </summary>
        [DataMember]
        public string TotalTxnPrice
        {
            get { return mstrTotalTxnPrice; }
            set { mstrTotalTxnPrice = value; }
        }
        /// <summary>
        /// Displays Created Date
        /// </summary>
        [DataMember]
        public DateTime CreatedDate
        {
            get { return mdtCreatedDate; }
            set { mdtCreatedDate = value; }
        }
        /// <summary>
        /// Displays IP Address
        /// </summary>
        [DataMember]
        public string IPAddress
        {
            get { return mstrIPAddress; }
            set { mstrIPAddress = value; }
        }
        /// <summary>
        /// Displays Refereral Identifier
        /// </summary>
        [DataMember]
        public string RefereralIdentifier
        {
            get { return mstrRefereralIdentifier; }
            set { mstrRefereralIdentifier = value; }
        }
        /// <summary>
        /// Displays Referer Id
        /// </summary>
        [DataMember]
        public int RefererId
        {
            get { return mintRefererId; }
            set { mintRefererId = value; }
        }
        /// <summary>
        /// Displays Booking Reference
        /// </summary>
        [DataMember]
        public string BookingReference
        {
            get { return mstrBookingReference; }
            set { mstrBookingReference = value; }
        }
        /// <summary>
        /// Displays User Reference
        /// </summary>
        [DataMember]
        public string UserReference
        {
            get { return mstrUserReference; }
            set { mstrUserReference = value; }
        }
        
        /// <summary>
        /// Displays Status
        /// </summary>
        [DataMember]
        public int Status
        {
            get { return mintStatus; }
            set { mintStatus = value; }
        }

        /// <summary>
        /// Displays SupplierId
        /// </summary>
        [DataMember]
        public int SupplierId
        {
            get { return mintSupplierId; }
            set { mintSupplierId = value; }
        }

        
        /// <summary>
        /// Displays SessionId
        /// </summary>
        [DataMember]
        public string SessionId
        {
            get { return mstrSessionId; }
            set { mstrSessionId = value; }
        }
        /// <summary>
        /// Displays Trip Id
        /// </summary>
        [DataMember]
        public string TripId
        {
            get { return mstrTripId; }
            set { mstrTripId = value; }
        }
        /// <summary>
        /// Displays SupplierId
        /// </summary>
        [DataMember]
        public int TotalPoints
        {
            get { return mintTotalPoints; }
            set { mintTotalPoints = value; }
        }
        /// <summary>
        /// Displays IsActive
        /// </summary>
        [DataMember]
        public bool IsActive
        {
            get { return mbIsActive; }
            set { mbIsActive = value; }
        }
        /// <summary>
        /// Displays ContactNo
        /// </summary>
        [DataMember]
        public string ContactNo
        {
            get { return mstrContactNo; }
            set { mstrContactNo = value; }
        }
        /// <summary>
        /// Displays EmailID
        /// </summary>
        [DataMember]
        public string EmailID
        {
            get { return mstrEmailID; }
            set { mstrEmailID = value; }
        }
        #endregion
    }
}
