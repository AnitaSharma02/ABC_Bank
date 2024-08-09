using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductMeta
    {
        [DataMember]
        public Pagination pagination { get; set; }
    }
}