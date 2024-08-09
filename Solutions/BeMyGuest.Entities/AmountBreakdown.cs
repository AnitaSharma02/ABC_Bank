using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class AmountBreakdown
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public string price { get; set; }
        [DataMember]
        public decimal convertedAmount { get; set; } = 0;
    }
}
