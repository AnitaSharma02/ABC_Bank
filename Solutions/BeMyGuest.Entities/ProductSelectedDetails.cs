using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductSelectedDetails
    {
        [DataMember]
        public string puuid { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public int adultCount { get; set; }
        [DataMember]
        public int childrenCount { get; set; }
        [DataMember]
        public int seniorsCount { get; set; }
        [DataMember]
        public string ptuuid { get; set; }
        [DataMember]
        public string timeSlotUuid { get; set; }
    }
}