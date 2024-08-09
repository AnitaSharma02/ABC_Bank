using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class SearchExperiencesCriteria
    {
        [DataMember]
        public string searchTerm { get; set; }
        [DataMember]
        public List<string> categoryNames { get; set; }
        [DataMember]
        public List<string> typeNames { get; set; }
    }
}