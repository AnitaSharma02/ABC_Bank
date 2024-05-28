using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Masters.Entities
{
    public class BookingPaymentBreakage
    {
        #region Private Variables
        int mintId = 0;
        string mstrCurrency = string.Empty;
        float mfltAmount = 0.0f;
        string mstrTxnReference = string.Empty;
        string mstrBookingReferenceId = string.Empty;
        int mintBookingPaymentId = 0;
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
        /// Displays Currency
        /// </summary>
        [DataMember]
        public string Currency
        {
            get { return mstrCurrency; }
            set { mstrCurrency = value; }
        }

        /// <summary>
        /// Displays Amount
        /// </summary>
        [DataMember]
        public float Amount
        {
            get { return mfltAmount; }
            set { mfltAmount = value; }
        }

        /// <summary>
        /// Displays TxnReference
        /// </summary>
        [DataMember]
        public string TxnReference
        {
            get { return mstrTxnReference; }
            set { mstrTxnReference = value; }
        }

        /// <summary>
        /// Displays BookingReferenceId
        /// </summary>
        [DataMember]
        public string BookingReferenceId
        {
            get { return mstrBookingReferenceId; }
            set { mstrBookingReferenceId = value; }
        }

        /// <summary>
        /// Displays BookingPaymentId
        /// </summary>
        [DataMember]
        public int BookingPaymentId
        {
            get { return mintBookingPaymentId; }
            set { mintBookingPaymentId = value; }
        }

        #endregion
    }
}
