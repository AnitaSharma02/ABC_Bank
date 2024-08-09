using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class TicketType
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public bool? allowed { get; set; }
        [DataMember]
        public int min { get; set; }
        [DataMember]
        public int max { get; set; }
        [DataMember]
        public int minAge { get; set; }
        [DataMember]
        public int maxAge { get; set; }
        [DataMember]
        public decimal? recommendedMarkup { get; set; }
        [DataMember]
        public decimal? parityPrice { get; set; }
        [DataMember]
        public decimal? gateRatePrice { get; set; }
        [DataMember]
        public int quantity { get; set; }
    }
}