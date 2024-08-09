using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ItemUUID
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public TypeInfo typeinfo { get; set; }
        [DataMember]
        public TypePriceByDateData typePriceByDate { get; set; }
    }
}
