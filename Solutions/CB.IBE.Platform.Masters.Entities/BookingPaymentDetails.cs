using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Masters.Entities
{
    public class BookingPaymentDetails
    {
        private int mintId = 0;
        private int mintRefererId = 0;
        private float mfltPGCashAmount = 0.0f;
        private float mfltCashAmount = 0.0f;
        private float mfltDefaultCashAmount = 0.0f;
        private int mintPoints = 0;
        private PaymentType menumPaymentType;
        private ServiceType menumServiceType;
        private string mstrPointsTxnRefererence = string.Empty;
        private string mstrMemberId = string.Empty;
        private string mstrCashTxnRefererence = string.Empty;
        private string mstrBookingRefererence = string.Empty;
        private int mintBookingStatus = 0;
        PaymentStatus menumPaymentStatus;
        private DateTime mdtCreated_Date = DateTime.MinValue.ToUniversalTime();
        private string mstrCreatedBy = string.Empty;
        private DateTime mdtUpdated_Date = DateTime.MinValue.ToUniversalTime();
        private string mstrUpdatedBy = string.Empty;
        private List<BookingPaymentBreakage> mobjBookingPaymentBreakageList = new List<BookingPaymentBreakage>();



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
        /// Displays RefererId
        /// </summary>
        [DataMember]
        public int RefererId
        {
            get { return mintRefererId; }
            set { mintRefererId = value; }
        }

        /// <summary>

        /// Displays PGCashAmount
        /// </summary>
        [DataMember]
        public float PGCashAmount
        {
            get { return mfltPGCashAmount; }
            set { mfltPGCashAmount = value; }
        }
        /// <summary>

        /// Displays CashAmount
        /// </summary>
        [DataMember]
        public float CashAmount
        {
            get { return mfltCashAmount; }
            set { mfltCashAmount = value; }
        }
        /// <summary>

        /// Displays Default CashAmount
        /// </summary>
        [DataMember]
        public float DefaultCashAmount
        {
            get { return mfltDefaultCashAmount; }
            set { mfltDefaultCashAmount = value; }
        }
        /// <summary>
        /// Displays Id
        /// </summary>
        [DataMember]
        public int Points
        {
            get { return mintPoints; }
            set { mintPoints = value; }
        }
        /// <summary>
        /// Payment Type
        /// </summary>
        [DataMember]
        public PaymentType PaymentType
        {
            get { return menumPaymentType; }
            set { menumPaymentType = value; }
        }
        /// <summary>
        /// Service Type
        /// </summary>
        [DataMember]
        public ServiceType ServiceType
        {
            get { return menumServiceType; }
            set { menumServiceType = value; }
        }
        /// <summary>
        /// Payment Status
        /// </summary>
        [DataMember]
        public PaymentStatus PaymentStatus
        {
            get { return menumPaymentStatus; }
            set { menumPaymentStatus = value; }
        }
        /// <summary>
        /// PointsTxnRefererence
        /// </summary>
        [DataMember]
        public string PointsTxnRefererence
        {
            get { return mstrPointsTxnRefererence; }
            set { mstrPointsTxnRefererence = value; }
        }
        /// <summary>
        /// CashTxnRefererence
        /// </summary>
        [DataMember]
        public string CashTxnRefererence
        {
            get { return mstrCashTxnRefererence; }
            set { mstrCashTxnRefererence = value; }
        }
        /// <summary>
        /// MemberId
        /// </summary>
        [DataMember]
        public string MemberId
        {
            get { return mstrMemberId; }
            set { mstrMemberId = value; }
        }
        /// <summary>
        /// BookingRefererence
        /// </summary>
        [DataMember]
        public string BookingRefererence
        {
            get { return mstrBookingRefererence; }
            set { mstrBookingRefererence = value; }
        }
        /// <summary>
        /// Displays BookingStatus
        /// </summary>
        [DataMember]
        public int BookingStatus
        {
            get { return mintBookingStatus; }
            set { mintBookingStatus = value; }
        }
        /// <summary>
        /// Created_Date
        /// </summary>
        [DataMember]
        public DateTime Created_Date
        {
            get { return mdtCreated_Date; }
            set { mdtCreated_Date = value; }
        }
        /// <summary>
        /// CreatedBy
        /// </summary>
        [DataMember]
        public string CreatedBy
        {
            get { return mstrCreatedBy; }
            set { mstrCreatedBy = value; }
        }
        /// <summary>
        /// Updated_Date
        /// </summary>
        [DataMember]
        public DateTime Updated_Date
        {
            get { return mdtUpdated_Date; }
            set { mdtUpdated_Date = value; }
        }
        /// <summary>
        /// UpdatedBy
        /// </summary>
        [DataMember]
        public string UpdatedBy
        {
            get { return mstrUpdatedBy; }
            set { mstrUpdatedBy = value; }
        }

        /// <summary>
        /// displays BookingPaymentBreakage List
        /// </summary>
        [DataMember]
        public List<BookingPaymentBreakage> BookingPaymentBreakageList
        {
            get { return mobjBookingPaymentBreakageList; }
            set { mobjBookingPaymentBreakageList = value; }
        }
    }
}
