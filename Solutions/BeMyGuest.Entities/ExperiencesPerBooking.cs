using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ExperiencesPerBooking
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string nameTranslated { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string descriptionTranslated { get; set; }
        [DataMember]
        [JsonProperty("required")]
        public bool IsRequired { get; set; } = false;
        [DataMember]
        public bool addOn { get; set; } = false;
        [DataMember]
        public string formatRegex { get; set; }
        [DataMember]
        public int inputType { get; set; } = 0;
        [DataMember]
        public int minNumber { get; set; }
        [DataMember]
        public int maxNumber { get; set; }
        [DataMember]
        public string validFrom { get; set; }
        [DataMember]
        public string validTo { get; set; }
        [DataMember]
        public decimal? price { get; set; }
        [DataMember]
        public List<ExperiencesPerBookingItem> items { get; set; }
        [DataMember]
        public string value { get; set; }
    }
}