using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PaxPricingInfoList
    {
        private List<PaxPricingInfo> paxPricingInfoField=new List<PaxPricingInfo>();

        [DataMember]
        public List<PaxPricingInfo> PaxPricingInfo 
         {
             get { return paxPricingInfoField; } 
             set { paxPricingInfoField = value; } 
         }

    }
}
