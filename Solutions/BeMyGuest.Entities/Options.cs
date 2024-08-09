using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Options
    {
        [DataMember]
        public List<ExperiencesPerBooking> perBooking { get; set; }
        [DataMember]
        public List<ExperiencesPerPax> perPax { get; set; }
    }
}