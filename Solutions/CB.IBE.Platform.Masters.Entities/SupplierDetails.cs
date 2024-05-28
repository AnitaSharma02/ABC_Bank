using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace CB.IBE.Platform.Masters.Entities
{

    [DataContract]
    [Serializable]
    public class SupplierDetails
    {
        #region Private Variables
        int mintId = 0;
        string mstrDescription = string.Empty;
        string mstrServiceType = string.Empty;
        bool mboolIsOffline = false;
        DateTime mdateCreatedDate = DateTime.MinValue.ToUniversalTime();
        int mintMID = 0;
        string mstrC2BShortCode = string.Empty;
        #endregion
        #region Poperties
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
        /// Displays Description
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return mstrDescription; }
            set { mstrDescription = value; }
        }
        /// <summary>
        /// Displays ServiceType
        /// </summary>
        [DataMember]
        public string ServiceType
        {
            get { return mstrServiceType; }
            set { mstrServiceType = value; }
        }
        /// <summary>
        /// Displays IsOffline
        /// </summary>
        [DataMember]
        public bool IsOffline
        {
            get { return mboolIsOffline; }
            set { mboolIsOffline = value; }
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
        /// Displays MID
        /// </summary>
        [DataMember]
        public int MID
        {
            get { return mintMID; }
            set { mintMID = value; }
        }
        /// <summary>
        /// Displays C2BShortCode
        /// </summary>
        [DataMember]
        public string C2BShortCode
        {
            get { return mstrC2BShortCode; }
            set { mstrC2BShortCode = value; }
        }
        #endregion

    }
}
