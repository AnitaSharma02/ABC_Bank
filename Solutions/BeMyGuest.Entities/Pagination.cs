using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Pagination
    {
        [DataMember]
        public int total { get; set; } 
        [DataMember]
        public int count { get; set; } 
        [DataMember]
        public int per_page { get; set; } 
        [DataMember]
        public int current_page { get; set; } 
        [DataMember]
        public int total_pages { get; set; } 
    }
}