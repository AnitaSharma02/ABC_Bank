using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductInfoResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public ProductInfo data { get; set; }
        [DataMember]
        public ProductTypeDetail producttypedetails { get; set; }
        [DataMember]
        public SearchError error { get; set; }
    }
}