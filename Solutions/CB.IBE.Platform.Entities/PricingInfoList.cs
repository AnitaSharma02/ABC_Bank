using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PricingInfoList
    {
        private List<PricingInfo> pricingInfoField;

        [DataMember]
        public List<PricingInfo> PricingInfo { get { return pricingInfoField; } set { pricingInfoField = value; } }

    }
}
