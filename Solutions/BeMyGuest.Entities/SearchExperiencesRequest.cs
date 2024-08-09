using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class SearchExperiencesRequest
    {
        [DataMember]
        public string search_term { get; set; }
        [DataMember]
        public List<string> type_name { get; set; }
        [DataMember]
        public List<string> category { get; set; }
        [DataMember]
        public int page { get; set; }
        [DataMember]
        public int per_page { get; set; }
    }
}
