using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductsByFilterResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public List<ProductInfo> data { get; set; }
        [DataMember]
        public ProductMeta meta { get; set; }
        [DataMember]
        public DateTime? timestamp { get; set; }
    }
}