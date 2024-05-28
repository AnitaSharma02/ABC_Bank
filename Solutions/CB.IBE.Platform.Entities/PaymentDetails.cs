using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PaymentDetails
    {
        #region Private Variables

        private string paymenttypeField = string.Empty;

        private string amountField = string.Empty;
        
        private string paymentstatusField = string.Empty;

        #endregion

        #region Properties

        [DataMember]
        public string paymenttype
        {
            get { return paymenttypeField; }
            set { paymenttypeField = value; }
        }

        [DataMember]
        public string amount
        {
            get { return amountField; }
            set { amountField = value; }
        }

        [DataMember]
        public string paymentstatus
        {
            get { return paymentstatusField; }
            set { paymentstatusField = value; }
        }

        #endregion

    }
}
