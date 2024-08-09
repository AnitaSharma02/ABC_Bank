using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ExperiencesTypesAndCategory
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public List<string> types { get; set; }
        [DataMember]
        public List<string> categories { get; set; }
    }
}