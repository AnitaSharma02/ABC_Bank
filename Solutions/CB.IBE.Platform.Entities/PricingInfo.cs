using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PricingInfo
    {
        private string indexField = string.Empty;

        private string farebasiscodeField = string.Empty;

        private string farekeyField = string.Empty;
        private string faretypeField = string.Empty;

        private PricingElements pricingElementsField=new PricingElements();


        /// <remarks/>
        /// 
        [DataMember]
        public string index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        
        [DataMember]
        /// <remarks/>
        public string farebasiscode
        {
            get
            {
                return this.farebasiscodeField;
            }
            set
            {
                this.farebasiscodeField = value;
            }
        }
        
        [DataMember]
        /// <remarks/>
        public string farekey
        {
            get
            {
                return this.farekeyField;
            }
            set
            {
                this.farekeyField = value;
            }
        }
        [DataMember]
        /// <remarks/>
        public string fareType
        {
            get
            {
                return this.faretypeField;
            }
            set
            {
                this.faretypeField = value;
            }
        }
        [DataMember]
        public PricingElements PricingElements
        {
            get
            {
                return this.pricingElementsField;
            }
            set
            {
                this.pricingElementsField = value;
            }
        }
    }
}
