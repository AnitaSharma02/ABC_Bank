using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductTypeDetail
    {
        [DataMember]
        public List<ItemUUID> item_uuid { get; set; }
        [DataMember]
        public ProductSelectedDetails ProductSelectedDetails { get; set; }
    }
}