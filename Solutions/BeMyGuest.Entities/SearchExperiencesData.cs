using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class SearchExperiencesData
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string titleTranslated { get; set; }
        [DataMember]
        public string validFrom { get; set; }
        [DataMember]
        public string validThrough { get; set; }
        [DataMember]
        public decimal basePrice { get; set; } = 0;
        [DataMember]
        public decimal convertedAmount { get; set; } = 0;
        [DataMember]
        public string convertedCurrency { get; set; }
        [DataMember]
        public string typeName { get; set; }
        [DataMember]
        public string typeUuid { get; set; }
        [DataMember]
        public string imagebaseUrl { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string category { get; set; }
        [DataMember]
        public string updatedAt { get; set; }
    }
}
