using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductTypesPriceByDateRequest
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string pointConvrtRate { get; set; }
    }
}