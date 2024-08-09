using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class SearchExperiencesResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public List<SearchExperiencesData> data { get; set; }
        [DataMember]
        public ProductMeta meta { get; set; }
        [DataMember]
        public SearchExperiencesCriteria searchExperiencesCriteria { get; set; }
    }
}
