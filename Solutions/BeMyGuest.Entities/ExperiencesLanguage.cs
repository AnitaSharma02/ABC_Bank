using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ExperiencesLanguage
    {
        [DataMember]
        public string name { get; set; }
    }
}