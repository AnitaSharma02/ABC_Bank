using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductsByFilterRequest
    {
        [DataMember]
        public int page { get; set; }
        [DataMember]
        public int per_page { get; set; }
    }
}