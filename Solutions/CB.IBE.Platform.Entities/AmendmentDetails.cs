using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class AmendmentDetails
    {
        #region Private Variables

        int mintId = 0;
        string mstrComments = string.Empty;
        string mstrAdditionalDetails1 = string.Empty;
        string mstrAdditionalDetails2 = string.Empty;
        string mstrTransactionReference = string.Empty;
        int mintPaymentStatus = 0;
        float mfltTotalAmount = 0.0f;
        float mfltServiceFees = 0.0f;
        float mfltAmendmentFees = 0.0f;
        string mstrPaymentType = string.Empty;
        string mstrBookingRefId = string.Empty;
        int mintBookingId = 0;
        int mintTotalPoints = 0;
        int mintRefererId = 0;
        int mintExpirationResetTime = 0;
        string mstrURL = string.Empty;
        string mstrUniqueRefId = string.Empty;
        string mstrIPAddress = string.Empty;
        string mstrMemberEmailId = string.Empty;
        DateTime mdtCreatedDate = DateTime.MinValue.ToUniversalTime();
        DateTime mdtExpiryDate = DateTime.MinValue.ToUniversalTime();
        string mstrCreatedBy = string.Empty;

        #endregion

        #region Properties

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
        /// Displays Comments
        /// </summary>
        [DataMember]
        public string Comments
        {
            get { return mstrComments; }
            set { mstrComments = value; }
        }

        /// <summary>
        /// Displays Additional Details1
        /// </summary>
        [DataMember]
        public string AdditionalDetails1
        {
            get { return mstrAdditionalDetails1; }
            set { mstrAdditionalDetails1 = value; }
        }

        /// <summary>
        /// Displays Additional Details2
        /// </summary>
        [DataMember]
        public string AdditionalDetails2
        {
            get { return mstrAdditionalDetails2; }
            set { mstrAdditionalDetails2 = value; }
        }


        /// <summary>
        /// Displays Transaction Reference
        /// </summary>
        [DataMember]
        public string TransactionReference
        {
            get { return mstrTransactionReference; }
            set { mstrTransactionReference = value; }
        }

        /// <summary>
        /// Displays Payment Status
        /// </summary>
        [DataMember]
        public int PaymentStatus
        {
            get { return mintPaymentStatus; }
            set { mintPaymentStatus = value; }
        }


        /// <summary>
        /// Displays Total Amount
        /// </summary>
        [DataMember]
        public float TotalAmount
        {
            get { return mfltTotalAmount; }
            set { mfltTotalAmount = value; }
        }

        /// <summary>
        /// Displays Service Fees
        /// </summary>
        [DataMember]
        public float ServiceFees
        {
            get { return mfltServiceFees; }
            set { mfltServiceFees = value; }
        }

        /// <summary>
        /// Displays Amendment Fees
        /// </summary>
        [DataMember]
        public float AmendmentFees
        {
            get { return mfltAmendmentFees; }
            set { mfltAmendmentFees = value; }
        }

        /// <summary>
        /// Displays Payment Type
        /// </summary>
        [DataMember]
        public string PaymentType
        {
            get { return mstrPaymentType; }
            set { mstrPaymentType = value; }
        }

        /// <summary>
        /// Displays Booking Ref Id
        /// </summary>
        [DataMember]
        public string BookingRefId
        {
            get { return mstrBookingRefId; }
            set { mstrBookingRefId = value; }
        }

        /// <summary>
        /// Displays Booking Id
        /// </summary>
        [DataMember]
        public int BookingId
        {
            get { return mintBookingId; }
            set { mintBookingId = value; }
        }

        /// <summary>
        /// Displays Total Points
        /// </summary>
        [DataMember]
        public int TotalPoints
        {
            get { return mintTotalPoints; }
            set { mintTotalPoints = value; }
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
        /// Displays Created By
        /// </summary>
        [DataMember]
        public string CreatedBy
        {
            get { return mstrCreatedBy; }
            set { mstrCreatedBy = value; }
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
        /// Displays ExpirationResetTime
        /// </summary>
        [DataMember]
        public int ExpirationResetTime
        {
            get { return mintExpirationResetTime; }
            set { mintExpirationResetTime = value; }
        }
        /// <summary>
        /// Displays URL
        /// </summary>
        [DataMember]
        public string URL
        {
            get { return mstrURL; }
            set { mstrURL = value; }
        }
        /// <summary>
        /// Displays UniqueRefId
        /// </summary>
        [DataMember]
        public string UniqueRefId
        {
            get { return mstrUniqueRefId; }
            set { mstrUniqueRefId = value; }
        }
        /// <summary>
        /// Displays IPAddress
        /// </summary>
        [DataMember]
        public string IPAddress
        {
            get { return mstrIPAddress; }
            set { mstrIPAddress = value; }
        }
        /// <summary>
        /// Displays MemberId
        /// </summary>
        [DataMember]
        public string MemberEmailId
        {
            get { return mstrMemberEmailId; }
            set { mstrMemberEmailId = value; }
        }
        /// <summary>
        /// Displays ExpiryDate
        /// </summary>
        [DataMember]
        public DateTime ExpiryDate
        {
            get { return mdtExpiryDate; }
            set { mdtExpiryDate = value; }
        }


        #endregion
    }
}
