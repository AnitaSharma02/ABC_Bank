using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace CB.IBE.Platform.Masters.Entities
{
    [DataContract]
    [Serializable]
    public class RefererMarkup
    {
        #region Private Variables

        int mintId = 0;
        bool mboolIsPercentage = false;
        string mstrMarkupDetailCurrencyValue = string.Empty;
        bool mboolIsActive = false;
        int mintRefererId = 0;
        int mintSupplierId = 0;
        int mintTimeRangeFrom = 0;
        int mintTimeRangeTo = 0;
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
        /// Displays IsPercentage
        /// </summary>
        [DataMember]
        public bool IsPercentage
        {
            get { return mboolIsPercentage; }
            set { mboolIsPercentage = value; }
        }
        /// <summary>
        /// Displays MarkupDetailCurrencyValue
        /// </summary>
        [DataMember]
        public string MarkupDetailCurrencyValue
        {
            get { return mstrMarkupDetailCurrencyValue; }
            set { mstrMarkupDetailCurrencyValue = value; }
        }
        /// <summary>
        /// Displays IsActive
        /// </summary>
        [DataMember]
        public bool IsActive
        {
            get { return mboolIsActive; }
            set { mboolIsActive = value; }
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
        /// Displays TimeRange From
        /// </summary>
        [DataMember]
        public int TimeRangeFrom
        {
            get { return mintTimeRangeFrom; }
            set { mintTimeRangeFrom = value; }
        }
        /// <summary>
        /// Displays TimeRange To
        /// </summary>
        [DataMember]
        public int TimeRangeTo
        {
            get { return mintTimeRangeTo; }
            set { mintTimeRangeTo = value; }
        }
        /// <summary>
        /// Displays Supplier Id
        /// </summary>
        [DataMember]
        public int SupplierId
        {
            get { return mintSupplierId; }
            set { mintSupplierId = value; }
        }
        #endregion
    }
}
