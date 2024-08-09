using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Validity
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public int days { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public bool? hasBatchValidityDate { get; set; }
    }
}