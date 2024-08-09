using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class TimeSlots
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string startTime { get; set; }
        [DataMember]
        public string endTime { get; set; }
    }
}