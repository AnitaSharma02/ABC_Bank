using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PaxTax
    {
        private string codeField = string.Empty;

        private decimal amountField = 0;

        /// <remarks/>
        /// 
        [DataMember]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal Amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }
    }
}
