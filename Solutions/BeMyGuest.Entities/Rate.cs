using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Rate
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public Meta meta { get; set; }
        [DataMember]
        public string currency { get; set; }
        [DataMember]
        public decimal amount { get; set; }
        [DataMember]
        public string format { get; set; }
        [DataMember]
        public string changedformat { get; set; }
        [DataMember]
        public string convertedCurrency { get; set; }
    }
}