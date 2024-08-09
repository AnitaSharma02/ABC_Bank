using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ExperiencesCurrency
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string symbol { get; set; }
    }
}