using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    /// <remarks/>
    [Serializable]
    [DataContract]
    public class PricingElement
    {
        private string categoryField = string.Empty;

        private string codeField = string.Empty;

        private string amountField = string.Empty;

        /// <remarks/>
        /// 
        [DataMember]
        public string category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string code
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
        public string amount
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
