using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class TypePriceByDateData
    {
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string weekday { get; set; }
        [DataMember]
        public bool available { get; set; }
        [DataMember]
        public List<TimeSlots> timeslots { get; set; }
        [DataMember]
        public List<Options> options { get; set; }
        [DataMember]
        public List<Rate> rates { get; set; }
    }
}