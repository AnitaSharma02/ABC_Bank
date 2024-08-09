using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Photo
    {
        [DataMember]
        public Paths paths { get; set; }
    }
}