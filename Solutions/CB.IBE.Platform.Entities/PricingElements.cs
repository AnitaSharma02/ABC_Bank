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
    public class PricingElements
    {
        private PricingElement[] pricingElementsField;

        [DataMember]
        public PricingElement[] PricingElement { get { return pricingElementsField; } set { pricingElementsField = value; } }
    }
}
