using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ExperiencesBookingRequestOptions
    {
        [DataMember]
        public List<ExperiencesPerBooking> perBooking { get; set; } = null;
        [DataMember]
        public List<List<ExperiencesPerPax>> perPax { get; set; } = null;
    }
}
