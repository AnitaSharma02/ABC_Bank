using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Location
    {
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string country { get; set; }
    }
}