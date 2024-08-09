using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ExperiencesPerBookingItem
    {
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string labelTranslated { get; set; }
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public decimal? price { get; set; }
    }
}