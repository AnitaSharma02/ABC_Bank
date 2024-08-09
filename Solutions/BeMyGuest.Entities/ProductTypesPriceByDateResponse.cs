using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductTypesPriceByDateResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public TypePriceByDateData data { get; set; }
        [DataMember]
        public DateTime? timestamp { get; set; }
    }
}