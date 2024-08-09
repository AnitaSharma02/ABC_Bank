using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string salutation { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string phone { get; set; }
    }
}