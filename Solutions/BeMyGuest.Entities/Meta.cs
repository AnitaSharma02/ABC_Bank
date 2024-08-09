using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Meta
    {
        [DataMember]
        public double? markup { get; set; }
    }
}