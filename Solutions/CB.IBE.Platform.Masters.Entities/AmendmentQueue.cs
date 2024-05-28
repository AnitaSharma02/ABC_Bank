using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Masters.Entities
{
    [DataContract]
    [Serializable]
    public class AmendmentQueue
    {
        #region Private Variables

        int mintId = 0;
        string mstrMemberId = string.Empty;
        string mstrBookingRefId = string.Empty;
        string mstrAmendmentDetails = string.Empty;
        string mstrAdditionalDetails1 = string.Empty;
        string mstrAdditionalDetails2 = string.Empty;
        RefererDetails lobjRefererDetails = new RefererDetails();
        DateTime mdtCreatedOn = DateTime.MinValue.ToUniversalTime();
        string mstrUpdatedBy = string.Empty;
        DateTime mdtUpdatedOn = DateTime.MinValue.ToUniversalTime();
        AmendmentStatus menumAmendmentStatus;
        
        #endregion

        #region Poperties

        [DataMember]
        public int Id
        {
            get { return mintId; }
            set { mintId = value; }
        }

        [DataMember]
        public string MemberId
        {
            get { return mstrMemberId; }
            set { mstrMemberId = value; }
        }

        [DataMember]
        public string BookingRefId
        {
            get { return mstrBookingRefId; }
            set { mstrBookingRefId = value; }
        }

        [DataMember]
        public string AmendmentDetails
        {
            get { return mstrAmendmentDetails; }
            set { mstrAmendmentDetails = value; }
        }

        [DataMember]
        public string AdditionalDetails1
        {
            get { return mstrAdditionalDetails1; }
            set { mstrAdditionalDetails1 = value; }
        }

        [DataMember]
        public string AdditionalDetails2
        {
            get { return mstrAdditionalDetails2; }
            set { mstrAdditionalDetails2 = value; }
        }

        [DataMember]
        public RefererDetails RefererDetails
        {
            get { return lobjRefererDetails; }
            set { lobjRefererDetails = value; }
        }

        [DataMember]
        public DateTime CreatedOn
        {
            get { return mdtCreatedOn; }
            set { mdtCreatedOn = value; }
        }

        [DataMember]
        public DateTime UpdatedOn
        {
            get { return mdtUpdatedOn; }
            set { mdtUpdatedOn = value; }
        }

        [DataMember]
        public string UpdatedBy
        {
            get { return mstrUpdatedBy; }
            set { mstrUpdatedBy = value; }
        }

        [DataMember]
        public AmendmentStatus AmendmentStatus
        {
            get { return menumAmendmentStatus; }
            set { menumAmendmentStatus = value; }
        }

        #endregion
    }
}
